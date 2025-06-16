using System;
using System.Collections.Generic;
using PoultryProject.BL.Models;
using PoultryProject.DL;
using PoultryProject.Interfaces;

namespace PoultryProject.BL.Bl
{
    public class SupplierpayBL : Isupplierpay
    {
        public bool updatepayments(supplierpay pay)
        {
            try
            {
                // Input validations
                if (pay == null)
                {
                    Console.WriteLine("Invalid input: supplierpay object is null.");
                    return false;
                }

                if (pay.billid <= 0)
                {
                    Console.WriteLine("Invalid Bill ID.");
                    return false;
                }

                if (pay.supplierid <= 0)
                {
                    Console.WriteLine("Invalid Supplier ID.");
                    return false;
                }


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
