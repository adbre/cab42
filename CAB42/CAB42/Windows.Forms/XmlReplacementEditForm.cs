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

    public partial class XmlReplacementEditForm : Form
    {
        private XmlReplacementRule includeRule;

        public XmlReplacementEditForm()
        {
            this.InitializeComponent();
        }

        public XmlReplacementRule Value
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
                    this.tbFileName.Text = value.Tag;
                    this.textBox1.Text = value.Value;
                }
                else
                {
                    this.tbFileName.Text = string.Empty;
                    this.textBox1.Text = string.Empty;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbFileName.Text))
            {
                MessageBox.Show(this, "The name must not be empty", this.Text);
                return;
            }

            if (this.includeRule == null)
            {
                this.includeRule = new XmlReplacementRule();
            }

            this.includeRule.Tag = this.tbFileName.Text;
            this.includeRule.Value = this.textBox1.Text;

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
    }
}
