//-----------------------------------------------------------------------
// <copyright file="AddRegItem.cs" company="42A Consulting">
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
    /// A registry key item in the INF file.
    /// </summary>
    public class AddRegItem
    {
        /// <summary>
        /// The registry key.
        /// </summary>
        private string subKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRegItem"/> class.
        /// </summary>
        /// <param name="key">The registry key name.</param>
        public AddRegItem(string key)
        {
            this.Subkey = key;
            this.OverwriteExisting = true;
        }

        /// <summary>
        /// Gets or sets a value that specifies the registry root location.
        /// </summary>
        public RegistryHives Hive { get; set; }

        /// <summary>
        /// Gets or sets a string that specifies the registry key name.
        /// </summary>
        public string Subkey
        {
            get
            {
                return this.subKey;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value");
                }

                this.subKey = value;
            }
        }

        /// <summary>
        /// Gets or sets the registry value name. If empty or null, the "(default)" registry value name is used.
        /// </summary>
        public string ValueName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this registry key should overwrite existing keys.
        /// </summary>
        public bool OverwriteExisting { get; set; }

        /// <summary>
        /// Gets or sets the registry key value data type.
        /// </summary>
        public RegistryValueTypes Type { get; set; }

        /// <summary>
        /// Gets or sets the string representation of the registry key value data type, formatted accordingly to the <see cref="Type"/> property.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Returns a string representation of avalue.
        /// </summary>
        /// <param name="value">The value to format as string.</param>
        /// <returns>A string representation of the value.</returns>
        public static string FormatValue(int value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Returns a string representation of a value.
        /// </summary>
        /// <param name="value">The value to format as string.</param>
        /// <returns>A string representation of the value.</returns>
        public static string FormatValue(string value)
        {
            return value;
        }

        /// <summary>
        /// Returns a string representation of a value.
        /// </summary>
        /// <param name="values">The value to format as string.</param>
        /// <returns>A string representation of the value.</returns>
        public static string FormatValue(params string[] values)
        {
            if (values == null)
            {
                return null;
            }
            else
            {
                return string.Join(",", values);
            }
        }

        /// <summary>
        /// Returns a string representation of a value.
        /// </summary>
        /// <param name="values">The value to format as string.</param>
        /// <returns>A string representation of the value.</returns>
        public static string FormatValue(params byte[] values)
        {
            if (values != null)
            {
                var hexList = new List<string>();
                foreach (var @byte in values)
                {
                    hexList.Add(@byte.ToString("x").PadLeft(2, '0'));
                }

                return string.Join(",", hexList.ToArray());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Sets the value of this registry key value to a DWORD (Int32) value.
        /// </summary>
        /// <param name="value">The 32-bit integer value to set.</param>
        public void SetValue(int value)
        {
            this.Type = RegistryValueTypes.DWORD;
            this.Value = FormatValue(value);
        }

        /// <summary>
        /// Sets the value of this registry key value to a SZ (string) value.
        /// </summary>
        /// <param name="value">The string value to set.</param>
        public void SetValue(string value)
        {
            this.Type = RegistryValueTypes.SZ;
            this.Value = FormatValue(value);
        }

        /// <summary>
        /// Sets the value of this registry key value to a MULTIPLE_SZ (string array) value.
        /// </summary>
        /// <param name="strings">The collection of string values to set.</param>
        public void SetValue(params string[] strings)
        {
            this.Type = RegistryValueTypes.MULTI_SZ;
            this.Value = FormatValue(strings);
        }

        /// <summary>
        /// Sets the value of this registry key value to binary data.
        /// </summary>
        /// <param name="value">A collection of bytes.</param>
        public void SetValue(params byte[] value)
        {
            this.Type = RegistryValueTypes.BINARY;
            this.Value = FormatValue(value);            
        }

        /// <summary>
        /// Returns a string representation of the current object.
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            string format = "{0},{1},{2},{3},{4}";

            if (string.IsNullOrEmpty(this.Value))
            {
                format = "{0},{1},{2},{3}";
            }

            return string.Format(
                format, 
                this.GetHiveName(), 
                this.Subkey,
                this.ValueName,
                this.GetFlagsAsHex(),
                this.Value);
        }

        /// <summary>
        /// Returns the Hive name.
        /// </summary>
        /// <returns>The hive name as a string</returns>
        private string GetHiveName()
        {
            return this.Hive.ToString();
        }

        /// <summary>
        /// Returns the registry key flags as a hexadecimal formatted string, including the '0x' prefix.
        /// </summary>
        /// <returns>A hexadecimal formatted string</returns>
        private string GetFlagsAsHex()
        {
            var flag = this.GetTypeFlag();

            if (!this.OverwriteExisting)
            {
                flag |= AddRegFlags.FLG_ADDREG_NOCLOBBER;
            }

            return string.Concat("0x", flag.ToString("x").PadLeft(8, '0'));
        }

        /// <summary>
        /// Gets the flags for the registry key.
        /// </summary>
        /// <returns>a integer value containing the option flags</returns>
        private AddRegFlags GetTypeFlag()
        {
            switch (this.Type)
            {
                case RegistryValueTypes.SZ:
                    return AddRegFlags.FLG_ADDREG_TYPE_SZ;
                case RegistryValueTypes.MULTI_SZ:
                    return AddRegFlags.FLG_ADDREG_TYPE_MULTI_SZ;
                case RegistryValueTypes.BINARY:
                    return AddRegFlags.FLG_ADDREG_TYPE_BINARY;
                case RegistryValueTypes.DWORD:
                    return AddRegFlags.FLG_ADDREG_TYPE_DWORD;

                default:
                    throw new InvalidOperationException("The Type property is set to a value that is not recognized as a valid registry key value data type.");
            }
        }
    }
}
