using System.Diagnostics;
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
        private const int MINUTE = 60_000;
        private string _fallDuration => $"{300 / Metronome.BPM}s"; // 5 beats in de toekomst kijken

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
                double totalTravelMs = MINUTE / Metronome.BPM * 5 * 0.9; //5% onder triggerlijn door laten als hitbox en fall duration is 5 * bpm s dus * 5
                double triggerEnterMs = totalTravelMs * .9; //hitbox van 10%
                int barIndex = _random.Next(0, _numberOfBars);
                _blockMap[barIndex].Add(_random.Next());
                StateHasChanged();
                _ = TrackTrigger(_blockMap[barIndex].Last(), (int)triggerEnterMs, (int)totalTravelMs); // de gereturnde task wel doen, niet opslaan
            });
        }

        private async Task TrackTrigger(int blockId, int enterDelay, int exitDelay)
        {
            await Task.Delay(enterDelay);
            OnTriggerEntry(blockId);
            await Task.Delay(exitDelay - enterDelay);
            OnTriggerExit(blockId);
        }

        private void OnTriggerEntry(int blockId)
        {
            Debug.WriteLine($"{blockId} IN TRIGGER ZONE LIJN");
        }
        private void OnTriggerExit(int blockId)
        {
            Debug.WriteLine($"{blockId} UIT TRIGGER ZONE LIJN");
        }
    }
}
