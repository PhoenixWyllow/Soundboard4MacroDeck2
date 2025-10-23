using Soundboard4MacroDeck.Models;
using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.ViewModels;

using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;

using System.ComponentModel;

namespace Soundboard4MacroDeck.Views;

internal enum SoundboardGlobalConfigViewV2Page { Output, Audio, Categories }

public partial class SoundboardGlobalConfigViewV2 : DialogForm
{
    private readonly SoundboardGlobalConfigViewModel _viewModel;
    public SoundboardGlobalConfigViewV2(MacroDeckPlugin plugin)
    {
        _viewModel = new(plugin);

        InitializeComponent();
        ApplyLocalization();
        SetCloseIconVisible(true);

        _viewModel.OnSetDeviceIndex += (_, _) => { comboBoxDevices.SelectedIndex = _viewModel.DevicesIndex; };
    }

    internal static SoundboardGlobalConfigViewV2 NewAtPage(SoundboardGlobalConfigViewV2Page page)
    {
        SoundboardGlobalConfigViewV2 view = new(PluginInstance.Current);
        switch (page)
        {
            case SoundboardGlobalConfigViewV2Page.Audio:
                view.navigation.SelectedTab = view.audioFilePage;
                break;
            case SoundboardGlobalConfigViewV2Page.Categories:
                view.navigation.SelectedTab = view.categoryPage;
                break;
            default:
                break;
        }
        return view;
    }

    private void ApplyLocalization()
    {
        outputPage.Text = LocalizationManager.Instance.GlobalConfigOutputDevice;
        audioFilePage.Text = LocalizationManager.Instance.GlobalConfigAudioFiles;
        categoryPage.Text = LocalizationManager.Instance.GlobalConfigAudioCategories;
        linkLabelResetDevice.Text = LocalizationManager.Instance.UseSystemDefaultDevice;
        labelDevices.Text = LocalizationManager.Instance.OutputDevicesGlobal;
        buttonOK.Text = LanguageManager.Strings.Ok;
        categoriesAdd.Image = SuchByte.MacroDeck.Properties.Resources.Create_Normal;
        audioFileAdd.Image = SuchByte.MacroDeck.Properties.Resources.Create_Normal;
        categoriesRemove.Image = SuchByte.MacroDeck.Properties.Resources.Delete_Normal;
        audioFileRemove.Image = SuchByte.MacroDeck.Properties.Resources.Delete_Normal;
        categoriesAdd.Text = LocalizationManager.Instance.AddLabel;
        audioFileAdd.Text = LocalizationManager.Instance.AddLabel;
        categoriesRemove.Text = LocalizationManager.Instance.DeleteLabel;
        audioFileRemove.Text = LocalizationManager.Instance.DeleteLabel;
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
        categoriesRemove.Click += CategoriesRemove_Click;
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
            HeaderText = nameof(AudioCategory.Name),
            Resizable = DataGridViewTriState.True,
            MinimumWidth = 200,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
        categoriesTable.DataSource = _viewModel.AudioCategories;
        categoriesTable.CellEndEdit += CategoriesTable_CellEndEdit;
    }

    private BindingList<AudioCategory>? categoryComboBoxList;

    private BindingList<AudioFileItem>? audioFilesList;

    private void InitAudioFilesPage()
    {
        audioFileAdd.Click += AudioFileAdd_Click;
        audioFileRemove.Click += AudioFileRemove_Click;
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

        // Validate and fix any orphaned category references before creating UI
        int fixedCount = _viewModel.ValidateAndFixAudioFileCategories();
        if (fixedCount > 0)
        {
            MacroDeckLogger.Info(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2),
                    $"Fixed {fixedCount} audio file(s) with invalid category references.");
        }

        // Create category list with "Uncategorized" option
        List<AudioCategory> categoriesWithNone = [AudioCategory.NoneOrUncategorized, .. _viewModel.AudioCategories];
        categoryComboBoxList = new BindingList<AudioCategory>(categoriesWithNone);

        DataGridViewComboBoxColumn categoryBox = new()
        {
            DataPropertyName = nameof(AudioFileItem.CategoryId),
            HeaderText = "Category",
            DisplayMember = nameof(AudioCategory.Name),
            ValueMember = nameof(AudioCategory.Id),
            DataSource = categoryComboBoxList,
            DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing  // Only show dropdown when editing
        };

        audioFilesTable.DataError += AudioFilesTable_DataError;
        audioFilesTable.CellFormatting += AudioFilesTable_CellFormatting;
        audioFilesTable.Columns.Add(categoryBox);

