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

            actionConfigurator.ActionSave += OnActionSave;
        }

        private void InitMore()
        {
            // openFileDialog
            var types = $"*.{string.Join(";*.", Services.SoundPlayer.Extensions)}";
            this.openFileDialog.Filter = $"Audio File ({types})|{types}";
            this.openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // filePath
            filePath.Text = _viewModel.LastCheckedPath;
            checkedFile = string.IsNullOrWhiteSpace(_viewModel.LastCheckedPath);
        }

        private async void OnActionSave(object sender, EventArgs e)
        {
            if (!checkedFile)
            {
                await CheckFile();
            }

            _viewModel.SaveConfig();
        }

        private async Task CheckFile()
        {
            bool hasFile = !checkedFile
                && (filePath.Text.Equals(_viewModel.LastCheckedPath)
                || await _viewModel.GetBytesFromFile(filePath.Text));
            if (!hasFile)
            {
                filePath.Text = _viewModel.LastCheckedPath;
                using var messageBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
                messageBox.ShowDialog("InvalidFile", "CouldNotUseFile", MessageBoxButtons.OK);
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
                await CheckFile();
            }
        }

        private void FilePath_TextChanged(object sender, EventArgs e)
        {
            checkedFile = false;
        }
    }
}
