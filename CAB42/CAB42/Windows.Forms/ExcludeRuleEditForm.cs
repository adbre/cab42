//-----------------------------------------------------------------------
// <copyright file="ExcludeRuleEditForm.cs" company="42A Consulting">
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

    /// <summary>
    /// A dialoge to edit exclude rules.
    /// </summary>
    public partial class ExcludeRuleEditForm : Form
    {
        private ExcludeRule includeRule;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcludeRuleEditForm"/> class.
        /// </summary>
        public ExcludeRuleEditForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or set the exclude rule value to be edited.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If setting this property to null, a new <see cref="ExcludeRule"/> 
        ///         instance will be created with the values entered by the user.
        ///     </para>
        /// </remarks>
        public ExcludeRule Value
        {
            get
            {
                return this.includeRule;
            }

            set
            {
                this.includeRule = value;

                if (value != null)
                {
                    this.tbFileName.Text = value.FileName;
                }
                else
                {
                    this.tbFileName.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// When the user presses the 'OK' button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A empty <see cref="EventArgs"/>.</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbFileName.Text))
            {
                MessageBox.Show(this, "The path must not be empty", this.Text);
                return;
            }

            if (this.includeRule == null)
            {
                this.includeRule = new ExcludeRule();
            }

            this.includeRule.FileName = this.tbFileName.Text;

            this.Close(DialogResult.OK);
        }

        /// <summary>
        /// When the user presses the 'Cancel' button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A empty <see cref="EventArgs"/>.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(DialogResult.Cancel);
        }

        /// <summary>
        /// Closes the form with the specified dialog result.
        /// </summary>
        /// <param name="dialogResult">The dialog result value to set the <see cref="System.Windows.Forms.Form.DialogResult"/> property to.</param>
        private void Close(System.Windows.Forms.DialogResult dialogResult)
        {
            this.DialogResult = dialogResult;

            this.Close();
        }
    }
}
