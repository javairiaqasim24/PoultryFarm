using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoultryProject.BL.Models;
using PoultryProject.Interfaces;
using pro.BL.Model;
using Poultary.DL;
using pro.Interface;
using pro.DL;

namespace Poultary.BL.Models
{
    public class CustomerBL : Icustomer
    {
        public bool Add(Customers s)
        {
            if (string.IsNullOrWhiteSpace(s.Name)) return false;
            if (string.IsNullOrWhiteSpace(s.Contact)) return false;
            if (string.IsNullOrWhiteSpace(s.Address)) return false;

            return CustomerDL.AddCustomer(s);
        }

        public bool Update(Customers s, int id)
        {
            if (string.IsNullOrWhiteSpace(s.Name)) return false;
            if (string.IsNullOrWhiteSpace(s.Contact)) return false;
            if (string.IsNullOrWhiteSpace(s.Address)) return false;

            return CustomerDL.UpdateCustomers(s, id);
        }

        public bool delete(int id)
        {
            return CustomerDL.DeleteCustomer(id);
        }

        public List<Customers> GetCustomers()
        {
            return CustomerDL.GetCustomers();
        }

        public List<Customers> GetCustomersbyName(string name)
        {
            return CustomerDL.SearchCustomersByName(name);
        }
    }
}
