// -----------------------------------------------------------------------
// <copyright file="FeedbackTextWriter.cs" company="Microsoft">
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
    /// A class for forwarding data written to a IBuildFeedback interface.
    /// </summary>
    public class FeedbackTextWriter : System.IO.TextWriter
    {
        /// <summary>
        /// The underlying encoding.
        /// </summary>
        private Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the FeedbackTextWriter class using the specified feedback object as the underlying output target and the specified encoding.
        /// </summary>
        /// <param name="feedback">The feedback object</param>
        /// <param name="encoding">The encoding</param>
        public FeedbackTextWriter(IBuildFeedback feedback, Encoding encoding)
        {
            if (feedback == null)
            {
                throw new ArgumentNullException("feedback");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            this.FeedbackTarget = feedback;
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the FeedbackTextWriter class using the specified feedback object as the underlying output target, using UTF-8 encoding.
        /// </summary>
        /// <param name="feedback">The feedback object</param>
        public FeedbackTextWriter(IBuildFeedback feedback)
            : this(feedback, Encoding.UTF8)
        {
            // nothing else to do here.
        }

        /// <inheritdoc />
        public override Encoding Encoding
        {
            get
            {
                return this.encoding;
            }
        }

        /// <summary>
        /// Gets or sets the feedback target for data.
        /// </summary>
        public IBuildFeedback FeedbackTarget { get; set; }

        /// <inheritdoc />
        public override void Write(string value)
        {
            this.FeedbackTarget.Write(value);
        }

        /// <inheritdoc />
        public override void WriteLine()
        {
            this.FeedbackTarget.WriteLine();
        }

        /// <inheritdoc />
        public override void WriteLine(string format, params object[] arg)
        {
            this.FeedbackTarget.WriteLine(format, arg);
        }

        /// <inheritdoc />
        public override void Write(char value)
        {
            this.FeedbackTarget.Write(value.ToString());
        }
    }
}
