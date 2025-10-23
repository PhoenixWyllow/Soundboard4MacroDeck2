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
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Category = "Category";
    }

    private readonly SoundboardGlobalConfigViewModel _viewModel;
    private BindingList<AudioCategory>? _categoryComboBoxList;
    private readonly DataGridController<AudioCategory> _categoriesController;
    private readonly DataGridController<AudioFileItem> _audioFilesController;

    public SoundboardGlobalConfigViewV2(MacroDeckPlugin plugin)
    {
        _viewModel = new(plugin);

        InitializeComponent();

        // Initialize logger for toolbar operations
        OperationLogger logger = new(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2));

        // Initialize grid controllers
        _categoriesController = new DataGridController<AudioCategory>(categoriesTable);
        _categoriesController.SetLogger(logger, "Audio category");
        _audioFilesController = new DataGridController<AudioFileItem>(audioFilesTable);
        _audioFilesController.SetLogger(logger, "Audio file");

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
        ConfigureCategoriesColumns();

        // Bind data to the grid controller (after columns are configured)
        _categoriesController.Data.BindData(_viewModel.AudioCategories);

        // Attach add and remove buttons
        _categoriesController.AttachAddButton(categoriesAdd, itemFactory: () => new AudioCategory(), addToViewModel: _viewModel.AddAudioCategory);

        _categoriesController.AttachRemoveButton(categoriesRemove, removeFromViewModel: _viewModel.DeleteAudioCategory);

        // Attach CellEndEdit handler with simple error handling
        _categoriesController.Data.OnCellEndEdit(_viewModel.UpdateCategory, ex => LogErrorAndTrace("Error updating category", ex));
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

    private static void LogErrorAndTrace(string message, Exception? ex)
    {
        MacroDeckLogger.Error(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), $"{message}: {ex?.Message ?? "No message"}");
        MacroDeckLogger.Trace(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), ex?.StackTrace ?? "No stack trace");
    }

    private void InitAudioFilesPage()
    {
        // Configure columns (including the category combobox)
        ConfigureAudioFilesColumns();

        // Validate and fix any orphaned category references before creating UI
        int fixedCount = _viewModel.ValidateAndFixAudioFileCategories();
        if (fixedCount > 0)
        {
            MacroDeckLogger.Info(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), $"Fixed {fixedCount} audio file(s) with invalid category references.");
        }

        // Bind data to the grid after columns are configured
        _audioFilesController.Data.BindData(_viewModel.AudioFiles);

        // Attach add button
        _audioFilesController.AttachAddButton(audioFileAdd, itemFactory: CreateAudioFileItemFromDialog, addToViewModel: _viewModel.ValidateAudioFileAddition);

        // Attach remove button
        _audioFilesController.AttachRemoveButton(audioFileRemove, removeFromViewModel: _viewModel.DeleteAudioFile);

        // Attach CellEndEdit handler with simple error handling
        _audioFilesController.Data.OnCellEndEdit(_viewModel.UpdateAudioFile, ex => LogErrorAndTrace("Error updating audio file", ex));

        // Attach CellFormatting handler to format Category column
        _audioFilesController.Data.OnCellFormatting(ColumnNames.Category, item =>
        {
            var category = _viewModel.AudioCategories.FirstOrDefault(c => c.Id == item.CategoryId, defaultValue: AudioCategory.NoneOrUncategorized);
            return category.Name;
        });

        // Attach DataError handler
        _audioFilesController.Data.OnDataError((columnName, rowIndex, ex) =>
        {
            LogErrorAndTrace($"Data error in column '{columnName}' at row {rowIndex}", ex);
        });
    }

    private AudioFileItem? CreateAudioFileItemFromDialog()
    {
        using var audioAddDialog = new SoundboardGlobalAudioAddView(_viewModel);
        if (audioAddDialog.ShowDialog(this) != DialogResult.OK)
        {
            return null;
        }
        return _viewModel.LastAudioFile?.ToAudioFileItem();
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
        _audioFilesController.Data.EndEdit();
        _categoriesController.Data.EndEdit();
        _viewModel.SaveConfig();
    }

}