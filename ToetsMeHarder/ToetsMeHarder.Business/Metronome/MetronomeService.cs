using System;
using System.Timers;
using Plugin.Maui.Audio;

namespace ToetsMeHarder.PianoGUI.Business
{
    public class MetronomeService
    {
        private readonly System.Timers.Timer _timer;
        private readonly IAudioPlayer _player;
        private int _bpm = 60;

        public event EventHandler Beat;
        public bool IsRunning => _timer.Enabled;

        public int BPM
        {
            get => _bpm;
            set
            {
                _bpm = Math.Clamp(value, 20, 300);
                _timer.Interval = 60000.0 / _bpm;
            }
        }

        public MetronomeService(IAudioManager audioManager)
        {
            // Audio bestand moet in Resources/Raw en als MauiAsset gemarkeerd
            var audioFile = FileSystem.OpenAppPackageFileAsync(@"C:\Users\forge\Documents\School\ToetsMeHarder\ToetsMeHarder\ToetsMeHarder.Business\Resources\Sound\metronoom.mp3").Result;
            _player = audioManager.CreatePlayer(audioFile);

            _timer = new System.Timers.Timer(60000.0 / _bpm)
            {
                AutoReset = true
            };
            _timer.Elapsed += (s, e) =>
            {
                _player.Play();
                Beat?.Invoke(this, EventArgs.Empty);
            };
        }

        public void Start() => _timer.Start();
        public void Stop() => _timer.Stop();
    }
}
