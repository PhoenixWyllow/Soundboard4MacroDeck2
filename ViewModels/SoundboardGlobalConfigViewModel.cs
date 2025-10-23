using Soundboard4MacroDeck.Base;
using Soundboard4MacroDeck.Models;
using Soundboard4MacroDeck.Services;

using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;

using SystemIOFile = System.IO.File;

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
            List<AudioFileItem> items = [];
            var files = PluginInstance.DbContext.GetAudioFiles();
            foreach (var file in files)
            {
                items.Add(file.ToAudioFileItem());
            }
            return items;
        }
    }

    public IList<AudioCategory> AudioCategories => PluginInstance.DbContext.GetAudioCategories();

    public AudioFile? LastAudioFile { get; internal set; }

    /// <summary>
    /// Validates and fixes audio files with invalid category references.
    /// Returns the number of files that were fixed.
    /// </summary>
    public int ValidateAndFixAudioFileCategories()
    {
        var audioFiles = PluginInstance.DbContext.GetAudioFiles();
        var validCategoryIds = AudioCategories.Select(c => c.Id).ToHashSet();
        int fixedCount = 0;

        foreach (var file in audioFiles)
        {
            if (file.CategoryId != 0 && !validCategoryIds.Contains(file.CategoryId))
            {
                MacroDeckLogger.Warning(PluginInstance.Current, typeof(SoundboardGlobalConfigViewModel), 
                    $"Audio file '{file.Name}' (ID: {file.Id}) has invalid CategoryId: {file.CategoryId}. Resetting to 0 (uncategorized).");

                file.CategoryId = 0;
                PluginInstance.DbContext.UpdateAudioFile(file);
                fixedCount++;
            }
        }

        return fixedCount;
    }

    /// <summary>
    /// Adds a new audio category to the database.
    /// </summary>
    /// <returns>True if the category was added successfully, false otherwise.</returns>
    public bool AddAudioCategory(AudioCategory newCategory)
    {
        try
        {
            PluginInstance.DbContext.InsertAudioCategory(newCategory);
            return true;
        }
        catch (Exception ex)
        {
            MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardGlobalConfigViewModel),
                $"Failed to add audio category: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Deletes an audio category from the database after validation.
    /// </summary>
    /// <param name="category">The category to delete.</param>
    /// <returns>True if the category was deleted successfully, false otherwise.</returns>
    public bool DeleteAudioCategory(AudioCategory category)
    {
        if (!CanDeleteAudioCategory(category))
        {
            return false;
        }

        try
        {
            return PluginInstance.DbContext.DeleteAudioCategory(category.Id);
        }
        catch (Exception ex)
        {
            MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardGlobalConfigViewModel),
                $"Failed to delete audio category '{category.Name}': {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Deletes an audio file from the database after validation.
    /// </summary>
    /// <param name="audioFile">The audio file to delete.</param>
    /// <returns>True if the file was deleted successfully, false otherwise.</returns>
    public bool DeleteAudioFile(AudioFileItem audioFile)
    {
        if (!CanDeleteAudioFile(audioFile))
        {
            return false;
        }

        try
        {
            return PluginInstance.DbContext.DeleteAudioFile(audioFile.Id);
        }
        catch (Exception ex)
        {
            MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardGlobalConfigViewModel),
                $"Failed to delete audio file '{audioFile.Name}': {ex.Message}");
            return false;
        }
    }

    public void UpdateAudioFile(AudioFileItem editedItem)
    {
        var file = PluginInstance.DbContext.FindAudioFile(editedItem.Id);
        if (file is null)
        {
            MacroDeckLogger.Warning(PluginInstance.Current, typeof(SoundboardGlobalConfigViewModel),
                $"Audio file with ID {editedItem.Id} not found in database.");
            return;
        }

        file.Name = editedItem.Name;
        file.CategoryId = editedItem.CategoryId;
        PluginInstance.DbContext.UpdateAudioFile(file);
    }

    internal void UpdateCategory(AudioCategory editedCategory)
    {
        var audioCategory = PluginInstance.DbContext.FindAudioCategory(editedCategory.Id);
        if (audioCategory is null)
        {
            MacroDeckLogger.Warning(PluginInstance.Current, typeof(SoundboardGlobalConfigViewModel),
                $"Audio category with ID {editedCategory.Id} not found in database.");
            return;
        }

        audioCategory.Name = editedCategory.Name;
        PluginInstance.DbContext.UpdateAudioCategory(audioCategory);
    }

    public AudioFile? GetBytesFromFile(string filePath)
    {
        byte[]? data = null;
        if (SystemIOFile.Exists(filePath))
        {
            data = SystemIOFile.ReadAllBytes(filePath);
        }

        return TryApplyFile(data, filePath);
    }

    public async Task<AudioFile?> GetFromUrlAsync(string urlPath, IProgress<float> progress, CancellationToken cancellationToken)
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

    private static AudioFile? TryApplyFile(byte[]? data, string path)
    {
        if (data is not null
            && AudioFileTypes.IsValidFile(data, out string extension))
        {
            //if match has extension in IncorrectHeaders, notify the user
            if (AudioFileTypes.IncorrectHeaders.Any(h => h.ExtensionsArray.Contains(extension)))
            {
                using var messageBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
                messageBox.ShowDialog(string.Empty, LocalizationManager.Instance.GlobalConfigIncorrectFileHeader + $" ({extension})", MessageBoxButtons.OK);
            }
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

    private static string GetFileName(string path, string? ext)
    {
        return string.IsNullOrWhiteSpace(ext)
            ? Path.GetFileName(path)
            : Path.GetFileNameWithoutExtension(path) + ext[1..];
    }

    public bool CanDeleteAudioCategory(AudioCategory? category)
    {
        if (category is null)
        {
            return false;
        }
        bool inUse = AudioFiles.Any(f => f.CategoryId == category.Id);
        if (inUse)
        {
            using var messageBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
            messageBox.ShowDialog(string.Empty, LocalizationManager.Instance.CategoryRemoveErrorLabel, MessageBoxButtons.OK);
            return false;
        }

        return ConfirmDelete(category.Name);
    }

    public bool CanDeleteAudioFile(AudioFileItem? audioFile)
    {
        if (audioFile is null)
        {
            return false;
        }
        return ConfirmDelete(audioFile.Name);
    }

    private static bool ConfirmDelete(string name)
    {
        using var confirmMessageBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
        var confirmResult = confirmMessageBox.ShowDialog(
            string.Format(LocalizationManager.Instance.ConfirmDeleteLabel, name),
            LocalizationManager.Instance.ConfirmDeleteWarningLabel,
            MessageBoxButtons.YesNo);
        return (confirmResult == DialogResult.Yes);
    }
}