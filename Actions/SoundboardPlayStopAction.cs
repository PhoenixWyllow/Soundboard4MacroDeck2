using Soundboard4MacroDeck.Models;
using Soundboard4MacroDeck.Services;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System;

namespace Soundboard4MacroDeck.Actions
{
    public class SoundboardPlayStopAction : PluginAction
    {
        /// <summary>
        /// Name of the action
        /// </summary>
        public override string Name => LocalizationManager.Instance.ActionPlayStopSoundName;

        /// <summary>
        /// A short description what this action does
        /// </summary>
        public override string Description => $"{LocalizationManager.Instance.ActionPlayStopSoundDescription}.{Environment.NewLine}({LocalizationManager.Instance.ActionSuggestButtonStates})";

        /// <summary>
        /// Set true if the plugin can be configured.
        /// </summary>
        public override bool CanConfigure => true;

        /// <summary>
        /// Return the ActionConfigControl for this action.
        /// </summary>
        /// <returns></returns>
        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new Views.SoundboardActionConfigView(this);
        }

        /// <summary>
        /// Gets called when the button with this action gets pressed.
        /// </summary>
        /// <param name="clientId">Returns the client id</param>
        /// <param name="actionButton">Returns the pressed action button</param>
        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (string.IsNullOrWhiteSpace(Configuration))
            {
                return;
            }

            try
            {
                SoundPlayer.Execute(SoundboardActions.PlayStop, Configuration, actionButton);
            }
            catch
            {
                SoundPlayer.StopAll();
            }
        }
    }
}
