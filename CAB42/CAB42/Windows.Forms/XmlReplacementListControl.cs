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

    public partial class XmlReplacementListControl : UserControl
    {
        private XmlReplacementRuleCollection collection;

        public XmlReplacementListControl()
        {
            this.InitializeComponent();

            this.collection = new XmlReplacementRuleCollection();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public XmlReplacementRuleCollection Items
        {
            get
            {
                return this.collection;
            }
            
            set
            {
                this.collection = value;
                this.listView2.Items.Clear();

                if (value != null && value.Count > 0)
                {
                    this.PopulateListView(value);
                }
            }
        }

        private void PopulateListView(IEnumerable<XmlReplacementRule> collection)
        {
            foreach (var rule in collection)
            {
                this.PopulateListView(rule);
            }
        }

        private void PopulateListView(XmlReplacementRule rule)
        {
            var lvi = new ListViewItem();

            this.PopulateListView(lvi, rule);

            this.listView2.Items.Add(lvi);
        }

        private void PopulateListView(ListViewItem lvi, XmlReplacementRule rule)
        {
            lvi.Tag = rule;

            int i = 0;
            this.PopulateListViewItem(lvi, i++, rule.Tag);
            this.PopulateListViewItem(lvi, i++, rule.Value);
        }

        private void PopulateListView(ListViewItem lvi, params string[] values)
        {
            this.PopulateListView(lvi, lvi.SubItems.Count, values);
        }

        private void PopulateListView(ListViewItem lvi, int offset, params string[] values)
        {
            for (int i = offset; i < values.Length; i++)
            {
                this.PopulateListViewItem(lvi, i, values[i]);
            }
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

        private void Add(XmlReplacementRule rule)
        {
            this.collection.Add(rule);

            this.PopulateListView(rule);
        }

        private void btnIncludeAdd_Click(object sender, EventArgs e)
        {
            using (var f = new XmlReplacementEditForm())
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
                if (lvi != null)
                {
                    var rule = lvi.Tag as XmlReplacementRule;

                    if (rule != null)
                    {
                        using (var f = new XmlReplacementEditForm())
                        {
                            f.Value = rule;

                            if (f.ShowDialog(this) == DialogResult.OK)
                            {
                                this.PopulateListView(lvi, rule);
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
                    var rule = lvi.Tag as XmlReplacementRule;

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
                            this.collection.Remove(rule);
                            this.listView2.Items.Remove(lvi);
                        }
                    }
                    else
                    {
                        // This should never happen.
                        this.listView2.Items.Remove(lvi);
                    }
                }
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool itemSelected = this.listView2.SelectedItems != null && this.listView2.SelectedItems.Count > 0;

            this.btnIncludeEdit.Enabled = itemSelected;
            this.btnIncludeDelete.Enabled = itemSelected;
        }
    }
}
