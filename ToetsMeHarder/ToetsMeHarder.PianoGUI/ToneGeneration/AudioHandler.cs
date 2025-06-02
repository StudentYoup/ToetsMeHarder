using System.Collections.Concurrent;
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
    private const int LOOP_DURATION = 1000;


    private double[] sineTable = new double[SAMPLESIZE];
   
    private IAudioManager audioManager = AudioManager.Current;
    private Dictionary<double, MemoryStream> _freqWaveCache = new();

    
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
        
        if (!_freqWaveCache.TryGetValue(note.Frequentie, out MemoryStream stream)) //als nog niet in cache, maak aan
        {
            stream = (MemoryStream)GenerateWaveForm(note.Frequentie, LOOP_DURATION, short.MaxValue / 4);
            stream.Position = stream.Seek(0, SeekOrigin.Begin);
            _freqWaveCache[note.Frequentie] = stream;
        }
        
        MemoryStream copy = new(stream.ToArray());

        IAudioPlayer player = audioManager.CreatePlayer(copy);
        player.Loop = true;
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
                _playingNotes.Remove(note.Frequentie);
            }
            else
            {
                RegisterCommand(new AudioStopCommand(new Note(note.Frequentie)));
            }

        }
    }

    public void AudioCommandHandleLoop()
    {
        while (true)
        {
            Thread.Sleep(1);
            if (_commandList.Count <= 0) continue;
            AudioCommand? command = null;
            _commandList.TryDequeue(out command);
            if (command == null) continue;
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

            short sampleValue = (short)(Math.Clamp(value, -1, 1) * amplitude);
            writer.Write(sampleValue);
        }
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }

    
}