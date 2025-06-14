using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KIMS;
using MySql.Data.MySqlClient;

namespace PoultryProject.DL
{
    internal class addpaymentdl
    {
        public static int GetCustomerIdByName(string name)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT CustomerID FROM customers WHERE Name = @name LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        public static List<int> GetBillIdsByCustomer(int customerId)
        {
            List<int> billIds = new List<int>();
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT BillID FROM customerpayments WHERE CustomerID = @cid";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cid", customerId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            billIds.Add(reader.GetInt32("BillID"));
                        }
                    }
                }
            }
            return billIds;
        }

        public static decimal GetDueAmount(int customerId, int billId)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT `Due amount` FROM customerpayments WHERE CustomerID = @cid AND BillID = @bid";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cid", customerId);
                    cmd.Parameters.AddWithValue("@bid", billId);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToDecimal(result) : 0;
                }
            }
        }
    }
}