//-----------------------------------------------------------------------
// <copyright file="OutputFileInfo.cs" company="42A Consulting">
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
    /// Represents a file as it will be located on a target machine file system.
    /// </summary>
    public class OutputFileInfo : OutputFileSystemInfo
    {
        /// <summary>
        /// The source file.
        /// </summary>
        private string sourceFileName;

        /// <summary>
        /// The date/time when the source file was last modified.
        /// </summary>
        private DateTime lastModified;

        /// <summary>
        /// The size, in bytes, of the source file.
        /// </summary>
        private long fileSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputFileInfo"/> class.
        /// </summary>
        /// <param name="fileName">The filename of the file, as it will be on the target machine's file system.</param>
        /// <param name="parentDirectory">The parent directory for this file.</param>
        public OutputFileInfo(string fileName, OutputDirectoryInfo parentDirectory)
            : base(fileName, parentDirectory)
        {
            // nothing
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputFileInfo"/> class.
        /// </summary>
        /// <param name="fileName">The filename of the file, as it will be on the target machine's file system.</param>
        /// <param name="sourceFile">The full path and filename of the file that will be included in a build.</param>
        /// <param name="parentDirectory">The parent directory for this file.</param>
        public OutputFileInfo(string fileName, string sourceFile, OutputDirectoryInfo parentDirectory, IncludeRule rule)
            : this(fileName, parentDirectory)
        {
            this.SourceFile = sourceFile;
            this.IncludeRule = rule;
        }

        /// <summary>
        /// Gets the source file.
        /// </summary>
        public string SourceFile
        {
            get
            {
                return this.sourceFileName;
            }

            private set
            {
                this.sourceFileName = value;

                if (!string.IsNullOrEmpty(this.SourceFile) && System.IO.File.Exists(this.SourceFile))
                {
                    var fileInfo = new System.IO.FileInfo(this.SourceFile);

                    this.lastModified = fileInfo.LastWriteTime;
                    this.fileSize = fileInfo.Length;
                }
            }
        }

        /// <summary>
        /// Gets the file extension of the <see cref="Name"/> property.
        /// </summary>
        public string Extension
        {
            get { return System.IO.Path.GetExtension(this.Name); }
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> object describing the last date and time for when the source item was modified.
        /// </summary>
        public DateTime Modified
        {
            get
            {
                return this.lastModified;
            }
        }

        /// <summary>
        /// Gets a string description for the file type.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         In the current implementation this property is a alias for <see cref="Extension"/>.
        ///         This behaviour may change in the future, returning for example a string returned by Windows describing
        ///         the file type.
        ///     </para>
        /// </remarks>
        public string Type
        {
            get
            {
                return this.Extension;
            }
        }

        /// <summary>
        /// Gets the size, in bytes, of the source file.
        /// </summary>
        public long Size
        {
            get
            {
                return this.fileSize;
            }
        }

        public IncludeRule IncludeRule { get; private set; }
    }
}
