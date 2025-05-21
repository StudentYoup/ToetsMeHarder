using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business;
using ToetsMeHarder.Business.LiedjesComponent;
using ToetsMeHarder.PianoGUI.Pages;

namespace ToetsMeHarder.PianoGUI.Components.Layout
{
    public partial class FallingBlocks
    {

        [Inject]
        public MetronomeService Metronome { get; set; } = default!;
        public List<string> Blocks = new List<string>();
        private Random _random = new();
        private Dictionary<int, List<int>> _blockMap = new();
        private int _numberOfBars = 40;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            // Randomly populate each bar with block IDs (just for demo)
            for (int i = 0; i < _numberOfBars; i++)
            {
                _blockMap[i] = new List<int> { 1 };
            }
            DropBlock();
            
        }

        /*make one new block per second*/
        public async Task DropBlock()
        {
            while (true)
            {
                int barIndex = _random.Next(0, _numberOfBars);
                _blockMap[barIndex].Add(_random.Next());
                StateHasChanged();
                await Task.Delay(60_000 / Metronome.BPM); // Timing = 1min / bpm
            }
        }


    }
}
