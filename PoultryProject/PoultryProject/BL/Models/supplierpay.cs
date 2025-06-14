using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoultryProject.BL.Models
{
    public  class supplierpay
    {
        public int id {  get; set; }
        public int billid {  get; set; }
        public string suppliername { get; set; }
        public int supplierid { get; set; }
        public double payedamount { get; set; }
        public double dueamount { get; set; }
        public string notes { get; set; }
        public supplierpay(int id, int billid, string suppliername, int supplierid, double payedamount, double dueamount, string notes)
        {
            this.id = id;
            this.billid = billid;
            this.suppliername = suppliername;
            this.supplierid = supplierid;
            this.payedamount = payedamount;
            this.dueamount = dueamount;
            this.notes = notes;
        }
    }
}
