//-----------------------------------------------------------------------
// <copyright file="OutputShortcutInfo.cs" company="42A Consulting">
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
    /// Describes a shortcut to another file.
    /// </summary>
    public class OutputShortcutInfo : OutputFileSystemInfo
    {
        /// <summary>
        /// Initializes a new instance of the OutputShortcutInfo class with the specified target, filename and parent directory.
        /// </summary>
        /// <param name="target">The target which this shortcut points at.</param>
        /// <param name="name">The name of the shortcut file.</param>
        /// <param name="directory">The directory in which the shortcut will be created.</param>
        public OutputShortcutInfo(OutputFileSystemInfo target, string name, OutputDirectoryInfo directory)
            : base(name, directory)
        {
            this.Target = target;
        }

        /// <summary>
        /// Gets or sets the target which this shortcut points at.
        /// </summary>
        public OutputFileSystemInfo Target { get; set; }
    }
}
