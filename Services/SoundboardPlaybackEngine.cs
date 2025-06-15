using NAudio.CoreAudioApi;
using NAudio.Wave;

using Soundboard4MacroDeck.Models;

using SuchByte.MacroDeck.Logging;

using System.Timers;

using Timer = System.Timers.Timer;

namespace Soundboard4MacroDeck.Services;

public sealed class SoundboardPlaybackEngine : IDisposable
{
    private readonly ActionParametersV2 _actionParameters;
    private readonly string _internalId;
    
    private IWavePlayer outputDevice;
    private AudioReader audioReader;
    private Timer? playbackTimer;

    public string GetReaderId(string prefix) => $"sb_{_actionParameters.AudioFileId}{prefix}_{_internalId}";
    public TimeSpan CurrentTime => audioReader.CurrentTime;
    public TimeSpan TotalTime => audioReader.TotalTime;

    public bool HasTimeOutput { get; }

    public SoundboardPlaybackEngine(ActionParametersV2 actionParameters, string internalId, bool enableLoop, bool hasTimeOutput)
    {
        _actionParameters = actionParameters;
        _internalId = internalId;
        HasTimeOutput = hasTimeOutput;

        outputDevice = new WasapiOut(GetDevice(), AudioClientShareMode.Shared, true, 200);

        audioReader = new AudioReader(actionParameters.FileName, actionParameters.FileData!, enableLoop)
        {
            Volume = Math.Min(actionParameters.Volume / 100f, 1f)
        };

        Init();
    }

    private void PlaybackTimer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        Elapsed?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler<EventArgs>? Elapsed;

    private void OnOutputDevicePlaybackStopped(object? _, StoppedEventArgs e)
    {
        MacroDeckLogger.Trace(PluginInstance.Current, typeof(SoundboardPlaybackEngine), "Stopped:"+_internalId);
        PlaybackStopped?.Invoke(this, e);
    }

    public event EventHandler<StoppedEventArgs>? PlaybackStopped;

    private void Init()
    {
        outputDevice.PlaybackStopped += OnOutputDevicePlaybackStopped;
        //outputDevice.Volume = Math.Min(_actionParameters.Volume / 100f, 1f);

        outputDevice.Init(audioReader.WaveProvider);

        if (HasTimeOutput)
        {
            playbackTimer = new(400);
            playbackTimer.Elapsed += PlaybackTimer_Elapsed;
        }
    }

    public void Play()
    {
        MacroDeckLogger.Trace(PluginInstance.Current, typeof(SoundboardPlaybackEngine), "Play:"+_internalId);
        playbackTimer?.Start();
        outputDevice.Play();
    }

    public void Stop()
    {
        MacroDeckLogger.Trace(PluginInstance.Current, typeof(SoundboardPlaybackEngine), "Stop:"+_internalId);
        playbackTimer?.Stop();
        outputDevice?.Stop();
    }

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

    public bool Equals(SoundboardPlaybackEngine engine)
    {
        return engine.Equals(_internalId);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        return ReferenceEquals(this, obj) || (obj is SoundboardPlaybackEngine engine && Equals(engine));
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return (_internalId is not null ? _internalId.GetHashCode() : 0);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        playbackTimer?.Dispose();
        playbackTimer = null;
        outputDevice.Dispose();
        audioReader.Dispose();
    }
}