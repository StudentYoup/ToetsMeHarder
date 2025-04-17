namespace ToetsMeHarder.Business;


public class Class1
{
    IAudioHandler audioHandler;

    public Class1(IAudioHandler audioHandler)
    {
        this.audioHandler = audioHandler;
        audioHandler.PlayAudio(new Note(440,5000));
    }
}
