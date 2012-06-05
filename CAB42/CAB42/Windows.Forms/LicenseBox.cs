//-----------------------------------------------------------------------
// <copyright file="LicenseBox.cs" company="42A Consulting">
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
    using System.Windows.Forms;

    /// <summary>
    /// A dialog box for displaying the license terms.
    /// </summary>
    public partial class LicenseBox : Form
    {
        /// <summary>
        /// Initializes a new instance of the LicenseBox class.
        /// </summary>
        public LicenseBox()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the license terms to be displayed in the dialog box.
        /// </summary>
        public string LicenseTerms
        {
            get
            {
                return this.textBox1.Text;
            }

            set
            {
                this.textBox1.Text = value;
            }
        }

        /// <summary>
        /// Shows the form as a modal dialog box with the specified owner.
        /// </summary>
        /// <param name="owner">The owner of the modal dialog box.</param>
        /// <param name="licenseTerms">The license terms to display.</param>
        /// <param name="caption">The title of the dialog box to display.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResult ShowDialog(IWin32Window owner, string licenseTerms, string caption = null)
        {
            using (var f = new LicenseBox())
            {
                f.LicenseTerms = licenseTerms;

                if (!string.IsNullOrEmpty(caption))
                {
                    f.Text = caption;
                }

                return ((System.Windows.Forms.Form)f).ShowDialog(owner);
            }
        }

        /// <inheritdoc />
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // do not select any text...
            this.textBox1.Select(0, 0);
        }
    }
}
