namespace C42A.CAB42.Windows.Forms
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    public partial class RegistryKeyValueListControl : UserControl
    {
        private RegistryKeyValueCollection collection;

        public RegistryKeyValueListControl()
        {
            this.InitializeComponent();

            this.collection = new RegistryKeyValueCollection();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public BuildProfile BuildProfile { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public RegistryKeyValueCollection Items
        {
            get
            {
                return this.collection;
            }
            
            set
            {
                this.collection = value;
                this.listView2.Items.Clear();

                if (value != null)
                {
                    this.Enabled = true;
                    this.Populate(value.Values, false);
                }
                else
                {
                    this.Enabled = false;
                }
            }
        }

        private void Populate(IEnumerable<RegistryKeyValue> collection, bool inherited)
        {
            foreach (var rule in collection)
            {
                this.Populate(rule, inherited);
            }
        }

        private void Populate(RegistryKeyValue rule, bool inherited = false)
        {
            var lvi = new ListViewItem();

            this.Populate(lvi, rule, inherited);

            if (lvi != null)
            {
                this.listView2.Items.Add(lvi);
            }
        }

        private void Populate(ListViewItem lvi, RegistryKeyValue rule, bool inherited = false)
        {
            if (!inherited)
            {
                lvi.Tag = rule;
            }
            else
            {
                lvi.ForeColor = Color.Gray;
            }

            int i = 0;
            this.PopulateListViewItem(lvi, i++, rule.Name);
            this.PopulateListViewItem(lvi, i++, rule.Type.ToString());
            this.PopulateListViewItem(lvi, i++, rule.Value);
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

        private void Add(RegistryKeyValue rule)
        {
            this.collection.Add(rule);

            this.Items = this.Items;
        }

        private void btnIncludeAdd_Click(object sender, EventArgs e)
        {
            using (var f = new RegistryKeyValueEditForm())
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
                    var rule = lvi.Tag as RegistryKeyValue;

                    if (rule != null)
                    {
                        using (var f = new RegistryKeyValueEditForm())
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
                    var rule = lvi.Tag as RegistryKeyValue;

                    if (rule != null)
                    {
                        var dialogResult = MessageBox.Show(
                            this,
                            "Are you sure you want to delete this value?",
                            "Delete value",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button2);

                        if (dialogResult == DialogResult.Yes)
                        {
                            this.collection.Remove(rule.Name);
                            this.listView2.Items.Remove(lvi);
                        }
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
    }
}
