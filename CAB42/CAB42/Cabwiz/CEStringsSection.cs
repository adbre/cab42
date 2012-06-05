//-----------------------------------------------------------------------
// <copyright file="CEStringsSection.cs" company="42A Consulting">
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
    /// The CEStrings section in the cabwiz .INF file.
    /// </summary>
    public class CEStringsSection : InformationFileSection
    {
        /// <summary>
        /// Initializes a new instance of the CEStringsSection class.
        /// </summary>
        public CEStringsSection()
            : base("CEStrings")
        {
            this.AppName = string.Empty;
            this.InstallDir = @"%CE1%\%AppName%";
        }

        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// Gets or sets the installation directory. This will default to '%CE1%\%AppName%'.
        /// </summary>
        public string InstallDir { get; set; }
    }
}
