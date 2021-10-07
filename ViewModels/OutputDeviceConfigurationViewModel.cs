using NAudio.CoreAudioApi;
using Soundboard4MacroDeck.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Soundboard4MacroDeck.ViewModels
{
    public abstract class OutputDeviceConfigurationViewModel : ISoundboardBaseConfigViewModel
    {
        private readonly IOutputConfiguration _globalOutputConfiguration;
        protected IOutputConfiguration OutputConfiguration { get; }
        public List<MMDevice> Devices { get; private set; }
        public int DevicesIndex { get; private set; }

        public bool IsDefaultDevice() => OutputConfiguration.UseDefaultDevice;

        public event EventHandler OnSetDeviceIndex;

        ISerializableConfiguration ISoundboardBaseConfigViewModel.SerializableConfiguration => OutputConfiguration;

        protected OutputDeviceConfigurationViewModel(IOutputConfiguration parameters)
        {
            OutputConfiguration = parameters;
            _globalOutputConfiguration = parameters as GlobalParameters ?? Services.SoundPlayer.Instance.GetGlobalConfiguration();
        }

        public void LoadDevices()
        {
            if (OutputConfiguration.MustGetDefaultDevice())
            {
                OutputConfiguration.OutputDeviceId = _globalOutputConfiguration.OutputDeviceId;
            }
            FetchAvailableDevices();
        }

        private void FetchAvailableDevices()
        {
            using var enumerator = new MMDeviceEnumerator();
            Devices = new List<MMDevice>(enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active));
        }

        public void LoadDeviceIndex()
        {
            SetDeviceIndex(OutputConfiguration.MustGetDefaultDevice(), OutputConfiguration.OutputDeviceId);
        }

        public void ResetDevice()
        {
            SetDeviceIndex(OutputConfiguration is GlobalParameters, _globalOutputConfiguration.OutputDeviceId);
            OutputConfiguration.UseDefaultDevice = true;
        }

        private void SetDeviceIndex(bool getDefault, string deviceId)
        {
            if (getDefault)
            {
                using var enumerator = new MMDeviceEnumerator();
                SetDeviceById(enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia).ID);
            }
            else
            {
                SetDeviceById(deviceId);
            }
            OnSetDeviceIndex?.Invoke(DevicesIndex, null);
        }

        private void SetDeviceById(string deviceId)
        {
            DevicesIndex = Devices.FindIndex(d => d.ID.Equals(deviceId));
            OutputConfiguration.OutputDeviceId = deviceId;
        }

        public void SetDevice(int selectedIndex)
        {
            if (DevicesIndex != selectedIndex)
            {
                DevicesIndex = selectedIndex;
                OutputConfiguration.OutputDeviceId = Devices[selectedIndex].ID;
                OutputConfiguration.UseDefaultDevice = false;
            }
        }

        public abstract void SetConfig();

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
