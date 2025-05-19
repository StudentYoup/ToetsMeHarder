using ToetsMeHarder.Business.FallingBlocks;

namespace ToetsMeHarder.Business.LiedjesComponent;

public class Song(string naam, int bpm,int duration, string key,int id)
{
    public int Id { get; set; } = id;
    public string Naam { get; set; } = naam;
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