using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Poultary.BL.Models;

namespace Poultary.BL.Bl
{
    public class SupplierBL :Interfaces.ISupplier
    {
        public bool Add(Supplier s)
        {
            if (string.IsNullOrWhiteSpace(s.Name))
                MessageBox.Show("Name is required.");

            if (string.IsNullOrWhiteSpace(s.Contact))
                MessageBox.Show("Contact is required.");

            if (s.Contact.Length < 11 || s.Contact.Length > 15)
                MessageBox.Show("Contact must be 11 to 15 characters long.");

            if (string.IsNullOrWhiteSpace(s.Address))
                MessageBox.Show("Address is required.");

            return DL.SupplierDL.AddSupplier(s);
        }

        public bool Update(Supplier s,int id)
        {
            if (string.IsNullOrWhiteSpace(s.Name))
                MessageBox.Show("Name is required.");

            if (string.IsNullOrWhiteSpace(s.Contact))
                MessageBox.Show("Contact is required.");

            if (s.Contact.Length >11 && s.Contact.Length <12)
                MessageBox.Show("Contact must be 11 characters long.");

            if (string.IsNullOrWhiteSpace(s.Address))
                MessageBox.Show("Address is required.");


            return DL.SupplierDL.UpdateSupplier(s,id);
        }

        public bool delete(int id)
        {
            return DL.SupplierDL.DeleteSupplier(id);
        }

        public List<Supplier> GetSuppliers()
        {
            return DL.SupplierDL.GetSuppliers();
        }

        public List<Supplier> GetSuppliersbyName(string name)
        {
            return DL.SupplierDL.SearchSuppliersByName(name);
        }
        public List<string> getsupplierbytype(string type)
        {
            return DL.SupplierDL.GetSupplierNamesByType(type);
        }
    }
}
