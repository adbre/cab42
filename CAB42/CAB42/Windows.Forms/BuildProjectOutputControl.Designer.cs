namespace C42A.CAB42.Windows.Forms
{
    partial class BuildProjectOutputControl
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
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Application Folder");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Program Files Folder");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Start Menu Folder", 1, 1);
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("File System on Target Machine", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildProjectOutputControl));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.chFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFileModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFileType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFileSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFileSource = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFileSourcePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ignoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreThisItemInTheSelectedProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreThisItemForAllProfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFromIgnoreListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.openIncludeRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView1);
            this.splitContainer1.Size = new System.Drawing.Size(925, 386);
            this.splitContainer1.SplitterDistance = 211;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode5.ImageKey = "folder_wrench.png";
            treeNode5.Name = "Node1";
            treeNode5.SelectedImageIndex = 1;
            treeNode5.Text = "Application Folder";
            treeNode6.ImageKey = "folder_wrench.png";
            treeNode6.Name = "Node3";
            treeNode6.SelectedImageKey = "folder_wrench.png";
            treeNode6.Text = "Program Files Folder";
            treeNode7.ImageIndex = 1;
            treeNode7.Name = "Node0";
            treeNode7.SelectedImageIndex = 1;
            treeNode7.Text = "Start Menu Folder";
            treeNode8.Name = "Node0";
            treeNode8.Text = "File System on Target Machine";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(211, 386);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnTreeViewAfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "computer.png");
            this.imageList1.Images.SetKeyName(1, "folder_wrench.png");
            this.imageList1.Images.SetKeyName(2, "folder.png");
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFileName,
            this.chFileModified,
            this.chFileType,
            this.chFileSize,
            this.chFileSource,
            this.chFileSourcePath});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(710, 386);
            this.listView1.SmallImageList = this.imageList2;
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.OnListViewDoubleClick);
            // 
            // chFileName
            // 
            this.chFileName.Text = "File Name";
            this.chFileName.Width = 273;
            // 
            // chFileModified
            // 
            this.chFileModified.Text = "Date modified";
            this.chFileModified.Width = 152;
            // 
            // chFileType
            // 
            this.chFileType.Text = "Type";
            this.chFileType.Width = 151;
            // 
            // chFileSize
            // 
            this.chFileSize.Text = "Size";
            this.chFileSize.Width = 102;
            // 
            // chFileSource
            // 
            this.chFileSource.Text = "Source Filename";
            this.chFileSource.Width = 120;
            // 
            // chFileSourcePath
            // 
            this.chFileSourcePath.Width = 180;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openIncludeRuleToolStripMenuItem,
            this.openDirectoryToolStripMenuItem,
            this.openfileToolStripMenuItem,
            this.ignoreToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(182, 114);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // ignoreToolStripMenuItem
            // 
            this.ignoreToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ignoreThisItemInTheSelectedProfileToolStripMenuItem,
            this.ignoreThisItemForAllProfilesToolStripMenuItem,
            this.removeFromIgnoreListToolStripMenuItem});
            this.ignoreToolStripMenuItem.Name = "ignoreToolStripMenuItem";
            this.ignoreToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.ignoreToolStripMenuItem.Text = "Ignore";
            this.ignoreToolStripMenuItem.DropDownOpening += new System.EventHandler(this.ignoreToolStripMenuItem_DropDownOpening);
            // 
            // ignoreThisItemInTheSelectedProfileToolStripMenuItem
            // 
            this.ignoreThisItemInTheSelectedProfileToolStripMenuItem.Name = "ignoreThisItemInTheSelectedProfileToolStripMenuItem";
            this.ignoreThisItemInTheSelectedProfileToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.ignoreThisItemInTheSelectedProfileToolStripMenuItem.Text = "Ignore this item in the selected profile";
            this.ignoreThisItemInTheSelectedProfileToolStripMenuItem.Click += new System.EventHandler(this.ignoreThisItemInTheSelectedProfileToolStripMenuItem_Click);
            // 
            // ignoreThisItemForAllProfilesToolStripMenuItem
            // 
            this.ignoreThisItemForAllProfilesToolStripMenuItem.Name = "ignoreThisItemForAllProfilesToolStripMenuItem";
            this.ignoreThisItemForAllProfilesToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.ignoreThisItemForAllProfilesToolStripMenuItem.Text = "Ignore this item for all profiles";
            this.ignoreThisItemForAllProfilesToolStripMenuItem.Click += new System.EventHandler(this.ignoreThisItemForAllProfilesToolStripMenuItem_Click);
            // 
            // removeFromIgnoreListToolStripMenuItem
            // 
            this.removeFromIgnoreListToolStripMenuItem.Name = "removeFromIgnoreListToolStripMenuItem";
            this.removeFromIgnoreListToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.removeFromIgnoreListToolStripMenuItem.Text = "Remove from ignore list";
            this.removeFromIgnoreListToolStripMenuItem.Click += new System.EventHandler(this.removeFromIgnoreListToolStripMenuItem_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "folder.png");
            this.imageList2.Images.SetKeyName(1, "page.png");
            this.imageList2.Images.SetKeyName(2, "database_gear.png");
            this.imageList2.Images.SetKeyName(3, "sound.png");
            this.imageList2.Images.SetKeyName(4, "application.png");
            this.imageList2.Images.SetKeyName(5, "page_code.png");
            this.imageList2.Images.SetKeyName(6, "page_edit.png");
            this.imageList2.Images.SetKeyName(7, "delete.png");
            // 
            // openIncludeRuleToolStripMenuItem
            // 
            this.openIncludeRuleToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openIncludeRuleToolStripMenuItem.Name = "openIncludeRuleToolStripMenuItem";
            this.openIncludeRuleToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.openIncludeRuleToolStripMenuItem.Text = "&Open include rule...";
            this.openIncludeRuleToolStripMenuItem.Click += new System.EventHandler(this.openIncludeRuleToolStripMenuItem_Click);
            // 
            // openDirectoryToolStripMenuItem
            // 
            this.openDirectoryToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openDirectoryToolStripMenuItem.Name = "openDirectoryToolStripMenuItem";
            this.openDirectoryToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.openDirectoryToolStripMenuItem.Text = "&Open directory";
            this.openDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openDirectoryToolStripMenuItem_Click);
            // 
            // openfileToolStripMenuItem
            // 
            this.openfileToolStripMenuItem.Name = "openfileToolStripMenuItem";
            this.openfileToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.openfileToolStripMenuItem.Text = "Open &file";
            this.openfileToolStripMenuItem.Click += new System.EventHandler(this.openfileToolStripMenuItem_Click);
            // 
            // BuildProjectOutputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "BuildProjectOutputControl";
            this.Size = new System.Drawing.Size(925, 386);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader chFileName;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ColumnHeader chFileModified;
        private System.Windows.Forms.ColumnHeader chFileType;
        private System.Windows.Forms.ColumnHeader chFileSize;
        private System.Windows.Forms.ColumnHeader chFileSource;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ignoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ignoreThisItemInTheSelectedProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ignoreThisItemForAllProfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFromIgnoreListToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader chFileSourcePath;
        private System.Windows.Forms.ToolStripMenuItem openIncludeRuleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openfileToolStripMenuItem;


    }
}
