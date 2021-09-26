using MacroDeckSoundboard.Models;
using MacroDeckSoundboard.ViewModels;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroDeckSoundboard.Views
{
    public partial class SoundboardActionConfigView : ActionConfigControl
    {
        private readonly SoundboardActionConfigViewModel _viewModel;

        public SoundboardActionConfigView(IMacroDeckAction action, ActionConfigurator actionConfigurator)
        {
            _viewModel = new SoundboardActionConfigViewModel(action, actionConfigurator);

            InitializeComponent();
            InitMore();
            this.Load += SoundboardActionConfig_Load;
            this.filePath.TextChanged += FilePath_TextChanged;
            actionConfigurator.ActionSave += OnActionSave;
        }

        private void InitMore()
        {
            // openFileDialog
            var types = $"*.{string.Join(";*.",Services.SoundPlayer.Extensions)};*.aac";
            this.openFileDialog.Filter = $"Audio File ({types})|{types}";
            this.openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }


        private void SoundboardActionConfig_Load(object sender, EventArgs e)
        {
        }

        private async void FilePath_TextChanged(object sender, EventArgs e)
        {
            bool hasFile = await _viewModel.GetBytesFrom(((TextBox)sender).Text, fileProgressBar);
            if (!hasFile)
            {
                ((TextBox)sender).Text = "";
                using var messageBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
                messageBox.ShowDialog("InvalidFile", "CouldNotUseFile", MessageBoxButtons.OK);
                return;
            }
        }

        private void OnActionSave(object sender, EventArgs e)
        {
            _viewModel.SaveConfig();
        }

    }
}
