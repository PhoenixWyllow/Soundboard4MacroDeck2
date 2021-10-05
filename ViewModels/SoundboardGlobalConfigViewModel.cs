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
        public SoundboardGlobalConfigViewModel(IMacroDeckPlugin plugin) : base(plugin)
        {
        }


        public override void SetConfig()
        {
            PluginConfiguration.SetValue(_plugin, nameof(SoundboardGlobalConfigViewModel), _outputConfiguration.Serialize());
        }
    }
}
