using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pro.BL.Model;
using pro.BL.Bl;
using pro.Interface;
using pro.BL.Bl;
using Poultary.BL.Models;

namespace pro.UI
{
    public partial class AddCustomer : Form
    {
        Icustomer cus=new CustomerBL();
        private Customer main;
        public AddCustomer(Customer main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {

            string name = txtname.Text;
            string contact = txtcontact.Text;
            string address = txtaddress.Text;
            

            Customers c = new Customers(name, contact, address);
            bool result = cus.Add(c);
            if (result)
            {
                MessageBox.Show("Customer added successfully.");
                main.LoadCustomer();
                this.Close();

            }
            else
            {
                MessageBox.Show("Failed to add Customer. Please try again.");
            }
        }

        private void AddCustomer_Load(object sender, EventArgs e)
        {

        }
    }
}
