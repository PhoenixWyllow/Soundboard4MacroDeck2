using MacroDeckSoundboard.Models;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroDeckSoundboard.Views
{
    public partial class SoundboardActionConfig : ActionConfigControl
    {
        private readonly IMacroDeckAction _action;
        private readonly ActionParameters _parameters;

        public SoundboardActionConfig(IMacroDeckAction action, ActionConfigurator actionConfigurator)
        {
            _action = action;
            _parameters = ActionParameters.Deserialize(_action.Configuration);

            InitializeComponent();
            InitMore();
            this.Load += SoundboardActionConfig_Load;
            this.filePath.TextChanged += FilePath_TextChanged;
            //this.ParentForm.FormClosing += ParentForm_FormClosing;
            actionConfigurator.ActionSave += OnActionSave;
        }

        private void OnActionSave(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void InitMore()
        {
            // openFileDialog
            //this.openFileDialog.Filter = "Audio File (*.mp3;*.aiff;*.wav;*.wma;*.aac)|*.mp3;*.aiff;*.wav;*.wma;*.aac";
            var types = $"*.{string.Join(";*.",Lib.SoundPlayer.Extensions)};*.aac";
            this.openFileDialog.Filter = $"Audio File ({types})|{types}";
            this.openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void SoundboardActionConfig_Load(object sender, EventArgs e)
        {
        }
        private void SaveConfig()
        {
            try
            {
                _action.Configuration = _parameters.Serialize();
                Debug.WriteLine($"{nameof(SoundboardActionConfig)} saved");
            }
            catch 
            {
            }
        }

        private async void FilePath_TextChanged(object sender, EventArgs e)
        {
            var file = await GetBytesFrom(((TextBox)sender).Text);
            if (file is null)
            {
                ((TextBox)sender).Text = "";
                using var messageBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
                messageBox.ShowDialog("InvalidFile", "CouldNotUseFile", MessageBoxButtons.OK);
                return;
            }
            _parameters.FileData = file;
        }

        private Task<byte[]> GetBytesFrom(string fileOrURL)
        {
            Uri uri = new Uri(fileOrURL);
            if (uri.IsFile)
            {
                return System.IO.File.ReadAllBytesAsync(fileOrURL);
            }
            return GetFromUrl(uri);
        }

        private async Task<byte[]> GetFromUrl(Uri uri)
        {
            byte[] data = null;

            try
            {
                fileProgressBar.Visible = true;
                using (var webClient = new System.Net.WebClient())
                {
                    webClient.DownloadProgressChanged += (s, e) =>
                    {
                        fileProgressBar.Value = e.ProgressPercentage;
                    };
                    webClient.DownloadFileCompleted += (s, e) =>
                    {
                        fileProgressBar.Visible = false;
                    };

                    data = await webClient.DownloadDataTaskAsync(uri).ConfigureAwait(false);
                }
                if (data is null || !Lib.SoundPlayer.Instance.IsValidFile(data))
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                //forbidden, proxy issues, file not found (404) etc
            }
            finally
            {
                fileProgressBar.Visible = false;
            }

            return data;
        }

    }
}
