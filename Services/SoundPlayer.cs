using NAudio.Wave;
using Soundboard4MacroDeck.Base;
using Soundboard4MacroDeck.Models;
using System;
using Myrmec;
using NAudio.CoreAudioApi;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.Server;
using System.Collections.Generic;

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

        public static void Execute(SoundboardActions action, string config, ActionButton actionButton)
        {
            var actionParameters = ActionParameters.Deserialize(config);
            if (actionParameters.FileData is null)
            {
                return;
            }

            SoundPlayer soundPlayer = new SoundPlayer()
            {
                actionParameters = actionParameters,
            };
            SoundPlayers.Add(soundPlayer);

            Retry.Do(() => soundPlayer.Play(action, actionButton));
        }

        public static void StopAll()
        {
            lock (key)
            {
                if (SoundPlayers.Count != 0)
                {
                    foreach (var player in SoundPlayers.ToArray())
                    {
                        player.StopAudio();
                    }
                }
            }
        }

        private static readonly object key = new object();
        private static List<SoundPlayer> SoundPlayers { get; } = new List<SoundPlayer>();

        private IWavePlayer outputDevice;
        private AudioBytesReader fileReader;
        private ActionParameters actionParameters;
        private Guid internalGuid;

        private SoundPlayer()
        {
            internalGuid = Guid.NewGuid();
        }

        public bool Equals(SoundPlayer soundPlayer)
        {
            return internalGuid.Equals(soundPlayer.internalGuid);
        }

        public override bool Equals(object obj)
        {
            return obj is SoundPlayer sp && Equals(sp);
        }

        public override int GetHashCode()
        {
            return internalGuid.GetHashCode();
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

            SoundPlayers.Remove(this);
        }

        private void Play(SoundboardActions action, ActionButton actionButton)
        {
            switch (action)
            {
                case SoundboardActions.Overlap:
                    PlayAudio(actionButton);
                    break;

                case SoundboardActions.PlayStop:
                    PlayOrStop(actionButton);
                    break;

                case SoundboardActions.Play:
                    StopAll();
                    PlayAudio(actionButton);
                    break;

                case SoundboardActions.Loop:
                    LoopOrStop(actionButton);
                    break;

                default:
                    break;
            }

        }

        private void PlayOrStop(ActionButton actionButton)
        {
            bool currentlyPlaying = actionButton.State;
            StopAll();
            if (!currentlyPlaying)
            {
                PlayAudio(actionButton);
            }
        }

        private void LoopOrStop(ActionButton actionButton)
        {
            bool currentlyPlaying = actionButton.State;
            StopAll();
            if (!currentlyPlaying)
            {
                PlayAudio(actionButton, enableLoop: true);
            }
        }

        private void PlayAudio(ActionButton actionButton, bool enableLoop = false)
        {
            EnsureOutputDevice();
            outputDevice.PlaybackStopped += (s, e) => MacroDeckServer.SetState(actionButton, false);
            MacroDeckServer.SetState(actionButton, true);

            if (fileReader is null || !fileReader.FileName.Equals(actionParameters.FileName))
            {
                fileReader = new AudioBytesReader(actionParameters.FileName, actionParameters.FileData)
                {
                    Volume = Math.Min(actionParameters.Volume / 100f, 1f),
                    LoopingEnabled = enableLoop,
                };
                outputDevice.Init(fileReader);
            }

            outputDevice.Play();
        }

        private void EnsureOutputDevice()
        {
            if (outputDevice == null)
            {
                outputDevice = new WasapiOut(GetDevice(), AudioClientShareMode.Shared, true, 200); //setting only the device, others should be as default.
                outputDevice.PlaybackStopped += OnPlaybackStopped;
            }
        }

        private MMDevice GetDevice()
        {
            using var devices = new MMDeviceEnumerator();
            if (!actionParameters.MustGetDefaultDevice())
            {
                return devices.GetDevice(actionParameters.OutputDeviceId);
            }
            IOutputConfiguration globalParameters = Main.Configuration;
            return !globalParameters.MustGetDefaultDevice() //if
                ? devices.GetDevice(globalParameters.OutputDeviceId)
                : devices.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia); //else
        }
    }

}
