using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business;
using Plugin.Maui.Audio;

namespace ToetsMeHarder.PianoGUI.Pages
{
    public partial class MetronomeBase : ComponentBase
    {
        [Inject]
        public MetronomeService Metronome { get; set; } = default!; // moet injecteren vanuit mauiProgram anders werkt het niet :-)

        [Inject] 
        public IAudioManager AudioManager { get; set; } = default!; // same

        private IAudioPlayer _player;

        protected override async Task OnInitializedAsync()
        {
            var file = await FileSystem.OpenAppPackageFileAsync(@"Resources\Sound\metronoom.mp3");
            _player = AudioManager.CreatePlayer(file);
            Metronome.Beat += OnBeat;
        }

        private void OnBeat(object? sender, EventArgs e)
        {
            _player?.Play();
        }



        protected string BpmText
        {
            get => Metronome.BPM.ToString();
            set
            {
                if (int.TryParse(value, out var bpm))
                    Metronome.BPM = bpm;
            }
        }

        protected void IncreaseBpm() => Metronome.BPM++;

        protected void DecreaseBpm() => Metronome.BPM--;

        protected void ToggleMetronome()
        {
            if (Metronome.IsRunning)
                Metronome.Stop();
            else
                Metronome.Start();
        }
    }
}
