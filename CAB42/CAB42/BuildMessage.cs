// -----------------------------------------------------------------------
// <copyright file="BuildMessage.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace C42A.CAB42
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A message which describes a error, warning or information message to give the user feedback from a build process.
    /// </summary>
    public class BuildMessage
    {
        /// <summary>
        /// Initializes a new instance of the BuildMessage class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="file">A filename</param>
        /// <param name="line">The line number in the file.</param>
        /// <param name="column">The column number in the file.</param>
        /// <param name="type">The type of message</param>
        public BuildMessage(string message, string file, int line, int column, BuildMessageType type)
            : this()
        {
            this.Description = message;
            this.File = file;
            this.Line = line;
            this.Column = column;
            this.Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the BuildMessage class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="file">A filename</param>
        /// <param name="line">The line number in the file.</param>
        /// <param name="type">The type of message</param>
        public BuildMessage(string message, string file, int line, BuildMessageType type)
            : this(message, file, line, 0, type)
        {
            // nothing else to do here
        }

        /// <summary>
        /// Initializes a new instance of the BuildMessage class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="file">A filename</param>
        /// <param name="type">The type of message</param>
        public BuildMessage(string message, string file, BuildMessageType type)
            : this(message, file, 0, 0, type)
        {
            // nothing else to do here
        }

        /// <summary>
        /// Initializes a new instance of the BuildMessage class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type of message</param>
        public BuildMessage(string message, BuildMessageType type)
            : this(message, null, 0, 0, type)
        {
            // nothing else to do here
        }

        /// <summary>
        /// Initializes a new instance of the BuildMessage class.
        /// </summary>
        /// <param name="x">A exception object describing a error</param>
        public BuildMessage(Exception x)
            : this()
        {
            this.Description = x.Message;
            this.Error = x;
            this.Type = BuildMessageType.Error;
        }

        /// <summary>
        /// Initializes a new instance of the BuildMessage class.
        /// </summary>
        public BuildMessage()
        {
            // nothing else to do here.
        }

        /// <summary>
        /// Gets or sets the message string.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the file associated with this message.
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Gets or sets the line number associated with this message.
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// Gets or sets the column number associated with this message.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Gets or sets the name of the project associated with this message.
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// Gets or sets the name of the configuration associated with this message.
        /// </summary>
        public string Configuration { get; set; }

        /// <summary>
        /// Gets or sets the exception object associated with this message.
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// Gets or sets the message type of this message.
        /// </summary>
        public BuildMessageType Type { get; set; }
    }
}
