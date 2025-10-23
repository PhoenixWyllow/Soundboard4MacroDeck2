using Soundboard4MacroDeck.Models;
using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.ViewModels;
using Soundboard4MacroDeck.Views.Common;

using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;

using System.ComponentModel;

namespace Soundboard4MacroDeck.Views;

internal enum SoundboardGlobalConfigViewV2Page { Output, Audio, Categories }

public partial class SoundboardGlobalConfigViewV2 : DialogForm
{
    private static class ColumnNames
    {
        public const string Id = nameof(AudioFileItem.Id);
        public const string Name = nameof(AudioFileItem.Name);
        public const string Category = "Category";
    }

    private readonly SoundboardGlobalConfigViewModel _viewModel;
    private BindingList<AudioCategory>? _categoryComboBoxList;
    private readonly DataGridHelper<AudioCategory> _categoriesGridHelper;
    private readonly DataGridHelper<AudioFileItem> _audioFilesGridHelper;

    public SoundboardGlobalConfigViewV2(MacroDeckPlugin plugin)
    {
        _viewModel = new(plugin);

        InitializeComponent();

        // Initialize grid helpers (data binding will happen in Init methods)
        _categoriesGridHelper = new DataGridHelper<AudioCategory>(categoriesTable);
        _audioFilesGridHelper = new DataGridHelper<AudioFileItem>(audioFilesTable);

        ApplyLocalization();
        SetCloseIconVisible(true);

        _viewModel.OnSetDeviceIndex += (_, _) => { comboBoxDevices.SelectedIndex = _viewModel.DevicesIndex; };
    }

    internal static SoundboardGlobalConfigViewV2 CreateViewForPage(SoundboardGlobalConfigViewV2Page page)
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

        ConfigureCategoriesColumns();

        // Bind data to the grid helper (after columns are configured)
        _categoriesGridHelper.BindData(_viewModel.AudioCategories);

