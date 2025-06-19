using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using pro.BL.Model;
using KIMS;

namespace pro.DL
{
    public class CustomerDL
    {
        public static bool AddCustomer(Customers cus)
        {
            try
            {
                string query = @"insert into customers (Name,ContactInfo,Address) values (@name,@contact,@address)";
                var parameterDict = new Dictionary<string, object>
                {
                    {"@name", cus.Name },
                    {"@contact", cus.Contact },
                    {"@address", cus.Address },
                };

                MySqlParameter[] parameters = parameterDict
                    .Select(p => new MySqlParameter(p.Key, p.Value ?? DBNull.Value))
                    .ToArray();

                DatabaseHelper.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (SqlException) { return false; }
            catch (InvalidOperationException) { return false; }
            catch (Exception) { return false; }
        }

        public static bool UpdateCustomers(Customers cus, int CustomerID)
        {
            try
            {
                string query = @"update customers 
                                 set Name = @name, 
                                     ContactInfo = @contact,               
                                     Address = @address                                 
                                 where CustomerID = @id";

                var parameterDict = new Dictionary<string, object>
                {
                    {"@name", cus.Name },
                    {"@contact", cus.Contact },
                    {"@address", cus.Address },
                    {"@id", CustomerID }
                };

                MySqlParameter[] parameters = parameterDict
                    .Select(p => new MySqlParameter(p.Key, p.Value ?? DBNull.Value))
                    .ToArray();

                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (SqlException) { return false; }
            catch (InvalidOperationException) { return false; }
            catch (Exception) { return false; }
        }

        public static bool DeleteCustomer(int id)
        {
            try
            {
                string query = @"delete from customers where CustomerID=@ID";
                var parameterDict = new Dictionary<string, object>
                {
                    {"@ID", id }
                };

                MySqlParameter[] parameters = parameterDict
                    .Select(p => new MySqlParameter(p.Key, p.Value ?? DBNull.Value))
                    .ToArray();

                int rows = DatabaseHelper.ExecuteNonQuery(query, parameters);
                return rows > 0;
            }
            catch (SqlException) { return false; }
            catch (Exception) { return false; }
        }

        public static List<Customers> GetCustomers()
        {
            string query = "SELECT CustomerID, Name, ContactInfo, Address FROM customers";
            List<Customers> cus = new List<Customers>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customers s = new Customers
                        {
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            Name = reader["Name"].ToString(),
                            Contact = reader["ContactInfo"].ToString(),
                            Address = reader["Address"].ToString()
                        };
                        cus.Add(s);
                    }
                }
            }

            return cus;
        }

        public static List<Customers> SearchCustomersByName(string name)
        {
            string query = @"SELECT CustomerID, Name, ContactInfo, Address 
                             FROM customers 
                             WHERE Name LIKE @name";

            List<Customers> cus = new List<Customers>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", "%" + name + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customers s = new Customers
                            {
                                CustomerID = Convert.ToInt32(reader["CustomerID"]),
                                Name = reader["Name"].ToString(),
                                Contact = reader["ContactInfo"].ToString(),
                                Address = reader["Address"].ToString()
                            };
                            cus.Add(s);
                        }
                    }
                }
            }

            return cus;
        }
    }
}
