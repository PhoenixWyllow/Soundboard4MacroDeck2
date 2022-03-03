using Soundboard4MacroDeck.Models;
using Soundboard4MacroDeck.Services;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Linq;
using System.Threading.Tasks;
using SystemIOFile = System.IO.File;
using SystemNetWebClient = System.Net.WebClient;

namespace Soundboard4MacroDeck.ViewModels
{
    public class SoundboardActionConfigViewModel : OutputDeviceConfigurationViewModel
    {
        private readonly PluginAction _action;
        private ActionParameters Parameters => OutputConfiguration as ActionParameters;
        public string LastCheckedPath => Parameters.FilePath;

        public int PlayVolume
        {
            get => Parameters.Volume;
            set => Parameters.Volume = value;
        }

        public bool SyncButtonState
        {
            get => Parameters.SyncButtonState;
            set => Parameters.SyncButtonState = value;
        }

        public SoundboardActionConfigViewModel(PluginAction action)
            : base(ActionParameters.Deserialize(action.Configuration))
        {
            _action = action;
        }

        public override void SetConfig()
        {
            _action.ConfigurationSummary = Parameters.FileName;
            _action.Configuration = Parameters.Serialize();
        }

        public bool GetBytesFromFile(string filePath)
        {
            byte[] data = null;
            if (SystemIOFile.Exists(filePath))
            {
                data = SystemIOFile.ReadAllBytes(filePath);
            }

            return TryApplyFile(data, filePath);
        }

        public async Task<bool> GetFromUrlAsync(string urlPath, Action<int> progressCallback)
        {
            bool success = false;
            try
            {
                using var webClient = new SystemNetWebClient();
                webClient.DownloadProgressChanged += (s, e) =>
                {
                    progressCallback?.Invoke(e.ProgressPercentage);
                };
                webClient.DownloadDataCompleted += (s, e) =>
                {
                    success = TryApplyFile(e.Result, urlPath);
                };

                await webClient.DownloadDataTaskAsync(urlPath).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //forbidden, proxy issues, file not found (404) etc
                MacroDeckLogger.Error(Main.Instance, typeof(SoundboardActionConfigViewModel), $"{nameof(GetFromUrlAsync)}: {ex.Message}");
            }

            return success;
        }

        private bool TryApplyFile(byte[] data, string urlPath)
        {
            if (data != null
                && Base.AudioFileTypes.IsValidFile(data, out string extension))
            {
                Parameters.FileData = data;
                Parameters.FilePath = urlPath;
                Parameters.FileExt = extension;// Base.AudioFileTypes.Extensions.FirstOrDefault(ext => ext.EndsWith(extension));
                return true;
            }
            return false;
        }

    }
}
