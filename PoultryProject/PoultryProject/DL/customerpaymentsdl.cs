using System.Data;
using MySql.Data.MySqlClient;
using KIMS;
using System;

namespace PoultryProject.DL
{
    internal class customerpaymentsdl
    {
        public static DataTable LoadCustomerPayments()
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT 
                        b.BillID AS 'Bill ID',
                        c.Name AS 'Name',
                        b.weight AS 'Weight',
                        b.TotalAmount AS 'Total Amount',
                        p.`payed amount` AS 'Paid Amount',
                        p.`Due amount` AS 'Remaining Amount',
                        b.SaleDate AS 'SaleDate'
                    FROM customerbills b
                    JOIN customers c ON b.CustomerID = c.CustomerID
                    JOIN customerpayments p ON p.BillID = b.BillID
                    ORDER BY b.BillID DESC;
                ";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable SearchCustomerPayments(string customerName)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT 
                r.BillID AS 'Bill ID',
                c.Name AS 'Customer Name',
                r.Date AS 'Payment Date',
                r.Payment AS 'Paid Amount'
            FROM customerpricerecord r
            JOIN customers c ON r.Customer_ID = c.CustomerID
            WHERE c.Name LIKE @name
            ORDER BY r.Date DESC;
        ";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", "%" + customerName + "%");

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static double GetTotalDueTocustomer()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT SUM(DueAmount) FROM customerpayments WHERE DueAmount > 0";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        return result != DBNull.Value ? Convert.ToDouble(result) : 0.0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching total supplier dues: " + ex.Message);
                return 0.0;
            }
        }
    }
}
    