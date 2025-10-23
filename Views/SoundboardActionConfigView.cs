using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.ViewModels;

using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;

namespace Soundboard4MacroDeck.Views;

public partial class SoundboardActionConfigView : ActionConfigControl
{
    private readonly SoundboardActionConfigViewModel _viewModel;

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
        checkBoxEnsureUniqueRandom.Text = LocalizationManager.Instance.ActionCategoryRandomUnique;
        labelDevices.Text = LocalizationManager.Instance.OutputDevicesAction;
        labelVolume.Text = LocalizationManager.Instance.ActionPlaySoundVolume;
        labelFile.Text = LocalizationManager.Instance.ActionPlaySoundFilePath;
        labelCategory.Text = LocalizationManager.Instance.ActionCategoryAudioCategory;
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

        comboBoxAudio.Visible = labelFile.Visible = !_viewModel.IsCategoryAction;
        comboBoxCategory.Visible = labelCategory.Visible = _viewModel.IsCategoryAction;
        checkBoxEnsureUniqueRandom.Visible = _viewModel.IsCategoryAction;
        if (_viewModel.IsCategoryAction)
        {
            LoadAudioCategorySelection();
        }
        else
        {
            LoadAudioFileSelection();
        }

        checkBoxOverrideDevice.Checked = !_viewModel.IsDefaultDevice();
    }

    private void LoadAudioCategorySelection()
    {
        comboBoxCategory.Items.Clear();
        comboBoxCategory.Items.AddRange(PluginInstance.DbContext.GetAudioCategoriesArray());
        if (_viewModel.SelectedAudioCategory is not null)
        {
            comboBoxCategory.SelectedIndex = comboBoxCategory.Items.IndexOf(_viewModel.SelectedAudioCategory);
        }
    }

    private void LoadAudioFileSelection()
    {
        comboBoxAudio.Items.Clear();
        comboBoxAudio.Items.AddRange(PluginInstance.DbContext.GetAudioFilesArray());
        if (_viewModel.SelectedAudioFile is not null)
        {
            comboBoxAudio.SelectedIndex = comboBoxAudio.Items.IndexOf(_viewModel.SelectedAudioFile);
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

    private void ComboBoxAudio_SelectedIndexChanged(object sender, EventArgs e)
    {
        _viewModel.SelectedAudioFile = (Models.AudioFile)comboBoxAudio.SelectedItem;
    }

    private void ComboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        _viewModel.SelectedAudioCategory = (Models.AudioCategory)comboBoxCategory.SelectedItem;
    }

    private void ButtonAddAudio_Click(object sender, EventArgs e)
    {
        var page = _viewModel.IsCategoryAction ? SoundboardGlobalConfigViewV2Page.Categories : SoundboardGlobalConfigViewV2Page.Audio;
        using var audioAddDialog = SoundboardGlobalConfigViewV2.CreateViewForPage(page);
        if (audioAddDialog.ShowDialog(ParentForm) == DialogResult.OK)
        {
            LoadAudioFileSelection();
        }
    }

    private void UniqueRandomSoundsCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        _viewModel.EnsureUniqueRandomSound = checkBoxEnsureUniqueRandom.Checked;
    }
}