//-----------------------------------------------------------------------
// <copyright file="VersionSection.cs" company="42A Consulting">
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

    public class VersionSection : InformationFileSection
    {
        public VersionSection()
            : base("Version")
        {
            this.Signature = "$Windows NT$";
            this.Provider = "Microsoft";
            this.CESignature = "$Windows CE$";
        }

        public string Signature { get; set; }

        public string Provider { get; set; }

        public string CESignature { get; set; }
    }
}
