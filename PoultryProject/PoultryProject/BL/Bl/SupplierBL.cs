using System;
using System.Collections.Generic;
using pro.BL.Model;
using pro.Interface;

namespace pro.BL.Bl
{
    public class SupplierBL : Isupplier
    {
        public bool Add(Suppliers s)
        {
            if (string.IsNullOrWhiteSpace(s.Name)) return false;
            if (string.IsNullOrWhiteSpace(s.Contact)) return false;
            if (string.IsNullOrWhiteSpace(s.Address)) return false;
            if (string.IsNullOrWhiteSpace(s.SupplierType)) return false;

            return DL.SupplierDL.AddSupplier(s);
        }

        public bool Update(Suppliers s, int id)
        {
            if (string.IsNullOrWhiteSpace(s.Name)) return false;
            if (string.IsNullOrWhiteSpace(s.Contact)) return false;
            if (string.IsNullOrWhiteSpace(s.Address)) return false;
            if (string.IsNullOrWhiteSpace(s.SupplierType)) return false;

            return DL.SupplierDL.UpdateSupplier(s, id);
        }

        public bool delete(int id)
        {
            return DL.SupplierDL.DeleteSupplier(id);
        }

        public List<Suppliers> GetSuppliers()
        {
            return DL.SupplierDL.GetSuppliers();
        }

        public List<Suppliers> GetSuppliersbyName(string name)
        {
            return DL.SupplierDL.SearchSuppliersByName(name);
        }

        public List<string> getsupplierbytype(string type)
        {
            return DL.SupplierDL.GetSupplierNamesByType(type);
        }
    }
}
