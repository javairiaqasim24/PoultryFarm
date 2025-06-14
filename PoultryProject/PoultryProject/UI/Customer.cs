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
using pro.BL.Model;
using pro.Interface;

namespace pro.UI
{
    public partial class Customer : Form
    {
        Icustomer supp = new CustomerBL();
        int currentitemid = -1;
        public Customer()
        {
            InitializeComponent();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            LoadCustomer();
            dataGridViewCustomer.RowEnter += dataGridViewCustomer_rowselected;
        }
        public void LoadCustomer()
        {

            var customers = supp.GetCustomers();
            dataGridViewCustomer.DataSource = customers;
            dataGridViewCustomer.Columns["CustomerID"].Visible = false;
            dataGridViewCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridViewCustomer_rowselected(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow selectedRow = dataGridViewCustomer.Rows[e.RowIndex];
            Customers selectedItem = selectedRow.DataBoundItem as Customers;
            if (selectedItem == null) return;

            txtname.Text = selectedItem.Name;
            txtcontact.Text = selectedItem.Contact.ToString();
            txtaddress.Text = selectedItem.Address.ToString();
            


            currentitemid = selectedItem.CustomerID;

        }
        private void ClearInputs()
        {
            txtname.Text = "";
            txtcontact.Text = "";
            txtaddress.Text = "";
            currentitemid = -1;
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomer.CurrentRow == null) { return; }
            Customers selectedUser = dataGridViewCustomer.CurrentRow.DataBoundItem as Customers;
            if (selectedUser == null) return;
            selectedUser.Name = txtname.Text;
            selectedUser.Contact = txtcontact.Text;
            selectedUser.Address = txtaddress.Text;         
            selectedUser.CustomerID = currentitemid;
            bool result = supp.Update(selectedUser, currentitemid);
            if (result == true)
            {
                MessageBox.Show("Item Updated Successfully");
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Item Not Updated");
            }
            LoadCustomer();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomer.CurrentRow == null) return;
            Customers selecteditems = dataGridViewCustomer.CurrentRow.DataBoundItem as Customers;
            if (selecteditems == null) return;

            selecteditems.CustomerID = currentitemid;
            DialogResult result = MessageBox.Show($"Are you sure you want to delete {selecteditems.Name}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (supp.delete(currentitemid))
            {
                MessageBox.Show("Item Deleted Successfully");
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Item Not Deleted");
            }
            LoadCustomer();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AddCustomer addCustomer = new AddCustomer(this);
            addCustomer.ShowDialog();   
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtsearch.Text.Trim();
            List<Customers> filteredSuppliers;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                filteredSuppliers = supp.GetCustomers();
            }
            else
            {
                filteredSuppliers = supp.GetCustomersbyName(searchText);
            }

            dataGridViewCustomer.DataSource = filteredSuppliers;
        }
    }
}
