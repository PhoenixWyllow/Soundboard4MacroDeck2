﻿using SuchByte.MacroDeck.Plugins;
using System.Collections.Generic;
using System.Drawing;
using Soundboard4MacroDeck.Actions;

namespace Soundboard4MacroDeck
{
    public class Main : IMacroDeckPlugin
    {
        /// <summary>
        /// Name of the plugin
        /// </summary>
        public string Name => typeof(Main).Assembly.GetName().Name;

        /// <summary>
        /// Version of the plugin
        /// </summary>
        public string Version => typeof(Main).Assembly.GetName().Version.ToString();

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
        public bool CanConfigure => false; //TODO

        /// <summary>
        /// Gets called when Macro Deck enables the plugin
        /// </summary>
        public void Enable()
        {
            Services.SoundPlayer.CreateInstance();

            Actions = new List<IMacroDeckAction>
            {
                new SoundboardPlayAction(),
            };
        }

        /// <summary>
        /// Gets called when the user wants to configure the plugin
        /// </summary>
        public void OpenConfigurator()
        {
            //TODO
            //using var pluginConfig = new Views.SoundboardGlobalConfig();
            //pluginConfig.ShowDialog();
        }
    }
}
