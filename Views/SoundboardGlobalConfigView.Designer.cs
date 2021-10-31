
namespace Soundboard4MacroDeck.Views
{
    partial class SoundboardGlobalConfigView
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
            this.comboBoxDevices = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.buttonOK = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.labelDevices = new System.Windows.Forms.Label();
            this.linkLabelResetDevice = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // comboBoxDevices
            // 
            this.comboBoxDevices.BackColor = System.Drawing.Color.DimGray;
            this.comboBoxDevices.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDevices.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxDevices.ForeColor = System.Drawing.Color.White;
            this.comboBoxDevices.Icon = null;
            this.comboBoxDevices.Location = new System.Drawing.Point(24, 46);
            this.comboBoxDevices.Name = "comboBoxDevices";
            this.comboBoxDevices.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.comboBoxDevices.SelectedIndex = -1;
            this.comboBoxDevices.SelectedItem = null;
            this.comboBoxDevices.Size = new System.Drawing.Size(404, 26);
            this.comboBoxDevices.TabIndex = 3;
            this.comboBoxDevices.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDevices_SelectedIndexChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.buttonOK.BorderRadius = 8;
            this.buttonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonOK.ForeColor = System.Drawing.Color.White;
            this.buttonOK.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.buttonOK.Icon = null;
            this.buttonOK.Location = new System.Drawing.Point(352, 188);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Progress = 0;
            this.buttonOK.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.buttonOK.Size = new System.Drawing.Size(75, 27);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // labelDevices
            // 
            this.labelDevices.AutoSize = true;
            this.labelDevices.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelDevices.Location = new System.Drawing.Point(24, 24);
            this.labelDevices.Name = "labelDevices";
            this.labelDevices.Size = new System.Drawing.Size(159, 19);
            this.labelDevices.TabIndex = 5;
            this.labelDevices.Text = "Default output device";
            // 
            // linkLabelResetDevice
            // 
            this.linkLabelResetDevice.AutoSize = true;
            this.linkLabelResetDevice.LinkColor = System.Drawing.Color.DodgerBlue;
            this.linkLabelResetDevice.Location = new System.Drawing.Point(272, 77);
            this.linkLabelResetDevice.Name = "linkLabelResetDevice";
            this.linkLabelResetDevice.Size = new System.Drawing.Size(155, 16);
            this.linkLabelResetDevice.TabIndex = 6;
            this.linkLabelResetDevice.TabStop = true;
            this.linkLabelResetDevice.Text = "Use system default device";
            this.linkLabelResetDevice.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelResetDevice_LinkClicked);
            // 
            // SoundboardGlobalConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 236);
            this.Controls.Add(this.linkLabelResetDevice);
            this.Controls.Add(this.labelDevices);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxDevices);
            this.Name = "SoundboardGlobalConfigView";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Text = "GlobalConfig";
            this.Load += new System.EventHandler(this.SoundboardGlobalConfigView_Load);
            this.Controls.SetChildIndex(this.comboBoxDevices, 0);
            this.Controls.SetChildIndex(this.buttonOK, 0);
            this.Controls.SetChildIndex(this.labelDevices, 0);
            this.Controls.SetChildIndex(this.linkLabelResetDevice, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox comboBoxDevices;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary buttonOK;
        private System.Windows.Forms.Label labelDevices;
        private System.Windows.Forms.LinkLabel linkLabelResetDevice;
    }
}