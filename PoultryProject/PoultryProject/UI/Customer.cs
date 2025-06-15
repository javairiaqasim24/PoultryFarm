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
using Poultary.BL.Models;
using Poultary.BL.Bl;

namespace pro.UI
{
    public partial class Customer : Form
    {
        Icustomer supp = new CustomerBL();
        int currentitemid = -1;
        private bool isPanelCollapsed = true;
        private const int PanelExpandedWidth = 181;
        private const int PanelCollapsedWidth = 50;
        private const int SlideStep = 10;
        private Color hoverColor = Color.FromArgb(40, 55, 71);
        public Customer()
        {
            InitializeComponent();
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            panel7.Dock = DockStyle.Fill;
            this.Shown += ViewOrderAd_Shown;
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

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            var pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                pictureBox.BackColor = hoverColor;
                pictureBox.Cursor = Cursors.Hand;


            }
        }
        private void ViewOrderAd_Shown(object sender, EventArgs e)
        {
            flowLayoutPanel1.Width = PanelCollapsedWidth;

            this.PerformLayout();
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            var pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                pictureBox.BackColor = Color.Transparent;
                pictureBox.Cursor = Cursors.Default;


            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isPanelCollapsed)
            {
                flowLayoutPanel1.Width += SlideStep;
                if (flowLayoutPanel1.Width >= PanelExpandedWidth)
                {
                    timer1.Stop();
                    isPanelCollapsed = false;
                }
            }
            else
            {
                flowLayoutPanel1.Width -= SlideStep;
                if (flowLayoutPanel1.Width <= PanelCollapsedWidth)
                {
                    timer1.Stop();
                    isPanelCollapsed = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            LoadCustomer();
        }
    }
}
