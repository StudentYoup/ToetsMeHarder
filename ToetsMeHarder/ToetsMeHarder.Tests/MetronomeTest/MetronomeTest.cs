using ToetsMeHarder.Business;
using NUnit.Framework;

namespace ToetsMeHarder.Tests.MetronomeTest;

public class MetronomeServiceTests
{
    private MetronomeService _metronome = new();


    [Test]
    public void BPM_SettingBelow20_ShouldBe20()
    {
        _metronome.BPM = 10;
        Assert.That(_metronome.BPM, Is.EqualTo(20));
    }

    [Test]
    public void BPM_SettingAbove300_ShouldBe300()
    {
        _metronome.BPM = 400;
        Assert.That(_metronome.BPM, Is.EqualTo(300));
    }

    [Test]
    public void Start_ShouldSetIsRunningToTrue()
    {
        _metronome.Start();
        Assert.That(_metronome.IsRunning, Is.True);
    }

    [Test]
    public void Stop_ShouldSetIsRunningToFalse()
    {
        _metronome.Start();
        _metronome.Stop();
        Assert.That(_metronome.IsRunning, Is.False);
    }
}
