using System.Text.Json.Serialization;

namespace Soundboard4MacroDeck.Models;

internal sealed class Localization
{
    [JsonPropertyName("_attribution_")]
    public string Attribution { get; set; } = "built-in values";
    [JsonPropertyName("_language_")]
    public string Language { get; set; } = "English (default)";
    public string Soundboard4MacroDeckDescription { get; set; } = "A soundboard plugin for Macro Deck 2";
    public string ActionPlaySoundName { get; set; } = "Play sound";
    public string ActionPlaySoundDescription { get; set; } = "Plays the configured file";
    public string ActionPlaySoundVolume { get; set; } = "Volume";
    public string ActionPlaySoundFilePath { get; set; } = "File path";
    public string ActionPlaySoundFilePathPlaceholder { get; set; } = "Get local file";
    public string ActionPlaySoundFileBrowse { get; set; } = "Browse";
    public string ActionPlaySoundURLGet { get; set; } = "Get from URL";
    public string ActionPlaySoundURLFile { get; set; } = "Direct URL of audio file";
    public string ActionPlaySoundInvalidFile { get; set; } = "Invalid File";
    public string ActionPlaySoundFileCouldNotUseFile { get; set; } = "Could not use file. Please check the path is valid and try again.";
    public string ActionPlaySoundURLCouldNotUseFile { get; set; } = "Could not use file. Please check the link and your connection and try again.";
    public string GenericLabelOr { get; set; } = "or";
    public string OutputDevicesGlobal { get; set; } = "Default output device";
    public string OutputDevicesAction { get; set; } = "Default output device for button";
    public string UseSystemDefaultDevice { get; set; } = "Use system default device";
    public string OverrideDefaultDevice { get; set; } = "Use custom output device";
    public string ActionPlayStopSoundName { get; set; } = "Play/Stop sound";
    public string ActionPlayStopSoundDescription { get; set; } = "Play the configured file on first tap and Stop on second tap";
    public string ActionOverlapSoundName { get; set; } = "Overlapping audio";
    public string ActionOverlapSoundDescription { get; set; } = "Play the configured file and overlap other audio";
    public string ActionLoopSoundName { get; set; } = "Looping audio";
    public string ActionLoopSoundDescription { get; set; } = "Play the configured file on loop until it is stopped on second tap";
    public string ActionSuggestButtonStates { get; set; } = "Setup of the button's active and inactive states is recommended for this action";
    public string ActionStopSoundName { get; set; } = "Stop all sounds";
    public string ActionStopSoundDescription { get; set; } = "Forces stop of all currently playing sounds";
    public string SyncButtonState { get; set; } = "Sync button state with audio";
}