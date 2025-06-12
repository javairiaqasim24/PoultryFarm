using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poultary.BL.Models
{
    internal class Customer
    {
        public string name { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }


        public Customer(string name, string contact, string address)
        {
            this.name = name;          
            Contact = contact;
            Address = address;
           

        }
    }
}
