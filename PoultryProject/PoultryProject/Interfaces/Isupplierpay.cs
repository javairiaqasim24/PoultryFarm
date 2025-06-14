using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoultryProject.BL.Models;

namespace PoultryProject.Interfaces
{
    public interface Isupplierpay
    {
        bool updatepayments(supplierpay p);
        List<supplierpay> getsupplierpayments();

    }
}
