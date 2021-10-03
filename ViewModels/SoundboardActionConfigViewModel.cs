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
    public class SoundboardActionConfigViewModel
    {
        private readonly IMacroDeckAction _action;
        private readonly ActionParameters _parameters;
        public string LastCheckedPath => _parameters.FilePath;

        public int PlayVolume
        {
            get => _parameters.Volume;
            set => _parameters.Volume = value;
        }

        public SoundboardActionConfigViewModel(IMacroDeckAction action)
        {
            _action = action;
            _parameters = ActionParameters.Deserialize(action.Configuration);
            _parameters.ActionType = (SoundboardActions)((_action as ISoundboardPlayAction)?.ActionType);
        }

        public void SaveConfig()
        {
            try
            {
                _action.Configuration = _parameters.Serialize();
                Debug.WriteLine($"{nameof(SoundboardActionConfigViewModel)} config saved");
            }
            catch (Exception ex)
            {
                Debug.Fail($"{nameof(SoundboardActionConfigViewModel)} config NOT saved");
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task<bool> GetBytesFromFileAsync(string filePath)
        {
            byte[] data = null;
            if (SystemIOFile.Exists(filePath))
            {
                data = await SystemIOFile.ReadAllBytesAsync(filePath);

                if (data != null && !Services.SoundPlayer.IsValidFile(data, out string extension))
                {
                    data = null;
                }
            }
            if (data != null)
            {
                _parameters.FileData = data;
                _parameters.FilePath = filePath;
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
                    _parameters.FileData = data;
                    _parameters.FilePath = urlPath;
                    _parameters.FileExt = AudioFileTypes.Extensions.FirstOrDefault(ext => ext.EndsWith(extension));
                }
            }

            return data != null;
        }
    }
}
