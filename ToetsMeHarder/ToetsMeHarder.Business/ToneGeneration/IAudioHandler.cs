using Plugin.Maui.Audio;

namespace ToetsMeHarder.Business;

public interface AudioHandler
{
    public IAudioPlayer PlayAudio(Note note);
    public void StopAudio(IAudioPlayer player);
}