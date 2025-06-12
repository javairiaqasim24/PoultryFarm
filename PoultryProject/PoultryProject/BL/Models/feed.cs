using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultary.BL.Models
{
    public class feed
    {
        public int id { get; set; }
        public string name { get; set; }
        public float weight { get; set; }
        public int quantity { get; set; }
        public int supplier_id { get; set; }
        public string suppliername { get; set; }
        public DateTime purchasedate { get; set; }
        public int price { get; set; }
        public feed(int id, string name, float weight, int quantity, int supplier_id, string suppliername, DateTime purchasedate, int price)
        {
            this.id = id;
            this.name = name;
            this.weight = weight;
            this.quantity = quantity;
            this.supplier_id = supplier_id;
            this.suppliername = suppliername;
            this.purchasedate = purchasedate;
            this.price = price;
        }

        public feed(string name, DateTime purchaseDate, int price, float weight, int quantity, string supplierName)
        {
            this.name = name;
            purchasedate = purchaseDate;
            this.price = price;
            this.weight = weight;
            this.quantity = quantity;
            suppliername = supplierName;
        }
    }
}
