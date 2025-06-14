using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoultryProject.BL.Models;
using PoultryProject.DL;
using PoultryProject.Interfaces;

namespace PoultryProject.BL.Bl
{
    public class SupplierpayBL:Isupplierpay
    {
        public bool updatepayments(supplierpay pay)
        {
            try
            {
                return SupplierPayDL.UpdateFullSupplierPayment(pay);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating supplier payment: " + ex.Message);
                return false;
            }
        }
        public List<supplierpay> getsupplierpayments()
        {
            try
            {
                return SupplierPayDL.getsupplierpayments();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching supplier payments: " + ex.Message);
                return new List<supplierpay>();
            }
        }
    }
}
