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


namespace pro.UI
{
    public partial class Supplier : Form
    {
        Isupplier supp = new SupplierBL();
        int currentitemid = -1;
        public Supplier()
        {
            InitializeComponent();
        }

        private void Supplier_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
            dataGridViewSupplier.RowEnter += dataGridViewSupplier_rowselected;
        }
        public void LoadSuppliers()
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
            Suppliers selectedItem = selectedRow.DataBoundItem as Suppliers;
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
            Suppliers selecteditems = dataGridViewSupplier.CurrentRow.DataBoundItem as Suppliers;
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

        private void btnadd_Click(object sender, EventArgs e)
        {
            AddSupplier add=new AddSupplier();
            add.ShowDialog();
        }
    }
}
