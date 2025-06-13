using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Poultary.BL.Bl;
using Poultary.BL.Models;
using Poultary.Interfaces;
using Poultary.DL; // Ensure this is included for SupplierDl

namespace Poultary.UI
{
    public partial class Addfeed : Form
    {
        feedinterface ibl = new feedBL();
        ISupplier supplier= new SupplierBL(); // Assuming you have a SupplierBL class that implements ISupplier

        public Addfeed()
        {
            InitializeComponent();
            this.Load += Addfeed_Load; // Link the load event
        }

        private void Addfeed_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
        }

        private void LoadSuppliers()
        {
            try
            {
                var suppliers = supplier.getsupplierbytype("feed"); // Your static method
                txtsupplier.DataSource = suppliers;
                txtsupplier.SelectedIndex = -1; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load suppliers: {ex.Message}");
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtname.Text;
                double weight = Convert.ToDouble(txtweight.Text);
                int quantity = int.Parse(txtquantity.Text);
                DateTime purchaseDate = txtdate.Value;
                int price = int.Parse(txtprice.Text);
                string supplierName = txtsupplier.Text;

                feed c = new feed(name, purchaseDate, price, weight, quantity, supplierName);
                bool result = ibl.addfeed(c);
                if (result)
                {
                    MessageBox.Show("Feed batch added successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add feed batch. Please try again.");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numbers for weight, quantity, and price.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}");
            }
        }
    }
}
