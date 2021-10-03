using NAudio.CoreAudioApi;
using Soundboard4MacroDeck.Models;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Soundboard4MacroDeck.ViewModels
{
    public class SoundboardGlobalConfigViewModel
    {
        private readonly IMacroDeckPlugin _plugin;
        private readonly GlobalParameters _parameters;
        public List<MMDevice> Devices { get; set; }
        public int DevicesIndex { get; set; }

        public SoundboardGlobalConfigViewModel(IMacroDeckPlugin plugin)
        {
            _plugin = plugin;
            _parameters = GlobalParameters.Deserialize(PluginConfiguration.GetValue(_plugin, nameof(SoundboardGlobalConfigViewModel)));
            FetchAvailableDevices();
            
        }

        internal void SetDevice(int selectedIndex)
        {
            DevicesIndex = selectedIndex;
            _parameters.OutputDeviceId = Devices[DevicesIndex].ID;
        }

        protected void FetchAvailableDevices()
        {
            bool needsDefault = string.IsNullOrWhiteSpace(_parameters.OutputDeviceId);
            List<MMDevice> devices = new List<MMDevice>();

            int idx = 0;
            using var enumerator = new MMDeviceEnumerator();
            foreach (var device in enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
            {
                devices.Add(device);
                if (!needsDefault && device.ID.Equals(_parameters.OutputDeviceId))
                {
                    DevicesIndex = idx;
                }
                idx++;
            }
            Devices = new List<MMDevice>(devices);
            
            if (needsDefault)
            {
                SetDefaultDevice(enumerator);
            }
        }

        private void SetDefaultDevice(MMDeviceEnumerator enumerator)
        {
            var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            _parameters.OutputDeviceId = device.ID;
            DevicesIndex = Devices.FindIndex(d => d.ID.Equals(device.ID));
        }

        public void SaveConfig()
        {
            try
            {
                PluginConfiguration.SetValue(_plugin, nameof(SoundboardGlobalConfigViewModel), _parameters.Serialize());
                Debug.WriteLine($"{nameof(SoundboardGlobalConfigViewModel)} config saved");
            }
            catch (Exception ex)
            {
                Debug.Fail($"{nameof(SoundboardGlobalConfigViewModel)} config NOT saved");
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
