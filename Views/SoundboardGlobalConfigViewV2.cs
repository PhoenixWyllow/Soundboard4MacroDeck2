﻿using Soundboard4MacroDeck.Models;
using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.ViewModels;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.ComponentModel;
using System.Windows.Forms;

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

    private BindingList<AudioCategory> categoryComboBoxList;

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

        categoryComboBoxList = new(_viewModel.AudioCategories);
        DataGridViewComboBoxColumn categoryBox = new()
        {
            DataPropertyName = nameof(AudioFileItem.CategoryId),
            HeaderText = "Category",
            DisplayMember = nameof(AudioCategory.Name),  // Display the 'Name' property of the AudioCategory
            ValueMember = nameof(AudioCategory.Id),  // Use the 'Id' property of the AudioCategory as the actual value
            DataSource = categoryComboBoxList
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
            audioFilesList.Add(_viewModel.LastAudioFile.ToAudioFileItem());
            _viewModel.LastAudioFile = null;
        }
    }

    private void Navigation_Selecting(object sender, TabControlCancelEventArgs e)
    {
        if (e.TabPage.Name == audioFilePage.Name)
        {
            // Refresh audioCategories
            categoryComboBoxList.Clear();
            foreach (var cat in _viewModel.AudioCategories)
            {
                categoryComboBoxList.Add(cat);
            }
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
            var editedRow = categoriesTable.Rows[e.RowIndex];
            AudioCategory editedCategory = (AudioCategory)editedRow.DataBoundItem;
            _viewModel.UpdateCategory(editedCategory);
        }
        catch (Exception ex)
        {
            MacroDeckLogger.Trace(PluginInstance.Current, typeof(SoundboardGlobalConfigViewV2), ex.Message);
        }
    }

    private void AudioFilesTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
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