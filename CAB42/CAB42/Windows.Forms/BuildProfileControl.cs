//-----------------------------------------------------------------------
// <copyright file="BuildProfileControl.cs" company="42A Consulting">
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
    /// A control to edit profiles for a project.
    /// </summary>
    public partial class BuildProfileControl : UserControl
    {
        /// <summary>
        /// The project.
        /// </summary>
        private ProjectInfo projectInfo;

        /// <summary>
        /// The currently selected profile.
        /// </summary>
        private BuildProfile selectedProfile;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProfileControl"/> class.
        /// </summary>
        public BuildProfileControl()
        {
            this.InitializeComponent();

            this.Project = null;
        }

        /// <summary>
        /// Gets or sets the project object.
        /// </summary>
        public ProjectInfo Project
        {
            get
            {
                return this.projectInfo;
            }

            set
            {
                this.projectInfo = value;

                this.cbProfile.Items.Clear();
                this.cbInheritsFrom.Items.Clear();

                if (value != null)
                {
                    this.Enabled = true;

                    var items = value.Profiles.Values.ToArray();
                    this.cbProfile.Items.AddRange(items);
                    this.cbInheritsFrom.Items.AddRange(items);
                    this.cbInheritsFrom.Items.Add("<None>");

                    this.cbProfile.SelectedItem = value.CurrentProfile;
                }
                else
                {
                    this.Enabled = false;
                    this.SelectedProfile = null;
                }
            }
        }

        /// <summary>
        /// Gets or sets selected profile.
        /// </summary>
        public BuildProfile SelectedProfile
        {
            get
            {
                return this.selectedProfile;
            }

            set
            {
                this.selectedProfile = value;
                this.panel1.Enabled = value != null;
                this.cbProfile.SelectedItem = value;

                if (this.selectedProfile != null)
                {
                    this.excludeListControl1.Items = value.Excludes;
                    this.includeRuleListControl1.Items = value.Includes;
                    this.userVariableListControl1.BuildProfile = value;
                    this.userVariableListControl1.Items = value.Variables;
                    this.cbInheritsFrom.SelectedItem = value.Parent;
                    this.cbxProfileIsPlaceholder.Checked = value.IsPlaceholder;
                }
                else
                {
                    this.excludeListControl1.Items = null;
                    this.userVariableListControl1.BuildProfile = null;
                    this.userVariableListControl1.Items = null;
                    this.cbInheritsFrom.SelectedItem = null;
                    this.cbxProfileIsPlaceholder.Checked = false;
                }
            }
        }

        /// <summary>
        /// Occurs when the user has changed the selected profile.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> with event data.</param>
        private void ProfileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedProfile = this.cbProfile.SelectedItem as BuildProfile;
        }

        /// <summary>
        /// Occurs when the user has changed the selected parent profile.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> with event data.</param>
        private void InheritsFromComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var profile = this.cbInheritsFrom.SelectedItem as BuildProfile;

            if (profile != null && this.SelectedProfile != null)
            {
                if (profile.IsSubProfileOf(this.SelectedProfile))
                {
                    MessageBox.Show(this, "The selected profile inherits the current profile");

                    // Reset drop-down list
                    this.cbInheritsFrom.SelectedItem = null;
                }
                else
                {
                    this.SelectedProfile.Parent = profile;
                }
            }
            else if (this.SelectedProfile != null)
            {
                this.SelectedProfile.Parent = null;
            }
        }

        /// <summary>
        /// Occurs when the button for creating a new profile has been clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> with event data.</param>
        private void NewProfileButton_Click(object sender, EventArgs e)
        {
            this.CreateNewProfile(null);
        }

        /// <summary>
        /// Occurs when the button for copying the currently selected profile has been clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> with event data.</param>
        private void CopyProfileButton_Click(object sender, EventArgs e)
        {
            this.CreateNewProfile(this.SelectedProfile);
        }

        /// <summary>
        /// Creates a new profile, with the specified parent.
        /// </summary>
        /// <param name="parent">The parent for the new profile to be created. If null, the created profile won't have a parent.</param>
        private void CreateNewProfile(BuildProfile parent)
        {
            try
            {
                using (var f = new AddBuildProfileForm())
                {
                    if (f.ShowDialog(this) == DialogResult.OK)
                    {
                        f.Value.Parent = parent;

                        this.Project.Profiles.Add(f.Value);

                        this.cbProfile.Items.Add(f.Value);
                        this.cbInheritsFrom.Items.Add(f.Value);

                        this.cbProfile.SelectedItem = f.Value;
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(this, x.Message);
            }
        }

        private void cbxProfileIsPlaceholder_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SelectedProfile != null)
            {
                this.SelectedProfile.IsPlaceholder = this.cbxProfileIsPlaceholder.Checked;
            }
        }
    }
}
