namespace C42A.CAB42.Windows.Forms
{
    partial class IncludeRuleEditForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tbShortcutFileName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCreateStartMenuShortcut = new System.Windows.Forms.CheckBox();
            this.pSingleFileActions = new System.Windows.Forms.Panel();
            this.pMenuShurtcut = new System.Windows.Forms.Panel();
            this.xmlReplacementListControl1 = new C42A.CAB42.Windows.Forms.XmlReplacementListControl();
            this.groupBox1.SuspendLayout();
            this.pSingleFileActions.SuspendLayout();
            this.pMenuShurtcut.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path";
            // 
            // tbPath
            // 
            this.tbPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPath.Location = new System.Drawing.Point(69, 22);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(486, 20);
            this.tbPath.TabIndex = 1;
            this.tbPath.TextChanged += new System.EventHandler(this.tbPath_TextChanged);
            // 
            // tbFolder
            // 
            this.tbFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFolder.Location = new System.Drawing.Point(69, 48);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.Size = new System.Drawing.Size(486, 20);
            this.tbFolder.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sub Folder";
            // 
            // tbFileName
            // 
            this.tbFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileName.Location = new System.Drawing.Point(69, 74);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(486, 20);
            this.tbFileName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "File Name";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(525, 453);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(444, 453);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(561, 20);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(39, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.xmlReplacementListControl1);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(3, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(582, 219);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "XML tag values";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(3, 79);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(126, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Modify XML contents";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // tbShortcutFileName
            // 
            this.tbShortcutFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbShortcutFileName.Location = new System.Drawing.Point(57, 3);
            this.tbShortcutFileName.Name = "tbShortcutFileName";
            this.tbShortcutFileName.Size = new System.Drawing.Size(486, 20);
            this.tbShortcutFileName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "File Name";
            // 
            // cbCreateStartMenuShortcut
            // 
            this.cbCreateStartMenuShortcut.AutoSize = true;
            this.cbCreateStartMenuShortcut.Location = new System.Drawing.Point(3, 3);
            this.cbCreateStartMenuShortcut.Name = "cbCreateStartMenuShortcut";
            this.cbCreateStartMenuShortcut.Size = new System.Drawing.Size(153, 17);
            this.cbCreateStartMenuShortcut.TabIndex = 0;
            this.cbCreateStartMenuShortcut.Text = "Create Start Menu shortcut";
            this.cbCreateStartMenuShortcut.UseVisualStyleBackColor = true;
            this.cbCreateStartMenuShortcut.CheckedChanged += new System.EventHandler(this.cbCreateStartMenuShortcut_CheckedChanged);
            // 
            // pSingleFileActions
            // 
            this.pSingleFileActions.Controls.Add(this.pMenuShurtcut);
            this.pSingleFileActions.Controls.Add(this.cbCreateStartMenuShortcut);
            this.pSingleFileActions.Controls.Add(this.checkBox1);
            this.pSingleFileActions.Controls.Add(this.groupBox1);
            this.pSingleFileActions.Location = new System.Drawing.Point(12, 123);
            this.pSingleFileActions.Name = "pSingleFileActions";
            this.pSingleFileActions.Size = new System.Drawing.Size(588, 324);
            this.pSingleFileActions.TabIndex = 7;
            // 
            // pMenuShurtcut
            // 
            this.pMenuShurtcut.Controls.Add(this.label4);
            this.pMenuShurtcut.Controls.Add(this.tbShortcutFileName);
            this.pMenuShurtcut.Enabled = false;
            this.pMenuShurtcut.Location = new System.Drawing.Point(0, 26);
            this.pMenuShurtcut.Name = "pMenuShurtcut";
            this.pMenuShurtcut.Size = new System.Drawing.Size(582, 34);
            this.pMenuShurtcut.TabIndex = 5;
            // 
            // xmlReplacementListControl1
            // 
            this.xmlReplacementListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmlReplacementListControl1.Location = new System.Drawing.Point(3, 16);
            this.xmlReplacementListControl1.Name = "xmlReplacementListControl1";
            this.xmlReplacementListControl1.Size = new System.Drawing.Size(576, 200);
            this.xmlReplacementListControl1.TabIndex = 0;
            // 
            // IncludeRuleEditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(612, 488);
            this.Controls.Add(this.pSingleFileActions);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "IncludeRuleEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Include Rule";
            this.groupBox1.ResumeLayout(false);
            this.pSingleFileActions.ResumeLayout(false);
            this.pSingleFileActions.PerformLayout();
            this.pMenuShurtcut.ResumeLayout(false);
            this.pMenuShurtcut.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.TextBox tbFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private XmlReplacementListControl xmlReplacementListControl1;
        private System.Windows.Forms.TextBox tbShortcutFileName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbCreateStartMenuShortcut;
        private System.Windows.Forms.Panel pSingleFileActions;
        private System.Windows.Forms.Panel pMenuShurtcut;
    }
}