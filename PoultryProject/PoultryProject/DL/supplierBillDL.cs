using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoultryProject.BL.Models;
using MySql.Data.MySqlClient;
using KIMS;

namespace PoultryProject.DL
{
    public class supplierBillDL
    {
        public static bool addsupplier(supplierbill b)
        {
            b.supplierid = GetConditionIdByName(b.suppliername);
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO supplierbills (SupplierID, BatchName, Notes, Amount) 
                                     VALUES (@SupplierID, @BatchName, @Notes, @Amount);";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SupplierID", b.supplierid);
                        cmd.Parameters.AddWithValue("@BatchName", b.batchname);
                        cmd.Parameters.AddWithValue("@Notes", b.batchdescription);
                        cmd.Parameters.AddWithValue("@Amount", b.amount);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting supplier bill: " + ex.Message);
                return false;
            }
        }
        private static int GetConditionIdByName(string name)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT SupplierID FROM suppliers WHERE SupplierType = 'chicks' AND Name = @name";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }
        public static List<string> GetAllSupplierNames()
        {
            List<string> names = new List<string>();

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Name FROM suppliers WHERE SupplierType = 'chicks'";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            names.Add(reader.GetString("Name"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching supplier names: " + ex.Message);
            }

            return names;
        }

    }
}
