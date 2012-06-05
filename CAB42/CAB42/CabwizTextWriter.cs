//-----------------------------------------------------------------------
// <copyright file="CabwizTextWriter.cs" company="42A Consulting">
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
    /// A class which will extract information, warning and error messages from a string output from a cabwiz.exe process.
    /// </summary>
    public class CabwizTextWriter : FeedbackTextWriter
    {
        private IBuildTask activeTask;

        /// <summary>
        /// Initializes a new instance of the CabwizTextWriter class.
        /// </summary>
        /// <param name="feedback">The feedback target</param>
        /// <param name="encoding">The encoding to use</param>
        public CabwizTextWriter(IBuildFeedback feedback, Encoding encoding)
            : base(feedback, encoding)
        {
            this.InitializeMapping();
        }

        /// <summary>
        /// Initializes a new instance of the CabwizTextWriter class, using UTF-8 encoding.
        /// </summary>
        /// <param name="feedback">The feedback target</param>
        public CabwizTextWriter(IBuildFeedback feedback)
            : base(feedback)
        {
            this.InitializeMapping();
        }

        /// <summary>
        /// Gets the mapping of string prefixes to the build message types.
        /// </summary>
        public Dictionary<string, BuildMessageType> MessageMapping { get; private set; }

        public void SetActiveTask(IBuildTask task)
        {
            this.activeTask = task;
        }

        /// <summary>
        /// Writes a string to the feedback target output, extracting any build message found in the string.
        /// </summary>
        /// <param name="value">The string to write</param>
        public override void Write(string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && this.MessageMapping != null)
            {
                foreach (var mapping in this.MessageMapping)
                {
                    if (value.StartsWith(mapping.Key))
                    {
                        var description = value.Substring(mapping.Key.Length).Trim();

                        var message = new BuildMessage()
                        {
                            Type = mapping.Value,
                            Description = description
                        };

                        if (this.activeTask != null)
                        {
                            message.Configuration = this.activeTask.Configuration;
                            message.Project = this.activeTask.ProjectName;
                        }

                        this.FeedbackTarget.AddMessage(message);

                        break;
                    }
                }
            }

            base.Write(value);
        }

        /// <inheritdoc />
        public override void WriteLine(string value)
        {
            this.Write(string.Concat(value, Environment.NewLine));
        }

        /// <summary>
        /// Initializes the MessageMapping property.
        /// </summary>
        private void InitializeMapping()
        {
            this.MessageMapping = new Dictionary<string, BuildMessageType>();

            this.MessageMapping.Add("Information: ", BuildMessageType.Information);
            this.MessageMapping.Add("Warning: ", BuildMessageType.Warning);
            this.MessageMapping.Add("Error: ", BuildMessageType.Error);
        }
    }
}
