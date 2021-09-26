using System;
using System.Collections.Immutable;
using MacroDeckSoundboard.Models;
using MimeDetective;
using MimeDetective.Storage;
using NAudio.Wave;

namespace MacroDeckSoundboard.Services
{
    public sealed class SoundPlayer
    {
        public static readonly ImmutableArray<string> Extensions = new[]{
            "aif","m4a","mid","midi","mp3","ogg","wav","wma",
            }.ToImmutableArray();

        public static SoundPlayer Instance => _instance.Value;
        private static readonly Lazy<SoundPlayer> _instance = new Lazy<SoundPlayer>(() => new SoundPlayer());
        private ContentInspector Inspector { get; }
        private SoundPlayer()
        {
            var AllDefinitions = new MimeDetective.Definitions.CondensedBuilder()
            {
                UsageType = MimeDetective.Definitions.Licensing.UsageType.PersonalNonCommercial
            }.Build();
            
            var ScopedDefinitions = AllDefinitions
                .ScopeExtensions(Extensions) //Limit results to only the extensions provided
                .TrimMeta() //don't care about the meta information (definition author, creation date, etc)
                .TrimDescription() //don't care about the description
                .TrimMimeType() //don't care about the mime type
                .ToImmutableArray();

            Inspector = new ContentInspectorBuilder()
            {
                Definitions = ScopedDefinitions,
            }.Build();
        }

        public bool IsValidFile(byte[] data)
        {
            var results = Inspector.Inspect(data.ToImmutableArray());
            return !results.IsDefaultOrEmpty;
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
