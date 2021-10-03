using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.ViewModels;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Soundboard4MacroDeck.Views
{
    public partial class SoundboardActionConfigView : ActionConfigControl
    {
        private readonly SoundboardActionConfigViewModel _viewModel;
        private bool checkedFile = false;

        public SoundboardActionConfigView(IMacroDeckAction action, ActionConfigurator actionConfigurator)
        {
            _viewModel = new SoundboardActionConfigViewModel(action);

            InitializeComponent();
            InitMore();
            ApplyLocalization();

            actionConfigurator.ActionSave += OnActionSave;
        }
        private void ApplyLocalization()
        {
            this.buttonGetFromURL.Text = Localization.Instance.ActionPlaySoundURLGet;
            this.fileBrowse.Text = Localization.Instance.ActionPlaySoundFileBrowse;
            this.filePath.PlaceholderText = Localization.Instance.ActionPlaySoundFilePathPlaceholder;
            this.labelFile.Text = Localization.Instance.ActionPlaySoundFilePath;
            this.labelVolume.Text = Localization.Instance.ActionPlaySoundVolume;
            this.labelOr.Text = Localization.Instance.GenericLabelOr;
        }

        private void InitMore()
        {
            // openFileDialog
            string types = $"{string.Join(";", Models.AudioFileTypes.Extensions)}";
            this.openFileDialog.Filter = $"Audio File ({types})|{types}";
            this.openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // filePath
            filePath.Text = _viewModel.LastCheckedPath;
            volumeBar.Value = _viewModel.PlayVolume;
            checkedFile = string.IsNullOrWhiteSpace(_viewModel.LastCheckedPath);
        }

        private async void OnActionSave(object sender, EventArgs e)
        {
            if (!checkedFile)
            {
                await CheckFileAsync();
            }

            _viewModel.SaveConfig();
        }

        private async Task CheckFileAsync()
        {
            bool hasFile = !checkedFile
                && (filePath.Text.Equals(_viewModel.LastCheckedPath)
                || await _viewModel.GetBytesFromFileAsync(filePath.Text));
            if (!hasFile)
            {
                filePath.Text = _viewModel.LastCheckedPath;
                using var messageBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
                messageBox.ShowDialog(Localization.Instance.ActionPlaySoundInvalidFile, Localization.Instance.ActionPlaySoundFileCouldNotUseFile, MessageBoxButtons.OK);
                return;
            }
            checkedFile = true;
        }

        private async void FileBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this.ParentForm).Equals(DialogResult.OK))
            {
                checkedFile = false;
                filePath.Text = openFileDialog.FileName;
                await CheckFileAsync();
            }
        }

        private void FilePath_TextChanged(object sender, EventArgs e)
        {
            checkedFile = false;
        }

        private void ButtonGetFromURL_Click(object sender, EventArgs e)
        {
            using var getFromURLDialog = new GetFileFromWebView(_viewModel);
            if (getFromURLDialog.ShowDialog(this.ParentForm) == DialogResult.OK)
            {
                filePath.Text = _viewModel.LastCheckedPath;
                checkedFile = true;
            }
        }

        private void VolumeBar_ValueChanged(object sender, EventArgs e)
        {
            if (volumeNum.Value != volumeBar.Value)
            {
                volumeNum.Value = volumeBar.Value;
            }
            _viewModel.PlayVolume = volumeBar.Value;
        }

        private void VolumeNum_ValueChanged(object sender, EventArgs e)
        {
            if (volumeBar.Value != volumeNum.Value)
            {
                volumeBar.Value = (int)volumeNum.Value;
            }
        }
    }
}
