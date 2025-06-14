using System;
using System.Data;
using System.Windows.Forms;
using PoultryProject.DL;

namespace PoultryProject.UI
{
    public partial class customerpayments : Form
    {
        public customerpayments()
        {
            InitializeComponent();
            LoadPaymentData();

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
                dataGridView2.AutoGenerateColumns = true; // Allow auto-binding for simplicity
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

    }
}
