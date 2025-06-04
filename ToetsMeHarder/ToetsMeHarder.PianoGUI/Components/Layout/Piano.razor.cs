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
        public PianoManager pianoManager = new PianoManager();
        [Inject] private IJSRuntime? JSRuntime { get; set; }
        [Inject] private MidiService MidiService { get; set; }
        
        public void HandleKeyDown(KeyboardEventArgs e)
        {
            if (pianoManager._pianoKeys.ContainsKey(e.Key) && !pianoManager._pressedKeys.ContainsKey(pianoManager._pianoKeys[e.Key]))
            {
                var noteKeyVal = pianoManager._pianoKeys[e.Key];

                PlayNote(noteKeyVal);

                JSRuntime.InvokeVoidAsync("setKeyActive", pianoManager._pianoKeys[e.Key].ToString());
            }
        }
        public void HandleKeyUp(KeyboardEventArgs e)
        {
            if (pianoManager._pianoKeys.ContainsKey(e.Key))
            {
                var noteId = pianoManager._pianoKeys[e.Key];
                if (!pianoManager._pressedKeys.ContainsKey(noteId)) return;

                pianoManager.StopNote(noteId);

                JSRuntime.InvokeVoidAsync("setKeyInactive", pianoManager._pianoKeys[e.Key].ToString());
            }
        }

        public void PlayNote(KeyValue key)
        {
            double frequency = pianoManager._noteFrequencies[key];
            if (pianoManager._pressedKeys.ContainsKey(key)) return;
            pianoManager._pressedKeys.Add(key, pianoManager._audioHandler.PlayAudio(new Note(frequency)));

            FallingBlocks.Instance.CheckKeyPress(key);
        }

        public void OnLostFocus()
        {
            foreach (KeyValue key in pianoManager._pressedKeys.Keys.ToList())
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

            if (pianoManager._noteFrequencies.ContainsKey(noteId))
            {
                PlayNote(noteId);

                JSRuntime.InvokeVoidAsync("setKeyActive", noteId.ToString());
            }
        }
        private void OnMidiUp(int status, int note, int velocity)
        {
            var noteId = MidiService.midiNotes[note];

            if (pianoManager._noteFrequencies.ContainsKey(noteId))
            {
                if (!pianoManager._pressedKeys.ContainsKey(noteId)) return;

                pianoManager.StopNote(noteId);

                JSRuntime.InvokeVoidAsync("setKeyInactive", noteId.ToString());

            }
        }
    }
}
