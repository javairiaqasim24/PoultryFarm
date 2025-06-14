using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pro.BL.Bl;
using pro.Interface;
using pro.BL.Model;
using pro.UI;
using PoultryProject.UI;

namespace pro.UI
{
    public partial class AddSupplier : Form
    {
        Isupplier supp = new SupplierBL();
        public AddSupplier()
        {
            InitializeComponent();
            
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            string name = txtname.Text;
            string contact = txtcontact.Text;
            string address = txtaddress.Text;
            string type = comboBoxType.SelectedItem.ToString();

            Suppliers c = new Suppliers(name, contact, address, type);
            bool result = supp.Add(c);
            if (result)
            {
                MessageBox.Show("Supplier added successfully.");
                this.Close();

            }
            else
            {
                MessageBox.Show("Failed to add Supplier. Please try again.");
            }
        }
    }
}
