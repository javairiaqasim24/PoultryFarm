using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using Poultary.BL.Models;
using KIMS;

namespace Poultary.DL
{
    public  class chickbatchDL
    {
        public static bool addchickbatch(ChickenBatch c)
        { 
            c.supplier_id = GetConditionIdByName(c.supplierName);
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO chickbatches (BatchName, purchaseDate, batchprice, batchweight, Quantity, supplier_id) " +
                               "VALUES (@BatchName, @purchaseDate, @batchprice, @batchweight, @batchquantity, @supplier_id)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BatchName", c.BatchName);
                    cmd.Parameters.AddWithValue("@purchaseDate", c.purchaseDate);
                    cmd.Parameters.AddWithValue("@batchprice", c.batchprice);
                    cmd.Parameters.AddWithValue("@batchweight", c.batchweight);
                    cmd.Parameters.AddWithValue("@batchquantity", c.batchquantity);
                    cmd.Parameters.AddWithValue("@supplier_id", c.supplier_id);
                    return cmd.ExecuteNonQuery() > 0;
                }

            }
        }
        public static List<ChickenBatch> getchickbatch()
        {
            List<ChickenBatch> batches = new List<ChickenBatch>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT cb.BatchId, cb.BatchName, cb.purchaseDate, cb.batchprice, cb.batchweight, cb.Quantity, s.Name " +
                               "FROM chickbatches cb JOIN suppliers s ON cb.supplier_id = s.SupplierID WHERE s.SupplierType = 'Chick'";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ChickenBatch batch = new ChickenBatch(
                                reader.GetInt32("BatchId"),
                                reader.GetString("BatchName"),
                                reader.GetDateTime("purchaseDate"),
                                reader.GetInt32("batchprice"),
                                reader.GetDouble("batchweight"),
                                reader.GetInt32("Quantity"),
                                reader.GetString("Name"),
                                GetConditionIdByName(reader.GetString("Name"))
                            );
                            batches.Add(batch);
                        }
                    }
                }
            }
            return batches;
        }
        public static bool updatebatch(ChickenBatch c)
        {
            c.supplier_id = GetConditionIdByName(c.supplierName);
            using (var conn=DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "UPDATE chickbatches SET BatchName = @BatchName, purchaseDate = @purchaseDate, batchprice = @batchprice, batchweight = @batchweight, Quantity = @Quantity, supplier_id = @supplier_id WHERE BatchId = @BatchId";
                using (var cmd =new MySqlCommand(query,conn))
                {
                    cmd.Parameters.AddWithValue("@BatchName", c.BatchName);
                    cmd.Parameters.AddWithValue("@purchaseDate", c.purchaseDate);
                    cmd.Parameters.AddWithValue("@batchprice", c.batchprice);
                    cmd.Parameters.AddWithValue("@batchweight", c.batchweight);
                    cmd.Parameters.AddWithValue("@Quantity", c.batchquantity);
                    cmd.Parameters.AddWithValue("@supplier_id", c.supplier_id);
                    cmd.Parameters.AddWithValue("@BatchId", c.BatchId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public static bool deletebatch(int batchId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM chickbatches WHERE BatchId = @BatchId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BatchId", batchId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        private static int GetConditionIdByName(string name)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT SupplierID FROM suppliers WHERE SupplierType = 'Chick' AND Name = @name";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name",name);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }
        public static List<ChickenBatch> SearchBatchesByName(string partialName)
        {
            List<ChickenBatch> batches = new List<ChickenBatch>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT cb.BatchId, cb.BatchName, cb.purchaseDate, cb.batchprice, cb.batchweight, cb.Quantity, s.Name
                         FROM chickbatches cb
                         JOIN suppliers s ON cb.supplier_id = s.SupplierID
                         WHERE s.SupplierType = 'Chick' AND cb.BatchName LIKE @partialName";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@partialName", "%" + partialName + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ChickenBatch batch = new ChickenBatch(
                                reader.GetInt32("BatchId"),
                                reader.GetString("BatchName"),
                                reader.GetDateTime("purchaseDate"),
                                reader.GetInt32("batchprice"),
                                reader.GetDouble("batchweight"),
                                reader.GetInt32("Quantity"),
                                reader.GetString("Name"),
                                GetConditionIdByName(reader.GetString("Name"))
                            );
                            batches.Add(batch);
                        }
                    }
                }
            }

            return batches;
        }

    }
}
