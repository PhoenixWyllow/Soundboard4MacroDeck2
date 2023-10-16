using SuchByte.MacroDeck.Plugins;
using Soundboard4MacroDeck.Actions;
using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.Models;

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