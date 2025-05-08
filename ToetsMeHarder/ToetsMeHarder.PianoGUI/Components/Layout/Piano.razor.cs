using ToetsMeHarder.Business;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Controls;
using Plugin.Maui.Audio;


namespace ToetsMeHarder.PianoGUI.Components.Layout
{
    public partial class Piano
    {
        private AudioHandler _audioHandler = new AudioHandler();
    
        private Dictionary<string, IAudioPlayer> _pressedKeys = new Dictionary<string, IAudioPlayer>();

        private readonly Dictionary<string, string> _pianoKeys = new()
        {
            ["q"] = "a2",
            ["2"] = "a#2",
            ["w"] = "b2",
            ["e"] = "c3",
            ["4"] = "c#3",
            ["r"] = "d3",
            ["5"] = "d#3",
            ["t"] = "e3",
            ["y"] = "f3",
            ["7"] = "f#3",
            ["u"] = "g3",
            ["8"] = "g#3",
            ["i"] = "a3",
            ["9"] = "a#3",
            ["o"] = "b3",
            ["p"] = "c4",
            ["-"] = "c#4",
            ["["] = "d4",
            ["="] = "d#4",
            ["]"] = "e4",
            ["z"] = "f4",
            ["s"] = "f#4",
            ["x"] = "g4",
            ["d"] = "g#4",
            ["c"] = "a4",
            ["f"] = "a#4",
            ["v"] = "b4",
            ["b"] = "c5",
            ["h"] = "c#5",
            ["n"] = "d5",
            ["j"] = "d#5",
            ["m"] = "e5",
            [","] = "f5",
            ["l"] = "f#5",
            ["."] = "g5",
            [";"] = "g#5",
            ["/"] = "a5"
            // [""] = "a#5",
            // [""] = "b5",
            // [""] = "c6",
        };
        
        private readonly Dictionary<string, double> _noteFrequencies = new Dictionary<string, double>
        {
            { "a2", 110.00 },
            { "a#2", 116.54 },
            { "b2", 123.47 },
            { "c3", 130.81 },
            { "c#3", 138.59 },
            { "d3", 146.83 },
            { "d#3", 155.56 },
            { "e3", 164.81 },
            { "f3", 174.61 },
            { "f#3", 185.00 },
            { "g3", 196.00 },
            { "g#3", 207.65 },
            { "a3", 220.00 },
            { "a#3", 233.08 },
            { "b3", 246.94 },
            { "c4", 261.63 },
            { "c#4", 277.18 },
            { "d4", 293.66 },
            { "d#4", 311.13 },
            { "e4", 329.63 },
            { "f4", 349.23 },
            { "f#4", 369.99 },
            { "g4", 392.00 },
            { "g#4", 415.30 },
            { "a4", 440.00 },
            { "a#4", 466.16 },
            { "b4", 493.88 },
            { "c5", 523.25 },
            { "c#5", 554.37 },
            { "d5", 587.33 },
            { "d#5", 622.25 },
            { "e5", 659.26 },
            { "f5", 698.46 },
            { "f#5", 739.99 },
            { "g5", 783.99 },
            { "g#5", 830.61 },
            { "a5", 880.00 },
            { "a#5", 932.33 },
            { "b5", 987.77 },
            { "c6", 1046.50 }
        };


        [Inject] private IJSRuntime JSRuntime { get; set; }
        public void HandleKeyDown(KeyboardEventArgs e)
        {
            if (_pianoKeys.ContainsKey(e.Key) && !_pressedKeys.ContainsKey(e.Key))
            {
                var noteId = _pianoKeys[e.Key];
                JSRuntime.InvokeVoidAsync("setKeyActive", noteId);
            }
        }
        public void HandleKeyUp(KeyboardEventArgs e)
        {
            if (_pianoKeys.ContainsKey(e.Key) && _pressedKeys.ContainsKey(e.Key))
            {
                var noteId = _pianoKeys[e.Key];
                _audioHandler.StopAudio(_pressedKeys[noteId]);

                _pressedKeys.Remove(noteId);

                JSRuntime.InvokeVoidAsync("setKeyInactive", noteId);
            }
        }
        public void PlayNote(double frequentie)
        {
            var key = _noteFrequencies.FirstOrDefault(x => x.Value == frequentie).Key;
            if (_pressedKeys.ContainsKey(key)) return;
            _pressedKeys.Add(key, _audioHandler.PlayAudio(new Note(frequentie)));
        }
    }
}
