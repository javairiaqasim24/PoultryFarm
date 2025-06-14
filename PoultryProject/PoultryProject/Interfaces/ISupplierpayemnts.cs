using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoultryProject.BL.Models;

namespace PoultryProject.Interfaces
{
    public interface ISupplierpayemnts
    {
        bool addsupplierpayments(supplierpayment s);
        List<string> getsuppliernames(string text);
        List<supplierpayment> getsupplierpayments();
    }
}
