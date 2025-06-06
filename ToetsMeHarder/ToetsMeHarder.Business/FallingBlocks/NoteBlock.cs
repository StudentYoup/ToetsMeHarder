﻿namespace ToetsMeHarder.Business.FallingBlocks
{
    public class NoteBlock
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public KeyValue Key { get; set; }
        public int Length { get; set; }
        public Double StartPosition { get; set; }

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
