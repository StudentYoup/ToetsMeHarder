using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business;
using ToetsMeHarder.Business.FallingBlocks;

namespace ToetsMeHarder.PianoGUI.Components.Layout
{
    public partial class FallingBlocks
    {

        [Inject]
        public MetronomeService Metronome { get; set; } = default!;

        private Dictionary<int, List<NoteBlock>> _blockMap = new();
        private int _numberOfBars = 40;
        private double beats = 0;

        private string _fallDuration => $"{300 / Metronome.BPM}s";

        protected override void OnInitialized()
        {
            base.OnInitialized();

            // Randomly populate each bar with block IDs (just for demo)
            for (int i = 0; i < _numberOfBars; i++)
            {
                _blockMap[i] = new List<NoteBlock>();
            }
            Metronome.Beat += OnBeat;
        }

        private void OnBeat(object? sender, EventArgs e)
        {
            InvokeAsync(async () =>
            {
                beats++;
                foreach (NoteBlock block in TestSongs.CreateSong1().Where(q => q.StartPosition == beats)) 
                {
                    _blockMap[(int)block.Key].Add(block);
                }

                await Task.Delay(60_000 / Metronome.BPM / 2);

                beats += 0.5;
                foreach (NoteBlock block in TestSongs.CreateSong1().Where(q => q.StartPosition == beats))
                {
                    _blockMap[(int)block.Key].Add(block);
                }

                StateHasChanged();
            });
        }
    }
}
