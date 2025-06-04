using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToetsMeHarder.Business.SongsComponent;
using ToetsMeHarder.Business;
using ToetsMeHarder.Business.ResultComponent;

namespace ToetsMeHarder.Business.FallingBlocks
{
    public class FallingBlocksManager
    {
        public MetronomeService Metronome { get; set; } = default!;
        public Result CurrentResult = new();
        public Dictionary<KeyValue, List<NoteBlock>> BlockMap = new();
        public double beats = 0;
        public readonly KeyValue[] Keys = (KeyValue[])Enum.GetValues(typeof(KeyValue));
        public Songs? selectedSong = null;
        public Songs? lastSong = null;
        public void resetBlocks()
        {
            foreach (NoteBlock block in selectedSong.NoteBlocks)
            {
                block.CurrentState = NoteState.Falling;
            }

            foreach (var key in BlockMap.Keys)
            {
                BlockMap[key].Clear();
            }
        }
        public string GetNoteClass(NoteState state)
        {
            switch (state)
            {
                case NoteState.Hit:
                    return "hit";
                case NoteState.CanBeHit:
                    return "can-be-hit";
                case NoteState.Miss:
                    return "miss";
                default:
                    return "";
            }
        }
        public void fillResults()
        {
            CurrentResult.SongTitle = selectedSong.Name;
            CurrentResult.BPM = selectedSong.BPM;
            CurrentResult.TotalNotes = selectedSong.NoteBlocks.Count;
        }

        public void FillBlockMap()
        {
            foreach (KeyValue key in Keys)
            {
                BlockMap[key] = new List<NoteBlock>();
            }
        }

    }
}
