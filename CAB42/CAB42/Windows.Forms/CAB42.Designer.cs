namespace C42A.CAB42.Windows.Forms
{
    partial class CAB42
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSaveProject = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBuildCab = new System.Windows.Forms.ToolStripButton();
            this.tcbCurrentProfile = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutCAB42ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buildProjectControl1 = new C42A.CAB42.Windows.Forms.BuildProjectControl();
            this.licenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSaveProject,
            this.tsbOpenProject,
            this.toolStripSeparator1,
            this.tsbBuildCab,
            this.tcbCurrentProfile,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1008, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbSaveProject
            // 
            this.tsbSaveProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveProject.Image = global::C42A.Properties.Resources.disk;
            this.tsbSaveProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveProject.Name = "tsbSaveProject";
            this.tsbSaveProject.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveProject.Text = "toolStripButton1";
            this.tsbSaveProject.Click += new System.EventHandler(this.tsbSaveProject_Click);
            // 
            // tsbOpenProject
            // 
            this.tsbOpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpenProject.Image = global::C42A.Properties.Resources.folder;
            this.tsbOpenProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenProject.Name = "tsbOpenProject";
            this.tsbOpenProject.Size = new System.Drawing.Size(23, 22);
            this.tsbOpenProject.Text = "toolStripButton2";
            this.tsbOpenProject.Click += new System.EventHandler(this.tsbOpenProject_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbBuildCab
            // 
            this.tsbBuildCab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBuildCab.Image = global::C42A.Properties.Resources.brick_go;
            this.tsbBuildCab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBuildCab.Name = "tsbBuildCab";
            this.tsbBuildCab.Size = new System.Drawing.Size(23, 22);
            this.tsbBuildCab.Text = "Build CAB";
            this.tsbBuildCab.Click += new System.EventHandler(this.tsbBuildCab_Click);
            // 
            // tcbCurrentProfile
            // 
            this.tcbCurrentProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tcbCurrentProfile.Name = "tcbCurrentProfile";
            this.tcbCurrentProfile.Size = new System.Drawing.Size(121, 25);
            this.tcbCurrentProfile.DropDown += new System.EventHandler(this.tcbCurrentProfile_DropDown);
            this.tcbCurrentProfile.SelectedIndexChanged += new System.EventHandler(this.tcbCurrentProfile_SelectedIndexChanged);
            this.tcbCurrentProfile.Click += new System.EventHandler(this.tcbCurrentProfile_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::C42A.Properties.Resources.arrow_refresh;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.newToolStripMenuItem.Text = "&New..";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.licenseToolStripMenuItem,
            this.aboutCAB42ToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "&Help";
            // 
            // aboutCAB42ToolStripMenuItem
            // 
            this.aboutCAB42ToolStripMenuItem.Name = "aboutCAB42ToolStripMenuItem";
            this.aboutCAB42ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.aboutCAB42ToolStripMenuItem.Text = "&About CAB42...";
            this.aboutCAB42ToolStripMenuItem.Click += new System.EventHandler(this.aboutCAB42ToolStripMenuItem_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "c42";
            this.saveFileDialog1.Filter = "CAB42 files|*.c42|XML files|*.xml|All files|*.*";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "c42";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "CAB42 files|*.c42|XML files|*.xml|All files|*.*";
            // 
            // buildProjectControl1
            // 
            this.buildProjectControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buildProjectControl1.Location = new System.Drawing.Point(0, 49);
            this.buildProjectControl1.Name = "buildProjectControl1";
            this.buildProjectControl1.ProjectInfo = null;
            this.buildProjectControl1.Size = new System.Drawing.Size(1008, 541);
            this.buildProjectControl1.TabIndex = 4;
            // 
            // licenseToolStripMenuItem
            // 
            this.licenseToolStripMenuItem.Name = "licenseToolStripMenuItem";
            this.licenseToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.licenseToolStripMenuItem.Text = "&License...";
            this.licenseToolStripMenuItem.Click += new System.EventHandler(this.licenseToolStripMenuItem_Click);
            // 
            // CAB42
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 590);
            this.Controls.Add(this.buildProjectControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CAB42";
            this.Text = "CAB42";
            this.Shown += new System.EventHandler(this.CAB42_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSaveProject;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbOpenProject;
        private System.Windows.Forms.ToolStripButton tsbBuildCab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox tcbCurrentProfile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private BuildProjectControl buildProjectControl1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutCAB42ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem licenseToolStripMenuItem;

    }
}

