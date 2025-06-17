using System;
using System.Collections.Generic;
using PoultryProject.BL.Models;
using PoultryProject.DL;
using PoultryProject.Interfaces;

namespace PoultryProject.BL.Bl
{
    public class supplierpaymentBL : ISupplierpayemnts
    {
        public bool addsupplierpayments(supplierpayment s)
        {
            try
            {
                // Validate input
                if (s == null)
                {
                    Console.WriteLine("Invalid input: supplierpayment object is null.");
                    return false;
                }

              

                if (s.amountPaid <= 0)
                {
                    Console.WriteLine("Amount must be greater than 0.");
                    return false;
                }
                return supplierpaymentDl.addsupplierpayment(s);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding supplier payment: " + ex.Message);
                return false;
            }
        }

        public List<string> getsuppliernames(string text)
        {
            return supplierpaymentDl.GetSupplierNamesLike(text);
        }

        public List<supplierpayment> getsupplierpayments()
        {
            return supplierpaymentDl.GetAllSupplierPriceRecords();
        }
    }
}
