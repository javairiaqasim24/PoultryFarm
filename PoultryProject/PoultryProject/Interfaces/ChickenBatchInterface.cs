using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poultary.BL.Models;


namespace Poultary.Interfaces
{
   public interface ChickenBatchInterface
    {
        bool AddChickenBatch(ChickenBatch chickenBatch);
        List<ChickenBatch> GetChickenBatches();
        bool UpdateChickenBatch(ChickenBatch chickenBatch);
        bool DeleteChickenBatch(int batchId);
        List<ChickenBatch> getsearchitem(string word);
    }
}
