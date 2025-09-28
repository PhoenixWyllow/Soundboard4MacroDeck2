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
            comboBoxCategory = new RoundedComboBox();
            labelCategory = new Label();
            checkBoxEnsureUniqueRandom = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)volumeBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)volumeNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)buttonAddAudio).BeginInit();
            SuspendLayout();
            // 
            // volumeBar
            // 
            volumeBar.Location = new Point(49, 198);
            volumeBar.Margin = new Padding(2);
            volumeBar.Maximum = 100;
            volumeBar.Name = "volumeBar";
            volumeBar.Size = new Size(510, 45);
            volumeBar.TabIndex = 4;
            volumeBar.TickFrequency = 10;
            volumeBar.TickStyle = TickStyle.Both;
            volumeBar.ValueChanged += VolumeBar_ValueChanged;
            // 
            // labelFile
            // 
            labelFile.AutoSize = true;
            labelFile.Font = new Font("Tahoma", 11.25F);
            labelFile.Location = new Point(29, 35);
            labelFile.Margin = new Padding(2, 0, 2, 0);
            labelFile.Name = "labelFile";
            labelFile.Size = new Size(28, 18);
            labelFile.TabIndex = 6;
            labelFile.Text = "File";
            labelFile.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelVolume
            // 
            labelVolume.AutoSize = true;
            labelVolume.Font = new Font("Tahoma", 11.25F);
            labelVolume.Location = new Point(29, 169);
            labelVolume.Margin = new Padding(2, 0, 2, 0);
            labelVolume.Name = "labelVolume";
            labelVolume.Size = new Size(56, 18);
            labelVolume.TabIndex = 7;
            labelVolume.Text = "Volume";
            labelVolume.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // volumeNum
            // 
            volumeNum.BackColor = Color.DimGray;
            volumeNum.Font = new Font("Tahoma", 9.75F);
            volumeNum.ForeColor = Color.White;
            volumeNum.Location = new Point(625, 209);
            volumeNum.Margin = new Padding(2);
            volumeNum.Name = "volumeNum";
            volumeNum.Size = new Size(56, 23);
            volumeNum.TabIndex = 8;
            volumeNum.TextAlign = HorizontalAlignment.Center;
            volumeNum.ValueChanged += VolumeNum_ValueChanged;
            // 
            // labelDevices
            // 
            labelDevices.AutoSize = true;
            labelDevices.Font = new Font("Tahoma", 11.25F);
            labelDevices.Location = new Point(29, 269);
            labelDevices.Margin = new Padding(2, 0, 2, 0);
            labelDevices.Name = "labelDevices";
            labelDevices.Size = new Size(147, 18);
            labelDevices.TabIndex = 10;
            labelDevices.Text = "Default output device";
            labelDevices.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // comboBoxDevices
            // 
            comboBoxDevices.BackColor = Color.DimGray;
            comboBoxDevices.Cursor = Cursors.Hand;
            comboBoxDevices.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDevices.Font = new Font("Tahoma", 9.75F);
            comboBoxDevices.ForeColor = Color.White;
            comboBoxDevices.Icon = null;
            comboBoxDevices.Location = new Point(49, 343);
            comboBoxDevices.Margin = new Padding(2);
            comboBoxDevices.Name = "comboBoxDevices";
            comboBoxDevices.Padding = new Padding(5, 1, 5, 1);
            comboBoxDevices.SelectedIndex = -1;
            comboBoxDevices.SelectedItem = null;
            comboBoxDevices.Size = new Size(632, 26);
            comboBoxDevices.TabIndex = 9;
            comboBoxDevices.SelectedIndexChanged += ComboBoxDevices_SelectedIndexChanged;
            // 
            // checkBoxOverrideDevice
            // 
            checkBoxOverrideDevice.AutoSize = true;
            checkBoxOverrideDevice.CheckAlign = ContentAlignment.MiddleRight;
            checkBoxOverrideDevice.Checked = true;
            checkBoxOverrideDevice.CheckState = CheckState.Checked;
            checkBoxOverrideDevice.Font = new Font("Tahoma", 9.75F);
            checkBoxOverrideDevice.Location = new Point(43, 308);
            checkBoxOverrideDevice.Margin = new Padding(2);
            checkBoxOverrideDevice.Name = "checkBoxOverrideDevice";
            checkBoxOverrideDevice.Size = new Size(198, 20);
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
            checkBoxSyncButtonState.Font = new Font("Tahoma", 11.25F);
            checkBoxSyncButtonState.Location = new Point(29, 114);
            checkBoxSyncButtonState.Margin = new Padding(2);
            checkBoxSyncButtonState.Name = "checkBoxSyncButtonState";
            checkBoxSyncButtonState.Size = new Size(212, 22);
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
            comboBoxAudio.Font = new Font("Tahoma", 9.75F);
            comboBoxAudio.ForeColor = Color.White;
            comboBoxAudio.Icon = null;
            comboBoxAudio.Location = new Point(49, 63);
            comboBoxAudio.Margin = new Padding(2);
            comboBoxAudio.Name = "comboBoxAudio";
            comboBoxAudio.Padding = new Padding(5, 1, 5, 1);
            comboBoxAudio.SelectedIndex = -1;
            comboBoxAudio.SelectedItem = null;
            comboBoxAudio.Size = new Size(632, 26);
            comboBoxAudio.TabIndex = 13;
            comboBoxAudio.SelectedIndexChanged += ComboBoxAudio_SelectedIndexChanged;
            // 
            // buttonAddAudio
            // 
            buttonAddAudio.BackColor = Color.Transparent;
            buttonAddAudio.BackgroundImage = (Image)resources.GetObject("buttonAddAudio.BackgroundImage");
            buttonAddAudio.BackgroundImageLayout = ImageLayout.Stretch;
            buttonAddAudio.Font = new Font("Tahoma", 9.75F);
            buttonAddAudio.ForeColor = Color.White;
            buttonAddAudio.HoverImage = (Image)resources.GetObject("buttonAddAudio.HoverImage");
            buttonAddAudio.Location = new Point(717, 65);
            buttonAddAudio.Name = "buttonAddAudio";
            buttonAddAudio.Size = new Size(25, 25);
            buttonAddAudio.TabIndex = 14;
            buttonAddAudio.TabStop = false;
            buttonAddAudio.Click += ButtonAddAudio_Click;
            // 
            // comboBoxCategory
            // 
            comboBoxCategory.BackColor = Color.DimGray;
            comboBoxCategory.Cursor = Cursors.Hand;
            comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCategory.Font = new Font("Tahoma", 9.75F);
            comboBoxCategory.ForeColor = Color.White;
            comboBoxCategory.Icon = null;
            comboBoxCategory.Location = new Point(47, 64);
            comboBoxCategory.Margin = new Padding(2);
            comboBoxCategory.Name = "comboBoxCategory";
            comboBoxCategory.Padding = new Padding(5, 1, 5, 1);
            comboBoxCategory.SelectedIndex = -1;
            comboBoxCategory.SelectedItem = null;
            comboBoxCategory.Size = new Size(632, 26);
            comboBoxCategory.TabIndex = 16;
            comboBoxCategory.Visible = false;
            comboBoxCategory.SelectedIndexChanged += ComboBoxCategory_SelectedIndexChanged;
            // 
            // labelCategory
            // 
            labelCategory.AutoSize = true;
            labelCategory.Font = new Font("Tahoma", 11.25F);
            labelCategory.Location = new Point(26, 36);
            labelCategory.Margin = new Padding(2, 0, 2, 0);
            labelCategory.Name = "labelCategory";
            labelCategory.Size = new Size(67, 18);
            labelCategory.TabIndex = 15;
            labelCategory.Text = "Category";
            labelCategory.TextAlign = ContentAlignment.MiddleLeft;
            labelCategory.Visible = false;
            // 
            // checkBoxEnsureUniqueRandom
            // 
            checkBoxEnsureUniqueRandom.AutoSize = true;
            checkBoxEnsureUniqueRandom.CheckAlign = ContentAlignment.MiddleRight;
            checkBoxEnsureUniqueRandom.Checked = true;
            checkBoxEnsureUniqueRandom.CheckState = CheckState.Checked;
            checkBoxEnsureUniqueRandom.Font = new Font("Tahoma", 11.25F);
            checkBoxEnsureUniqueRandom.Location = new Point(483, 114);
            checkBoxEnsureUniqueRandom.Margin = new Padding(1);
            checkBoxEnsureUniqueRandom.Name = "checkBoxEnsureUniqueRandom";
            checkBoxEnsureUniqueRandom.Size = new Size(198, 22);
            checkBoxEnsureUniqueRandom.TabIndex = 17;
            checkBoxEnsureUniqueRandom.Text = "Use unique random sound";
            checkBoxEnsureUniqueRandom.UseVisualStyleBackColor = true;
            checkBoxEnsureUniqueRandom.Visible = false;
            checkBoxEnsureUniqueRandom.CheckedChanged += UniqueRandomSoundsCheckbox_CheckedChanged;
            // 
            // SoundboardActionConfigView
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(checkBoxEnsureUniqueRandom);
            Controls.Add(comboBoxCategory);
            Controls.Add(labelCategory);
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
            Margin = new Padding(39, 21, 39, 21);
            Name = "SoundboardActionConfigView";
            Size = new Size(800, 600);
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
        private RoundedComboBox comboBoxCategory;
        private Label labelCategory;
        private CheckBox checkBoxEnsureUniqueRandom;
    }
}