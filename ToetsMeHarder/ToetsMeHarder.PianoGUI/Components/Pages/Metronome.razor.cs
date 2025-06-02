using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business;
using Plugin.Maui.Audio;
using ToetsMeHarder.Business.SongsComponent;
using ToetsMeHarder.PianoGUI.Components.Pages;

namespace ToetsMeHarder.PianoGUI.Pages
{
    public partial class MetronomeBase : ComponentBase
    {
        [Inject]
        public MetronomeService Metronome { get; set; } = default!; // moet injecteren vanuit mauiProgram anders werkt het niet :-)

        [Inject] 
        public IAudioManager AudioManager { get; set; } = default!; // same

        private IAudioPlayer _player;
        public string BpmText
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

        protected async Task ToggleMetronome()
        {
            if (Metronome.IsRunning)
                Metronome.Stop();
            else
                Metronome.Start();
            await Home.Instance.FocusWrapper();
        }


        protected override void OnInitialized()
        {
            base.OnInitialized();
            SongsManager.Instance.RegisterPropertyChangedFunction(OnliedjeChanged);

        }

        protected override async Task OnInitializedAsync()
        {
            var file = await FileSystem.OpenAppPackageFileAsync(@"Resources\Sound\metronoom.mp3");
            _player = AudioManager.CreatePlayer(file);
            Metronome.Beat += OnBeat;
        }

        private void OnliedjeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (SongsManager.Instance.ChosenSong == null)
            {
                Metronome.BPM = 60;
                StateHasChanged();
                return;
            }
            if (e.PropertyName != nameof(SongsManager.Instance.ChosenSong)) return;
            Metronome.BPM = SongsManager.Instance.ChosenSong.BPM;
            BpmText = Metronome.BPM.ToString();
            InvokeAsync(StateHasChanged);
        }


        private void OnBeat(object? sender, EventArgs e)
        {
            _player?.Play();
        }

        
    }
}
