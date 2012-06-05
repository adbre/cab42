namespace C42A.CAB42.Windows.Forms
{
    partial class BuildProjectSettingsControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildProjectSettingsControl));
            this.label1 = new System.Windows.Forms.Label();
            this.tbxOutputFileName = new System.Windows.Forms.TextBox();
            this.tbxProjectVersion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxReleaseName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxOutputPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOutputPathBrowse = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxApplicationName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxCompanyName = new System.Windows.Forms.TextBox();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnBuildPathBrowse = new System.Windows.Forms.Button();
            this.tbxBuildPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpUserVariables = new System.Windows.Forms.TabPage();
            this.tpRegistryKeys = new System.Windows.Forms.TabPage();
            this.tpBuildSettings = new System.Windows.Forms.TabPage();
            this.userVariableListControl1 = new C42A.CAB42.Windows.Forms.UserVariableListControl();
            this.registryKeyListControl1 = new C42A.CAB42.Windows.Forms.RegistryKeyListControl();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tpUserVariables.SuspendLayout();
            this.tpRegistryKeys.SuspendLayout();
            this.tpBuildSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Output (CAB) filename:";
            // 
            // tbxOutputFileName
            // 
            this.tbxOutputFileName.Location = new System.Drawing.Point(6, 27);
            this.tbxOutputFileName.Name = "tbxOutputFileName";
            this.tbxOutputFileName.Size = new System.Drawing.Size(365, 20);
            this.tbxOutputFileName.TabIndex = 1;
            this.tbxOutputFileName.Validating += new System.ComponentModel.CancelEventHandler(this.tbxOutputFileName_Validating);
            this.tbxOutputFileName.Validated += new System.EventHandler(this.tbxOutputFileName_Validated);
            // 
            // tbxProjectVersion
            // 
            this.tbxProjectVersion.Location = new System.Drawing.Point(6, 117);
            this.tbxProjectVersion.Name = "tbxProjectVersion";
            this.tbxProjectVersion.Size = new System.Drawing.Size(365, 20);
            this.tbxProjectVersion.TabIndex = 3;
            this.tbxProjectVersion.Validating += new System.ComponentModel.CancelEventHandler(this.tbxProjectVersion_Validating);
            this.tbxProjectVersion.Validated += new System.EventHandler(this.tbxProjectVersion_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Project Version:";
            // 
            // tbxReleaseName
            // 
            this.tbxReleaseName.Location = new System.Drawing.Point(6, 156);
            this.tbxReleaseName.Name = "tbxReleaseName";
            this.tbxReleaseName.Size = new System.Drawing.Size(365, 20);
            this.tbxReleaseName.TabIndex = 5;
            this.tbxReleaseName.Validating += new System.ComponentModel.CancelEventHandler(this.tbxReleaseName_Validating);
            this.tbxReleaseName.Validated += new System.EventHandler(this.tbxReleaseName_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Release Name:";
            // 
            // tbxOutputPath
            // 
            this.tbxOutputPath.Location = new System.Drawing.Point(6, 19);
            this.tbxOutputPath.Name = "tbxOutputPath";
            this.tbxOutputPath.Size = new System.Drawing.Size(365, 20);
            this.tbxOutputPath.TabIndex = 7;
            this.tbxOutputPath.Validating += new System.ComponentModel.CancelEventHandler(this.tbxOutputPath_Validating);
            this.tbxOutputPath.Validated += new System.EventHandler(this.tbxOutputPath_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Output path:";
            // 
            // btnOutputPathBrowse
            // 
            this.btnOutputPathBrowse.Location = new System.Drawing.Point(397, 17);
            this.btnOutputPathBrowse.Name = "btnOutputPathBrowse";
            this.btnOutputPathBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnOutputPathBrowse.TabIndex = 8;
            this.btnOutputPathBrowse.Text = "Browse";
            this.btnOutputPathBrowse.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(478, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(302, 180);
            this.label5.TabIndex = 9;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // tbxApplicationName
            // 
            this.tbxApplicationName.Location = new System.Drawing.Point(6, 78);
            this.tbxApplicationName.Name = "tbxApplicationName";
            this.tbxApplicationName.Size = new System.Drawing.Size(365, 20);
            this.tbxApplicationName.TabIndex = 11;
            this.tbxApplicationName.Validating += new System.ComponentModel.CancelEventHandler(this.tbxApplicationName_Validating);
            this.tbxApplicationName.Validated += new System.EventHandler(this.tbxApplicationName_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Application Name:";
            // 
            // tbxCompanyName
            // 
            this.tbxCompanyName.Location = new System.Drawing.Point(6, 195);
            this.tbxCompanyName.Name = "tbxCompanyName";
            this.tbxCompanyName.Size = new System.Drawing.Size(365, 20);
            this.tbxCompanyName.TabIndex = 13;
            this.tbxCompanyName.Validating += new System.ComponentModel.CancelEventHandler(this.tbxCompanyName_Validating);
            this.tbxCompanyName.Validated += new System.EventHandler(this.tbxCompanyName_Validated);
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Location = new System.Drawing.Point(3, 179);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(85, 13);
            this.lblCompanyName.TabIndex = 12;
            this.lblCompanyName.Text = "Company Name:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnBuildPathBrowse
            // 
            this.btnBuildPathBrowse.Location = new System.Drawing.Point(397, 56);
            this.btnBuildPathBrowse.Name = "btnBuildPathBrowse";
            this.btnBuildPathBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBuildPathBrowse.TabIndex = 16;
            this.btnBuildPathBrowse.Text = "Browse";
            this.btnBuildPathBrowse.UseVisualStyleBackColor = true;
            // 
            // tbxBuildPath
            // 
            this.tbxBuildPath.Location = new System.Drawing.Point(6, 58);
            this.tbxBuildPath.Name = "tbxBuildPath";
            this.tbxBuildPath.Size = new System.Drawing.Size(365, 20);
            this.tbxBuildPath.TabIndex = 15;
            this.tbxBuildPath.Validating += new System.ComponentModel.CancelEventHandler(this.tbxBuildPath_Validating);
            this.tbxBuildPath.Validated += new System.EventHandler(this.tbxBuildPath_Validated);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Build path:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpUserVariables);
            this.tabControl1.Controls.Add(this.tpRegistryKeys);
            this.tabControl1.Controls.Add(this.tpBuildSettings);
            this.tabControl1.Location = new System.Drawing.Point(6, 221);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(797, 195);
            this.tabControl1.TabIndex = 19;
            // 
            // tpUserVariables
            // 
            this.tpUserVariables.Controls.Add(this.userVariableListControl1);
            this.tpUserVariables.Location = new System.Drawing.Point(4, 22);
            this.tpUserVariables.Name = "tpUserVariables";
            this.tpUserVariables.Padding = new System.Windows.Forms.Padding(3);
            this.tpUserVariables.Size = new System.Drawing.Size(789, 169);
            this.tpUserVariables.TabIndex = 0;
            this.tpUserVariables.Text = "Global User Variables";
            this.tpUserVariables.UseVisualStyleBackColor = true;
            // 
            // tpRegistryKeys
            // 
            this.tpRegistryKeys.Controls.Add(this.registryKeyListControl1);
            this.tpRegistryKeys.Location = new System.Drawing.Point(4, 22);
            this.tpRegistryKeys.Name = "tpRegistryKeys";
            this.tpRegistryKeys.Padding = new System.Windows.Forms.Padding(3);
            this.tpRegistryKeys.Size = new System.Drawing.Size(789, 169);
            this.tpRegistryKeys.TabIndex = 1;
            this.tpRegistryKeys.Text = "Registry Keys";
            this.tpRegistryKeys.UseVisualStyleBackColor = true;
            // 
            // tpBuildSettings
            // 
            this.tpBuildSettings.Controls.Add(this.label4);
            this.tpBuildSettings.Controls.Add(this.btnBuildPathBrowse);
            this.tpBuildSettings.Controls.Add(this.tbxOutputPath);
            this.tpBuildSettings.Controls.Add(this.tbxBuildPath);
            this.tpBuildSettings.Controls.Add(this.btnOutputPathBrowse);
            this.tpBuildSettings.Controls.Add(this.label7);
            this.tpBuildSettings.Location = new System.Drawing.Point(4, 22);
            this.tpBuildSettings.Name = "tpBuildSettings";
            this.tpBuildSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuildSettings.Size = new System.Drawing.Size(789, 169);
            this.tpBuildSettings.TabIndex = 2;
            this.tpBuildSettings.Text = "Build";
            this.tpBuildSettings.UseVisualStyleBackColor = true;
            // 
            // userVariableListControl1
            // 
            this.userVariableListControl1.BuildProfile = null;
            this.userVariableListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userVariableListControl1.Location = new System.Drawing.Point(3, 3);
            this.userVariableListControl1.Name = "userVariableListControl1";
            this.userVariableListControl1.Size = new System.Drawing.Size(783, 163);
            this.userVariableListControl1.TabIndex = 18;
            // 
            // registryKeyListControl1
            // 
            this.registryKeyListControl1.BuildProfile = null;
            this.registryKeyListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.registryKeyListControl1.Location = new System.Drawing.Point(3, 3);
            this.registryKeyListControl1.Name = "registryKeyListControl1";
            this.registryKeyListControl1.Size = new System.Drawing.Size(783, 163);
            this.registryKeyListControl1.TabIndex = 0;
            // 
            // BuildProjectSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tbxCompanyName);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.tbxApplicationName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbxReleaseName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxProjectVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxOutputFileName);
            this.Controls.Add(this.label1);
            this.Name = "BuildProjectSettingsControl";
            this.Size = new System.Drawing.Size(806, 421);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tpUserVariables.ResumeLayout(false);
            this.tpRegistryKeys.ResumeLayout(false);
            this.tpBuildSettings.ResumeLayout(false);
            this.tpBuildSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxOutputFileName;
        private System.Windows.Forms.TextBox tbxProjectVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxReleaseName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxOutputPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOutputPathBrowse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxApplicationName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxCompanyName;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnBuildPathBrowse;
        private System.Windows.Forms.TextBox tbxBuildPath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpUserVariables;
        private UserVariableListControl userVariableListControl1;
        private System.Windows.Forms.TabPage tpRegistryKeys;
        private System.Windows.Forms.TabPage tpBuildSettings;
        private RegistryKeyListControl registryKeyListControl1;
    }
}
