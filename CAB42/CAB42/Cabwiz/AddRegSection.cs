//-----------------------------------------------------------------------
// <copyright file="AddRegSection.cs" company="42A Consulting">
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
    /// A Cabwiz .inf section for adding registry keys on a target machine.
    /// </summary>
    public class AddRegSection : InformationFileSection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRegSection"/> class.
        /// </summary>
        /// <param name="sectionName">The name of the section.</param>
        public AddRegSection(string sectionName)
            : base(sectionName)
        {
            this.RegistryKeys = new List<AddRegItem>();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRegSection"/> class with the name 'AddReg'.
        /// </summary>
        public AddRegSection()
            : this("AddReg")
        {
            // nothing else
        }

        /// <summary>
        /// Gets a list of <see cref="AddRegItem"/>.
        /// </summary>
        public List<AddRegItem> RegistryKeys { get; private set; }

        /// <summary>
        /// Writes the current section, and all items, to the specified stream.
        /// </summary>
        /// <param name="s">The stream to write to.</param>
        /// <param name="encoding">The encoding to use when writing.</param>
        public override void WriteSection(System.IO.Stream s, Encoding encoding)
        {
            this.WriteSectionTitle(s, encoding);

            foreach (var file in this.RegistryKeys)
            {
                this.WriteLine(s, encoding, file.ToString());
            }
        }
    }
}
