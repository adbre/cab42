//-----------------------------------------------------------------------
// <copyright file="OutputFileSystemInfo.cs" company="42A Consulting">
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
    /// Provides the base class for both <see cref="OutputFileInfo"/> and <see cref="OutputDirectoryInfo"/> outputs.
    /// </summary>
    public abstract class OutputFileSystemInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputFileSystemInfo"/> class.
        /// </summary>
        /// <param name="name">The name for the file system item.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is null or empty.</exception>
        protected OutputFileSystemInfo(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputFileSystemInfo"/> class, to located within the specified directory.
        /// </summary>
        /// <param name="name">The name for the file system item.</param>
        /// <param name="directory">The parent directory for this filesystem item. This parameter can be null.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is null or empty.</exception>
        protected OutputFileSystemInfo(string name, OutputDirectoryInfo directory)
            : this(name)
        {
            this.Directory = directory;
        }

        /// <summary>
        /// Gets the name of this filesystem item. This is the string passed to the constructor.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the full name of this filesystem item.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The returned string will be a string combined by this filesystem item's parent fullname and this name, 
        ///         joined by the system path separator character.
        ///     </para>
        ///     <para>
        ///         If this filesystem item has no parent directory, the returned string will be equal to <see cref="Name"/>.
        ///     </para>
        /// </remarks>
        public string FullName
        {
            get
            {
                if (this.Directory != null)
                {
                    return System.IO.Path.Combine(this.Directory.FullName, this.Name);
                }
                else
                {
                    return this.Name;
                }
            }
        }
        
        /// <summary>
        /// Gets the parent directory for this filesystem info object is located.
        /// </summary>
        public OutputDirectoryInfo Directory { get; private set; }
    }
}
