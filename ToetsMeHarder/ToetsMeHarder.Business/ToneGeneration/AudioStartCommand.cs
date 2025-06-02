namespace ToetsMeHarder.Business;

public class AudioStartCommand(Note note) : AudioCommand(note)
{
    public override void Execute(IAudioHandler handler)
    {
        handler.PlayAudio(note);
    }
}