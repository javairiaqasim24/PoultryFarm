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
                MessageBox.Show("Chicken batch added successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to add chicken batch. Please try again.");
            }
        }

        private void Addchickform_Load(object sender, EventArgs e)
        {
            load(); 
        }
        private void load()
        {
            try
            {
                var suppliers = supplier.getsupplierbytype("chick"); 
                txtsupplier.DataSource = suppliers;
                txtsupplier.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load suppliers: {ex.Message}");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new pro.UI.AddSupplier().ShowDialog();
        }

        private void txtsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
