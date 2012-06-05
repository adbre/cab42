//-----------------------------------------------------------------------
// <copyright file="CEDeviceSection.cs" company="42A Consulting">
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
    /// A Cabwiz .inf CEDevice section.
    /// </summary>
    public class CEDeviceSection : InformationFileSection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CEDeviceSection"/> class.
        /// </summary>
        public CEDeviceSection()
            : base("CEDevice")
        {
            this.VersionMin = "4.0";
            this.VersionMax = "6.99";
            this.BuildMax = "0xE0000000";
        }

        /// <summary>
        /// Gets or sets the minimum version value. Defaults to "4.0".
        /// </summary>
        [InformationFileQuote(false)]
        public string VersionMin { get; set; }

        /// <summary>
        /// Gets or sets the maximum version value. Defaults to "6.99".
        /// </summary>
        [InformationFileQuote(false)]
        public string VersionMax { get; set; }

        /// <summary>
        /// Gets or sets the maximum build value. Defaults to "0xE0000000".
        /// </summary>
        [InformationFileQuote(false)]
        public string BuildMax { get; set; }
    }
}
