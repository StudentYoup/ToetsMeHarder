using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business;
using ToetsMeHarder.Business.FallingBlocks;
using ToetsMeHarder.Business.SongsComponent;
using ToetsMeHarder.PianoGUI.Components.Pages;

namespace ToetsMeHarder.PianoGUI.Components.Layout
{
    public partial class FallingBlocks
    {
        public static FallingBlocks instance;

        [Inject]
        public MetronomeService Metronome { get; set; } = default!;

        private Dictionary<KeyValue, List<NoteBlock>> _blockMap = new();
        private int _numberOfBars = 40;
        private double beats = 0;
        private KeyValue key = new KeyValue();
        private string _fallDuration => $"{300 / Metronome.BPM}s"; // 5 beats in de toekomst kijken
        private readonly KeyValue[] Keys = (KeyValue[])Enum.GetValues(typeof(KeyValue));
        private const int MINUTE = 60_000;
        private Songs? selectedSong = null;
        private Songs? lastSong = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            instance = this;
            SongsManager.Instance.RegisterPropertyChangedFunction(HandleSongChanged);
            // Randomly populate each bar with block IDs (just for demo)
            foreach (KeyValue key in Keys)
            {
                _blockMap[key] = new List<NoteBlock>();
            }
            Metronome.Beat += OnBeat;
        }

        private void HandleSongChanged(object sender, EventArgs e)
        {
            selectedSong = SongsManager.Instance.ChosenSong;
            StateHasChanged();
        }

        private void OnBeat(object? sender, EventArgs e)
        {
            if (selectedSong == null) return;

            InvokeAsync(async () =>
            {
                foreach (NoteBlock block in selectedSong.NoteBlocks.Where(q => q.StartPosition == beats))
                {
                    _blockMap[block.Key].Add(block);
                    double totalTravelMs = MINUTE / Metronome.BPM * 5 * 0.9; //5% onder triggerlijn door laten als hitbox en fall duration is 5 * bpm s dus * 5
                    double triggerEnterMs = totalTravelMs * .9; //hitbox van 10%
                    _ = TrackTrigger(block, (int)triggerEnterMs, (int)totalTravelMs); // de gereturnde task wel doen, niet opslaan
                }
                beats += 0.5;
                StateHasChanged();

                await Task.Delay(MINUTE / Metronome.BPM / 2);

                foreach (NoteBlock block in selectedSong.NoteBlocks.Where(q => q.StartPosition == beats))
                {
                    _blockMap[block.Key].Add(block);
                    double totalTravelMs = MINUTE / Metronome.BPM * 5 * 0.9; //5% onder triggerlijn door laten als hitbox en fall duration is 5 * bpm s dus * 5
                    double triggerEnterMs = totalTravelMs * .9; //hitbox van 10%
                    _ = TrackTrigger(block, (int)triggerEnterMs, (int)totalTravelMs); // de gereturnde task wel doen, niet opslaan
                }
                beats += 0.5;
                StateHasChanged();

                if (beats >= selectedSong.Duration)
                {
                    //popup weergeven einde liedje
                    Home.Instance.resultPopUp = true;
                    Metronome.Stop();
                    lastSong = selectedSong;
                    SongsManager.Instance.ChosenSong = null;
                    beats = 0;

                    StateHasChanged();
                }
            });
        }


        private string CreateCSSClass(string key) //create classes for bars above white and above black keys
        {
            string color = key.Contains("1") ? "black " : "white ";
            return $"vertical-bar-{color}";// bijvoorbeeld: "black-bar" of "white-bar"
        }

        private async Task TrackTrigger(NoteBlock block, int enterDelay, int exitDelay)
        {
            await Task.Delay(enterDelay);
            OnTriggerEntry(block.Id);
            await Task.Delay(exitDelay - enterDelay);
            OnTriggerExit(block.Id);
        }

        private void OnTriggerEntry(int blockId)
        {
            Debug.WriteLine($"{blockId} IN TRIGGER ZONE LIJN");
        }
        private void OnTriggerExit(int blockId)
        {
            Debug.WriteLine($"{blockId} UIT TRIGGER ZONE LIJN");
        }
        public void Retry()
        {
            SongsManager.Instance.ChosenSong = lastSong;
            Home.Instance.resultPopUp = false;
            StateHasChanged();
        }
    }
}
