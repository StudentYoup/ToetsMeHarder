public interface IMetronomeService
{
    int BPM { get; set; }
    bool IsRunning { get; }
    event EventHandler Beat;
    void Start();
    void Stop();
}
