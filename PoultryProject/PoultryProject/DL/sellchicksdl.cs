using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KIMS;
using MySql.Data.MySqlClient;


namespace PoultryProject.DL
{
    internal class sellchicksdl
    {
        public static List<string> nameofcustomers = new List<string>();
        public static List<string> Getnames(string columnName)
        {
            List<string> nameofcustomers = new List<string>();
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT  name FROM customers WHERE name IS NOT NULL AND name != ''";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string value = reader["name"]?.ToString().Trim();
                            if (!string.IsNullOrEmpty(value)) nameofcustomers.Add(value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Add diagnostic logging here
                File.WriteAllText("db_error.log", $"[{DateTime.Now}] Error: {ex.Message}\n{ex.StackTrace}");
                throw; // Re-throw to preserve original error behavior
            }

            return nameofcustomers;
        }


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

        public static int GetLatestCustomerBillId(int customerId)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT BillID FROM customerbills WHERE CustomerID = @cid ORDER BY BillID DESC LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cid", customerId);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }


    }
}
