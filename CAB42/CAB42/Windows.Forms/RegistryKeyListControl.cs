//-----------------------------------------------------------------------
// <copyright file="RegistryKeyListControl.cs" company="42A Consulting">
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

    public partial class RegistryKeyListControl : UserControl
    {
        private RegistryKeyCollection collection;

        private List<string> uniqueKeyNames;

        public RegistryKeyListControl()
        {
            this.InitializeComponent();

            this.collection = new RegistryKeyCollection();
            this.uniqueKeyNames = new List<string>();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public BuildProfile BuildProfile { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public RegistryKeyCollection Items
        {
            get
            {
                return this.collection;
            }
            
            set
            {
                this.collection = value;
                this.listView2.Items.Clear();
                this.uniqueKeyNames.Clear();

                if (value != null && value.Count > 0)
                {
                    this.Populate(value.Values, false);
                }

                this.registryKeyValueListControl1.Items = null;
            }
        }

        private void Populate(IEnumerable<RegistryKey> collection, bool inherited)
        {
            foreach (var rule in collection)
            {
                this.Populate(rule, inherited);
            }
        }

        private void Populate(RegistryKey rule, bool inherited = false)
        {
            var lvi = new ListViewItem();

            this.Populate(lvi, rule, inherited);

            if (!inherited && !this.uniqueKeyNames.Contains(rule.Name))
            {
                this.uniqueKeyNames.Add(rule.Name);
            }
            else if (inherited && this.uniqueKeyNames.Contains(rule.Name))
            {
                lvi = null;
            }

            if (lvi != null)
            {
                this.listView2.Items.Add(lvi);
            }
        }

        private void Populate(ListViewItem lvi, RegistryKey rule, bool inherited = false)
        {
            if (!inherited)
            {
                lvi.Tag = rule;
            }
            else
            {
                lvi.Tag = new InheritedVariableContainer()
                {
                    Value = rule
                };

                lvi.ForeColor = Color.Gray;
            }

            int i = 0;
            this.PopulateListViewItem(lvi, i++, rule.Name);
        }

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

        private void Add(RegistryKey rule)
        {
            this.collection.Add(rule);

            this.Items = this.Items;
        }

        private void btnIncludeAdd_Click(object sender, EventArgs e)
        {
            using (var f = new RegistryKeyEditForm())
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    this.Add(f.Value);
                }
            }
        }

        private void btnIncludeEdit_Click(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems != null && this.listView2.SelectedItems.Count > 0)
            {
                var lvi = this.listView2.SelectedItems[0];
                if (lvi != null && lvi.Tag != null)
                {
                    var rule = lvi.Tag as RegistryKey;
                    var copyFrom = lvi.Tag as InheritedVariableContainer;

                    if (rule != null)
                    {
                        using (var f = new RegistryKeyEditForm())
                        {
                            f.Value = rule;

                            if (f.ShowDialog(this) == DialogResult.OK)
                            {
                                this.Populate(lvi, rule);
                            }
                        }
                    }                
                }
            }
        }

        private void btnIncludeDelete_Click(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems != null && this.listView2.SelectedItems.Count > 0)
            {
                var lvi = this.listView2.SelectedItems[0];
                if (lvi != null)
                {
                    var rule = lvi.Tag as RegistryKey;
                    var inheritedRule = lvi.Tag as InheritedVariableContainer;

                    if (rule != null)
                    {
                        var dialogResult = MessageBox.Show(
                            this,
                            "Are you sure you want to delete this key?",
                            "Delete key",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button2);

                        if (dialogResult == DialogResult.Yes)
                        {
                            this.collection.Remove(rule.Name);
                            this.listView2.Items.Remove(lvi);
                        }
                    }
                    else if (inheritedRule != null)
                    {
                        var message = "The variable you have selected is inherited from a parent or the " +
                            "global profile and cannot be deleted from this profile.";

                        MessageBox.Show(
                            this, 
                            message, 
                            this.btnIncludeDelete.Text);

                        return;
                    }
                    else
                    {
                        this.listView2.Items.Remove(lvi);
                    }

                    this.Items = this.Items;
                }
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool itemSelected = this.listView2.SelectedItems != null && this.listView2.SelectedItems.Count > 0;

            this.btnIncludeEdit.Enabled = itemSelected;
            this.btnIncludeDelete.Enabled = itemSelected;

            RegistryKey selectedKey = null;
            if (this.listView2.SelectedItems != null && this.listView2.SelectedItems.Count > 0)
            {
                var lvi = this.listView2.SelectedItems[0];

                if (lvi != null && lvi.Tag != null)
                {
                    selectedKey = lvi.Tag as RegistryKey;
                }
            }

            if (selectedKey != null)
            {
                this.registryKeyValueListControl1.Items = selectedKey.Values;
            }
            else
            {
                this.registryKeyValueListControl1.Items = null;
            }
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            this.btnIncludeEdit.PerformClick();
        }

        private class InheritedVariableContainer
        {
            public RegistryKey Value { get; set; }
        }
    }
}
