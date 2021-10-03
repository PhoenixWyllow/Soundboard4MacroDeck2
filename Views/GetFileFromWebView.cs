using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.ViewModels;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using System;
using System.Windows.Forms;

namespace Soundboard4MacroDeck.Views
{
    public partial class GetFileFromWebView : DialogForm
    {
        private readonly SoundboardActionConfigViewModel _viewModel;
        private bool checkedFile;
        public GetFileFromWebView(SoundboardActionConfigViewModel parentViewModel)
        {
            _viewModel = parentViewModel;

            InitializeComponent();
            ApplyLocalization();

            urlBox.Text = _viewModel.LastCheckedPath;
            checkedFile = string.IsNullOrWhiteSpace(_viewModel.LastCheckedPath);
        }

        private void ApplyLocalization()
        {
            this.labelURLFile.Text = Localization.Instance.ActionPlaySoundURLFile;
            this.buttonOK.Text = LanguageManager.Strings.Ok;
        }

        private async void ButtonOK_Click(object sender, EventArgs e)
        {
            bool hasFile = !checkedFile
                && (urlBox.Text.Equals(_viewModel.LastCheckedPath)
                || await _viewModel.GetFromUrl(urlBox.Text, fileProgressBar));
            if (!hasFile)
            {
                urlBox.Text = _viewModel.LastCheckedPath;
                using var messageBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
                messageBox.ShowDialog(Localization.Instance.ActionPlaySoundInvalidFile, Localization.Instance.ActionPlaySoundURLCouldNotUseFile, MessageBoxButtons.OK);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void UrlBox_TextChanged(object sender, EventArgs e)
        {
            checkedFile = false;
        }
    }
}
