using ToetsMeHarder.Business.FallingBlocks;

namespace ToetsMeHarder.Business.SongsComponent;

public class Songs(string name, int bpm, int duration, string key)
{
    public string Name { get; set; } = name;
    public List<List<Note>> Notes { get; set; }
    public int BPM { get; set; } = bpm;
    public int Duration { get; set; } = duration;
    public string Key { get; set; } = key;

    public List<NoteBlock> blocks = new List<NoteBlock>();

    public void FillBlocks()
    {
        Random r = new Random();
        var values = Enum.GetValues(typeof(KeyValue));
        for (int i = 0; i < 20; i++)
        {
            blocks.Add(new NoteBlock(i, 0, (KeyValue)values.GetValue(r.Next(values.Length)), r.Next(3), i));
        }
    } 
}