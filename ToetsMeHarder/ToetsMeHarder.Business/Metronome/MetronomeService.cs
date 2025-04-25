using System.Timers;

namespace ToetsMeHarder.Business
{
    public class MetronomeService
    {
        private System.Timers.Timer _timer;
        private int _bpm = 60; // start bpm
        private const int MIN_BPM = 20;
        private const int MAX_BPM = 300;
        public event EventHandler? Beat;

        public bool IsRunning => _timer.Enabled;
        

        public int BPM
        {
            get => _bpm;
            set
            {
                _bpm = Math.Clamp(value, MIN_BPM, MAX_BPM); // BPM tussen de MIN en MAX
                UpdateTimerInterval();
            }
        }

        public MetronomeService()
        {
            _timer = new System.Timers.Timer
            {
                AutoReset = true
            };
            _timer.Elapsed += (s, e) => Beat?.Invoke(this, EventArgs.Empty);
            UpdateTimerInterval();
        }

        private void UpdateTimerInterval()
        {
            _timer.Interval = 60000.0 / _bpm;  // 1 minuut = 60_000 ms -> dit delen door bpm geeft tijd tussen beats
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
