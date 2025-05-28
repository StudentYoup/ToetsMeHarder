using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using Plugin.Maui.Audio;
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
        private string _fallDuration => $"{300 / Metronome.BPM}s"; // 5 beats in de toekomst kijken
        private readonly KeyValue[] Keys = (KeyValue[])Enum.GetValues(typeof(KeyValue));
        private const int MINUTE = 60_000;
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // Randomly populate each bar with block IDs (just for demo)
            foreach (KeyValue key in Keys)
            {
                _blockMap[key] = new List<NoteBlock>();
            }
            Metronome.Beat += OnBeat;
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
                    double totalTravelMs = MINUTE / Metronome.BPM * 5 * 0.85; //5% onder triggerlijn door laten als hitbox en fall duration is 5 * bpm s dus * 5
                    double triggerEnterMs = totalTravelMs * .9; //hitbox van 10%
                    _ = TrackTrigger(block, (int)triggerEnterMs, (int)totalTravelMs); // de gereturnde task wel doen, niet opslaan
                }
                beats += 0.5;
                StateHasChanged();
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

            }
        }

        private string GetNoteClass(NoteBlock.NoteState state)
        {
            switch (state)
            {
                case NoteBlock.NoteState.Hit:
                    return "hit";
                case NoteBlock.NoteState.Miss:
                    return "miss";
                default:
                    return "";
            }

        }

        public void CheckKeyPress(Dictionary<KeyValue, IAudioPlayer> pressedKeys)
        {
            //voor elke noot in het liedje moeten  checken of hij in de lijst zit
            //&& hij moet op CanBeHit state zijn
            var canBeHit = _blockMap.Values
                        .SelectMany(list => list)
                        .Where(note => note.CurrentState == NoteBlock.NoteState.CanBeHit
                                       && pressedKeys.ContainsKey(note.Key));

            foreach (NoteBlock note in canBeHit)
            {
                note.CurrentState = NoteBlock.NoteState.Hit;
            }

        }

    }
}
