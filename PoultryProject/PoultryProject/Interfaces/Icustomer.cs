using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pro.BL.Model;

namespace pro.Interface
{
    public interface Icustomer
    {
        bool Add(Customers s);
        bool Update(Customers s, int id);
        bool delete(int id);
        List<Customers> GetCustomers();

        List<Customers> GetCustomersbyName(string text);
    }
}
