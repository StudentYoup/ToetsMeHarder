namespace ToetsMeHarder.Business;

public class Note(int pitch,int duration)
{
    public int Pitch { get; private init; } = pitch;
    public int Duration { get; private init; } = duration;
}