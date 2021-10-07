using Soundboard4MacroDeck.Models;
using SuchByte.MacroDeck.Plugins;

namespace Soundboard4MacroDeck.ViewModels
{
    public class SoundboardGlobalConfigViewModel : OutputDeviceConfigurationViewModel
    {
        private readonly IMacroDeckPlugin _plugin;
        public SoundboardGlobalConfigViewModel(IMacroDeckPlugin plugin) 
            : base(GlobalParameters.Deserialize(PluginConfiguration.GetValue(plugin, nameof(SoundboardGlobalConfigViewModel))))
        {
            _plugin = plugin;
        }


        public override void SetConfig()
        {
            PluginConfiguration.SetValue(_plugin, nameof(SoundboardGlobalConfigViewModel), OutputConfiguration.Serialize());
        }
    }
}
