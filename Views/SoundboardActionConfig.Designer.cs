
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Windows.Forms;

namespace MacroDeckSoundboard.Views
{
    partial class SoundboardActionConfig : ActionConfigControl
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
            this.fileProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // filePath
            // 
            this.filePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.filePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.filePath.BackColor = System.Drawing.Color.DimGray;
            this.filePath.ForeColor = System.Drawing.Color.White;
            this.filePath.Location = new System.Drawing.Point(10, 40);
            this.filePath.Margin = new System.Windows.Forms.Padding(0);
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(380, 30);
            this.filePath.TabIndex = 0;
            // 
            // fileBrowse
            // 
            this.fileBrowse.BackColor = System.Drawing.Color.DodgerBlue;
            this.fileBrowse.BorderRadius = 8;
            this.fileBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fileBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fileBrowse.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fileBrowse.ForeColor = System.Drawing.Color.White;
            this.fileBrowse.Location = new System.Drawing.Point(393, 40);
            this.fileBrowse.Name = "fileBrowse";
            this.fileBrowse.Size = new System.Drawing.Size(75, 30);
            this.fileBrowse.TabIndex = 1;
            this.fileBrowse.Text = "Browse";
            this.fileBrowse.UseVisualStyleBackColor = true;
            // 
            // fileProgressBar
            // 
            this.fileProgressBar.Location = new System.Drawing.Point(10, 70);
            this.fileProgressBar.Margin = new System.Windows.Forms.Padding(0);
            this.fileProgressBar.Name = "fileProgressBar";
            this.fileProgressBar.Size = new System.Drawing.Size(380, 3);
            this.fileProgressBar.TabIndex = 2;
            this.fileProgressBar.Visible = false;
            // 
            // SoundboardActionConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fileProgressBar);
            this.Controls.Add(this.fileBrowse);
            this.Controls.Add(this.filePath);
            this.Margin = new System.Windows.Forms.Padding(10);
            this.Name = "SoundboardActionConfig";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void FileBrowse_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath.Text = openFileDialog.FileName;
            }
        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox filePath;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary fileBrowse;
        private ProgressBar fileProgressBar;
    }
}