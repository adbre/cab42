namespace C42A.CAB42.Windows.Forms
{
    partial class RegistryKeyValueListControl
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
            this.chValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblInheritedHelp = new System.Windows.Forms.Label();
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnIncludeDelete
            // 
            this.btnIncludeDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIncludeDelete.Enabled = false;
            this.btnIncludeDelete.Location = new System.Drawing.Point(165, 137);
            this.btnIncludeDelete.Name = "btnIncludeDelete";
            this.btnIncludeDelete.Size = new System.Drawing.Size(91, 23);
            this.btnIncludeDelete.TabIndex = 8;
            this.btnIncludeDelete.Text = "Delete Value";
            this.btnIncludeDelete.UseVisualStyleBackColor = true;
            this.btnIncludeDelete.Click += new System.EventHandler(this.btnIncludeDelete_Click);
            // 
            // btnIncludeEdit
            // 
            this.btnIncludeEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIncludeEdit.Enabled = false;
            this.btnIncludeEdit.Location = new System.Drawing.Point(84, 137);
            this.btnIncludeEdit.Name = "btnIncludeEdit";
            this.btnIncludeEdit.Size = new System.Drawing.Size(75, 23);
            this.btnIncludeEdit.TabIndex = 7;
            this.btnIncludeEdit.Text = "Edit Value";
            this.btnIncludeEdit.UseVisualStyleBackColor = true;
            this.btnIncludeEdit.Click += new System.EventHandler(this.btnIncludeEdit_Click);
            // 
            // btnIncludeAdd
            // 
            this.btnIncludeAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIncludeAdd.Location = new System.Drawing.Point(3, 137);
            this.btnIncludeAdd.Name = "btnIncludeAdd";
            this.btnIncludeAdd.Size = new System.Drawing.Size(75, 23);
            this.btnIncludeAdd.TabIndex = 6;
            this.btnIncludeAdd.Text = "Add Value";
            this.btnIncludeAdd.UseVisualStyleBackColor = true;
            this.btnIncludeAdd.Click += new System.EventHandler(this.btnIncludeAdd_Click);
            // 
            // listView2
            // 
            this.listView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chType,
            this.chValue});
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(3, 3);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(836, 128);
            this.listView2.TabIndex = 5;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            this.listView2.DoubleClick += new System.EventHandler(this.listView2_DoubleClick);
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 196;
            // 
            // chValue
            // 
            this.chValue.Text = "Data";
            this.chValue.Width = 374;
            // 
            // lblInheritedHelp
            // 
            this.lblInheritedHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInheritedHelp.AutoSize = true;
            this.lblInheritedHelp.Location = new System.Drawing.Point(285, 142);
            this.lblInheritedHelp.Name = "lblInheritedHelp";
            this.lblInheritedHelp.Size = new System.Drawing.Size(343, 13);
            this.lblInheritedHelp.TabIndex = 9;
            this.lblInheritedHelp.Text = "Grayed items are inherited from a parent profile or from the global profile.";
            // 
            // chType
            // 
            this.chType.Text = "Type";
            this.chType.Width = 121;
            // 
            // RegistryKeyValueListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblInheritedHelp);
            this.Controls.Add(this.btnIncludeDelete);
            this.Controls.Add(this.btnIncludeEdit);
            this.Controls.Add(this.btnIncludeAdd);
            this.Controls.Add(this.listView2);
            this.Name = "RegistryKeyValueListControl";
            this.Size = new System.Drawing.Size(842, 162);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIncludeDelete;
        private System.Windows.Forms.Button btnIncludeEdit;
        private System.Windows.Forms.Button btnIncludeAdd;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.Label lblInheritedHelp;
        private System.Windows.Forms.ColumnHeader chType;
    }
}
