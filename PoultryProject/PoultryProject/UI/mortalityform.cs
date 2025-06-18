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
using MySql.Data.MySqlClient;
using Poultary.BL.Bl;
using Poultary.BL.Models;
using Poultary.DL;
using Poultary.Interfaces;
using PoultryProject.UI;
using pro.UI;

namespace Poultary.UI
{
    
    public partial class mortalityform : Form
    {
        int currentitemid = -1;
        mortalityinterface ibl = new mortalityBl();
        private bool isPanelCollapsed = false;
        private const int PanelExpandedWidth = 181;
        private const int PanelCollapsedWidth = 55;
        private const int SlideStep = 10;
        private Color hoverColor = Color.FromArgb(40, 55, 71);
        public mortalityform()
        {
            InitializeComponent();
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            panel7.Dock = DockStyle.Fill;
            //this.Shown += ViewOrderAd_Shown;
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
            LoadSupplierComboBox();
        }
        private void loadgrid()
        {
            var list = ibl.getmortality();
            dataGridView2.DataSource = list;
            dataGridView2.Columns["mortalityId"].Visible = false; 
            dataGridView2.Columns["batchId"].Visible = false; 
            dataGridView2.Columns["remainingcount"].Visible=false;
            dataGridView2.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);

            LoadSupplierComboBox();
        }
        private void LoadSupplierComboBox()
        {
            txtsupplier.AutoCompleteMode = AutoCompleteMode.None;
            txtsupplier.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtsupplier.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            txtsupplier.DropDownStyle = ComboBoxStyle.DropDown;
        }
        private void txtsupplier_TextUpdate(object sender, EventArgs e)
        {
            string searchText = txtsupplier.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
                return;

            var matchingNames = mortalityDL.GetBatchNames(searchText);

            var autoSource = new AutoCompleteStringCollection();
            autoSource.AddRange(matchingNames.ToArray());
            txtsupplier.AutoCompleteCustomSource = autoSource;

            txtsupplier.DataSource = null;
            txtsupplier.Items.Clear();
            txtsupplier.Items.AddRange(matchingNames.ToArray());

            txtsupplier.DroppedDown = true;
            txtsupplier.SelectionStart = txtsupplier.Text.Length;
            txtsupplier.SelectionLength = 0;
            Cursor.Current = Cursors.Default;
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
            try
            {
                if (dataGridView2.CurrentRow == null)
                {
                    MessageBox.Show("Please select a record to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedMortality = dataGridView2.CurrentRow.DataBoundItem as mortality;
                if (selectedMortality == null)
                {
                    MessageBox.Show("Invalid selection. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string batchName = txtsupplier.Text.Trim();
                if (string.IsNullOrWhiteSpace(batchName))
                {
                    MessageBox.Show("Batch name cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtquantity.Text, out int count) || count <= 0)
                {
                    MessageBox.Show("Please enter a valid mortality count (greater than 0).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string reason = txtreason.Text.Trim();
                if (string.IsNullOrWhiteSpace(reason))
                {
                    MessageBox.Show("reason cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DateTime date = txtdate.Value;

                selectedMortality.batchName = batchName;
                selectedMortality.count = count;
                selectedMortality.date = date;
                selectedMortality.reason = reason;
                selectedMortality.mortalityId = currentitemid;

                bool result = ibl.updatemortality(selectedMortality);

                if (result)
                {
                    MessageBox.Show("Mortality record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadgrid();
                }
                else
                {
                    MessageBox.Show("Failed to update mortality record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Batch name already exists. Please enter a unique name.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            pro.UI.Supplier s = new pro.UI.Supplier();
            this.Hide();
            s.ShowDialog();
            this.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pro.UI.Supplier m = new pro.UI.Supplier();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pro.UI.Customer c = new pro.UI.Customer();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            pro.UI.Customer c = new pro.UI.Customer();
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

        private void timer1_Tick_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new feedform().ShowDialog();
            this.Close();   
        }

        private void button7_Click(object sender, EventArgs e)
        {
            customerpayments c = new customerpayments();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            customerpayments c = new customerpayments();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            feedform f = new feedform();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void txtsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
