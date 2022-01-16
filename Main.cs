using SuchByte.MacroDeck.Plugins;
using System.Collections.Generic;
using System.Drawing;
using Soundboard4MacroDeck.Actions;
using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.Models;

namespace Soundboard4MacroDeck
{
    public class Main : MacroDeckPlugin
    {
        /// <summary>
        /// Short description what the plugin can do
        /// </summary>
        public override string Description => LocalizationManager.Instance.Soundboard4MacroDeckDescription;

        /// <summary>
        /// Icon for the plugin
        /// </summary>
        public override Image Icon => Properties.Resources.SoundboardIcon;

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
            Instance = this;

            Actions = new List<PluginAction>
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
        internal static MacroDeckPlugin Instance { get; set; }
        internal static IOutputConfiguration Configuration => GlobalParameters.Deserialize(PluginConfiguration.GetValue(Instance, nameof(ViewModels.SoundboardGlobalConfigViewModel)));
    }
}
