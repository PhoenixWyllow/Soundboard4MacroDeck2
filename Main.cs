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

        Actions = new()
        {
            new SoundboardPlayAction(),
            new SoundboardPlayStopAction(),
            new SoundboardOverlapAction(),
            new SoundboardLoopAction(),
            new SoundboardStopAction(),
        };
    }

    /// <summary>
    /// Gets called when the user wants to configure the plugin
    /// </summary>
    public override void OpenConfigurator()
    {
        using var pluginConfig = new Views.SoundboardGlobalConfigView(this);
        pluginConfig.ShowDialog();
    }
}
internal static class PluginInstance
{
    internal static MacroDeckPlugin Current { get; set; }
    internal static IOutputConfiguration Configuration => GlobalParameters.Deserialize(PluginConfiguration.GetValue(Current, nameof(ViewModels.SoundboardGlobalConfigViewModel)));

}