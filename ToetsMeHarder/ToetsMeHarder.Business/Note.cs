namespace ToetsMeHarder.Business;

public class Note(double frequentie, int duration)
{
    public double Frequentie { get; private init; } = frequentie;
    public int Duration { get; private init; } = duration;
}