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
        [Inject] private AudioHandler _audioHandler { get; set; }
        [Inject] private IJSRuntime? JSRuntime { get; set; }
        [Inject] private MidiService MidiService { get; set; }
        
        public void HandleKeyDown(KeyboardEventArgs e)
        {
            if (pianoManager.PianoKeys.ContainsKey(e.Key) && !pianoManager.PressedKeys.ContainsKey(pianoManager.PianoKeys[e.Key]))
            {
                var noteKeyVal = pianoManager.PianoKeys[e.Key];

                PlayNote(noteKeyVal);

                JSRuntime.InvokeVoidAsync("setKeyActive", pianoManager.PianoKeys[e.Key].ToString());
            }
        }
        public void HandleKeyUp(KeyboardEventArgs e)
        {
            if (pianoManager.PianoKeys.ContainsKey(e.Key))
            {
                var noteId = pianoManager.PianoKeys[e.Key];
                if (!pianoManager.PressedKeys.ContainsKey(noteId)) return;

                StopNote(noteId);

                JSRuntime.InvokeVoidAsync("setKeyInactive", pianoManager.PianoKeys[e.Key].ToString());
            }
        }

        public void PlayNote(KeyValue key)
        {
            double frequency = pianoManager.NoteFrequencies[key];
            if (pianoManager.PressedKeys.ContainsKey(key)) return;
            pianoManager.PressedKeys.Add(key, _audioHandler.PlayAudio(new Note(frequency)));

            FallingBlocks.Instance.CheckKeyPress(key);
        }
        public void StopNote(KeyValue key)
        {
            if (!pianoManager.PressedKeys.ContainsKey(key)) return;
            _audioHandler.StopAudio(pianoManager.PressedKeys[key]);
            pianoManager.PressedKeys.Remove(key);
        }
        public void OnLostFocus()
        {
            foreach (KeyValue key in pianoManager.PressedKeys.Keys.ToList())
            {
                JSRuntime.InvokeVoidAsync("setKeyInactive", key.ToString());
                StopNote(key);
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

            if (pianoManager.NoteFrequencies.ContainsKey(noteId))
            {
                PlayNote(noteId);

                JSRuntime.InvokeVoidAsync("setKeyActive", noteId.ToString());
            }
        }
        private void OnMidiUp(int status, int note, int velocity)
        {
            var noteId = MidiService.midiNotes[note];

            if (pianoManager.NoteFrequencies.ContainsKey(noteId))
            {
                if (!pianoManager.PressedKeys.ContainsKey(noteId)) return;

                StopNote(noteId);

                JSRuntime.InvokeVoidAsync("setKeyInactive", noteId.ToString());

            }
        }
    }
}
