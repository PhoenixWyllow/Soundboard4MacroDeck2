using MacroDeckSoundboard.Models;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace MacroDeckSoundboard.ViewModels
{
    internal class SoundboardActionConfigViewModel
    {
        private readonly IMacroDeckAction _action;
        private readonly ActionParameters _parameters;

        public SoundboardActionConfigViewModel(IMacroDeckAction action, ActionConfigurator actionConfigurator)
        {
            _action = action;
            _parameters = ActionParameters.Deserialize(_action.Configuration);
        }

        public void SaveConfig()
        {
            try
            {
                _action.Configuration = _parameters.Serialize();
                Debug.WriteLine($"{nameof(SoundboardActionConfigViewModel)} saved");
            }
            catch (Exception ex)
            {
                Debug.Fail($"{nameof(SoundboardActionConfigViewModel)} NOT saved");
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task<bool> GetBytesFrom(string fileOrURL, System.Windows.Forms.ProgressBar progressBar)
        {
            byte[] data = null;
            Uri uri = new Uri(fileOrURL);
            if (uri.IsWellFormedOriginalString())
            {
                if (uri.IsFile)
                {
                    data = await System.IO.File.ReadAllBytesAsync(fileOrURL);
                }
                else
                {
                    data = await GetFromUrl(uri, progressBar);
                }

                if (data != null && !Services.SoundPlayer.Instance.IsValidFile(data))
                {
                    data = null;
                }
            }
            _parameters.FileData = data;

            return data != null;
        }

        private Task<byte[]> GetFromUrl(Uri uri, System.Windows.Forms.ProgressBar progressBar)
        {
            try
            {
                progressBar.Visible = true;

                using var webClient = new System.Net.WebClient();
                webClient.DownloadProgressChanged += (s, e) =>
                {
                    progressBar.Value = e.ProgressPercentage;
                };
                webClient.DownloadFileCompleted += (s, e) =>
                {
                    progressBar.Visible = false;
                };

                return webClient.DownloadDataTaskAsync(uri);
            }
            catch (Exception ex)
            {
                //forbidden, proxy issues, file not found (404) etc
            }
            finally
            {
                progressBar.Visible = false;
            }

            return null;
        }
    }
}
