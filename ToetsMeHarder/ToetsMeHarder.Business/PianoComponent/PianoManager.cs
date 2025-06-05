using Plugin.Maui.Audio;
using ToetsMeHarder.Business.FallingBlocks;

namespace ToetsMeHarder.Business.PianoComponent
{
    public class PianoManager (IAudioHandler _audioHandler)
    {
        public string? midiName = null;
        public KeyModus _keyModus = KeyModus.Key;
        public Dictionary<KeyValue, IAudioPlayer> PressedKeys = new Dictionary<KeyValue, IAudioPlayer>();
        public readonly Dictionary<string, KeyValue> PianoKeys = new()
        {
            ["q"] = KeyValue.a2,
            ["2"] = KeyValue.a21,
            ["w"] = KeyValue.b2,
            ["e"] = KeyValue.c3,
            ["4"] = KeyValue.c31,
            ["r"] = KeyValue.d3,
            ["5"] = KeyValue.d31,
            ["t"] = KeyValue.e3,
            ["y"] = KeyValue.f3,
            ["7"] = KeyValue.f31,
            ["u"] = KeyValue.g3,
            ["8"] = KeyValue.g31,
            ["i"] = KeyValue.a3,
            ["9"] = KeyValue.a31,
            ["o"] = KeyValue.b3,
            ["p"] = KeyValue.c4,
            ["-"] = KeyValue.c41,
            ["["] = KeyValue.d4,
            ["="] = KeyValue.d41,
            ["]"] = KeyValue.e4,
            ["\\"] = KeyValue.f4,
            ["a"] = KeyValue.f41,
            ["z"] = KeyValue.g4,
            ["s"] = KeyValue.g41,
            ["x"] = KeyValue.a4,
            ["d"] = KeyValue.a41,
            ["c"] = KeyValue.b4,
            ["v"] = KeyValue.c5,
            ["g"] = KeyValue.c51,
            ["b"] = KeyValue.d5,
            ["h"] = KeyValue.d51,
            ["n"] = KeyValue.e5,
            ["m"] = KeyValue.f5,
            ["k"] = KeyValue.f51,
            [","] = KeyValue.g5,
            ["l"] = KeyValue.g51,
            ["."] = KeyValue.a5,
            [";"] = KeyValue.a51,
            ["/"] = KeyValue.b5,
            [""] = KeyValue.c6
        };
        public readonly Dictionary<KeyValue, double> NoteFrequencies = new Dictionary<KeyValue, double>
        {
            { KeyValue.a2, 110.00 },
            { KeyValue.a21, 116.54 },
            { KeyValue.b2, 123.47 },
            { KeyValue.c3, 130.81 },
            { KeyValue.c31, 138.59 },
            { KeyValue.d3, 146.83 },
            { KeyValue.d31, 155.56 },
            { KeyValue.e3, 164.81 },
            { KeyValue.f3, 174.61 },
            { KeyValue.f31, 185.00 },
            { KeyValue.g3, 196.00 },
            { KeyValue.g31, 207.65 },
            { KeyValue.a3, 220.00 },
            { KeyValue.a31, 233.08 },
            { KeyValue.b3, 246.94 },
            { KeyValue.c4, 261.63 },
            { KeyValue.c41, 277.18 },
            { KeyValue.d4, 293.66 },
            { KeyValue.d41, 311.13 },
            { KeyValue.e4, 329.63 },
            { KeyValue.f4, 349.23 },
            { KeyValue.f41, 369.99 },
            { KeyValue.g4, 392.00 },
            { KeyValue.g41, 415.30 },
            { KeyValue.a4, 440.00 },
            { KeyValue.a41, 466.16 },
            { KeyValue.b4, 493.88 },
            { KeyValue.c5, 523.25 },
            { KeyValue.c51, 554.37 },
            { KeyValue.d5, 587.33 },
            { KeyValue.d51, 622.25 },
            { KeyValue.e5, 659.26 },
            { KeyValue.f5, 698.46 },
            { KeyValue.f51, 739.99 },
            { KeyValue.g5, 783.99 },
            { KeyValue.g51, 830.61 },
            { KeyValue.a5, 880.00 },
            { KeyValue.a51, 932.33 },
            { KeyValue.b5, 987.77 },
            { KeyValue.c6, 1046.50 }
        };
        public void ChangeKeyModus()
        {
            if (_keyModus == KeyModus.Blank) _keyModus = KeyModus.Note;
            else if (_keyModus == KeyModus.Note) _keyModus = KeyModus.Key;
            else if (_keyModus == KeyModus.Key) _keyModus = KeyModus.Blank;
        }

        public string GetKeyName(KeyValue key)
        {
            string name = key.ToString();
            name = name.Replace("1", "#");
            return name;
        }

        public void PlayNote(KeyValue key)
        {
            double frequency = NoteFrequencies[key];
            if (PressedKeys.ContainsKey(key)) return;
            PressedKeys.Add(key, _audioHandler.PlayAudio(new Note(frequency)));

            
        }
        public void StopNote(KeyValue key)
        {
            if (!PressedKeys.ContainsKey(key)) return;
            _audioHandler.StopAudio(PressedKeys[key]);
            PressedKeys.Remove(key);
        }
    }
}
