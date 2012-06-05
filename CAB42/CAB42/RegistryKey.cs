//-----------------------------------------------------------------------
// <copyright file="RegistryKey.cs" company="42A Consulting">
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
    /// A object which represents a registry key to be created on the target machine.
    /// </summary>
    public class RegistryKey
    {
        /// <summary>
        /// The key name
        /// </summary>
        private string keyName;

        /// <summary>
        /// Initializes a new instance of the RegistryKey class.
        /// </summary>
        public RegistryKey()
        {
            this.Values = new RegistryKeyValueCollection();
        }

        /// <summary>
        /// Initializes a new instance of the RegistryKey class with the specified key name.
        /// </summary>
        /// <param name="keyName">The key name</param>
        public RegistryKey(string keyName)
            : this()
        {
            this.Name = keyName;
        }

        /// <summary>
        /// Gets or sets the collection of registry key values.
        /// </summary>
        [XmlIgnore]
        public RegistryKeyValueCollection Values { get; set; }

        /// <summary>
        /// Gets or sets the registry hive which this key will reside.
        /// </summary>
        [XmlIgnore]
        public Cabwiz.RegistryHives Hive { get; set; }

        /// <summary>
        /// Gets or sets the registry key name, including information about the key hive.
        /// </summary>
        [XmlAttribute("Key")]
        public string Name
        {
            get
            {
                return this.keyName;
            }

            set
            {
                this.keyName = value;

                if (!string.IsNullOrEmpty(value))
                {
                    var pathRoot = C42A.CSharp.FileUtils.GetTopDirectory(value);

                    if (!string.IsNullOrEmpty(pathRoot))
                    {
                        Cabwiz.RegistryHives hive;
                        if (!Enum.TryParse<Cabwiz.RegistryHives>(pathRoot, out hive))
                        {
                            throw new FormatException("The path root is not a valid registry key hive name: " + pathRoot);
                        }
                        else
                        {
                            this.Hive = hive;
                        }
                    }
                    else
                    {
                        throw new FormatException("The specified key does not contain information about the key hive.");
                    }
                }
                else
                {
                    throw new ArgumentNullException("value");
                }
            }
        }

        /// <summary>
        /// Gets or sets the registry key values. 
        /// Do not use this property from code. 
        /// This property only exists as a means to serialize the values.
        /// </summary>
        [XmlArray("Values"), XmlArrayItem("Value")]
        public RegistryKeyValue[] ValueArray
        {
            get
            {
                return this.Values.Values.ToArray();
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    var keyValues = new RegistryKeyValueCollection();

                    foreach (var keyValue in value)
                    {
                        AddValue(keyValues, keyValue);
                    }

                    this.Values = keyValues;
                }
                else
                {
                    this.Values.Clear();
                }
            }
        }

        /// <summary>
        /// Adds a registry key value to this registry key.
        /// </summary>
        /// <param name="value">The RegistryKeyValue object to add.</param>
        public void AddValue(RegistryKeyValue value)
        {
            AddValue(this.Values, value);
        }

        /// <summary>
        /// Adds a registry key value to the registry key using the specified name and value.
        /// </summary>
        /// <param name="name">The name of the value.</param>
        /// <param name="value">The data of the value.</param>
        /// <returns>
        ///     The RegistryKeyValue object which was added.
        /// </returns>
        public RegistryKeyValue AddValue(string name, int value)
        {
            var keyValue = new RegistryKeyValue(name, value);

            this.AddValue(keyValue);

            return keyValue;
        }

        /// <summary>
        /// Adds a registry key value to the registry key using the specified name and value.
        /// </summary>
        /// <param name="name">The name of the value.</param>
        /// <param name="value">The data of the value.</param>
        /// <returns>
        ///     The RegistryKeyValue object which was added.
        /// </returns>
        public RegistryKeyValue AddValue(string name, string value)
        {
            var keyValue = new RegistryKeyValue(name, value);

            this.AddValue(keyValue);

            return keyValue;
        }

        /// <summary>
        /// Adds a registry key value to the registry key using the specified name and value.
        /// </summary>
        /// <param name="name">The name of the value.</param>
        /// <param name="value">The data of the value.</param>
        /// <returns>
        ///     The RegistryKeyValue object which was added.
        /// </returns>
        public RegistryKeyValue AddValue(string name, params string[] value)
        {
            var keyValue = new RegistryKeyValue(name, value);

            this.AddValue(keyValue);

            return keyValue;
        }

        /// <summary>
        /// Adds a registry key value to the registry key using the specified name and value.
        /// </summary>
        /// <param name="name">The name of the value.</param>
        /// <param name="value">The data of the value.</param>
        /// <returns>
        ///     The RegistryKeyValue object which was added.
        /// </returns>
        public RegistryKeyValue AddValue(string name, params byte[] value)
        {
            var keyValue = new RegistryKeyValue(name, value);

            this.AddValue(keyValue);

            return keyValue;
        }

        /// <summary>
        /// Adds a registry key value to the specified key value collection.
        /// </summary>
        /// <param name="collection">The collection which the value should be added.</param>
        /// <param name="value">The registry key value.</param>
        private static void AddValue(RegistryKeyValueCollection collection, RegistryKeyValue value)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (string.IsNullOrEmpty(value.Name))
            {
                throw new InvalidOperationException("One of the registry key values had no name specified.");
            }
            else if (collection.ContainsKey(value.Name))
            {
                throw new InvalidOperationException("One of the registry key value names occured more than once for the same key: " + value.Name);
            }
            else
            {
                collection.Add(value.Name, value);
            }
        }
    }
}
