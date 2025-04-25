using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business;
using Plugin.Maui.Audio;

namespace ToetsMeHarder.PianoGUI.Pages
{
    public partial class Metronome : ComponentBase
    {
        [Inject]
        public MetronomeService metronome { get; set; } = default!; // moet injecteren vanuit mauiProgram anders werkt het niet :-)

        [Inject] 
        public IAudioManager AudioManager { get; set; } = default!; // same

        private IAudioPlayer _player;

        protected override async Task OnInitializedAsync()
        {
            var file = await FileSystem.OpenAppPackageFileAsync(@"Resources\Sound\metronoom.mp3");
            _player = AudioManager.CreatePlayer(file);
            metronome.Beat += OnBeat;
        }

        private void OnBeat(object? sender, EventArgs e)
        {
            _player?.Play();
        }



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
