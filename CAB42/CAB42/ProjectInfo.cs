//-----------------------------------------------------------------------
// <copyright file="ProjectInfo.cs" company="42A Consulting">
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
    /// A CAB build project.
    /// </summary>
    public partial class ProjectInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectInfo"/> class.
        /// </summary>
        public ProjectInfo()
        {
            this.GlobalExcludeRules = new ExcludeRuleCollection();
            this.GlobalUserVariables = new UserVariableCollection();
            this.GlobalRegistryKeys = new RegistryKeyCollection();
            this.GlobalIncludeRules = new IncludeRuleCollection();
            this.Output = new ProjectOutput(this);
            this.Profiles = new BuildProfileCollection(this);

            this.OutputPath = "bin";
            this.BuildPath = @"obj\$(Profile)";
        }

        /// <summary>
        /// Gets or sets information about the file on the filesystem from which this project was opened.
        /// </summary>
        [XmlIgnore]
        public System.IO.FileInfo ProjectFile { get; set; }

        /// <summary>
        /// Gets the <see cref="Output"/> object for this project.
        /// </summary>
        [XmlIgnore]
        public ProjectOutput Output { get; private set; }

        /// <summary>
        /// Gets or sets the output filename (without extension) for this project.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The value in this property will be used parsed and variables replaced by it's respective values
        ///         when producing the final output filename used when building the project.
        ///     </para>
        /// </remarks>
        public string OutputFileName { get; set; }

        /// <summary>
        /// Gets or sets the path to the output directory for this build project, relative to <see cref="ProjectFile"/>.
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// Gets or sets the path to a directory where temporary files will be located during the build process.
        /// </summary>
        public string BuildPath { get; set; }

        /// <summary>
        /// Gets or sets the name of the application this build project is used for.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the name which created the application used by this build project.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Strings longer than 32 characters will be truncated when installed on smart devices.
        ///     </para>
        /// </remarks>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the release name for this build project. Example: RC1
        /// </summary>
        public string ReleaseName { get; set; }

        /// <summary>
        /// Gets or sets the version of the application.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This property is ignored when serializing, as <see cref="System.Version"/> is a 
        ///         read-only class. Instead, the <see cref="ProjectVersionString"/> is used as a work-around.
        ///     </para>
        /// </remarks>
        [XmlIgnore]
        public Version ProjectVersion { get; set; }

        /// <summary>
        /// Gets or sets a string describing the project's version as defined by <see cref="ProjectVersion"/>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This property is mainly used as a work-around for the XML serialization, 
        ///         as the <see cref="System.Version"/> is a read-only class.
        ///     </para>
        /// </remarks>
        [XmlElement("ProjectVersion")]
        public string ProjectVersionString
        {
            get
            {
                if (this.ProjectVersion == null)
                {
                    return null;
                }
                else
                {
                    return this.ProjectVersion.ToString();
                }
            }

            set
            {
                // The below call will throw exception if not well-formatted.
                this.ProjectVersion = Version.Parse(value);
            }
        }

        /// <summary>
        /// Gets or sets the collection of global user variables.
        /// </summary>
        [XmlIgnore]
        public UserVariableCollection GlobalUserVariables { get; set; }

        /// <summary>
        /// Gets or sets the collection of user variables.
        /// This property should not be used from code. It is only a placeholder to enable XML serialization. 
        /// Use the <see cref="GlobalUserVariables"/> property instead.
        /// </summary>
        public UserVariable[] UserVariables
        {
            get
            {
                return this.GlobalUserVariables.Values.ToArray();
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

                    this.GlobalUserVariables = variables;
                }
                else
                {
                    this.GlobalUserVariables.Clear();
                }
            }
        }

        /// <summary>
        /// Gets or sets the global collection of registry keys.
        /// </summary>
        [XmlIgnore]
        public RegistryKeyCollection GlobalRegistryKeys { get; set; }

        /// <summary>
        /// Gets or sets the global collection of registry keys. 
        /// This property should not be used from code. It is only a placeholder to enable XML serialization. 
        /// Use the <see cref="GlobalRegistryKeys"/> property instead.
        /// </summary>
        [XmlArray, XmlArrayItem("Key")]
        public RegistryKey[] RegistryKeys
        {
            get
            {
                return this.GlobalRegistryKeys.Values.ToArray();
            }

            set
            {
                if (value != null && value.Length > 0)
                {
                    var variables = new RegistryKeyCollection();

                    foreach (var variable in value)
                    {
                        variables.Add(variable);
                    }

                    this.GlobalRegistryKeys = variables;
                }
                else
                {
                    this.GlobalRegistryKeys.Clear();
                }
            }
        }

        /// <summary>
        /// Gets or sets the collection of include rules for this project.
        /// </summary>
        public IncludeRuleCollection GlobalIncludeRules { get; set; }

        /// <summary>
        /// Gets or sets the collection of global exclude rules for this project.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Global exclude rules applies always, regardless of which build profile is currently used.
        ///     </para>
        /// </remarks>
        public ExcludeRuleCollection GlobalExcludeRules { get; set; }

        /// <summary>
        /// Gets or sets the collection of build profiles for this project.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This property is ignored when serializing, as the profile collection is a dictionary.
        ///         Instead, the <see cref="BuildProfiles"/> property is used (which will return a array).
        ///     </para>
        /// </remarks>
        [XmlIgnore]
        public BuildProfileCollection Profiles { get; set; }

        /// <summary>
        /// Gets or sets the currently selected profile.
        /// </summary>
        [XmlIgnore]
        public BuildProfile CurrentProfile { get; set; }

        /// <summary>
        /// Gets or sets the build profiles used by this project.
        /// </summary>
        /// /<remarks>
        ///     <para>
        ///         When setting this property, the elements of the passed array will
        ///         added to the <see cref="Profiles"/> collection.
        ///     </para>
        ///     <para>
        ///         After all profiles has been added, their inheritance will be resolved.
        ///     </para>
        /// </remarks>
        public BuildProfile[] BuildProfiles
        {
            get
            {
                return this.Profiles.Values.ToArray();
            }

            set
            {
                if (value != null && value.Length > 0)
                {
                    var profiles = new BuildProfileCollection(this);

                    // 1st pass... add all profiles to the collection
                    foreach (var profile in value)
                    {
                        if (this.CurrentProfile == null)
                        {
                            this.CurrentProfile = profile;
                        }

                        profiles.Add(profile, false);
                    }

                    // 2nd pass... build family tree
                    foreach (var profile in value)
                    {
                        if (!string.IsNullOrEmpty(profile.InheritsFrom))
                        {
                            if (!profiles.ContainsKey(profile.InheritsFrom))
                            {
                                throw new InvalidOperationException(string.Format(
                                    "The profile '{0}' references a profile '{1}' that could not be found.",
                                    profile.Name,
                                    profile.InheritsFrom));
                            }

                            profile.Parent = profiles[profile.InheritsFrom];
                        }
                    }

                    this.Profiles = profiles;
                }
                else
                {
                    this.Profiles.Clear();
                }
            }
        }

        /// <summary>
        /// Returns build tasks for this project.
        /// </summary>
        /// <returns>A collection of build tasks.</returns>
        public IBuildTask[] CreateBuildTasks()
        {
            return BuildProfileTask.CreateBuildTasks(this);
        }

        /// <summary>
        /// Returns a collection of include rules.
        /// </summary>
        /// <param name="profile">The profile to use to build the include rule collection. Set this parameter to null if the currently selected profile should be used instead.</param>
        /// <returns>A collection of include rules.</returns>
        public IncludeRuleCollection GetFiles(BuildProfile profile = null)
        {
            var l = new IncludeRuleCollection();

            if (this.GlobalIncludeRules != null)
            {
                l.AddRange(this.GlobalIncludeRules);
            }
            
            if (profile != null)
            {
                l.AddRange(profile.GetFiles());
            }
            else if (this.CurrentProfile != null)
            {
                l.AddRange(this.CurrentProfile.GetFiles());
            }

            return l;
        }

        /// <summary>
        /// Prepares outout for a specified profile.
        /// </summary>
        /// <param name="profile">The profile for which the output should be generated.</param>
        /// <returns>A object which describes which files to include and where they should be stored in the final output.</returns>
        /// <remarks>
        ///     <para>
        ///         Currently, this method is merly a placeholder/workaround for future implementations as the architecture may change.
        ///     </para>
        /// </remarks>
        public ProjectOutput GetOutput(BuildProfile profile = null)
        {
            var output = this.Output;

            output.Refresh(profile);

            return output;
        }

        /// <summary>
        /// Determines whether <paramref name="path"/> is excluded by the global settings or the current selected profile.
        /// </summary>
        /// <param name="path">The path to deterine wether it is excluded.</param>
        /// <returns>A value indicating whether <paramref name="path"/> is excluded globally or by the <see cref="CurrentProfile"/>.</returns>
        public bool IsExcluded(string path, BuildProfile profile = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            if (System.IO.Path.IsPathRooted(path) && this.IsExcludedExact(this.GetRelativePath(path), profile))
            {
                return true;
            }
            else
            {
                return this.IsExcludedExact(path, profile);
            }
        }

        /// <summary>
        /// Determines whether <paramref name="path"/> is excluded by the global settings or the current selected profile.
        /// </summary>
        /// <param name="path">The path to deterine wether it is excluded.</param>
        /// <returns>A value indicating whether <paramref name="path"/> is excluded globally or by the <see cref="CurrentProfile"/>.</returns>
        public bool IsExcludedExact(string path, BuildProfile profile = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            else if (this.GlobalExcludeRules.IsExcluded(path))
            {
                return true;
            }
            else if (profile != null)
            {
                return profile.IsExcluded(path);
            }
            else if (this.CurrentProfile != null)
            {
                return this.CurrentProfile.IsExcluded(path);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds the specified path to a exclude list, as determined by the <paramref name="option"/> argument.
        /// </summary>
        /// <param name="path">The path to ignore.</param>
        /// <param name="option">How the path should be ignored.</param>
        /// <returns>The <see cref="ExcludeRule"/> object added to the ignore list.</returns>
        public ExcludeRule Exclude(string path, ExcludeRuleOptions option)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            var relative = this.GetRelativePath(path);

            if (option == ExcludeRuleOptions.Global)
            {
                return this.GlobalExcludeRules.Add(relative);
            }
            else if (this.CurrentProfile != null)
            {
                return this.CurrentProfile.Excludes.Add(relative);
            }
            else
            {
                throw new InvalidOperationException("Cannot add the path to the current profile's ignore list as no profile is currently selected.");
            }
        }

        /// <summary>
        /// Removed <paramref name="path"/> from the global exclude list and every profile in this project.
        /// </summary>
        /// <param name="path">The path to remove.</param>
        public void UnExclude(string path)
        {
            this.GlobalExcludeRules.Remove(path);

            foreach (var profile in this.Profiles)
            {
                profile.Value.Excludes.Remove(path);
            }
        }

        /// <summary>
        /// Parses <see cref="OutputFileName"/> and replaces all variable references with their respective values for the current selected profile.
        /// </summary>
        /// <returns>A filename (without extension) that will be used by the build script.</returns>
        public string GetOutputFileName()
        {
            return this.GetOutputFileName(this.CurrentProfile);
        }

        /// <summary>
        /// Parses <see cref="OutputFileName"/> and replaces all variable references with their respective values for the specified profile
        /// </summary>
        /// <param name="profile">The profile to be used when producing the filename.</param>
        /// <returns>A filename (without extension) that will be used by the build script.</returns>
        public string GetOutputFileName(BuildProfile profile)
        {
            string fileName = this.OutputFileName;

            if (!string.IsNullOrEmpty(fileName))
            {
                fileName = this.ParseVariables(profile, fileName);
            }
            else
            {
                fileName = System.IO.Path.GetFileNameWithoutExtension(this.ProjectFile.Name);
            }

            var buildPath = this.GetBuildDirectory(profile);
            if (!string.IsNullOrEmpty(buildPath))
            {
                fileName = System.IO.Path.Combine(buildPath, fileName);
            }

            if (!System.IO.Path.IsPathRooted(fileName))
            {
                fileName = System.IO.Path.Combine(this.ProjectFile.DirectoryName, fileName);
            }

            return fileName;
        }

        public string GetOutputDirectory(BuildProfile profile)
        {
            var directory = this.OutputPath;

            directory = this.ParseVariables(profile, directory);

            if (!System.IO.Path.IsPathRooted(directory))
            {
                return System.IO.Path.Combine(this.ProjectFile.DirectoryName, directory);
            }
            else
            {
                return directory;
            }
        }

        public string GetBuildDirectory(BuildProfile profile)
        {
            var directory = this.BuildPath;

            directory = this.ParseVariables(profile, directory);

            if (!System.IO.Path.IsPathRooted(directory))
            {
                return System.IO.Path.Combine(this.ProjectFile.DirectoryName, directory);
            }
            else
            {
                return directory;
            }
        }

        /// <summary>
        /// Saves the current project.
        /// </summary>
        public void Save()
        {
            Save(this);
        }

        /// <summary>
        /// Produces CABWIZ information file objects for every profile in this project.
        /// </summary>
        /// <returns>A collection of CABWIZ information file objects.</returns>
        /// <remarks>
        ///     <para>
        ///         The CABWIZ information files will not be written to the filesysten.
        ///         The information is only created in memory, the caller has to call the 
        ///         <see cref="Cabwiz.InformationFile.WriteInformationFile"/> method to create
        ///         the actual .inf files.
        ///     </para>
        /// </remarks>
        public Cabwiz.InformationFile[] CreateCabwizInf()
        {
            var l = new List<Cabwiz.InformationFile>();
            if (this.Profiles.Count > 0)
            {
                foreach (var profile in this.Profiles.Values)
                {
                    l.Add(this.Output.CreateCabwizInf(profile));
                }
            }
            else
            {
                l.Add(this.Output.CreateCabwizInf());
            }

            return l.ToArray();
        }

        public string ParseVariables(BuildProfile profile, string value)
        {
            return this.ParseVariables(this.GetVariables(profile), value);
        }

        /// <summary>
        /// After the project file has been opened. Can be used for post
        /// </summary>
        protected virtual void OnOpen()
        {
            bool changed = false;

            // Make the paths in the exclude list relative.
            {
                if (this.MakeRelative(this.GlobalExcludeRules))
                {
                    changed = true;
                }

                foreach (var profile in this.Profiles.Values)
                {
                    if (this.MakeRelative(profile.Excludes))
                    {
                        changed = true;
                    }
                }
            }

            if (changed)
            {
                // TODO: What will happen if we had to change something? Auto-Save? No...
            }
        }

        /// <summary>
        /// Makes all exclude rules in the collection relative.
        /// </summary>
        /// <param name="collection">A collection of exclude rules to make relative.</param>
        /// <returns>A value indicating whether any rule was changed.</returns>
        private bool MakeRelative(ExcludeRuleCollection collection)
        {
            bool changed = false;

            foreach (var rule in collection)
            {
                if (System.IO.Path.IsPathRooted(rule.FileName))
                {
                    var relative = this.GetRelativePath(rule.FileName);

                    if (rule.FileName != relative)
                    {
                        changed = true;
                    }

                    rule.FileName = relative;
                }
            }

            return changed;
        }

        /// <summary>
        /// Gets the relative path from the project file to the specified path.
        /// </summary>
        /// <param name="path">The path which the relative path will reference to.</param>
        /// <returns>The relative path from the project file to <paramref name="path"/>.</returns>
        private string GetRelativePath(string path)
        {
            return CSharp.FileUtils.GetRelativePath(this.ProjectFile.FullName, path);
        }

        /// <summary>
        /// Returns a dictionary of variables with their names and respective values.
        /// </summary>
        /// <param name="profile">The profile for which to get the variables for.</param>
        /// <returns>A dictionary of variables and their values.</returns>
        private Dictionary<string, string> GetSysVariables(BuildProfile profile)
        {
            var variables = new Dictionary<string, string>();

            foreach (var name in this.GetVariableNames(profile))
            {
                this.AddVariable(variables, this.FormatVariableName(name.Key), name.Value);
            }

            return variables;
        }

        private Dictionary<string, string> GetVariables(BuildProfile profile)
        {
            var variables = this.GetSysVariables(profile);

            foreach (var userVariable in this.GetUserVariables(profile))
            {
                this.AddVariable(variables, this.FormatVariableName(userVariable.Key), this.ParseVariables(variables, userVariable.Value));
            }

            return variables;
        }

        private string ParseVariables(Dictionary<string, string> variables, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                foreach (var variable in variables)
                {
                    value = value.Replace(variable.Key, variable.Value ?? string.Empty);
                }
            }

            return value;
        }

        private void AddVariable(Dictionary<string, string> variables, string key, string value)
        {
            if (variables.ContainsKey(key))
            {
                variables[key] = value;
            }
            else
            {
                variables.Add(key, value);
            }
        }

        private Dictionary<string, string> GetUserVariables(BuildProfile profile)
        {
            var variables = new Dictionary<string, string>();

            this.AddUserVariables(variables, this.GlobalUserVariables);

            if (profile != null)
            {
                this.AddUserVariables(variables, profile.GetVariables());
            }

            return variables;
        }

        private void AddUserVariables(Dictionary<string, string> collection, UserVariableCollection variables)
        {
            this.AddUserVariables(collection, variables.Values);
        }

        private void AddUserVariables(Dictionary<string, string> collection, IEnumerable<UserVariable> variables)
        {
            foreach (var variable in variables)
            {
                this.AddVariable(collection, variable.Name, variable.Value);
            }
        }

        /// <summary>
        /// Formats a variable name to it's full name used when parsing.
        /// </summary>
        /// <param name="name">The name of the variable name to format.</param>
        /// <returns><paramref name="name"/> formatted into a variable reference. Example "foo" will produce the string "$(foo)".</returns>
        private string FormatVariableName(string name)
        {
            return string.Format("$({0})", name);
        }

        /// <summary>
        /// Returns a dictionary of variable names and their values.
        /// </summary>
        /// <param name="profile">The profile for which to get the variables for.</param>
        /// <returns>A dictionary of variable names and their values.</returns>
        private Dictionary<string, string> GetVariableNames(BuildProfile profile)
        {
            var variables = new Dictionary<string, string>();

            variables.Add("Version", this.ProjectVersionString);
            variables.Add("ReleaseName", this.ReleaseName);
            variables.Add("Profile", profile != null ? profile.Name : null);
            variables.Add("ApplicationName", this.ApplicationName);
            variables.Add("CompanyName", this.CompanyName);

            foreach (var variable in variables.ToArray())
            {
                string value;
                if (string.IsNullOrEmpty(variable.Value))
                {
                    value = string.Empty;
                }
                else
                {
                    value = string.Concat("-", variable.Value);
                }

                variables.Add(string.Concat(variable.Key, "Part"), value);
            }

            return variables;
        }
    }
}
