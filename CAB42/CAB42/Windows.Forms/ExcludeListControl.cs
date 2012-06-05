namespace C42A.CAB42.Windows.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    public partial class ExcludeListControl : UserControl
    {
        private ExcludeRuleCollection collection;

        public ExcludeListControl()
        {
            this.InitializeComponent();

            this.collection = new ExcludeRuleCollection();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public ExcludeRuleCollection Items
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

        private void PopulateListView(IEnumerable<ExcludeRule> collection)
        {
            foreach (var rule in collection)
            {
                this.PopulateListView(rule);
            }
        }

        private void PopulateListView(ExcludeRule rule)
        {
            var lvi = new ListViewItem();

            this.PopulateListView(lvi, rule);

            this.listView2.Items.Add(lvi);
        }

        private void PopulateListView(ListViewItem lvi, ExcludeRule rule)
        {
            lvi.Tag = rule;

            int i = 0;
            this.PopulateListViewItem(lvi, i++, rule.FileName);
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

        private void Add(ExcludeRule rule)
        {
            this.collection.Add(rule);

            this.PopulateListView(rule);
        }

        private void btnIncludeAdd_Click(object sender, EventArgs e)
        {
            using (var f = new ExcludeRuleEditForm())
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
                    var rule = lvi.Tag as ExcludeRule;

                    if (rule != null)
                    {
                        using (var f = new ExcludeRuleEditForm())
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
            var dialogResult = MessageBox.Show(
                            this,
                            "Are you sure you want to delete this rule?",
                            "Delete rule",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button2);

            if (dialogResult == DialogResult.Yes)
            {

                if (this.listView2.SelectedItems != null && this.listView2.SelectedItems.Count > 0)
                {
                    var selectedItems = new List<ListViewItem>();
                    for (int i = 0; i < this.listView2.SelectedItems.Count; i++)
                        selectedItems.Add(this.listView2.SelectedItems[i]);

                    foreach (var lvi in selectedItems)
                    {
                        var rule = lvi.Tag as ExcludeRule;

                        if (rule != null)
                        {
                            this.collection.Remove(rule);
                            this.listView2.Items.Remove(lvi);
                        }
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
