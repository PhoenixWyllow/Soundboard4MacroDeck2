using Soundboard4MacroDeck.Models;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Soundboard4MacroDeck.ViewModels
{
    public class SoundboardActionConfigViewModel
    {
        private readonly IMacroDeckAction _action;
        private readonly ActionParameters _parameters;
        public string LastCheckedPath => _parameters.FilePath;

        public SoundboardActionConfigViewModel(IMacroDeckAction action)
        {
            _action = action;
            _parameters = ActionParameters.Deserialize(_action.Configuration);
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

        public async Task<bool> GetBytesFromFile(string filePath)
        {
            byte[] data = null;
            if (System.IO.File.Exists(filePath))
            {
                data = await System.IO.File.ReadAllBytesAsync(filePath);
                
                if (data != null && !Services.SoundPlayer.Instance.IsValidFile(data))
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

                data = await webClient.DownloadDataTaskAsync(urlPath);

                if (data != null && !Services.SoundPlayer.Instance.IsValidFile(data))
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

                if (data != null)
                {
                    _parameters.FileData = data;
                    _parameters.FilePath = urlPath;
                }
            }

            return data != null;
        }
    }
}
