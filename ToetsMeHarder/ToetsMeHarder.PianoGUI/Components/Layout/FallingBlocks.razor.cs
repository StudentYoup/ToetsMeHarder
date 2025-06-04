﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;
using Plugin.Maui.Audio;
using ToetsMeHarder.Business;
using ToetsMeHarder.Business.FallingBlocks;
using ToetsMeHarder.Business.FallingBlocks;
using ToetsMeHarder.Business.SongsComponent;
using ToetsMeHarder.PianoGUI.Components.Pages;

namespace ToetsMeHarder.PianoGUI.Components.Layout
{
    public partial class FallingBlocks
    {
        public FallingBlocksManager fallingBlocksManager = new FallingBlocksManager();
        public static FallingBlocks? instance = null;

        public const int MINUTE = 60_000;

        [Inject] public MetronomeService Metronome { get; set; } = default!;
        private string _fallDuration => $"{5 * (MINUTE / (Metronome.BPM))}ms"; // 5 beats in de toekomst kijken



        protected override void OnInitialized()
        {
            base.OnInitialized();
            instance = this;
            fallingBlocksManager.FillBlockMap();
            SongsManager.Instance.RegisterPropertyChangedFunction(HandleSongChanged);
            Metronome.Beat += OnBeat;
        }


        public void CheckKeyPress(KeyValue pressedKey)
        {
            //voor elke noot in het liedje moeten  checken of hij in de lijst zit
            //&& hij moet op CanBeHit state zijn
            if (!fallingBlocksManager._blockMap.ContainsKey(pressedKey))
            {
                fallingBlocksManager.CurrentResult.Misses++;
                return;
            }
            var canBeHit = fallingBlocksManager._blockMap[pressedKey]
                            .FirstOrDefault(note => note.CurrentState ==
                            NoteState.CanBeHit);
            if (canBeHit != null)
            {
                canBeHit.CurrentState = NoteState.Hit;
                fallingBlocksManager.CurrentResult.Hits++;
                InvokeAsync(async () => StateHasChanged());
            }
            else
            {
                fallingBlocksManager.CurrentResult.Misses++;
            }
        }

        public void Retry()
        {
            SongsManager.Instance.ChosenSong = fallingBlocksManager.lastSong;
            Home.Instance.resultPopUp = false;
            StateHasChanged();
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

        private void OnTriggerExit(NoteBlock noteBlock)
        {
            if (noteBlock.CurrentState != NoteState.Hit)
            {
                noteBlock.CurrentState = NoteState.Miss;
                StateHasChanged();
                fallingBlocksManager.CurrentResult.Misses++;
            }
        }

        private void OnTriggerEntry(NoteBlock noteBlock)
        {
            noteBlock.CurrentState = NoteState.CanBeHit;
            StateHasChanged();
        }

        private void HandleSongChanged(object sender, EventArgs e)
        {
            fallingBlocksManager.selectedSong = SongsManager.Instance.ChosenSong;
            fallingBlocksManager.beats = 0;
            if(fallingBlocksManager.selectedSong != null) fallingBlocksManager.resetBlocks();
            StateHasChanged();
        }

        private void CalculateFallingBlock()
        {
            foreach (NoteBlock block in fallingBlocksManager.selectedSong.NoteBlocks.Where(q => q.StartPosition == fallingBlocksManager.beats))
            {
                fallingBlocksManager._blockMap[block.Key].Add(block);
                double totalTravelMs = MINUTE / Metronome.BPM * 5 * 0.85; // triggerlijn op 85%
                double triggerEnterMs = totalTravelMs * .9; //hitbox van 10%
                _ = TrackTrigger(block, (int)triggerEnterMs, (int)totalTravelMs); // de gereturnde task wel doen, niet opslaan
            }
        }


        private void OnBeat(object? sender, EventArgs e)
        {
            if (fallingBlocksManager.selectedSong == null) return;

            InvokeAsync(async () =>
            {
                CalculateFallingBlock();

                fallingBlocksManager.beats += 0.5;
                StateHasChanged();

                await Task.Delay(MINUTE / Metronome.BPM / 2);

                CalculateFallingBlock();

                fallingBlocksManager.beats += 0.5;
                StateHasChanged();

                if (fallingBlocksManager.beats >= fallingBlocksManager.selectedSong.Duration)
                {
                    //popup weergeven einde liedje
                    Metronome.Stop();
                    fallingBlocksManager.resetBlocks();
                    fallingBlocksManager.fillResults();
                    Home.Instance.resultPopUp = true;
                    fallingBlocksManager.lastSong = fallingBlocksManager.selectedSong;
                    SongsManager.Instance.ChosenSong = null;
                    fallingBlocksManager.beats = 0;
                    fallingBlocksManager.CurrentResult = new();

                    StateHasChanged();
                }
            });
        }
    }
}
