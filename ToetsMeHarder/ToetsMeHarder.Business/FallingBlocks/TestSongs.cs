using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToetsMeHarder.Business.LiedjesComponent;

namespace ToetsMeHarder.Business.FallingBlocks
{
    public class TestSongs
    {
        public TestSongs()
        {
        }
        public static List<NoteBlock> CreateSong1()
        {
            List<NoteBlock> song = new List<NoteBlock>();

            song.Add(new NoteBlock(0, 1, KeyValue.e5, 8, 5));
            song.Add(new NoteBlock(0, 1, KeyValue.b4, 4, 6));
            song.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 6.5));
            song.Add(new NoteBlock(0, 1, KeyValue.d5, 8, 7));
            song.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 8));
            song.Add(new NoteBlock(0, 1, KeyValue.b4, 4, 8.5));
            song.Add(new NoteBlock(0, 1, KeyValue.a4, 8, 9));
            song.Add(new NoteBlock(0, 1, KeyValue.a4, 4, 10));
            song.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 10.5));
            song.Add(new NoteBlock(0, 1, KeyValue.e5, 8, 11));
            song.Add(new NoteBlock(0, 1, KeyValue.d5, 4, 12));
            song.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 12.5));
            song.Add(new NoteBlock(0, 1, KeyValue.b4, 8, 13));
            song.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 14));
            song.Add(new NoteBlock(0, 1, KeyValue.d5, 4, 14.5));
            song.Add(new NoteBlock(0, 1, KeyValue.e5, 4, 15));
            song.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 15.5));
            song.Add(new NoteBlock(0, 1, KeyValue.a4, 4, 16));
            song.Add(new NoteBlock(0, 1, KeyValue.a4, 8, 16.5));

            return song;
        }

        public static Dictionary<int, List<NoteBlock>> convertToBlockMap(List<NoteBlock> song, Dictionary<int, List<NoteBlock>> blockMap)
        {
            foreach (int key in blockMap.Keys) 
            {
                var notes = song.Where(q => q.Key == (KeyValue)key);
                foreach (var note in notes) 
                {
                    blockMap[key].Add(note);
                }
            } 
            return blockMap;
        }
    }
}
