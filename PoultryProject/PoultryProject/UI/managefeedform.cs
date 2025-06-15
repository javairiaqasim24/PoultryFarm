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
using PoultryProject.BL.Models;
using pro.Interface;
using pro.BL.Bl;
using PoultryProject.Interfaces;
using PoultryProject.BL.Bl;
using PoultryProject.UI;

namespace Poultary.UI
{
    public partial class managefeedform : Form
    {
        feedinterface ibl = new feedBL();
        Isupplier supplier = new SupplierBL();
        IsupplierBill idl = new supplierBillBL();

        int currentitemid = -1;
        public managefeedform()
        {
            InitializeComponent();
            this.Load += managefeedform_Load;
        }

        private void managefeedform_Load(object sender, EventArgs e)
        {
            loadgrid();
            dataGridView2.RowEnter += dataGridView2_rowselected;
            LoadSupplierComboBox();
        }
        private void loadgrid()
        {
            var list=ibl.getfeed();
            dataGridView2.DataSource = list;
            dataGridView2.Columns["id"].Visible = false; 
            dataGridView2.Columns["supplier_id"].Visible = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            var result=supplier.getsupplierbytype("feed");
            txtsupplier.DataSource = result;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            
            Addfeeed add = new Addfeeed();
            add.Show();
            
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null) { return; }
            feed selectedUser = dataGridView2.CurrentRow.DataBoundItem as feed;
            if (selectedUser == null) return;
            selectedUser.quantity = int.Parse(txtquantity.Text);
            selectedUser.purchasedate = txtdate.Value;
            selectedUser.name = txtname.Text;
            selectedUser.price = int.Parse(txtprice.Text);
            selectedUser.weight = double.Parse(txtweight.Text);
            selectedUser.suppliername = txtsupplier.Text;
            selectedUser.id = currentitemid;
            bool result = ibl.updatefeed(selectedUser);
            if (result == true)
            {
                MessageBox.Show("Item Updated Successfully");
            }
            else
            {
                MessageBox.Show("Item Not Updated");
            }
            loadgrid();

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            feed selecteditems = dataGridView2.CurrentRow?.DataBoundItem as feed;
            if (selecteditems == null) return;

            selecteditems.id = currentitemid;

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete {selecteditems.name}?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                if (ibl.deletefeed(currentitemid))
                {
                    MessageBox.Show("Item Deleted Successfully");
                }
                else
                {
                    MessageBox.Show("Item Not Deleted");
                }

                loadgrid();
            }
        }
        private void dataGridView2_rowselected(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];
            feed selectedItem = selectedRow.DataBoundItem as feed;
            if (selectedItem == null) return;

            txtquantity.Text = selectedItem.quantity.ToString();
            txtdate.Value = selectedItem.purchasedate;
            txtsupplier.Text = selectedItem.suppliername;
            txtname.Text = selectedItem.name;
            txtprice.Text = selectedItem.price.ToString();
            txtweight.Text = selectedItem.weight.ToString();

            currentitemid = selectedItem.id;



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                loadgrid(); // Reload the original full dataset
            }
            else
            {
                var filteredList = DL.feedDl.SearchFeedBatchesWithSupplier(text);
                dataGridView2.DataSource = filteredList;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void txtsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadSupplierComboBox()
        {
            txtsupplier.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtsupplier.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtsupplier.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            txtsupplier.DropDownStyle = ComboBoxStyle.DropDown;
        }
        private void txtsupplier_TextUpdate(object sender, EventArgs e)
        {
            string searchText = txtsupplier.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
                return;

            var matchingNames = idl.getsuppliernames(searchText);

            var autoSource = new AutoCompleteStringCollection();
            autoSource.AddRange(matchingNames.ToArray());
            txtsupplier.AutoCompleteCustomSource = autoSource;

            txtsupplier.DataSource = null;
            txtsupplier.Items.Clear();
            txtsupplier.Items.AddRange(matchingNames.ToArray());

            txtsupplier.DroppedDown = true;
            txtsupplier.SelectionStart = txtsupplier.Text.Length;
            txtsupplier.SelectionLength = 0;
        }

        private void btnedit_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            loadgrid();
        }
    }
}
