using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToetsMeHarder.Business.FallingBlocks
{
    public class NoteBlock
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public string Key { get; set; }
        public int Length { get; set; }
        public Double StartPosition { get; set; }

        List<NoteBlock> _noteBlocks = new List<NoteBlock>();
    }
}
