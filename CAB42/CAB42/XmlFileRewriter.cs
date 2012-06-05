//-----------------------------------------------------------------------
// <copyright file="XmlFileRewriter.cs" company="42A Consulting">
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
    using System.Xml;

    public class XmlFileRewriter
    {
        public const string RootElement = "#document";
        public const string TextElement = "#text";

        public XmlFileRewriter()
        {
            this.ReplaceValues = new Dictionary<string, string>();
        }

        private Dictionary<string, string> ReplaceValues { get; set; }

        public static bool IsPathRooted(string path)
        {
            return !string.IsNullOrEmpty(path) && path.StartsWith(RootElement);
        }

        public static bool IsPathTextElement(string path)
        {
            return !string.IsNullOrEmpty(path) && path.EndsWith(TextElement);
        }

        public static string Combine(string path1, string path2)
        {
            return System.IO.Path.Combine(path1, path2);
        }

        public void AddTextPath(string path, string value)
        {
            if (!IsPathRooted(path))
            {
                path = Combine(RootElement, path);
            }

            if (!IsPathTextElement(path))
            {
                path = Combine(path, TextElement);
            }

            if (this.ReplaceValues.ContainsKey(path))
            {
                this.ReplaceValues[path] = value;
            }
            else
            {
                this.ReplaceValues.Add(path, value);
            }
        }

        public void Clear()
        {
            this.ReplaceValues.Clear();
        }

        public void Rewrite(System.IO.FileInfo sourceFile, System.IO.FileInfo targetFile)
        {
            if (sourceFile == null)
            {
                throw new ArgumentNullException("sourceFile");
            }

            if (targetFile == null)
            {
                throw new ArgumentNullException("targetFile");
            }

            if (!sourceFile.Exists)
            {
                throw new System.IO.FileNotFoundException("The source file could not be found.", sourceFile.FullName);
            }

            if (!targetFile.Directory.Exists)
            {
                try
                {
                    targetFile.Directory.Create();
                }
                catch (Exception x)
                {
                    throw new System.IO.IOException("Could not create the directory for the target file.", x);
                }
            }

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(sourceFile.FullName);

            this.Rewrite(xmlDocument.DocumentElement);

            xmlDocument.Save(targetFile.FullName);
        }

        public void Rewrite(string source, string target)
        {
            this.Rewrite(new System.IO.FileInfo(source), new System.IO.FileInfo(target));
        }

        private string GetXmlElementPath(XmlNode node)
        {
            if (node.ParentNode != null)
            {
                return Combine(this.GetXmlElementPath(node.ParentNode), node.Name);
            }
            else
            {
                return node.Name;
            }
        }

        private void Rewrite(XmlNode node)
        {
            if (this.ReplaceValues == null)
            {
                return;
            }

            var path = this.GetXmlElementPath(node);
            var textPath = System.IO.Path.Combine(path, TextElement);

             if (node.NodeType == XmlNodeType.Element && this.ReplaceValues.ContainsKey(textPath))
            {
                var replaceValue = this.ReplaceValues[textPath] ?? string.Empty;

                if (node.HasChildNodes && node.FirstChild.NodeType == XmlNodeType.Text)
                {
                    node.FirstChild.Value = replaceValue;
                }
                else if (!node.HasChildNodes)
                {
                    var textNode = node.OwnerDocument.CreateTextNode(replaceValue);

                    node.AppendChild(textNode);
                }
            }
            else
            {
                foreach (XmlNode subNode in node.ChildNodes)
                {
                    this.Rewrite(subNode);
                }
            }
        }
    }
}
