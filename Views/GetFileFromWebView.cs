using Soundboard4MacroDeck.ViewModels;
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Soundboard4MacroDeck.Views
{
    public partial class GetFileFromWebView : DialogForm
    {
        private readonly SoundboardActionConfigViewModel _viewModel;
        private bool checkedFile;
        public GetFileFromWebView(SoundboardActionConfigViewModel parentViewModel)
        {
            _viewModel = parentViewModel;

            InitializeComponent();

            urlBox.Text = _viewModel.LastCheckedPath;
            checkedFile = string.IsNullOrWhiteSpace(_viewModel.LastCheckedPath);
        }

        private async void ButtonOK_Click(object sender, EventArgs e)
        {
            bool hasFile = !checkedFile 
                && (urlBox.Text.Equals(_viewModel.LastCheckedPath) 
                || await _viewModel.GetFromUrl(urlBox.Text, fileProgressBar));
            if (!hasFile)
            {
                urlBox.Text = _viewModel.LastCheckedPath;
                using var messageBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
                messageBox.ShowDialog("InvalidFile", "CouldNotUseFile", MessageBoxButtons.OK);
                return;
            }
            checkedFile = true;
        }

        private void UrlBox_TextChanged(object sender, EventArgs e)
        {
            checkedFile = false;
        }
    }
}
