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
using Poultary.UI;
using Poultary;
using PoultryProject.UI;


namespace pro.UI
{
    public partial class Supplier : Form
    {
        Isupplier supp = new SupplierBL();
        int currentitemid = -1;
        private bool isPanelCollapsed = false;
        private const int PanelExpandedWidth = 181;
        private const int PanelCollapsedWidth = 55;
        private const int SlideStep = 10;
        private Color hoverColor = Color.FromArgb(40, 55, 71);
        public Supplier()
        {
            InitializeComponent();
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            panel7.Dock = DockStyle.Fill;
            //this.Shown += ViewOrderAd_Shown;
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
            dataGridViewSupplier.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);

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
            try
            {
                if (dataGridViewSupplier.CurrentRow == null)
                {
                    MessageBox.Show("Please select a supplier to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Suppliers selectedUser = dataGridViewSupplier.CurrentRow.DataBoundItem as Suppliers;
                if (selectedUser == null)
                {
                    MessageBox.Show("Selected supplier is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string name = txtname.Text.Trim();
                string contact = txtcontact.Text.Trim();
                string address = txtaddress.Text.Trim();
                string supplierType = comboBoxType.SelectedItem?.ToString();

                // UI-level validation
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Please enter the supplier's name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtname.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(contact))
                {
                    MessageBox.Show("Please enter the supplier's contact.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcontact.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(address))
                {
                    MessageBox.Show("Please enter the supplier's address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtaddress.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(supplierType))
                {
                    MessageBox.Show("Please select the supplier type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBoxType.Focus();
                    return;
                }

                // Set updated values
                selectedUser.Name = name;
                selectedUser.Contact = contact;
                selectedUser.Address = address;
                selectedUser.SupplierType = supplierType;
                selectedUser.SupplierID = currentitemid;

                // Call update method
                bool result = supp.Update(selectedUser, currentitemid);
                if (result)
                {
                    MessageBox.Show("Supplier updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Supplier could not be updated. Please try again.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                LoadSuppliers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewSupplier.CurrentRow == null) return;
            Suppliers selecteditems = dataGridViewSupplier.CurrentRow.DataBoundItem as Suppliers;
            if (selecteditems == null) return;

            selecteditems.SupplierID = currentitemid;
            DialogResult result = MessageBox.Show($"Are you sure you want to delete {selecteditems.Name}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes) // User chose Yes
            {
                if (supp.delete(currentitemid))
                {
                    MessageBox.Show("Item Deleted Successfully");
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Item Not Deleted");
                }
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

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            LoadSuppliers();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pro.UI.Customer customer = new pro.UI.Customer();
            this.Hide();
            customer.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            chicksform chick = new chicksform();
            chick.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            feedform feed = new feedform();
            feed.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pro.UI.Supplier s = new pro.UI.Supplier();
            this.Hide();
            s.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Staff f = new Staff();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            customerpayments customerpayments = new customerpayments();
            this.Hide();
            customerpayments.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            this.Close();
        }

        //private void Supplier_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    Application.Exit();
        //}

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            customerpayments customerpayments = new customerpayments();
            this.Hide();
            customerpayments.ShowDialog();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            chicksform m = new chicksform();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            feedform f = new feedform();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pro.UI.Supplier s = new pro.UI.Supplier();
            this.Hide();
            s.ShowDialog();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            new priccerecordform().ShowDialog();
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            new supplierpayments().ShowDialog();
            this.Close();

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Staff f = new Staff();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }
    }
}
