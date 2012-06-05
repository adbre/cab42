//-----------------------------------------------------------------------
// <copyright file="InformationFileSection.cs" company="42A Consulting">
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

    public class InformationFileSection
    {
        public InformationFileSection(string sectionName)
        {
            this.SectionName = sectionName;
        }

        [InformationFileIgnore(true)]
        public string SectionName { get; private set; }

        public virtual void WriteSection(System.IO.Stream s, Encoding encoding)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            this.WriteSectionTitle(s, encoding);

            foreach (var kvp in this.GetKeysAndValues())
            {
                this.WriteLine(s, encoding, kvp.Key, kvp.Value);
            }
        }

        protected virtual void WriteSectionTitle(System.IO.Stream s, Encoding encoding)
        {
            this.WriteSectionTitle(s, encoding, this.SectionName);
        }

        protected virtual void WriteSectionTitle(System.IO.Stream s, Encoding encoding, string sectionName)
        {
            this.WriteLine(s, encoding, string.Format("[{0}]", sectionName));
        }

        protected virtual void WriteLine(System.IO.Stream s, Encoding encoding, string line)
        {
            InformationFile.WriteLine(s, encoding, line);
        }

        protected virtual IEnumerable<KeyValuePair<string, string>> GetKeysAndValues()
        {
            var type = this.GetType();

            var properties = type.GetProperties();

            var l = this.CreateKeyValuePairCollection();

            foreach (var property in properties)
            {
                var skip = false;
                var quote = true;
                var attributes = property.GetCustomAttributes(true);

                var value = property.GetValue(this, null);

                foreach (var attribute in attributes)
                {
                    if (attribute == null)
                    {
                        continue;
                    }

                    var attributeType = attribute.GetType();

                    if (attributeType == typeof(InformationFileIgnoreAttribute))
                    {
                        var attributeIgnore = attribute as InformationFileIgnoreAttribute;
                        if (attributeIgnore != null)
                        {
                            skip = attributeIgnore.Ignore;
                        }
                    }
                    else if (attributeType == typeof(InformationFileIgnoreIfEmptyAttribute))
                    {
                        var attributeIgnore = attribute as InformationFileIgnoreIfEmptyAttribute;
                        if (attributeIgnore != null)
                        {
                            if (attributeIgnore.IgnoreIfEmpty)
                            {
                                if (value == null || string.IsNullOrEmpty(value.ToString()))
                                {
                                    skip = true;
                                }
                            }
                        }
                    }
                    else if (attributeType == typeof(InformationFileQuoteAttribute))
                    {
                        var attributeQuote = attribute as InformationFileQuoteAttribute;
                        if (attributeQuote != null)
                        {
                            quote = attributeQuote.Quote;
                        }
                    }
                }

                if (skip)
                    continue;

                if (value != null && typeof(IEnumerable<object>).IsInstanceOfType(value))
                {
                    List<string> parts = new List<string>();

                    var valueEnumerable = value as IEnumerable<object>;

                    foreach (var valueItem in valueEnumerable)
                    {
                        if (valueItem != null)
                        {
                            parts.Add(this.Quote(valueItem.ToString()));
                        }
                        else
                        {
                            parts.Add(null);
                        }
                    }

                    value = string.Join(",", parts.ToArray());
                }
                else if (value != null && quote)
                {
                    string stringValue = value as string;

                    if (!string.IsNullOrEmpty(stringValue))
                    {
                        value = this.Quote(stringValue);
                    }
                }

                if (value == null)
                    value = string.Empty;

                l.Add(new KeyValuePair<string, string>(property.Name, value.ToString()));
            }

            return l.ToArray();
        }

        protected List<KeyValuePair<string, string>> CreateKeyValuePairCollection()
        {
            return new List<KeyValuePair<string, string>>();
        }

        protected string Quote(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                return string.Concat("\"", s, "\"");
            }

            return s;
        }

        private void WriteLine(System.IO.Stream s, Encoding encoding, string key, string value)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            var line = string.Format("{0}={1}", key, value);

            InformationFile.WriteLine(s, encoding, line);
        }
    }
}
