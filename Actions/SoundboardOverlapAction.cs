using Soundboard4MacroDeck.Models;
using Soundboard4MacroDeck.Services;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;

namespace Soundboard4MacroDeck.Actions
{
    public class SoundboardOverlapAction : PluginAction
    {
        /// <summary>
        /// Name of the action
        /// </summary>
        public override string Name => Localization.Instance.ActionOverlapSoundName;

        /// <summary>
        /// A short description what this action does
        /// </summary>
        public override string Description => Localization.Instance.ActionOverlapSoundDescription;

        /// <summary>
        /// Displayname of the action. Can be changed later depending on the configuration, if plugin can be configured.
        /// </summary>
        public override string DisplayName { get; set; } = Localization.Instance.ActionOverlapSoundName;

        /// <summary>
        /// Set true if the plugin can be configured.
        /// </summary>
        public override bool CanConfigure => true;

        /// <summary>
        /// Return the ActionConfigControl for this action, if action can be configured. Return null if plugin cannot be configured and you set CanConfigure to false.
        /// </summary>
        /// <returns></returns>
        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new Views.SoundboardActionConfigView(this, actionConfigurator);
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
                SoundPlayer.Execute(SoundboardActions.Overlap, Configuration, actionButton);
            }
            catch
            {
                SoundPlayer.StopAll();
            }
        }
    }
}
