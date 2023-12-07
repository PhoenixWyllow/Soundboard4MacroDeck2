using Soundboard4MacroDeck.Base;
using Soundboard4MacroDeck.Models;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using SystemIOFile = System.IO.File;
using Soundboard4MacroDeck.Services;

namespace Soundboard4MacroDeck.ViewModels;

public class SoundboardGlobalConfigViewModel : OutputDeviceConfigurationViewModel
{
    private readonly MacroDeckPlugin _plugin;
    public SoundboardGlobalConfigViewModel(MacroDeckPlugin plugin)
        : base(GlobalParameters.Deserialize(PluginConfiguration.GetValue(plugin, nameof(SoundboardGlobalConfigViewModel))))
    {
        _plugin = plugin;
    }


    public override void SetConfig()
    {
        PluginConfiguration.SetValue(_plugin, nameof(SoundboardGlobalConfigViewModel), OutputConfiguration.Serialize());
    }

    public IList<AudioFileItem> AudioFiles
    {
        get
        {
            List<AudioFileItem> items = new();
            var files = PluginInstance.DbContext.GetAudioFiles();
            foreach (var file in files)
            {
                items.Add(file.ToAudioFileItem());
            }
            return items;
        }
    }

    public IList<AudioCategory> AudioCategories => PluginInstance.DbContext.GetAudioCategories();

    public AudioFile LastAudioFile { get; internal set; }

    public void UpdateAudioFile(AudioFileItem editedItem)
    {
        var file = PluginInstance.DbContext.FindAudioFile(editedItem.Id);
        file.Name = editedItem.Name;
        file.CategoryId = editedItem.CategoryId;
        PluginInstance.DbContext.UpdateAudioFile(file);
    }

    internal void UpdateCategory(AudioCategory editedCategory)
    {
        var audioCategory = PluginInstance.DbContext.FindAudioCategory(editedCategory.Id);
        audioCategory.Name = editedCategory.Name;
        PluginInstance.DbContext.UpdateAudioCategory(audioCategory);
    }

    public AudioFile GetBytesFromFile(string filePath)
    {
        byte[] data = null;
        if (SystemIOFile.Exists(filePath))
        {
            data = SystemIOFile.ReadAllBytes(filePath);
        }

        return TryApplyFile(data, filePath);
    }

    public async Task<AudioFile> GetFromUrlAsync(string urlPath, IProgress<float> progress, CancellationToken cancellationToken)
    {
        try
        {
            using HttpClient client = new();
            client.Timeout = TimeSpan.FromMinutes(1);

            // Use the custom extension method to download the data.
            // The passed progress-instance will receive the download status updates.
            var file = await client.DownloadBytesAsync(urlPath, progress, cancellationToken).ConfigureAwait(false);
            return TryApplyFile(file, urlPath);
        }
        catch (Exception ex)
        {
            //forbidden, proxy issues, file not found (404) etc
            MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardGlobalConfigViewModel), $"{nameof(GetFromUrlAsync)}: {ex.Message}");
        }

        return null;
    }

    private static AudioFile TryApplyFile(byte[] data, string path)
    {
        if (data != null
            && AudioFileTypes.IsValidFile(data, out string extension))
        {
            var ext = AudioFileTypes.Extensions.FirstOrDefault(ext => ext.EndsWith(extension));

            AudioFile audioFile = new()
            {
                Data = data,
                Name = GetFileName(path, ext),
            };
            return audioFile;
        }
        return null;
    }

    private static string GetFileName(string path, string ext)
    {
        return string.IsNullOrWhiteSpace(ext)
            ? System.IO.Path.GetFileName(path)
            : System.IO.Path.GetFileNameWithoutExtension(path) + ext[1..];
    }
}