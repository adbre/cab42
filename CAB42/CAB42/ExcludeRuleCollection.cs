//-----------------------------------------------------------------------
// <copyright file="ExcludeRuleCollection.cs" company="42A Consulting">
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
    /// A collection of <see cref="ExcludeRule"/> objects.
    /// </summary>
    public class ExcludeRuleCollection : List<ExcludeRule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExcludeRuleCollection"/> class.
        /// </summary>
        public ExcludeRuleCollection()
        {
            // nothing
        }

        /// <summary>
        /// Determines whether <paramref name="path"/> is excluded by any of the <see cref="ExcludeRule"/> items in this collection.
        /// </summary>
        /// <param name="path">The path to search for.</param>
        /// <returns>A value indicating whether <paramref name="path"/> is excluded by any of the rules in this collection.</returns>
        public bool IsExcluded(string path)
        {
            var match = this.Find(path);

            return match != null && match.Length > 0;
        }

        /// <summary>
        /// Searches for exclude rules in this collection which matches <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path to search for.</param>
        /// <returns>A array of <see cref="ExcludeRule"/> which matches <paramref name="path"/></returns>
        public ExcludeRule[] Find(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                var l = new List<ExcludeRule>();
                foreach (var rule in this)
                {
                    if (rule.FileName.Equals(path, StringComparison.InvariantCultureIgnoreCase))
                    {
                        l.Add(rule);
                    }
                }

                return l.ToArray();
            }

            return null;
        }

        /// <summary>
        /// Adds a exclude rule for the specified path to the collection.
        /// </summary>
        /// <param name="path">The path to exclude.</param>
        /// <returns>The <see cref="ExcludeRule"/> added.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is null or empty.</exception>
        public ExcludeRule Add(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            var rule = new ExcludeRule() { FileName = path };

            this.Add(rule);

            return rule;
        }

        /// <summary>
        /// Removes a path from the collection.
        /// </summary>
        /// <param name="path">The path to remove.</param>
        /// <remarks>
        ///     <para>
        ///         If this collection contains multiple rules which all matches <paramref name="path"/>, 
        ///         all of them will be removed from the collection
        ///     </para>
        /// </remarks>
        public void Remove(string path)
        {
            var matches = this.Find(path);

            foreach (var match in matches)
            {
                this.Remove(match);
            }
        }
    }
}
