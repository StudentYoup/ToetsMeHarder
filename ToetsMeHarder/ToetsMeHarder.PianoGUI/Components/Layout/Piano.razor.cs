using ToetsMeHarder.Business;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;


namespace ToetsMeHarder.PianoGUI.Components.Layout{

public partial class Piano
{
    private AudioHandler _audioHandler = new AudioHandler();

    private const int _NOTE_DURATION = 1000;
    
      private HashSet<string> _pressedKeys = new HashSet<string>();

    private readonly Dictionary<string, Note> _pianoKeys = new()
    {
        ["1"] = new Note(110.00, _NOTE_DURATION),
        ["2"] = new Note(116.54, _NOTE_DURATION),
        ["3"] = new Note(123.47, _NOTE_DURATION),
        ["4"] = new Note(130.81, _NOTE_DURATION),
        ["5"] = new Note(138.59, _NOTE_DURATION),
        ["6"] = new Note(146.83, _NOTE_DURATION),
        ["7"] = new Note(155.56, _NOTE_DURATION),
        ["8"] = new Note(164.81, _NOTE_DURATION),
        ["9"] = new Note(174.61, _NOTE_DURATION),
        ["0"] = new Note(185.00, _NOTE_DURATION),
        ["q"] = new Note(196.00, _NOTE_DURATION),
        ["w"] = new Note(207.65, _NOTE_DURATION),
        ["e"] = new Note(220.00, _NOTE_DURATION),
        ["r"] = new Note(233.08, _NOTE_DURATION),
        ["t"] = new Note(246.94, _NOTE_DURATION),
        ["y"] = new Note(261.63, _NOTE_DURATION),
        ["u"] = new Note(277.18, _NOTE_DURATION),
        ["i"] = new Note(293.66, _NOTE_DURATION),
        ["o"] = new Note(311.13, _NOTE_DURATION),
        ["p"] = new Note(329.63, _NOTE_DURATION),
        ["a"] = new Note(349.23, _NOTE_DURATION),
        ["s"] = new Note(369.99, _NOTE_DURATION),
        ["d"] = new Note(392.00, _NOTE_DURATION),
        ["f"] = new Note(415.30, _NOTE_DURATION),
        ["g"] = new Note(440.00, _NOTE_DURATION),
        ["h"] = new Note(466.16, _NOTE_DURATION),
        ["j"] = new Note(493.88, _NOTE_DURATION),
        ["k"] = new Note(523.25, _NOTE_DURATION),
        ["l"] = new Note(554.37, _NOTE_DURATION),
        [";"] = new Note(587.33, _NOTE_DURATION),
        ["'"] = new Note(622.25, _NOTE_DURATION),
        ["z"] = new Note(659.26, _NOTE_DURATION),
        ["x"] = new Note(698.46, _NOTE_DURATION),
        ["c"] = new Note(739.99, _NOTE_DURATION),
        ["v"] = new Note(783.99, _NOTE_DURATION),
        ["b"] = new Note(830.61, _NOTE_DURATION),
        ["n"] = new Note(880.00, _NOTE_DURATION),
        ["m"] = new Note(932.33, _NOTE_DURATION),
        [","] = new Note(987.77, _NOTE_DURATION),
        ["."] = new Note(1046.50, _NOTE_DURATION),
    };

    public void HandleKeyDown(KeyboardEventArgs e)
    {
        if(_pianoKeys.ContainsKey(e.Key) && !_pressedKeys.Contains(e.Key))
        {
            _audioHandler.PlayAudio(_pianoKeys[e.Key]);
            _pressedKeys.Add(e.Key);
        }
    }
    public void HandleKeyUp(KeyboardEventArgs e)
    {
        if(_pianoKeys.ContainsKey(e.Key) && _pressedKeys.Contains(e.Key))
        {
            _pressedKeys.Remove(e.Key);
        }
    }
}
}
