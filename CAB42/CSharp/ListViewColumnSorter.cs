//-----------------------------------------------------------------------
// <copyright file="ListViewColumnSorter.cs" company="42A Consulting">
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
namespace SkandiaBevakning.SIS
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        #region Fields
        /// <summary>
        /// Specifies the column to be sorted
        /// </summary>
        private int columnToSort;

        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        private SortOrder orderOfSort;

        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        private CaseInsensitiveComparer objectCompare;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the ListViewColumnSorter class.
        /// </summary>
        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            this.columnToSort = 0;

            // Initialize the sort order to 'none'
            this.orderOfSort = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            this.objectCompare = new CaseInsensitiveComparer();
        }

        /// <summary>
        /// Initializes a new instance of the ListViewColumnSorter class.
        /// Uses the default constructor and registeres 
        /// a ColumnClick event handler on the specified listView object.
        /// </summary>
        /// <param name="listView">the ListViewItem to register a ColumnClick event handler on</param>
        public ListViewColumnSorter(ListView listView) 
            : this()
        {
            this.RegisterColumnClickHandler(listView);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn
        {
            get
            {
                return this.columnToSort;
            }

            set
            {
                this.columnToSort = value;
            }
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order
        {
            get
            {
                return this.orderOfSort;
            }

            set
            {
                this.orderOfSort = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            // Compare the two items
            compareResult = this.objectCompare.Compare(
                listviewX.SubItems[this.columnToSort].Text, 
                listviewY.SubItems[this.columnToSort].Text);

            // Calculate correct return value based on object comparison
            if (this.orderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (this.orderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return -compareResult;
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

        /// <summary>
        /// Registers internal handling of the ColumnClick event on the specified listview.
        /// </summary>
        /// <param name="listView">the ListView to register eventhandler on.</param>
        public void RegisterColumnClickHandler(ListView listView)
        {
            if (listView == null)
            {
                throw new ArgumentNullException("listView");
            }

            listView.ColumnClick += new ColumnClickEventHandler(this.ListViewColumnClick);
        }

        /// <summary>
        /// Un registers internal handling of the ColumnClick event on the specified listView.
        /// </summary>
        /// <param name="listView">the ListView to unregister event handler on.</param>
        public void UnregisterColumnClickHandler(ListView listView)
        {
            if (listView == null)
            {
                throw new ArgumentNullException("listView");
            }

            listView.ColumnClick -= new ColumnClickEventHandler(this.ListViewColumnClick);
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// Handles the ColumnClick event on ListViews. Performs a common sort handle logic for listviews.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A empty EventArgs object.</param>
        /// <remarks>
        ///     <para>
        ///         Sorts the listview according to to clicked column using 
        ///         ListView.ListViewItemSorter property and ListView.Sort() method.
        ///     </para>
        ///     <para>
        ///         If the column was clicked twice in a row the sort order is reversed.
        ///     </para>
        ///     <para>
        ///         Stores this ListViewColumnSorter object on the ListView's  ListViewItemSorter 
        ///         property in order to be able to call the ListView.Sort() method.
        ///     </para>
        /// </remarks>
        protected virtual void ListViewColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView listView = sender as ListView;

            if (listView == null)
            {
                return;
            }

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == this.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (this.Order == SortOrder.Ascending)
                {
                    this.Order = SortOrder.Descending;
                }
                else
                {
                    this.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                this.SortColumn = e.Column;
                this.Order = SortOrder.Ascending;
            }

            if (listView.ListViewItemSorter != this)
            {
                listView.ListViewItemSorter = this;
            }

            // Perform the sort with these new sort options.
            listView.Sort();
        }
        #endregion
    }
}
