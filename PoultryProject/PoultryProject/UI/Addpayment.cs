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
    public partial class Addpayment : Form
    {
        public static List<string> namesofcustomers = new List<string>();
        public Addpayment()
        {
            InitializeComponent();
            LoadNames();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtdate_ValueChanged(object sender, EventArgs e)
        {
            //date
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                string customerName = txtcustomer.Text.Trim();
                int billId = int.Parse(txtbill.Text.Trim());
                decimal paidNow = decimal.Parse(txtamount.Text.Trim());
                DateTime paymentDate = txtdate.Value;

                int customerId = addpaymentdl.GetCustomerIdByName(customerName);
                if (customerId == -1)
                {
                    MessageBox.Show("Customer not found.");
                    return;
                }

                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // 1. Insert into customerpricerecord
                    string insertRecord = @"INSERT INTO customerpricerecord (customer_id, date, payment, BillID) 
                                    VALUES (@cid, @date, @amt, @bid)";
                    using (MySqlCommand cmd = new MySqlCommand(insertRecord, conn))
                    {
                        cmd.Parameters.AddWithValue("@cid", customerId);
                        cmd.Parameters.AddWithValue("@date", paymentDate);
                        cmd.Parameters.AddWithValue("@amt", paidNow);
                        cmd.Parameters.AddWithValue("@bid", billId);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Update customerpayments (accumulate paid + recalculate due)
                    string updatePayment = @"UPDATE customerpayments 
                                     SET `payed amount` = `payed amount` + @paid,
                                         `Dueamount` = `Dueamount` - @paid
                                     WHERE CustomerID = @cid AND BillID = @bid";

                    using (MySqlCommand cmd = new MySqlCommand(updatePayment, conn))
                    {
                        cmd.Parameters.AddWithValue("@paid", paidNow);
                        cmd.Parameters.AddWithValue("@cid", customerId);
                        cmd.Parameters.AddWithValue("@bid", billId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Payment recorded and updated successfully.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
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

                txtcustomer.DataSource = namesofcustomers;
                txtcustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtcustomer.AutoCompleteSource = AutoCompleteSource.CustomSource;

                var autoCompleteSource = new AutoCompleteStringCollection();
                autoCompleteSource.AddRange(namesofcustomers.ToArray());
                txtcustomer.AutoCompleteCustomSource = autoCompleteSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}\nCheck db_error.log for details");
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txt1_Click(object sender, EventArgs e)
        {

        }

        private void txtamount_TextChanged(object sender, EventArgs e)
        {
            //qunantity
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtcustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbill.Text = "";
            string customerName = txtcustomer.Text.Trim();
            int customerId = addpaymentdl.GetCustomerIdByName(customerName);

            if (customerId == -1)
            {
                MessageBox.Show("Customer not found.");
                return;
            }

            List<int> billIds = addpaymentdl.GetBillIdsByCustomer(customerId);
            txtbill.DataSource = billIds;

            txtremining.Text = ""; // clear remaining when customer changes
        }




        private void txtremining_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbill_SelectedIndexChanged(object sender, EventArgs e)
        {
            string customerName = txtcustomer.Text.Trim();
            int customerId = addpaymentdl.GetCustomerIdByName(customerName);

            if (customerId == -1 || txtbill.SelectedItem == null)
            {
                txtremining.Text = "";
                return;
            }

            int billId = Convert.ToInt32(txtbill.SelectedItem);

            // Validate if the bill exists for this customer
            if (!addpaymentdl.DoesCustomerBillExist(customerId, billId))
            {
                txtremining.Text = "";
                MessageBox.Show("No such bill found for this customer.", "Invalid Bill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal due = addpaymentdl.GetDueAmount(customerId, billId);
            txtremining.Text = due.ToString("F2");
        }


        private void Addpayment_Load(object sender, EventArgs e)
        {

        }
    }
}
