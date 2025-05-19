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
        public KeyValues Key;
        public int Length { get; set; }
        public Double StartPosition { get; set; }

        List<NoteBlock> _noteBlocks = new List<NoteBlock>();

        public NoteBlock(int id, int songId, KeyValues key, int length, double startPosition)
        {
            Id = id;
            SongId = songId;
            Key = key;
            Length = length;
            StartPosition = startPosition;
        }

        public enum KeyValues /*All sharp values have a 1 on the end because a # is not allowed in an enum*/
        {
            a2,
            a21,
            b2,
            c3,
            c31,
            d3,
            d31,
            e3,
            f3,
            f31,
            g3,
            g31,
            a3,
            a31,
            b3,
            c4,
            c41,
            d4,
            d41,
            e4,
            f4,
            f41,
            g4,
            g41,
            a4,
            a41,
            b4,
            c5,
            c51,
            d5,
            d51,
            e5,
            f5,
            f51,
            g5,
            g51,
            a5,
            a51,
            b5,
            c6

        }

    }
}
