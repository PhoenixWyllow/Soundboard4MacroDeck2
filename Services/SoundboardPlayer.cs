using Soundboard4MacroDeck.Models;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.Server;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Variables;

namespace Soundboard4MacroDeck.Services;

public static class SoundboardPlayer
{
    public static void Execute(SoundboardActions action, string config, ActionButton actionButton)
    {
        var actionParameters = ActionParametersV2.Deserialize(config);

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
            foreach (var player in ActivePlaybackEngines.ToArray().Where(p => p.MatchesInternalId(internalId)))
            {
                player?.Stop();
            }
            
        }
    }

    private static readonly object key = new();
    private static List<SoundboardPlaybackEngine> ActivePlaybackEngines { get; } = new();

    private static void OnPlaybackStopped(object? sender, EventArgs _)
    {
        var playbackEngine = (SoundboardPlaybackEngine)sender!;
        ActivePlaybackEngines.Remove(playbackEngine);
        SetVariables(playbackEngine, true);
        playbackEngine?.Dispose();
    }

    private static void Play(SoundboardActions action, ActionParametersV2 actionParameters, ActionButton actionButton)
    {
        switch (action)
        {
            case SoundboardActions.Play:
                StopAll();
                PlayAudio(actionParameters, actionButton, useVars: true);
                break;

            case SoundboardActions.Overlap:
                PlayAudio(actionParameters, actionButton);
                break;

            case SoundboardActions.PlayStop:
                PlayOrStop(actionParameters, actionButton, useVars: true);
                break;

            case SoundboardActions.Loop:
                PlayOrStop(actionParameters, actionButton, enableLoop: true);
                break;

            case SoundboardActions.RandomFromCategory:
                ApplyRandom(actionParameters, actionButton);
                StopAll();
                PlayAudio(actionParameters, actionButton);
                break;

            default:
                break;
        }

    }

    private static readonly Dictionary<string, int> lastRandSoundIds = [];

    private static void ApplyRandom(ActionParametersV2 actionParameters, ActionButton actionButton)
    {
        actionParameters.FileData = null;
        var files = PluginInstance.DbContext.GetAudioFileItems(actionParameters.AudioCategoryId);
        if (files.Count == 0)
        {
            MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardPlayer), "Category not found or no audio files found in the selected category.");
            return;
        }

        var chosen = Random.Shared.Next(files.Count);
        if (actionParameters.EnsureUniqueRandomSound)
        {
            if (!lastRandSoundIds.TryGetValue(actionButton.Guid, out var lastUsedId))
            {
                lastUsedId = -1;
            }
            while (chosen == lastUsedId)
            {
                chosen = Random.Shared.Next(files.Count);
            }
            lastRandSoundIds[actionButton.Guid] = chosen;
        }
        var audio = files[chosen];
        actionParameters.AudioFileId = audio.Id;
        actionParameters.FileName = audio.Name;
    }

    private static void PlayOrStop(ActionParametersV2 actionParameters, ActionButton actionButton, bool enableLoop = false, bool useVars = false)
    {
        bool currentlyPlaying = ActivePlaybackEngines.Any(p => p.MatchesInternalId(actionButton.Guid));
        if (currentlyPlaying)
        {
            StopCurrent(actionButton.Guid);
        }
        else
        {
            StopAll();
            PlayAudio(actionParameters, actionButton, enableLoop, useVars);
        }
    }

    private static void PlayAudio(ActionParametersV2 actionParameters, ActionButton actionButton, bool enableLoop = false, bool useVars = false)
    {
        actionParameters.FileData ??= PluginInstance.DbContext.FindAudioFile(actionParameters.AudioFileId)?.Data;
        if (actionParameters.FileData is null)
        {
            if (actionParameters.AudioCategoryId <= 0)
            {
                MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardPlayer), "Audio file not found. Cannot play sound.");
            }
            return;
        }
        var playbackEngine = new SoundboardPlaybackEngine(actionParameters, actionButton.Guid, enableLoop, useVars);
        if (actionParameters.SyncButtonState)
        {
            playbackEngine.PlaybackStopped += (_, _) => MacroDeckServer.SetState(actionButton, false);
            MacroDeckServer.SetState(actionButton, true);
        }

        playbackEngine.Elapsed += PlaybackEngine_Elapsed;
        playbackEngine.PlaybackStopped += OnPlaybackStopped;

        ActivePlaybackEngines.Add(playbackEngine);

        playbackEngine.Play();
    }

    private static void PlaybackEngine_Elapsed(object? sender, EventArgs e)
    {
        if (sender is not null and SoundboardPlaybackEngine playbackEngine)
        {
            SetVariables(playbackEngine);
        }
    }

    public static void SetVariables(SoundboardPlaybackEngine playbackEngine, bool reset = false)
    {
        if (!playbackEngine.HasTimeOutput)
        {
            return;
        }
        TimeSpan time = reset ? TimeSpan.Zero : playbackEngine.CurrentTime;
        TimeSpan remain = reset ? TimeSpan.Zero : (playbackEngine.TotalTime - playbackEngine.CurrentTime);
        VariableManager.SetValue(playbackEngine.GetReaderId("_elapsed"), time.ToString(@"mm\:ss"), VariableType.String, PluginInstance.Current, null);
        VariableManager.SetValue(playbackEngine.GetReaderId("_remains"), remain.ToString(@"mm\:ss"), VariableType.String, PluginInstance.Current, null);
    }
}