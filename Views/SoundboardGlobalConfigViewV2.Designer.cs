
using System.Windows.Forms;

namespace Soundboard4MacroDeck.Views
{
    partial class SoundboardGlobalConfigViewV2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoundboardGlobalConfigViewV2));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            comboBoxDevices = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            buttonOK = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            labelDevices = new System.Windows.Forms.Label();
            linkLabelResetDevice = new System.Windows.Forms.LinkLabel();
            outputPage = new System.Windows.Forms.TabPage();
            audioFilePage = new System.Windows.Forms.TabPage();
            toolStrip2 = new System.Windows.Forms.ToolStrip();
            audioFileAdd = new System.Windows.Forms.ToolStripButton();
            audioFilesTable = new System.Windows.Forms.DataGridView();
            setupPage = new System.Windows.Forms.TabPage();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            categoriesAdd = new System.Windows.Forms.ToolStripButton();
            categoriesTable = new System.Windows.Forms.DataGridView();
            navigation = new SuchByte.MacroDeck.GUI.CustomControls.VerticalTabControl();
            outputPage.SuspendLayout();
            audioFilePage.SuspendLayout();
            toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)audioFilesTable).BeginInit();
            setupPage.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)categoriesTable).BeginInit();
            navigation.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxDevices
            // 
            comboBoxDevices.BackColor = System.Drawing.Color.DimGray;
            comboBoxDevices.Cursor = System.Windows.Forms.Cursors.Hand;
            comboBoxDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxDevices.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBoxDevices.ForeColor = System.Drawing.Color.White;
            comboBoxDevices.Icon = null;
            comboBoxDevices.Location = new System.Drawing.Point(6, 45);
            comboBoxDevices.Name = "comboBoxDevices";
            comboBoxDevices.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            comboBoxDevices.SelectedIndex = -1;
            comboBoxDevices.SelectedItem = null;
            comboBoxDevices.Size = new System.Drawing.Size(404, 34);
            comboBoxDevices.TabIndex = 3;
            comboBoxDevices.SelectedIndexChanged += ComboBoxDevices_SelectedIndexChanged;
            // 
            // buttonOK
            // 
            buttonOK.BorderRadius = 8;
            buttonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonOK.ForeColor = System.Drawing.Color.White;
            buttonOK.HoverColor = System.Drawing.Color.FromArgb(0, 89, 184);
            buttonOK.Icon = null;
            buttonOK.Location = new System.Drawing.Point(824, 444);
            buttonOK.Name = "buttonOK";
            buttonOK.Progress = 0;
            buttonOK.ProgressColor = System.Drawing.Color.FromArgb(0, 46, 94);
            buttonOK.Size = new System.Drawing.Size(75, 27);
            buttonOK.TabIndex = 4;
            buttonOK.Text = "Ok";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.UseWindowsAccentColor = true;
            buttonOK.WriteProgress = true;
            buttonOK.Click += ButtonOK_Click;
            // 
            // labelDevices
            // 
            labelDevices.AutoSize = true;
            labelDevices.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelDevices.ForeColor = System.Drawing.Color.Gray;
            labelDevices.Location = new System.Drawing.Point(6, 13);
            labelDevices.Name = "labelDevices";
            labelDevices.Size = new System.Drawing.Size(240, 29);
            labelDevices.TabIndex = 5;
            labelDevices.Text = "Default output device";
            // 
            // linkLabelResetDevice
            // 
            linkLabelResetDevice.AutoSize = true;
            linkLabelResetDevice.LinkColor = System.Drawing.Color.DodgerBlue;
            linkLabelResetDevice.Location = new System.Drawing.Point(166, 82);
            linkLabelResetDevice.Name = "linkLabelResetDevice";
            linkLabelResetDevice.Size = new System.Drawing.Size(243, 24);
            linkLabelResetDevice.TabIndex = 6;
            linkLabelResetDevice.TabStop = true;
            linkLabelResetDevice.Text = "Use system default device";
            linkLabelResetDevice.LinkClicked += LinkLabelResetDevice_LinkClicked;
            // 
            // outputPage
            // 
            outputPage.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            outputPage.Controls.Add(labelDevices);
            outputPage.Controls.Add(linkLabelResetDevice);
            outputPage.Controls.Add(comboBoxDevices);
            outputPage.Location = new System.Drawing.Point(204, 4);
            outputPage.Name = "outputPage";
            outputPage.Padding = new System.Windows.Forms.Padding(3);
            outputPage.Size = new System.Drawing.Size(688, 423);
            outputPage.TabIndex = 0;
            outputPage.Text = "outputPage";
            // 
            // audioFilePage
            // 
            audioFilePage.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            audioFilePage.Controls.Add(toolStrip2);
            audioFilePage.Controls.Add(audioFilesTable);
            audioFilePage.Location = new System.Drawing.Point(204, 4);
            audioFilePage.Name = "audioFilePage";
            audioFilePage.Padding = new System.Windows.Forms.Padding(3);
            audioFilePage.Size = new System.Drawing.Size(688, 423);
            audioFilePage.TabIndex = 1;
            audioFilePage.Text = "audioFilePage";
            // 
            // toolStrip2
            // 
            toolStrip2.BackColor = System.Drawing.Color.Transparent;
            toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { audioFileAdd });
            toolStrip2.Location = new System.Drawing.Point(3, 3);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Size = new System.Drawing.Size(682, 33);
            toolStrip2.TabIndex = 2;
            toolStrip2.Text = "toolStrip2";
            // 
            // audioFileAdd
            // 
            audioFileAdd.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            audioFileAdd.ForeColor = System.Drawing.Color.Black;
            audioFileAdd.Image = (System.Drawing.Image)resources.GetObject("audioFileAdd.Image");
            audioFileAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            audioFileAdd.Name = "audioFileAdd";
            audioFileAdd.Size = new System.Drawing.Size(34, 28);
            // 
            // audioFilesTable
            // 
            audioFilesTable.AllowUserToAddRows = false;
            audioFilesTable.AllowUserToDeleteRows = false;
            audioFilesTable.AllowUserToResizeRows = false;
            audioFilesTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            audioFilesTable.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            audioFilesTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            audioFilesTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            audioFilesTable.DefaultCellStyle = dataGridViewCellStyle1;
            audioFilesTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            audioFilesTable.Location = new System.Drawing.Point(3, 36);
            audioFilesTable.MultiSelect = false;
            audioFilesTable.Name = "audioFilesTable";
            audioFilesTable.RowHeadersWidth = 10;
            audioFilesTable.RowTemplate.Height = 33;
            audioFilesTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            audioFilesTable.Size = new System.Drawing.Size(682, 384);
            audioFilesTable.TabIndex = 0;
            // 
            // setupPage
            // 
            setupPage.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            setupPage.Controls.Add(toolStrip1);
            setupPage.Controls.Add(categoriesTable);
            setupPage.Location = new System.Drawing.Point(204, 4);
            setupPage.Name = "setupPage";
            setupPage.Padding = new System.Windows.Forms.Padding(3);
            setupPage.Size = new System.Drawing.Size(688, 423);
            setupPage.TabIndex = 2;
            setupPage.Text = "setupPage";
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { categoriesAdd });
            toolStrip1.Location = new System.Drawing.Point(3, 3);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(682, 33);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // categoriesAdd
            // 
            categoriesAdd.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            categoriesAdd.ForeColor = System.Drawing.Color.Black;
            categoriesAdd.Image = (System.Drawing.Image)resources.GetObject("categoriesAdd.Image");
            categoriesAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            categoriesAdd.Name = "categoriesAdd";
            categoriesAdd.Size = new System.Drawing.Size(34, 28);
            // 
            // categoriesTable
            // 
            categoriesTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            categoriesTable.BackgroundColor = System.Drawing.SystemColors.Window;
            categoriesTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            categoriesTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            categoriesTable.DefaultCellStyle = dataGridViewCellStyle1;
            categoriesTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            categoriesTable.Location = new System.Drawing.Point(3, 38);
            categoriesTable.Name = "categoriesTable";
            categoriesTable.RowHeadersWidth = 10;
            categoriesTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            categoriesTable.RowTemplate.Height = 33;
            categoriesTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            categoriesTable.Size = new System.Drawing.Size(682, 382);
            categoriesTable.TabIndex = 0;
            // 
            // navigation
            // 
            navigation.Alignment = System.Windows.Forms.TabAlignment.Left;
            navigation.Controls.Add(outputPage);
            navigation.Controls.Add(audioFilePage);
            navigation.Controls.Add(setupPage);
            navigation.ItemSize = new System.Drawing.Size(44, 200);
            navigation.Location = new System.Drawing.Point(3, 3);
            navigation.Multiline = true;
            navigation.Name = "navigation";
            navigation.SelectedIndex = 0;
            navigation.SelectedTabColor = System.Drawing.Color.FromArgb(0, 103, 205);
            navigation.Size = new System.Drawing.Size(896, 431);
            navigation.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            navigation.TabIndex = 7;
            navigation.Selecting += Navigation_Selecting;
            // 
            // SoundboardGlobalConfigViewV2
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(907, 490);
            Controls.Add(navigation);
            Controls.Add(buttonOK);
            Location = new System.Drawing.Point(0, 0);
            Name = "SoundboardGlobalConfigViewV2";
            Padding = new System.Windows.Forms.Padding(20);
            Text = "GlobalConfig";
            Load += SoundboardGlobalConfigView_Load;
            Controls.SetChildIndex(buttonOK, 0);
            Controls.SetChildIndex(navigation, 0);
            outputPage.ResumeLayout(false);
            outputPage.PerformLayout();
            audioFilePage.ResumeLayout(false);
            audioFilePage.PerformLayout();
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)audioFilesTable).EndInit();
            setupPage.ResumeLayout(false);
            setupPage.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)categoriesTable).EndInit();
            navigation.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox comboBoxDevices;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary buttonOK;
        private System.Windows.Forms.Label labelDevices;
        private System.Windows.Forms.LinkLabel linkLabelResetDevice;
        private SuchByte.MacroDeck.GUI.CustomControls.VerticalTabControl navigation;
        private System.Windows.Forms.TabPage outputPage;
        private System.Windows.Forms.TabPage audioFilePage;
        private System.Windows.Forms.TabPage setupPage;
        private System.Windows.Forms.DataGridView audioFilesTable;
        private System.Windows.Forms.DataGridView categoriesTable;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton categoriesAdd;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton audioFileAdd;
    }
}