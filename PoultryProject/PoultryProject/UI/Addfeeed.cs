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
            string name = txtname.Text; 
            int quantity = Convert.ToInt32(txtquantity.Text);
            DateTime purchaseDate = txtdate.Value;
            string supplier=txtsupplier.Text;
            double weight = Convert.ToDouble(txtweight.Text);
            int price = Convert.ToInt32(txtprice.Text);
            // Validate inputs
            if (string.IsNullOrWhiteSpace(name) || quantity <= 0 || weight <= 0 || price <= 0 || string.IsNullOrWhiteSpace(supplier))
            {
                MessageBox.Show("Please fill in all fields correctly.");
                return;
            }
            feed f = new feed(name, purchaseDate, price, weight, quantity, supplier);
            bool result = ibl.addfeed(f);
            if (result)
            {
                MessageBox.Show("Feed added successfully.");
                this.Close(); 
            }
            else
            {
                MessageBox.Show("Failed to add feed. Please try again.");
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pro.UI.AddSupplier supplierUI = new pro.UI.AddSupplier();
            supplierUI.ShowDialog();
        }
    }
}
