using NAudio.Wave;
using Soundboard4MacroDeck.Base;
using Soundboard4MacroDeck.Models;
using System;
using NAudio.CoreAudioApi;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.Server;
using System.Collections.Generic;
using System.Linq;
using SuchByte.MacroDeck.Logging;

namespace Soundboard4MacroDeck.Services
{
    public sealed class SoundPlayer
    {
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
                actionButton = actionButton,
                internalId = actionButton.Guid,
            };

            Retry.Do(() => soundPlayer.Play(action));
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
                    SoundPlayers.Clear();
                }
            }
        }
        public void StopCurrent(bool stopThisOnly)
        {
            if (!stopThisOnly)
            {
                StopAll();
                return;
            }
            lock (key)
            {
                if (SoundPlayers.Count != 0)
                {
                    foreach (var player in SoundPlayers.ToArray().Where(p => p.internalId.Equals(internalId)))
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
        private ActionButton actionButton;
        private string internalId;

        private SoundPlayer()
        {
        }

        public bool Equals(SoundPlayer soundPlayer)
        {
            return internalId.Equals(soundPlayer.internalId);
        }

        public override bool Equals(object obj)
        {
            return obj is SoundPlayer sp && Equals(sp);
        }

        public override int GetHashCode()
        {
            return internalId.GetHashCode();
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

        private void Play(SoundboardActions action)
        {
            switch (action)
            {
                case SoundboardActions.Play:
                    StopAll();
                    PlayAudio();
                    break;

                case SoundboardActions.Overlap:
                    PlayAudio();
                    break;

                case SoundboardActions.PlayStop:
                    PlayOrStop();
                    break;

                case SoundboardActions.Loop:
                    PlayOrStop(enableLoop: true);
                    break;

                default:
                    break;
            }

        }

        private void PlayOrStop(bool enableLoop = false)
        {
            MacroDeckLogger.Trace(Main.Instance, internalId);
            //bool currentlyPlaying = actionButton.State;
            bool currentlyPlaying = SoundPlayers.Any(p => p.internalId.Equals(internalId));
            StopCurrent(currentlyPlaying);
            if (!currentlyPlaying)
            {
                PlayAudio(enableLoop);
            }
        }

        private void PlayAudio(bool enableLoop = false)
        {

            EnsureOutputDevice();
            if (actionParameters.SyncButtonState)
            {
                outputDevice.PlaybackStopped += (s, e) => MacroDeckServer.SetState(actionButton, false);
                MacroDeckServer.SetState(actionButton, true);
            }

            if (fileReader is null || !fileReader.FileName.Equals(actionParameters.FileName))
            {
                fileReader = new AudioBytesReader(actionParameters.FileName, actionParameters.FileData)
                {
                    Volume = Math.Min(actionParameters.Volume / 100f, 1f),
                    LoopingEnabled = enableLoop,
                };
                outputDevice.Init(fileReader);
            }

            SoundPlayers.Add(this);

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
