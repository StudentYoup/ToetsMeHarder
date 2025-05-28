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
        [Inject]
        public MetronomeService Metronome { get; set; } = default!;

        private Dictionary<KeyValue, List<NoteBlock>> _blockMap = new();
        private int _numberOfBars = 40;
        private double beats = 0;
        private KeyValue key = new KeyValue();
        private string _fallDuration => $"{300 / Metronome.BPM}s"; // 5 beats in de toekomst kijken
        private readonly KeyValue[] Keys = (KeyValue[])Enum.GetValues(typeof(KeyValue));
        private const int MINUTE = 60_000;
        private Songs selectedSong = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
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
            InvokeAsync(async () =>
            {
                foreach (NoteBlock block in TestSongs.CreateSong1().Where(q => q.StartPosition == beats))
                {
                    _blockMap[block.Key].Add(block);
                    double totalTravelMs = MINUTE / Metronome.BPM * 5 * 0.9; //5% onder triggerlijn door laten als hitbox en fall duration is 5 * bpm s dus * 5
                    double triggerEnterMs = totalTravelMs * .9; //hitbox van 10%
                    _ = TrackTrigger(block, (int)triggerEnterMs, (int)totalTravelMs); // de gereturnde task wel doen, niet opslaan
                }
                beats += 0.5;
                StateHasChanged();
                await Task.Delay(MINUTE / Metronome.BPM / 2);

                foreach (NoteBlock block in TestSongs.CreateSong1().Where(q => q.StartPosition == beats))
                {
                    _blockMap[block.Key].Add(block);
                    double totalTravelMs = MINUTE / Metronome.BPM * 5 * 0.9; //5% onder triggerlijn door laten als hitbox en fall duration is 5 * bpm s dus * 5
                    double triggerEnterMs = totalTravelMs * .9; //hitbox van 10%
                    _ = TrackTrigger(block, (int)triggerEnterMs, (int)totalTravelMs); // de gereturnde task wel doen, niet opslaan
                }
                beats += 0.5;
                StateHasChanged();
            });

            if(beats == SongsManager.Instance.ChosenSong.Duration)
            {
                //popup weergeven einde liedje
                Home.Instance.resultPopUp = true;
            }
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
    }
}
