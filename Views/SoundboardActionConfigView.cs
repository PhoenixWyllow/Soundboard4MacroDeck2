using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.ViewModels;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Linq;
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
        labelVolume.Text = LocalizationManager.Instance.ActionPlaySoundVolume;
        labelFile.Text = LocalizationManager.Instance.ActionPlaySoundFilePath;
    }

    public override bool OnActionSave()
    {
        _viewModel.SaveConfig();

        return true;
    }

    private void SoundboardActionConfigView_Load(object sender, EventArgs e)
    {
        checkBoxSyncButtonState.Checked = _viewModel.SyncButtonState;
        volumeBar.Value = _viewModel.PlayVolume;

        //devices
        _viewModel.LoadDevices();
        comboBoxDevices.Items.AddRange(_viewModel.Devices.ToArray());
        _viewModel.LoadDeviceIndex();
        comboBoxAudio.Items.AddRange(PluginInstance.DbContext.GetAudioFiles().ToArray());
        if (_viewModel.SelectedAudioFile is not null)
        {
            comboBoxAudio.SelectedIndex = _viewModel.SelectedAudioFile.Id - 1;
        }

        checkBoxOverrideDevice.Checked = !_viewModel.IsDefaultDevice();
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

    private void ComboBoxAudio_SelectedIndexChanged(object sender, EventArgs e)
    {
        _viewModel.SelectedAudioFile = (Models.AudioFile)comboBoxAudio.SelectedItem;
    }
}