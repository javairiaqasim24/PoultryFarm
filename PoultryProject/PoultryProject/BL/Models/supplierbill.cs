using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoultryProject.BL.Models
{
    public  class supplierbill
    {
        public int id { get; set; }
        public string suppliername { get; set; }
        public int supplierid { get; set; }
        public DateTime date { get; set; }
        public double amount { get; set; }
        public int batchid { get; set; }
        public string batchname { get; set; }
        public string batchdescription { get; set; }
        public supplierbill(int id, string suppliername, int supplierid, DateTime date, double amount, int batchid, string batchname, string batchdescription)
        {
            this.id = id;
            this.suppliername = suppliername;
            this.supplierid = supplierid;
            this.date = date;
            this.amount = amount;
            this.batchid = batchid;
            this.batchname = batchname;
            this.batchdescription = batchdescription;
        }
    }
}
