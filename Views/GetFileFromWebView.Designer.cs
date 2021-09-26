
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;

namespace Soundboard4MacroDeck.Views
{
    partial class GetFileFromWebView
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
            this.buttonOK = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.labelURLFile = new System.Windows.Forms.Label();
            this.urlBox = new System.Windows.Forms.TextBox();
            this.fileProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.buttonOK.BorderRadius = 8;
            this.buttonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonOK.ForeColor = System.Drawing.Color.White;
            this.buttonOK.Location = new System.Drawing.Point(387, 71);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 25);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // labelURLFile
            // 
            this.labelURLFile.AutoSize = true;
            this.labelURLFile.Location = new System.Drawing.Point(14, 14);
            this.labelURLFile.Name = "labelURLFile";
            this.labelURLFile.Size = new System.Drawing.Size(137, 16);
            this.labelURLFile.TabIndex = 3;
            this.labelURLFile.Text = "Direct URL of audio file";
            // 
            // urlBox
            // 
            this.urlBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.urlBox.Location = new System.Drawing.Point(14, 34);
            this.urlBox.Name = "urlBox";
            this.urlBox.PlaceholderText = "URL";
            this.urlBox.Size = new System.Drawing.Size(448, 23);
            this.urlBox.TabIndex = 4;
            this.urlBox.TextChanged += new System.EventHandler(this.UrlBox_TextChanged);
            // 
            // fileProgressBar
            // 
            this.fileProgressBar.Location = new System.Drawing.Point(14, 60);
            this.fileProgressBar.Margin = new System.Windows.Forms.Padding(0);
            this.fileProgressBar.Name = "fileProgressBar";
            this.fileProgressBar.Size = new System.Drawing.Size(448, 5);
            this.fileProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.fileProgressBar.TabIndex = 5;
            this.fileProgressBar.Visible = false;
            // 
            // GetFileFromWebView
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 109);
            this.Controls.Add(this.fileProgressBar);
            this.Controls.Add(this.urlBox);
            this.Controls.Add(this.labelURLFile);
            this.Controls.Add(this.buttonOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GetFileFromWebView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "GetFileFromWebView";
            this.Controls.SetChildIndex(this.buttonOK, 0);
            this.Controls.SetChildIndex(this.labelURLFile, 0);
            this.Controls.SetChildIndex(this.urlBox, 0);
            this.Controls.SetChildIndex(this.fileProgressBar, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ButtonPrimary buttonOK;
        private System.Windows.Forms.Label labelURLFile;
        private System.Windows.Forms.TextBox urlBox;
        private System.Windows.Forms.ProgressBar fileProgressBar;
    }
}