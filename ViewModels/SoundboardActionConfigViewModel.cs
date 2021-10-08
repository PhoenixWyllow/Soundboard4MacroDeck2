using Myrmec;
using Soundboard4MacroDeck.Actions;
using Soundboard4MacroDeck.Models;
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
        private readonly IMacroDeckAction _action;
        private ActionParameters Parameters => OutputConfiguration as ActionParameters;
        public string LastCheckedPath => Parameters.FilePath;

        public int PlayVolume
        {
            get => Parameters.Volume;
            set => Parameters.Volume = value;
        }

        public SoundboardActionConfigViewModel(IMacroDeckAction action)
            : base(ActionParameters.Deserialize(action.Configuration))
        {
            _action = action;
            Parameters.ActionType = (SoundboardActions)((_action as ISoundboardAction)?.ActionType);
        }
        
        public override void SetConfig()
        {
            _action.Configuration = Parameters.Serialize();
        }

        public async Task<bool> GetBytesFromFileAsync(string filePath)
        {
            byte[] data = null;
            if (SystemIOFile.Exists(filePath))
            {
                data = await SystemIOFile.ReadAllBytesAsync(filePath);
            }

            return TryApplyFile(data, filePath);
        }

        public async Task<bool> GetFromUrl(string urlPath, Action<int> progressCallback)
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
            }

            return success;
        }

        private bool TryApplyFile(byte[] data, string urlPath)
        {
            if (data != null
                && IsValidFile(data, out string extension))
            {
                Parameters.FileData = data;
                Parameters.FilePath = urlPath;
                Parameters.FileExt = Base.AudioFileTypes.Extensions.FirstOrDefault(ext => ext.EndsWith(extension));
                return true;
            }
            return false;
        }

        public bool IsValidFile(byte[] data, out string extension)
        {
            byte[] fileHead = new byte[100];

            Array.Copy(data, fileHead, fileHead.Length);

            var sniffer = new Sniffer();
            sniffer.Populate(Base.AudioFileTypes.Records);

            var matches = sniffer.Match(fileHead);
            if (matches.Count > 0)
            {
                extension = matches[0];
                return true;
            }
            extension = string.Empty;
            return false;
        }
    }
}
