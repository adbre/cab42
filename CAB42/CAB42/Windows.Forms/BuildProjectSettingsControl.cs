//-----------------------------------------------------------------------
// <copyright file="BuildProjectSettingsControl.cs" company="42A Consulting">
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
    /// A Window Forms Control which describes and manages a <see cref="ProjectInfo"/> object.
    /// </summary>
    public partial class BuildProjectSettingsControl : UserControl
    {
        /// <summary>
        /// The project info which is currently the selected value of this control.
        /// </summary>
        private ProjectInfo buildProject;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProjectSettingsControl"/> class.
        /// </summary>
        public BuildProjectSettingsControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the <see cref="ProjectInfo"/> which is to be displayed by this control.
        /// </summary>
        public ProjectInfo ProjectInfo
        {
            get
            {
                return this.buildProject;
            }

            set
            {
                this.buildProject = value;

                if (value != null)
                {
                    this.tbxOutputFileName.Text = value.OutputFileName;
                    this.tbxOutputPath.Text = value.OutputPath;
                    this.tbxReleaseName.Text = value.ReleaseName;
                    this.tbxApplicationName.Text = value.ApplicationName;
                    this.tbxCompanyName.Text = value.CompanyName;
                    this.tbxBuildPath.Text = value.BuildPath;

                    this.userVariableListControl1.Items = value.GlobalUserVariables;
                    this.registryKeyListControl1.Items = value.GlobalRegistryKeys;

                    if (value.ProjectVersion != null)
                    {
                        this.tbxProjectVersion.Text = value.ProjectVersion.ToString();
                    }
                    else
                    {
                        this.tbxProjectVersion.Text = string.Empty;
                    }
                }
                else
                {
                    this.tbxApplicationName.Text = string.Empty;
                    this.tbxCompanyName.Text = string.Empty;
                    this.tbxOutputFileName.Text = string.Empty;
                    this.tbxOutputFileName.Text = string.Empty;
                    this.tbxOutputPath.Text = string.Empty;
                    this.tbxProjectVersion.Text = string.Empty;
                    this.tbxReleaseName.Text = string.Empty;
                    this.tbxBuildPath.Text = string.Empty;

                    this.userVariableListControl1.Items = null;
                    this.registryKeyListControl1.Items = null;
                }
            }
        }

        /// <summary>
        /// Sets the Cancel property of the specified <see cref="CancelEventArgs"/> to true, after the user has been informed that the validation failed.
        /// </summary>
        /// <param name="sender">The source of the Validation event.</param>
        /// <param name="e">A <see cref="CancelEventArgs"/> to signal that the validation failed.</param>
        /// <param name="message">A message to be displayed to the user, informing that the validation failed.</param>
        private void Cancel(object sender, CancelEventArgs e, string message)
        {
            this.Cancel(sender as TextBox, e, message);
        }

        /// <summary>
        /// Sets the Cancel property of the specified <see cref="CancelEventArgs"/> to true, after the user has been informed that the validation failed.
        /// </summary>
        /// <param name="TextBox">The source of the Validation event.</param>
        /// <param name="e">A <see cref="CancelEventArgs"/> to signal that the validation failed.</param>
        /// <param name="message">A message to be displayed to the user, informing that the validation failed.</param>
        private void Cancel(TextBox textBox, CancelEventArgs e, string message)
        {
            if (textBox != null)
            {
                this.errorProvider1.SetError(textBox, message);
                textBox.SelectAll();
            }
            else
            {
                MessageBox.Show(this, message, "Input validation");
            }

            e.Cancel = true;
        }

        /// <summary>
        /// Clears any error being displayed by the error provider for the specified textbox.
        /// </summary>
        /// <param name="textBox">The textbox which should be cleared from errors flags.</param>
        private void ClearError(TextBox textBox)
        {
            this.errorProvider1.SetError(textBox, string.Empty);
        }

        /// <summary>
        /// Validates the contents of the OutputFileName textbox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="CancelEventArgs"/> to signal whether validation was successful or not.</param>
        private void tbxOutputFileName_Validating(object sender, CancelEventArgs e)
        {
            var invalidChars = System.IO.Path.GetInvalidFileNameChars();

            if (string.IsNullOrEmpty(this.tbxOutputFileName.Text))
            {
                this.Cancel(this.tbxOutputFileName, e, "The output filename cannot be empty.");
            }
            else if (this.tbxOutputFileName.Text.IndexOfAny(invalidChars) > -1)
            {
                this.Cancel(this.tbxOutputFileName, e, "The output filename contains characters not allowed in a filename.");
            }
            else
            {
                this.ClearError(this.tbxOutputFileName);
            }
        }

        /// <summary>
        /// Saves validated data from the OutputFileName textbox.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A empty <see cref="EventArgs"/> object</param>
        private void tbxOutputFileName_Validated(object sender, EventArgs e)
        {
            if (this.ProjectInfo != null)
            {
                this.ProjectInfo.OutputFileName = this.tbxOutputFileName.Text;
            }
        }

        /// <summary>
        /// Validates the contents of the ProjectVersion textbox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="CancelEventArgs"/> to signal whether validation was successful or not.</param>
        private void tbxProjectVersion_Validating(object sender, CancelEventArgs e)
        {
            Version version;
            if (!string.IsNullOrEmpty(this.tbxProjectVersion.Text) &&
                !Version.TryParse(this.tbxProjectVersion.Text, out version))
            {
                this.Cancel(this.tbxProjectVersion, e, "The version string is not well-formatted.");
            }
            else
            {
                this.ClearError(this.tbxProjectVersion);
            }
        }

        /// <summary>
        /// Saves validated data from the ProjectVersion textbox.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A empty <see cref="EventArgs"/> object</param>
        private void tbxProjectVersion_Validated(object sender, EventArgs e)
        {
            Version version = null;
            if (!string.IsNullOrEmpty(this.tbxProjectVersion.Text))
            {
                // This call should never result in version being null, as the validation has
                // already passed Validating successfully.
                Version.TryParse(this.tbxProjectVersion.Text, out version);
            }

            if (this.ProjectInfo != null)
            {
                this.ProjectInfo.ProjectVersion = version;
            }
        }


        /// <summary>
        /// Validates the contents of the ReleaseName textbox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="CancelEventArgs"/> to signal whether validation was successful or not.</param>
        private void tbxReleaseName_Validating(object sender, CancelEventArgs e)
        {
            var invalidChars = System.IO.Path.GetInvalidFileNameChars();

            if (!string.IsNullOrEmpty(this.tbxReleaseName.Text) &&
                this.tbxReleaseName.Text.IndexOfAny(invalidChars) > -1)
            {
                this.Cancel(this.tbxReleaseName, e, "The release name contains characters not allowed in a filename.");
            }
            else
            {
                this.ClearError(this.tbxReleaseName);
            }
        }

        /// <summary>
        /// Saves validated data from the ReleaseName textbox.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A empty <see cref="EventArgs"/> object</param>
        private void tbxReleaseName_Validated(object sender, EventArgs e)
        {
            if (this.ProjectInfo != null)
            {
                if (string.IsNullOrEmpty(this.tbxReleaseName.Text))
                {
                    this.ProjectInfo.ReleaseName = null;
                }
                else
                {
                    this.ProjectInfo.ReleaseName = this.tbxReleaseName.Text;
                }
            }
        }

        /// <summary>
        /// Validates the contents of the OutputPath textbox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="CancelEventArgs"/> to signal whether validation was successful or not.</param>
        private void tbxOutputPath_Validating(object sender, CancelEventArgs e)
        {
            var invalidChars = System.IO.Path.GetInvalidPathChars();

            if (!string.IsNullOrEmpty(this.tbxOutputPath.Text) &&
                this.tbxOutputPath.Text.IndexOfAny(invalidChars) > -1)
            {
                this.Cancel(this.tbxOutputPath, e, "The output path contains illegal characters.");
            }
            else
            {
                this.ClearError(this.tbxOutputPath);
            }
        }

        /// <summary>
        /// Saves validated data from the OutputPath textbox.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A empty <see cref="EventArgs"/> object</param>
        private void tbxOutputPath_Validated(object sender, EventArgs e)
        {
            if (this.ProjectInfo != null)
            {
                this.ProjectInfo.OutputPath = this.tbxOutputPath.Text;
            }
        }

        /// <summary>
        /// Validates the contents of the ApplicationName textbox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="CancelEventArgs"/> to signal whether validation was successful or not.</param>
        private void tbxApplicationName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbxApplicationName.Text))
            {
                this.Cancel(this.tbxApplicationName, e, "The application name cannot be empty.");
            }
            else
            {
                this.ClearError(this.tbxApplicationName);
            }
        }

        /// <summary>
        /// Saves validated data from the ApplicationName textbox.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A empty <see cref="EventArgs"/> object</param>
        private void tbxApplicationName_Validated(object sender, EventArgs e)
        {
            if (this.ProjectInfo != null)
            {
                this.ProjectInfo.ApplicationName = this.tbxApplicationName.Text;
            }
        }

        /// <summary>
        /// Validates the contents of the CompanyName textbox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="CancelEventArgs"/> to signal whether validation was successful or not.</param>
        private void tbxCompanyName_Validating(object sender, CancelEventArgs e)
        {
            // The Company name (or the "Provider") string is limited to 32 characters by Cabwiz.
            // When reading the project from the XML file, the length is irrellevant.
            // Cabwiz will allow strings longer than 32 characters, but it will be truncated on the device.
            // Only check it here
            // See http://msdn.microsoft.com/en-us/library/ms933757.aspx
            if (this.tbxCompanyName.Text.Length > 32)
            {
                var message = new StringBuilder();
                message.AppendLine("The company name string is restricted to 32 characters in Windows CE.");
                message.AppendLine("If you choose to continue, the value will be truncated on the device when installing.");
                message.AppendLine("Do you want to ignore this warning and continue?");

                var dialogResult = MessageBox.Show(
                    this,
                    message.ToString(),
                    "Input validation",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button3);

                if (dialogResult == DialogResult.Yes)
                {
                    // nothing, the input has been validated.
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.Cancel(this.tbxCompanyName, e, null);
                    return;
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    if (this.ProjectInfo != null)
                    {
                        this.tbxCompanyName.Text = this.ProjectInfo.CompanyName;
                    }
                    else
                    {
                        this.tbxCompanyName.Text = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Saves validated data from the CompanyName textbox.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A empty <see cref="EventArgs"/> object</param>
        private void tbxCompanyName_Validated(object sender, EventArgs e)
        {
            if (this.ProjectInfo != null)
            {
                this.ProjectInfo.CompanyName = this.tbxCompanyName.Text;
            }
        }

        /// <summary>
        /// Validates the contents of the BuildPath textbox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="CancelEventArgs"/> to signal whether validation was successful or not.</param>
        private void tbxBuildPath_Validating(object sender, CancelEventArgs e)
        {
            var invalidChars = System.IO.Path.GetInvalidPathChars();

            if (!string.IsNullOrEmpty(this.tbxBuildPath.Text) &&
                this.tbxBuildPath.Text.IndexOfAny(invalidChars) > -1)
            {
                this.Cancel(this.tbxBuildPath, e, "The build path contains illegal characters.");
            }
            else
            {
                this.ClearError(this.tbxBuildPath);
            }
        }

        /// <summary>
        /// Saves validated data from the BuildPath textbox.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A empty <see cref="EventArgs"/> object</param>
        private void tbxBuildPath_Validated(object sender, EventArgs e)
        {
            if (this.ProjectInfo != null)
            {
                this.ProjectInfo.BuildPath = this.tbxBuildPath.Text;
            }
        }
    }
}
