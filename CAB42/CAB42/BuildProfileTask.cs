//-----------------------------------------------------------------------
// <copyright file="BuildProfileTask.cs" company="42A Consulting">
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
    /// A task for building a profile.
    /// </summary>
    public class BuildProfileTask : ICabwizBuildTask
    {
        /// <summary>
        /// Initializes a new instance of the BuildProfileTask class with the specified profile.
        /// </summary>
        /// <param name="profile">The profile to build.</param>
        public BuildProfileTask(BuildProfile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException("profile");
            }

            this.Profile = profile;
            this.Project = profile.ProjectInfo;
        }

        /// <summary>
        /// Initializes a new instance of the BuildProfileTask class with the specified project.
        /// </summary>
        /// <param name="profile">The project to build.</param>
        public BuildProfileTask(ProjectInfo project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("project");
            }

            this.Project = project;
        }

        /// <summary>
        /// Initializes a new instance of the BuildProfileTask class with the specified project and profile.
        /// </summary>
        /// <param name="project">The project to build.</param>
        /// <param name="profile">The profile to build.</param>
        public BuildProfileTask(ProjectInfo project, BuildProfile profile)
        {
            if (project == null)
            {
                throw new ArgumentNullException("project");
            }

            if (profile != null && project != profile.ProjectInfo)
            {
                throw new ArgumentException("The profile argument is not null and is not a profile child of the specified project", "profile");
            }

            this.Project = project;
            this.Profile = profile;
        }

        /// <summary>
        /// Gets the project for this build task.
        /// </summary>
        public ProjectInfo Project { get; private set; }

        /// <summary>
        /// Gets the profile for this build task. Null if default.
        /// </summary>
        public BuildProfile Profile { get; private set; }

        /// <inheritdoc />
        public string ProjectName
        {
            get
            {
                return this.Project.ProjectFile.Name;
            }
        }

        /// <inheritdoc />
        public string Configuration
        {
            get
            {
                if (this.Profile != null)
                {
                    return this.Profile.Name;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Creates 
        /// </summary>
        /// <param name="project">The project for which to create the build tasks for.</param>
        /// <returns>A collection of build tasks.</returns>
        public static IBuildTask[] CreateBuildTasks(ProjectInfo project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("project");
            }

            var tasks = new List<IBuildTask>();

            if (project.Profiles != null && project.Profiles.Count > 0)
            {
                foreach (var profile in project.Profiles.Where(p => !p.Value.IsPlaceholder))
                {
                    tasks.Add(new BuildProfileTask(project, profile.Value));
                }
            }
            else
            {
                tasks.Add(new BuildProfileTask(project));
            }

            return tasks.ToArray();
        }

        /// <summary>
        /// Builds the project according to the current profile.
        /// </summary>
        /// <param name="cabwiz">The cabwiz application reference</param>
        /// <param name="feedback">The build context.</param>
        /// <returns>A value indicating whether the build was successful.</returns>
        public bool Build(Cabwiz.CabwizApplication cabwiz, IBuildFeedback feedback)
        {
            var project = this.Profile.ProjectInfo;
            var profile = this.Profile;

            if (cabwiz == null)
            {
                throw new ArgumentNullException("cabwiz", "Missing reference to a CabwizApplication object.");
            }

            var output = project.GetOutput(profile);

            // Create the .INF file for Cabwiz to process.
            var inf = output.CreateCabwizInf(profile);

            // Set the output directory
            cabwiz.DestinationDirectory = project.GetOutputDirectory(profile);

            // add some feedback
            feedback.WriteLine("   > {0}", cabwiz.PreviewCommandLine(inf));

            int exitCode = cabwiz.Run(inf);
            if (exitCode != 0)
            {
                feedback.WriteLine("   {0} returned {1}", System.IO.Path.GetFileName(cabwiz.FileName), exitCode);

                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Returns a string simlar to "Project: {0}, Configuration: {1}" where {0} is the name of the project file, and {1} is the name of the selected profile.
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            var projectName = this.Project.ProjectFile.Name;
            var profileName = this.Profile != null ? this.Profile.Name : "<default>";

            return string.Format("Project: {0}, Configuration: {1}", projectName, profileName);
        }

        /// <inheritdoc />
        bool IBuildTask.Build(IBuildFeedback feedback)
        {
            // this will ALWAYS result in a ArgumentNullException, but we cannot proceed without cabwiz
            return this.Build(null, feedback);
        }
    }
}
