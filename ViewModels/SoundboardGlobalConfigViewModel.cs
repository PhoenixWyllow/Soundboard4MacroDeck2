using NAudio.CoreAudioApi;
using Soundboard4MacroDeck.Models;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Soundboard4MacroDeck.ViewModels
{
    public class SoundboardGlobalConfigViewModel : OutputDeviceConfigurationViewModel
    {
        private readonly IMacroDeckPlugin _plugin;
        public SoundboardGlobalConfigViewModel(IMacroDeckPlugin plugin) 
            : base(GlobalParameters.Deserialize(PluginConfiguration.GetValue(plugin, nameof(SoundboardGlobalConfigViewModel))))
        {
        }


        public override void SetConfig()
        {
            PluginConfiguration.SetValue(_plugin, nameof(SoundboardGlobalConfigViewModel), OutputConfiguration.Serialize());
        }
    }
}
