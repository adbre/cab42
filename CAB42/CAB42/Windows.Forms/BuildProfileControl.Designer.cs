namespace C42A.CAB42.Windows.Forms
{
    partial class BuildProfileControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCopyProfile = new System.Windows.Forms.Button();
            this.btnNewProfile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbInheritsFrom = new System.Windows.Forms.ComboBox();
            this.cbProfile = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpUserVariables = new System.Windows.Forms.TabPage();
            this.userVariableListControl1 = new C42A.CAB42.Windows.Forms.UserVariableListControl();
            this.tpExcludeRules = new System.Windows.Forms.TabPage();
            this.excludeListControl1 = new C42A.CAB42.Windows.Forms.ExcludeListControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.includeRuleListControl1 = new C42A.CAB42.Windows.Forms.IncludeRuleListControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxProfileIsPlaceholder = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpUserVariables.SuspendLayout();
            this.tpExcludeRules.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Profile Name";
            // 
            // btnCopyProfile
            // 
            this.btnCopyProfile.Location = new System.Drawing.Point(425, 14);
            this.btnCopyProfile.Name = "btnCopyProfile";
            this.btnCopyProfile.Size = new System.Drawing.Size(128, 23);
            this.btnCopyProfile.TabIndex = 4;
            this.btnCopyProfile.Text = "Inherit from this profile";
            this.btnCopyProfile.UseVisualStyleBackColor = true;
            this.btnCopyProfile.Click += new System.EventHandler(this.CopyProfileButton_Click);
            // 
            // btnNewProfile
            // 
            this.btnNewProfile.Location = new System.Drawing.Point(432, 27);
            this.btnNewProfile.Name = "btnNewProfile";
            this.btnNewProfile.Size = new System.Drawing.Size(128, 23);
            this.btnNewProfile.TabIndex = 5;
            this.btnNewProfile.Text = "New Profile";
            this.btnNewProfile.UseVisualStyleBackColor = true;
            this.btnNewProfile.Click += new System.EventHandler(this.NewProfileButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Inherits From";
            // 
            // cbInheritsFrom
            // 
            this.cbInheritsFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInheritsFrom.FormattingEnabled = true;
            this.cbInheritsFrom.Location = new System.Drawing.Point(6, 16);
            this.cbInheritsFrom.Name = "cbInheritsFrom";
            this.cbInheritsFrom.Size = new System.Drawing.Size(365, 21);
            this.cbInheritsFrom.TabIndex = 7;
            this.cbInheritsFrom.SelectedIndexChanged += new System.EventHandler(this.InheritsFromComboBox_SelectedIndexChanged);
            // 
            // cbProfile
            // 
            this.cbProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProfile.FormattingEnabled = true;
            this.cbProfile.Location = new System.Drawing.Point(6, 27);
            this.cbProfile.Name = "cbProfile";
            this.cbProfile.Size = new System.Drawing.Size(365, 21);
            this.cbProfile.TabIndex = 8;
            this.cbProfile.SelectedIndexChanged += new System.EventHandler(this.ProfileComboBox_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(650, 420);
            this.panel1.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpUserVariables);
            this.tabControl1.Controls.Add(this.tpExcludeRules);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(650, 420);
            this.tabControl1.TabIndex = 6;
            // 
            // tpUserVariables
            // 
            this.tpUserVariables.Controls.Add(this.userVariableListControl1);
            this.tpUserVariables.Location = new System.Drawing.Point(4, 22);
            this.tpUserVariables.Name = "tpUserVariables";
            this.tpUserVariables.Padding = new System.Windows.Forms.Padding(3);
            this.tpUserVariables.Size = new System.Drawing.Size(642, 394);
            this.tpUserVariables.TabIndex = 0;
            this.tpUserVariables.Text = "User Variables";
            this.tpUserVariables.UseVisualStyleBackColor = true;
            // 
            // userVariableListControl1
            // 
            this.userVariableListControl1.BuildProfile = null;
            this.userVariableListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userVariableListControl1.Location = new System.Drawing.Point(3, 3);
            this.userVariableListControl1.Name = "userVariableListControl1";
            this.userVariableListControl1.Size = new System.Drawing.Size(636, 388);
            this.userVariableListControl1.TabIndex = 0;
            // 
            // tpExcludeRules
            // 
            this.tpExcludeRules.Controls.Add(this.excludeListControl1);
            this.tpExcludeRules.Location = new System.Drawing.Point(4, 22);
            this.tpExcludeRules.Name = "tpExcludeRules";
            this.tpExcludeRules.Padding = new System.Windows.Forms.Padding(3);
            this.tpExcludeRules.Size = new System.Drawing.Size(642, 394);
            this.tpExcludeRules.TabIndex = 1;
            this.tpExcludeRules.Text = "Exclude List";
            this.tpExcludeRules.UseVisualStyleBackColor = true;
            // 
            // excludeListControl1
            // 
            this.excludeListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.excludeListControl1.Location = new System.Drawing.Point(3, 3);
            this.excludeListControl1.Name = "excludeListControl1";
            this.excludeListControl1.Size = new System.Drawing.Size(636, 340);
            this.excludeListControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.includeRuleListControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(642, 394);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Include List";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // includeRuleListControl1
            // 
            this.includeRuleListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.includeRuleListControl1.Location = new System.Drawing.Point(3, 3);
            this.includeRuleListControl1.Name = "includeRuleListControl1";
            this.includeRuleListControl1.Size = new System.Drawing.Size(636, 340);
            this.includeRuleListControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.cbxProfileIsPlaceholder);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(642, 394);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Profile Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Options";
            // 
            // cbxProfileIsPlaceholder
            // 
            this.cbxProfileIsPlaceholder.AutoSize = true;
            this.cbxProfileIsPlaceholder.Location = new System.Drawing.Point(9, 93);
            this.cbxProfileIsPlaceholder.Name = "cbxProfileIsPlaceholder";
            this.cbxProfileIsPlaceholder.Size = new System.Drawing.Size(264, 17);
            this.cbxProfileIsPlaceholder.TabIndex = 3;
            this.cbxProfileIsPlaceholder.Text = "This profile is only a placeholder (do not build CAB)";
            this.cbxProfileIsPlaceholder.UseVisualStyleBackColor = true;
            this.cbxProfileIsPlaceholder.CheckedChanged += new System.EventHandler(this.cbxProfileIsPlaceholder_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cbInheritsFrom);
            this.panel2.Controls.Add(this.btnCopyProfile);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(636, 48);
            this.panel2.TabIndex = 2;
            // 
            // BuildProfileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbProfile);
            this.Controls.Add(this.btnNewProfile);
            this.Controls.Add(this.label1);
            this.Name = "BuildProfileControl";
            this.Size = new System.Drawing.Size(653, 477);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpUserVariables.ResumeLayout(false);
            this.tpExcludeRules.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCopyProfile;
        private System.Windows.Forms.Button btnNewProfile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbInheritsFrom;
        private System.Windows.Forms.ComboBox cbProfile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private ExcludeListControl excludeListControl1;
        private UserVariableListControl userVariableListControl1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpUserVariables;
        private System.Windows.Forms.TabPage tpExcludeRules;
        private System.Windows.Forms.TabPage tabPage1;
        private IncludeRuleListControl includeRuleListControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbxProfileIsPlaceholder;

    }
}