        // Attach CellEndEdit handler with simple error handling
        _categoriesGridHelper.OnCellEndEdit(_viewModel.UpdateCategory, ex => LogErrorAndTrace("Error updating category", ex));
    }

    private void ConfigureCategoriesColumns()
    {
        categoriesTable.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = ColumnNames.Id,
            DataPropertyName = nameof(AudioCategory.Id),
            HeaderText = nameof(AudioCategory.Id),
            ReadOnly = true,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        });

        categoriesTable.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = ColumnNames.Name,
            DataPropertyName = nameof(AudioCategory.Name),
            HeaderText = nameof(AudioCategory.Name),
            Resizable = DataGridViewTriState.True,
            MinimumWidth = 200,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
    }

    private void CategoriesAdd_Click(object? sender, EventArgs e)
    {
        AudioCategory newCategory = new();
        if (_viewModel.AddAudioCategory(newCategory))
        {
            _categoriesGridHelper.Add(newCategory);
            MacroDeckLogger.Info(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), "Audio category added successfully.");
            return;
        }

        MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), "Failed to add audio category.");
    }

    private void CategoriesRemove_Click(object? sender, EventArgs e)
    {
        var selectedCategory = _categoriesGridHelper.GetSelectedItem();
        if (selectedCategory is null)
        {
            return;
        }

        if (_viewModel.DeleteAudioCategory(selectedCategory))
        {
            _categoriesGridHelper.Remove(selectedCategory);
            MacroDeckLogger.Info(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), "Audio category removed successfully.");
        }
    }

    private static void LogErrorAndTrace(string message, Exception? ex)
    {
        MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), $"{message}: {ex?.Message ?? "No message"}");
        MacroDeckLogger.Trace(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), ex?.StackTrace ?? "No stack trace");
    }

    private void InitAudioFilesPage()
    {
        audioFileAdd.Click += AudioFileAdd_Click;
        audioFileRemove.Click += AudioFileRemove_Click;

        // Configure columns first (including the category combobox)
        ConfigureAudioFilesColumns();

        // Validate and fix any orphaned category references before creating UI
        int fixedCount = _viewModel.ValidateAndFixAudioFileCategories();
        if (fixedCount > 0)
        {
            MacroDeckLogger.Info(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), $"Fixed {fixedCount} audio file(s) with invalid category references.");
        }

        // Bind data to the grid helper (after all columns are configured)
        _audioFilesGridHelper.BindData(_viewModel.AudioFiles);

        // Attach CellEndEdit handler with simple error handling
        _audioFilesGridHelper.OnCellEndEdit(_viewModel.UpdateAudioFile, ex => LogErrorAndTrace("Error updating audio file", ex));

        // Attach CellFormatting handler to format Category column
        _audioFilesGridHelper.OnCellFormatting(ColumnNames.Category, item =>
        {
            var category = _viewModel.AudioCategories.FirstOrDefault(c => c.Id == item.CategoryId, defaultValue: AudioCategory.NoneOrUncategorized);
            return category.Name;
        });

        // Attach DataError handler
        _audioFilesGridHelper.OnDataError((columnName, rowIndex, ex) =>
        {
            LogErrorAndTrace($"Data error in column '{columnName}' at row {rowIndex}", ex);
        });
    }

    private void ConfigureAudioFilesColumns()
    {
        audioFilesTable.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = ColumnNames.Id,
            DataPropertyName = nameof(AudioFileItem.Id),
            HeaderText = nameof(AudioFileItem.Id),
            ReadOnly = true,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        });

        audioFilesTable.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = ColumnNames.Name,
            DataPropertyName = nameof(AudioFileItem.Name),
            HeaderText = nameof(AudioFileItem.Name)
        });

        InitializeCategoryComboBox();
    }

    private void InitializeCategoryComboBox()
    {
        // Create category list with "Uncategorized" option
        List<AudioCategory> categoriesWithNone = [AudioCategory.NoneOrUncategorized, .. _viewModel.AudioCategories];
        _categoryComboBoxList = new BindingList<AudioCategory>(categoriesWithNone);

        DataGridViewComboBoxColumn categoryBox = new()
        {
            Name = ColumnNames.Category,
            DataPropertyName = nameof(AudioFileItem.CategoryId),
            HeaderText = ColumnNames.Category,
            DisplayMember = nameof(AudioCategory.Name),
            ValueMember = nameof(AudioCategory.Id),
            DataSource = _categoryComboBoxList,
            DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing  // Only show dropdown when editing
        };

        audioFilesTable.Columns.Add(categoryBox);
    }

    private void AudioFileAdd_Click(object? sender, EventArgs e)
    {
        using var audioAddDialog = new SoundboardGlobalAudioAddView(_viewModel);
        if (audioAddDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        if (_viewModel.LastAudioFile is null)
        {
            MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), "Audio file cannot be added as there is no valid audio present.");
            return;
        }

        _audioFilesGridHelper.Add(_viewModel.LastAudioFile.ToAudioFileItem());
        _viewModel.LastAudioFile = null;

        MacroDeckLogger.Info(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), "Audio file added successfully.");
    }

    private void AudioFileRemove_Click(object? sender, EventArgs e)
    {
        var selectedItem = _audioFilesGridHelper.GetSelectedItem();
        if (selectedItem is null)
        {
            return;
        }

        if (_viewModel.DeleteAudioFile(selectedItem))
        {
            _audioFilesGridHelper.Remove(selectedItem);
            MacroDeckLogger.Info(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), "Audio file removed successfully.");
        }
    }

    private void Navigation_Selecting(object sender, TabControlCancelEventArgs e)
    {
        if (e.TabPage?.Name == audioFilePage.Name)
        {
            RefreshCategoryComboBox();
        }
    }

    private void RefreshCategoryComboBox()
    {
        if (_categoryComboBoxList is null)
        {
            MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), "Category list is null. Please reopen the config view.");
            return;
        }

        // Refresh audioCategories
        _categoryComboBoxList.Clear();
        _categoryComboBoxList.Add(AudioCategory.NoneOrUncategorized);

        foreach (var cat in _viewModel.AudioCategories)
        {
            _categoryComboBoxList.Add(cat);
        }

        // Warn if no categories exist (only "Uncategorized" is present)
        if (_categoryComboBoxList.Count == 1)
        {
            MacroDeckLogger.Warning(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), "No audio categories available. Please add at least one category before adding audio files.");
        }
    }

    private void ComboBoxDevices_SelectedIndexChanged(object sender, EventArgs e)
    {
        _viewModel.SetDevice(comboBoxDevices.SelectedIndex);
    }

    private void LinkLabelResetDevice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        _viewModel.ResetDevice();
    }

    private void ButtonOK_Click(object sender, EventArgs e)
    {
        _audioFilesGridHelper.EndEdit();
        _categoriesGridHelper.EndEdit();
        _viewModel.SaveConfig();
    }

}