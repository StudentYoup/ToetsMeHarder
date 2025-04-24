using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business;

namespace ToetsMeHarder.PianoGUI.Pages
{
    public partial class Metronome : ComponentBase
    {
        [Inject]
        public MetronomeService metronome { get; set; } = default!; // moet injecteren vanuit mauiProgram anders werkt het niet :-)


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
