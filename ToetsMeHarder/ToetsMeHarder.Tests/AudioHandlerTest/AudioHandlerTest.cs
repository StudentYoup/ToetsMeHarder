using Plugin.Maui.Audio;
using ToetsMeHarder.Business;
namespace ToetsMeHarder.Tests.AudioHandlerTest;
public class AudioHandlerTests
{
    private AudioHandler _handler = new();


    [Test]
    public void PlayAudio_ShouldReturnsPlayer()
    {
        Note note = new(440.0);
        IAudioPlayer player = _handler.PlayAudio(note);
        Assert.That(player, Is.Not.Null);
    }

    [Test]
    public void StopAudio_ShouldStopPlayer()
    {
        Note note = new(440.0);
        IAudioPlayer player = _handler.PlayAudio(note);

        _handler.StopAudio(player);
        Assert.That(player.IsPlaying, Is.False);
    }
}
