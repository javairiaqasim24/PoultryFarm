using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pro.BL.Bl;
using pro.BL.Model;

namespace pro.Interface
{
    public interface Isupplier
    {
        bool Add(Suppliers s);
        bool Update(Suppliers s, int id);
        bool delete(int id);
        List<Suppliers> GetSuppliers();

        List<Suppliers> GetSuppliersbyName(string text);
        List<string> getsupplierbytype(string type);
    }
}
