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
using pro.UI;

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
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            panel7.Dock = DockStyle.Fill;
            this.Shown += ViewOrderAd_Shown;
        }

        private void managefeedform_Load(object sender, EventArgs e)
        {
            loadgrid();
            dataGridView2.RowEnter += dataGridView2_rowselected;
            LoadSupplierComboBox();
        }
        private void loadgrid()
        {
            var list = ibl.getfeed();
            dataGridView2.DataSource = list;
            dataGridView2.Columns["id"].Visible = false;
            dataGridView2.Columns["supplier_id"].Visible = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
         
        }
        private void button8_Click(object sender, EventArgs e)
        {

            Addfeeed add = new Addfeeed();
            add.Show();

        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.CurrentRow == null)
                {
                    MessageBox.Show("Please select a feed item to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedFeed = dataGridView2.CurrentRow.DataBoundItem as feed;
                if (selectedFeed == null)
                {
                    MessageBox.Show("Invalid selection. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Input validation
                string name = txtname.Text.Trim();
                string supplier = txtsupplier.Text.Trim();
                DateTime purchaseDate = txtdate.Value;

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(supplier))
                {
                    MessageBox.Show("Feed name and supplier name cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtquantity.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity greater than 0.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtprice.Text, out int price) || price <= 0)
                {
                    MessageBox.Show("Please enter a valid price greater than 0.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(txtweight.Text, out double weight) || weight <= 0)
                {
                    MessageBox.Show("Please enter a valid weight greater than 0.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Assign values
                selectedFeed.name = name;
                selectedFeed.quantity = quantity;
                selectedFeed.purchasedate = purchaseDate;
                selectedFeed.price = price;
                selectedFeed.weight = weight;
                selectedFeed.suppliername = supplier;
                selectedFeed.id = currentitemid;

                // Perform update
                bool result = ibl.updatefeed(selectedFeed);

                if (result)
                {
                    MessageBox.Show("Feed item updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs(); // Optional
                    loadgrid();
                }
                else
                {
                    MessageBox.Show("Failed to update feed item.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearInputs()
        {
            txtname.Clear();
            txtquantity.Clear();
            txtprice.Clear();
            txtweight.Clear();
            txtsupplier.SelectedIndex=-1;
            txtdate.Value = DateTime.Now;
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
            txtsupplier.AutoCompleteMode = AutoCompleteMode.None; // ✅ No auto-fill
            txtsupplier.AutoCompleteSource = AutoCompleteSource.None; // ✅ We're handling suggestions manually
            txtsupplier.AutoCompleteCustomSource = null; // not needed since using Items directly

            txtsupplier.DropDownStyle = ComboBoxStyle.DropDown; // ✅ Allow typing
        }

        private void txtsupplier_TextUpdate(object sender, EventArgs e)
        {
            string searchText = txtsupplier.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
                return;

            var matchingNames = idl.getsuppliernames(searchText,"Feed");

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void ViewOrderAd_Shown(object sender, EventArgs e)
        {
            flowLayoutPanel1.Width = PanelCollapsedWidth;

            this.PerformLayout();
        }
        private bool isPanelCollapsed = true;
        private const int PanelExpandedWidth = 181;
        private const int PanelCollapsedWidth = 50;
        private const int SlideStep = 25;
        private Color hoverColor = Color.FromArgb(40, 55, 71);
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

        private void button10_Click(object sender, EventArgs e)
        {


        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            new trackfeedform().ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form1().ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new pro.UI.Supplier().ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new pro.UI.Customer().ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            new pro.UI.Staff().ShowDialog();
            this.Close();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            customerpayments c = new customerpayments();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            customerpayments c = new customerpayments();
            this.Hide();
            c.ShowDialog();
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
    }
}
