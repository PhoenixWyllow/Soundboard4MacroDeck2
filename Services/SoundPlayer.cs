using NAudio.Wave;
using Soundboard4MacroDeck.Models;
using System;
using Myrmec;
using System.IO;
using System.Linq;

namespace Soundboard4MacroDeck.Services
{
    public sealed class SoundPlayer
    {
        public static void CreateInstance()
        {
            if (Instance is null)
            {
                lock (load)
                {
                    Instance = new SoundPlayer();
                }
            }
        }
        private static readonly object load = new object();

        public static SoundPlayer Instance { get; private set; }// => _instance.Value;
        //private static readonly Lazy<SoundPlayer> _instance = new Lazy<SoundPlayer>(() => new SoundPlayer());

        private SoundPlayer()
        {
        }

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

        private WaveOutEvent _outputDevice;
        private AudioBytesReader _fileReader;

        private ActionParameters Parameters { get; set; }


        public void Execute(string config)
        {
            var parameters = ActionParameters.Deserialize(config);

            if (parameters.FileData is null)
            {
                return;
            }

            Parameters = parameters;

            Retry.Do(Play, retryInterval: TimeSpan.FromSeconds(1.0), maxAttemptCount: 3);
        }

        public void StopAll()
        {
            StopAudio();
        }

        private void Play()
        {
            switch (Parameters.ActionType)
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
            _outputDevice?.Stop();
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            _outputDevice?.Dispose();
            _outputDevice = null;
            _fileReader?.Dispose();
            _fileReader = null;
        }

        private void PlaySingle()
        {
            if (_outputDevice == null)
            {
                _outputDevice = new WaveOutEvent();
                _outputDevice.PlaybackStopped += OnPlaybackStopped;
            }

            _outputDevice.Volume = Math.Min(Parameters.Volume / 100f, 1f);

            if (_fileReader is null || !_fileReader.FileName.Equals(Parameters.FileName))
            {
                _fileReader = new AudioBytesReader(Parameters.FileName, Parameters.FileData);
                _outputDevice.Init(_fileReader);
            }

            _outputDevice.Play();
        }
    }

}
