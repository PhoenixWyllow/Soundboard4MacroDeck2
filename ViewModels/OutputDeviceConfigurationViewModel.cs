using NAudio.CoreAudioApi;
using Soundboard4MacroDeck.Models;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Soundboard4MacroDeck.ViewModels
{
    public abstract class OutputDeviceConfigurationViewModel : ISoundboardBaseConfigViewModel
    {
        protected readonly IMacroDeckPlugin _plugin;


        protected IOutputConfiguration _outputConfiguration;
        protected IOutputConfiguration GlobalOutputConfiguration => GlobalParameters.Deserialize(PluginConfiguration.GetValue(_plugin, nameof(SoundboardGlobalConfigViewModel)));
        public List<MMDevice> Devices { get; set; }
        public int DevicesIndex { get; set; }

        ISerializableConfiguration ISoundboardBaseConfigViewModel.SerializableConfiguration => _outputConfiguration;

        protected OutputDeviceConfigurationViewModel(IMacroDeckPlugin plugin)
        {
            _plugin = plugin;
            _outputConfiguration = GlobalOutputConfiguration;
        }

        protected OutputDeviceConfigurationViewModel(IMacroDeckPlugin plugin, IOutputConfiguration parameters)
        {
            _plugin = plugin;
            _outputConfiguration = parameters;
        }

        public void Load()
        {
            if (_outputConfiguration.MustGetDefaultDevice())
            {
                _outputConfiguration.OutputDeviceId = GlobalOutputConfiguration.OutputDeviceId;
            }
            FetchAvailableDevices();
        }

        public abstract void SetConfig();

        public void SetDevice(int selectedIndex)
        {
            DevicesIndex = selectedIndex;
            _outputConfiguration.OutputDeviceId = Devices[selectedIndex].ID;
            _outputConfiguration.UseDefaultDevice = false;
        }

        public void ResetDevice()
        {
            if (_outputConfiguration is GlobalParameters)
            {
                using var enumerator = new MMDeviceEnumerator();
                SetDefaultDevice(enumerator);
            }
            else
            {
                _outputConfiguration.OutputDeviceId = GlobalOutputConfiguration.OutputDeviceId;
                DevicesIndex = Devices.FindIndex(d => d.ID.Equals(GlobalOutputConfiguration.OutputDeviceId));
            }
            _outputConfiguration.UseDefaultDevice = true;
        }


        private void FetchAvailableDevices()
        {
            bool needsDefault = _outputConfiguration.MustGetDefaultDevice();
            List<MMDevice> devices = new List<MMDevice>();

            int idx = 0;
            using var enumerator = new MMDeviceEnumerator();
            foreach (var device in enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
            {
                devices.Add(device);
                if (!needsDefault && device.ID.Equals(_outputConfiguration.OutputDeviceId))
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
            SetDevice(Devices.FindIndex(d => d.ID.Equals(device.ID)));
        }

        public void SaveConfig()
        {
            try
            {
                SetConfig();
                Debug.WriteLine($"{GetType().Name} config saved");
            }
            catch (Exception ex)
            {
                Debug.Fail($"{GetType().Name} config NOT saved");
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
