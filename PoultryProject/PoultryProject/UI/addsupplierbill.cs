using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poultary.BL.Models;
using PoultryProject.BL.Bl;
using PoultryProject.BL.Models;
using PoultryProject.Interfaces;
using pro.BL.Bl;

namespace PoultryProject.UI
{
    public partial class addsupplierbill : Form
    {
        IsupplierBill ibl = new supplierBillBL();
        public addsupplierbill()
        {
            InitializeComponent();
        }

        private void addsupplierbill_Load(object sender, EventArgs e)
        {
            LoadSupplierComboBox();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            string supplier = txtsupplier.Text;  
            string batch = txtname.Text;
            DateTime date = txtdate.Value;
            string description = txtnotes.Text;

            if (string.IsNullOrEmpty(supplier) || string.IsNullOrEmpty(batch) || string.IsNullOrEmpty(txtamount.Text))
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }

            if (!double.TryParse(txtamount.Text, out double amount))
            {
                MessageBox.Show("Invalid amount format.");
                return;
            }

            supplierbill bill = new supplierbill(supplier, date, amount, batch, description);
            bool result = ibl.addsupplierbills(bill);

            MessageBox.Show(result ? "Supplier bill added successfully." : "Failed to add supplier bill. Please try again.");
        }

        private void LoadSupplierComboBox()
        {
            txtsupplier.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtsupplier.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtsupplier.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            txtsupplier.DropDownStyle = ComboBoxStyle.DropDown;
         
        }
        private void txtsupplier_TextUpdate(object sender, EventArgs e)
        {
            string searchText = txtsupplier.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
                return;

            var matchingNames = ibl.getsuppliernames(searchText,"Chick");

            var autoSource = new AutoCompleteStringCollection();
            autoSource.AddRange(matchingNames.ToArray());
            txtsupplier.AutoCompleteCustomSource = autoSource;

            txtsupplier.DataSource = null;
            txtsupplier.Items.Clear();
            txtsupplier.Items.AddRange(matchingNames.ToArray());

            txtsupplier.DroppedDown = true;
            txtsupplier.SelectionStart = txtsupplier.Text.Length;
            txtsupplier.SelectionLength = 0;
        }

        private void txtsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
