using System.Collections.Concurrent;
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
    private const int LOOP_DURATION = 10000;

    //deze constants zijn nodig voor het maken van de ADSR envelope
    private const double ATTACKTIME = 0.002;
    private const double DECAYTIME = 0.05;
    private const double SUSTAINLEVEL = 0.6;
    private const double RELEASETIME = 0.9;

    private double[] sineTable = new double[SAMPLESIZE];
   
    private IAudioManager audioManager = AudioManager.Current;
    
    private Queue<AudioCommand> _commandList = new Queue<AudioCommand>();
    private Thread _audioThread;
    Dictionary<double, IAudioPlayer> _playingNotes = new Dictionary<double, IAudioPlayer>();
    
    public AudioHandler():base()
    {
        for(int i = 0; i < SAMPLESIZE; i++)
        {
            sineTable[i] = Math.Sin((double)i / SAMPLESIZE * TAU);
        }

        _audioThread = new Thread(AudioCommandHandleLoop) { IsBackground = true };
        _audioThread.Start();
    }

    
    
    private double GetSine(double phase)
    {
        phase = phase % 1.0;
        if (phase < 0) phase += 1.0;
        int index = (int)((phase * SAMPLESIZE) % SAMPLESIZE);
        return sineTable[index];
    }


    public void PlayAudio(Note note)
    {
        if(_playingNotes.ContainsKey(note.Frequentie)) return;
        
        Stream audiostream =  GenerateWaveForm(note.Frequentie, LOOP_DURATION, short.MaxValue / 4);
        IAudioPlayer player = audioManager.CreatePlayer(audiostream);
        player.Play();
        _playingNotes.Add(note.Frequentie, player);
    }

    public void StopAudio(Note note)
    {
        if (_playingNotes.ContainsKey(note.Frequentie))
        {           
            if (_playingNotes[note.Frequentie].IsPlaying)
            {
                _playingNotes[note.Frequentie].Stop();
                _playingNotes[note.Frequentie].Dispose();
            }
            _playingNotes.Remove(note.Frequentie);
        }
    }

    public void AudioCommandHandleLoop()
    {
        while (true)
        {
            AudioCommand? command = null;
            _commandList.TryDequeue(out command);

            if (command != null)
                command.Execute(this);
        }
    }

    public void RegisterCommand(AudioCommand command)
    {
        _commandList.Enqueue(command);
    }
    

    private Stream  GenerateWaveForm(double frequentie,int duration, short amplitude = short.MaxValue)
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
            double phase = frequentie * time;
            double value = 0;
            value += GetSine(1 * phase);
            value += 0.5 * GetSine(2 * phase);
            value += 0.25 * GetSine(3 * phase);
            value += 0.12 * GetSine(4 * phase);
            value += 0.07 * GetSine(5 * phase);

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