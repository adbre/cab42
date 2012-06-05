namespace C42A.CAB42.Windows.Forms
{
    partial class RegistryKeyListControl
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
            this.btnIncludeDelete = new System.Windows.Forms.Button();
            this.btnIncludeEdit = new System.Windows.Forms.Button();
            this.btnIncludeAdd = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblInheritedHelp = new System.Windows.Forms.Label();
            this.registryKeyValueListControl1 = new C42A.CAB42.Windows.Forms.RegistryKeyValueListControl();
            this.SuspendLayout();
            // 
            // btnIncludeDelete
            // 
            this.btnIncludeDelete.Enabled = false;
            this.btnIncludeDelete.Location = new System.Drawing.Point(165, 130);
            this.btnIncludeDelete.Name = "btnIncludeDelete";
            this.btnIncludeDelete.Size = new System.Drawing.Size(91, 23);
            this.btnIncludeDelete.TabIndex = 8;
            this.btnIncludeDelete.Text = "Delete Key";
            this.btnIncludeDelete.UseVisualStyleBackColor = true;
            this.btnIncludeDelete.Click += new System.EventHandler(this.btnIncludeDelete_Click);
            // 
            // btnIncludeEdit
            // 
            this.btnIncludeEdit.Enabled = false;
            this.btnIncludeEdit.Location = new System.Drawing.Point(84, 130);
            this.btnIncludeEdit.Name = "btnIncludeEdit";
            this.btnIncludeEdit.Size = new System.Drawing.Size(75, 23);
            this.btnIncludeEdit.TabIndex = 7;
            this.btnIncludeEdit.Text = "Edit Key";
            this.btnIncludeEdit.UseVisualStyleBackColor = true;
            this.btnIncludeEdit.Click += new System.EventHandler(this.btnIncludeEdit_Click);
            // 
            // btnIncludeAdd
            // 
            this.btnIncludeAdd.Location = new System.Drawing.Point(3, 130);
            this.btnIncludeAdd.Name = "btnIncludeAdd";
            this.btnIncludeAdd.Size = new System.Drawing.Size(75, 23);
            this.btnIncludeAdd.TabIndex = 6;
            this.btnIncludeAdd.Text = "Add Key";
            this.btnIncludeAdd.UseVisualStyleBackColor = true;
            this.btnIncludeAdd.Click += new System.EventHandler(this.btnIncludeAdd_Click);
            // 
            // listView2
            // 
            this.listView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName});
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(3, 3);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(836, 121);
            this.listView2.TabIndex = 5;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            this.listView2.DoubleClick += new System.EventHandler(this.listView2_DoubleClick);
            // 
            // chName
            // 
            this.chName.Text = "Key";
            this.chName.Width = 710;
            // 
            // lblInheritedHelp
            // 
            this.lblInheritedHelp.AutoSize = true;
            this.lblInheritedHelp.Location = new System.Drawing.Point(262, 135);
            this.lblInheritedHelp.Name = "lblInheritedHelp";
            this.lblInheritedHelp.Size = new System.Drawing.Size(343, 13);
            this.lblInheritedHelp.TabIndex = 9;
            this.lblInheritedHelp.Text = "Grayed items are inherited from a parent profile or from the global profile.";
            // 
            // registryKeyValueListControl1
            // 
            this.registryKeyValueListControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.registryKeyValueListControl1.BuildProfile = null;
            this.registryKeyValueListControl1.Enabled = false;
            this.registryKeyValueListControl1.Location = new System.Drawing.Point(3, 159);
            this.registryKeyValueListControl1.Name = "registryKeyValueListControl1";
            this.registryKeyValueListControl1.Size = new System.Drawing.Size(836, 185);
            this.registryKeyValueListControl1.TabIndex = 10;
            // 
            // RegistryKeyListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.registryKeyValueListControl1);
            this.Controls.Add(this.lblInheritedHelp);
            this.Controls.Add(this.btnIncludeDelete);
            this.Controls.Add(this.btnIncludeEdit);
            this.Controls.Add(this.btnIncludeAdd);
            this.Controls.Add(this.listView2);
            this.Name = "RegistryKeyListControl";
            this.Size = new System.Drawing.Size(842, 347);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIncludeDelete;
        private System.Windows.Forms.Button btnIncludeEdit;
        private System.Windows.Forms.Button btnIncludeAdd;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.Label lblInheritedHelp;
        private RegistryKeyValueListControl registryKeyValueListControl1;
    }
}
