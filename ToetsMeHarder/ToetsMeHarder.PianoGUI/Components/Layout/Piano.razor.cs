using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business;
using ToetsMeHarder.Business.FallingBlocks;
using ToetsMeHarder.Business.Midi;
using ToetsMeHarder.Business.PianoComponent;


namespace ToetsMeHarder.PianoGUI.Components.Layout
{
    public partial class Piano : ComponentBase
    {
        
        [Inject] private AudioHandler _audioHandler { get; set; }
        public PianoManager pianoManager;
        [Inject] private IJSRuntime? JSRuntime { get; set; }
        [Inject] private MidiService MidiService { get; set; }
        
        public void HandleKeyDown(KeyboardEventArgs e)
        {
            if (pianoManager.PianoKeys.ContainsKey(e.Key) && !pianoManager.PressedKeys.ContainsKey(pianoManager.PianoKeys[e.Key]))
            {
                var noteKeyVal = pianoManager.PianoKeys[e.Key];

                pianoManager.PlayNote(noteKeyVal);
                FallingBlocks.Instance.CheckKeyPress(noteKeyVal);

                JSRuntime.InvokeVoidAsync("setKeyActive", pianoManager.PianoKeys[e.Key].ToString());
            }
        }
        public void HandleKeyUp(KeyboardEventArgs e)
        {
            if (pianoManager.PianoKeys.ContainsKey(e.Key))
            {
                var noteId = pianoManager.PianoKeys[e.Key];
                if (!pianoManager.PressedKeys.ContainsKey(noteId)) return;

                pianoManager.StopNote(noteId);

                JSRuntime.InvokeVoidAsync("setKeyInactive", pianoManager.PianoKeys[e.Key].ToString());
            }
        }

        
        public void OnLostFocus()
        {
            foreach (KeyValue key in pianoManager.PressedKeys.Keys.ToList())
            {
                JSRuntime.InvokeVoidAsync("setKeyInactive", key.ToString());
                pianoManager.StopNote(key);
            }
        }

        private string CreateCSSClass(string key)
        {
            string color = key.Contains("1") ? "black " : "white ";
            string letter = key.Replace("1", "").ToLower()[0].ToString(); // haalt letter iut key
            string addition = key.Contains("1") ? "s" : ""; // s toevoegen bij zwarte keys

            return color + letter + addition; // bijvoorbeeld: "black cs" of "white c"
        }

        //Midi:
        protected override void OnInitialized()
        {
            pianoManager = new PianoManager(_audioHandler);

            MidiService.OnMidiDown += OnMidiDown;
            MidiService.OnMidiUp += OnMidiUp;

            MidiService.OnMidiConnected += name =>
            {
                pianoManager.midiName = name;
                InvokeAsync(StateHasChanged);
            };
            MidiService.OnMidiDisconnected += name =>
            {
                pianoManager.midiName = null;
                InvokeAsync(StateHasChanged);
                OnLostFocus();
            };
            pianoManager.midiName = MidiService.MidiName;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                MidiService.StartUSBWatcher();
            }
        }
        private void OnMidiDown(int status, int note, int velocity)
        {
            var noteId = MidiService.midiNotes[note];

            if (pianoManager.NoteFrequencies.ContainsKey(noteId))
            {
                pianoManager.PlayNote(noteId);
                FallingBlocks.Instance.CheckKeyPress(noteId);

                JSRuntime.InvokeVoidAsync("setKeyActive", noteId.ToString());
            }
        }
        private void OnMidiUp(int status, int note, int velocity)
        {
            var noteId = MidiService.midiNotes[note];

            if (pianoManager.NoteFrequencies.ContainsKey(noteId))
            {
                if (!pianoManager.PressedKeys.ContainsKey(noteId)) return;

                pianoManager.StopNote(noteId);

                JSRuntime.InvokeVoidAsync("setKeyInactive", noteId.ToString());

            }
        }
    }
}
