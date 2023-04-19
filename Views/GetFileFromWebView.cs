using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.ViewModels;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Soundboard4MacroDeck.Views;

public partial class GetFileFromWebView : DialogForm
{
    private readonly SoundboardActionConfigViewModel _viewModel;
    private bool _checkedFile;
    public GetFileFromWebView(SoundboardActionConfigViewModel parentViewModel)
    {
        _viewModel = parentViewModel;

        InitializeComponent();
        ApplyLocalization();

        urlBox.Text = _viewModel.LastCheckedPath;
        _checkedFile = string.IsNullOrWhiteSpace(_viewModel.LastCheckedPath);
    }

    private void ApplyLocalization()
    {
        this.labelURLFile.Text = LocalizationManager.Instance.ActionPlaySoundURLFile;
        this.buttonOK.Text = LanguageManager.Strings.Ok;
    }

    private async void ButtonOK_Click(object sender, EventArgs e)
    {
        Progress<float> progress = new(progress => buttonOK.Progress = (int)progress);
        bool hasFile = !_checkedFile
                       && (urlBox.Text.Equals(_viewModel.LastCheckedPath)
                           || await _viewModel.GetFromUrlAsync(urlBox.Text, progress, new CancellationTokenSource().Token)); //using Macro Deck PrimaryButton as numeric progress bar/indicator
        if (!hasFile)
        {
            urlBox.Text = _viewModel.LastCheckedPath;
            using var messageBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
            messageBox.ShowDialog(LocalizationManager.Instance.ActionPlaySoundInvalidFile, LocalizationManager.Instance.ActionPlaySoundURLCouldNotUseFile, MessageBoxButtons.OK);
            return;
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private void UrlBox_TextChanged(object sender, EventArgs e)
    {
        _checkedFile = false;
    }
}