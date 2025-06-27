using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KIMS;
using MySql.Data.MySqlClient;
using Poultary.BL.Models;

namespace Poultary.DL
{
    public  class chickenDL
    {
        public static List<chicken> getinfo()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
           SELECT 
    c.BatchID AS batch_id,
    c.BatchName AS batch_name,
    c.supplier_id AS supplierid,
    s.Name AS suppliername,
    c.Quantity AS quantity,
    IFNULL(SUM(m.Count), 0) AS diedquantity,
    IFNULL(MAX(fu.SacksUsed), 0) AS sacks_used,
    IFNULL(c.batchprice, 0) + 
    IFNULL(MAX(fu.SacksUsed * (fb.batchprice / NULLIF(fb.QuantitySacks, 0))), 0) AS total_expenses
    FROM chickbatches c
    LEFT JOIN suppliers s ON c.supplier_id = s.SupplierID
    LEFT JOIN chickmortality m ON c.BatchID = m.BatchId
    LEFT JOIN feedusage fu ON fu.batchID = c.BatchID
    LEFT JOIN feedbatches fb ON fu.FeedBatchID = fb.FeedBatchID
    GROUP BY c.BatchID, c.BatchName, c.supplier_id, s.Name, c.Quantity, c.batchprice;

        ";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<chicken> chickens = new List<chicken>();
                        while (reader.Read())
                        {
                            chickens.Add(new chicken
                            (
                                reader.GetInt32("batch_id"),
                                reader.GetString("batch_name"),
                                reader.GetInt32("supplierid"),
                                reader.GetString("suppliername"),
                                reader.GetInt32("quantity"),
                                reader.GetInt32("diedquantity"),
                                reader.GetInt32("sacks_used"),                 // Use GetInt32 if it's INT
                                reader.GetInt32("total_expenses")
                            ));
                        }
                        return chickens;
                    }
                }
            }
        }
        public static List<chicken> getinfos(string searchText)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
            SELECT 
    c.BatchID AS batch_id,
    c.BatchName AS batch_name,
    c.supplier_id AS supplierid,
    s.Name AS suppliername,
    c.Quantity AS quantity,
    COALESCE(SUM(m.Count), 0) AS diedquantity,
    COALESCE(fu.SacksUsed, 0) AS sacks_used,
    COALESCE(c.batchprice, 0) + 
    COALESCE((fu.SacksUsed * (fb.batchprice / NULLIF(fb.QuantitySacks, 0))), 0) AS total_expenses
    FROM chickbatches c
    LEFT JOIN suppliers s ON c.supplier_id = s.SupplierID
    LEFT JOIN chickmortality m ON c.BatchID = m.BatchId
    LEFT JOIN feedusage fu ON fu.batchID = c.BatchID
    LEFT JOIN feedbatches fb ON fu.FeedBatchID = fb.FeedBatchID
    WHERE c.BatchName LIKE @search OR s.Name LIKE @search
    GROUP BY c.BatchID, c.BatchName, c.supplier_id, s.Name, c.Quantity, c.batchprice, fu.SacksUsed, fb.batchprice, fb.QuantitySacks;

        ";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", $"%{searchText}%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<chicken> chickens = new List<chicken>();
                        while (reader.Read())
                        {
                            chickens.Add(new chicken
                            (
                                reader.GetInt32("batch_id"),
                                reader.GetString("batch_name"),
                                reader.GetInt32("supplierid"),
                                reader.GetString("suppliername"),
                                reader.GetInt32("quantity"),
                                reader.GetInt32("diedquantity"),
                                reader.GetInt32("sacks_used"),                 
                                reader.GetInt32("total_expenses")
                            ));
                        }
                        return chickens;
                    }
                }
            }
        }
        public static int GetTotalRemainingChicks()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT 
                SUM(c.Quantity - IFNULL(m.total_died, 0)) AS total_remaining_chicks
            FROM chickbatches c
            LEFT JOIN (
                SELECT batchId, SUM(Count) AS total_died
                FROM chickmortality
                GROUP BY batchId
            ) m ON c.BatchID = m.batchId;
        ";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToInt32(result) : 0;
                }
            }
        }


    }
}
