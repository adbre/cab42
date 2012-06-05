

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

    public partial class RegistryKeyValueEditForm : Form
    {
        private RegistryKeyValue includeRule;

        public RegistryKeyValueEditForm()
        {
            this.InitializeComponent();
        }
        public RegistryKeyValue Value
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
                    this.includeRule = new RegistryKeyValue();
                }

                this.includeRule.Name = this.tbName.Text;
                this.includeRule.Type = this.GetValueType();
                this.includeRule.Value = this.GetFormatedValue(this.includeRule.Type, this.tbValue.Text);

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

        private Cabwiz.RegistryValueTypes GetValueType()
        {
            var types = new Cabwiz.RegistryValueTypes[] {
                Cabwiz.RegistryValueTypes.String,
                Cabwiz.RegistryValueTypes.BINARY,
                Cabwiz.RegistryValueTypes.DWORD,
                Cabwiz.RegistryValueTypes.MULTI_SZ
            };

            if (this.comboBox1.SelectedIndex > -1 && this.comboBox1.SelectedIndex < types.Length)
            {
                return types[this.comboBox1.SelectedIndex];
            }
            else
            {
                return default(Cabwiz.RegistryValueTypes);
            }
        }

        private string GetFormatedValue(Cabwiz.RegistryValueTypes type, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            switch (type)
            {
                case Cabwiz.RegistryValueTypes.SZ:
                case Cabwiz.RegistryValueTypes.MULTI_SZ:
                    return value;
                    
                case Cabwiz.RegistryValueTypes.DWORD:
                    {
                        int nValue;

                        var invariant = System.Globalization.CultureInfo.InvariantCulture;

                        var cultures = new System.IFormatProvider[] {
                            System.Globalization.CultureInfo.CurrentUICulture,
                            invariant
                        };

                        foreach (var culture in cultures)
                        {
                            if (int.TryParse(value, System.Globalization.NumberStyles.Integer, culture, out nValue))
                            {
                                return nValue.ToString(invariant);
                            }
                        }

                        // If we have reached this point, no parse was successful. Whine a bit.
                        throw new FormatException("The value could not a well-formatted 32-bit integer.");
                    }

                case Cabwiz.RegistryValueTypes.BINARY:
                    {
                        throw new NotSupportedException("BINARY data types are not yet supported by the visual editor.");
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException("type");
                    }
            }
        }
    }
}
