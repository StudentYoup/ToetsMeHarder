namespace ToetsMeHarder.Business.LiedjesComponent;

public class Liedje(string naam, int bpm)
{
    public string Naam { get; set; } = naam;
    public List<List<Note>> Notes {get; set;}
    public int BPM { get; set; } = bpm;
}