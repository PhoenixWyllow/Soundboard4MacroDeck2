using NAudio.Wave;
using Soundboard4MacroDeck.Models;
using System;

namespace Soundboard4MacroDeck.Services
{
    public sealed class SoundPlayer
    {
        public static string[] Extensions { get; } = {
            "mid", "midi", "m4a", "mp4a", "mpga", "mp3", "m3a", "ogg", "weba", "aac", "aif", "aiff", "flac", "m3u", "wma", "wav",
            };

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

        public bool IsValidFile(byte[] data)
        {
            return Array.Exists(Extensions, ext => ext == HeyRed.Mime.MimeGuesser.GuessExtension(data));
        }

        private WaveOutEvent _outputDevice;
        private SoundFileHandler _fileHandler;

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
            _outputDevice.Dispose();
            _outputDevice = null;
            _fileHandler?.Dispose();
            _fileHandler = null;
        }

        private void PlaySingle()
        {
            if (_outputDevice == null)
            {
                _outputDevice = new WaveOutEvent();
                _outputDevice.PlaybackStopped += OnPlaybackStopped;
            }

            _outputDevice.Volume = Math.Min(Parameters.Volume / 100f, 1f);

            if (_fileHandler is null)
            {
                _fileHandler = new SoundFileHandler(Parameters.FileData);
                _outputDevice.Init(_fileHandler.RawSource);
            }
            _outputDevice.Play();
        }
    }
    
}
