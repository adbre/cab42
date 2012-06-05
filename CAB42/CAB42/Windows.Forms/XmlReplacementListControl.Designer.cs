namespace C42A.CAB42.Windows.Forms
{
    partial class XmlReplacementListControl
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
            this.btnIncludeDelete.Text = "Delete Variable";
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
            this.btnIncludeEdit.Text = "Edit Variable";
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
            this.btnIncludeAdd.Text = "Add Variable";
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
            // 
            // chName
            // 
            this.chName.Text = "Tag";
            this.chName.Width = 374;
            // 
            // chValue
            // 
            this.chValue.Text = "Value";
            this.chValue.Width = 374;
            // 
            // XmlReplacementListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnIncludeDelete);
            this.Controls.Add(this.btnIncludeEdit);
            this.Controls.Add(this.btnIncludeAdd);
            this.Controls.Add(this.listView2);
            this.Name = "XmlReplacementListControl";
            this.Size = new System.Drawing.Size(842, 162);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnIncludeDelete;
        private System.Windows.Forms.Button btnIncludeEdit;
        private System.Windows.Forms.Button btnIncludeAdd;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chValue;
    }
}
