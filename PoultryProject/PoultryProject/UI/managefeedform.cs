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
using Poultary.Interfaces;
using PoultryProject.BL.Models;

namespace Poultary.UI
{
    public partial class managefeedform : Form
    {
        feedinterface ibl = new feedBL();
        ISupplier supplier = new SupplierBL();
        int currentitemid = -1;
        public managefeedform()
        {
            InitializeComponent();
            this.Load += managefeedform_Load;
        }

        private void managefeedform_Load(object sender, EventArgs e)
        {
            loadgrid();
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
            Addfeed addfeed = new Addfeed();
            addfeed.Show();
            this.Close();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {

            if (dataGridView2.CurrentRow == null) { return; }
            trackfeed selectedUser = dataGridView2.CurrentRow.DataBoundItem as trackfeed;
            if (selectedUser == null) return;
            selectedUser.sacksUsed = int.Parse(txtquantity.Text);
            selectedUser.date = txtdate.Value;
            selectedUser.name = txtsupplier.Text;
            selectedUser.id = currentitemid;
            bool result = ibl.updatetrack(selectedUser);
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
            trackfeed selecteditems = dataGridView2.CurrentRow?.DataBoundItem as trackfeed;
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
                if (ibl.deletetrack(currentitemid))
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
            trackfeed selectedItem = selectedRow.DataBoundItem as trackfeed;
            if (selectedItem == null) return;

            txtquantity.Text = selectedItem.sacksUsed.ToString();
            txtdate.Value = selectedItem.date;
            txtsupplier.Text = selectedItem.name;
            currentitemid = selectedItem.id;



        }
    }
}
