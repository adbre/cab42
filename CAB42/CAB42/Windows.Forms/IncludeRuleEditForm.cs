//-----------------------------------------------------------------------
// <copyright file="IncludeRuleEditForm.cs" company="42A Consulting">
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

    public partial class IncludeRuleEditForm : Form
    {
        private IncludeRule includeRule;

        public IncludeRuleEditForm()
        {
            this.InitializeComponent();
        }

        public IncludeRule Value
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
                    this.tbPath.Text = value.Path;
                    this.tbFolder.Text = value.Folder;
                    this.tbFileName.Text = value.FileName;

                    this.tbShortcutFileName.Text = value.StartMenuShortcut;
                    this.xmlReplacementListControl1.Items = value.XmlReplacementRules;

                    this.checkBox1.Checked = value.XmlReplacementRules.Count > 0;
                    this.cbCreateStartMenuShortcut.Checked = !string.IsNullOrEmpty(value.StartMenuShortcut);
                }
                else
                {
                    this.tbPath.Text = string.Empty;
                    this.tbFolder.Text = string.Empty;
                    this.tbFileName.Text = string.Empty;
                    this.tbShortcutFileName.Text = string.Empty;

                    this.cbCreateStartMenuShortcut.Checked = false;
                    this.checkBox1.Checked = false;

                    this.xmlReplacementListControl1.Items = null;
                }
            }
        }

        public static DialogResult ShowDialog(IWin32Window owner, IncludeRule includeRule)
        {
            if (includeRule == null)
            {
                throw new ArgumentNullException("includeRule");
            }

            using (var f = new IncludeRuleEditForm())
            {
                f.Value = includeRule;

                return f.ShowDialog(owner);
            }
        }

        public static DialogResult ShowDialog(IWin32Window owner, out IncludeRule includeRule)
        {
            using (var f = new IncludeRuleEditForm())
            {
                var dialogResult = f.ShowDialog(owner);

                if (dialogResult == DialogResult.OK)
                {
                    includeRule = f.Value;
                }
                else
                {
                    includeRule = null;
                }

                return dialogResult;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbPath.Text))
            {
                MessageBox.Show(this, "The path must not be empty", this.Text);
                return;
            }

            if (this.includeRule == null)
            {
                this.includeRule = new IncludeRule();
                this.includeRule.XmlReplacementRules = this.xmlReplacementListControl1.Items;
            }
            else if (this.includeRule.XmlReplacementRules.Count > 0 && !this.checkBox1.Checked)
            {
                var dialogResult = MessageBox.Show(
                    "Warning! You are about to save the include rule with the XML box unchecked.\r\n" +
                    "If you continue,  all XML rules for this file will be deleted.\r\n" +
                    "Are you sure you want to continue?",
                    "Saving changes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                if (dialogResult == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            this.includeRule.FileName = this.tbFileName.Text;
            this.includeRule.Folder = this.tbFolder.Text;
            this.includeRule.Path = this.tbPath.Text;

            if (this.cbCreateStartMenuShortcut.Checked)
            {
                this.includeRule.StartMenuShortcut = this.tbShortcutFileName.Text;
            }
            else
            {
                this.includeRule.StartMenuShortcut = null;
            }

            if (!this.checkBox1.Checked)
            {
                this.includeRule.XmlReplacementRules.Clear();
            }

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox1.Enabled = this.checkBox1.Checked;
        }

        private void cbCreateStartMenuShortcut_CheckedChanged(object sender, EventArgs e)
        {
            this.pMenuShurtcut.Enabled = this.cbCreateStartMenuShortcut.Checked;
        }

        private void tbPath_TextChanged(object sender, EventArgs e)
        {
            this.pSingleFileActions.Enabled = !this.tbPath.Text.Contains('*');
        }
    }
}
