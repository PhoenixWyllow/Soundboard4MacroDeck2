using Soundboard4MacroDeck.Models;
using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.ViewModels;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Plugins;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Soundboard4MacroDeck.Views;

public partial class SoundboardGlobalConfigViewV2 : DialogForm
{
    private readonly SoundboardGlobalConfigViewModel _viewModel;
    public SoundboardGlobalConfigViewV2(MacroDeckPlugin plugin)
    {
        _viewModel = new(plugin);

        InitializeComponent();
        ApplyLocalization();

        _viewModel.OnSetDeviceIndex += (_, _) => { comboBoxDevices.SelectedIndex = _viewModel.DevicesIndex; };
    }
    private void ApplyLocalization()
    {
        outputPage.Text = LocalizationManager.Instance.GlobalConfigOutputDevice;
        audioFilePage.Text = LocalizationManager.Instance.GlobalConfigAudioFiles;
        setupPage.Text = LocalizationManager.Instance.GlobalConfigAudioCategories;
        linkLabelResetDevice.Text = LocalizationManager.Instance.UseSystemDefaultDevice;
        labelDevices.Text = LocalizationManager.Instance.OutputDevicesGlobal;
        buttonOK.Text = LanguageManager.Strings.Ok;
        categoriesAdd.Image = SuchByte.MacroDeck.Properties.Resources.Create_Normal;
        audioFileAdd.Image = SuchByte.MacroDeck.Properties.Resources.Create_Normal;
    }

    private void SoundboardGlobalConfigView_Load(object sender, EventArgs e)
    {
        _viewModel.LoadDevices();
        comboBoxDevices.Items.AddRange(_viewModel.Devices.ToArray());
        _viewModel.LoadDeviceIndex();
        InitCategoriesPage();
        InitAudioFilesPage();
    }

    private void InitCategoriesPage()
    {
        categoriesAdd.Click += CategoriesAdd_Click;
        categoriesTable.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AudioCategory.Id),
            HeaderText = nameof(AudioCategory.Id),
            ReadOnly = true,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        });

        categoriesTable.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AudioCategory.Name),
            HeaderText = nameof(AudioCategory.Name)
        });
        categoriesTable.DataSource = _viewModel.AudioCategories;
        categoriesTable.CellEndEdit += CategoriesTable_CellEndEdit;
    }

    private DataGridViewComboBoxColumn categoryBox;

    private BindingList<AudioFileItem> audioFilesList;
    private void InitAudioFilesPage()
    {
        audioFileAdd.Click += AudioFileAdd_Click;
        audioFilesTable.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AudioFileItem.Id),
            HeaderText = nameof(AudioFileItem.Id),
            ReadOnly = true,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        });

        audioFilesTable.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(AudioFileItem.Name),
            HeaderText = nameof(AudioFileItem.Name)
        });

        categoryBox = new()
        {
            DataPropertyName = nameof(AudioFileItem.CategoryId),
            HeaderText = "Category",
            DisplayMember = nameof(AudioCategory.Name),  // Display the 'Name' property of the AudioCategory
            ValueMember = nameof(AudioCategory.Id),  // Use the 'Id' property of the AudioCategory as the actual value
            DataSource = _viewModel.AudioCategories
        };
        audioFilesTable.Columns.Add(categoryBox);

        audioFilesList = new(_viewModel.AudioFiles);
        audioFilesTable.DataSource = audioFilesList;
        audioFilesTable.CellEndEdit += AudioFilesTable_CellEndEdit; 
    }

    private void AudioFileAdd_Click(object sender, EventArgs e)
    {
        using var audioAddDialog = new SoundboardGlobalAudioAddView(_viewModel);
        if (audioAddDialog.ShowDialog(this) == DialogResult.OK)
        {
            audioFilesList.Add(_viewModel.ItemFromAudioFile(_viewModel.LastAudioFile));
            _viewModel.LastAudioFile = null;
        }
    }

    private void Navigation_Selecting(object sender, TabControlCancelEventArgs e)
    {
        if (e.TabPage.Name == audioFilePage.Name)
        {
            // Refresh audioCategories
            categoryBox.DataSource = null;
            categoryBox.DataSource = _viewModel.AudioCategories;
        }
    }

    private void CategoriesAdd_Click(object sender, EventArgs e)
    {
        PluginInstance.DbContext.InsertAudioCategory(new());
        categoriesTable.DataSource = _viewModel.AudioCategories;
    }
    private void CategoriesTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            var row = categoriesTable.Rows[e.RowIndex];
            _viewModel.UpdateCategory(
                (int)row.Cells[nameof(AudioCategory.Id)].Value,
                (string)row.Cells[nameof(AudioCategory.Name)].Value
                );
        }
        catch (Exception ex)
        {
        }
    }

    private void AudioFilesTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            var row = audioFilesTable.Rows[e.RowIndex];
            _viewModel.UpdateAudioFile(
                (int)row.Cells[nameof(AudioFileItem.Id)].Value, 
                (string)row.Cells[nameof(AudioFileItem.Name)].Value, 
                (int)row.Cells[nameof(AudioFileItem.CategoryId)].Value
                );
        }
        catch (Exception ex)
        {
        }
    }

    private void ComboBoxDevices_SelectedIndexChanged(object sender, EventArgs e)
    {
        _viewModel.SetDevice(comboBoxDevices.SelectedIndex);
    }

    private void ButtonOK_Click(object sender, EventArgs e)
    {
        _viewModel.SaveConfig();
    }

    private void LinkLabelResetDevice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        _viewModel.ResetDevice();
    }
}