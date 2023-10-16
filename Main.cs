using SuchByte.MacroDeck.Plugins;
using Soundboard4MacroDeck.Actions;
using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.Models;
using System;
using SuchByte.MacroDeck.GUI.CustomControls;
using Soundboard4MacroDeck.Properties;

namespace Soundboard4MacroDeck;

public class Main : MacroDeckPlugin
{
    /// <summary>
    /// Can the plugin be configured? E.g. accounts
    /// </summary>
    public override bool CanConfigure => true;

    /// <summary>
    /// Gets called when Macro Deck enables the plugin
    /// </summary>
    public override void Enable()
    {
        LocalizationManager.CreateInstance();
        PluginInstance.Current = this;
        PluginInstance.DbContext = new SoundboardContext();

        Actions = new()
        {
            new SoundboardPlayAction(),
            new SoundboardPlayStopAction(),
            new SoundboardOverlapAction(),
            new SoundboardLoopAction(),
            new SoundboardStopAction(),
        };

        if (PluginInstance.DbContext.IsInitialCreate)
        {
            SuchByte.MacroDeck.MacroDeck.OnMacroDeckLoaded += (_, _) => ConfigUpdater.MoveToContext();
        }

        SoundboardContext.AddBackupCreationHook();
        SuchByte.MacroDeck.MacroDeck.OnMainWindowLoad += MacroDeck_OnMainWindowLoad;
    }

    private void MacroDeck_OnMainWindowLoad(object sender, EventArgs e)
    {
        if (sender is SuchByte.MacroDeck.GUI.MainWindow mainWindow)
        {
            ContentSelectorButton statusButton = new()
            {
                BackgroundImage = Resources.SoundboardIcon,
            };
            statusButton.Click += (_, _) => OpenConfigurator();
            mainWindow.contentButtonPanel.Controls.Add(statusButton);
        }
    }

    /// <summary>
    /// Gets called when the user wants to configure the plugin
    /// </summary>
    public override void OpenConfigurator()
    {
        using var pluginConfig = new Views.SoundboardGlobalConfigViewV2(this);
        pluginConfig.ShowDialog();
    }
}
internal static class PluginInstance
{
    internal static SoundboardContext DbContext { get; set; }
    internal static MacroDeckPlugin Current { get; set; }
    internal static IOutputConfiguration Configuration => GlobalParameters.Deserialize(PluginConfiguration.GetValue(Current, nameof(ViewModels.SoundboardGlobalConfigViewModel)));

}