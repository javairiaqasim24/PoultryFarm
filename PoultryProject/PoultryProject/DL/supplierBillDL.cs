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
                    string query = @"INSERT INTO supplierbills (SupplierID, BatchName, Notes, TotalAmount,Date) 
                                     VALUES (@SupplierID, @BatchName, @Notes, @Amount,@Date);";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SupplierID", b.supplierid);
                        cmd.Parameters.AddWithValue("@BatchName", b.batchname);
                        cmd.Parameters.AddWithValue("@Notes", b.batchdescription);
                        cmd.Parameters.AddWithValue("@Amount", b.amount);
                        cmd.Parameters.AddWithValue("@Date", b.date);

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
                string query = "SELECT SupplierID FROM suppliers WHERE  Name = @name";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }
        public static List<string> GetSupplierNamesLike(string partialName)
        {
            List<string> names = new List<string>();

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Name FROM suppliers WHERE Name LIKE @name";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", $"%{partialName}%");

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                names.Add(reader.GetString("Name"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error searching supplier names: " + ex.Message);
            }

            return names;
        }

        public static List<supplierbill> GetBillsBySupplierName(string supplierName)
        {
            List<supplierbill> bills = new List<supplierbill>();

            try
            {
                int supplierId = GetConditionIdByName(supplierName);
                if (supplierId == -1)
                {
                    Console.WriteLine("Supplier not found.");
                    return bills;
                }

                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT BillID,BatchName, Notes, TotalAmount, Date 
                             FROM supplierbills 
                             WHERE SupplierID = @SupplierID";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SupplierID", supplierId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                supplierbill bill = new supplierbill(
                                    reader.GetInt32("BillID"),
                                       supplierName,
                                            supplierId,
                                            reader.GetDateTime("Date"),
                                                 reader.GetDouble("TotalAmount"),
                                                 0,
                               reader.GetString("BatchName"),
                             reader.IsDBNull(reader.GetOrdinal("Notes")) ? "" : reader.GetString("Notes")
 );


                                bills.Add(bill);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving supplier bills: " + ex.Message);
            }

            return bills;
        }


    }
}
