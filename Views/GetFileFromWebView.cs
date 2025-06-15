using Soundboard4MacroDeck.Models;
using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.ViewModels;

using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;

namespace Soundboard4MacroDeck.Views;

public partial class GetFileFromWebView : DialogForm
{
    private readonly SoundboardGlobalConfigViewModel _viewModel;
    public GetFileFromWebView(SoundboardGlobalConfigViewModel parentViewModel)
    {
        _viewModel = parentViewModel;

        InitializeComponent();
        ApplyLocalization();
    }

    private void ApplyLocalization()
    {
        labelURLFile.Text = LocalizationManager.Instance.ActionPlaySoundURLFile;
        buttonOK.Text = LanguageManager.Strings.Ok;
    }

    private async void ButtonOK_Click(object sender, EventArgs e)
    {
        Progress<float> progress = new(progress => buttonOK.Progress = (int)progress);
        AudioFile? audioFile = await _viewModel.GetFromUrlAsync(urlBox.Text, progress, new CancellationTokenSource().Token); //using Macro Deck PrimaryButton as numeric progress bar/indicator
        if (audioFile is null)
        {
            using var messageBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
            messageBox.ShowDialog(LocalizationManager.Instance.ActionPlaySoundInvalidFile, LocalizationManager.Instance.ActionPlaySoundURLCouldNotUseFile, MessageBoxButtons.OK);
            return;
        }

        _viewModel.LastAudioFile = audioFile;
        DialogResult = DialogResult.OK;
        Close();
    }
}