using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pro.BL.Bl;
using pro.BL.Model;

namespace pro.BL.Bl
{
    public class SupplierBL:Interface.Isupplier
    {
        public bool Add(Suppliers s)
        {
            if (string.IsNullOrWhiteSpace(s.Name))
            {
                MessageBox.Show("Name is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(s.Contact))
            {
                MessageBox.Show("Contact is required.");
                return false;
            }

            if (s.Contact.Length < 11 || s.Contact.Length > 15)
            {
                MessageBox.Show("Contact must be 11 to 15 characters long.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(s.Address))
            {
                MessageBox.Show("Address is required.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(s.SupplierType))
            {
                MessageBox.Show("Please select an option from the ComboBox.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return DL.SupplierDL.AddSupplier(s);
        }

        public bool Update(Suppliers s, int id)
        {
            if (string.IsNullOrWhiteSpace(s.Name))
            {
                MessageBox.Show("Name is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(s.Contact))
            {
                MessageBox.Show("Contact is required.");
                return false;
            }

            if (s.Contact.Length < 11 || s.Contact.Length > 15)
            {
                MessageBox.Show("Contact must be 11 to 15 characters long.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(s.Address))
            {
                MessageBox.Show("Address is required.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(s.SupplierType))
            {
                MessageBox.Show("Please select an option from the ComboBox.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


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

