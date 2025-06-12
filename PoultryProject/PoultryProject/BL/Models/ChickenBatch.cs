using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultary.BL.Models
{
    public class ChickenBatch
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public DateTime purchaseDate { get; set; }
        public int batchprice { get; set; }
        public double batchweight { get; set; }
        public int batchquantity { get; set; }
        public string supplierName { get; set; }
        public int supplier_id { get; set; }
        public ChickenBatch(int batchId, string batchName, DateTime purchaseDate, int batchprice, double batchweight, int batchquantity, string supplierName, int supplier_id)
        {
            BatchId = batchId;
            BatchName = batchName;
            this.purchaseDate = purchaseDate;
            this.batchprice = batchprice;
            this.batchweight = batchweight;
            this.batchquantity = batchquantity;
            this.supplierName = supplierName;
            this.supplier_id = supplier_id;
        }
        public ChickenBatch( string batchName, DateTime purchaseDate, int batchprice, double batchweight, int batchquantity, string supplierName)
        {
            BatchName = batchName;
            this.purchaseDate = purchaseDate;
            this.batchprice = batchprice;
            this.batchweight = batchweight;
            this.batchquantity = batchquantity;
            this.supplierName = supplierName;
        }
    }
}
