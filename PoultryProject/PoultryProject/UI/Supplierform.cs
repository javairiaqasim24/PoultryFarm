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
using PoultryProject.UI;
using pro.BL.Bl;
using pro.BL.Model;
using pro.Interface;

namespace Poultary.UI
{
    public partial class Supplierform : Form
    {
        Isupplier supp = new SupplierBL();
        int currentitemid = -1;
        public Supplierform()
        {
            InitializeComponent();
        }

        private void Supplierform_Load(object sender, EventArgs e)
        {

        }
        private void LoadSuppliers()
        {

            var suppliers = supp.GetSuppliers();
            dataGridViewSupplier.DataSource = suppliers;
            dataGridViewSupplier.Columns["SupplierID"].Visible = false;
            dataGridViewSupplier.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridViewSupplier_rowselected(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow selectedRow = dataGridViewSupplier.Rows[e.RowIndex];
            Supplier selectedItem = selectedRow.DataBoundItem as Supplier;
            if (selectedItem == null) return;

            txtname.Text = selectedItem.Name;
            txtcontact.Text = selectedItem.Contact.ToString();
            txtaddress.Text = selectedItem.Address.ToString();
            comboBoxType.SelectedItem = selectedItem.SupplierType.ToString();


            currentitemid = selectedItem.SupplierID;

        }
        private void ClearInputs()
        {
            txtname.Text = "";
            txtcontact.Text = "";
            txtaddress.Text = "";
            comboBoxType.SelectedItem = "";
            currentitemid = -1;
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dataGridViewSupplier.CurrentRow == null) { return; }
           Suppliers selectedUser = dataGridViewSupplier.CurrentRow.DataBoundItem as Suppliers;
            if (selectedUser == null) return;
            selectedUser.Name = txtname.Text;
            selectedUser.Contact = txtcontact.Text;
            selectedUser.Address = txtaddress.Text;
            selectedUser.SupplierType = comboBoxType.SelectedItem.ToString();
            selectedUser.SupplierID = currentitemid;
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
            LoadSuppliers();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewSupplier.CurrentRow == null) return;
            Supplier selecteditems = dataGridViewSupplier.CurrentRow.DataBoundItem as Supplier;
            if (selecteditems == null) return;

            selecteditems.SupplierID = currentitemid;
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
            LoadSuppliers();
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

            }
            else
            {
                MessageBox.Show("Failed to add Supplier. Please try again.");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            List<Suppliers> filteredSuppliers;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                filteredSuppliers = supp.GetSuppliers();
            }
            else
            {
                filteredSuppliers = supp.GetSuppliersbyName(searchText);
            }

            dataGridViewSupplier.DataSource = filteredSuppliers;
        }

        private void btnadd_Click_1(object sender, EventArgs e)
        {
            new pro.UI.AddSupplier().ShowDialog();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            LoadSuppliers();
        }

        private void button7_Click(object sender, EventArgs e)
        {
           customerpayments customerpayments = new customerpayments();
            this.Hide();
            customerpayments.ShowDialog();
            this.Close();

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            customerpayments customerpayments = new customerpayments();
            this.Hide();
            customerpayments.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chicksform m = new chicksform();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            chicksform m = new chicksform();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            feedform f = new feedform();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            feedform f = new feedform();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void dataGridViewSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new pro.UI.Customer().ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pro.UI.Supplier s = new pro.UI.Supplier();
            this.Hide();
            s.ShowDialog();
            this.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pro.UI.Supplier s = new pro.UI.Supplier();
            this.Hide();
            s.ShowDialog();
            this.Close();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }
    }
}
