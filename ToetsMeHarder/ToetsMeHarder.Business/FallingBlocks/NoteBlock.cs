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
        public KeyValue Key;
        public int Length { get; set; }
        public Double StartPosition { get; set; }
        public enum NoteState
        {
            Falling,
            CanBeHit,
            Hit,
            Miss
        }
        public NoteState CurrentState = NoteState.Falling;

        public NoteBlock(int id, int songId, KeyValue key, int length, double startPosition)
        {
            Id = id;
            SongId = songId;
            Key = key;
            Length = length;
            StartPosition = startPosition;
        }
    }
}
