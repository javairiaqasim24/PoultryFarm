using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultary.BL.Models
{
     public class chicken
    {
     public int batch_id { get; set; }
        public string batch_name { get; set; }
        public int supplierid { get; set; }
        public string suppliername { get; set; }
        public int quantity {  get; set; }
        public int diedquantity { get; set; }
        public int SacksUsed { get; set; }             
        public decimal TotalExpenses { get; set; }
        public chicken(int batch_id, string batch_name, int supplierid, string suppliername, int quantity, int diedquantity, int sacksUsed, decimal totalExpenses)
        {
            this.batch_id = batch_id;
            this.batch_name = batch_name;
            this.supplierid = supplierid;
            this.suppliername = suppliername;
            this.quantity = quantity;
            this.diedquantity = diedquantity;
            SacksUsed = sacksUsed;
            TotalExpenses = totalExpenses;
        }
    }
}
