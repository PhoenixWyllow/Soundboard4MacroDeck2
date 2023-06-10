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
    private ActionParameters Parameters => OutputConfiguration as ActionParameters;
    public string LastCheckedPath => Parameters.FilePath;

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
        : base(ActionParameters.Deserialize(action.Configuration))
    {
        _action = action;
    }

    public override void SetConfig()
    {
        _action.ConfigurationSummary = Parameters.FileName;
        _action.Configuration = Parameters.Serialize();
    }

    public bool GetBytesFromFile(string filePath)
    {
        byte[] data = null;
        if (SystemIOFile.Exists(filePath))
        {
            data = SystemIOFile.ReadAllBytes(filePath);
        }

        return TryApplyFile(data, filePath);
    }

    public async Task<bool> GetFromUrlAsync(string urlPath, IProgress<float> progress, CancellationToken cancellationToken)
    {
        bool success = false;
        try
        {
            using HttpClient client = new();
            client.Timeout = TimeSpan.FromMinutes(5);
        
            // Use the custom extension method to download the data.
            // The passed progress-instance will receive the download status updates.
            var file = await client.DownloadBytesAsync(urlPath, progress, cancellationToken).ConfigureAwait(false);
            success = TryApplyFile(file, urlPath);
        }
        catch (Exception ex)
        {
            //forbidden, proxy issues, file not found (404) etc
            MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardActionConfigViewModel), $"{nameof(GetFromUrlAsync)}: {ex.Message}");
        }

        return success;
    }

    private bool TryApplyFile(byte[] data, string urlPath)
    {
        if (data != null
            && AudioFileTypes.IsValidFile(data, out string extension))
        {
            Parameters.FileData = data;
            Parameters.FilePath = urlPath;
            Parameters.FileExt = AudioFileTypes.Extensions.FirstOrDefault(ext => ext.EndsWith(extension));
            return true;
        }
        return false;
    }

}