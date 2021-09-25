using SuchByte.MacroDeck;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace MacroDeckSoundboard.Actions
{
    public class SoundboardPlayAction : IMacroDeckAction
    {

        /// <summary>
        /// Name of the action
        /// </summary>
        public string Name => "Play sound";

        /// <summary>
        /// A short description what this action does
        /// </summary>
        public string Description => "Plays the configured file";

        /// <summary>
        /// Displayname of the action. Can be changed later depending on the configuration, if plugin can be configured.
        /// </summary>
        public string DisplayName { get; set; } = "Play sound";

        /// <summary>
        /// Configuration of the action. Just leave it how it is.
        /// </summary>
        public string Configuration { get; set; } = string.Empty;

        /// <summary>
        /// Set true if the plugin can be configured.
        /// </summary>
        public bool CanConfigure => true;

        /// <summary>
        /// Return the ActionConfigControl for this action, if action can be configured. Return null if plugin cannot be configured and you set CanConfigure to false.
        /// </summary>
        /// <returns></returns>
        public ActionConfigControl GetActionConfigurator(ActionConfigurator actionConfigurator)
        {
            return new Views.SoundboardActionConfig(this, actionConfigurator);
        }

        /// <summary>
        /// Gets called when the button with this action gets pressed.
        /// </summary>
        /// <param name="clientId">Returns the client id</param>
        /// <param name="actionButton">Returns the pressed action button</param>
        public void Trigger(string clientId, ActionButton actionButton)
        {
            if (string.IsNullOrWhiteSpace(Configuration))
            {
                return;
            }
            
            try
            {
                //if (Main.Sinusbot == null || Main.Sinusbot.LoggedIn == false) return;
                //string instanceId = JObject.Parse(this.Configuration)["instanceId"].ToString();
                //string fileId = JObject.Parse(this.Configuration)["fileId"].ToString();
                //int volume = Int32.Parse(JObject.Parse(this.Configuration)["volume"].ToString());
                //if (volume > -1)
                //{
                //    Main.Sinusbot.SetVolume(instanceId, volume);
                //}
                //Main.Sinusbot.PlayFile(instanceId, fileId);
            }
            catch { }
        }
    }
}
