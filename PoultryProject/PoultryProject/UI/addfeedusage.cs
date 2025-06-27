using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static List<string> namesofbatches = new List<string>();
        ITrackfeed ibl = new trackfeedBL();

        public addfeedusage()
        {
            InitializeComponent();
            LoadNames();
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
            try
            {
                if (txtsupplier.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a batch.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string batchName = txtsupplier.Text;

                if (!int.TryParse(txtquantity.Text, out int count) || count <= 0)
                {
                    MessageBox.Show("Please enter a valid sack count (greater than 0).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if(txtchickbatches.SelectedIndex == -1)
                {
                    MessageBox.Show("Please enter the name of the batch of chicks , to which the feed is being supplied.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string name = txtchickbatches.Text;

                DateTime date = txtdate.Value;

                var obj = new trackfeed(batchName, count, date , name);
                bool result = ibl.addtrack(obj);

                if (result)
                {
                    MessageBox.Show("Feed usage added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add feed usage.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                // Catch validation errors thrown by BL
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            catch (Exception ex)
            {
                // Catch all unexpected errors
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ClearFields()
        {
            txtsupplier.SelectedIndex = -1;
            txtquantity.Clear();
            txtdate.Value = DateTime.Now;
        }

        private void txtchickbatches_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadNames()
        {
            try
            {


                namesofbatches = trackfeedDL.Getnames("BatchName");

                // Diagnostic output
                Debug.WriteLine($"Loaded {namesofbatches.Count}  BatchName");
                if (namesofbatches.Count == 0)
                {
                    MessageBox.Show("No customers found in database. Check products table.");
                }

                txtchickbatches.DataSource = namesofbatches;
                txtchickbatches.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtchickbatches.AutoCompleteSource = AutoCompleteSource.CustomSource;

                var autoCompleteSource = new AutoCompleteStringCollection();
                autoCompleteSource.AddRange(namesofbatches.ToArray());
                txtchickbatches.AutoCompleteCustomSource = autoCompleteSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading name: {ex.Message}\nCheck db_error.log for details");
            }
        }

    }
}
