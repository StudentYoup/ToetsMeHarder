namespace ToetsMeHarder.Business;

public abstract class AudioCommand (Note note)
{   
    public abstract void Execute(IAudioHandler handler);
}