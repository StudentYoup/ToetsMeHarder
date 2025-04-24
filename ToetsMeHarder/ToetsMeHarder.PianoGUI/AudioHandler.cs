using ToetsMeHarder.Business;
using Plugin.Maui.Audio;

namespace ToetsMeHarder;

public class AudioHandler : IAudioHandler
{
    
    private const double TAU = 2* Math.PI;
    
    // deze constants zijn nodig om het fileformat wav op te bouwen in een binarywriter
    private const int SAMPLESIZE = 44100;
    private const int HEADERSIZE = 8;
    private const int FORMATCHUNKSIZE = 16;
    private const short FORMATTYPE = 1;
    private const short TRACKS = 1;
    private const short BITSPERSAMPLE = 16;
    private const short FRAMESIZE = (short)(TRACKS * ((TRACKS * ((BITSPERSAMPLE + 7) / 8))));
    private const int BYTESPERSECOND = SAMPLESIZE * FRAMESIZE;
    private const int WAVESYZE = 4;
    
    private IAudioManager audioManager = AudioManager.Current;
    
    public void PlayAudio(Note note)
    {
        Stream audiostream = GenerateWaveForm(note.Pitch, note.Duration,short.MaxValue/4);
        IAudioPlayer player = audioManager.CreatePlayer(audiostream);
        player.Play();
    } 

    private Stream GenerateWaveForm(double frequentie,int duration, short amplitude = short.MaxValue)
    {
        //deze variabelen bepalen de lengte van de toon
        int samples = (int)((decimal)SAMPLESIZE * (duration / 1000));
        int dataChunkSize = samples * FRAMESIZE;
        int fileSize = WAVESYZE + HEADERSIZE + FORMATCHUNKSIZE + HEADERSIZE + dataChunkSize;
        
        //hier word de wav file opgebouwd
        Stream stream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(stream);
        writer.Write(0x46464952);
        writer.Write(fileSize);
        writer.Write(0x45564157);
        writer.Write(0x20746d66);
        writer.Write(FORMATCHUNKSIZE);
        writer.Write(FORMATTYPE);
        writer.Write(TRACKS);
        writer.Write(SAMPLESIZE);
        writer.Write(BYTESPERSECOND);
        writer.Write(FRAMESIZE);
        writer.Write(BITSPERSAMPLE);
        writer.Write(0x61746164);
        writer.Write(dataChunkSize);
        
        for (int i = 0; i < samples; i++)
        {
            //dit is een formule voor een standaard sine waveform
            writer.Write((short)(amplitude * (Math.Sin((frequentie * TAU / SAMPLESIZE) * i))));
        }
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }
}