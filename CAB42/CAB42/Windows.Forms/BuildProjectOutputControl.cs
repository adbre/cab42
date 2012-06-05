//-----------------------------------------------------------------------
// <copyright file="BuildProjectOutputControl.cs" company="42A Consulting">
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

    using C42A.CSharp;
    using C42A.Win32;

    /// <summary>
    /// A control for displaying the filesystem output for a project.
    /// </summary>
    public partial class BuildProjectOutputControl : UserControl
    {
        /// <summary>
        /// The root node for the filesystem on the target machine.
        /// </summary>
        private TreeNode nodeRemoteFileSystem;

        /// <summary>
        /// The application's primary installation folder.
        /// </summary>
        private TreeNode nodeInstallDir;

        /// <summary>
        /// A TreeView node for the Program Files folder on the target machine.
        /// </summary>
        private TreeNode nodeProgramFiles;

        private TreeNode nodeStartMenu;

        /// <summary>
        /// The project.
        /// </summary>
        private ProjectOutput projectOutput;

        public BuildProjectOutputControl()
        {
            this.InitializeComponent();

            this.nodeRemoteFileSystem = this.treeView1.Nodes[0];
            this.nodeInstallDir = this.nodeRemoteFileSystem.Nodes[0];
            this.nodeProgramFiles = this.nodeRemoteFileSystem.Nodes[1];
            this.nodeStartMenu = this.nodeRemoteFileSystem.Nodes[2];
        }

        /// <summary>
        /// Gets or sets the project output object to be displayed.
        /// </summary>
        public ProjectOutput ProjectOutput
        {
            get
            {
                return this.projectOutput;
            }

            set
            {
                this.projectOutput = value;

                this.Refresh();
            }
        }

        /// <summary>
        /// Refreshes the control to reflect any changes made in the selected project.
        /// </summary>
        public new void Refresh()
        {
            this.nodeInstallDir.Nodes.Clear();
            this.nodeProgramFiles.Nodes.Clear();
            this.listView1.Items.Clear();

            this.SuspendLayout();

            if (this.projectOutput != null)
            {
                this.projectOutput.Refresh();

                this.PopulateTree(this.nodeInstallDir, this.projectOutput.InstallationDirectory);
                this.PopulateTree(this.nodeProgramFiles, this.projectOutput.ProgramFiles);
                this.PopulateTree(this.nodeStartMenu, this.projectOutput.StartMenu);

                this.treeView1.SelectedNode = this.nodeInstallDir;
                this.OnTreeViewAfterSelect(this, new TreeViewEventArgs(this.nodeInstallDir));
            }

            this.nodeRemoteFileSystem.ExpandAll();

            this.ResumeLayout(false);
        }

        /// <summary>
        /// Inherited from <see cref="System.Windows.Forms.Control"/>. Raises the <see cref="System.Windows.Forms.Control.Load"/> event.
        /// </summary>
        /// <param name="e">A <see cref="EventArgs"/> containg event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.nodeRemoteFileSystem.ExpandAll();
        }

        /// <summary>
        /// Populates a <see cref="TreeNode"/> with the contents of a <see cref="OutputDirectoryInfo"/>.
        /// </summary>
        /// <param name="treeNode">The <see cref="TreeNode"/> on which to add the contents of the directory.</param>
        /// <param name="directoryInfo">The directory contents to add to the tree node.</param>
        /// <remarks>
        ///     <para>
        ///         This method will set the <see cref="TreeNode.Tag"/> property to <paramref name="directoryInfo"/>.
        ///     </para>
        ///     <para>
        ///         This method will be called recursivly for each sub directory in <paramref name="directoryInfo"/>, creating
        ///         a sub node on <paramref name="treeNode"/>, creating a folder structure in the tree view.
        ///     </para>
        /// </remarks>
        private void PopulateTree(TreeNode treeNode, OutputDirectoryInfo directoryInfo)
        {
            treeNode.Tag = directoryInfo;

            foreach (var subDirectory in directoryInfo.SubDirectories)
            {
                var subNode = treeNode.Nodes.Add(subDirectory.Name);

                subNode.ImageIndex = 2;
                subNode.SelectedImageIndex = 2;

                this.PopulateTree(subNode, subDirectory);
            }
        }

        /// <summary>
        /// Populates the <see cref="ListView"/> of this control with the contents of a directory.
        /// </summary>
        /// <param name="directory">The directory which contents will be listed in the listview.</param>
        private void PopulateListView(OutputDirectoryInfo directory)
        {
            int index = 0;

            if (directory != null)
            {
                foreach (var subDirectory in directory.SubDirectories)
                {
                    this.PopulateListViewItem(this.GetNextListViewItem(index++), subDirectory);
                }

                foreach (var file in directory.Files)
                {
                    this.PopulateListViewItem(this.GetNextListViewItem(index++), file);
                }

                foreach (var shortcut in directory.Shortcuts)
                {
                    this.PopulateListViewItem(this.GetNextListViewItem(index++), shortcut);
                }
            }

            while (index < this.listView1.Items.Count)
            {
                this.listView1.Items.RemoveAt(index);
            }

            this.listView1.SelectedItems.Clear();
        }

        /// <summary>
        /// Gets the next <see cref="ListViewItem"/> in the <see cref="ListView.Items"/> collection.
        /// </summary>
        /// <param name="index">The index of the ListViewItem to get.</param>
        /// <returns>A <see cref="ListViewItem"/> at <paramref name="index"/>.</returns>
        private ListViewItem GetNextListViewItem(int index)
        {
            if (index < this.listView1.Items.Count)
            {
                return this.listView1.Items[index];
            }
            else
            {
                var lvi = new ListViewItem();

                this.listView1.Items.Add(lvi);

                return lvi;
            }
        }

        /// <summary>
        /// Creates a new <see cref="ListViewItem"/> and populates it with file information.
        /// </summary>
        /// <param name="file">The file data to populate the listview item with.</param>
        private void PopulateListViewItem(OutputFileInfo file)
        {
            var lvi = new ListViewItem();

            this.PopulateListViewItem(lvi, file);

            this.listView1.Items.Add(lvi);
        }

        /// <summary>
        /// Creates a new <see cref="ListViewItem"/> and populates it with directory information.
        /// </summary>
        /// <param name="subDirectory">The directory to add to the listview.</param>
        private void PopulateListViewItem(OutputDirectoryInfo subDirectory)
        {
            var lvi = new ListViewItem();

            this.PopulateListViewItem(lvi, subDirectory);

            this.listView1.Items.Add(lvi);
        }

        /// <summary>
        /// Gets the image icon index, appropriate for the specified extension.
        /// </summary>
        /// <param name="extension">The file extension</param>
        /// <returns>A integer value representing the image index for the file icon.</returns>
        private int GetFileImageIndex(string extension)
        {
            switch (extension.ToLower())
            {
                case "$excluded":
                    return 7;

                case ".txt":
                case ".log":
                    return 6;

                case ".xml":
                case ".settings":
                    return 5;

                case ".exe":
                    return 4;

                case ".wav":
                    return 3;
                case ".dll":
                    return 2;

                default:
                    return 1;
            }
        }

        private void PopulateListViewItem(ListViewItem lvi, OutputShortcutInfo shortcut)
        {
            lvi.Tag = shortcut;

            bool excluded = false;
            string extension = ".lnk";

            if (this.ProjectOutput != null)
            {
                var targetFile = shortcut.Target as OutputFileInfo;
                if (targetFile != null)
                {
                    excluded = this.ProjectOutput.ProjectInfo.IsExcluded(targetFile.SourceFile);
                    extension = targetFile.Extension;
                }
            }

            if (excluded)
            {
                lvi.ForeColor = Color.Gray;
                lvi.ImageIndex = this.GetFileImageIndex("$excluded");
            }
            else
            {
                lvi.ForeColor = this.listView1.ForeColor;
                lvi.ImageIndex = this.GetFileImageIndex(extension);
            }

            int i = 0;
            this.PopulateListViewItem(lvi, i++, shortcut.Name);
            this.PopulateListViewItem(lvi, i++, string.Empty);
            this.PopulateListViewItem(lvi, i++, "Shortcut");
            this.PopulateListViewItem(lvi, i++, string.Empty);
            this.PopulateListViewItem(lvi, i++, shortcut.Target.FullName);
            this.PopulateListViewItem(lvi, i++, string.Empty);
        }

        /// <summary>
        /// Populates a <see cref="ListViewItem"/> with information from <paramref name="file"/>.
        /// </summary>
        /// <param name="lvi">The listview item to fill with data.</param>
        /// <param name="file">The file information.</param>
        private void PopulateListViewItem(ListViewItem lvi, OutputFileInfo file)
        {
            lvi.Tag = file;

            if (this.ProjectOutput != null && this.ProjectOutput.ProjectInfo.IsExcluded(file.SourceFile))
            {
                lvi.ForeColor = Color.Gray;
                lvi.ImageIndex = this.GetFileImageIndex("$excluded");
            }
            else
            {
                lvi.ForeColor = this.listView1.ForeColor;
                lvi.ImageIndex = this.GetFileImageIndex(file.Extension);
            }

            int i = 0;
            this.PopulateListViewItem(lvi, i++, file.Name);
            this.PopulateListViewItem(lvi, i++, file.Modified.ToString("yyyy-MM-dd HH:mm:ss"));
            this.PopulateListViewItem(lvi, i++, file.Type);
            this.PopulateListViewItem(lvi, i++, file.Size.ToHumanReadableSize());
            this.PopulateListViewItem(lvi, i++, !string.IsNullOrEmpty(file.SourceFile) ? System.IO.Path.GetFileName(file.SourceFile) : string.Empty);
            this.PopulateListViewItem(lvi, i++, file.SourceFile);
            this.PopulateListViewItem(lvi, i++);
        }

        /// <summary>
        /// Populates a <see cref="ListViewItem"/> with information from <see cref="subDirectory"/>.
        /// </summary>
        /// <param name="lvi">The listview item to fill with data.</param>
        /// <param name="subDirectory">The directory information.</param>
        private void PopulateListViewItem(ListViewItem lvi, OutputDirectoryInfo subDirectory)
        {
            lvi.Tag = subDirectory;

            lvi.ImageIndex = 0;

            int i = 0;
            this.PopulateListViewItem(lvi, i++, subDirectory.Name);
            this.PopulateListViewItem(lvi, i++);
        }

        /// <summary>
        /// Sets the <see cref="ListViewItem.Text"/> property to <paramref name="value"/> at the <paramref name="index"/>.
        /// </summary>
        /// <param name="lvi">The listview item to set the text value.</param>
        /// <param name="index">The index of the sub item to set the text property.</param>
        /// <param name="value">The text to set.</param>
        private void PopulateListViewItem(ListViewItem lvi, int index, string value)
        {
            if (lvi.SubItems.Count > index)
            {
                lvi.SubItems[index].Text = value;
            }
            else
            {
                lvi.SubItems.Add(value);
            }
        }

        private void PopulateListViewItem(ListViewItem lvi, int index)
        {
            for (; index < lvi.SubItems.Count; index++)
            {
                this.PopulateListViewItem(lvi, index, null);
            }
        }

        /// <summary>
        /// Occurs after a node has been selected in the <see cref="TreeView"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="TreeViewEventArgs"/> containing event data.</param>
        private void OnTreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            var selectedDirectory = e.Node.Tag as OutputDirectoryInfo;

            this.listView1.Tag = e.Node;

            this.PopulateListView(selectedDirectory);
        }

        /// <summary>
        /// Occurs when the user double clicks in the listview.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> containing event data.</param>
        private void OnListViewDoubleClick(object sender, EventArgs e)
        {
            var directoryInfo = this.GetSelectedDirectory();
            var fileInfo = this.GetSelectedFile();
            var shortcutInfo = this.GetSelectedFileSystemInfo() as OutputShortcutInfo;

            var lvi = this.listView1.SelectedItems.Count > 0 ? this.listView1.SelectedItems[0] : null;

            if (directoryInfo != null)
            {
                var parentNode = this.listView1.Tag as TreeNode;

                if (parentNode != null)
                {
                    foreach (var nodeObject in parentNode.Nodes)
                    {
                        var node = nodeObject as TreeNode;

                        if (node != null && node.Tag == directoryInfo)
                        {
                            this.treeView1.SelectedNode = node;
                            break;
                        }
                    }
                }
            }
            else if (fileInfo != null || shortcutInfo != null)
            {
                IncludeRule includeRule = null;

                if (fileInfo != null)
                {
                    includeRule = fileInfo.IncludeRule;
                }
                else if (shortcutInfo != null)
                {
                    var targetFile = shortcutInfo.Target as OutputFileInfo;

                    if (targetFile != null)
                    {
                        includeRule = targetFile.IncludeRule;
                    }
                }

                if (includeRule != null && lvi != null)
                {
                    if (IncludeRuleEditForm.ShowDialog(this, includeRule) == DialogResult.OK)
                    {
                        if (fileInfo != null)
                        {
                            this.PopulateListViewItem(lvi, fileInfo);
                        }
                        else if (shortcutInfo != null)
                        {
                            this.PopulateListViewItem(lvi, shortcutInfo);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the first selected <see cref="OutputFileSystemInfo"/> object.
        /// </summary>
        /// <param name="lvi">
        ///     When this method returns, this parameter will be set to the <see cref="ListViewItem"/> 
        ///     which contained the returned <see cref="OutputFileSystemInfo"/> object.
        /// </param>
        /// <returns>The first selected <see cref="OutputFileSystemInfo"/> object in the listview.</returns>
        private OutputFileSystemInfo GetSelectedFileSystemInfo(out ListViewItem lvi)
        {
            lvi = null;
            OutputFileSystemInfo fileInfo = null;

            if (this.listView1.SelectedItems.Count > 0)
            {
                lvi = this.listView1.SelectedItems[0];

                if (lvi != null && lvi.Tag != null)
                {
                    fileInfo = lvi.Tag as OutputFileSystemInfo;
                }
            }

            return fileInfo;
        }

        /// <summary>
        /// Gets a collection of selected files, and their respective <see cref="ListViewItem"/>.
        /// </summary>
        /// <returns>A collection containing the selected <see cref="ListViewItem"/> and the <see cref="OutputFileInfo"/> which they represent.</returns>
        private IEnumerable<KeyValuePair<ListViewItem, OutputFileInfo>> GetSelectedFiles()
        {
            var l = new Dictionary<ListViewItem, OutputFileInfo>();

            if (this.listView1.SelectedItems.Count > 0)
            {
                foreach (var lviObject in this.listView1.SelectedItems)
                {
                    var lvi = lviObject as ListViewItem;

                    if (lvi != null && lvi.Tag != null)
                    {
                        var fileInfo = lvi.Tag as OutputFileInfo;

                        if (fileInfo != null)
                        {
                            l.Add(lvi, fileInfo);
                        }
                    }
                }
            }

            return l.ToArray();
        }

        /// <summary>
        /// Gets the first selected file.
        /// </summary>
        /// <returns>The first selected file in the ListView.</returns>
        private OutputFileInfo GetSelectedFile()
        {
            return this.GetSelectedFileSystemInfo() as OutputFileInfo;
        }

        private OutputDirectoryInfo GetSelectedDirectory()
        {
            return this.GetSelectedFileSystemInfo() as OutputDirectoryInfo;
        }

        private OutputFileSystemInfo GetSelectedFileSystemInfo()
        {
            ListViewItem lvi;
            return this.GetSelectedFileSystemInfo(out lvi);
        }

        private OutputFileInfo GetSelectedFile(out ListViewItem lvi)
        {
            return this.GetSelectedFileSystemInfo(out lvi) as OutputFileInfo;
        }

        private OutputDirectoryInfo GetSelectedDirectory(out ListViewItem lvi)
        {
            return this.GetSelectedFileSystemInfo(out lvi) as OutputDirectoryInfo;
        }

        private void ignoreToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            var fileInfo = this.GetSelectedFile();

            if (fileInfo != null)
            {
                bool ignored = this.ProjectOutput.ProjectInfo.IsExcluded(fileInfo.SourceFile);
                this.ignoreThisItemForAllProfilesToolStripMenuItem.Enabled = !ignored;
                this.ignoreThisItemInTheSelectedProfileToolStripMenuItem.Enabled = !ignored;
                this.removeFromIgnoreListToolStripMenuItem.Enabled = ignored;
            }
            else
            {
                this.ignoreThisItemForAllProfilesToolStripMenuItem.Enabled = false;
                this.ignoreThisItemInTheSelectedProfileToolStripMenuItem.Enabled = false;
                this.removeFromIgnoreListToolStripMenuItem.Enabled = false;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            var fileInfo = this.GetSelectedFile();
            var dirInfo = this.GetSelectedDirectory();
            var shortcutInfo = this.GetSelectedFileSystemInfo() as OutputShortcutInfo;

            bool hasFileInfo = fileInfo != null;
            bool hasShortcut = shortcutInfo != null;
            bool hasDirInfo = dirInfo != null;

            this.openDirectoryToolStripMenuItem.Visible = hasDirInfo;
            this.openIncludeRuleToolStripMenuItem.Visible = hasFileInfo || hasShortcut;
            this.openfileToolStripMenuItem.Visible = hasFileInfo || hasShortcut;
            this.openfileToolStripMenuItem.Enabled = hasFileInfo;
            this.ignoreToolStripMenuItem.Visible = hasFileInfo && !string.IsNullOrEmpty(fileInfo.SourceFile);
        }

        private void ignoreThisItemInTheSelectedProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ProjectOutput.ProjectInfo.CurrentProfile == null)
            {
                MessageBox.Show(this, "No profile is currently selected.");
            }
            else
            {
                foreach (var item in this.GetSelectedFiles())
                {
                    if (item.Value != null && !string.IsNullOrEmpty(item.Value.SourceFile))
                    {
                        this.ProjectOutput.ProjectInfo.Exclude(item.Value.SourceFile, ExcludeRuleOptions.CurrentProfile);

                        this.PopulateListViewItem(item.Key, item.Value);
                    }
                }
            }            
        }

        private void ignoreThisItemForAllProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in this.GetSelectedFiles())
            {
                if (item.Value != null && !string.IsNullOrEmpty(item.Value.SourceFile))
                {
                    this.ProjectOutput.ProjectInfo.Exclude(item.Value.SourceFile, ExcludeRuleOptions.Global);

                    this.PopulateListViewItem(item.Key, item.Value);
                }
            }
        }

        private void removeFromIgnoreListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in this.GetSelectedFiles())
            {
                if (item.Value != null && !string.IsNullOrEmpty(item.Value.SourceFile))
                {
                    this.ProjectOutput.ProjectInfo.UnExclude(item.Value.SourceFile);

                    this.PopulateListViewItem(item.Key, item.Value);
                }
            }
        }

        private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OnListViewDoubleClick(sender, e);
        }

        private void openIncludeRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OnListViewDoubleClick(sender, e);
        }

        private void openfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileInfo = this.GetSelectedFile();

            if (fileInfo != null)
            {
                if (!string.IsNullOrEmpty(fileInfo.SourceFile) && System.IO.File.Exists(fileInfo.SourceFile))
                {
                    try
                    {
                        using (System.Diagnostics.Process p = new System.Diagnostics.Process())
                        {
                            p.StartInfo.FileName = fileInfo.SourceFile;

                            if (!p.Start())
                            {
                                MessageBox.Show(this, string.Format("Could not open file {0}", fileInfo.Name));
                            }
                        }
                    }
                    catch (Win32Exception x)
                    {
                        // If no application is registered for this file,
                        // the NativeErrorCode will be 1155.
                        // TODO: Add dialog to allow user to select a program to open the file with.

                        MessageBox.Show(this, x.Message);
                    }
                    catch (Exception x)
                    {
#if DEBUG
                        MessageBox.Show(this, x.ToString());
#endif
                    }
                }
                else
                {
                    MessageBox.Show(this, string.Format("The source location for file '{0}' could not be resolved.", fileInfo.Name));
                }
            }
        }
    }
}
