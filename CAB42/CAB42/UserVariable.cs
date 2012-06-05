//-----------------------------------------------------------------------
// <copyright file="UserVariable.cs" company="42A Consulting">
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

    /// <summary>
    /// Describes a user-defined variable in a project.
    /// </summary>
    public class UserVariable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserVariable"/> class.
        /// </summary>
        public UserVariable()
        {
            // nothing else to do here
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserVariable"/> class, with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The variable's value.</param>
        public UserVariable(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the name of the variable.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the variable.
        /// </summary>
        public string Value { get; set; }
    }
}
