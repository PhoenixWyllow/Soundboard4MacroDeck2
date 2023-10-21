using SuchByte.MacroDeck.GUI.CustomControls;

namespace Soundboard4MacroDeck.Views;

partial class SoundboardGlobalAudioAddView
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
        labelFile = new System.Windows.Forms.Label();
        filePath = new RoundedTextBox();
        buttonGetFromURL = new ButtonPrimary();
        fileBrowse = new ButtonPrimary();
        labelOr = new System.Windows.Forms.Label();
        buttonOK = new ButtonPrimary();
        openFileDialog = new System.Windows.Forms.OpenFileDialog();
        SuspendLayout();
        // 
        // labelFile
        // 
        labelFile.AutoSize = true;
        labelFile.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        labelFile.Location = new System.Drawing.Point(35, 33);
        labelFile.Margin = new System.Windows.Forms.Padding(72, 31, 72, 31);
        labelFile.Name = "labelFile";
        labelFile.Size = new System.Drawing.Size(99, 28);
        labelFile.TabIndex = 7;
        labelFile.Text = "File path";
        labelFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // filePath
        // 
        filePath.BackColor = System.Drawing.Color.DimGray;
        filePath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
        filePath.ForeColor = System.Drawing.Color.White;
        filePath.Icon = null;
        filePath.Location = new System.Drawing.Point(51, 92);
        filePath.Margin = new System.Windows.Forms.Padding(20);
        filePath.MaxCharacters = 32767;
        filePath.Multiline = false;
        filePath.Name = "filePath";
        filePath.Padding = new System.Windows.Forms.Padding(10);
        filePath.PasswordChar = false;
        filePath.PlaceHolderColor = System.Drawing.Color.Gray;
        filePath.PlaceHolderText = "Get file locally";
        filePath.ReadOnly = false;
        filePath.ScrollBars = System.Windows.Forms.ScrollBars.None;
        filePath.SelectionStart = 0;
        filePath.Size = new System.Drawing.Size(600, 43);
        filePath.TabIndex = 8;
        filePath.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
        filePath.TextChanged += FilePath_TextChanged;
        // 
        // buttonGetFromURL
        // 
        buttonGetFromURL.BorderRadius = 8;
        buttonGetFromURL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        buttonGetFromURL.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        buttonGetFromURL.ForeColor = System.Drawing.Color.White;
        buttonGetFromURL.HoverColor = System.Drawing.Color.FromArgb(0, 89, 184);
        buttonGetFromURL.Icon = null;
        buttonGetFromURL.Location = new System.Drawing.Point(611, 179);
        buttonGetFromURL.Margin = new System.Windows.Forms.Padding(20);
        buttonGetFromURL.Name = "buttonGetFromURL";
        buttonGetFromURL.Progress = 0;
        buttonGetFromURL.ProgressColor = System.Drawing.Color.FromArgb(0, 46, 94);
        buttonGetFromURL.Size = new System.Drawing.Size(200, 40);
        buttonGetFromURL.TabIndex = 10;
        buttonGetFromURL.Text = "Get from URL";
        buttonGetFromURL.UseVisualStyleBackColor = true;
        buttonGetFromURL.UseWindowsAccentColor = true;
        buttonGetFromURL.WriteProgress = true;
        buttonGetFromURL.Click += ButtonGetFromURL_Click;
        // 
        // fileBrowse
        // 
        fileBrowse.BorderRadius = 8;
        fileBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        fileBrowse.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        fileBrowse.ForeColor = System.Drawing.Color.White;
        fileBrowse.HoverColor = System.Drawing.Color.FromArgb(0, 89, 184);
        fileBrowse.Icon = null;
        fileBrowse.Location = new System.Drawing.Point(691, 93);
        fileBrowse.Margin = new System.Windows.Forms.Padding(20);
        fileBrowse.Name = "fileBrowse";
        fileBrowse.Progress = 0;
        fileBrowse.ProgressColor = System.Drawing.Color.FromArgb(0, 46, 94);
        fileBrowse.Size = new System.Drawing.Size(120, 40);
        fileBrowse.TabIndex = 9;
        fileBrowse.Text = "Browse";
        fileBrowse.UseVisualStyleBackColor = true;
        fileBrowse.UseWindowsAccentColor = true;
        fileBrowse.WriteProgress = true;
        fileBrowse.Click += FileBrowse_Click;
        // 
        // labelOr
        // 
        labelOr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        labelOr.Location = new System.Drawing.Point(438, 177);
        labelOr.Margin = new System.Windows.Forms.Padding(43, 0, 43, 0);
        labelOr.Name = "labelOr";
        labelOr.Size = new System.Drawing.Size(157, 45);
        labelOr.TabIndex = 11;
        labelOr.Text = "or";
        labelOr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // buttonOK
        // 
        buttonOK.BorderRadius = 8;
        buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
        buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        buttonOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        buttonOK.ForeColor = System.Drawing.Color.White;
        buttonOK.HoverColor = System.Drawing.Color.FromArgb(0, 89, 184);
        buttonOK.Icon = null;
        buttonOK.Location = new System.Drawing.Point(691, 319);
        buttonOK.Margin = new System.Windows.Forms.Padding(20);
        buttonOK.Name = "buttonOK";
        buttonOK.Progress = 0;
        buttonOK.ProgressColor = System.Drawing.Color.FromArgb(0, 46, 94);
        buttonOK.Size = new System.Drawing.Size(120, 40);
        buttonOK.TabIndex = 12;
        buttonOK.Text = "Ok";
        buttonOK.UseVisualStyleBackColor = true;
        buttonOK.UseWindowsAccentColor = true;
        buttonOK.WriteProgress = true;
        buttonOK.Click += ButtonOK_Click;
        // 
        // SoundboardGlobalAudioAddView
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(875, 422);
        Controls.Add(buttonOK);
        Controls.Add(buttonGetFromURL);
        Controls.Add(fileBrowse);
        Controls.Add(labelOr);
        Controls.Add(filePath);
        Controls.Add(labelFile);
        Location = new System.Drawing.Point(0, 0);
        Name = "SoundboardGlobalAudioAddView";
        Text = "SoundboardGlobalAudioAddView";
        Load += SoundboardGlobalAudioAddView_Load;
        Controls.SetChildIndex(labelFile, 0);
        Controls.SetChildIndex(filePath, 0);
        Controls.SetChildIndex(labelOr, 0);
        Controls.SetChildIndex(fileBrowse, 0);
        Controls.SetChildIndex(buttonGetFromURL, 0);
        Controls.SetChildIndex(buttonOK, 0);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label labelFile;
    private RoundedTextBox filePath;
    private ButtonPrimary buttonGetFromURL;
    private ButtonPrimary fileBrowse;
    private System.Windows.Forms.Label labelOr;
    private ButtonPrimary buttonOK;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
}