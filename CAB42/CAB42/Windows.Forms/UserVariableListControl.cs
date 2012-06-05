//-----------------------------------------------------------------------
// <copyright file="UserVariableListControl.cs" company="42A Consulting">
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

    public partial class UserVariableListControl : UserControl
    {
        private UserVariableCollection collection;

        private List<string> uniqueVariables;

        public UserVariableListControl()
        {
            this.InitializeComponent();

            this.collection = new UserVariableCollection();
            this.uniqueVariables = new List<string>();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public BuildProfile BuildProfile { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public UserVariableCollection Items
        {
            get
            {
                return this.collection;
            }
            
            set
            {
                this.collection = value;
                this.listView2.Items.Clear();
                this.uniqueVariables.Clear();

                if (value != null && value.Count > 0)
                {
                    this.Populate(value.Values, false);
                }

                if (this.BuildProfile != null)
                {
                    this.Populate(this.BuildProfile.GetVariables(), true);
                    this.lblInheritedHelp.Visible = true;
                }
                else
                {
                    this.lblInheritedHelp.Visible = false;
                }
            }
        }

        private void Populate(IEnumerable<UserVariable> collection, bool inherited)
        {
            foreach (var rule in collection)
            {
                this.Populate(rule, inherited);
            }
        }

        private void Populate(UserVariable rule, bool inherited = false)
        {
            var lvi = new ListViewItem();

            this.Populate(lvi, rule, inherited);

            if (!inherited && !this.uniqueVariables.Contains(rule.Name))
            {
                this.uniqueVariables.Add(rule.Name);
            }
            else if (inherited && this.uniqueVariables.Contains(rule.Name))
            {
                lvi = null;
            }

            if (lvi != null)
            {
                this.listView2.Items.Add(lvi);
            }
        }

        private void Populate(ListViewItem lvi, UserVariable rule, bool inherited = false)
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

            this.PopulateListView(lvi, rule.Name, rule.Value);
        }

        private void PopulateListView(ListViewItem lvi, string key, string value)
        {
            int i = 0;
            this.PopulateListViewItem(lvi, i++, key);
            this.PopulateListViewItem(lvi, i++, value);
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

        private void Add(UserVariable rule)
        {
            this.collection.Add(rule);

            this.Items = this.Items;
        }

        private void btnIncludeAdd_Click(object sender, EventArgs e)
        {
            using (var f = new UserVariableEditForm())
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
                    var rule = lvi.Tag as UserVariable;
                    var copyFrom = lvi.Tag as InheritedVariableContainer;

                    if (rule != null)
                    {
                        using (var f = new UserVariableEditForm())
                        {
                            f.Value = rule;

                            if (f.ShowDialog(this) == DialogResult.OK)
                            {
                                this.Populate(lvi, rule);
                            }
                        }
                    }
                    else if (copyFrom != null)
                    {
                        using (var f = new UserVariableEditForm())
                        {
                            f.CopyFrom = copyFrom.Value;

                            if (f.ShowDialog(this) == DialogResult.OK)
                            {
                                this.Add(f.Value);
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
                    var rule = lvi.Tag as UserVariable;
                    var inheritedRule = lvi.Tag as InheritedVariableContainer;

                    if (rule != null)
                    {
                        var dialogResult = MessageBox.Show(
                            this,
                            "Are you sure you want to delete this rule?",
                            "Delete rule",
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
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            this.btnIncludeEdit.PerformClick();
        }

        private class InheritedVariableContainer
        {
            public UserVariable Value { get; set; }
        }
    }
}
