using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoultryProject.BL.Models;
using PoultryProject.Interfaces;
using System.Windows.Forms;
using pro.BL.Models;

namespace Poultary.BL.Models
{
    public class CustomerBL:Interface.Icustomer
    {
        public bool Add(Customers s)
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

            return DL.CustomerDL.AddCustomer(s);
        }

        public bool Update(Customers s, int id)
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


            return DL.CustomerDL.UpdateCustomers(s, id);
        }

        public bool delete(int id)
        {
            return DL.CustomerDL.DeleteCustomer(id);
        }

        public List<Customers> GetCustomers()
        {
            return DL.CustomerDL.GetCustomers();
        }

        public List<Customers> GetCustomersbyName(string name)
        {
            return DL.CustomerDL.SearchCustomersByName(name);
        }
    }
}
