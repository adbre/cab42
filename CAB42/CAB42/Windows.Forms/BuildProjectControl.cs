//-----------------------------------------------------------------------
// <copyright file="BuildProjectControl.cs" company="42A Consulting">
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
    /// A control for editing a project.
    /// </summary>
    public partial class BuildProjectControl : UserControl
    {
        /// <summary>
        /// The project.
        /// </summary>
        private ProjectInfo buildProject;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProjectControl"/> class.
        /// </summary>
        public BuildProjectControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the project to edit.
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

                this.tabControl1.SelectedTab = this.tpInformation;

                this.Refresh();
            }
        }

        /// <summary>
        /// Refreshes the control to reflect any changes made in the project.
        /// </summary>
        public new void Refresh()
        {
            if (this.buildProject != null)
            {
                this.buildProjectOutputControl1.ProjectOutput = this.buildProject.Output;
                this.includeListControl1.Items = this.buildProject.GlobalIncludeRules;
            }
            else
            {
                this.buildProjectOutputControl1.ProjectOutput = null;
                this.includeListControl1.Items = null;
            }

            this.buildProjectSettingsControl1.ProjectInfo = this.buildProject;
            this.buildProfileControl1.Project = this.buildProject;

            this.tabControl1.Enabled = this.buildProject != null;
        }
    }
}
