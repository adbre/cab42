//-----------------------------------------------------------------------
// <copyright file="DefaultInstallSection.cs" company="42A Consulting">
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
    /// The DefaultInstall section in the cabwiz .INF file.
    /// </summary>
    public class DefaultInstallSection : InformationFileSection
    {
        /// <summary>
        /// Initializes a new instance of the DefaultInstallSection class.
        /// </summary>
        public DefaultInstallSection()
            : base("DefaultInstall")
        {
            this.AddReg = "RegKeys";
            this.CopyFiles = new List<string>();
            this.CEShortcuts = new List<string>();
        }

        /// <summary>
        /// Gets or sets the AddReg section name.
        /// </summary>
        public string AddReg { get; set; }

        /// <summary>
        /// Gets or sets a collection of files which to copy to the CAB output.
        /// </summary>
        public List<string> CopyFiles { get; set; }

        /// <summary>
        /// Gets or sets a collection of files which to include as shortcuts.
        /// </summary>
        public List<string> CEShortcuts { get; set; }
    }
}
