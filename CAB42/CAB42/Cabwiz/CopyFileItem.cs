//-----------------------------------------------------------------------
// <copyright file="CopyFileItem.cs" company="42A Consulting">
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
namespace C42A.CAB42.Cabwiz
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A class describing a file to be copied when creating a CAB.
    /// </summary>
    public class CopyFileItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CopyFileItem"/> class.
        /// </summary>
        /// <param name="destionationFileName">The name of the file as to be named on the target machine's filesytem.</param>
        /// <param name="sourceFileName">The absolute path to the file to be copied to the CAB file.</param>
        public CopyFileItem(string destionationFileName, string sourceFileName)
        {
            this.DestinationFileName = destionationFileName;
            this.SourceFileName = sourceFileName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CopyFileItem"/> class.
        /// </summary>
        /// <param name="fileName">The absolute path to the file to be copied to the CAB file.</param>
        public CopyFileItem(string fileName)
            : this(fileName, fileName)
        {
            // nothing
        }

        /// <summary>
        /// Gets or sets the destination file name as it will be on the target machine's fileystem.
        /// </summary>
        public string DestinationFileName { get; set; }

        /// <summary>
        /// Gets or sets the absolute path to the file to be copied when the CAB is created.
        /// </summary>
        public string SourceFileName { get; set; }

        /// <summary>
        /// Gets or sets the copy file flags.
        /// </summary>
        public CopyFileFlags Flags { get; set; }

        /// <summary>
        /// Gets a string formatted as it should be written to the .inf file.
        /// </summary>
        /// <returns>A formatted string to use in .inf file.</returns>
        public override string ToString()
        {
            // TODO: The Flags property is not really used. "0" is hardcoded to always be printed as the CopyFile flag.
            string flags = "0";

            return string.Format("\"{0}\",\"{1}\",,{2}", this.DestinationFileName, this.SourceFileName, flags);
        }
    }
}
