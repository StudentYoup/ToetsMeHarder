using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business;

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
        private double beats = 0;

        private string _fallDuration => $"{300 / Metronome.BPM}s";

        protected override void OnInitialized()
        {
            base.OnInitialized();

            // Randomly populate each bar with block IDs (just for demo)
            for (int i = 0; i < _numberOfBars; i++)
            {
                _blockMap[i] = new List<int> { 1 };
            }
            Metronome.Beat += OnBeat;
            
        }

        private void OnBeat(object? sender, EventArgs e)
        {
            InvokeAsync(() =>
            {
                int barIndex = _random.Next(0, _numberOfBars);
                _blockMap[barIndex].Add(_random.Next());
                beats++;
                StateHasChanged();
            });
        }
    }
}
