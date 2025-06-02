using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToetsMeHarder.Business.FallingBlocks;

namespace ToetsMeHarder.Business.SongsComponent
{
    public static class TestSongs
    {
        private static List<Songs> Songs = new List<Songs>();
        public static List<Songs> GetTestSongs()
        {
            Songs.Clear();
            Songs.Add(CreateSong1());
            return Songs;
        }

        public static Songs CreateSong1()
        {
            Songs Song1 = new Songs("Tetris", 80, 22, "A");
            //Eigenlijk BPM van 135

            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e5, 8, 5));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.b4, 4, 6));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 6.5));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d5, 8, 7));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 8));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.b4, 4, 8.5));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a4, 8, 9));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a4, 4, 10));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 10.5));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e5, 8, 11));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d5, 4, 12));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 12.5));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.b4, 8, 13));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 14));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d5, 4, 14.5));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e5, 4, 15));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 15.5));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a4, 4, 16));
            Song1.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a4, 8, 16.5));

            return Song1;
        }

        //Unused
        //public static Dictionary<int, List<NoteBlock>> convertToBlockMap(List<NoteBlock> song, Dictionary<int, List<NoteBlock>> blockMap)
        //{
        //    foreach (int key in blockMap.Keys) 
        //    {
        //        var notes = song.Where(q => q.Key == (KeyValue)key);
        //        foreach (var note in notes) 
        //        {
        //            blockMap[key].Add(note);
        //        }
        //    } 
        //    return blockMap;
        //}
    }
}
