using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Poultary.BL.Bl;
using Poultary.BL.Models;
using Poultary.Interfaces;

namespace Poultary.UI
{
    
    public partial class mortalityform : Form
    {
        int currentitemid = -1;
        mortalityinterface ibl = new mortalityBl();
        private bool isPanelCollapsed = true;
        private const int PanelExpandedWidth = 181;
        private const int PanelCollapsedWidth = 50;
        private const int SlideStep = 10;
        private Color hoverColor = Color.FromArgb(40, 55, 71);
        public mortalityform()
        {
            InitializeComponent();
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            panel7.Dock = DockStyle.Fill;
            this.Shown += ViewOrderAd_Shown;
        }
        private void ViewOrderAd_Shown(object sender, EventArgs e)
        {
            flowLayoutPanel1.Width = PanelCollapsedWidth;

            this.PerformLayout();
        }

        private void mortalityform_Load(object sender, EventArgs e)
        {
            loadgrid();
            dataGridView2.RowEnter+=dataGridView2_rowselected;
        }
        private void loadgrid()
        {
            var list = ibl.getmortality();
            dataGridView2.DataSource = list;
            dataGridView2.Columns["mortalityId"].Visible = false; 
            dataGridView2.Columns["batchId"].Visible = false; 
            dataGridView2.Columns["remainingcount"].Visible=false;
            dataGridView2.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            var lists = ibl.getbatchnames();
            txtsupplier.DataSource = lists;
txtsupplier.DisplayMember = "batchName";
            txtsupplier.ValueMember = "batchId";
            txtsupplier.SelectedIndex = -1; 
        }


        private void dataGridView2_rowselected(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];
            mortality selectedItem = selectedRow.DataBoundItem as mortality;
            if (selectedItem == null) return;

            txtsupplier.Text = selectedItem.batchName;
            txtreason.Text = selectedItem.reason;
            txtdate.Value = selectedItem.date;
            txtquantity.Text = selectedItem.count.ToString();


            currentitemid = selectedItem.mortalityId;

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
        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            new mortalityform().ShowDialog();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Managechicksform().ShowDialog();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new addmortality().Show();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null) { return; }
            mortality selectedUser = dataGridView2.CurrentRow.DataBoundItem as mortality;
            if (selectedUser == null) return;
            selectedUser.batchName = txtsupplier.Text;
            selectedUser.count = int.Parse(txtquantity.Text);
            selectedUser.date = txtdate.Value;
            selectedUser.mortalityId = currentitemid;
            bool result = ibl.updatemortality(selectedUser);
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            mortality selecteditems = dataGridView2.CurrentRow?.DataBoundItem as mortality;
            if (selecteditems == null) return;

            selecteditems.mortalityId = currentitemid;

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete {selecteditems.batchName}?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                if (ibl.deletemortality(currentitemid))
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

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            loadgrid();
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

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Managechicksform m = new Managechicksform();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Managechicksform m = new Managechicksform();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Supplierform m = new Supplierform();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Supplier m = new Supplier();
            this.Hide();
            
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Customer c = new Customer();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Customer c = new Customer();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Staff c = new Staff();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Staff c = new Staff();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }
        private void button9_Click(object sender, EventArgs e)
        {
           

          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string word = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(word))
            {
                // Load the full grid when search box is empty
                dataGridView2.DataSource = ibl.getmortality();
            }
            else
            {
                // Filtered search when text is entered
                var results = ibl.SearchMortality(word);
                dataGridView2.DataSource = results;
            }
        }

        private void txtreason_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
