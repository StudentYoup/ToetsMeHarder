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
            Songs.Add(CreateSong2());
            Songs.Add(CreateSong3());
            return Songs;
        }

        public static Songs CreateSong1()
        {
            Songs Song = new Songs("Tetris", 80, 22, "A");
            //Eigenlijk BPM van 135

            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e5, 8, 5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.b4, 4, 6));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 6.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d5, 8, 7));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 8));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.b4, 4, 8.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a4, 8, 9));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a4, 4, 10));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 10.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e5, 8, 11));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d5, 4, 12));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 12.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.b4, 8, 13));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 14));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d5, 4, 14.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e5, 4, 15));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c5, 4, 15.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a4, 4, 16));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a4, 8, 16.5));

            return Song;
        }
        public static Songs CreateSong2()
        {
            Songs Song = new Songs("Pirate's of the Caribean", 71, 42, "A");
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a3, 8, 4.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c4, 8, 4.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 5.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 5.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 6.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 6.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.f4, 8, 7.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.f4, 8, 7.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.f4, 8, 8.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 8.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 9.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 9.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 10.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c4, 8, 10.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c4, 8, 11.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 11.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a3, 8, 12.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c4, 8, 12.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 13.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 13.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 14.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 14.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.f4, 8, 15.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.f4, 8, 15.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.f4, 8, 16.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 16.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 17.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 17.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 18.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c4, 8, 18.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 19.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a3, 8, 19.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c4, 8, 20.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 20.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 21.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 21.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.f4, 8, 22.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 22.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 23.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 23.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a4, 8, 24.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a41, 8, 24.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a41, 8, 25.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a4, 8, 25.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 26.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a4, 8, 26.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 27.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 27.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 28.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.f4, 8, 28.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.f4, 8, 29.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 29.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a4, 8, 30.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 30.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 31.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.f4, 8, 31.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 32.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 32.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 33.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c41, 8, 33.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 34.0));
            return Song;
        }



        public static Songs CreateSong3()
        {
            Songs Song = new Songs("Jumping Chords", 120, 38, "C");
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c4, 8, 4.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 4.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 4.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g3, 8, 5.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 5.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 5.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c4, 8, 6.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 6.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 6.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g3, 8, 7.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 7.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 7.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.b3, 8, 8.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d41, 8, 8.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 8.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g3, 8, 9.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d41, 8, 9.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 9.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.b3, 8, 10.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d41, 8, 10.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 10.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g3, 8, 11.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d41, 8, 11.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 11.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a31, 8, 12.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 12.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 12.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g3, 8, 13.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 13.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 13.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a31, 8, 14.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 14.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 14.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g3, 8, 15.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 15.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 15.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a3, 8, 16.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c41, 8, 16.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 16.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e3, 8, 17.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c41, 8, 17.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 17.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a3, 8, 18.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c41, 8, 18.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 18.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e3, 8, 19.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c41, 8, 19.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 19.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.f3, 8, 20.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a3, 8, 20.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c4, 8, 20.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.f3, 8, 21.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a3, 8, 21.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c4, 8, 21.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.f31, 8, 22.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a3, 8, 22.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c4, 8, 22.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.f31, 8, 23.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a3, 8, 23.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c4, 8, 23.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g3, 8, 24.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c4, 8, 24.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 24.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g3, 8, 25.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c4, 8, 25.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 25.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a3, 8, 26.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c41, 8, 26.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 26.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a3, 8, 27.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c41, 8, 27.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 27.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 28.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.f4, 8, 28.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a4, 8, 28.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d4, 8, 29.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.f4, 8, 29.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.a4, 8, 29.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 30.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.b4, 8, 30.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d5, 8, 30.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 31.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.b4, 8, 31.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.d5, 8, 31.5));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.c4, 8, 32.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.e4, 8, 32.0));
            Song.NoteBlocks.Add(new NoteBlock(0, 1, KeyValue.g4, 8, 32.0));
            return Song;
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
