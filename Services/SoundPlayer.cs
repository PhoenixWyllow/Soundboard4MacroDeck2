using NAudio.Wave;
using Soundboard4MacroDeck.Models;
using System;
using NAudio.CoreAudioApi;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.Server;
using System.Collections.Generic;
using System.Linq;
using SuchByte.MacroDeck.Logging;

namespace Soundboard4MacroDeck.Services;

public sealed class SoundPlayer
{
    public static void Execute(SoundboardActions action, string config, ActionButton actionButton)
    {
        var actionParameters = ActionParameters.Deserialize(config);
        if (actionParameters.FileData is null)
        {
            return;
        }

        SoundPlayer soundPlayer = new(actionButton.Guid)
        {
            _actionParameters = actionParameters,
            _actionButton = actionButton,
        };

        Retry.Do(() => soundPlayer.Play(action));
    }

    public static void StopAll()
    {
        lock (key)
        {
            if (SoundPlayers.Count != 0)
            {
                foreach (var player in SoundPlayers.ToArray())
                {
                    player.StopAudio();
                }
                SoundPlayers.Clear();
            }
        }
    }
    public void StopCurrent(bool stopThisOnly)
    {
        if (!stopThisOnly)
        {
            StopAll();
            return;
        }
        lock (key)
        {
            if (SoundPlayers.Count != 0)
            {
                foreach (var player in SoundPlayers.ToArray().Where(p => p._internalId.Equals(_internalId)))
                {
                    player.StopAudio();
                }
            }
        }
    }

    private static readonly object key = new();
    private static List<SoundPlayer> SoundPlayers { get; } = new();

    private IWavePlayer _outputDevice;
    private AudioBytesReader _fileReader;
    private ActionParameters _actionParameters;
    private ActionButton _actionButton;
    private readonly string _internalId;

    private SoundPlayer(string internalId)
    {
        _internalId = internalId;
    }

    public bool Equals(SoundPlayer soundPlayer)
    {
        return _internalId.Equals(soundPlayer._internalId);
    }

    public override bool Equals(object obj)
    {
        return obj is SoundPlayer sp && Equals(sp);
    }

    public override int GetHashCode()
    {
        return _internalId.GetHashCode();
    }

    private void StopAudio()
    {
        _outputDevice?.Stop();
    }

    private void OnPlaybackStopped(object sender, StoppedEventArgs args)
    {
        _outputDevice?.Dispose();
        _outputDevice = null;
        _fileReader?.Dispose();
        _fileReader = null;

        SoundPlayers.Remove(this);
    }

    private void Play(SoundboardActions action)
    {
        switch (action)
        {
            case SoundboardActions.Play:
                StopAll();
                PlayAudio();
                break;

            case SoundboardActions.Overlap:
                PlayAudio();
                break;

            case SoundboardActions.PlayStop:
                PlayOrStop();
                break;

            case SoundboardActions.Loop:
                PlayOrStop(enableLoop: true);
                break;

            default:
                break;
        }

    }

    private void PlayOrStop(bool enableLoop = false)
    {
        MacroDeckLogger.Trace(Main.Instance, _internalId);
        //bool currentlyPlaying = actionButton.State;
        bool currentlyPlaying = SoundPlayers.Any(p => p._internalId.Equals(_internalId));
        StopCurrent(currentlyPlaying);
        if (!currentlyPlaying)
        {
            PlayAudio(enableLoop);
        }
    }

    private void PlayAudio(bool enableLoop = false)
    {
        EnsureOutputDevice();
        if (_actionParameters.SyncButtonState)
        {
            _outputDevice.PlaybackStopped += (_, _) => MacroDeckServer.SetState(_actionButton, false);
            MacroDeckServer.SetState(_actionButton, true);
        }

        if (_fileReader is null || !_fileReader.FileName.Equals(_actionParameters.FileName))
        {
            _fileReader = new(_actionParameters.FileName, _actionParameters.FileData)
            {
                Volume = Math.Min(_actionParameters.Volume / 100f, 1f),
                LoopingEnabled = enableLoop,
            };
            _outputDevice.Init(_fileReader);
        }

        SoundPlayers.Add(this);

        _outputDevice.Play();
    }

    private void EnsureOutputDevice()
    {
        if (_outputDevice == null)
        {
            _outputDevice = new WasapiOut(GetDevice(), AudioClientShareMode.Shared, true, 200);
            _outputDevice.PlaybackStopped += OnPlaybackStopped;
        }
    }

    private MMDevice GetDevice()//out int latency)
    {
        using var devices = new MMDeviceEnumerator();
        if (!_actionParameters.MustGetDefaultDevice())
        {
            //latency = actionParameters.Latency;
            return devices.GetDevice(_actionParameters.OutputDeviceId);
        }
        IOutputConfiguration globalParameters = Main.Configuration;
        //latency = globalParameters.Latency;
        return !globalParameters.MustGetDefaultDevice() //if
            ? devices.GetDevice(globalParameters.OutputDeviceId)
            : devices.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia); //else
    }
}