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

public static class SoundboardPlayer
{
    public static void Execute(SoundboardActions action, string config, ActionButton actionButton)
    {
        var actionParameters = ActionParameters.Deserialize(config);
        if (actionParameters.FileData is null)
        {
            return;
        }

        try
        {
            Retry.Do(() => Play(action, actionParameters, actionButton));
        }
        catch (Exception e)
        {
            MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardPlayer), e.Message);
        }
    }

    public static void StopAll()
    {
        lock (key)
        {
            if (ActivePlaybackEngines.Count != 0)
            {
                foreach (var player in ActivePlaybackEngines.ToArray()) //ToArray is required to create a separate object to enumerate over
                {
                    player?.Stop();
                }
            }
        }
    }
    private static void StopCurrent(string internalId)
    {
        lock (key)
        {
            if (ActivePlaybackEngines.Count == 0)
            {
                return;
            }
            foreach (var player in ActivePlaybackEngines.ToArray().Where(p => p.Equals(internalId)))
            {
                player?.Stop();
            }
            
        }
    }

    private static readonly object key = new();
    private static List<SoundboardPlaybackEngine> ActivePlaybackEngines { get; } = new();

    private static void OnPlaybackStopped(object sender, StoppedEventArgs args)
    {
        var playbackEngine = (SoundboardPlaybackEngine)sender;
        ActivePlaybackEngines.Remove(playbackEngine);
        playbackEngine?.Dispose();
    }

    private static void Play(SoundboardActions action, ActionParameters actionParameters, ActionButton actionButton)
    {
        switch (action)
        {
            case SoundboardActions.Play:
                StopAll();
                PlayAudio(actionParameters, actionButton);
                break;

            case SoundboardActions.Overlap:
                PlayAudio(actionParameters, actionButton);
                break;

            case SoundboardActions.PlayStop:
                PlayOrStop(actionParameters, actionButton);
                break;

            case SoundboardActions.Loop:
                PlayOrStop(actionParameters, actionButton, enableLoop: true);
                break;

            default:
                break;
        }

    }

    private static void PlayOrStop(ActionParameters actionParameters, ActionButton actionButton, bool enableLoop = false)
    {
        bool currentlyPlaying = ActivePlaybackEngines.Any(p => p.Equals(actionButton.Guid));
        if (currentlyPlaying)
        {
            StopCurrent(actionButton.Guid);
        }
        else
        {
            PlayAudio(actionParameters, actionButton, enableLoop);
        }
    }

    private static void PlayAudio(ActionParameters actionParameters, ActionButton actionButton, bool enableLoop = false)
    {
        var playbackEngine = new SoundboardPlaybackEngine(actionParameters, actionButton.Guid);
        if (actionParameters.SyncButtonState)
        {
            playbackEngine.PlaybackStopped += (_, _) => MacroDeckServer.SetState(actionButton, false);
            MacroDeckServer.SetState(actionButton, true);
        }
        playbackEngine.PlaybackStopped += OnPlaybackStopped;

        playbackEngine.Init(enableLoop);

        ActivePlaybackEngines.Add(playbackEngine);

        playbackEngine.Play();
    }

}