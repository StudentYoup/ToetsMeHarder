using Plugin.Maui.Audio;

namespace ToetsMeHarder.Business;

public interface IAudioHandler
{
    public IAudioPlayer PlayAudio(Note note);
    public void StopAudio(IAudioPlayer player);
}