using Microsoft.AspNetCore.Components;
using ToetsMeHarder.PianoGUI.Business;

namespace ToetsMeHarder.PianoGUI.Pages
{
    public partial class Metronome : ComponentBase
    {
        [Inject]
        public MetronomeService metronome { get; set; } = default!;

        protected string BpmText
        {
            get => metronome.BPM.ToString();
            set
            {
                if (int.TryParse(value, out var bpm))
                    metronome.BPM = bpm;
            }
        }

        protected void IncreaseBpm() => metronome.BPM++;

        protected void DecreaseBpm() => metronome.BPM--;

        protected void ToggleMetronome()
        {
            if (metronome.IsRunning)
                metronome.Stop();
            else
                metronome.Start();
        }
    }
}
