using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poultary.BL.Models;

namespace Poultary.Interfaces
{
    public interface ISupplier
    {
        bool Add(Supplier s);
        bool Update(Supplier s,int id);
        bool delete(int id);
        List<Supplier> GetSuppliers();

        List<Supplier> GetSuppliersbyName(string text);
        
    }
}
