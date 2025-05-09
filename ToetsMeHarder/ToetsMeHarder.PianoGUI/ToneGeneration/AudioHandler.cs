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
    private const int LOOP_DURATION = 10000;

    //deze constants zijn nodig voor het maken van de ADSR envelope
    private const double ATTACKTIME = 0.002;
    private const double DECAYTIME = 0.05;
    private const double SUSTAINLEVEL = 0.6;
    private const double RELEASETIME = 0.9;
    
    private IAudioManager audioManager = AudioManager.Current;
    
    public IAudioPlayer PlayAudio(Note note)
    {
        Stream audiostream = GenerateWaveForm(note.Frequentie, LOOP_DURATION,short.MaxValue/4);
        IAudioPlayer player = audioManager.CreatePlayer(audiostream);
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
            double time = (double)i / SAMPLESIZE;
            double value = 0;
            value += Math.Sin(frequentie * TAU * time);
            value += 0.5 * Math.Sin(frequentie * TAU * 2 * time);
            value += 0.25 * Math.Sin(frequentie * TAU * 3 * time);
            value += 0.12 * Math.Sin(frequentie * TAU * 4 * time);
            value += 0.07 * Math.Sin(frequentie * TAU * 5 * time);

            value *= GetADSR(time, samples);

            short sampleValue = (short)(Math.Clamp(value, -1, 1) * amplitude);
            writer.Write(sampleValue);
        }
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }

    private double GetADSR(double t, int sampleCount)
    {
        if (t < ATTACKTIME)
            return t / ATTACKTIME;
        else if (t < ATTACKTIME + DECAYTIME)
            return 1 - (1 - SUSTAINLEVEL) * ((t - ATTACKTIME) / DECAYTIME);
        else if (t < 1 / ((double)sampleCount / SAMPLESIZE) - RELEASETIME)
            return SUSTAINLEVEL;
        else
        {
            double releaseStart = ((double)sampleCount / SAMPLESIZE) - RELEASETIME;
            double releaseProgress = (t - releaseStart) / RELEASETIME;

            // Clamp to [0,1] to avoid overshooting
            releaseProgress = Math.Clamp(releaseProgress, 0.0, 1.0);

            return SUSTAINLEVEL * (1.0 - releaseProgress);
        }
    }
}