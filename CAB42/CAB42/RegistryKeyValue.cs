//-----------------------------------------------------------------------
// <copyright file="RegistryKeyValue.cs" company="42A Consulting">
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
    /// Represents a registry key value on a target machine.
    /// </summary>
    public class RegistryKeyValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryKeyValue"/> class.
        /// </summary>
        public RegistryKeyValue()
        {
            // nothing else to do here.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryKeyValue"/> class, with the specified value name.
        /// </summary>
        /// <param name="name">The name of the registry key value.</param>
        public RegistryKeyValue(string name)
            : this()
        {
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryKeyValue"/>, with the specified value name and data.
        /// </summary>
        /// <param name="name">The name of the registry key value.</param>
        /// <param name="value">The value data.</param>
        public RegistryKeyValue(string name, int value)
            : this(name)
        {
            this.Type = Cabwiz.RegistryValueTypes.DWORD;
            this.Value = Cabwiz.AddRegItem.FormatValue(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryKeyValue"/>, with the specified value name and data.
        /// </summary>
        /// <param name="name">The name of the registry key value.</param>
        /// <param name="value">The value data.</param>
        public RegistryKeyValue(string name, string value)
            : this(name)
        {
            this.Type = Cabwiz.RegistryValueTypes.SZ;
            this.Value = Cabwiz.AddRegItem.FormatValue(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryKeyValue"/>, with the specified value name and data.
        /// </summary>
        /// <param name="name">The name of the registry key value.</param>
        /// <param name="value">The value data.</param>
        public RegistryKeyValue(string name, params string[] value)
            : this(name)
        {
            this.Type = Cabwiz.RegistryValueTypes.MULTI_SZ;
            this.Value = Cabwiz.AddRegItem.FormatValue(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryKeyValue"/>, with the specified value name and data.
        /// </summary>
        /// <param name="name">The name of the registry key value.</param>
        /// <param name="value">The value data.</param>
        public RegistryKeyValue(string name, params byte[] value)
            : this(name)
        {
            this.Type = Cabwiz.RegistryValueTypes.BINARY;
            this.Value = Cabwiz.AddRegItem.FormatValue(value);
        }

        /// <summary>
        /// Gets or sets the <see cref="RegistryKey"/> object which this value resides in.
        /// </summary>
        [XmlIgnore]
        public RegistryKey Key { get; set; }

        /// <summary>
        /// Gets or sets the name of this value.
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }
    
        /// <summary>
        /// Gets or sets the registry value type (example DWORD or SZ etc).
        /// </summary>
        [XmlAttribute]
        public Cabwiz.RegistryValueTypes Type { get; set; }

        /// <summary>
        /// Gets or sets the string representation of the value.
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}
