using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToetsMeHarder.Business.ResultComponent
{
    public class ResultManager
    {
        public Result _result;
        public int CalculateAccuracy()
        {
            if (_result.Hits == 0 && _result.Misses == 0)
                return 0;
            return Math.Max(0, (int)(((double)_result.Hits / (_result.Hits + _result.Misses)) * 100));
        }
    }
}
