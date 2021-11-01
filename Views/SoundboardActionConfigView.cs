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

        public SoundboardActionConfigView(PluginAction action, ActionConfigurator actionConfigurator)
        {
            _viewModel = new SoundboardActionConfigViewModel(action);

            InitializeComponent();
            ApplyLocalization();

            _viewModel.OnSetDeviceIndex += (_, __) => { comboBoxDevices.SelectedIndex = _viewModel.DevicesIndex; };

            actionConfigurator.ActionSave += OnActionSave;
        }
        private void ApplyLocalization()
        {
            checkBoxOverrideDevice.Text = Localization.Instance.OverrideDefaultDevice;
            labelDevices.Text = Localization.Instance.OutputDevicesAction;
            buttonGetFromURL.Text = Localization.Instance.ActionPlaySoundURLGet;
            fileBrowse.Text = Localization.Instance.ActionPlaySoundFileBrowse;
            filePath.PlaceHolderText = Localization.Instance.ActionPlaySoundFilePathPlaceholder;
            labelFile.Text = Localization.Instance.ActionPlaySoundFilePath;
            labelVolume.Text = Localization.Instance.ActionPlaySoundVolume;
            labelOr.Text = Localization.Instance.GenericLabelOr;
        }

        private async void OnActionSave(object sender, EventArgs e)
        {
            if (!checkedFile)
            {
                await CheckFileAsync();
            }

            _viewModel.SaveConfig();
        }

        private void SoundboardActionConfigView_Load(object sender, EventArgs e)
        {
            _viewModel.LoadDevices();
            comboBoxDevices.Items.AddRange(_viewModel.Devices.ToArray());
            _viewModel.LoadDeviceIndex();
            checkBoxOverrideDevice.Checked = !_viewModel.IsDefaultDevice();

            // openFileDialog
            string types = $"{string.Join(";", Base.AudioFileTypes.Extensions)}";
            openFileDialog.Filter = $"Audio File ({types})|{types}";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // filePath
            if (!string.IsNullOrWhiteSpace(_viewModel.LastCheckedPath))
            {
                filePath.Text = _viewModel.LastCheckedPath;
            }
            volumeBar.Value = _viewModel.PlayVolume;
            checkedFile = string.IsNullOrWhiteSpace(_viewModel.LastCheckedPath);
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
            if (openFileDialog.ShowDialog(ParentForm).Equals(DialogResult.OK))
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
            if (getFromURLDialog.ShowDialog(ParentForm) == DialogResult.OK)
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

        private void CheckBoxOverrideDevice_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxDevices.Enabled = checkBoxOverrideDevice.Checked;
        }

        private void ComboBoxDevices_EnabledChanged(object sender, EventArgs e)
        {
            if (!comboBoxDevices.Enabled)
            {
                _viewModel.ResetDevice();
            }
        }

        private void ComboBoxDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            _viewModel.SetDevice(comboBoxDevices.SelectedIndex);
        }
    }
}
