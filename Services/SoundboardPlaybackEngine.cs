using System;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using Soundboard4MacroDeck.Models;
using SuchByte.MacroDeck.Logging;
using System.Timers;

namespace Soundboard4MacroDeck.Services;

public sealed class SoundboardPlaybackEngine : IDisposable
{
    private readonly ActionParametersV2 _actionParameters;
    private readonly string _internalId;
    
    // private readonly MixingSampleProvider _mixer;
    private IWavePlayer outputDevice;
    private AudioReader audioReader;
    private Timer playbackTimer;

    public string GetReaderId(string prefix) => $"sb_{_actionParameters.AudioFileId}{prefix}_{_internalId}";
    public TimeSpan CurrentTime => audioReader.CurrentTime;
    public TimeSpan TotalTime => audioReader.TotalTime;

    public bool HasTimeOutput { get; }

    public SoundboardPlaybackEngine(ActionParametersV2 actionParameters, string internalId, bool hasTimeOutput)
    {
        _actionParameters = actionParameters;
        _internalId = internalId;
        HasTimeOutput = hasTimeOutput;

        outputDevice = new WasapiOut(GetDevice(), AudioClientShareMode.Shared, true, 200);
        outputDevice.PlaybackStopped += OnOutputDevicePlaybackStopped;

        if (hasTimeOutput)
        {
            playbackTimer = new(400);
            playbackTimer.Elapsed += PlaybackTimer_Elapsed;
        }
        // _mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(outputDevice.OutputWaveFormat.SampleRate, outputDevice.OutputWaveFormat.Channels));
        // _mixer.ReadFully = true;
        // outputDevice.Init(_mixer);
        // outputDevice.Play();
    }

    private void PlaybackTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        Elapsed?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler<EventArgs> Elapsed;

    private void OnOutputDevicePlaybackStopped(object _, StoppedEventArgs e)
    {
        MacroDeckLogger.Trace(PluginInstance.Current, typeof(SoundboardPlaybackEngine), "Stopped:"+_internalId);
        PlaybackStopped?.Invoke(this, e);
    }

    public event EventHandler<StoppedEventArgs> PlaybackStopped;
    
    // public event EventHandler<SampleProviderEventArgs> AudioStopped;
    public void Init(bool enableLoop = false)
    {
        // _mixer.MixerInputEnded += (s,e) => AudioStopped?.Invoke(audioReader, e)
        // outputDevice.PlaybackStopped += (_,e) => PlaybackStopped?.Invoke(this,e);

        if (audioReader is null || !audioReader.FileName.Equals(_actionParameters.FileName))
        {
            audioReader = new AudioReader(_actionParameters.FileName, _actionParameters.FileData, enableLoop)
            {
                Volume = Math.Min(_actionParameters.Volume / 100f, 1f)
            };
            outputDevice.Init(audioReader);
        }

    }

    public void Play()
    {
        MacroDeckLogger.Trace(PluginInstance.Current, typeof(SoundboardPlaybackEngine), "Play:"+_internalId);
        // AddMixerInput(audioReader);
        playbackTimer?.Start();
        outputDevice.Play();
    }

    public void Stop()
    {
        MacroDeckLogger.Trace(PluginInstance.Current, typeof(SoundboardPlaybackEngine), "Stop:"+_internalId);
        playbackTimer?.Stop();
        outputDevice?.Stop();
    }
    
    // private void AddMixerInput(ISampleProvider input)
    // {
    //     _mixer.AddMixerInput(ConvertToRightChannelCount(input));
    // }
    // private ISampleProvider ConvertToRightChannelCount(ISampleProvider input)
    // {
    //     // if (input.WaveFormat.Channels == mixer.WaveFormat.Channels)
    //     // {
    //     //     return input;
    //     // }
    //     if (input.WaveFormat.Channels == 1 && _mixer.WaveFormat.Channels == 2)
    //     {
    //         return new MonoToStereoSampleProvider(input);
    //     }
    //     //throw new NotImplementedException("Not yet implemented this channel count conversion");
    //     return input;
    // }

    private MMDevice GetDevice()//out int latency)
    {
        using var devices = new MMDeviceEnumerator();
        if (!_actionParameters.MustGetDefaultDevice())
        {
            //latency = actionParameters.Latency;
            return devices.GetDevice(_actionParameters.OutputDeviceId);
        }
        IOutputConfiguration globalParameters = PluginInstance.Configuration;
        //latency = globalParameters.Latency;
        return !globalParameters.MustGetDefaultDevice() //if
            ? devices.GetDevice(globalParameters.OutputDeviceId)
            : devices.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia); //else
    }
    
    public bool Equals(string internalId)
    {
        return !string.IsNullOrWhiteSpace(internalId) && internalId == _internalId;
    }

    protected bool Equals(SoundboardPlaybackEngine engine)
    {
        return engine.Equals(_internalId);
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == typeof(SoundboardPlaybackEngine) && Equals((SoundboardPlaybackEngine)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return (_internalId != null ? _internalId.GetHashCode() : 0);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        playbackTimer?.Dispose();
        playbackTimer = null;
        outputDevice?.Dispose();
        outputDevice = null;
        audioReader?.Dispose();
        audioReader = null;
    }
}