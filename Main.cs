using SuchByte.MacroDeck.Plugins;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using MacroDeckSoundboard.Actions;

namespace MacroDeckSoundboard
{
    public class Main : IMacroDeckPlugin
    {
        /// <summary>
        /// Name of the plugin
        /// Please don't change
        /// </summary>
        public string Name => Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>
        /// Version of the plugin
        /// Please don't change
        /// </summary>
        public string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Author of the plugin
        /// </summary>
        public string Author => "PhoenixWyllow aka PW.Dev (pw.dev@outlook.com)";

        /// <summary>
        /// Short description what the plugin can do
        /// </summary>
        public string Description => "A soundboard plugin for Macro Deck 2";

        /// <summary>
        /// List of all the actions of the plugin
        /// </summary>
        public List<IMacroDeckAction> Actions { get; set; }

        /// <summary>
        /// Icon for the plugin
        /// </summary>
        public Image Icon => Properties.Resources.SoundboardIcon;

        /// <summary>
        /// Can the plugin be configured? E.g. accounts
        /// </summary>
        public bool CanConfigure => true;

        /// <summary>
        /// Gets called when Macro Deck enables the plugin
        /// </summary>
        public void Enable()
        {
            _ = Lib.SoundPlayer.Instance;

            Actions = new List<IMacroDeckAction>{
                new SoundboardPlayAction(),
            };
        }

        /// <summary>
        /// Gets called when the user wants to configure the plugin
        /// </summary>
        public void OpenConfigurator()
        {
            using var pluginConfig = new Views.SoundboardGlobalConfig();
            pluginConfig.ShowDialog();
        }
    }
}
