
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
            this.filePath = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.fileBrowse = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.buttonGetFromURL = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.volumeBar = new System.Windows.Forms.TrackBar();
            this.labelOr = new System.Windows.Forms.Label();
            this.labelFile = new System.Windows.Forms.Label();
            this.labelVolume = new System.Windows.Forms.Label();
            this.volumeNum = new System.Windows.Forms.NumericUpDown();
            this.labelDevices = new System.Windows.Forms.Label();
            this.comboBoxDevices = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.checkBoxOverrideDevice = new System.Windows.Forms.CheckBox();
            this.checkBoxSyncButtonState = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeNum)).BeginInit();
            this.SuspendLayout();
            // 
            // filePath
            // 
            this.filePath.BackColor = System.Drawing.Color.DimGray;
            this.filePath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.filePath.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.filePath.ForeColor = System.Drawing.Color.White;
            this.filePath.Icon = null;
            this.filePath.Location = new System.Drawing.Point(43, 43);
            this.filePath.MaxCharacters = 32767;
            this.filePath.Multiline = false;
            this.filePath.Name = "filePath";
            this.filePath.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.filePath.PasswordChar = false;
            this.filePath.PlaceHolderColor = System.Drawing.Color.Gray;
            this.filePath.PlaceHolderText = "Get file locally";
            this.filePath.ReadOnly = false;
            this.filePath.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.filePath.SelectionStart = 0;
            this.filePath.Size = new System.Drawing.Size(356, 27);
            this.filePath.TabIndex = 0;
            this.filePath.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.fileBrowse.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.fileBrowse.Icon = null;
            this.fileBrowse.Location = new System.Drawing.Point(405, 42);
            this.fileBrowse.Name = "fileBrowse";
            this.fileBrowse.Progress = 0;
            this.fileBrowse.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.fileBrowse.Size = new System.Drawing.Size(75, 28);
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
            this.buttonGetFromURL.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.buttonGetFromURL.Icon = null;
            this.buttonGetFromURL.Location = new System.Drawing.Point(550, 42);
            this.buttonGetFromURL.Name = "buttonGetFromURL";
            this.buttonGetFromURL.Progress = 0;
            this.buttonGetFromURL.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.buttonGetFromURL.Size = new System.Drawing.Size(95, 28);
            this.buttonGetFromURL.TabIndex = 3;
            this.buttonGetFromURL.Text = "Get from URL";
            this.buttonGetFromURL.UseVisualStyleBackColor = true;
            this.buttonGetFromURL.Click += new System.EventHandler(this.ButtonGetFromURL_Click);
            // 
            // volumeBar
            // 
            this.volumeBar.Location = new System.Drawing.Point(83, 140);
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
            this.labelOr.Location = new System.Drawing.Point(486, 43);
            this.labelOr.Name = "labelOr";
            this.labelOr.Size = new System.Drawing.Size(58, 27);
            this.labelOr.TabIndex = 5;
            this.labelOr.Text = "or";
            this.labelOr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelFile
            // 
            this.labelFile.AutoSize = true;
            this.labelFile.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelFile.Location = new System.Drawing.Point(13, 17);
            this.labelFile.Margin = new System.Windows.Forms.Padding(5);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(62, 18);
            this.labelFile.TabIndex = 6;
            this.labelFile.Text = "File path";
            this.labelFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVolume
            // 
            this.labelVolume.AutoSize = true;
            this.labelVolume.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelVolume.Location = new System.Drawing.Point(13, 153);
            this.labelVolume.Margin = new System.Windows.Forms.Padding(5);
            this.labelVolume.Name = "labelVolume";
            this.labelVolume.Size = new System.Drawing.Size(56, 18);
            this.labelVolume.TabIndex = 7;
            this.labelVolume.Text = "Volume";
            this.labelVolume.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // volumeNum
            // 
            this.volumeNum.BackColor = System.Drawing.Color.DimGray;
            this.volumeNum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.volumeNum.ForeColor = System.Drawing.Color.White;
            this.volumeNum.Location = new System.Drawing.Point(405, 151);
            this.volumeNum.Name = "volumeNum";
            this.volumeNum.Size = new System.Drawing.Size(60, 23);
            this.volumeNum.TabIndex = 8;
            this.volumeNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.volumeNum.ValueChanged += new System.EventHandler(this.VolumeNum_ValueChanged);
            // 
            // labelDevices
            // 
            this.labelDevices.AutoSize = true;
            this.labelDevices.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelDevices.Location = new System.Drawing.Point(15, 207);
            this.labelDevices.Margin = new System.Windows.Forms.Padding(5);
            this.labelDevices.Name = "labelDevices";
            this.labelDevices.Size = new System.Drawing.Size(147, 18);
            this.labelDevices.TabIndex = 10;
            this.labelDevices.Text = "Default output device";
            this.labelDevices.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxDevices
            // 
            this.comboBoxDevices.BackColor = System.Drawing.Color.DimGray;
            this.comboBoxDevices.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDevices.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxDevices.ForeColor = System.Drawing.Color.White;
            this.comboBoxDevices.Icon = null;
            this.comboBoxDevices.Location = new System.Drawing.Point(265, 229);
            this.comboBoxDevices.Name = "comboBoxDevices";
            this.comboBoxDevices.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.comboBoxDevices.SelectedIndex = -1;
            this.comboBoxDevices.SelectedItem = null;
            this.comboBoxDevices.Size = new System.Drawing.Size(380, 28);
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
            this.checkBoxOverrideDevice.Location = new System.Drawing.Point(43, 233);
            this.checkBoxOverrideDevice.Name = "checkBoxOverrideDevice";
            this.checkBoxOverrideDevice.Size = new System.Drawing.Size(198, 20);
            this.checkBoxOverrideDevice.TabIndex = 11;
            this.checkBoxOverrideDevice.Text = "Override default output device";
            this.checkBoxOverrideDevice.UseVisualStyleBackColor = true;
            this.checkBoxOverrideDevice.CheckedChanged += new System.EventHandler(this.CheckBoxOverrideDevice_CheckedChanged);
            // 
            // checkBoxSyncButtonState
            // 
            this.checkBoxSyncButtonState.AutoSize = true;
            this.checkBoxSyncButtonState.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxSyncButtonState.Checked = true;
            this.checkBoxSyncButtonState.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSyncButtonState.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxSyncButtonState.Location = new System.Drawing.Point(13, 95);
            this.checkBoxSyncButtonState.Name = "checkBoxSyncButtonState";
            this.checkBoxSyncButtonState.Size = new System.Drawing.Size(212, 22);
            this.checkBoxSyncButtonState.TabIndex = 12;
            this.checkBoxSyncButtonState.Text = "Sync button state with audio";
            this.checkBoxSyncButtonState.UseVisualStyleBackColor = true;
            this.checkBoxSyncButtonState.CheckedChanged += new System.EventHandler(this.CheckBoxSyncButtonState_CheckedChanged);
            // 
            // SoundboardActionConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxSyncButtonState);
            this.Controls.Add(this.buttonGetFromURL);
            this.Controls.Add(this.fileBrowse);
            this.Controls.Add(this.checkBoxOverrideDevice);
            this.Controls.Add(this.labelDevices);
            this.Controls.Add(this.comboBoxDevices);
            this.Controls.Add(this.volumeNum);
            this.Controls.Add(this.labelVolume);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.labelOr);
            this.Controls.Add(this.volumeBar);
            this.Controls.Add(this.filePath);
            this.Name = "SoundboardActionConfigView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Load += new System.EventHandler(this.SoundboardActionConfigView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private RoundedTextBox filePath;
        private ButtonPrimary fileBrowse;
        private ButtonPrimary buttonGetFromURL;
        private TrackBar volumeBar;
        private Label labelOr;
        private Label labelFile;
        private Label labelVolume;
        private NumericUpDown volumeNum;
        private Label labelDevices;
        private RoundedComboBox comboBoxDevices;
        private CheckBox checkBoxOverrideDevice;
        private CheckBox checkBoxSyncButtonState;
    }
}