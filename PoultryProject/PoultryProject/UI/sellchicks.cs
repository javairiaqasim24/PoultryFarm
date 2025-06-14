using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KIMS;
using MySql.Data.MySqlClient;
using PoultryProject.DL;

namespace PoultryProject.UI
{
    public partial class Sellchicks : Form
    {
        public static List<string> namesofcustomers = new List<string>();

        public Sellchicks()
        {
            InitializeComponent();
            LoadNames();
        }

        private void Sellchicks_Load(object sender, EventArgs e)
        {

        }

        private void LoadNames()
        {
            try
            {


                namesofcustomers = sellchicksdl.Getnames("medicname");

                // Diagnostic output
                Debug.WriteLine($"Loaded {namesofcustomers.Count} drug names");
                if (namesofcustomers.Count == 0)
                {
                    MessageBox.Show("No drug names found in database. Check products table.");
                }

                combocustomer.DataSource = namesofcustomers;
                combocustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                combocustomer.AutoCompleteSource = AutoCompleteSource.CustomSource;

                var autoCompleteSource = new AutoCompleteStringCollection();
                autoCompleteSource.AddRange(namesofcustomers.ToArray());
                combocustomer.AutoCompleteCustomSource = autoCompleteSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}\nCheck db_error.log for details");
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string customerName = combocustomer.Text.Trim();
                decimal weight = decimal.Parse(txtweight.Text);
                decimal totalAmount = decimal.Parse(txtamount.Text);
                decimal paidAmount = decimal.Parse(txtpaidamount.Text);
                DateTime saleDate = txtdate.Value;
                string notes = "Auto-entry"; // optional

                int customerId = sellchicksdl.GetCustomerIdByName(customerName);
                if (customerId == -1)
                {
                    MessageBox.Show("Customer not found.");
                    return;
                }

                int billId = -1;
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // 1. Insert into customerbills
                    string insertBillQuery = @"INSERT INTO customerbills (CustomerID, SaleDate, weight, TotalAmount, Notes)
                                       VALUES (@custId, @date, @weight, @totalAmt, @notes); SELECT LAST_INSERT_ID();";

                    using (MySqlCommand cmd = new MySqlCommand(insertBillQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@custId", customerId);
                        cmd.Parameters.AddWithValue("@date", saleDate);
                        cmd.Parameters.AddWithValue("@weight", weight);
                        cmd.Parameters.AddWithValue("@totalAmt", totalAmount);
                        cmd.Parameters.AddWithValue("@notes", notes);

                        billId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 2. Insert into customerpayments
                    string insertPaymentQuery = @"INSERT INTO customerpayments (CustomerID, BillID, `payed amount`, `Due amount`, Notes)
                                          VALUES (@custId, @billId, @paid, @due, @notes)";

                    using (MySqlCommand cmd = new MySqlCommand(insertPaymentQuery, conn))
                    {
                        decimal dueAmount = totalAmount - paidAmount;
                        cmd.Parameters.AddWithValue("@custId", customerId);
                        cmd.Parameters.AddWithValue("@billId", billId);
                        cmd.Parameters.AddWithValue("@paid", paidAmount);
                        cmd.Parameters.AddWithValue("@due", dueAmount);
                        cmd.Parameters.AddWithValue("@notes", notes);

                        cmd.ExecuteNonQuery();
                    }

                    // 3. Insert into customerpricerecord
                    string insertPriceRecordQuery = @"INSERT INTO customerpricerecord (customer_id, date, payment, BillID)
                                              VALUES (@custId, @date, @paid, @billId)";

                    using (MySqlCommand cmd = new MySqlCommand(insertPriceRecordQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@custId", customerId);
                        cmd.Parameters.AddWithValue("@date", saleDate);
                        cmd.Parameters.AddWithValue("@paid", paidAmount);
                        cmd.Parameters.AddWithValue("@billId", billId);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Sale and payment recorded successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void combocustomer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtcontact_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtweight_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtamount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtpaidamount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtdate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnprint_Click(object sender, EventArgs e)
        {

        }
    }
}
