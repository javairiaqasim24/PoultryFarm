using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Poultary;
using Poultary.UI;
using PoultryProject.DL;
using pro.BL.Model;
using pro.UI;

namespace PoultryProject.UI
{
    public partial class customerpayments : Form
    {
        private bool isPanelCollapsed = true;
        private const int PanelExpandedWidth = 181;
        private const int PanelCollapsedWidth = 50;
        private const int SlideStep = 20;
        public customerpayments()
        {
            InitializeComponent();
            LoadPaymentData();
            dataGridView2.ScrollBars = ScrollBars.Both;
            timer1.Interval = 5;
            timer1.Tick += timer1_Tick;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            panel7.Dock = DockStyle.Fill;
            this.Shown += ViewOrderAd_Shown;

        }

        private void LoadPaymentData()
        {
            try
            {
                dataGridView2.AutoGenerateColumns = false;

                // Clear existing columns first (after search)
                dataGridView2.Columns.Clear();

                // Define columns manually
                dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Bill ID",
                    DataPropertyName = "Bill ID"
                });
                dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Name",
                    DataPropertyName = "Name"
                });
                dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Weight",
                    DataPropertyName = "Weight"
                });
                dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Total Amount",
                    DataPropertyName = "Total Amount"
                });
                dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Paid Amount",
                    DataPropertyName = "Paid Amount"
                });
                dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Remaining Amount",
                    DataPropertyName = "Remaining Amount"
                });

                DataTable dt = customerpaymentsdl.LoadCustomerPayments();
                dataGridView2.DataSource = dt;

                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load customer payments: " + ex.Message);
            }
        }


        private void FormatGrid()
        {
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            dataGridView2.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10);
            dataGridView2.ReadOnly = true;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: add action for clicking a row
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Addpayment a = new Addpayment();
            a.Show();
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            txtsrch.Text = "";

           

            LoadPaymentData(); // Re-load the original full dataset
        }



        private void txtsrch_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnsrch_Click(object sender, EventArgs e)
        {
            string name = txtsrch.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a customer name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                dataGridView2.AutoGenerateColumns = true; 
                DataTable dt = customerpaymentsdl.SearchCustomerPayments(name);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No records found for this customer.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dataGridView2.DataSource = dt;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to perform search: " + ex.Message);
            }
        }

        private void customerpayments_Load(object sender, EventArgs e)
        {

            LoadPaymentData();
            FormatGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chicksform c = new chicksform();
            this.Hide();
            c.ShowDialog();
            this.Close();
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            chicksform c = new chicksform();
            this.Hide();
            c.ShowDialog();
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

        private void button4_Click(object sender, EventArgs e)
        {
            pro.UI.Supplier s = new Supplier();
            this.Hide();
            s.ShowDialog();
            this.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pro.UI.Supplier s = new Supplier();
            this.Hide();
            s.ShowDialog();
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
            Staff s = new Staff();
            this.Hide();
            s.ShowDialog();
            this.Close();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Staff s = new Staff();
            this.Hide();
            s.ShowDialog();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            customerpayments c = new customerpayments();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            customerpayments c = new customerpayments();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            supplierpayments c = new supplierpayments();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
