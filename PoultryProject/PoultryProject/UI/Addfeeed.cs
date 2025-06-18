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
using PoultryProject.DL;
using pro.BL.Bl;
using pro.Interface;

namespace PoultryProject.UI
{
    public partial class Addfeeed : Form
    {
        Isupplier supplier = new SupplierBL();
        feedinterface ibl = new feedBL();

        public Addfeeed()
        {
            InitializeComponent();
        }

        private void Addfeeed_Load(object sender, EventArgs e)
        {
            load();
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

            var matchingNames = supplierBillDL.GetSupplierNamesLike(searchText, "Feed");

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
        private void load()
        {
            //Load suppliers or other necessary data here
            //For example, you might want to populate a dropdown with suppliers
            var suppliers = supplier.getsupplierbytype("feed");
            txtsupplier.DataSource = suppliers;
            txtsupplier.SelectedIndex = -1; 
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtname.Text.Trim();
                string supplier = txtsupplier.Text.Trim();
                DateTime purchaseDate = txtdate.Value;

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(supplier))
                {
                    MessageBox.Show("Name and Supplier are required.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtquantity.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity greater than 0.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(txtweight.Text, out double weight) || weight <= 0)
                {
                    MessageBox.Show("Please enter a valid weight greater than 0.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtprice.Text, out int price) || price <= 0)
                {
                    MessageBox.Show("Please enter a valid price greater than 0.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                feed f = new feed(name, purchaseDate, price, weight, quantity, supplier);
                bool result = ibl.addfeed(f);

                if (result)
                {
                    MessageBox.Show("Feed added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add feed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pro.UI.AddSupplier supplierUI = new pro.UI.AddSupplier();
            supplierUI.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
