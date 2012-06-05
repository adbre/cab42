//-----------------------------------------------------------------------
// <copyright file="BuildFeedbackBase.cs" company="42A Consulting">
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
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A build feedback object which uses a textwriter as the underlying output target.
    /// </summary>
    public class BuildFeedbackBase : IBuildFeedback, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildFeedbackBase"/> class.
        /// </summary>
        public BuildFeedbackBase()
        {
            this.Messages = new BuildMessageCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildFeedbackBase"/> class using the default constructor and the specified textwriter.
        /// </summary>
        /// <param name="output">The underlying output target to use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="output"/> is a null reference</exception>
        public BuildFeedbackBase(TextWriter output)
            : this()
        {
            if (output == null)
            {
                throw new ArgumentNullException("output");
            }

            this.TextWriter = output;
        }

        /// <summary>
        /// Gets or sets the underlying TextWriter output target.
        /// </summary>
        public System.IO.TextWriter TextWriter { get; set; }

        /// <summary>
        /// Gets or sets a list of build messages.
        /// </summary>
        public BuildMessageCollection Messages { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether Exception objects when added as messages should also be printed to the textwriter.
        /// </summary>
        public bool PrintExceptions { get; set; }

        /// <summary>
        /// Disposes the underlying textwriter object.
        /// </summary>
        public void Dispose()
        {
            if (this.TextWriter != null)
            {
                this.TextWriter.Dispose();
                this.TextWriter = null;
            }
        }

        public void AddMessage(BuildMessage message)
        {
            this.Messages.Add(message);
        }

        public void AddMessage(string description, string file, int line, int column, BuildMessageType type)
        {
            this.Messages.Add(description, file, line, column, type);
        }

        public void AddMessage(Exception x)
        {
            this.Messages.Add(x);

            if (this.PrintExceptions)
            {
                this.WriteLine(x.ToString());
            }
        }

        public void WriteLine()
        {
            this.TextWriter.WriteLine();
        }

        public void WriteLine(string value)
        {
            this.TextWriter.WriteLine(value);
        }

        public void WriteLine(string format, params object[] args)
        {
            this.TextWriter.WriteLine(format, args);
        }

        public void Write(string value)
        {
            this.TextWriter.Write(value);
        }
    }
}
