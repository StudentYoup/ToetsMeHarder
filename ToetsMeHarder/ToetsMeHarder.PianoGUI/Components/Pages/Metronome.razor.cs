using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business;
using Plugin.Maui.Audio;
using ToetsMeHarder.Business.LiedjesComponent;

namespace ToetsMeHarder.PianoGUI.Pages
{
    public partial class MetronomeBase : ComponentBase
    {
        [Inject]
        public MetronomeService Metronome { get; set; } = default!; // moet injecteren vanuit mauiProgram anders werkt het niet :-)

        [Inject] 
        public IAudioManager AudioManager { get; set; } = default!; // same

        private IAudioPlayer _player;


        protected override void OnInitialized()
        {
            base.OnInitialized();
            SongManager.Instance.RegisterPropertyChangedFunction(OnliedjeChanged);
            
        }

        private void OnliedjeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName != nameof(SongManager.Instance.GekozenLiedje)) return;
            Metronome.BPM = SongManager.Instance.GekozenLiedje.BPM;
            BpmText = Metronome.BPM.ToString();
            InvokeAsync(StateHasChanged);
        }


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
