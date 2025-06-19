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
using PoultryProject.UI;

namespace pro.UI
{
    public partial class AddCustomer : Form
    {
        Icustomer cus=new CustomerBL();
       
        private Sellchicks sellchicks;
        private Sellchicks sellchicks1;

        public AddCustomer()
        {
            InitializeComponent();
          
        }

        public AddCustomer(Sellchicks sellchicks)
        {
            this.sellchicks = sellchicks;
        }



        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtname.Text.Trim();
                string contact = txtcontact.Text.Trim();
                string address = txtaddress.Text.Trim();

                // Basic UI-level validation for empty fields
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Please enter the customer's name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtname.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(contact))
                {
                    MessageBox.Show("Please enter the customer's contact.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcontact.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(address))
                {
                    MessageBox.Show("Please enter the customer's address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtaddress.Focus();
                    return;
                }

                // Create and add customer
                Customers c = new Customers(name, contact, address);
                bool result = cus.Add(c);

                if (result)
                {
                    MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearboxes();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add customer. Please check the input or try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddCustomer_Load(object sender, EventArgs e)
        {

        }
        private void clearboxes()
        {
            txtname.Clear();
            txtcontact.Clear();
            txtaddress.Clear();
        }
    }
}
