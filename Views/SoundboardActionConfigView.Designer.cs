
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
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.labelOr = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // filePath
            // 
            this.filePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.filePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.filePath.BackColor = System.Drawing.Color.DimGray;
            this.filePath.ForeColor = System.Drawing.Color.White;
            this.filePath.Location = new System.Drawing.Point(13, 40);
            this.filePath.Margin = new System.Windows.Forms.Padding(0);
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(380, 30);
            this.filePath.TabIndex = 0;
            this.filePath.TextChanged += new System.EventHandler(this.FilePath_TextChanged);
            // 
            // fileBrowse
            // 
            this.fileBrowse.BackColor = System.Drawing.Color.DodgerBlue;
            this.fileBrowse.BorderRadius = 8;
            this.fileBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fileBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fileBrowse.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fileBrowse.ForeColor = System.Drawing.Color.White;
            this.fileBrowse.Location = new System.Drawing.Point(396, 40);
            this.fileBrowse.Name = "fileBrowse";
            this.fileBrowse.Size = new System.Drawing.Size(75, 30);
            this.fileBrowse.TabIndex = 1;
            this.fileBrowse.Text = "Browse";
            this.fileBrowse.UseVisualStyleBackColor = true;
            this.fileBrowse.Click += new System.EventHandler(this.FileBrowse_Click);
            // 
            // buttonGetFromURL
            // 
            this.buttonGetFromURL.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonGetFromURL.BorderRadius = 8;
            this.buttonGetFromURL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGetFromURL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGetFromURL.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonGetFromURL.ForeColor = System.Drawing.Color.White;
            this.buttonGetFromURL.Location = new System.Drawing.Point(541, 40);
            this.buttonGetFromURL.Name = "buttonGetFromURL";
            this.buttonGetFromURL.Size = new System.Drawing.Size(95, 30);
            this.buttonGetFromURL.TabIndex = 3;
            this.buttonGetFromURL.Text = "Get from URL";
            this.buttonGetFromURL.UseVisualStyleBackColor = true;
            this.buttonGetFromURL.Click += new System.EventHandler(this.ButtonGetFromURL_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(13, 100);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(285, 45);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // labelOr
            // 
            this.labelOr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelOr.Location = new System.Drawing.Point(477, 40);
            this.labelOr.Name = "labelOr";
            this.labelOr.Size = new System.Drawing.Size(58, 30);
            this.labelOr.TabIndex = 5;
            this.labelOr.Text = "or";
            this.labelOr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SoundboardActionConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelOr);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.buttonGetFromURL);
            this.Controls.Add(this.fileBrowse);
            this.Controls.Add(this.filePath);
            this.Margin = new System.Windows.Forms.Padding(10);
            this.Name = "SoundboardActionConfigView";
            this.Padding = new System.Windows.Forms.Padding(10);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox filePath;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary fileBrowse;
        private ButtonPrimary buttonGetFromURL;
        private TrackBar trackBar1;
        private Label labelOr;
    }
}