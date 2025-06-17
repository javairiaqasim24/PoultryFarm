using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using pro.BL.Bl;
using pro.BL.Model;
using KIMS;
namespace pro.DL
{
    internal class SupplierDL
    {
        public static bool AddSupplier(Suppliers supplier)
        {
            try
            {
                string query = @"insert into suppliers (Name,ContactInfo,SupplierType,Address) values (@name,@contact,@type,@address)";
                var parameterDict = new Dictionary<string, object>
            {
                {"@name",supplier.Name },
                { "@contact", supplier.Contact },
                 { "@type", supplier.SupplierType },
                    { "@address", supplier.Address },

            };
                MySqlParameter[] parameters = parameterDict
               .Select(p => new MySqlParameter(p.Key, p.Value ?? DBNull.Value))
               .ToArray();
                DatabaseHelper.ExecuteNonQuery(query, parameters);
                return true;
            }

            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error occurred: " + sqlEx.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            catch (InvalidOperationException invOpEx)
            {
                MessageBox.Show("Invalid operation: " + invOpEx.Message, "Operation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool UpdateSupplier(Suppliers supplier, int supplierID)
        {
            try
            {
                string query = @"update suppliers 
                             set Name = @name, 
                                 ContactInfo = @contact, 
                                  SupplierType = @type,   
                                 Address = @address                                 
                             where SupplierID = @id";
                var parameterDict = new Dictionary<string, object>
            {
                {"@name", supplier.Name },

                {"@contact", supplier.Contact },

                {"@address", supplier.Address },

                {"@type", supplier.SupplierType },
                    {"@id", supplierID }
            };
                MySqlParameter[] parameters = parameterDict
              .Select(p => new MySqlParameter(p.Key, p.Value ?? DBNull.Value))
              .ToArray();
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);

                // Check if update was successful (1 or more rows affected)
                return rowsAffected > 0;
            }



            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error occurred: " + sqlEx.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (InvalidOperationException invOpEx)
            {
                MessageBox.Show("Invalid operation: " + invOpEx.Message, "Operation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        public static bool DeleteSupplier(int id)
        {
            try
            {
                string query = @"delete from suppliers where SupplierID=@ID";
                var parameterDict = new Dictionary<string, object>
                {
                    {"@ID",id }
                };
                MySqlParameter[] parameters = parameterDict
                  .Select(p => new MySqlParameter(p.Key, p.Value ?? DBNull.Value))
                  .ToArray();
                int rows = DatabaseHelper.ExecuteNonQuery(query, parameters);
                return rows > 0;
            }
            catch (SqlException sqlEx)
            {

                MessageBox.Show("Database error occurred: " + sqlEx.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;

        }

        public static List<Suppliers> GetSuppliers()
        {
            string query = "SELECT SupplierID, Name, ContactInfo,SupplierType, Address FROM suppliers";
            List<Suppliers> suppliers = new List<Suppliers>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Suppliers s = new Suppliers
                        {
                            SupplierID = Convert.ToInt32(reader["SupplierID"]),
                            Name = reader["Name"].ToString(),
                            Contact = reader["ContactInfo"].ToString(),
                            SupplierType = reader["SupplierType"].ToString(),
                            Address = reader["Address"].ToString()
                        };
                        suppliers.Add(s);
                    }
                }
            }

            return suppliers;
        }

        public static List<Suppliers> SearchSuppliersByName(string name)
        {
            string query = @"SELECT SupplierID, Name, ContactInfo,SupplierType, Address 
                     FROM suppliers 
                     WHERE Name LIKE @name";

            List<Suppliers> suppliers = new List<Suppliers>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", "%" + name + "%"); // wildcard search

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Suppliers s = new Suppliers
                            {
                                SupplierID = Convert.ToInt32(reader["SupplierID"]),
                                Name = reader["Name"].ToString(),
                                Contact = reader["ContactInfo"].ToString(),
                                SupplierType = reader["SupplierType"].ToString(),
                                Address = reader["Address"].ToString()
                            };
                            suppliers.Add(s);
                        }
                    }
                }
            }

            return suppliers;
        }
        public static List<string> GetSupplierNamesByType(string supplierType)
        {
            List<string> names = new List<string>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT Name FROM suppliers WHERE SupplierType = @type";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@type", supplierType);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            names.Add(reader.GetString("Name"));
                        }
                    }
                }
            }

            return names;
        }
    }
}
