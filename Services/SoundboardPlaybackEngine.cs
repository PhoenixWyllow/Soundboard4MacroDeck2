using NAudio.CoreAudioApi;
using NAudio.Wave;

using Soundboard4MacroDeck.Models;

using Timer = System.Timers.Timer;
using ElapsedEventArgs = System.Timers.ElapsedEventArgs;

namespace Soundboard4MacroDeck.Services;

public sealed class SoundboardPlaybackEngine : IDisposable
{
    private readonly ActionParametersV2 _actionParameters;
    private readonly string _internalId;
    private readonly string _engineId;
    
    private WasapiPlayer outputDevice;
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

        _engineId = GetReaderId(string.Empty);

        outputDevice = new WasapiPlayerBuilder()
            .WithDevice(GetDevice())
            .WithSharedMode()
            .WithEventSync()
            .WithLatency(200)
            .Build();


        //outputDevice = new WasapiOut(GetDevice(), AudioClientShareMode.Shared, true, 200);
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
        PluginLogger.Debug(nameof(SoundboardPlaybackEngine), e.Exception, "Stopped - {EngineId}", _engineId);
        PlaybackStopped?.Invoke(this, e);
    }

    public event EventHandler<StoppedEventArgs>? PlaybackStopped;

    private void Init()
    {
        outputDevice.PlaybackStopped += OnOutputDevicePlaybackStopped;

        outputDevice.Init(audioReader.WaveProvider);

        if (HasTimeOutput)
        {
            playbackTimer = new(400);
            playbackTimer.Elapsed += PlaybackTimer_Elapsed;
        }
    }

    public void Play()
    {
        PluginLogger.Debug(nameof(SoundboardPlaybackEngine), "Play - {EngineId}", _engineId);
        playbackTimer?.Start();
        outputDevice.Play();
    }

    public void Stop()
    {
        PluginLogger.Debug(nameof(SoundboardPlaybackEngine), "Stop - {EngineId}", _engineId);
        playbackTimer?.Stop();
        outputDevice?.Stop();
    }

    private MMDevice GetDevice()
    {
        using var devices = new MMDeviceEnumerator();
        if (!_actionParameters.MustGetDefaultDevice())
        {
            return devices.GetDevice(_actionParameters.OutputDeviceId);
        }
        IOutputConfiguration globalParameters = PluginInstance.Configuration;
        return !globalParameters.MustGetDefaultDevice() //if
            ? devices.GetDevice(globalParameters.OutputDeviceId)
            : devices.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia); //else
    }
    
    public bool MatchesInternalId(string internalId)
    {
        return !string.IsNullOrWhiteSpace(internalId) && internalId == _internalId;
    }

    public bool Equals(SoundboardPlaybackEngine engine)
    {
        return engine.MatchesInternalId(_internalId);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

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