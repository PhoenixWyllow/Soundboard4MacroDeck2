
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            comboBoxDevices = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            buttonOK = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            labelDevices = new Label();
            linkLabelResetDevice = new LinkLabel();
            outputPage = new TabPage();
            audioFilePage = new TabPage();
            toolStrip2 = new ToolStrip();
            audioFileAdd = new ToolStripButton();
            audioFilesTable = new DataGridView();
            categoryPage = new TabPage();
            toolStrip1 = new ToolStrip();
            categoriesAdd = new ToolStripButton();
            categoriesTable = new DataGridView();
            navigation = new SuchByte.MacroDeck.GUI.CustomControls.VerticalTabControl();
            outputPage.SuspendLayout();
            audioFilePage.SuspendLayout();
            toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)audioFilesTable).BeginInit();
            categoryPage.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)categoriesTable).BeginInit();
            navigation.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxDevices
            // 
            comboBoxDevices.BackColor = System.Drawing.Color.DimGray;
            comboBoxDevices.Cursor = Cursors.Hand;
            comboBoxDevices.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDevices.Font = new System.Drawing.Font("Tahoma", 9F);
            comboBoxDevices.ForeColor = System.Drawing.Color.White;
            comboBoxDevices.Icon = null;
            comboBoxDevices.Location = new System.Drawing.Point(6, 45);
            comboBoxDevices.Name = "comboBoxDevices";
            comboBoxDevices.Padding = new Padding(8, 2, 8, 2);
            comboBoxDevices.SelectedIndex = -1;
            comboBoxDevices.SelectedItem = null;
            comboBoxDevices.Size = new System.Drawing.Size(404, 26);
            comboBoxDevices.TabIndex = 3;
            comboBoxDevices.SelectedIndexChanged += ComboBoxDevices_SelectedIndexChanged;
            // 
            // buttonOK
            // 
            buttonOK.BorderRadius = 8;
            buttonOK.Cursor = Cursors.Hand;
            buttonOK.DialogResult = DialogResult.OK;
            buttonOK.FlatStyle = FlatStyle.Flat;
            buttonOK.Font = new System.Drawing.Font("Tahoma", 9.75F);
            buttonOK.ForeColor = System.Drawing.Color.White;
            buttonOK.HoverColor = System.Drawing.Color.FromArgb(0, 89, 184);
            buttonOK.Icon = null;
            buttonOK.Location = new System.Drawing.Point(824, 467);
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
            labelDevices.Font = new System.Drawing.Font("Tahoma", 12F);
            labelDevices.ForeColor = System.Drawing.Color.Gray;
            labelDevices.Location = new System.Drawing.Point(6, 13);
            labelDevices.Name = "labelDevices";
            labelDevices.Size = new System.Drawing.Size(159, 19);
            labelDevices.TabIndex = 5;
            labelDevices.Text = "Default output device";
            // 
            // linkLabelResetDevice
            // 
            linkLabelResetDevice.AutoSize = true;
            linkLabelResetDevice.LinkColor = System.Drawing.Color.DodgerBlue;
            linkLabelResetDevice.Location = new System.Drawing.Point(166, 82);
            linkLabelResetDevice.Name = "linkLabelResetDevice";
            linkLabelResetDevice.Size = new System.Drawing.Size(155, 16);
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
            outputPage.Padding = new Padding(3);
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
            audioFilePage.Padding = new Padding(3);
            audioFilePage.Size = new System.Drawing.Size(688, 423);
            audioFilePage.TabIndex = 1;
            audioFilePage.Text = "audioFilePage";
            // 
            // toolStrip2
            // 
            toolStrip2.BackColor = System.Drawing.Color.Transparent;
            toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            toolStrip2.Items.AddRange(new ToolStripItem[] { audioFileAdd });
            toolStrip2.Location = new System.Drawing.Point(3, 3);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Size = new System.Drawing.Size(682, 31);
            toolStrip2.TabIndex = 2;
            toolStrip2.Text = "toolStrip2";
            // 
            // audioFileAdd
            // 
            audioFileAdd.Alignment = ToolStripItemAlignment.Right;
            audioFileAdd.ForeColor = System.Drawing.Color.Black;
            audioFileAdd.Image = (System.Drawing.Image)resources.GetObject("audioFileAdd.Image");
            audioFileAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            audioFileAdd.Name = "audioFileAdd";
            audioFileAdd.Size = new System.Drawing.Size(28, 28);
            // 
            // audioFilesTable
            // 
            audioFilesTable.AllowUserToAddRows = false;
            audioFilesTable.AllowUserToDeleteRows = false;
            audioFilesTable.AllowUserToResizeRows = false;
            audioFilesTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            audioFilesTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            audioFilesTable.BackgroundColor = System.Drawing.Color.FromArgb(45, 45, 45);
            audioFilesTable.BorderStyle = BorderStyle.None;
            audioFilesTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            audioFilesTable.DefaultCellStyle = dataGridViewCellStyle1;
            audioFilesTable.Dock = DockStyle.Bottom;
            audioFilesTable.Location = new System.Drawing.Point(3, 36);
            audioFilesTable.MultiSelect = false;
            audioFilesTable.Name = "audioFilesTable";
            audioFilesTable.RowHeadersWidth = 10;
            audioFilesTable.RowTemplate.Height = 33;
            audioFilesTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            audioFilesTable.Size = new System.Drawing.Size(682, 384);
            audioFilesTable.TabIndex = 0;
            // 
            // categoryPage
            // 
            categoryPage.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            categoryPage.Controls.Add(toolStrip1);
            categoryPage.Controls.Add(categoriesTable);
            categoryPage.Location = new System.Drawing.Point(204, 4);
            categoryPage.Name = "categoryPage";
            categoryPage.Padding = new Padding(3);
            categoryPage.Size = new System.Drawing.Size(688, 423);
            categoryPage.TabIndex = 2;
            categoryPage.Text = "categoryPage";
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = System.Drawing.Color.Transparent;
            toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { categoriesAdd });
            toolStrip1.Location = new System.Drawing.Point(3, 3);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(682, 31);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // categoriesAdd
            // 
            categoriesAdd.Alignment = ToolStripItemAlignment.Right;
            categoriesAdd.ForeColor = System.Drawing.Color.Black;
            categoriesAdd.Image = (System.Drawing.Image)resources.GetObject("categoriesAdd.Image");
            categoriesAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            categoriesAdd.Name = "categoriesAdd";
            categoriesAdd.Size = new System.Drawing.Size(28, 28);
            // 
            // categoriesTable
            // 
            categoriesTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            categoriesTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            categoriesTable.BackgroundColor = System.Drawing.Color.FromArgb(45, 45, 45);
            categoriesTable.BorderStyle = BorderStyle.None;
            categoriesTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            categoriesTable.DefaultCellStyle = dataGridViewCellStyle1;
            categoriesTable.Dock = DockStyle.Bottom;
            categoriesTable.Location = new System.Drawing.Point(3, 38);
            categoriesTable.Name = "categoriesTable";
            categoriesTable.RowHeadersWidth = 10;
            categoriesTable.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            categoriesTable.RowTemplate.Height = 33;
            categoriesTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            categoriesTable.Size = new System.Drawing.Size(682, 382);
            categoriesTable.TabIndex = 0;
            // 
            // navigation
            // 
            navigation.Alignment = TabAlignment.Left;
            navigation.Controls.Add(outputPage);
            navigation.Controls.Add(audioFilePage);
            navigation.Controls.Add(categoryPage);
            navigation.ItemSize = new System.Drawing.Size(44, 200);
            navigation.Location = new System.Drawing.Point(3, 28);
            navigation.Multiline = true;
            navigation.Name = "navigation";
            navigation.SelectedIndex = 0;
            navigation.SelectedTabColor = System.Drawing.Color.FromArgb(0, 103, 205);
            navigation.Size = new System.Drawing.Size(896, 431);
            navigation.SizeMode = TabSizeMode.Fixed;
            navigation.TabIndex = 7;
            navigation.Selecting += Navigation_Selecting;
            // 
            // SoundboardGlobalConfigViewV2
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(907, 503);
            Controls.Add(navigation);
            Controls.Add(buttonOK);
            Name = "SoundboardGlobalConfigViewV2";
            Padding = new Padding(20);
            Text = "GlobalConfig";
            Load += SoundboardGlobalConfigView_Load;
            outputPage.ResumeLayout(false);
            outputPage.PerformLayout();
            audioFilePage.ResumeLayout(false);
            audioFilePage.PerformLayout();
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)audioFilesTable).EndInit();
            categoryPage.ResumeLayout(false);
            categoryPage.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)categoriesTable).EndInit();
            navigation.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox comboBoxDevices;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary buttonOK;
        private Label labelDevices;
        private LinkLabel linkLabelResetDevice;
        private SuchByte.MacroDeck.GUI.CustomControls.VerticalTabControl navigation;
        private TabPage outputPage;
        private TabPage audioFilePage;
        private TabPage categoryPage;
        private DataGridView audioFilesTable;
        private DataGridView categoriesTable;
        private ToolStrip toolStrip1;
        private ToolStripButton categoriesAdd;
        private ToolStrip toolStrip2;
        private ToolStripButton audioFileAdd;
    }
}