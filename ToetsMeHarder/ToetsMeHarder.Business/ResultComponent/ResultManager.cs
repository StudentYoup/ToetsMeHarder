using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToetsMeHarder.Business.ResultComponent
{
    public class ResultManager
    {
        public Result Result;
        public int CalculateAccuracy()
        {
            if (Result.Hits == 0 && Result.Misses == 0)
                return 0;
            return Math.Max(0, (int)(((double)Result.Hits / (Result.Hits + Result.Misses)) * 100));
        }
    }
}
