using NAudio.CoreAudioApi;

using Soundboard4MacroDeck.Models;

using SuchByte.MacroDeck.Logging;

namespace Soundboard4MacroDeck.ViewModels;

public abstract class OutputDeviceConfigurationViewModel : ISoundboardBaseConfigViewModel
{
    private readonly IOutputConfiguration _globalOutputConfiguration;
    protected IOutputConfiguration OutputConfiguration { get; }
    public List<MMDevice> Devices { get; private set; } = [];
    public int DevicesIndex { get; private set; }
    //public int Latency { get => OutputConfiguration.Latency; set => OutputConfiguration.Latency = value; }

    public bool IsDefaultDevice() => OutputConfiguration.MustGetDefaultDevice();

    public event EventHandler? OnSetDeviceIndex;

    ISerializableConfiguration ISoundboardBaseConfigViewModel.SerializableConfiguration => OutputConfiguration;

    protected OutputDeviceConfigurationViewModel(IOutputConfiguration parameters)
    {
        OutputConfiguration = parameters;
        _globalOutputConfiguration = parameters as GlobalParameters ?? PluginInstance.Configuration;
    }

    //public void ResetLatency()
    //{
    //    Latency = !(OutputConfiguration is GlobalParameters) ? _globalOutputConfiguration.Latency : 200;
    //}

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
        Devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).ToList();
    }

    public void LoadDeviceIndex()
    {
        SetDeviceIndex(IsDefaultDevice(), OutputConfiguration.OutputDeviceId!);
    }

    public void ResetDevice()
    {
        SetDeviceIndex(OutputConfiguration is GlobalParameters, _globalOutputConfiguration.OutputDeviceId!);
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
        OnSetDeviceIndex?.Invoke(this, EventArgs.Empty);
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
            MacroDeckLogger.Info(PluginInstance.Current, $"{GetType().Name}: config saved");
        }
        catch (Exception ex)
        {
            MacroDeckLogger.Error(PluginInstance.Current, $"{GetType().Name}: config NOT saved");
            MacroDeckLogger.Error(PluginInstance.Current, $"{GetType().Name}: {ex.Message}");
        }
    }
}