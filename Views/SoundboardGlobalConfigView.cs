using Soundboard4MacroDeck.Services;
using Soundboard4MacroDeck.ViewModels;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Plugins;

namespace Soundboard4MacroDeck.Views
{
    public partial class SoundboardGlobalConfigView : DialogForm
    {
        private readonly SoundboardGlobalConfigViewModel _viewModel;
        public SoundboardGlobalConfigView(IMacroDeckPlugin plugin)
        {
            _viewModel = new SoundboardGlobalConfigViewModel(plugin);
            InitializeComponent();
            ApplyLocalization();
        }
        private void ApplyLocalization()
        {
            this.linkLabelResetDevice.Text = Localization.Instance.UseSystemDefaultDevice;
            this.labelDevices.Text = Localization.Instance.OutputDevicesGlobal;
            this.buttonOK.Text = LanguageManager.Strings.Ok;
        }

        private void SoundboardGlobalConfigView_Load(object sender, System.EventArgs e)
        {
            _viewModel.Load();
            this.comboBoxDevices.Items.AddRange(_viewModel.Devices.ToArray());
            this.comboBoxDevices.SelectedIndex = _viewModel.DevicesIndex;
        }

        private void ComboBoxDevices_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            _viewModel.SetDevice(comboBoxDevices.SelectedIndex);
        }

        private void ButtonOK_Click(object sender, System.EventArgs e)
        {
            _viewModel.SaveConfig();
        }

        private void LinkLabelResetDevice_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            _viewModel.ResetDevice();
        }
    }
}
