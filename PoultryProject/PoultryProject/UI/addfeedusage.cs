using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PoultryProject.BL.Bl;
using PoultryProject.BL.Models;
using PoultryProject.DL;
/*using PoultryProject.Interfaces;
*/
namespace PoultryProject.UI
{
    public partial class addfeedusage : Form
    {
        ITrackfeed ibl = new trackfeedBL();

        public addfeedusage()
        {
            InitializeComponent();
        }

        private void addfeedusage_Load(object sender, EventArgs e)
        {
            LoadSupplierComboBox();
        }
        private void LoadSupplierComboBox()
        {
            txtsupplier.AutoCompleteMode = AutoCompleteMode.None;
            txtsupplier.AutoCompleteSource = AutoCompleteSource.None;
            txtsupplier.AutoCompleteCustomSource = null;
            txtsupplier.DropDownStyle = ComboBoxStyle.DropDown;
        }
        private void txtsupplier_TextUpdate(object sender, EventArgs e)
        {
            string searchText = txtsupplier.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
                return;

            var matchingNames = ibl.GetChickBatchNames(searchText);

            var autoSource = new AutoCompleteStringCollection();
            autoSource.AddRange(matchingNames.ToArray());
            txtsupplier.AutoCompleteCustomSource = autoSource;

            txtsupplier.DataSource = null;
            txtsupplier.Items.Clear();
            txtsupplier.Items.AddRange(matchingNames.ToArray());

            txtsupplier.DroppedDown = true;
            txtsupplier.SelectionStart = txtsupplier.Text.Length;
            txtsupplier.SelectionLength = 0;
            Cursor.Current = Cursors.Default;
        }
        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txtsupplier.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a batch.");
                return;
            }

            string batchName = txtsupplier.Text;

            if (!int.TryParse(txtquantity.Text, out int count))
            {
                MessageBox.Show("Please enter a valid sack count.");
                return;
            }

            DateTime date = txtdate.Value;

            var obj = new trackfeed(batchName, count, date);
            bool result = ibl.addtrack(obj);

            if (result)
            {
                MessageBox.Show("Feed usage added successfully.");
            }
            else
            {
                MessageBox.Show("Failed to add feed usage.");
            }
        }


        private void ClearFields()
        {
            txtsupplier.SelectedIndex = -1;
            txtquantity.Clear();
            txtdate.Value = DateTime.Now;
        }
    }
}
