//-----------------------------------------------------------------------
// <copyright file="ProjectOutput.cs" company="42A Consulting">
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
    /// A class which will produce a project output filesystem tree.
    /// </summary>
    public class ProjectOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectOutput"/> class.
        /// </summary>
        /// <param name="projectInfo">The project which this class will produce output for.</param>
        /// <exception cref="ArgumentNullException"><paramref name="projectInfo"/> is null.</exception>
        public ProjectOutput(ProjectInfo projectInfo)
        {
            if (projectInfo == null)
            {
                throw new ArgumentNullException("buildProject");
            }

            this.ProjectInfo = projectInfo;

            this.InstallationDirectory = new OutputDirectoryInfo(Cabwiz.SpecialFolderMacros.InstallationDirectory);
            this.ProgramFiles = new OutputDirectoryInfo(Cabwiz.SpecialFolderMacros.ProgramFiles);
            this.StartMenu = new OutputDirectoryInfo(Cabwiz.SpecialFolderMacros.StartMenu);
        }

        /// <summary>
        /// Gets the <see cref="ProjectInfo"/> object that was passed to the constructor.
        /// </summary>
        public ProjectInfo ProjectInfo { get; private set; }

        /// <summary>
        /// Gets the primary installation directory for the application.
        /// </summary>
        public OutputDirectoryInfo InstallationDirectory { get; private set; }

        /// <summary>
        /// Gets the Program Files directory on the target machine for this build project.
        /// </summary>
        public OutputDirectoryInfo ProgramFiles { get; private set; }

        public OutputDirectoryInfo StartMenu { get; private set; }

        /// <summary>
        /// Refreshes the output for the current project.
        /// </summary>
        public void Refresh(BuildProfile profile = null)
        {
            this.InstallationDirectory.Clear();
            this.ProgramFiles.Clear();
            this.StartMenu.Clear();

            var projectDirectory = this.ProjectInfo.ProjectFile.Directory;

            var rules = this.ProjectInfo.GetFiles(profile);

            foreach (var rule in rules)
            {
                var fullPath = System.IO.Path.Combine(projectDirectory.FullName, rule.Path);

                if (fullPath.Contains('*'))
                {
                    var dirName = System.IO.Path.GetDirectoryName(fullPath);

                    if (dirName.Contains('*'))
                    {
                        throw new InvalidOperationException("A wildcard was present in the path, but not on the filename portion.");
                    }
                    else if (System.IO.Directory.Exists(dirName))
                    {
                        var directory = new System.IO.DirectoryInfo(dirName);

                        var files = directory.GetFiles(System.IO.Path.GetFileName(fullPath));

                        foreach (var file in files)
                        {
                            this.AddFile(rule, file, rule.FileName);
                        }
                    }
                }
                else
                {
                    this.AddFile(rule, new System.IO.FileInfo(fullPath), rule.FileName);
                }                
            }
        }

        /// <summary>
        /// Creates a <see cref="Cabwiz.InformationFile"/> for the project and it's current selected build profile.
        /// </summary>
        /// <returns>A <see cref="Cabwiz.InformationFile"/> for the project and it's current selected build profile.</returns>
        public Cabwiz.InformationFile CreateCabwizInf()
        {
            return this.CreateCabwizInf(null);
        }

        /// <summary>
        /// Creates a <see cref="Cabwiz.InformationFile"/> for the project and the specified build profile.
        /// </summary>
        /// <param name="profile">The build profile to use when producing the output information.</param>
        /// <returns>A <see cref="Cabwiz.InformationFile"/> for the project and specified build profile.</returns>
        public Cabwiz.InformationFile CreateCabwizInf(BuildProfile profile)
        {
            var inf = new Cabwiz.InformationFile(this.ProjectInfo.ApplicationName, this.ProjectInfo.ProjectVersion);

            inf.FileName = string.Concat(this.ProjectInfo.GetOutputFileName(profile), Cabwiz.InformationFile.DefaultExtension);

            inf.Version.Provider = this.ProjectInfo.CompanyName;

            this.AddToCabwiz(inf, profile, this.InstallationDirectory);

            this.AddToCabwiz(inf, profile, this.ProjectInfo.GlobalRegistryKeys);

            return inf;
        }

        private void AddToCabwiz(Cabwiz.InformationFile inf, BuildProfile profile, RegistryKeyCollection registryKeys)
        {
            if (inf == null)
            {
                throw new ArgumentNullException("inf");
            }

            if (registryKeys != null)
            {
                foreach (var registryKey in registryKeys)
                {
                    this.AddToCabwiz(inf.AddReg, registryKey);
                }
            }
        }

        private void AddToCabwiz(Cabwiz.AddRegSection addRegSection, RegistryKey registryKey)
        {
            foreach (var registryValue in registryKey.Values)
            {
                addRegSection.RegistryKeys.Add(new Cabwiz.AddRegItem(registryKey.Name)
                {
                    ValueName = registryValue.Name,
                    Type = registryValue.Type,
                    Value = registryValue.Value
                });
            }
        }

        /// <summary>
        /// Add a directory to a information file, using the specified build profile.
        /// </summary>
        /// <param name="inf">The information file to add the directory to.</param>
        /// <param name="profile">The build profile used.</param>
        /// <param name="directoryInfo">The directory to add.</param>
        /// <remarks>
        ///     <para>
        ///         See <see cref="AddToCabwiz(Cabwiz.InformationFile inf, BuildProfile profile, FileInfo file)"/>
        ///             for information about what files are excluded.
        ///     </para>
        /// </remarks>
        private void AddToCabwiz(Cabwiz.InformationFile inf, BuildProfile profile, OutputDirectoryInfo directoryInfo)
        {
            foreach (var subDirectory in directoryInfo.SubDirectories)
            {
                this.AddToCabwiz(inf, profile, subDirectory);
            }

            foreach (var file in directoryInfo.Files)
            {
                this.AddToCabwiz(inf, profile, file);
            }
        }

        /// <summary>
        /// Add a file to a information file, using the specified build profile.
        /// </summary>
        /// <param name="inf">The information file to add the directory to.</param>
        /// <param name="profile">The build profile used.</param>
        /// <param name="file">The file to add.</param>
        /// <remarks>
        ///     <para>
        ///         The file will not be added to the information file if 
        ///             1. <paramref name="profile"/> is null, and <see cref="OutputFileInfo.SourceFile"/> 
        ///                 is excluded by the project's global exclude list or it's current selected build profile.
        ///             2. or, <paramref name="profile"/> is not null and <see cref="OutputFileInfo.SourceFile"/>
        ///                 is excluded by <paramref name="profile"/>.
        ///     </para>
        /// </remarks>
        private void AddToCabwiz(Cabwiz.InformationFile inf, BuildProfile profile, OutputFileInfo file)
        {
            if (!this.ProjectInfo.IsExcluded(file.SourceFile, profile))
            {
                var sourceFile = file.SourceFile;

                if (file.IncludeRule != null)
                {
                    if (file.IncludeRule.XmlReplacementRules.Count > 0)
                    {
                        var tempFile = this.GetObjFileName(file, profile);

                        var rewriter = new XmlFileRewriter();

                        foreach (var replacement in file.IncludeRule.XmlReplacementRules)
                        {
                            rewriter.AddTextPath(replacement.Tag, this.ProjectInfo.ParseVariables(profile, replacement.Value));
                        }

                        rewriter.Rewrite(sourceFile, tempFile);

                        sourceFile = tempFile;
                    }
                    else if (!string.IsNullOrWhiteSpace(file.IncludeRule.FileName))
                    {
                        var tempFile = this.GetObjFileName(file, profile);

                        System.IO.File.Copy(file.SourceFile, tempFile, true);

                        sourceFile = tempFile;
                    }

                    if (!string.IsNullOrEmpty(file.IncludeRule.StartMenuShortcut))
                    {

                        inf.AddStartMenuShortcutToFile(
                            file.Name,
                            System.IO.Path.GetFileName(file.IncludeRule.StartMenuShortcut), 
                            System.IO.Path.GetDirectoryName(file.IncludeRule.StartMenuShortcut));
                    }
                }

                inf.AddFile(sourceFile, file.Directory.FullName, file.Name);
            }
        }

        private string GetObjFileName(OutputFileInfo file, BuildProfile profile)
        {
            var buildDirectory = this.ProjectInfo.GetBuildDirectory(profile);

            if (file.IncludeRule != null)
            {
                if (!string.IsNullOrWhiteSpace(file.IncludeRule.FileName))
                {
                    return System.IO.Path.Combine(buildDirectory, file.IncludeRule.FileName);
                }
                else if (file.IncludeRule.XmlReplacementRules.Count > 0)
                {
                    return System.IO.Path.Combine(buildDirectory, file.Name);
                }
            }

            // failover
            return file.SourceFile;
        }

        /// <summary>
        /// Add a file to the project output, preserving the filename.
        /// </summary>
        /// <param name="rule">The rule which included the file.</param>
        /// <param name="fileInfo">The file on the harddrive to include.</param>
        private void AddFile(IncludeRule rule, System.IO.FileInfo fileInfo)
        {
            this.AddFile(rule, fileInfo, fileInfo.Name);
        }

        /// <summary>
        /// Add a file to the project output, with the specified filename.
        /// </summary>
        /// <param name="rule">The rule which included the file.</param>
        /// <param name="fileInfo">The file on the harddrive which contents to include.</param>
        /// <param name="fileName">The name to use on the target machine.</param>
        private void AddFile(IncludeRule rule, System.IO.FileInfo fileInfo, string fileName)
        {
            var dir = this.InstallationDirectory;

            if (!string.IsNullOrEmpty(rule.Folder))
            {
                dir = dir.AddSubDirectory(rule.Folder);
            }

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = fileInfo.Name;
            }

            var outputFile = dir.AddFile(fileName, fileInfo.FullName, rule);

            if (!string.IsNullOrEmpty(rule.StartMenuShortcut))
            {
                this.AddShortcut(rule.StartMenuShortcut, outputFile, rule);
            }
        }

        private void AddShortcut(string shortcutFileName, OutputFileInfo outputFile, IncludeRule rule)
        {
            var path = System.IO.Path.GetDirectoryName(shortcutFileName);
            var fileName = System.IO.Path.GetFileName(shortcutFileName);

            var dir = this.StartMenu;

            if (!string.IsNullOrEmpty(path))
            {
                dir = dir.AddSubDirectory(path);
            }

            dir.AddShortcut(fileName, outputFile, rule);
        }
    }
}
