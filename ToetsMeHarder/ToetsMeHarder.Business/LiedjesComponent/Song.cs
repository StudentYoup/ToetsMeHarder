namespace ToetsMeHarder.Business.LiedjesComponent;

public class Song(string naam, int bpm)
{
    public int Id { get; set; }
    public string Naam { get; set; } = naam;
    public int BPM { get; set; } = bpm;
    public int Duration { get; set; }
    public string Key { get; set; }
}