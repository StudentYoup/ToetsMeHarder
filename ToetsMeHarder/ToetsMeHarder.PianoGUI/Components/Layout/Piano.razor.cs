using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;

namespace ToetsMeHarder.PianoGUI.Components.Layout
{
    public partial class Piano
    {
        private AudioHandler _audioHandler = new AudioHandler();
    
        private HashSet<string> _pressedKeys = new HashSet<string>();

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
            ["\\"] = "f4",
            ["a"] = "f#4",
            ["z"] = "g4",
            ["s"] = "g#4",
            ["x"] = "a4",
            ["d"] = "a#4",
            ["c"] = "b4",
            ["v"] = "c5",
            ["g"] = "c#5",
            ["b"] = "d5",
            ["h"] = "d#5",
            ["n"] = "e5",
            ["m"] = "f5",
            ["k"] = "f#5",
            [","] = "g5",
            ["l"] = "g#5",
            ["."] = "a5",
            [";"] = "a#5",
            ["/"] = "b5"
            //["'"] = "c6"
        };
        [Inject] private IJSRuntime JSRuntime { get; set; }
        public void HandleKeyDown(KeyboardEventArgs e)
        {
            if (_pianoKeys.ContainsKey(e.Key) && !_pressedKeys.Contains(e.Key))
            {
                var noteId = _pianoKeys[e.Key];
                _pressedKeys.Add(e.Key);
                JSRuntime.InvokeVoidAsync("setKeyActive", noteId);
            }
        }
        public void HandleKeyUp(KeyboardEventArgs e)
        {
            if (_pianoKeys.ContainsKey(e.Key) && _pressedKeys.Contains(e.Key))
            {
                var noteId = _pianoKeys[e.Key];
                _pressedKeys.Remove(e.Key);
                JSRuntime.InvokeVoidAsync("setKeyInactive", noteId);
            }
        }

        private string keyModus = "Key";
        public void changeKeyModus()
        {
            if (keyModus == "Blank") keyModus = "Note";
            else if (keyModus == "Note") keyModus = "Key";
            else if (keyModus == "Key") keyModus = "Blank";
        }
    }
}
