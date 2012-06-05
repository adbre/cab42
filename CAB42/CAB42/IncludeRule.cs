//-----------------------------------------------------------------------
// <copyright file="IncludeRule.cs" company="42A Consulting">
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
    using System.Xml.Serialization;

    /// <summary>
    /// A rule which describes one or more files to be included in a build project.
    /// </summary>
    public class IncludeRule
    {
        public IncludeRule()
        {
            this.XmlReplacementRules = new XmlReplacementRuleCollection();
        }

        /// <summary>
        /// Gets or sets he path to include. This path can contain wildcards '*' to match multiple files.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This path is relative to the project's install path.
        ///     </para>
        /// </remarks>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the destination folder on the target machine, relative to the install path.
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// Gets or sets the destination file name on the target machine.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This value must be null if <see cref="Path"/> matches multiple files.
        ///     </para>
        /// </remarks>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets a relative path and filename for a shortcut to the included file to be created on the target machine's start menu.
        /// </summary>
        public string StartMenuShortcut { get; set; }

        /// <summary>
        /// Gets or sets a collection of XML replacement rules which can be used to customize a XML file with specific settings for a profile.
        /// </summary>
        public XmlReplacementRuleCollection XmlReplacementRules { get; set; }

        
        /// <summary>
        /// Gets a value indicating whether this include rule includes a single file.
        /// </summary>
        [XmlIgnore]
        public bool IsSingleFile
        {
            get
            {
                if (this.FileName != null && this.FileName.Contains('*'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
