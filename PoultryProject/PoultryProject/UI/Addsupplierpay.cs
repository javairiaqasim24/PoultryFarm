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
using PoultryProject.Interfaces;

namespace PoultryProject.UI
{
    public partial class Addsupplierpay : Form
    {
        ISupplierpayemnts ibl = new supplierpaymentBL();
        public Addsupplierpay()
        {
            InitializeComponent();
        }

        private void Addsupplierpay_Load(object sender, EventArgs e)
        {
            LoadSupplierComboBox();
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

        private void btnadd_Click(object sender, EventArgs e)
        {
            string name = txtsupplier.Text.Trim();
            int billid = Convert.ToInt32(txtbill.Text);
            DateTime date = txtdate.Value;
            double amount = Convert.ToDouble(txtamount.Text);
            supplierpayment s = new supplierpayment(billid, name, 0, date, amount);
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
    }

}