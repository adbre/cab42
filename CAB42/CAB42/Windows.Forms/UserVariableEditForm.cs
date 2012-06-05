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

    public partial class UserVariableEditForm : Form
    {
        private UserVariable includeRule;

        public UserVariableEditForm()
        {
            this.InitializeComponent();
        }

        public UserVariable CopyFrom { get; set; }

        public UserVariable Value
        {
            get
            {
                return this.includeRule;
            }

            set
            {
                this.includeRule = value;
                this.tbName.ReadOnly = value != null;

                this.UpdateControls(value);
            }
        }

        private void UpdateControls(UserVariable value)
        {
            if (value != null)
            {
                this.tbName.Text = value.Name;
                this.tbValue.Text = value.Value;
            }
            else
            {
                this.tbName.Text = string.Empty;
                this.tbValue.Text = string.Empty;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbName.Text))
            {
                MessageBox.Show(this, "The name must not be empty", this.Text);
                return;
            }

            if (this.includeRule == null)
            {
                this.includeRule = new UserVariable();
            }

            this.includeRule.Name = this.tbName.Text;
            this.includeRule.Value = this.tbValue.Text;

            this.Close(DialogResult.OK);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(DialogResult.Cancel);
        }

        private void Close(System.Windows.Forms.DialogResult dialogResult)
        {
            this.DialogResult = dialogResult;

            this.Close();
        }

        private void UserVariableEditForm_Load(object sender, EventArgs e)
        {
            if (this.Value == null)
            {
                this.UpdateControls(this.CopyFrom);
            }

            if (this.tbName.Text.Length > 0)
            {
                this.tbValue.Select();
            }
        }
    }
}
