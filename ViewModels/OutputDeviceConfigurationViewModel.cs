using NAudio.CoreAudioApi;
using Soundboard4MacroDeck.Models;
using SuchByte.MacroDeck.Logging;
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

        public bool IsDefaultDevice() => OutputConfiguration.MustGetDefaultDevice();

        public event EventHandler OnSetDeviceIndex;

        ISerializableConfiguration ISoundboardBaseConfigViewModel.SerializableConfiguration => OutputConfiguration;

        protected OutputDeviceConfigurationViewModel(IOutputConfiguration parameters)
        {
            OutputConfiguration = parameters;
            _globalOutputConfiguration = parameters as GlobalParameters ?? Main.Configuration;
        }

        public void LoadDevices()
        {
            if (IsDefaultDevice())
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
            SetDeviceIndex(IsDefaultDevice(), OutputConfiguration.OutputDeviceId);
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
                MacroDeckLogger.Info(Main.Instance, $"{GetType().Name}: config saved");
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(Main.Instance, $"{GetType().Name}: config NOT saved");
                MacroDeckLogger.Error(Main.Instance, $"{GetType().Name}: {ex.Message}");
            }
        }
    }
}
