namespace ToetsMeHarder.Business;

public abstract class AudioCommand (Note note)
{
    public Note NoteCommand { get; private init; } = note;
    
    public abstract void Execute(IAudioHandler handler);
}