using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using KIMS;
using MySql.Data.MySqlClient;
using PoultryProject.BL.Models;

namespace PoultryProject.DL
{
    public class trackfeedDL
    {
       
        public bool addtrack(trackfeed g)
        {
            try
            {
                g.batchid = getbatchid(g.feed_batch_name);
                g.chickid = GetchickbatchIdByName(g.chick_batch_name);

                if (g.batchid == -1)
                {
                    return false;  
                }

                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO feedusage (FeedBatchID, SacksUsed, Date , batchID) VALUES (@BatchID, @SacksUsed, @Date, @Chickid)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BatchID", g.batchid);
                        cmd.Parameters.AddWithValue("@SacksUsed", g.sacksUsed);
                        cmd.Parameters.AddWithValue("@Date", g.date);
                        cmd.Parameters.AddWithValue("@Chickid", g.chickid);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in addtrack: " + ex.Message);
                return false;
            }
        }

        public static int GetchickbatchIdByName(string name)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT BatchID FROM chickbatches WHERE BatchName = @name LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        public List<trackfeed> getAllTracks()
        {
            List<trackfeed> list = new List<trackfeed>();

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT f.UsageID, f.FeedBatchID, fb.batchname, f.SacksUsed, f.Date , cb.BatchName as Chick_Batch_name ,f.batchID
                             FROM feedusage f 
                             JOIN feedBatches fb ON f.FeedBatchID = fb.FeedBatchID
                             join chickbatches cb on f.batchID = cb.BatchID";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                trackfeed tf = new trackfeed(
                                    reader.GetInt32("UsageID"),
                                    reader.GetString("batchname"),
                                    reader.GetInt32("FeedBatchID"),
                                    reader.GetInt32("SacksUsed"),
                                    reader.GetDateTime("Date"),
                                    reader.GetInt32("batchID"),
                                    reader.GetString("Chick_Batch_name")

                                );

                                list.Add(tf);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in getAllTracks: {ex.Message}");
            }

            return list;
        }





        public bool updateTrack(trackfeed g)
        {
            try
            {
                g.batchid = getbatchid(g.feed_batch_name);
                if (g.batchid == -1) return false;

                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE feedusage 
                                     SET FeedBatchID = @batchid, SacksUsed = @sacksUsed, Date = @date 
                                     WHERE UsageID = @id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@batchid", g.batchid);
                        cmd.Parameters.AddWithValue("@sacksUsed", g.sacksUsed);
                        cmd.Parameters.AddWithValue("@date", g.date);
                        cmd.Parameters.AddWithValue("@id", g.id);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in updateTrack: {ex.Message}");
                return false;
            }
        }

        // Delete
        public bool deleteTrack(int id)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM feedusage WHERE UsageID = @id";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in deleteTrack: {ex.Message}");
                return false;
            }
        }

        private static int getbatchid(string name)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT FeedBatchID FROM feedBatches WHERE BatchName = @name";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in getbatchid: {ex.Message}");
                return -1;
            }
        }
        public List<string> GetChickBatchNames(string text)
        {
            List<string> names = new List<string>();
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT BatchName FROM feedbatches WHERE BatchName LIKE @searchText";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@searchText", "%" + text + "%");

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                names.Add(reader.GetString("BatchName"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching chick batch names: {ex.Message}");
            }

            return names;
        }



        public static List<trackfeed> SearchTrackFeeds(string searchText)
        {
            List<trackfeed> list = new List<trackfeed>();

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT f.UsageID, f.FeedBatchID, fb.BatchName, f.SacksUsed, f.Date 
                             FROM feedusage f
                             JOIN feedBatches fb ON f.FeedBatchID = fb.FeedBatchID
                             WHERE fb.BatchName LIKE @search
                                OR DATE_FORMAT(f.Date, '%Y-%m-%d') LIKE @search";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        string safeSearch = $"%{searchText.Replace('/', '-')}%";
                        cmd.Parameters.AddWithValue("@search", safeSearch);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                trackfeed tf = new trackfeed(
                                    reader.GetInt32("UsageID"),
                                    reader.GetString("BatchName"),
                                    reader.GetInt32("FeedBatchID"),
                                    reader.GetInt32("SacksUsed"),
                                    reader.GetDateTime("Date")
                                );

                                list.Add(tf);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching feed usage: {ex.Message}");
            }

            return list;
        }
        public static int GetTodaySacksUsed()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT SUM(SacksUsed) AS total_sacks_used_today 
                             FROM feedusage 
                             WHERE DATE(Date) = CURDATE()";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        return result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetTodaySacksUsed: " + ex.Message);
                return 0;
            }
        }

        public static List<string> namesofbatches = new List<string>();
        public static List<string> Getnames(string columnName)
        {
            List<string> namesofbatches = new List<string>();
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT  BatchName FROM chickbatches WHERE BatchName IS NOT NULL AND BatchName != ''";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string value = reader["BatchName"]?.ToString().Trim();
                            if (!string.IsNullOrEmpty(value)) namesofbatches.Add(value);
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

            return namesofbatches;
        }

    }
}
