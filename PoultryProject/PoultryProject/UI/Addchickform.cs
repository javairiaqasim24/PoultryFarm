using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Poultary.BL.Bl;
using Poultary.BL.Models;
using Poultary.Interfaces;
using PoultryProject.DL;
using pro.BL.Bl;
using pro.Interface;
using MySql.Data.MySqlClient;

namespace Poultary.UI
{
    public partial class Addchickform : Form
    {
        ChickenBatchInterface _chickenBatchService = new ChickenbatchBL();
        Isupplier supplier = new SupplierBL();
        public Addchickform()
        {
            InitializeComponent();
            this.Load += Addchickform_Load; 
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtname.Text;
                float weight = float.Parse(txtweight.Text);
                int quantity = int.Parse(txtquantity.Text);
                DateTime purchaseDate = txtdate.Value;
                int price = int.Parse(txtprice.Text);
                string supplierName = txtsupplier.Text;

                ChickenBatch c = new ChickenBatch(name, purchaseDate, price, weight, quantity, supplierName);

                bool result = _chickenBatchService.AddChickenBatch(c);

                if (result)
                {
                    MessageBox.Show("Chicken batch added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearboxes();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add chicken batch. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for weight, quantity, and price.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Batch name already exists. Please enter a unique name.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
       

        }


        private void Addchickform_Load(object sender, EventArgs e)
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

            var matchingNames = supplierBillDL.GetSupplierNamesLike(searchText,"Chick");

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
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new pro.UI.AddSupplier().ShowDialog();
        }

        private void txtsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void clearboxes()
        {
            txtname.Clear();
            txtquantity.Clear();
            txtsupplier.Text = "";
            txtweight.Clear();
            txtprice.Clear();
        }

    }
}
