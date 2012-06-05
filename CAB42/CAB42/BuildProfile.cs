//-----------------------------------------------------------------------
// <copyright file="BuildProfile.cs" company="42A Consulting">
//     Copyright 2011 42A Consulting
//     Licensed under the Apache License, Version 2.0 (the "License");
//     you may not use this file except in compliance with the License.
//     You may obtain a copy of the License at
//     
//     http://www.apache.org/licenses/LICENSE-2.0
//     
//     Unless required by applicable law or agreed to in writing, software
//     distributed under the License is distributed on an "AS IS" BASIS,
//     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//     See the License for the specific language governing permissions and
//     limitations under the License.
// </copyright>
//-----------------------------------------------------------------------
namespace C42A.CAB42
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// A class that describes a build profile for a project.
    /// </summary>
    public class BuildProfile
    {
        /// <summary>
        /// This profile's parent profile, if any.
        /// </summary>
        private BuildProfile parent;

        /// <summary>
        /// This profile's parent profile name.
        /// </summary>
        private string parentName;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProfile"/> class.
        /// </summary>
        public BuildProfile()
        {
            this.Excludes = new ExcludeRuleCollection();
            this.Includes = new IncludeRuleCollection();
            this.Variables = new UserVariableCollection();
        }

        /// <summary>
        /// Gets or sets the name of this profile.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this build profile is a placeholder, or if it should be used to build a CAB file.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If this property is set to true, no CAB file will be built with this profile. Instead, this profile
        ///         can be used only as a placeholder for common settings shared (inherited) by other profiles.
        ///     </para>
        /// </remarks>
        public bool IsPlaceholder { get; set; }

        /// <summary>
        /// Gets or sets the name of the parent build profile.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If <see cref="ProjectInfo"/> is not null and the name being set can be found in the project's profile collection,
        ///         <see cref="Parent"/> will be set to the value returned by <see cref="ProjectInfo.FindProfile"/>.
        ///     </para>
        /// </remarks>
        public string InheritsFrom
        {
            get
            {
                return this.parentName;
            }

            set
            {
                if (value != this.parentName)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        this.parentName = value;

                        if (this.ProjectInfo != null && this.ProjectInfo.Profiles != null)
                        {
                            // Use the Parent property to also call this InheritsFrom property recursivly,
                            // to reflect the exact spelling of the parents name.
                            this.Parent = this.ProjectInfo.Profiles.FindProfile(value);
                        }
                        else
                        {
                            // Using the Parent property here will cause a recursive call to this InheritsFrom property,
                            // setting also InheritsFrom to null. So we use the parent field instead.
                            this.parent = null;
                        }
                    }
                    else
                    {
                        this.parentName = null;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the project which this build profile belongs to.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The profile is referenced by the project, and will therefor be located within the serialized
        ///         project object. This property is ignored when serializing to prevent circular reference.
        ///     </para>
        /// </remarks>
        [XmlIgnore]
        public ProjectInfo ProjectInfo { get; set; }

        /// <summary>
        /// Gets or sets this profile's parent profile.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This property is ignored when serializing. 
        ///         Instead, the <see cref="InheritsFrom"/> property is used to reflect the parent profile.
        ///     </para>
        /// </remarks>
        [XmlIgnore]
        public BuildProfile Parent
        {
            get
            {
                return this.parent;
            }

            set
            {
                this.parent = value;
                this.InheritsFrom = value != null ? value.Name : null;
            }
        }

        /// <summary>
        /// Gets or sets the collection of exclude rules for this profile.
        /// </summary>
        public ExcludeRuleCollection Excludes { get; set; }

        /// <summary>
        /// Gets or sets the collection of include rules for this profile.
        /// </summary>
        public IncludeRuleCollection Includes { get; set; }

        [XmlIgnore]
        public UserVariableCollection Variables { get; set; }

        public UserVariable[] UserVariables
        {
            get
            {
                return this.Variables.Values.ToArray();
            }

            set
            {
                if (value != null && value.Length > 0)
                {
                    var variables = new UserVariableCollection();

                    foreach (var variable in value)
                    {
                        variables.Add(variable);
                    }

                    this.Variables = variables;
                }
                else
                {
                    this.Variables.Clear();
                }
            }
        }

        public string GetOutputDirectory()
        {
            return this.ProjectInfo.GetOutputDirectory(this);
        }

        public string GetOutputFileName()
        {
            return this.ProjectInfo.GetOutputFileName(this);
        }

        public UserVariable[] GetVariables(bool inheritance = true)
        {
            var variables = new Dictionary<string, UserVariable>();

            if (inheritance)
            {
                if (this.Parent != null)
                {
                    this.AddVariables(variables, this.parent.GetVariables(true));
                }
                else if (this.ProjectInfo != null && this.ProjectInfo.GlobalUserVariables != null)
                {
                    // This is the top parent... add the global user variables
                    this.AddVariables(variables, this.ProjectInfo.GlobalUserVariables);
                }
            }

            this.AddVariables(variables, this.UserVariables);

            return variables.Values.ToArray();
        }

        public IncludeRuleCollection GetFiles()
        {
            var l = new IncludeRuleCollection();

            if (this.Parent != null)
            {
                l.AddRange(this.Parent.GetFiles());
            }

            if (this.Includes != null)
            {
                l.AddRange(this.Includes);
            }

            return l;
        }

        /// <summary>
        /// Determines whether <paramref name="path"/> is excluded by this profile, or any of it's parents.
        /// </summary>
        /// <param name="path">The path to search for.</param>
        /// <param name="inheritance">If true, this profile's parent and the project global exclude list will be included in the search.</param>
        /// <returns>
        ///     A value indicating whether <paramref name="path"/> is excluded by this profile.
        /// </returns>
        public bool IsExcluded(string path, bool inheritance = true)
        {
            if (this.Excludes != null && this.Excludes.IsExcluded(path))
            {
                return true;
            }
            else if (this.Parent != null && inheritance)
            {
                return this.Parent.IsExcluded(path, true);
            }
            else if (this.ProjectInfo != null && inheritance)
            {
                return this.ProjectInfo.GlobalExcludeRules.IsExcluded(path);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether <paramref name="profile"/> is located in this profile's family tree.
        /// </summary>
        /// <param name="profile">The profile to look for in the parent chain.</param>
        /// <returns>A value indicating whether <paramref name="profile"/> is located in this profile's family tree.</returns>
        public bool IsSubProfileOf(BuildProfile profile)
        {
            if (this == profile)
            {
                return true;
            }
            else if (this.Parent != null)
            {
                return this.Parent.IsSubProfileOf(profile);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a string that represents the <see cref="BuildProfile"/> object.
        /// </summary>
        /// <returns>Returns the <see cref="Name"/> property.</returns>
        public override string ToString()
        {
            return this.Name;
        }

        private void AddVariables(Dictionary<string, UserVariable> variables, UserVariableCollection collection)
        {
            this.AddVariables(variables, collection.Values);
        }

        private void AddVariables(Dictionary<string, UserVariable> variables, IEnumerable<UserVariable> collection)
        {
            foreach (var variable in collection)
            {
                this.AddVariable(variables, variable);
            }
        }

        private void AddVariable(Dictionary<string, UserVariable> variables, UserVariable variable)
        {
            if (variables.ContainsKey(variable.Name))
            {
                variables[variable.Name] = variable;
            }
            else
            {
                variables.Add(variable.Name, variable);
            }
        }
    }
}
