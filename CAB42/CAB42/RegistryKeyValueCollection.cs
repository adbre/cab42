//-----------------------------------------------------------------------
// <copyright file="RegistryKeyValueCollection.cs" company="42A Consulting">
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
    /// A dictionary of <see cref="RegistryKeyValue"/> objects.
    /// </summary>
    public class RegistryKeyValueCollection : Dictionary<string, RegistryKeyValue>, ICollection<RegistryKeyValue>, System.Collections.ICollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryKeyValueCollection"/> class.
        /// </summary>
        public RegistryKeyValueCollection()
        {
            // nothing else to do here.
        }
        
        /// <summary>
        /// Gets a value indicating whether this collection is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Adds the specified <see cref="RegistryKeyValue"/> item to the dictionary.
        /// </summary>
        /// <param name="variable">The <see cref="RegistryKeyValue"/> item to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="variable"/> is a null reference</exception>
        /// <exception cref="ArgumentException">The Name property of <paramref name="variable"/> is a null reference or a empty string.</exception>
        public void Add(RegistryKeyValue variable)
        {
            if (variable == null)
            {
                throw new ArgumentNullException("variable");
            }

            if (string.IsNullOrEmpty(variable.Name))
            {
                throw new ArgumentException("The RegistryKeyValue Name property is null or empty.", "variable");
            }

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
        /// Determines whether the specified <see cref="RegistryKeyValue"/> item exists in this collection.
        /// </summary>
        /// <param name="item">The <see cref="RegistryKeyValue"/> item to search for.</param>
        /// <returns>
        ///     True if a <see cref="RegistryKeyValue"/> object exists in this dictionary with the same Name as the specified item, false otherwise.
        /// </returns>
        public bool Contains(RegistryKeyValue item)
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
        /// Copies the <see cref="RegistryKeyValue"/> items in this dictionary to the specified array, starting at the specified index.
        /// </summary>
        /// <param name="array">The array to copy data to.</param>
        /// <param name="arrayIndex">A zero-based array index at which the copying begins.</param>
        public void CopyTo(RegistryKeyValue[] array, int arrayIndex)
        {
            this.Values.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes a <see cref="RegistryKeyValue"/> item from the dictionary.
        /// </summary>
        /// <param name="item">A <see cref="RegistryKeyValue"/> object with the same name as the item to be removed.</param>
        /// <returns>A value indicating whether a item was removed or not.</returns>
        public bool Remove(RegistryKeyValue item)
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
        /// Gets an enumerator which iterates through all <see cref="RegistryKeyValue"/> objects in this dictionary.
        /// </summary>
        /// <returns>
        ///     An enumerator which iterates through all <see cref="RegistryKeyValue"/> objects in this dictionary.
        /// </returns>
        public new IEnumerator<RegistryKeyValue> GetEnumerator()
        {
            return this.Values.GetEnumerator();
        }
    }
}
