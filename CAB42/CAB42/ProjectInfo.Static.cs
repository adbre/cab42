﻿//-----------------------------------------------------------------------
// <copyright file="ProjectInfo.Static.cs" company="42A Consulting">
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
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml.Serialization;

    using C42A.CSharp;

    partial class ProjectInfo
    {
        /// <summary>
        /// The XML serializer for this type.
        /// </summary>
        private static XmlSerializer xmlSerializer;

        /// <summary>
        /// Serializes a <see cref="ProjectInfo"/> object and saving the output to the project's <see cref="ProjectInfo.ProjectFile"/>.
        /// </summary>
        /// <param name="project">The project to save.</param>
        public static void Save(ProjectInfo project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("project");
            }

            SaveAs(project, project.ProjectFile);
        }

        /// <summary>
        /// Serializes a <see cref="ProjectInfo"/> object and saving the output to <paramref name="file"/>.
        /// </summary>
        /// <param name="project">The project to save.</param>
        /// <param name="file">The file to save the output to.</param>
        public static void SaveAs(ProjectInfo project, System.IO.FileInfo file)
        {
            if (project == null)
            {
                throw new ArgumentNullException("project");
            }

            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            using (var stream = file.Open(System.IO.FileMode.Create))
            {
                Serialize(project, stream);
            }
        }

        /// <summary>
        /// Serializes a <see cref="ProjectInfo"/> object and saving the output to <paramref name="fileName"/>.
        /// </summary>
        /// <param name="project">The project to save.</param>
        /// <param name="fileName">The file to save the output to.</param>
        public static void SaveAs(ProjectInfo project, string fileName)
        {
            SaveAs(project, new System.IO.FileInfo(fileName));
        }

        /// <summary>
        /// Serializes a <see cref="ProjectInfo"/> object to the specified output stream.
        /// </summary>
        /// <param name="project">The object to serialize.</param>
        /// <param name="stream">The stream to write the serialized contents to.</param>
        public static void Serialize(ProjectInfo project, System.IO.Stream stream)
        {
            if (xmlSerializer == null)
            {
                xmlSerializer = new XmlSerializer(typeof(ProjectInfo));
            }

            xmlSerializer.Serialize(stream, project);
        }

        /// <summary>
        /// Creates a new <see cref="ProjectInfo"/> instance, and creates it's project file.
        /// </summary>
        /// <param name="fileName">The filename for the project to create.</param>
        /// <returns>A <see cref="ProjectInfo"/> object.</returns>
        public static ProjectInfo New(string fileName)
        {
            // Create project file.
            Save(new ProjectInfo() { ProjectFile = new System.IO.FileInfo(fileName) });

            // Open it (to ensure everything is working)
            return Open(fileName);
        }

        /// <summary>
        /// Open and reads the specified file, parsing it into a <see cref="ProjectInfp"/> instance.
        /// </summary>
        /// <param name="fileName">The file to read.</param>
        /// <returns>The <see cref="ProjectInfo"/> object which the file was parsed into.</returns>
        public static ProjectInfo Open(string fileName)
        {
            if (xmlSerializer == null)
            {
                xmlSerializer = new XmlSerializer(typeof(ProjectInfo));
            }

            var fileInfo = new System.IO.FileInfo(fileName);

            using (var stream = fileInfo.Open(System.IO.FileMode.Open))
            {
                var deserialized = xmlSerializer.Deserialize(stream) as ProjectInfo;

                if (deserialized != null)
                {
                    deserialized.ProjectFile = fileInfo;

                    if (string.IsNullOrEmpty(deserialized.OutputFileName))
                    {
                        deserialized.OutputFileName = System.IO.Path.GetFileNameWithoutExtension(fileInfo.Name);
                    }

                    deserialized.OnOpen();

                    return deserialized;
                }
            }

            // failover
            return null;
        }

        public static ProjectInfo Open(ProgramOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");
            if (string.IsNullOrEmpty(options.FileName)) throw new ArgumentException("FileName is null or empty.");

            var result = File.Exists(options.FileName) ? Open(options.FileName) : New(options.FileName);
            
            foreach (var variable in options.Variables)
            {
                if (result.SetSysVariable(variable.Key, variable.Value)) continue;
                result.GlobalUserVariables[variable.Key] = new UserVariable(variable.Key, variable.Value);
            }

            return result;
        }

        private bool SetSysVariable(string key, string value)
        {
            switch (key)
            {
                case "Version":
                    this.ProjectVersion = VersionHelper.ParseGitDescription(value);
                    return true;

                case "ReleaseName":
                    this.ReleaseName = value;
                    return true;
            }

            return false;
        }
    }
}