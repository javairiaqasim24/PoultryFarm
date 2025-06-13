using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pro.BL.Model
{
    public class Suppliers
    {
        public int SupplierID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string SupplierType { get; set; }

        public Suppliers() { }
        public Suppliers(string name, string contact, string address, string supplierType)
        {
            Name = name;
            Contact = contact;
            Address = address;
            this.SupplierType = supplierType;
        }
    }
}
