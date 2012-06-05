//-----------------------------------------------------------------------
// <copyright file="UserVariableCollection.cs" company="42A Consulting">
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
    /// A dictionary of <see cref="UserVariable"/> objects.
    /// </summary>
    public class UserVariableCollection : Dictionary<string, UserVariable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserVariableCollection"/> class.
        /// </summary>
        public UserVariableCollection()
        {
            // nothing else to do here.
        }

        /// <summary>
        /// Adds the specified <see cref="UserVariable"/> item to the dictionary.
        /// </summary>
        /// <param name="variable">The <see cref="UserVariable"/> item to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="variable"/> is a null reference</exception>
        /// <exception cref="ArgumentException">The Name property of <paramref name="variable"/> is a null reference or a empty string.</exception>
        public void Add(UserVariable variable)
        {
            if (variable == null)
            {
                throw new ArgumentNullException("variable");
            }

            if (string.IsNullOrEmpty(variable.Name))
            {
                throw new ArgumentException("The Name property is null or empty.", "variable");
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
        /// Creates and adds a <see cref="UserVariable"/> item to the dictionary, with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The variable's value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="variable"/> is a null reference</exception>
        /// <exception cref="ArgumentException">The Name property of <paramref name="variable"/> is a null reference or a empty string.</exception>
        /// <returns>The <see cref="UserVariable"/> object which was added.</returns>
        public UserVariable Add(string name, string value)
        {
            var variable = new UserVariable(name, value);

            this.Add(variable);

            return variable;
        }
    }
}
