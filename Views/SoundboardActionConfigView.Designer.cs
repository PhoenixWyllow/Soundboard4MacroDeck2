using SuchByte.MacroDeck.GUI.CustomControls;
using System;
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
            volumeBar = new TrackBar();
            labelFile = new Label();
            labelVolume = new Label();
            volumeNum = new NumericUpDown();
            labelDevices = new Label();
            comboBoxDevices = new RoundedComboBox();
            checkBoxOverrideDevice = new CheckBox();
            checkBoxSyncButtonState = new CheckBox();
            comboBoxAudio = new RoundedComboBox();
            ((System.ComponentModel.ISupportInitialize)volumeBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)volumeNum).BeginInit();
            SuspendLayout();
            // 
            // volumeBar
            // 
            volumeBar.Location = new System.Drawing.Point(74, 297);
            volumeBar.Maximum = 100;
            volumeBar.Name = "volumeBar";
            volumeBar.Size = new System.Drawing.Size(510, 69);
            volumeBar.TabIndex = 4;
            volumeBar.TickFrequency = 10;
            volumeBar.TickStyle = TickStyle.Both;
            volumeBar.ValueChanged += VolumeBar_ValueChanged;
            // 
            // labelFile
            // 
            labelFile.AutoSize = true;
            labelFile.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelFile.Location = new System.Drawing.Point(43, 53);
            labelFile.Name = "labelFile";
            labelFile.Size = new System.Drawing.Size(99, 28);
            labelFile.TabIndex = 6;
            labelFile.Text = "File path";
            labelFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVolume
            // 
            labelVolume.AutoSize = true;
            labelVolume.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelVolume.Location = new System.Drawing.Point(43, 253);
            labelVolume.Name = "labelVolume";
            labelVolume.Size = new System.Drawing.Size(87, 28);
            labelVolume.TabIndex = 7;
            labelVolume.Text = "Volume";
            labelVolume.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // volumeNum
            // 
            volumeNum.BackColor = System.Drawing.Color.DimGray;
            volumeNum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            volumeNum.ForeColor = System.Drawing.Color.White;
            volumeNum.Location = new System.Drawing.Point(624, 316);
            volumeNum.Name = "volumeNum";
            volumeNum.Size = new System.Drawing.Size(91, 31);
            volumeNum.TabIndex = 8;
            volumeNum.TextAlign = HorizontalAlignment.Center;
            volumeNum.ValueChanged += VolumeNum_ValueChanged;
            // 
            // labelDevices
            // 
            labelDevices.AutoSize = true;
            labelDevices.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelDevices.Location = new System.Drawing.Point(43, 403);
            labelDevices.Name = "labelDevices";
            labelDevices.Size = new System.Drawing.Size(230, 28);
            labelDevices.TabIndex = 10;
            labelDevices.Text = "Default output device";
            labelDevices.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxDevices
            // 
            comboBoxDevices.BackColor = System.Drawing.Color.DimGray;
            comboBoxDevices.Cursor = Cursors.Hand;
            comboBoxDevices.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDevices.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBoxDevices.ForeColor = System.Drawing.Color.White;
            comboBoxDevices.Icon = null;
            comboBoxDevices.Location = new System.Drawing.Point(74, 514);
            comboBoxDevices.Name = "comboBoxDevices";
            comboBoxDevices.Padding = new Padding(8, 2, 8, 2);
            comboBoxDevices.SelectedIndex = -1;
            comboBoxDevices.SelectedItem = null;
            comboBoxDevices.Size = new System.Drawing.Size(632, 36);
            comboBoxDevices.TabIndex = 9;
            comboBoxDevices.SelectedIndexChanged += ComboBoxDevices_SelectedIndexChanged;
            // 
            // checkBoxOverrideDevice
            // 
            checkBoxOverrideDevice.AutoSize = true;
            checkBoxOverrideDevice.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            checkBoxOverrideDevice.Checked = true;
            checkBoxOverrideDevice.CheckState = CheckState.Checked;
            checkBoxOverrideDevice.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxOverrideDevice.Location = new System.Drawing.Point(65, 462);
            checkBoxOverrideDevice.Name = "checkBoxOverrideDevice";
            checkBoxOverrideDevice.Size = new System.Drawing.Size(307, 28);
            checkBoxOverrideDevice.TabIndex = 11;
            checkBoxOverrideDevice.Text = "Override default output device";
            checkBoxOverrideDevice.UseVisualStyleBackColor = true;
            checkBoxOverrideDevice.CheckedChanged += CheckBoxOverrideDevice_CheckedChanged;
            // 
            // checkBoxSyncButtonState
            // 
            checkBoxSyncButtonState.AutoSize = true;
            checkBoxSyncButtonState.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            checkBoxSyncButtonState.Checked = true;
            checkBoxSyncButtonState.CheckState = CheckState.Checked;
            checkBoxSyncButtonState.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxSyncButtonState.Location = new System.Drawing.Point(43, 171);
            checkBoxSyncButtonState.Name = "checkBoxSyncButtonState";
            checkBoxSyncButtonState.Size = new System.Drawing.Size(329, 32);
            checkBoxSyncButtonState.TabIndex = 12;
            checkBoxSyncButtonState.Text = "Sync button state with audio";
            checkBoxSyncButtonState.UseVisualStyleBackColor = true;
            checkBoxSyncButtonState.CheckedChanged += CheckBoxSyncButtonState_CheckedChanged;
            // 
            // comboBoxAudio
            // 
            comboBoxAudio.BackColor = System.Drawing.Color.DimGray;
            comboBoxAudio.Cursor = Cursors.Hand;
            comboBoxAudio.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAudio.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBoxAudio.ForeColor = System.Drawing.Color.White;
            comboBoxAudio.Icon = null;
            comboBoxAudio.Location = new System.Drawing.Point(74, 95);
            comboBoxAudio.Name = "comboBoxAudio";
            comboBoxAudio.Padding = new Padding(8, 2, 8, 2);
            comboBoxAudio.SelectedIndex = -1;
            comboBoxAudio.SelectedItem = null;
            comboBoxAudio.Size = new System.Drawing.Size(632, 36);
            comboBoxAudio.TabIndex = 13;
            comboBoxAudio.SelectedIndexChanged += ComboBoxAudio_SelectedIndexChanged;
            // 
            // SoundboardActionConfigView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
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
            Size = new System.Drawing.Size(804, 639);
            Load += SoundboardActionConfigView_Load;
            ((System.ComponentModel.ISupportInitialize)volumeBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)volumeNum).EndInit();
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
    }
}