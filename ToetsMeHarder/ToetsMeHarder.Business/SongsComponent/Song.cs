using ToetsMeHarder.Business.FallingBlocks;

namespace ToetsMeHarder.Business.SongsComponent;

public class Songs(string name, int bpm, int duration, string key)
{
    public string Name { get; set; } = name;
    public int BPM { get; set; } = bpm;
    public double Duration { get; set; } = duration;
    public string Key { get; set; } = key;

    public List<NoteBlock> NoteBlocks = new List<NoteBlock>();
}