using ToetsMeHarder.Business;
using Plugin.Maui.Audio;
using System.Runtime.InteropServices;
using System.Diagnostics;

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
    private const int LOOP_DURATION = 2000;
    
    private IAudioManager audioManager = AudioManager.Current;
    
    public IAudioPlayer PlayAudio(Note note)
    {
        Stream audiostream = GenerateWaveForm(note.Frequentie, LOOP_DURATION, short.MaxValue/4);
        IAudioPlayer player = audioManager.CreatePlayer(audiostream);
        player.Loop = true;
        player.Play();

        return player;
    } 

    public void StopAudio(IAudioPlayer player)
    {   
            try{

            
            if (player.IsPlaying)
            {
                player.Stop();
                player.Dispose();
            }else
            {
                player.Dispose();
            }
            }catch (Exception e)
            {
               player.Loop = false;
               Console.WriteLine("AudioHandler crash: " + e.Message);
            }
    }

    private Stream GenerateWaveForm(double frequentie,int duration, short amplitude = short.MaxValue)
    {
        //deze variabelen bepalen de lengte van de toon
        int samples = (int)((double)SAMPLESIZE * ((double)duration / 1000f));
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