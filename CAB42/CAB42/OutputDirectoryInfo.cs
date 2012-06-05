//-----------------------------------------------------------------------
// <copyright file="OutputDirectoryInfo.cs" company="42A Consulting">
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
    /// Represents a directory on a target file system when building the project.
    /// </summary>
    public class OutputDirectoryInfo : OutputFileSystemInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputDirectoryInfo"/> class.
        /// </summary>
        /// <param name="name">The name of the directory.</param>
        /// <param name="directory">The parent directory.</param>
        public OutputDirectoryInfo(string name, OutputDirectoryInfo directory)
            : base(name, directory)
        {
            this.Shortcuts = new List<OutputShortcutInfo>();
            this.Files = new List<OutputFileInfo>();
            this.SubDirectories = new List<OutputDirectoryInfo>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputDirectoryInfo"/> class.
        /// </summary>
        /// <param name="name">The name of the directory.</param>
        public OutputDirectoryInfo(string name)
            : this(name, null)
        {
            // nothing
        }

        /// <summary>
        /// Gets a collection of all filesytem items located within this directory.
        /// </summary>
        public IEnumerable<OutputFileSystemInfo> Items
        {
            get
            {
                List<OutputFileSystemInfo> l = new List<OutputFileSystemInfo>();

                l.AddRange(this.SubDirectories);
                l.AddRange(this.Files);
                l.AddRange(this.Shortcuts);

                return l.ToArray();
            }
        }

        /// <summary>
        /// Gets a collection of sub directories located within this directory.
        /// </summary>
        public List<OutputDirectoryInfo> SubDirectories { get; private set; }

        /// <summary>
        /// Gets a collection of files located within this directory.
        /// </summary>
        public List<OutputFileInfo> Files { get; private set; }

        /// <summary>
        /// Gets a collection of shortcuts located within this directory.
        /// </summary>
        public List<OutputShortcutInfo> Shortcuts { get; private set; }

        /// <summary>
        /// Clears this directory from all file system objects.
        /// </summary>
        public void Clear()
        {
            this.SubDirectories.Clear();
            this.Files.Clear();
            this.Shortcuts.Clear();
        }

        /// <summary>
        /// Gets the <see cref="OutputDirectoryInfo"/> object contained in this collection, identified by it's name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>The <see cref="OutputDirectoryInfo"/> with the specified name. Null if not found.</returns>
        public OutputDirectoryInfo GetDirectory(string name)
        {
            foreach (var subdir in this.SubDirectories)
            {
                if (subdir.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return subdir;
                }
            }

            return null;
        }

        /// <summary>
        /// Add a sub directory with the specified name.
        /// </summary>
        /// <param name="name">The name of the sub directory to add.</param>
        /// <returns>The <see cref="OutputDirectoryInfo"/> object added.</returns>
        public OutputDirectoryInfo AddSubDirectory(string name)
        {
            int sepIndex = name.IndexOf(System.IO.Path.PathSeparator);

            if (sepIndex > 0)
            {
                string firstName = name.Substring(0, sepIndex);

                var subdir = this.GetDirectory(firstName);
                if (subdir == null)
                {
                    subdir = new OutputDirectoryInfo(firstName, this);

                    subdir.AddSubDirectory(name.Substring(sepIndex + 1));

                    this.SubDirectories.Add(subdir);
                }

                return subdir;
            }
            else
            {
                var subdir = this.GetDirectory(name);

                if (subdir == null)
                {
                    subdir = new OutputDirectoryInfo(name, this);

                    this.SubDirectories.Add(subdir);
                }

                return subdir;
            }
        }

        /// <summary>
        /// Add a file to this directory.
        /// </summary>
        /// <param name="fileName">The name of the file as it will be on the target file system.</param>
        /// <param name="sourceFileName">The source file.</param>
        /// <returns>The <see cref="OutputFileInfo"/> object added.</returns>
        public OutputFileInfo AddFile(string fileName, string sourceFileName, IncludeRule rule)
        {
            var fileInfo = new OutputFileInfo(fileName, sourceFileName, this, rule);

            this.Files.Add(fileInfo);

            return fileInfo;
        }

        public OutputShortcutInfo AddShortcut(string shortcutFileName, OutputFileInfo target, IncludeRule rule)
        {
            var shortcutInfo = new OutputShortcutInfo(target, shortcutFileName, this);

            this.Shortcuts.Add(shortcutInfo);

            return shortcutInfo;
        }
    }
}
