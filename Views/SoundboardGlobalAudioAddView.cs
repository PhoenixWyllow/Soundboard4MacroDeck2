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
        string types = $"{string.Join(";", Base.AudioFileTypes.Extensions)}";
        openFileDialog.Filter = @$"Audio File ({types})|{types}";
        openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }

    private void FileBrowse_Click(object sender, EventArgs e)
    {
        if (openFileDialog.ShowDialog(this) == DialogResult.OK)
        {
            fromUrl = false;
            filePath.Text = openFileDialog.FileName;
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
        if (_viewModel.LastAudioFile is null)
        {
            using var messageBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
            messageBox.ShowDialog(LocalizationManager.Instance.ActionPlaySoundInvalidFile,
                !fromUrl ? LocalizationManager.Instance.ActionPlaySoundFileCouldNotUseFile : LocalizationManager.Instance.ActionPlaySoundURLCouldNotUseFile,
                MessageBoxButtons.OK);
            return;
        }

        _viewModel.LastAudioFile.CategoryId = 1;
        var id = PluginInstance.DbContext.InsertAudioFile(_viewModel.LastAudioFile);
        using AudioReader reader = new(_viewModel.LastAudioFile.Name, _viewModel.LastAudioFile.Data, false);
        SuchByte.MacroDeck.Variables.VariableManager.SetValue($"sb_{id}", reader.TotalTime.ToString(@"mm\:ss"), SuchByte.MacroDeck.Variables.VariableType.String, PluginInstance.Current, null);
        _viewModel.LastAudioFile.Id = id;

        DialogResult = DialogResult.OK;
        Close();
    }
}
