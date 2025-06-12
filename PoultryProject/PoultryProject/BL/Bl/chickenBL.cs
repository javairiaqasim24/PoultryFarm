using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poultary.BL.Models;
using Poultary.Interfaces;

namespace Poultary.BL.Bl
{
    public class chickenBL : chickeninterfaceBL
    {
        public List<chicken> getinfo()
        {
            return DL.chickenDL.getinfo();
        }
    }
}
