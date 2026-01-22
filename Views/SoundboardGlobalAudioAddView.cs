using Soundboard4MacroDeck.Models;
using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.ViewModels;

using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Logging;

namespace Soundboard4MacroDeck.Views;
public partial class SoundboardGlobalAudioAddView : DialogForm
{
    private readonly SoundboardGlobalConfigViewModel _viewModel;
    private bool fromUrl = false;

    public SoundboardGlobalAudioAddView(SoundboardGlobalConfigViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();
        SetCloseIconVisible(true);
        ApplyLocalization();
    }

    private void ApplyLocalization()
    {
        Text = "Soundboard - " + LocalizationManager.Instance.GlobalConfigAddAudio;
        buttonGetFromURL.Text = LocalizationManager.Instance.ActionPlaySoundURLGet;
        fileBrowse.Text = LocalizationManager.Instance.ActionPlaySoundFileBrowse;
        filePath.PlaceHolderText = LocalizationManager.Instance.ActionPlaySoundFilePathPlaceholder;
        labelFile.Text = LocalizationManager.Instance.ActionPlaySoundFilePath;
        labelOr.Text = LocalizationManager.Instance.GenericLabelOr;
        buttonOK.Text = LanguageManager.Strings.Ok;
    }

    private void SoundboardGlobalAudioAddView_Load(object sender, EventArgs e)
    {
        // openFileDialog
        string types = string.Join(';', Base.AudioFileTypes.Extensions);
        openFileDialog.Filter = @$"Audio File ({types})|{types}";
        openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }

    private void FileBrowse_Click(object sender, EventArgs e)
    {
        openFileDialog.Multiselect = true;
        if (openFileDialog.ShowDialog(this) == DialogResult.OK)
        {
            fromUrl = false;
            if (openFileDialog.FileNames.Length == 1)
            {
                filePath.Text = openFileDialog.FileName;
                return;  // Single: OK_Click handles normally
            }
            else
            {
                filePath.Text = string.Join("█", openFileDialog.FileNames);  // PATH█PATH█PATH
                return;  // Multi: OK_Click processes all
            }
        }
    }

    private void FilePath_TextChanged(object sender, EventArgs e)
    {
        if (!fromUrl)
        {
            _viewModel.LastAudioFile = _viewModel.GetBytesFromFile(filePath.Text);
        }
    }

    private void ButtonGetFromURL_Click(object sender, EventArgs e)
    {
        using var getFromUrlDialog = new GetFileFromWebView(_viewModel);
        if (getFromUrlDialog.ShowDialog(this) == DialogResult.OK)
        {
            if (_viewModel.LastAudioFile is null)
            {
                MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), "Audio file cannot be added as there is no valid audio present.");
                return;
            }
            fromUrl = true;
            filePath.Text = _viewModel.LastAudioFile.Name;
        }
    }

    private void ButtonOK_Click(object sender, EventArgs e)
    {
        // Handle █ paths → populate LOCAL batch list
        List<AudioFile> batchFiles = new();
        string[] paths = filePath.Text.Split('█', StringSplitOptions.RemoveEmptyEntries);
        foreach(string path in paths)
        {
            var file = _viewModel.GetBytesFromFile(path.Trim());
            if (file != null) 
            {
                file.CategoryId = AudioCategory.NoneOrUncategorized.Id;
                batchFiles.Add(file);
            }
        }
        // Process ALL files in IMMEDIATELY
        foreach(var audioFile in batchFiles)
        {
            if (audioFile is null) continue;
            audioFile.CategoryId = AudioCategory.NoneOrUncategorized.Id;
            var id = PluginInstance.DbContext.InsertAudioFile(audioFile);
            using AudioReader reader = new(audioFile.Name, audioFile.Data, false);
            SuchByte.MacroDeck.Variables.VariableManager.SetValue($"sb_{id}", reader.TotalTime.ToString(@"mm\:ss"), SuchByte.MacroDeck.Variables.VariableType.String, PluginInstance.Current, null);
            audioFile.Id = id;
        }
        if (this.Owner is SoundboardGlobalConfigViewV2 parentView)
        {
            parentView.RefreshAudioFilesPage();  // Refresh UI from DB
        }
        DialogResult = DialogResult.OK;
        Close();
    }
}
