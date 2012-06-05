//-----------------------------------------------------------------------
// <copyright file="BuildProfileCollection.cs" company="42A Consulting">
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

    /// <summary>
    /// A collection of <see cref="BuildProfile"/> objects.
    /// </summary>
    public class BuildProfileCollection : Dictionary<string, BuildProfile>
    {
        /// <summary>
        /// The parent project.
        /// </summary>
        private ProjectInfo project;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProfileCollection"/> class.
        /// </summary>
        /// <param name="projectInfo">The <see cref="Project"/> which this profile should belong to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="projectInfo"/> is a null reference.</exception>
        public BuildProfileCollection(ProjectInfo projectInfo)
        {
            if (projectInfo == null)
            {
                throw new ArgumentNullException("projectInfo");
            }

            this.Project = projectInfo;
        }

        /// <summary>
        /// Gets or sets the <see cref="Project"/> object this collection belongs to.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         When set, all <see cref="BuildProfile"/> items in this collection will also have 
        ///         their <see cref="BuildProfile.Project"/> property set to the value being set.
        ///     </para>
        /// </remarks>
        public ProjectInfo Project
        {
            get
            {
                return this.project;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                this.project = value;

                foreach (var profile in this)
                {
                    profile.Value.ProjectInfo = value;
                }
            }
        }

        /// <summary>
        /// Adds a <see cref="BuildProfile"/> object to the collection.
        /// </summary>
        /// <param name="profile">The <see cref="BuildProfile"/> to add.</param>
        /// <param name="resolveFamily">If true, the <see cref="BuildProfile.InheritsFrom"/> property will be resolved to a instance already located in this collection.</param>
        /// <exception cref="ArgumentNullException"><paramref name="profile"/> is a null reference.</exception>
        /// <exception cref="InvalidOperationException"><see cref="BuildProfile.Name"/> is null or empty.</exception>
        /// <exception cref="InvalidOperationException">The <see cref="BuildProfile.Name"/> is a name which already exists in the collection.</exception>
        public void Add(BuildProfile profile, bool resolveFamily = true)
        {
            if (profile == null)
            {
                throw new ArgumentNullException("profile");
            }

            if (string.IsNullOrEmpty(profile.Name))
            {
                throw new InvalidOperationException(
                    "Attempted to add a profile without name.");
            }

            if (this.ContainsKey(profile.Name))
            {
                throw new InvalidOperationException(string.Format(
                    "A profile with the name '{0}' already exists in the collection.",
                    profile.Name));
            }

            if (resolveFamily && !string.IsNullOrEmpty(profile.InheritsFrom))
            {
                if (!this.ContainsKey(profile.InheritsFrom))
                {
                    throw new InvalidOperationException(string.Format(
                        "The profile '{0}' references a profile '{1}' that could not be found.",
                        profile.Name,
                        profile.InheritsFrom));
                }

                profile.Parent = this[profile.InheritsFrom];
            }

            profile.ProjectInfo = this.Project;

            this.Add(profile.Name, profile);
        }

        /// <summary>
        /// Adds a collection of <see cref="BuildProfile"/> objects to the collection, resolving the family tree.
        /// </summary>
        /// <param name="profiles">A collection of <see cref="BuildProfile"/> objects to add.</param>
        /// <remarks>
        ///     <para>
        ///         For the family resolve to work properly, a profile which is inherited must preceeds the 
        ///         inheritee in the collection.
        ///     </para>
        /// </remarks>
        public void AddRange(IEnumerable<BuildProfile> profiles)
        {
            this.AddRange(profiles, true);
        }

        /// <summary>
        /// Adds a collection of <see cref="BuildProfile"/> objects to the collection, resolving the family tree.
        /// </summary>
        /// <param name="profiles">A collection of <see cref="BuildProfile"/> objects to add.</param>
        /// <param name="resolveFamily">If true, the <see cref="BuildProfile.InheritsFrom"/> property will be resolved to a instance already located in this collection.</param>
        /// <remarks>
        ///     <para>
        ///         For the family resolve to work properly, a profile which is inherited must preceeds the 
        ///         inheritee in the collection.
        ///     </para>
        /// </remarks>
        public void AddRange(IEnumerable<BuildProfile> profiles, bool resolveFamily)
        {
            foreach (var profile in profiles)
            {
                this.Add(profile, resolveFamily);
            }
        }

        /// <summary>
        /// Finds a profile by its name in this collection, using case insensitive search.
        /// </summary>
        /// <param name="name">The name of the profile to search for.</param>
        /// <returns>The <see cref="BuildProfile"/> object with a name matching <paramref name="name"/> found in this collection. Null if not found.</returns>
        public BuildProfile FindProfile(string name)
        {
            return this.FindProfile(name, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Finds a profile by its name in this collection.
        /// </summary>
        /// <param name="name">The name of the profile to search for.</param>
        /// <param name="comparisonType">The string comparison method to use.</param>
        /// <returns>The <see cref="BuildProfile"/> object with a name matching <paramref name="name"/> found in this collection. Null if not found.</returns>
        public BuildProfile FindProfile(string name, StringComparison comparisonType)
        {
            if (!string.IsNullOrEmpty(name))
            {
                if (this.ContainsKey(name))
                {
                    return this[name];
                }
                else
                {
                    foreach (var profile in this.Values)
                    {
                        if (profile.Name.Equals(name, comparisonType))
                        {
                            return profile;
                        }
                    }
                }
            }

            // Failover
            return null;
        }
    }
}
