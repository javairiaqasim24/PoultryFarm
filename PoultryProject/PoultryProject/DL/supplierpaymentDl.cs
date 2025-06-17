using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KIMS;
using MySql.Data.MySqlClient;
using PoultryProject.BL.Models;

namespace PoultryProject.DL
{
    public class supplierpaymentDl
    {
        public static bool addsupplierpayment(supplierpayment s)
        {
            s.supplierID = GetCustomerIdByName(s.supplierName);
            if (s.supplierID == -1)
            {
                throw new Exception($"Supplier '{s.supplierName}' not found in database.");
            }

            string query = "INSERT INTO supplierpricerecord (BillID, supplier_id, date, payment) VALUES (@billID, @supplierID, @paymentDate, @amountPaid)";
            var parameters = new MySqlParameter[]
            {
        new MySqlParameter("@billID", s.billID),
        new MySqlParameter("@supplierID", s.supplierID),
        new MySqlParameter("@paymentDate", s.paymentDate),
        new MySqlParameter("@amountPaid", s.amountPaid)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public static int GetCustomerIdByName(string name)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT SupplierID FROM suppliers WHERE Name = @name LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }
        public static List<int> GetBillIdsBySupplierName(string supplierName)
        {
            var billIds = new List<int>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
            SELECT sb.BillID
            FROM supplierbills sb
            INNER JOIN suppliers s ON sb.SupplierID = s.SupplierID
            WHERE s.Name = @name";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", supplierName);

                    using (var reader = cmd.ExecuteReader())
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
        public static List<supplierpayment> GetAllSupplierPriceRecords()
        {
            var records = new List<supplierpayment>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                SELECT 
                    spr.BillID,
                    spr.supplier_id,
                    s.Name AS SupplierName,
                    spr.date,
                    spr.payment
                FROM 
                    supplierpricerecord spr
                JOIN 
                    suppliers s ON spr.supplier_id = s.SupplierID";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var record = new supplierpayment(
                            reader.GetInt32("BillID"),
                            reader.GetString("SupplierName"),
                            reader.GetInt32("supplier_id"),
                            reader.GetDateTime("date"),
                            reader.GetDouble("payment")
                        );

                        records.Add(record);
                    }
                }
            }

            return records;
        }
    }
}
