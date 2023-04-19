using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.ViewModels;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Windows.Forms;

namespace Soundboard4MacroDeck.Views;

public partial class SoundboardActionConfigView : ActionConfigControl
{
    private readonly SoundboardActionConfigViewModel _viewModel;
    private bool _checkedFile = false;

    public SoundboardActionConfigView(PluginAction action)
    {
        _viewModel = new(action);

        InitializeComponent();
        ApplyLocalization();

        _viewModel.OnSetDeviceIndex += (_, _) => { comboBoxDevices.SelectedIndex = _viewModel.DevicesIndex; };

    }
    private void ApplyLocalization()
    {
        checkBoxOverrideDevice.Text = LocalizationManager.Instance.OverrideDefaultDevice;
        checkBoxSyncButtonState.Text = LocalizationManager.Instance.SyncButtonState;
        labelDevices.Text = LocalizationManager.Instance.OutputDevicesAction;
        buttonGetFromURL.Text = LocalizationManager.Instance.ActionPlaySoundURLGet;
        fileBrowse.Text = LocalizationManager.Instance.ActionPlaySoundFileBrowse;
        filePath.PlaceHolderText = LocalizationManager.Instance.ActionPlaySoundFilePathPlaceholder;
        labelFile.Text = LocalizationManager.Instance.ActionPlaySoundFilePath;
        labelVolume.Text = LocalizationManager.Instance.ActionPlaySoundVolume;
        labelOr.Text = LocalizationManager.Instance.GenericLabelOr;
    }

    public override bool OnActionSave()
    {
        if (!_checkedFile)
        {
            CheckFileAsync();
        }

        _viewModel.SaveConfig();

        return _checkedFile;
    }

    private void SoundboardActionConfigView_Load(object sender, EventArgs e)
    {
        checkBoxSyncButtonState.Checked = _viewModel.SyncButtonState;
        volumeBar.Value = _viewModel.PlayVolume;

        //devices
        _viewModel.LoadDevices();
        comboBoxDevices.Items.AddRange(_viewModel.Devices.ToArray());
        _viewModel.LoadDeviceIndex();

        checkBoxOverrideDevice.Checked = !_viewModel.IsDefaultDevice();

        // openFileDialog
        string types = $"{string.Join(";", Base.AudioFileTypes.Extensions)}";
        openFileDialog.Filter = @$"Audio File ({types})|{types}";
        openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // filePath
        if (!string.IsNullOrWhiteSpace(_viewModel.LastCheckedPath))
        {
            filePath.Text = _viewModel.LastCheckedPath;
        }
        _checkedFile = string.IsNullOrWhiteSpace(_viewModel.LastCheckedPath);
    }

    private void CheckFileAsync()
    {
        bool hasFile = !_checkedFile
                       && (filePath.Text.Equals(_viewModel.LastCheckedPath)
                           || _viewModel.GetBytesFromFile(filePath.Text));
        if (!hasFile)
        {
            filePath.Text = _viewModel.LastCheckedPath;
            using var messageBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
            messageBox.ShowDialog(LocalizationManager.Instance.ActionPlaySoundInvalidFile, LocalizationManager.Instance.ActionPlaySoundFileCouldNotUseFile, MessageBoxButtons.OK);
            return;
        }
        _checkedFile = true;
    }

    private void FileBrowse_Click(object sender, EventArgs e)
    {
        if (openFileDialog.ShowDialog(ParentForm).Equals(DialogResult.OK))
        {
            _checkedFile = false;
            filePath.Text = openFileDialog.FileName;
            CheckFileAsync();
        }
    }

    private void FilePath_TextChanged(object sender, EventArgs e)
    {
        _checkedFile = false;
    }

    private void ButtonGetFromURL_Click(object sender, EventArgs e)
    {
        using var getFromUrlDialog = new GetFileFromWebView(_viewModel);
        if (getFromUrlDialog.ShowDialog(ParentForm) == DialogResult.OK)
        {
            filePath.Text = _viewModel.LastCheckedPath;
            _checkedFile = true;
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
        _viewModel.ResetDevice();
    }

    private void ComboBoxDevices_SelectedIndexChanged(object sender, EventArgs e)
    {
        _viewModel.SetDevice(comboBoxDevices.SelectedIndex);
    }

    private void CheckBoxSyncButtonState_CheckedChanged(object sender, EventArgs e)
    {
        _viewModel.SyncButtonState = checkBoxSyncButtonState.Checked;
    }
}