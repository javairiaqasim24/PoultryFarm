using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoultryProject.BL.Models;
using PoultryProject.Interfaces;

namespace PoultryProject.BL.Bl
{
    public  class supplierBillBL:IsupplierBill
    {
        public bool addsupplierbills(supplierbill b)
        {
            try
            {
                return PoultryProject.DL.supplierBillDL.addsupplier(b);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding supplier bill: " + ex.Message);
                return false;
            }
        }
        public List<string> getsuppliernames()
        {
            try
            {
                return PoultryProject.DL.supplierBillDL.GetAllSupplierNames();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting supplier names: " + ex.Message);
                return new List<string>();
            }
        }
    }
}
