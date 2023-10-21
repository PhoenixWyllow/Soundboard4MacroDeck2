using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Soundboard4MacroDeck.Views
{
    partial class SoundboardActionConfigView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoundboardActionConfigView));
            volumeBar = new TrackBar();
            labelFile = new Label();
            labelVolume = new Label();
            volumeNum = new NumericUpDown();
            labelDevices = new Label();
            comboBoxDevices = new RoundedComboBox();
            checkBoxOverrideDevice = new CheckBox();
            checkBoxSyncButtonState = new CheckBox();
            comboBoxAudio = new RoundedComboBox();
            buttonAddAudio = new PictureButton();
            ((System.ComponentModel.ISupportInitialize)volumeBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)volumeNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)buttonAddAudio).BeginInit();
            SuspendLayout();
            // 
            // volumeBar
            // 
            volumeBar.Location = new Point(74, 297);
            volumeBar.Maximum = 100;
            volumeBar.Name = "volumeBar";
            volumeBar.Size = new Size(510, 69);
            volumeBar.TabIndex = 4;
            volumeBar.TickFrequency = 10;
            volumeBar.TickStyle = TickStyle.Both;
            volumeBar.ValueChanged += VolumeBar_ValueChanged;
            // 
            // labelFile
            // 
            labelFile.AutoSize = true;
            labelFile.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelFile.Location = new Point(43, 53);
            labelFile.Name = "labelFile";
            labelFile.Size = new Size(46, 28);
            labelFile.TabIndex = 6;
            labelFile.Text = "File";
            labelFile.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelVolume
            // 
            labelVolume.AutoSize = true;
            labelVolume.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelVolume.Location = new Point(43, 253);
            labelVolume.Name = "labelVolume";
            labelVolume.Size = new Size(87, 28);
            labelVolume.TabIndex = 7;
            labelVolume.Text = "Volume";
            labelVolume.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // volumeNum
            // 
            volumeNum.BackColor = Color.DimGray;
            volumeNum.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            volumeNum.ForeColor = Color.White;
            volumeNum.Location = new Point(624, 316);
            volumeNum.Name = "volumeNum";
            volumeNum.Size = new Size(91, 31);
            volumeNum.TabIndex = 8;
            volumeNum.TextAlign = HorizontalAlignment.Center;
            volumeNum.ValueChanged += VolumeNum_ValueChanged;
            // 
            // labelDevices
            // 
            labelDevices.AutoSize = true;
            labelDevices.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelDevices.Location = new Point(43, 403);
            labelDevices.Name = "labelDevices";
            labelDevices.Size = new Size(230, 28);
            labelDevices.TabIndex = 10;
            labelDevices.Text = "Default output device";
            labelDevices.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // comboBoxDevices
            // 
            comboBoxDevices.BackColor = Color.DimGray;
            comboBoxDevices.Cursor = Cursors.Hand;
            comboBoxDevices.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDevices.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboBoxDevices.ForeColor = Color.White;
            comboBoxDevices.Icon = null;
            comboBoxDevices.Location = new Point(74, 514);
            comboBoxDevices.Name = "comboBoxDevices";
            comboBoxDevices.Padding = new Padding(8, 2, 8, 2);
            comboBoxDevices.SelectedIndex = -1;
            comboBoxDevices.SelectedItem = null;
            comboBoxDevices.Size = new Size(632, 36);
            comboBoxDevices.TabIndex = 9;
            comboBoxDevices.SelectedIndexChanged += ComboBoxDevices_SelectedIndexChanged;
            // 
            // checkBoxOverrideDevice
            // 
            checkBoxOverrideDevice.AutoSize = true;
            checkBoxOverrideDevice.CheckAlign = ContentAlignment.MiddleRight;
            checkBoxOverrideDevice.Checked = true;
            checkBoxOverrideDevice.CheckState = CheckState.Checked;
            checkBoxOverrideDevice.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            checkBoxOverrideDevice.Location = new Point(65, 462);
            checkBoxOverrideDevice.Name = "checkBoxOverrideDevice";
            checkBoxOverrideDevice.Size = new Size(307, 28);
            checkBoxOverrideDevice.TabIndex = 11;
            checkBoxOverrideDevice.Text = "Override default output device";
            checkBoxOverrideDevice.UseVisualStyleBackColor = true;
            checkBoxOverrideDevice.CheckedChanged += CheckBoxOverrideDevice_CheckedChanged;
            // 
            // checkBoxSyncButtonState
            // 
            checkBoxSyncButtonState.AutoSize = true;
            checkBoxSyncButtonState.CheckAlign = ContentAlignment.MiddleRight;
            checkBoxSyncButtonState.Checked = true;
            checkBoxSyncButtonState.CheckState = CheckState.Checked;
            checkBoxSyncButtonState.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            checkBoxSyncButtonState.Location = new Point(43, 171);
            checkBoxSyncButtonState.Name = "checkBoxSyncButtonState";
            checkBoxSyncButtonState.Size = new Size(329, 32);
            checkBoxSyncButtonState.TabIndex = 12;
            checkBoxSyncButtonState.Text = "Sync button state with audio";
            checkBoxSyncButtonState.UseVisualStyleBackColor = true;
            checkBoxSyncButtonState.CheckedChanged += CheckBoxSyncButtonState_CheckedChanged;
            // 
            // comboBoxAudio
            // 
            comboBoxAudio.BackColor = Color.DimGray;
            comboBoxAudio.Cursor = Cursors.Hand;
            comboBoxAudio.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAudio.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboBoxAudio.ForeColor = Color.White;
            comboBoxAudio.Icon = null;
            comboBoxAudio.Location = new Point(74, 95);
            comboBoxAudio.Name = "comboBoxAudio";
            comboBoxAudio.Padding = new Padding(8, 2, 8, 2);
            comboBoxAudio.SelectedIndex = -1;
            comboBoxAudio.SelectedItem = null;
            comboBoxAudio.Size = new Size(632, 36);
            comboBoxAudio.TabIndex = 13;
            comboBoxAudio.SelectedIndexChanged += ComboBoxAudio_SelectedIndexChanged;
            // 
            // buttonAddAudio
            // 
            buttonAddAudio.BackColor = Color.Transparent;
            buttonAddAudio.BackgroundImage = (Image)resources.GetObject("buttonAddAudio.BackgroundImage");
            buttonAddAudio.BackgroundImageLayout = ImageLayout.Stretch;
            buttonAddAudio.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            buttonAddAudio.ForeColor = Color.White;
            buttonAddAudio.HoverImage = (Image)resources.GetObject("buttonAddAudio.HoverImage");
            buttonAddAudio.Location = new Point(729, 101);
            buttonAddAudio.Name = "buttonAddAudio";
            buttonAddAudio.Size = new Size(25, 25);
            buttonAddAudio.TabIndex = 14;
            buttonAddAudio.TabStop = false;
            buttonAddAudio.Click += ButtonAddAudio_Click;
            // 
            // SoundboardActionConfigView
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(buttonAddAudio);
            Controls.Add(comboBoxAudio);
            Controls.Add(checkBoxSyncButtonState);
            Controls.Add(checkBoxOverrideDevice);
            Controls.Add(labelDevices);
            Controls.Add(comboBoxDevices);
            Controls.Add(volumeNum);
            Controls.Add(labelVolume);
            Controls.Add(labelFile);
            Controls.Add(volumeBar);
            Margin = new Padding(58, 31, 58, 31);
            Name = "SoundboardActionConfigView";
            Size = new Size(804, 639);
            Load += SoundboardActionConfigView_Load;
            ((System.ComponentModel.ISupportInitialize)volumeBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)volumeNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)buttonAddAudio).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TrackBar volumeBar;
        private Label labelFile;
        private Label labelVolume;
        private NumericUpDown volumeNum;
        private Label labelDevices;
        private RoundedComboBox comboBoxDevices;
        private CheckBox checkBoxOverrideDevice;
        private CheckBox checkBoxSyncButtonState;
        private RoundedComboBox comboBoxAudio;
        private PictureButton buttonAddAudio;
    }
}