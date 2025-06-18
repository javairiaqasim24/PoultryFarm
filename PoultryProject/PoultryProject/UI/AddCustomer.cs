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

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(contact))
                {
                    MessageBox.Show("Please enter all required fields (Name and Contact).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Customers c = new Customers(name, contact, address);
                bool result = cus.Add(c);

                if (result)
                {
                    MessageBox.Show("Customer added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add Customer. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex) when (ex.Number == 1062)
            {
                // Duplicate entry violation (e.g., name already exists)
                MessageBox.Show("Customer with this name already exists. Please choose a different name.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AddCustomer_Load(object sender, EventArgs e)
        {

        }
    }
}