        audioFilesList = new(_viewModel.AudioFiles);
        audioFilesTable.DataSource = audioFilesList;
        audioFilesTable.CellEndEdit += AudioFilesTable_CellEndEdit;
    }

    private void AudioFilesTable_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        // Handle Category column formatting to display category name even if value is invalid
        if (audioFilesTable.Columns[e.ColumnIndex].Name == "Category" ||
            audioFilesTable.Columns[e.ColumnIndex].HeaderText == "Category")
        {
            if (e.RowIndex >= 0 && e.RowIndex < audioFilesTable.Rows.Count)
            {
                var row = audioFilesTable.Rows[e.RowIndex];
                if (row.DataBoundItem is AudioFileItem item)
                {
                    var category = _viewModel.AudioCategories.FirstOrDefault(c => c.Id == item.CategoryId, defaultValue: AudioCategory.NoneOrUncategorized);
                    if (category is not null)
                    {
                        e.Value = category.Name;
                        e.FormattingApplied = true;
                    }
                    else
                    {
                        // Invalid category - show placeholder
                        e.Value = AudioCategory.NoneOrUncategorized.Name;
                        e.FormattingApplied = true;
                    }
                }
            }
        }
    }

    private void AudioFilesTable_DataError(object? sender, DataGridViewDataErrorEventArgs e)
    {
        e.Cancel = true;
        e.ThrowException = false;

        string columnName = audioFilesTable.Columns[e.ColumnIndex]?.HeaderText ?? "Unknown";
        MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2),
            $"Data error in column '{columnName}' at row {e.RowIndex}: {e.Exception?.Message ?? "No message"}");
        MacroDeckLogger.Trace(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), e.Exception?.StackTrace ?? "No stack trace");
    }

    private void AudioFileAdd_Click(object? sender, EventArgs e)
    {
        using var audioAddDialog = new SoundboardGlobalAudioAddView(_viewModel);
        if (audioAddDialog.ShowDialog(this) == DialogResult.OK)
        {
            if (audioFilesList is null || _viewModel.LastAudioFile is null)
            {
                MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), "Audio file cannot be added as there is no valid audio present.");
                return;
            }
            audioFilesList.Add(_viewModel.LastAudioFile.ToAudioFileItem());
            _viewModel.LastAudioFile = null;

            MacroDeckLogger.Info(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), "Audio file added successfully.");
        }
    }

    private void AudioFileRemove_Click(object? sender, EventArgs e)
    {
        if (audioFilesTable.SelectedRows.Count > 0 && audioFilesList is not null)
        {
            var row = audioFilesTable.SelectedRows[0];
            if (row.DataBoundItem is AudioFileItem item)
            {
                bool canDelete = _viewModel.CanDeleteAudioFile(item);
                if (canDelete)
                {
                    PluginInstance.DbContext.DeleteAudioFile(item.Id);
                    audioFilesList.Remove(item);
                    MacroDeckLogger.Info(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), "Audio file removed successfully.");
                }
            }
        }
    }

    private void CategoriesRemove_Click(object? sender, EventArgs e)
    {
        if (categoriesTable.SelectedRows.Count > 0)
        {
            var row = categoriesTable.SelectedRows[0];
            if (row.DataBoundItem is AudioCategory cat)
            {
                bool canDelete = _viewModel.CanDeleteAudioCategory(cat);
                if (canDelete)
                {
                    PluginInstance.DbContext.DeleteAudioCategory(cat.Id);
                    categoriesTable.DataSource = _viewModel.AudioCategories;
                    MacroDeckLogger.Info(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), "Audio category removed successfully.");
                }
            }
        }
    }

    private void Navigation_Selecting(object sender, TabControlCancelEventArgs e)
    {
        if (e.TabPage?.Name == audioFilePage.Name)
        {
            if (categoryComboBoxList is null)
            {
                MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), "Category list is null. Please reopen the config view.");
                return;
            }

            // Refresh audioCategories
            categoryComboBoxList.Clear();

            // Add "Uncategorized" option
            categoryComboBoxList.Add(new AudioCategory { Id = 0, Name = "(Uncategorized)" });

            foreach (var cat in _viewModel.AudioCategories)
            {
                categoryComboBoxList.Add(cat);
            }

            // Warn if no categories exist
            if (!categoryComboBoxList.Any())
            {
                MacroDeckLogger.Warning(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2),
                         "No audio categories available. Please add at least one category before adding audio files.");
            }
        }
    }

    private void CategoriesAdd_Click(object? sender, EventArgs e)
    {
        PluginInstance.DbContext.InsertAudioCategory(new());
        categoriesTable.DataSource = _viewModel.AudioCategories;
    }

    private void CategoriesTable_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
    {
        try
        {
            var editedRow = categoriesTable.Rows[e.RowIndex];
            AudioCategory editedCategory = (AudioCategory)editedRow.DataBoundItem;
            _viewModel.UpdateCategory(editedCategory);
        }
        catch (Exception ex)
        {
            MacroDeckLogger.Trace(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), ex.Message);
        }
    }

    private void AudioFilesTable_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
    {
        try
        {
            var editedRow = audioFilesTable.Rows[e.RowIndex];
            AudioFileItem editedItem = (AudioFileItem)editedRow.DataBoundItem;
            _viewModel.UpdateAudioFile(editedItem);
        }
        catch (Exception ex)
        {
            MacroDeckLogger.Trace(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), ex.Message);
        }
    }

    private void ComboBoxDevices_SelectedIndexChanged(object sender, EventArgs e)
    {
        _viewModel.SetDevice(comboBoxDevices.SelectedIndex);
    }

    private void ButtonOK_Click(object sender, EventArgs e)
    {
        audioFilesTable.EndEdit();
        categoriesTable.EndEdit();
        _viewModel.SaveConfig();
    }

    private void LinkLabelResetDevice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        _viewModel.ResetDevice();
    }

}