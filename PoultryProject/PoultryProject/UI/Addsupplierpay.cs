using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PoultryProject.BL.Bl;
using PoultryProject.BL.Models;
using PoultryProject.DL;
using PoultryProject.Interfaces;

namespace PoultryProject.UI
{
    public partial class Addsupplierpay : Form
    {
        ISupplierpayemnts ibl = new supplierpaymentBL();
        public Addsupplierpay()
        {
            InitializeComponent();
            this.txtsupplier.Leave += new System.EventHandler(this.txtsupplier_Leave);

        }

        private void Addsupplierpay_Load(object sender, EventArgs e)
        {
            LoadSupplierComboBox();
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

            var matchingNames = ibl.getsuppliernames(searchText);

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
        private void txtsupplier_Leave(object sender, EventArgs e)
        {
            string supplierName = txtsupplier.Text.Trim();
            if (!string.IsNullOrEmpty(supplierName))
            {
                LoadSupplierBillIds(supplierName);
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtsupplier.Text.Trim();
                int billid = Convert.ToInt32(txtbill.Text);
                DateTime date = txtdate.Value;
                double amount = Convert.ToDouble(txtamount.Text);

                supplierpayment s = new supplierpayment(billid, name, date, amount);

                bool result = ibl.addsupplierpayments(s);

                if (result)
                {
                    MessageBox.Show("Supplier payment added successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add supplier payment. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void txtbill_SelectedIndexChanged(object sender, EventArgs e)
        {
            // REMOVE this completely
        }

        private void LoadSupplierBillIds(string supplierName)
        {
            if (string.IsNullOrWhiteSpace(supplierName))
                return;

            var billIds = supplierpaymentDl.GetBillIdsBySupplierName(supplierName);

            txtbill.DataSource = null;
            txtbill.Items.Clear();

            if (billIds != null && billIds.Count > 0)
            {
                // Convert to string list if needed
                var items = billIds.Select(id => id.ToString()).ToArray();

                txtbill.Items.AddRange(items);

                // Let the user select manually — keep no selection initially
                txtbill.SelectedIndex = -1;
                txtbill.Text = ""; // Clear display text manually if needed
            }
        }



    }
}