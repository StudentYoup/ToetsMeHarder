namespace PianoProject;

public class Note(int noteID, int pitch,int duration)
{
    public int Pitch { get; private init; } = pitch;
    public int Duration { get; private init; } = duration;
    public int NoteID { get; private init; } = noteID;
}