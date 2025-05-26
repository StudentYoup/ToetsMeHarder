using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business;
using ToetsMeHarder.Business.FallingBlocks;

namespace ToetsMeHarder.PianoGUI.Components.Layout
{
    public partial class FallingBlocks
    {

        [Inject]
        public MetronomeService Metronome { get; set; } = default!;

        private Dictionary<KeyValue, List<NoteBlock>> _blockMap = new();
        private int _numberOfBars = 40;
        private double beats = 0;
        private KeyValue key = new KeyValue();
        private string _fallDuration => $"{300 / Metronome.BPM}s";

        private readonly KeyValue[] Keys = (KeyValue[])Enum.GetValues(typeof(KeyValue));


        protected override void OnInitialized()
        {
            base.OnInitialized();

            // Randomly populate each bar with block IDs (just for demo)
            foreach(KeyValue key in Keys)
            {
                _blockMap[key] = new List<NoteBlock>();
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
                    _blockMap[block.Key].Add(block);
                }

                await Task.Delay(60_000 / Metronome.BPM / 2);

                beats += 0.5;
                foreach (NoteBlock block in TestSongs.CreateSong1().Where(q => q.StartPosition == beats))
                {
                    _blockMap[block.Key].Add(block);
                }

                StateHasChanged();
            });
        }


        private string CreateCSSClass(string key) //create classes for bars above white and above black keys
        {
            string color = key.Contains("1") ? "black " : "white ";
            return $"vertical-bar-{color}";// bijvoorbeeld: "black-bar" of "white-bar"
        }

    }
}
