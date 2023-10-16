using Soundboard4MacroDeck.Models;
using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.ViewModels;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        if (openFileDialog.ShowDialog(ParentForm).Equals(DialogResult.OK))
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
        _viewModel.LastAudioFile.Id = id;

        DialogResult = DialogResult.OK;
        Close();
    }
}
