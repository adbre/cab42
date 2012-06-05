//-----------------------------------------------------------------------
// <copyright file="ListViewExtensions.cs" company="42A Consulting">
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
namespace C42A.CSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Extensions for the System.Windows.Forms.ListView class and support classes.
    /// </summary>
    public static class ListViewExtensions
    {
        /// <summary>
        /// Anger associationsobject (Tag) och fyller en ListViewItem med text.
        /// </summary>
        /// <param name="lvi">The list view item to populate data in.</param>
        /// <param name="tag">The tag object to bind the row to.</param>
        /// <param name="values">A collection of values to fill the sub items with.</param>
        public static void PopulateListViewItem(this System.Windows.Forms.ListViewItem lvi, object tag, params string[] values)
        {
            lvi.Tag = tag;

            PopulateListViewItem(lvi, values);
        }

        /// <summary>
        /// Fyller en ListViewItem med text genom att anropa ToString() på objekten i value-samlingen.
        /// </summary>
        /// <param name="lvi">The list view item to populate data in.</param>
        /// <param name="tag">The tag object to bind the row to.</param>
        /// <param name="values">A collection of values to fill the sub items with.</param>
        /// <returns>The number of sub items added.</returns>
        public static int PopulateListViewItem(this System.Windows.Forms.ListViewItem lvi, object tag, params object[] values)
        {
            int i = 0;

            lvi.Tag = tag;

            for (; i < values.Length; ++i)
            {
                PopulateListViewItem(lvi, i, values[i] != null ? values[i].ToString() : null);
            }

            return i;
        }

        /// <summary>
        /// Fyller en ListViewItem med text genom att anropa ToString() på objekten i value-samlingen.
        /// </summary>
        /// <param name="lvi">The list view item to populate data in.</param>
        /// <param name="values">A collection of values to fill the sub items with.</param>
        /// <returns>The number of sub items added.</returns>
        public static int PopulateListViewSubItem(this System.Windows.Forms.ListViewItem lvi, params object[] values)
        {
            int i = 0;

            for (; i < values.Length; ++i)
            {
                PopulateListViewItem(lvi, i, values[i] != null ? values[i].ToString() : null);
            }

            return i;
        }

        /// <summary>
        /// Fyller en ListViewItem med text
        /// </summary>
        /// <param name="lvi">The list view item to populate data in.</param>
        /// <param name="values">A collection of values to fill the sub items with.</param>
        /// <returns>The number of sub items added.</returns>
        public static int PopulateListViewItem(this System.Windows.Forms.ListViewItem lvi, params string[] values)
        {
            int i = 0;

            for (; i < values.Length; ++i)
            {
                PopulateListViewItem(lvi, i, values[i]);
            }

            return i;
        }

        /// <summary>
        /// Anger text för en underenhet i en ListViewItem.
        /// Ifall inte positionen finns i listan läggs en kolumn till på listan.
        /// </summary>
        /// <param name="lvi">Aktuell ListViewItem</param>
        /// <param name="index">Kolumn index som värdet skall anges</param>
        /// <param name="value">Text som skall anges</param>
        public static void PopulateListViewItem(this System.Windows.Forms.ListViewItem lvi, int index, string value)
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
    }
}
