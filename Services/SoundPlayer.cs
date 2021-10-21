using NAudio.Wave;
using Soundboard4MacroDeck.Base;
using Soundboard4MacroDeck.Models;
using System;
using Myrmec;
using SuchByte.MacroDeck.Plugins;
using NAudio.CoreAudioApi;

namespace Soundboard4MacroDeck.Services
{
    public sealed class SoundPlayer
    {
        public static bool IsValidFile(byte[] data, out string extension)
        {
            byte[] fileHead = new byte[100];

            Array.Copy(data, fileHead, fileHead.Length);

            Sniffer sniffer = new Sniffer();
            sniffer.Populate(AudioFileTypes.Records);

            var matches = sniffer.Match(fileHead);
            if (matches.Count > 0)
            {
                extension = matches[0];
                return true;
            }
            extension = string.Empty;
            return false;
        }

        public static void CreateInstance(MacroDeckPlugin plugin)
        {
            if (Instance is null)
            {
                lock (load)
                {
                    Instance = new SoundPlayer(plugin);
                }
            }
        }
        private static readonly object load = new object();

        public static SoundPlayer Instance { get; private set; }// => _instance.Value;
        //private static readonly Lazy<SoundPlayer> _instance = new Lazy<SoundPlayer>(() => new SoundPlayer());

        private SoundPlayer(MacroDeckPlugin plugin)
        {
            _plugin = plugin;
        }

        private readonly MacroDeckPlugin _plugin;

        private IWavePlayer outputDevice;
        private AudioBytesReader fileReader;
        private ActionParameters actionParameters;

        public IOutputConfiguration GetGlobalConfiguration() => GlobalParameters.Deserialize(PluginConfiguration.GetValue(_plugin, nameof(ViewModels.SoundboardGlobalConfigViewModel)));

        public void Execute(SoundboardActions action, string config)
        {
            actionParameters = ActionParameters.Deserialize(config);

            if (actionParameters.FileData is null)
            {
                return;
            }

            Retry.Do(() => Play(action));
        }

        public void StopAll()
        {
            StopAudio();
        }

        private void Play(SoundboardActions action)
        {
            switch (action)
            {
                case SoundboardActions.Play:
                    StopAll();
                    PlaySingle();
                    break;

                case SoundboardActions.Overlap:
                    PlaySingle();
                    break;
                default:
                    break;
            }

        }

        private void StopAudio()
        {
            outputDevice?.Stop();
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            outputDevice?.Dispose();
            outputDevice = null;
            fileReader?.Dispose();
            fileReader = null;
        }

        private MMDevice GetDevice()
        {
            using var devices = new MMDeviceEnumerator();
            if (actionParameters.MustGetDefaultDevice())
            {
                IOutputConfiguration globalParameters = GetGlobalConfiguration();
                return !globalParameters.MustGetDefaultDevice() //if
                    ? devices.GetDevice(globalParameters.OutputDeviceId)
                    : devices.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia); //else
            }
            return devices.GetDevice(actionParameters.OutputDeviceId);
        }

        private void SetFile()
        {
            fileReader = new AudioBytesReader(actionParameters.FileName, actionParameters.FileData)
            {
                Volume = Math.Min(actionParameters.Volume / 100f, 1f)
            };
            outputDevice.Init(fileReader);
        }

        private void PlaySingle()
        {
            if (outputDevice == null)
            {
                outputDevice = new WasapiOut(GetDevice(), AudioClientShareMode.Shared, true, 200); //setting only the device, others should be as default.
                outputDevice.PlaybackStopped += OnPlaybackStopped;
            }

            if (fileReader is null || !fileReader.FileName.Equals(actionParameters.FileName))
            {
                SetFile();
            }

            outputDevice.Play();
        }
    }

}
