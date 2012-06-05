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

    using C42A.CAB42.Cabwiz;

    public partial class CAB42 : Form
    {
        private bool populatingProfiles = false;
        private ProjectInfo buildProject;

        public CAB42()
        {
            var controlStyles = ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer;

            this.SetStyle(controlStyles, true);

            this.InitializeComponent();

            this.BuildProject = null;
        }

        /// <summary>
        /// Gets or sets a file path which, when the Show event is triggered, will be read and and opened automatically.
        /// </summary>
        public string OpenFileOnShow { get; set; }

        public ProjectInfo BuildProject
        {
            get
            {
                return this.buildProject;
            }

            set
            {
                this.buildProject = value;

                this.buildProjectControl1.ProjectInfo = value;

                if (value != null)
                {
                    this.Text = string.Format("{0} - {1}", value.ProjectFile.Name, "CAB42");
                }
                else
                {
                    this.Text = "CAB42";
                }

                this.PopulateProfileDropDown();

                bool haveBuildProject = value != null;

                this.tsbSaveProject.Enabled = haveBuildProject;
                this.tsbBuildCab.Enabled = haveBuildProject;
                this.tcbCurrentProfile.Enabled = haveBuildProject;
                this.toolStripButton1.Enabled = haveBuildProject;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (!string.IsNullOrEmpty(this.OpenFileOnShow))
            {
                this.OpenProjectFile(this.OpenFileOnShow);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DialogResult Save()
        {
            this.buildProjectControl1.Validate();

            ProjectInfo project;

            if (this.BuildProject == null)
            {
                project = new ProjectInfo();
            }
            else
            {
                project = this.BuildProject;
            }

            if (project.ProjectFile == null)
            {
                if (this.saveFileDialog1.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
                {
                    return System.Windows.Forms.DialogResult.Cancel;
                }
                else
                {
                    project.ProjectFile = new System.IO.FileInfo(this.saveFileDialog1.FileName);
                }
            }

            project.Save();

            if (this.BuildProject != project)
                this.BuildProject = project;

            return System.Windows.Forms.DialogResult.OK;
        }

        private void tsbSaveProject_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void tsbOpenProject_Click(object sender, EventArgs e)
        {
            this.OpenProjectFile(null);
        }

        private void OpenProjectFile(string fileName)
        {
            if (!this.AskIfUnsaved())
            {
                return;
            }

            if (string.IsNullOrEmpty(fileName))
            {
                if (this.openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = this.openFileDialog1.FileName;
                }
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                try
                {
                    var newProject = ProjectInfo.Open(fileName);
                    if (newProject != null)
                    {
                        this.BuildProject = newProject;
                    }
                    else
                    {
                        ////MessageBox.Show(this, "No project file was opened", "Open project");
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(this, x.Message, "Failed to open project");
                }
            }
        }

        private bool HasUnsaved()
        {
            return true;
        }

        private bool AskIfUnsaved()
        {
            if (this.BuildProject != null && this.HasUnsaved())
            {
                var message = string.Format(
                        "Opening another project will disregard from any changes made to the current project.{0}" +
                        "Do you want to save your current work before continuing?",
                        Environment.NewLine);

                var dialogResult = MessageBox.Show(
                    this,
                    message,
                    "Open project",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button3);

                if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                {
                    return false;
                }
                else if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    if (this.Save() != System.Windows.Forms.DialogResult.OK)
                    {
                        return false;
                    }
                }
                else if (dialogResult == System.Windows.Forms.DialogResult.No)
                {
                    // do not save work... (do nothing)
                }
            }

            return true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.BuildProject != null)
            {
                this.BuildProject.Output.Refresh();
            }

            this.buildProjectControl1.Refresh();
        }

        private void tsbBuildCab_Click(object sender, EventArgs e)
        {
            if (this.BuildProject != null)
            {
                if (this.HasUnsaved())
                {
                    this.Save();
                }

                using (var f = new BuildForm())
                {
                    using (var buildContext = new CabwizBuildContext())
                    {
                        f.BuildTasks = this.BuildProject.CreateBuildTasks();
                        f.Context = buildContext;

                        f.ShowDialog(this);
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "No project is currently opened", this.tsbBuildCab.Text);
            }
        }

        private void tcbCurrentProfile_Click(object sender, EventArgs e)
        {

        }

        private void tcbCurrentProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.populatingProfiles)
            {
                var profile = this.tcbCurrentProfile.SelectedItem as BuildProfile;

                if (this.BuildProject != null)
                {
                    this.BuildProject.CurrentProfile = profile;

                    // refresh
                    this.toolStripButton1.PerformClick();
                }
            }
        }

        private void tcbCurrentProfile_DropDown(object sender, EventArgs e)
        {
            this.PopulateProfileDropDown();
        }

        private void PopulateProfileDropDown()
        {
            if (!this.populatingProfiles)
            {
                this.populatingProfiles = true;

                try
                {
                    this.tcbCurrentProfile.Items.Clear();

                    if (this.BuildProject != null)
                    {
                        this.tcbCurrentProfile.Items.AddRange(this.BuildProject.Profiles.Values.ToArray());

                        this.tcbCurrentProfile.SelectedItem = this.BuildProject.CurrentProfile;
                    }
                }
                finally
                {
                    this.populatingProfiles = false;
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.AskIfUnsaved())
                {
                    if (this.saveFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        this.BuildProject = ProjectInfo.New(this.saveFileDialog1.FileName);
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(this, x.Message, "Failed to create new project file");
            }
        }

        private void aboutCAB42ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new AboutBox())
            {
                f.ShowDialog(this);
            }
        }

        private void CAB42_Shown(object sender, EventArgs e)
        {
            var shellInfo = new global::C42A.Win32.ShellExtension(".c42");

            if (!shellInfo.IsDefaultApplication())
            {
                var message = "This application is not registered as the default handler for .c42 files.\r\n" +
                    "Do you want this program to be the default handler for CAB 42 files?";

                var dialogResult = MessageBox.Show(
                    this,
                    message,
                    "File Extension Association",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.None,
                    MessageBoxDefaultButton.Button2);

                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    shellInfo.Save();
                }
            }
        }

        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LicenseBox.ShowDialog(this, Properties.Resources.LICENSE);
        }
    }
}
