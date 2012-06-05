//-----------------------------------------------------------------------
// <copyright file="RegistryKeyCollection.cs" company="42A Consulting">
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
    /// A dictionary of registry keys.
    /// </summary>
    public class RegistryKeyCollection : Dictionary<string, RegistryKey>, ICollection<RegistryKey>
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="RegistryKeyCollection"/> class.
        /// </summary>
        public RegistryKeyCollection()
        {
            // nothing else to do here
        }

        /// <summary>
        /// Gets a value indicating whether the dictionary is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Adds a RegistryKey to the end of the dictionary.
        /// </summary>
        /// <param name="variable">The item to add</param>
        public void Add(RegistryKey variable)
        {
            if (this.ContainsKey(variable.Name))
            {
                this[variable.Name] = variable;
            }
            else
            {
                this.Add(variable.Name, variable);
            }
        }

        /// <summary>
        /// Determines whether the specified RegistryKey item exists in the dictionary.
        /// </summary>
        /// <param name="item">The RegistryKey item to look for.</param>
        /// <returns>
        ///     A value indicating whether a RegistryKey item with the same name as the item specified exists in the dictionary. 
        ///     If <paramref name="item"/> is null, this method will always return false.
        /// </returns>
        public bool Contains(RegistryKey item)
        {
            if (item != null && this.ContainsKey(item.Name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Copies the values in the dictionary to the specified array, starting at the specified array index.
        /// </summary>
        /// <param name="array">The target array.</param>
        /// <param name="arrayIndex">The zero-based index in the array which the copying begins.</param>
        public void CopyTo(RegistryKey[] array, int arrayIndex)
        {
            this.Values.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the any RegistryKey item from the dictionary with the same name as the specified object.
        /// </summary>
        /// <param name="item">The RegistryKey object which name to use when removing items.</param>
        /// <returns>A value indicating whether a object was removed or not. If <paramref name="item"/> is null, this method always returns false.</returns>
        public bool Remove(RegistryKey item)
        {
            if (item != null)
            {
                return this.Remove(item.Name);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets an enumerator which iterates through the RegistryKey items in this dictionary.
        /// </summary>
        /// <returns>An enumerator which iterates through the RegistryKey items.</returns>
        public new IEnumerator<RegistryKey> GetEnumerator()
        {
            return this.Values.GetEnumerator();
        }
    }
}
