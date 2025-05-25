using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business;
using ToetsMeHarder.Business.FallingBlocks;
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
        private KeyValue key = new KeyValue();
        private int x = 0;

        private readonly Dictionary<KeyValue, double> _noteFrequencies = new Dictionary<KeyValue, double>
        {
            { KeyValue.a2, 110.00 },
            { KeyValue.a21, 116.54 },
            { KeyValue.b2, 123.47 },
            { KeyValue.c3, 130.81 },
            { KeyValue.c31, 138.59 },
            { KeyValue.d3, 146.83 },
            { KeyValue.d31, 155.56 },
            { KeyValue.e3, 164.81 },
            { KeyValue.f3, 174.61 },
            { KeyValue.f31, 185.00 },
            { KeyValue.g3, 196.00 },
            { KeyValue.g31, 207.65 },
            { KeyValue.a3, 220.00 },
            { KeyValue.a31, 233.08 },
            { KeyValue.b3, 246.94 },
            { KeyValue.c4, 261.63 },
            { KeyValue.c41, 277.18 },
            { KeyValue.d4, 293.66 },
            { KeyValue.d41, 311.13 },
            { KeyValue.e4, 329.63 },
            { KeyValue.f4, 349.23 },
            { KeyValue.f41, 369.99 },
            { KeyValue.g4, 392.00 },
            { KeyValue.g41, 415.30 },
            { KeyValue.a4, 440.00 },
            { KeyValue.a41, 466.16 },
            { KeyValue.b4, 493.88 },
            { KeyValue.c5, 523.25 },
            { KeyValue.c51, 554.37 },
            { KeyValue.d5, 587.33 },
            { KeyValue.d51, 622.25 },
            { KeyValue.e5, 659.26 },
            { KeyValue.f5, 698.46 },
            { KeyValue.f51, 739.99 },
            { KeyValue.g5, 783.99 },
            { KeyValue.g51, 830.61 },
            { KeyValue.a5, 880.00 },
            { KeyValue.a51, 932.33 },
            { KeyValue.b5, 987.77 },
            { KeyValue.c6, 1046.50 }
        };

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

        private string CreateCSSClass(string key)
        {
            string color = key.Contains("1") ? "black " : "white ";
            return $"vertical-bar-{color}";// bijvoorbeeld: "black-bar" of "white-bar"
        }


    }
}
