namespace ToetsMeHarder.Business;

public class AudioStopCommand(Note note) : AudioCommand(note)
{
    public override void Execute(IAudioHandler handler)
    {
        handler.StopAudio(NoteCommand);
    }
}