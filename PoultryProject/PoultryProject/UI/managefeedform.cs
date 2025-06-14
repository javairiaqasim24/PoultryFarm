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

namespace Poultary.UI
{
    public partial class managefeedform : Form
    {
        feedinterface ibl = new feedBL();
        Isupplier supplier = new SupplierBL();
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
            this.Hide();
            //Addfeed addfeed = new Addfeed();
            //addfeed.Show();
            this.Close();
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
    }
}
