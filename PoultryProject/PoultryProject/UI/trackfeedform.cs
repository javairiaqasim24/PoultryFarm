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
using System.Xml.Linq;
using PoultryProject.BL.Bl;
using PoultryProject.BL.Models;
using Poultary.UI;

namespace PoultryProject.UI
{
    public partial class trackfeedform : Form
    {
        int currentitemid = -1;
        ITrackfeed ibl=new trackfeedBL();
        private bool isPanelCollapsed = true;
        private const int PanelExpandedWidth = 181;
        private const int PanelCollapsedWidth = 50;
        private const int SlideStep = 10;
        private Color hoverColor = Color.FromArgb(40, 55, 71);
        public trackfeedform()
        {
            InitializeComponent();
            timer1.Interval = 10;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            panel7.Dock = DockStyle.Fill;
            this.Shown += ViewOrderAd_Shown;
            timer1.Tick += timer1_Tick;

        }

        private void trackfeedform_Load(object sender, EventArgs e)
        {
            loadgrid();
        }
        private void loadgrid()
        {
            var list =ibl.getAllTracks();
            dataGridView2.DataSource=list;
            dataGridView2.Columns["id"].Visible = false;
            dataGridView2.Columns["batchid"].Visible = false;
            dataGridView2.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            var lists = ibl.GetChickBatchNames();
            txtsupplier.DataSource=lists;
            txtsupplier.SelectedIndex = -1;
            dataGridView2.RowEnter += dataGridView2_rowselected;

        }
        private void ViewOrderAd_Shown(object sender, EventArgs e)
        {
            flowLayoutPanel1.Width = PanelCollapsedWidth;

            this.PerformLayout();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

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

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            loadgrid();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string input = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                dataGridView2.DataSource = ibl.getAllTracks();
            }
            else
            {
                dataGridView2.DataSource = ibl.searchTrackFeeds(input);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            new managefeedform().ShowDialog();
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            new trackfeedform().ShowDialog();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
            new addfeedusage().ShowDialog();
        }
    }
}
