using System;
using System.Timers;
using Plugin.Maui.Audio;

namespace ToetsMeHarder.PianoGUI.Business
{
    public class MetronomeService
    {
        private readonly System.Timers.Timer _timer;
        private readonly IAudioPlayer _player; // Dotnet package voor audio, zie commit 17/04/2025 (01e6a3f)
        private int _bpm = 60;

        public event EventHandler Beat;
        public bool IsRunning => _timer.Enabled;

        public int BPM
        {
            get => _bpm;
            set
            {
                _bpm = Math.Clamp(value, 20, 300); // BPM tussen de 20 en 300
                _timer.Interval = 60000.0 / _bpm; // 1 minuut = 60_000 ms -> dit delen door bpm geeft tijd tussen beats
            }
        }

        public MetronomeService(IAudioManager audioManager)
        {
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
