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
    public class supplierpaymentBL:ISupplierpayemnts
    {
        public bool addsupplierpayments (supplierpayment s)
        {
            return supplierpaymentDl.addsupplierpayment (s);
        }
        public List<string> getsuppliernames(string text)
        {
            return supplierpaymentDl.GetSupplierNamesLike (text);
        }
        public List<supplierpayment> getsupplierpayments()
        {
            return supplierpaymentDl.GetAllSupplierPriceRecords();
        }
    }
}
