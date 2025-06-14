using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pro.BL.Model
{
    public class Customers
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
       

        public Customers() { }
        public Customers(string name, string contact, string address)
        {
            Name = name;
            Contact = contact;
            Address = address;
           
        }
    }
}
