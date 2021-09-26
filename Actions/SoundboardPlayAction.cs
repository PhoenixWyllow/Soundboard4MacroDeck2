using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;

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
            return new Views.SoundboardActionConfigView(this, actionConfigurator);
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
            }
            catch
            {
            }
        }
    }
}
