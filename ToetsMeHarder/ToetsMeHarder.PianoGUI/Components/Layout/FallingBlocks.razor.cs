using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using Plugin.Maui.Audio;
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
        private string _fallDuration => $"{5 * (MINUTE / (Metronome.BPM)) }ms"; // 5 beats in de toekomst kijken
        private readonly KeyValue[] Keys = (KeyValue[])Enum.GetValues(typeof(KeyValue));
        private const int MINUTE = 60_000;
        private Songs? selectedSong = null;
        private Songs? lastSong = null;
        public ToetsMeHarder.Business.Result CurrentResult = new ToetsMeHarder.Business.Result();
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
            CurrentResult.SongTitle = selectedSong.Name;
            CurrentResult.BPM = selectedSong.BPM;
            CurrentResult.TotalNotes = selectedSong.NoteBlocks.Count;
            StateHasChanged();
        }

        private void OnBeat(object? sender, EventArgs e)
        {
            if (selectedSong == null) return;

            InvokeAsync(async () =>
            {
                CalculateFallingBlock();

                beats += 0.5;
                StateHasChanged();

                await Task.Delay(MINUTE / Metronome.BPM / 2);

                CalculateFallingBlock();

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
        private void CalculateFallingBlock()
        {
            foreach (NoteBlock block in selectedSong.NoteBlocks.Where(q => q.StartPosition == beats))
            {
                _blockMap[block.Key].Add(block);
                double totalTravelMs = MINUTE / Metronome.BPM * 5 * 0.85; // triggerlijn op 85%
                double triggerEnterMs = totalTravelMs * .9; //hitbox van 10%
                _ = TrackTrigger(block, (int)triggerEnterMs, (int)totalTravelMs); // de gereturnde task wel doen, niet opslaan
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
            OnTriggerEntry(block);
            StateHasChanged();

            await Task.Delay(exitDelay - enterDelay);
            OnTriggerExit(block);
            StateHasChanged();

        }

        private void OnTriggerEntry(NoteBlock noteBlock)
        {
            noteBlock.CurrentState = NoteBlock.NoteState.CanBeHit;
            StateHasChanged();

        }
        private void OnTriggerExit(NoteBlock noteBlock)
        {
            if (noteBlock.CurrentState != NoteBlock.NoteState.Hit)
            {
                noteBlock.CurrentState = NoteBlock.NoteState.Miss;
                StateHasChanged();
                CurrentResult.Misses++;
            }
        }

        private string GetNoteClass(NoteBlock.NoteState state)
        {
            switch (state)
            {
                case NoteBlock.NoteState.Hit:
                    return "hit";
                case NoteBlock.NoteState.CanBeHit:
                    return "can-be-hit";
                case NoteBlock.NoteState.Miss:
                    return "miss";
                default:
                    return "";
            }
        }

        public void CheckKeyPress(KeyValue pressedKey)
        {
            //voor elke noot in het liedje moeten  checken of hij in de lijst zit
            //&& hij moet op CanBeHit state zijn
            CurrentResult.Misses++; // elke keer dat je toets is het mis, wanneer hij toch de juiste key blijkt te zijn later --
            if (!_blockMap.ContainsKey(pressedKey)) return;
            

            var canBeHit = _blockMap[pressedKey]
                            .FirstOrDefault(note => note.CurrentState ==
                            NoteBlock.NoteState.CanBeHit);
            if (canBeHit != null)
            {
                canBeHit.CurrentState = NoteBlock.NoteState.Hit;
                CurrentResult.Hits++;
                CurrentResult.Misses--;
                StateHasChanged();
            }


        }

        public void Retry()
        {
            SongsManager.Instance.ChosenSong = lastSong;
            Home.Instance.resultPopUp = false;
            StateHasChanged();
            CurrentResult = new();
        }
    }
}
