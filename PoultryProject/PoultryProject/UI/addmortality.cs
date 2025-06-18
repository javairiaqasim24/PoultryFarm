using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poultary.BL.Bl;
using Poultary.BL.Models;
using Poultary.Interfaces;

namespace Poultary.UI
{
    public partial class addmortality : Form
    {
        mortalityinterface idl = new mortalityBl();
        public addmortality()
        {
            InitializeComponent();
        }

        private void addmortality_Load(object sender, EventArgs e)
        {
            LoadSupplierComboBox();
        }
       
        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txtsupplier.SelectedValue == null)
            {
                MessageBox.Show("Please select a batch.");
                return;
            }

            int batchId = Convert.ToInt32(txtsupplier.SelectedValue);
            string batchName = txtsupplier.Text;

            if (!int.TryParse(txtquantity.Text, out int count))
            {
                MessageBox.Show("Please enter a valid mortality count.");
                return;
            }

            DateTime date = txtdate.Value;
            string reason = txtreason.Text;

            // Manually set values using the constructor that matches your model
            var obj = new mortality(0, batchId, batchName, count, 0, date, reason);

            bool result = idl.addmortality(obj);
            if (result)
            {
                MessageBox.Show("Mortality added successfully");
            }
            else
            {
                MessageBox.Show("Failed to add mortality. Check if the batch exists.");
            }
        }

        private void txtsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadSupplierComboBox()
        {
            txtsupplier.AutoCompleteMode = AutoCompleteMode.None;
            txtsupplier.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtsupplier.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            txtsupplier.DropDownStyle = ComboBoxStyle.DropDown;
        }
        private void txtsupplier_TextUpdate(object sender, EventArgs e)
{
            string searchText = txtsupplier.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
                return;

            var matchingNames = idl.getbatchnames(searchText);

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

        private void txtdate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtreason_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt1_Click(object sender, EventArgs e)
        {

        }

        private void txtquantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
