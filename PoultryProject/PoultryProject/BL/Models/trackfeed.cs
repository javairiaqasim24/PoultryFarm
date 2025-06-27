using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoultryProject.BL.Models
{
    public class trackfeed
    {
        public int id { get; set; }            // Assuming a primary key column
        public string feed_batch_name { get; set; }       // Feed batch name (used to find batchid)
        public int batchid { get; set; }
        public int sacksUsed { get; set; }
        public DateTime date { get; set; }
        public string chick_batch_name {  get; set; }
        public int chickid {  get; set; }
        public trackfeed(int id,string name,int batchid,int sacksUsed,DateTime date) 
        {
            this.id = id;
            this.feed_batch_name = name;
            this.batchid = batchid;
            this.sacksUsed=sacksUsed;
            this.date = date; 
        }

        public trackfeed(int id, string name, int batchid, int sacksUsed, DateTime date, int chickid , string chickname)
        {
            this.id = id;
            this.feed_batch_name = name;
            this.batchid = batchid;
            this.sacksUsed = sacksUsed;
            this.date = date;
            this.chickid = chickid;
            this.chick_batch_name = chickname;
        }

        public trackfeed(string batchName, int count, DateTime date)
        {
            feed_batch_name = batchName;
            sacksUsed = count;
            this.date = date;
        }

        public trackfeed(string batchName, int count, DateTime date , string hcickname)
        {
            feed_batch_name = batchName;
            sacksUsed = count;
            this.date = date;
            chick_batch_name = hcickname;
        }

    }

}
