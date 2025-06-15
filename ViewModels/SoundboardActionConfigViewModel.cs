using Soundboard4MacroDeck.Models;
using SuchByte.MacroDeck.Plugins;

namespace Soundboard4MacroDeck.ViewModels;

public class SoundboardActionConfigViewModel : OutputDeviceConfigurationViewModel
{
    private readonly PluginAction _action;

    public bool IsCategoryAction { get; }

    private ActionParametersV2 Parameters => (ActionParametersV2)OutputConfiguration;

    public int PlayVolume
    {
        get => Parameters.Volume;
        set => Parameters.Volume = value;
    }

    public bool SyncButtonState
    {
        get => Parameters.SyncButtonState;
        set => Parameters.SyncButtonState = value;
    }

    public SoundboardActionConfigViewModel(PluginAction action)
        : base(ActionParametersV2.Deserialize(action.Configuration))
    {
        _action = action;
        IsCategoryAction = action is Actions.SoundboardCategoryRandomAction;
    }

    public override void SetConfig()
    {
        _action.ConfigurationSummary = IsCategoryAction ? $"{SelectedAudioCategory.Id} - {SelectedAudioCategory.Name}" : $"{Parameters.AudioFileId} - {Parameters.FileName}";
        _action.Configuration = Parameters.Serialize();
    }

    private AudioFile? selectedAudioFile;
    public AudioFile SelectedAudioFile
    {
        get => selectedAudioFile ??= PluginInstance.DbContext.FindAudioFile(Parameters.AudioFileId);
        set
        {
            selectedAudioFile = value;
            Parameters.AudioFileId = value.Id;
            Parameters.FileName = value.Name;
            Parameters.FileData = value.Data;
        }
    }

    private AudioCategory? selectedAudioCategory;
    public AudioCategory SelectedAudioCategory
    {
        get => selectedAudioCategory ??= PluginInstance.DbContext.FindAudioCategory(Parameters.AudioCategoryId);
        set
        {
            selectedAudioCategory = value;
            Parameters.AudioCategoryId = value.Id;
        }
    }
}