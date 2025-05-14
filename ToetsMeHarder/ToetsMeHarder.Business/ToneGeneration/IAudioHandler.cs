using Plugin.Maui.Audio;

namespace ToetsMeHarder.Business;

public interface IAudioHandler
{
    public Task<IAudioPlayer> PlayAudio(Note note);
}