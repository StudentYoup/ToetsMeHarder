namespace ToetsMeHarder.Business
{
    public interface IMetronomeService
    {
        event EventHandler? Beat;

        bool IsRunning { get; }

        int BPM { get; set; }

        void Start();
        void Stop();
    }
}
