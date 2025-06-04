using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToetsMeHarder.Business.ResultComponent
{
    public class Result
    {
        public string Username;
        public string SongTitle;
        public int TotalNotes;
        public int BPM;
        public int Hits = 0;
        public int Misses = 0;
        public int Accuracy;
    }
}
