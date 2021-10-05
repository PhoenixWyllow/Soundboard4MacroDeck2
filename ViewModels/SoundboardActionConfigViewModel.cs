using Soundboard4MacroDeck.Actions;
using Soundboard4MacroDeck.Models;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SystemIOFile = System.IO.File;
using SystemNetWebClient = System.Net.WebClient;

namespace Soundboard4MacroDeck.ViewModels
{
    public class SoundboardActionConfigViewModel : OutputDeviceConfigurationViewModel
    {
        private readonly IMacroDeckAction _action;
        private ActionParameters Parameters => _outputConfiguration as ActionParameters;
        public string LastCheckedPath => Parameters.FilePath;

        public int PlayVolume
        {
            get => Parameters.Volume;
            set => Parameters.Volume = value;
        }

        public SoundboardActionConfigViewModel(IMacroDeckAction action, IMacroDeckPlugin plugin)
            : base(plugin, ActionParameters.Deserialize(action.Configuration))
        {
            _action = action;
            //_parameters = _outputConfiguration as ActionParameters;
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

            if (data != null && Services.SoundPlayer.IsValidFile(data, out string extension))
            {
                Parameters.FileData = data;
                Parameters.FilePath = filePath;
                Parameters.FileExt = extension;
            }
            return data != null;
        }

        public async Task<bool> GetFromUrl(string urlPath, System.Windows.Forms.ProgressBar progressBar)
        {
            byte[] data = null;
            string extension = string.Empty;
            try
            {
                progressBar.Visible = true;

                using var webClient = new SystemNetWebClient();
                webClient.DownloadProgressChanged += (s, e) =>
                {
                    progressBar.Value = e.ProgressPercentage;
                };
                webClient.DownloadFileCompleted += (s, e) =>
                {
                    progressBar.Visible = false;
                };

                data = await webClient.DownloadDataTaskAsync(urlPath);

                if (data != null && !Services.SoundPlayer.IsValidFile(data, out extension))
                {
                    data = null;
                }
            }
            catch (Exception ex)
            {
                //forbidden, proxy issues, file not found (404) etc
            }
            finally
            {
                progressBar.Visible = false;

                if (data != null && !string.IsNullOrWhiteSpace(extension))
                {
                    Parameters.FileData = data;
                    Parameters.FilePath = urlPath;
                    Parameters.FileExt = Base.AudioFileTypes.Extensions.FirstOrDefault(ext => ext.EndsWith(extension));
                }
            }

            return data != null;
        }
    }
}
