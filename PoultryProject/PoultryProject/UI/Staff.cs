using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poultary;
using Poultary.UI;
using PoultryProject.UI;
using pro.BL.Bl;
using pro.BL.Model;
using pro.Interface;

namespace pro.UI
{
    public partial class Staff : Form
    {
        Istaff supp = new StaffBL();
        int currentitemid = -1;
        private bool isPanelCollapsed = true;
        private const int PanelExpandedWidth = 181;
        private const int PanelCollapsedWidth = 50;
        private const int SlideStep = 10;
        private Color hoverColor = Color.FromArgb(40, 55, 71);
        public Staff()
        {
            InitializeComponent();
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            panel7.Dock = DockStyle.Fill;
            this.Shown += ViewOrderAd_Shown;
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
        private void Staff_Load(object sender, EventArgs e)
        {
            LoadStaff();
            dataGridViewStaff.RowEnter += dataGridViewStaff_rowselected;
        }
        public void LoadStaff()
        {

            var staff = supp.GetStaff();
            dataGridViewStaff.DataSource = staff;
            dataGridViewStaff.Columns["StaffID"].Visible = false;
            dataGridViewStaff.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridViewStaff_rowselected(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow selectedRow = dataGridViewStaff.Rows[e.RowIndex];
            Staffs selectedItem = selectedRow.DataBoundItem as Staffs;
            if (selectedItem == null) return;

            txtname.Text = selectedItem.Name;
            txtcontact.Text = selectedItem.Contact.ToString();
            txtCNIC.Text = selectedItem.CNIC.ToString();
            comboBoxType.SelectedItem = selectedItem.Role.ToString();


            currentitemid = selectedItem.StaffID;

        }

        private void dataGridViewStaff_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ClearInputs()
        {
            txtname.Text = "";
            txtcontact.Text = "";
            txtCNIC.Text = "";
            comboBoxType.SelectedItem = "";
            currentitemid = -1;
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dataGridViewStaff.CurrentRow == null) { return; }
            Staffs selectedUser = dataGridViewStaff.CurrentRow.DataBoundItem as Staffs;
            if (selectedUser == null) return;
            selectedUser.Name = txtname.Text;
            selectedUser.Contact = txtcontact.Text;
            selectedUser.CNIC = txtCNIC.Text;
            selectedUser.Role = comboBoxType.SelectedItem.ToString();
            selectedUser.StaffID = currentitemid;
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
            LoadStaff();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewStaff.CurrentRow == null) return;
            Staffs selecteditems = dataGridViewStaff.CurrentRow.DataBoundItem as Staffs;
            if (selecteditems == null) return;

            selecteditems.StaffID = currentitemid;
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
            LoadStaff();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            List<Staffs> filteredSuppliers;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                filteredSuppliers = supp.GetStaff();
            }
            else
            {
                filteredSuppliers = supp.GetStaffbyName(searchText);
            }

            dataGridViewStaff.DataSource = filteredSuppliers;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AddStaff addStaff = new AddStaff(this);
            addStaff.ShowDialog();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            LoadStaff();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            pro.UI.Supplier supplier = new pro.UI.Supplier();
            supplier.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Customer customer = new Customer();
            customer.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Staff staff = new Staff();
            staff.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
           customerpayments customerpayments = new customerpayments();
            this.Hide();
            customerpayments.Show();
            this.Close();
        }

        private void Staff_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            customerpayments customerpayments = new customerpayments();
            this.Hide();
            customerpayments.Show();
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
    }
}
