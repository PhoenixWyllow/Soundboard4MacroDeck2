
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
            this.comboBoxDevices = new System.Windows.Forms.ComboBox();
            this.buttonOK = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.labelDevices = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxDevices
            // 
            this.comboBoxDevices.FormattingEnabled = true;
            this.comboBoxDevices.Location = new System.Drawing.Point(23, 43);
            this.comboBoxDevices.Name = "comboBoxDevices";
            this.comboBoxDevices.Size = new System.Drawing.Size(404, 24);
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
            this.buttonOK.Location = new System.Drawing.Point(352, 188);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 25);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // labelDevices
            // 
            this.labelDevices.AutoSize = true;
            this.labelDevices.Location = new System.Drawing.Point(24, 24);
            this.labelDevices.Name = "labelDevices";
            this.labelDevices.Size = new System.Drawing.Size(127, 16);
            this.labelDevices.TabIndex = 5;
            this.labelDevices.Text = "Default output device";
            // 
            // SoundboardGlobalConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 236);
            this.Controls.Add(this.labelDevices);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxDevices);
            this.Name = "SoundboardGlobalConfigView";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Text = "GlobalConfig";
            this.Controls.SetChildIndex(this.comboBoxDevices, 0);
            this.Controls.SetChildIndex(this.buttonOK, 0);
            this.Controls.SetChildIndex(this.labelDevices, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxDevices;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary buttonOK;
        private System.Windows.Forms.Label labelDevices;
    }
}