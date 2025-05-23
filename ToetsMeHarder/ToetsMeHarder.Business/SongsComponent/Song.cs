namespace ToetsMeHarder.Business.LiedjesComponent;

public class Songs(string name, int bpm)
{
    public string Name { get; set; } = name;
    public List<List<Note>> Notes { get; set; }
    public int BPM { get; set; } = bpm;
}