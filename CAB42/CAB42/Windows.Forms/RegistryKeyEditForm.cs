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

    public partial class RegistryKeyEditForm : Form
    {
        private RegistryKey includeRule;

        public RegistryKeyEditForm()
        {
            this.InitializeComponent();
        }
        public RegistryKey Value
        {
            get
            {
                return this.includeRule;
            }

            set
            {
                this.includeRule = value;

                if (value != null)
                {
                    this.tbName.Text = value.Name;
                }
                else
                {
                    this.tbName.Text = string.Empty;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.tbName.Text))
                {
                    MessageBox.Show(this, "The name must not be empty", this.Text);
                    return;
                }

                if (this.includeRule == null)
                {
                    this.includeRule = new RegistryKey();
                }

                this.includeRule.Name = this.tbName.Text;

                this.Close(DialogResult.OK);
            }
            catch (Exception x)
            {
                MessageBox.Show(this, x.Message, "Saving changes");
            }
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

        }
    }
}
