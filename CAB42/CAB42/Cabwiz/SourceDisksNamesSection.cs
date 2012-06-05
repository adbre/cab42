//-----------------------------------------------------------------------
// <copyright file="SourceDisksNamesSection.cs" company="42A Consulting">
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

    public class SourceDisksNamesSection : InformationFileSection
    {
        public SourceDisksNamesSection()
            : base("SourceDisksNames")
        {

            this.Values = new Dictionary<int, SourceDiskName>();
        }

        public Dictionary<int, SourceDiskName> Values { get; private set; }

        public int Add(string path)
        {
            int index = this.NextIndex();

            return this.Add(string.Format("Common{0}", index, path), path);
        }

        public int Add(string comment, string path)
        {
            int index = this.NextIndex();
            this.Values.Add(index, new SourceDiskName(comment, path));

            return index;
        }

        protected override IEnumerable<KeyValuePair<string, string>> GetKeysAndValues()
        {
            var l = this.CreateKeyValuePairCollection();

            foreach (var kvp in this.Values)
            {
                l.Add(new KeyValuePair<string, string>(kvp.Key.ToString(), kvp.Value.ToString()));
            }

            return l.ToArray();
        }

        private int NextIndex()
        {
            return this.Values.Count + 1;
        }
    }
}
