using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Soundboard4MacroDeck.Base;
using Soundboard4MacroDeck.Models;
using Soundboard4MacroDeck.Services;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SystemIOFile = System.IO.File;

namespace Soundboard4MacroDeck.ViewModels;

public class SoundboardActionConfigViewModel : OutputDeviceConfigurationViewModel
{
    private readonly PluginAction _action;
    private ActionParametersV2 Parameters => OutputConfiguration as ActionParametersV2;

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
    }

    public override void SetConfig()
    {
        _action.ConfigurationSummary = Parameters.FileName;
        _action.Configuration = Parameters.Serialize();
    }

    private AudioFile selectedAudioFile;
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
}