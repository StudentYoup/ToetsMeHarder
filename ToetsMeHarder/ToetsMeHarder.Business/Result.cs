using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToetsMeHarder.Business;

namespace ToetsMeHarder.Business
{
    public class Result
    {

       public String Username { get; set; }
        public int SongID { get; set; }
        public int Accuracy { get; set; }
        public int Speed { get; set; }
        public int Total {  get; set; }
        public String SongTitle { get; set; }
    }
}
