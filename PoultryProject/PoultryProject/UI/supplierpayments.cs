using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poultary.BL.Models;
using PoultryProject.BL.Bl;
using PoultryProject.BL.Models;
using PoultryProject.DL;
using PoultryProject.Interfaces;

namespace PoultryProject.UI
{
    public partial class supplierpayments : Form
    {
        int currentitemid = -1;
        IsupplierBill ibl = new supplierBillBL();
        Isupplierpay idl = new SupplierpayBL();

        public supplierpayments()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Addsupplierpay().ShowDialog();
            this.Close();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null) { return; }
            supplierpay selectedUser = dataGridView2.CurrentRow.DataBoundItem as supplierpay;
            if (selectedUser == null) return;
            selectedUser.billid = Convert.ToInt32(txtbill.Text);
            selectedUser.notes = txtnotes.Text;
            selectedUser.payedamount = double.Parse(txtpayed.Text);
            selectedUser.dueamount = double.Parse(txtdue.Text);
            selectedUser.suppliername = txtsupplier.Text;
            selectedUser.id = currentitemid;
            bool result = idl.updatepayments(selectedUser);
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
        private void dataGridView2_rowselected(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];
            supplierpay selectedItem = selectedRow.DataBoundItem as supplierpay;
            if (selectedItem == null) return;
           txtnotes.Text=selectedItem.notes;
            txtbill.Text = selectedItem.billid.ToString();
            txtdue.Text = selectedItem.dueamount.ToString();
            txtpayed.Text = selectedItem.payedamount.ToString();
            txtsupplier.Text = selectedItem.suppliername;

            currentitemid = selectedItem.id;

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

            var matchingNames = ibl.getsuppliernames(searchText);

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

        private void supplierpayments_Load(object sender, EventArgs e)
        {
            loadgrid();
            LoadSupplierComboBox();
            dataGridView2.RowEnter += dataGridView2_rowselected;
        }
        private void loadgrid()
        {
            var list = idl.getsupplierpayments();
            dataGridView2.DataSource=list;
            dataGridView2.Columns["id"].Visible = false;
            dataGridView2.Columns["supplierid"].Visible = false; 

            dataGridView2.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(text))
            {
                var list = SupplierPayDL.SearchSupplierPaymentsByName(text);
                dataGridView2.DataSource = list;
            }
            else
            {
                loadgrid(); 
            }

            dataGridView2.Columns["id"].Visible = false;
            dataGridView2.Columns["supplierid"].Visible = false;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            loadgrid();
        }

        private void txtsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
