//-----------------------------------------------------------------------
// <copyright file="AddBuildProfileForm.cs" company="42A Consulting">
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
    /// A form used to create new build profiles.
    /// </summary>
    public partial class AddBuildProfileForm : Form
    {
        /// <summary>
        /// The build profile to edit or the profile created.
        /// </summary>
        private BuildProfile profile;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddBuildProfileForm"/> class.
        /// </summary>
        public AddBuildProfileForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the build profile to edit. If null, a build profile will be created.
        /// </summary>
        public BuildProfile Value
        {
            get
            {
                return this.profile;
            }

            set
            {
                this.profile = value;

                if (value != null)
                {
                    this.tbFileName.Text = value.Name;
                }
                else
                {
                    this.tbFileName.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Occurs when the user has clicked the OK button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contain the event data.</param>
        private void OnOKClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbFileName.Text))
            {
                MessageBox.Show(this, "The name must not be empty", this.Text);
                return;
            }

            if (this.profile == null)
            {
                this.profile = new BuildProfile();
            }

            this.profile.Name = this.tbFileName.Text;

            this.Close(DialogResult.OK);
        }

        /// <summary>
        /// Occurs when the user has clicked the Cancel button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contain the event data.</param>
        private void OnCancelClick(object sender, EventArgs e)
        {
            this.Close(DialogResult.Cancel);
        }

        /// <summary>
        /// Closes the form after the dialogresult has been set.
        /// </summary>
        /// <param name="dialogResult">The dialog result to set before closing the form.</param>
        private void Close(System.Windows.Forms.DialogResult dialogResult)
        {
            this.DialogResult = dialogResult;

            this.Close();
        }
    }
}
