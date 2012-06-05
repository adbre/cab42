//-----------------------------------------------------------------------
// <copyright file="XmlReplacementRule.cs" company="42A Consulting">
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
    /// Describes a XML replacement rule, which is used by the <see cref="XmlFileRewriter"/> class.
    /// </summary>
    public class XmlReplacementRule
    {
        /// <summary>
        /// Gets or sets the path in a XML document to a text element which is to be replaced with the string specified by the Value property.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets a string which will be replacing any existing contents in the specified XML tag.
        /// </summary>
        public string Value { get; set; }
    }
}
