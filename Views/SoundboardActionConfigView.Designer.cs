
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Windows.Forms;

namespace Soundboard4MacroDeck.Views
{
    partial class SoundboardActionConfigView : ActionConfigControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.filePath = new System.Windows.Forms.TextBox();
            this.fileBrowse = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.buttonGetFromURL = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.volumeBar = new System.Windows.Forms.TrackBar();
            this.labelOr = new System.Windows.Forms.Label();
            this.labelFile = new System.Windows.Forms.Label();
            this.labelVolume = new System.Windows.Forms.Label();
            this.volumeNum = new System.Windows.Forms.NumericUpDown();
            this.labelDevices = new System.Windows.Forms.Label();
            this.comboBoxDevices = new System.Windows.Forms.ComboBox();
            this.checkBoxOverrideDevice = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeNum)).BeginInit();
            this.SuspendLayout();
            // 
            // filePath
            // 
            this.filePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.filePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.filePath.BackColor = System.Drawing.Color.DimGray;
            this.filePath.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.filePath.ForeColor = System.Drawing.Color.White;
            this.filePath.Location = new System.Drawing.Point(44, 36);
            this.filePath.Name = "filePath";
            this.filePath.PlaceholderText = "Get file locally";
            this.filePath.Size = new System.Drawing.Size(380, 23);
            this.filePath.TabIndex = 0;
            this.filePath.TextChanged += new System.EventHandler(this.FilePath_TextChanged);
            // 
            // fileBrowse
            // 
            this.fileBrowse.AutoSize = true;
            this.fileBrowse.BackColor = System.Drawing.Color.DodgerBlue;
            this.fileBrowse.BorderRadius = 8;
            this.fileBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fileBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fileBrowse.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fileBrowse.ForeColor = System.Drawing.Color.White;
            this.fileBrowse.Location = new System.Drawing.Point(430, 32);
            this.fileBrowse.Name = "fileBrowse";
            this.fileBrowse.Size = new System.Drawing.Size(75, 30);
            this.fileBrowse.TabIndex = 1;
            this.fileBrowse.Text = "Browse";
            this.fileBrowse.UseVisualStyleBackColor = true;
            this.fileBrowse.Click += new System.EventHandler(this.FileBrowse_Click);
            // 
            // buttonGetFromURL
            // 
            this.buttonGetFromURL.AutoSize = true;
            this.buttonGetFromURL.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonGetFromURL.BorderRadius = 8;
            this.buttonGetFromURL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGetFromURL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGetFromURL.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonGetFromURL.ForeColor = System.Drawing.Color.White;
            this.buttonGetFromURL.Location = new System.Drawing.Point(575, 32);
            this.buttonGetFromURL.Name = "buttonGetFromURL";
            this.buttonGetFromURL.Size = new System.Drawing.Size(95, 30);
            this.buttonGetFromURL.TabIndex = 3;
            this.buttonGetFromURL.Text = "Get from URL";
            this.buttonGetFromURL.UseVisualStyleBackColor = true;
            this.buttonGetFromURL.Click += new System.EventHandler(this.ButtonGetFromURL_Click);
            // 
            // volumeBar
            // 
            this.volumeBar.Location = new System.Drawing.Point(44, 98);
            this.volumeBar.Maximum = 100;
            this.volumeBar.Name = "volumeBar";
            this.volumeBar.Size = new System.Drawing.Size(316, 45);
            this.volumeBar.TabIndex = 4;
            this.volumeBar.TickFrequency = 10;
            this.volumeBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.volumeBar.ValueChanged += new System.EventHandler(this.VolumeBar_ValueChanged);
            // 
            // labelOr
            // 
            this.labelOr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelOr.Location = new System.Drawing.Point(511, 32);
            this.labelOr.Name = "labelOr";
            this.labelOr.Size = new System.Drawing.Size(58, 30);
            this.labelOr.TabIndex = 5;
            this.labelOr.Text = "or";
            this.labelOr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelFile
            // 
            this.labelFile.AutoSize = true;
            this.labelFile.Location = new System.Drawing.Point(20, 5);
            this.labelFile.Margin = new System.Windows.Forms.Padding(5);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(82, 23);
            this.labelFile.TabIndex = 6;
            this.labelFile.Text = "File path";
            // 
            // labelVolume
            // 
            this.labelVolume.AutoSize = true;
            this.labelVolume.Location = new System.Drawing.Point(20, 67);
            this.labelVolume.Margin = new System.Windows.Forms.Padding(5);
            this.labelVolume.Name = "labelVolume";
            this.labelVolume.Size = new System.Drawing.Size(72, 23);
            this.labelVolume.TabIndex = 7;
            this.labelVolume.Text = "Volume";
            // 
            // volumeNum
            // 
            this.volumeNum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.volumeNum.Location = new System.Drawing.Point(366, 109);
            this.volumeNum.Name = "volumeNum";
            this.volumeNum.Size = new System.Drawing.Size(58, 23);
            this.volumeNum.TabIndex = 8;
            this.volumeNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.volumeNum.ValueChanged += new System.EventHandler(this.VolumeNum_ValueChanged);
            // 
            // labelDevices
            // 
            this.labelDevices.AutoSize = true;
            this.labelDevices.Location = new System.Drawing.Point(20, 151);
            this.labelDevices.Margin = new System.Windows.Forms.Padding(5);
            this.labelDevices.Name = "labelDevices";
            this.labelDevices.Size = new System.Drawing.Size(190, 23);
            this.labelDevices.TabIndex = 10;
            this.labelDevices.Text = "Default output device";
            // 
            // comboBoxDevices
            // 
            this.comboBoxDevices.BackColor = System.Drawing.Color.DimGray;
            this.comboBoxDevices.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxDevices.ForeColor = System.Drawing.Color.White;
            this.comboBoxDevices.FormattingEnabled = true;
            this.comboBoxDevices.Location = new System.Drawing.Point(44, 217);
            this.comboBoxDevices.Name = "comboBoxDevices";
            this.comboBoxDevices.Size = new System.Drawing.Size(380, 24);
            this.comboBoxDevices.TabIndex = 9;
            this.comboBoxDevices.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDevices_SelectedIndexChanged);
            this.comboBoxDevices.EnabledChanged += new System.EventHandler(this.ComboBoxDevices_EnabledChanged);
            // 
            // checkBoxOverrideDevice
            // 
            this.checkBoxOverrideDevice.AutoSize = true;
            this.checkBoxOverrideDevice.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxOverrideDevice.Checked = true;
            this.checkBoxOverrideDevice.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOverrideDevice.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxOverrideDevice.Location = new System.Drawing.Point(44, 189);
            this.checkBoxOverrideDevice.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.checkBoxOverrideDevice.Name = "checkBoxOverrideDevice";
            this.checkBoxOverrideDevice.Size = new System.Drawing.Size(198, 20);
            this.checkBoxOverrideDevice.TabIndex = 11;
            this.checkBoxOverrideDevice.Text = "Override default output device";
            this.checkBoxOverrideDevice.UseVisualStyleBackColor = true;
            this.checkBoxOverrideDevice.CheckedChanged += new System.EventHandler(this.CheckBoxOverrideDevice_CheckedChanged);
            // 
            // SoundboardActionConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxOverrideDevice);
            this.Controls.Add(this.labelDevices);
            this.Controls.Add(this.comboBoxDevices);
            this.Controls.Add(this.volumeNum);
            this.Controls.Add(this.labelVolume);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.labelOr);
            this.Controls.Add(this.volumeBar);
            this.Controls.Add(this.buttonGetFromURL);
            this.Controls.Add(this.fileBrowse);
            this.Controls.Add(this.filePath);
            this.Name = "SoundboardActionConfigView";
            this.Load += new System.EventHandler(this.SoundboardActionConfigView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox filePath;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary fileBrowse;
        private ButtonPrimary buttonGetFromURL;
        private TrackBar volumeBar;
        private Label labelOr;
        private Label labelFile;
        private Label labelVolume;
        private NumericUpDown volumeNum;
        private Label labelDevices;
        private System.Windows.Forms.ComboBox comboBoxDevices;
        private CheckBox checkBoxOverrideDevice;
    }
}