using Plugin.Maui.Audio;

namespace ToetsMeHarder.Business;

public interface IAudioHandler
{
    public void PlayAudio(Note note);
    public void StopAudio(Note note);
    public void AudioCommandHandleLoop();
    
    public void RegisterCommand(AudioCommand command);
}