//-----------------------------------------------------------------------
// <copyright file="BuildForm.cs" company="42A Consulting">
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

namespace C42A.CAB42.Windows.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    public partial class BuildForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the BuildForm class.
        /// </summary>
        public BuildForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the build context.
        /// </summary>
        public BuildTaskContext Context { get; set; }

        /// <summary>
        /// Gets or sets the build tasks to be executed.
        /// </summary>
        public IBuildTask[] BuildTasks { get; set; }

        /// <summary>
        /// Raises the Shown event and then starting the the background worker.
        /// </summary>
        /// <param name="e">A empty EventArgs object.</param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            this.backgroundWorker1.RunWorkerAsync();
        }

        /// <summary>
        /// Do background work. Creates a build context and executes the build tasks.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A DoWorkEventArgs object.</param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var textWriter = new TextboxWriter(this.textBox1))
            {
                var feedback = new BuildFeedbackBase(textWriter);

                var result = this.Context.Build(this.BuildTasks, feedback);

                e.Result = new WorkResult()
                {
                    Result = result,
                    Messages = feedback.Messages
                };
            }
        }

        /// <summary>
        /// Invoked when the background worker has completed it's execution.
        /// This method is executed on the UI thread.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A RunWorkerCompletedEventArgs object containing result data from the background worker.</param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.progressBar1.Style = ProgressBarStyle.Blocks;
            this.progressBar1.Value = this.progressBar1.Maximum;

            this.lblStatus.Text = string.Empty;

            var workResult = e.Result as WorkResult;
            if (workResult != null)
            {
                this.Bind(workResult.Messages);

                this.lblStatus.Text = string.Format(
                    "{1} succeeded, {2} failed, {3} skipped",
                    workResult.Result.Success ? "succeeded" : "failed",
                    workResult.Result.TasksSuccesful,
                    workResult.Result.TasksAborted,
                    workResult.Result.TasksSkipped,
                    workResult.Result.TotalSteps);

                this.tabControl1.SelectedTab = this.tabPage2;
            }
        }

        /// <summary>
        /// Invoked when the background worker has reported progress change.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A ProgressChangedEventArgs object containing data for the current progress state.</param>
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // nothing else to do here.
        }

        /// <summary>
        /// Adds a list of build messages, removing any existing build messages from the listview.
        /// </summary>
        /// <param name="messages">The messages to append.</param>
        private void Bind(IEnumerable<BuildMessage> messages)
        {
            this.listView1.BeginUpdate();

            this.listView1.Items.Clear();

            foreach (var message in messages)
            {
                this.Bind(message);
            }

            this.listView1.EndUpdate();
        }

        /// <summary>
        /// Adds a build message to the listview.
        /// </summary>
        /// <param name="info">The build message to add</param>
        private void Bind(BuildMessage info)
        {
            var lvi = new ListViewItem();

            this.Bind(lvi, info);

            this.listView1.Items.Add(lvi);
        }

        /// <summary>
        /// Binds a build message to the specified listview item.
        /// </summary>
        /// <param name="lvi">The ListViewItem</param>
        /// <param name="info">The BuildMessage</param>
        private void Bind(ListViewItem lvi, BuildMessage info)
        {
            lvi.Tag = info;

            int index = 0;
            this.BindValue(lvi, index++, info.Description);
            this.BindValue(lvi, index++, info.Configuration);
        }

        /// <summary>
        /// Binds a sub item at the specified index to the specified value.
        /// </summary>
        /// <param name="lvi">The ListViewItem which the subitems is to be added to.</param>
        /// <param name="index">The index which the value should be inserted at.</param>
        /// <param name="value">The value to be added.</param>
        private void BindValue(ListViewItem lvi, int index, string value)
        {
            if (lvi.SubItems.Count > index)
            {
                lvi.SubItems[index].Text = value;
            }
            else
            {
                lvi.SubItems.Add(value);
            }
        }

        /// <summary>
        /// The work result for the background worker.
        /// </summary>
        private class WorkResult
        {
            /// <summary>
            /// Gets or sets the build result.
            /// </summary>
            public IBuildResult Result { get; set; }

            /// <summary>
            /// Gets or sets the collection of build messages to be added to the listview upon completion.
            /// </summary>
            public IEnumerable<BuildMessage> Messages { get; set; }
        }

        /// <summary>
        /// Provides a TextWriter interface around a System.Windows.Forms.TextBox.
        /// </summary>
        private class TextboxWriter : System.IO.TextWriter
        {
            /// <summary>
            /// The encoding to use when converting from bytes to strings.
            /// </summary>
            private Encoding encoding;

            /// <summary>
            /// The synchronization root for thread-safe execution.
            /// </summary>
            private object syncroot = new object();

            /// <summary>
            /// Initializes a new instance of the TextboxWriter class with the specified textbox and UTF-8 encoding.
            /// </summary>
            /// <param name="textBox">The TextBox to wrap</param>
            public TextboxWriter(TextBox textBox)
                : this(textBox, Encoding.UTF8)
            {
                // nothing
            }

            /// <summary>
            /// Initializes a new instance of the TextboxWriter class with the specified textbox and encoding.
            /// </summary>
            /// <param name="textBox"></param>
            /// <param name="encoding"></param>
            public TextboxWriter(TextBox textBox, Encoding encoding)
            {
                this.TextBox = textBox;
                this.encoding = encoding;
            }

            /// <summary>
            /// A write callback for thread-safe execution.
            /// </summary>
            /// <param name="value">The string to be written</param>
            private delegate void WriteDelegate(string value);

            /// <summary>
            /// Gets or sets the textbox data will be written to.
            /// </summary>
            public TextBox TextBox { get; set; }

            /// <inheritdoc />
            public override Encoding Encoding
            {
                get { return this.encoding; }
            }

            /// <inheritdoc />
            public override void Write(string value)
            {
                lock (this.syncroot)
                {
                    if (this.TextBox != null && !this.TextBox.IsDisposed)
                    {
                        if (this.TextBox.InvokeRequired)
                        {
                            this.TextBox.BeginInvoke(new WriteDelegate(this.OnSafeWrite), value);
                        }
                        else
                        {
                            this.OnSafeWrite(value);
                        }
                    }
                }
            }

            /// <inheritdoc />
            public override void Write(char value)
            {
                lock (this.syncroot)
                {
                    this.Write(value.ToString());
                }
            }

            /// <inheritdoc />
            public override void WriteLine()
            {
                lock (this.syncroot)
                {
                    base.WriteLine();
                }
            }

            /// <inheritdoc />
            public override void WriteLine(string value)
            {
                lock (this.syncroot)
                {
                    this.Write(string.Concat(value, Environment.NewLine));
                }
            }

            /// <inheritdoc />
            public override void Write(char[] buffer)
            {
                lock (this.syncroot)
                {
                    base.Write(buffer);
                }
            }

            /// <summary>
            /// This method must always be called in a thread-safe context, otherwise exceptions will be thrown.
            /// </summary>
            /// <param name="value"></param>
            protected virtual void OnSafeWrite(string value)
            {
                this.TextBox.AppendText(value);
            }
        }
    }
}
