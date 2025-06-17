using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoultryProject.BL.Models
{
    public class supplierpayment
    {
        public int billID { get; set; }
        public string supplierName { get; set; }
        public int supplierID { get; set; }
        public DateTime paymentDate { get; set; }
        public double amountPaid { get; set; }
        public supplierpayment(int billID, string supplierName, int supplierID, DateTime paymentDate, double amountPaid)
        {
            this.billID = billID;
            this.supplierName = supplierName;
            this.supplierID = supplierID;
            this.paymentDate = paymentDate;
            this.amountPaid = amountPaid;
        }
        public supplierpayment(int billID, string supplierName, DateTime paymentDate, double amountPaid)
        {
            this.billID = billID;
            this.supplierName = supplierName;
            this.paymentDate = paymentDate;
            this.amountPaid = amountPaid;
        }
    }
}
