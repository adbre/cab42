namespace C42A.CAB42.Windows.Forms
{
    partial class BuildProjectControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpInformation = new System.Windows.Forms.TabPage();
            this.buildProjectSettingsControl1 = new C42A.CAB42.Windows.Forms.BuildProjectSettingsControl();
            this.tpOutput = new System.Windows.Forms.TabPage();
            this.buildProjectOutputControl1 = new C42A.CAB42.Windows.Forms.BuildProjectOutputControl();
            this.tpIncludes = new System.Windows.Forms.TabPage();
            this.includeListControl1 = new C42A.CAB42.Windows.Forms.IncludeRuleListControl();
            this.tpProfiles = new System.Windows.Forms.TabPage();
            this.buildProfileControl1 = new C42A.CAB42.Windows.Forms.BuildProfileControl();
            this.tabControl1.SuspendLayout();
            this.tpInformation.SuspendLayout();
            this.tpOutput.SuspendLayout();
            this.tpIncludes.SuspendLayout();
            this.tpProfiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpInformation);
            this.tabControl1.Controls.Add(this.tpOutput);
            this.tabControl1.Controls.Add(this.tpIncludes);
            this.tabControl1.Controls.Add(this.tpProfiles);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(796, 515);
            this.tabControl1.TabIndex = 1;
            // 
            // tpInformation
            // 
            this.tpInformation.Controls.Add(this.buildProjectSettingsControl1);
            this.tpInformation.Location = new System.Drawing.Point(4, 22);
            this.tpInformation.Name = "tpInformation";
            this.tpInformation.Padding = new System.Windows.Forms.Padding(3);
            this.tpInformation.Size = new System.Drawing.Size(788, 489);
            this.tpInformation.TabIndex = 3;
            this.tpInformation.Text = "Information";
            this.tpInformation.UseVisualStyleBackColor = true;
            // 
            // buildProjectSettingsControl1
            // 
            this.buildProjectSettingsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buildProjectSettingsControl1.Location = new System.Drawing.Point(3, 3);
            this.buildProjectSettingsControl1.Name = "buildProjectSettingsControl1";
            this.buildProjectSettingsControl1.ProjectInfo = null;
            this.buildProjectSettingsControl1.Size = new System.Drawing.Size(782, 483);
            this.buildProjectSettingsControl1.TabIndex = 0;
            // 
            // tpOutput
            // 
            this.tpOutput.Controls.Add(this.buildProjectOutputControl1);
            this.tpOutput.Location = new System.Drawing.Point(4, 22);
            this.tpOutput.Name = "tpOutput";
            this.tpOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tpOutput.Size = new System.Drawing.Size(788, 489);
            this.tpOutput.TabIndex = 2;
            this.tpOutput.Text = "Output";
            this.tpOutput.UseVisualStyleBackColor = true;
            // 
            // buildProjectOutputControl1
            // 
            this.buildProjectOutputControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buildProjectOutputControl1.Location = new System.Drawing.Point(3, 3);
            this.buildProjectOutputControl1.Name = "buildProjectOutputControl1";
            this.buildProjectOutputControl1.ProjectOutput = null;
            this.buildProjectOutputControl1.Size = new System.Drawing.Size(580, 266);
            this.buildProjectOutputControl1.TabIndex = 0;
            // 
            // tpIncludes
            // 
            this.tpIncludes.Controls.Add(this.includeListControl1);
            this.tpIncludes.Location = new System.Drawing.Point(4, 22);
            this.tpIncludes.Name = "tpIncludes";
            this.tpIncludes.Padding = new System.Windows.Forms.Padding(3);
            this.tpIncludes.Size = new System.Drawing.Size(788, 489);
            this.tpIncludes.TabIndex = 0;
            this.tpIncludes.Text = "Includes";
            this.tpIncludes.UseVisualStyleBackColor = true;
            // 
            // includeListControl1
            // 
            this.includeListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.includeListControl1.Location = new System.Drawing.Point(3, 3);
            this.includeListControl1.Name = "includeListControl1";
            this.includeListControl1.Size = new System.Drawing.Size(580, 266);
            this.includeListControl1.TabIndex = 0;
            // 
            // tpProfiles
            // 
            this.tpProfiles.Controls.Add(this.buildProfileControl1);
            this.tpProfiles.Location = new System.Drawing.Point(4, 22);
            this.tpProfiles.Name = "tpProfiles";
            this.tpProfiles.Padding = new System.Windows.Forms.Padding(3);
            this.tpProfiles.Size = new System.Drawing.Size(788, 489);
            this.tpProfiles.TabIndex = 1;
            this.tpProfiles.Text = "Profiles";
            this.tpProfiles.UseVisualStyleBackColor = true;
            // 
            // buildProfileControl1
            // 
            this.buildProfileControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buildProfileControl1.Enabled = false;
            this.buildProfileControl1.Location = new System.Drawing.Point(3, 3);
            this.buildProfileControl1.Name = "buildProfileControl1";
            this.buildProfileControl1.Project = null;
            this.buildProfileControl1.SelectedProfile = null;
            this.buildProfileControl1.Size = new System.Drawing.Size(580, 266);
            this.buildProfileControl1.TabIndex = 0;
            // 
            // BuildProjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "BuildProjectControl";
            this.Size = new System.Drawing.Size(796, 515);
            this.tabControl1.ResumeLayout(false);
            this.tpInformation.ResumeLayout(false);
            this.tpOutput.ResumeLayout(false);
            this.tpIncludes.ResumeLayout(false);
            this.tpProfiles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpIncludes;
        private IncludeRuleListControl includeListControl1;
        private System.Windows.Forms.TabPage tpProfiles;
        private System.Windows.Forms.TabPage tpOutput;
        private BuildProjectOutputControl buildProjectOutputControl1;
        private System.Windows.Forms.TabPage tpInformation;
        private BuildProjectSettingsControl buildProjectSettingsControl1;
        private BuildProfileControl buildProfileControl1;
    }
}
