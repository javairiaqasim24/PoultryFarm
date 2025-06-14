using System;
using System.Collections.Generic;
using KIMS;
using MySql.Data.MySqlClient;
using PoultryProject.BL.Models;

namespace PoultryProject.DL
{
    public class SupplierPayDL
    {
        public static List<supplierpay> getsupplierpayments()
        {
            var list = new List<supplierpay>();

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT spr.BillID, s.Name AS SupplierName, spr.PayedAmount, spr.DueAmount,
                                                spr.SupplierID, spr.PaymentID, spr.Notes 
                                         FROM supplierpayments spr 
                                         JOIN suppliers s ON spr.SupplierID = s.SupplierID";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var payment = new supplierpay(
                                reader.GetInt32("PaymentID"),
                                reader.GetInt32("BillID"),
                                reader.GetString("SupplierName"),
                                reader.GetInt32("SupplierID"),
                                reader.GetDouble("PayedAmount"),
                                reader.GetDouble("DueAmount"),
                                reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString("Notes")
                            );

                            list.Add(payment);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching supplier payments: " + ex.Message);
            }

            return list;
        }

        public static bool UpdateFullSupplierPayment(supplierpay payment)
        {
            payment.supplierid=GetCustomerIdByName(payment.suppliername);
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = @"UPDATE supplierpayments 
                                 SET BillID = @BillID, 
                                     SupplierID = @SupplierID, 
                                     PayedAmount = @PayedAmount, 
                                     DueAmount = @DueAmount, 
                                     Notes = @Notes 
                                 WHERE PaymentID = @PaymentID";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BillID", payment.billid);
                        cmd.Parameters.AddWithValue("@SupplierID", payment.supplierid);
                        cmd.Parameters.AddWithValue("@PayedAmount", payment.payedamount);
                        cmd.Parameters.AddWithValue("@DueAmount", payment.dueamount);
                        cmd.Parameters.AddWithValue("@Notes", payment.notes ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PaymentID", payment.id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating full supplier payment: " + ex.Message);
                return false;
            }
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
        public static List<supplierpay> SearchSupplierPaymentsByName(string partialName)
        {
            var list = new List<supplierpay>();

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT spr.BillID, s.Name AS SupplierName, spr.PayedAmount, spr.DueAmount,
                                     spr.SupplierID, spr.PaymentID, spr.Notes 
                              FROM supplierpayments spr 
                              JOIN suppliers s ON spr.SupplierID = s.SupplierID
                              WHERE s.Name LIKE @partialName";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@partialName", $"%{partialName}%");

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var payment = new supplierpay(
                                    reader.GetInt32("PaymentID"),
                                    reader.GetInt32("BillID"),
                                    reader.GetString("SupplierName"),
                                    reader.GetInt32("SupplierID"),
                                    reader.GetDouble("PayedAmount"),
                                    reader.GetDouble("DueAmount"),
                                    reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString("Notes")
                                );

                                list.Add(payment);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error searching supplier payments: " + ex.Message);
            }

            return list;
        }

    }
}
