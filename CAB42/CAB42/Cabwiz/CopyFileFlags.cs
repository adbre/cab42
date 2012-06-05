//-----------------------------------------------------------------------
// <copyright file="CopyFileFlags.cs" company="42A Consulting">
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

    [Flags]
    public enum CopyFileFlags : uint
    {
        /// <summary>
        /// Default flags.
        /// </summary>
        Default = 0x00000000,

        /// <summary>
        /// Warn a user if an attempt is made to skip a file after an error occurs.
        /// </summary>
        WarnIfSkip = 0x00000001,

        /// <summary>
        /// Do not allow a user to skip copying a file.
        /// </summary>
        NoSkip = 0x00000002,

        /// <summary>
        /// Do not overwrite a file in the destination directory.
        /// </summary>
        NoOverwrite = 0x00000010,

        /// <summary>
        /// Copy the source file to the destination directory only if the file is in the destination directory.
        /// </summary>
        ReplaceOnly = 0x00000400,

        /// <summary>
        /// Do not copy files if the target file is newer.
        /// </summary>
        NoDateDialog = 0x20000000,

        /// <summary>
        /// Ignore date while overwriting the target file.
        /// </summary>
        NoDateCheck = 0x40000000,

        /// <summary>
        /// Create a reference when a shared DLL is counted.
        /// </summary>
        Shared = 0x80000000
    }    
}
