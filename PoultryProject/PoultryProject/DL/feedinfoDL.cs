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
    public  class feedinfoDL
    {
        public static List<feedinfo> getinfo()
        {
            List<feedinfo> list = new List<feedinfo>();

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    fb.FeedBatchID AS id,
                    fb.BatchName AS name,
                    fb.Quantitysacks AS totalsacks,
                    IFNULL(SUM(fu.SacksUsed), 0) AS sacksUsed,
                    (fb.Quantitysacks - IFNULL(SUM(fu.SacksUsed), 0)) AS remaining,
                    s.Name AS suppliername
                FROM feedbatches fb
                LEFT JOIN feedusage fu ON fb.FeedBatchID = fu.FeedBatchID
                LEFT JOIN suppliers s ON fb.SupplierID = s.SupplierID
                GROUP BY fb.FeedBatchID, fb.BatchName, fb.Quantitysacks, s.Name;";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            feedinfo fi = new feedinfo
                            {
                                id = reader.GetInt32("id"),
                                name = reader.GetString("name"),
                                totalsacks = reader.GetInt32("totalsacks"),
                                sacksUsed = reader.GetInt32("sacksUsed"),
                                remaining = reader.GetInt32("remaining"),
                                suppliername = reader.IsDBNull(reader.GetOrdinal("suppliername")) ? null : reader.GetString("suppliername")
                            };

                            list.Add(fi);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return list;
        }
        public static List<feedinfo> SearchFeedInfo(string searchText)
        {
            List<feedinfo> list = new List<feedinfo>();

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    fb.FeedBatchID AS id,
                    fb.BatchName AS name,
                    fb.Quantitysacks AS totalsacks,
                    IFNULL(SUM(fu.SacksUsed), 0) AS sacksUsed,
                    (fb.Quantitysacks - IFNULL(SUM(fu.SacksUsed), 0)) AS remaining,
                    s.Name AS suppliername
                FROM feedbatches fb
                LEFT JOIN feedusage fu ON fb.FeedBatchID = fu.FeedBatchID
                LEFT JOIN suppliers s ON fb.SupplierID = s.SupplierID
                WHERE fb.BatchName LIKE @search OR s.Name LIKE @search
                GROUP BY fb.FeedBatchID, fb.BatchName, fb.Quantitysacks, s.Name;";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", $"%{searchText}%");

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                feedinfo fi = new feedinfo
                                {
                                    id = reader.GetInt32("id"),
                                    name = reader.GetString("name"),
                                    totalsacks = reader.GetInt32("totalsacks"),
                                    sacksUsed = reader.GetInt32("sacksUsed"),
                                    remaining = reader.GetInt32("remaining"),
                                    suppliername = reader.IsDBNull(reader.GetOrdinal("suppliername")) ? null : reader.GetString("suppliername")
                                };

                                list.Add(fi);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return list;
        }

    }
}
