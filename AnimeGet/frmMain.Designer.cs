﻿namespace AnimeGet
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSeries = new System.Windows.Forms.ComboBox();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.dgvVideos = new System.Windows.Forms.DataGridView();
            this.dgvcLinkSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvcTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvcStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcDownload = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dgvcUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcVideo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnFetch = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tmrDownloader = new System.Windows.Forms.Timer(this.components);
            this.tmrFetcher = new System.Windows.Forms.Timer(this.components);
            this.tmrDetector = new System.Windows.Forms.Timer(this.components);
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVideos)).BeginInit();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(69, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Series";
            // 
            // cbSeries
            // 
            this.cbSeries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbSeries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSeries.FormattingEnabled = true;
            this.cbSeries.Location = new System.Drawing.Point(3, 23);
            this.cbSeries.Name = "cbSeries";
            this.cbSeries.Size = new System.Drawing.Size(742, 21);
            this.cbSeries.TabIndex = 0;
            this.cbSeries.SelectedIndexChanged += new System.EventHandler(this.cbSeries_SelectedIndexChanged);
            // 
            // tbUrl
            // 
            this.tbUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbUrl.Location = new System.Drawing.Point(3, 53);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(742, 20);
            this.tbUrl.TabIndex = 2;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tbUrl, 0, 2);
            this.tlpMain.Controls.Add(this.cbSeries, 0, 1);
            this.tlpMain.Controls.Add(this.label1, 0, 0);
            this.tlpMain.Controls.Add(this.dgvVideos, 0, 4);
            this.tlpMain.Controls.Add(this.tlpButtons, 0, 3);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 5;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(748, 550);
            this.tlpMain.TabIndex = 3;
            // 
            // dgvVideos
            // 
            this.dgvVideos.AllowUserToAddRows = false;
            this.dgvVideos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVideos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcLinkSelected,
            this.dgvcTitle,
            this.dgvcType,
            this.dgvcStatus,
            this.dgvcDownload,
            this.dgvcUrl,
            this.dgvcVideo});
            this.dgvVideos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVideos.Location = new System.Drawing.Point(3, 118);
            this.dgvVideos.Name = "dgvVideos";
            this.dgvVideos.Size = new System.Drawing.Size(742, 429);
            this.dgvVideos.TabIndex = 4;
            this.dgvVideos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVideos_CellContentClick);
            this.dgvVideos.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVideos_CellValueChanged);
            // 
            // dgvcLinkSelected
            // 
            this.dgvcLinkSelected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvcLinkSelected.Frozen = true;
            this.dgvcLinkSelected.HeaderText = "Select";
            this.dgvcLinkSelected.Name = "dgvcLinkSelected";
            this.dgvcLinkSelected.Width = 20;
            // 
            // dgvcTitle
            // 
            this.dgvcTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvcTitle.HeaderText = "Title";
            this.dgvcTitle.Name = "dgvcTitle";
            // 
            // dgvcType
            // 
            this.dgvcType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvcType.FillWeight = 191.453F;
            this.dgvcType.HeaderText = "Type";
            this.dgvcType.Name = "dgvcType";
            this.dgvcType.Width = 120;
            // 
            // dgvcStatus
            // 
            this.dgvcStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvcStatus.HeaderText = "Status";
            this.dgvcStatus.Name = "dgvcStatus";
            this.dgvcStatus.ReadOnly = true;
            this.dgvcStatus.Width = 80;
            // 
            // dgvcDownload
            // 
            this.dgvcDownload.HeaderText = "Download";
            this.dgvcDownload.Name = "dgvcDownload";
            this.dgvcDownload.Width = 65;
            // 
            // dgvcUrl
            // 
            this.dgvcUrl.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvcUrl.HeaderText = "URL";
            this.dgvcUrl.Name = "dgvcUrl";
            this.dgvcUrl.ReadOnly = true;
            // 
            // dgvcVideo
            // 
            this.dgvcVideo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvcVideo.HeaderText = "Video";
            this.dgvcVideo.Name = "dgvcVideo";
            this.dgvcVideo.ReadOnly = true;
            // 
            // tlpButtons
            // 
            this.tlpButtons.ColumnCount = 6;
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tlpButtons.Controls.Add(this.tbPath, 2, 0);
            this.tlpButtons.Controls.Add(this.btnFetch, 0, 0);
            this.tlpButtons.Controls.Add(this.btnBrowse, 3, 0);
            this.tlpButtons.Controls.Add(this.btnSettings, 4, 0);
            this.tlpButtons.Controls.Add(this.btnAbout, 5, 0);
            this.tlpButtons.Controls.Add(this.label2, 1, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.Location = new System.Drawing.Point(3, 83);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.Size = new System.Drawing.Size(742, 29);
            this.tlpButtons.TabIndex = 6;
            // 
            // tbPath
            // 
            this.tbPath.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbPath.Location = new System.Drawing.Point(168, 6);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(346, 20);
            this.tbPath.TabIndex = 2;
            this.tbPath.Text = "D:\\TV\\Anime";
            // 
            // btnFetch
            // 
            this.btnFetch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFetch.Location = new System.Drawing.Point(3, 3);
            this.btnFetch.Name = "btnFetch";
            this.btnFetch.Size = new System.Drawing.Size(69, 23);
            this.btnFetch.TabIndex = 0;
            this.btnFetch.Text = "Fetch!";
            this.btnFetch.UseVisualStyleBackColor = true;
            this.btnFetch.Click += new System.EventHandler(this.btnFetch_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBrowse.Location = new System.Drawing.Point(520, 3);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(69, 23);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // btnSettings
            // 
            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSettings.Location = new System.Drawing.Point(595, 3);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(69, 23);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // btnAbout
            // 
            this.btnAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAbout.Location = new System.Drawing.Point(670, 3);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(69, 23);
            this.btnAbout.TabIndex = 5;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(78, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 29);
            this.label2.TabIndex = 6;
            this.label2.Text = "Download path";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmrDownloader
            // 
            this.tmrDownloader.Enabled = true;
            this.tmrDownloader.Tick += new System.EventHandler(this.tmrDownloader_Tick);
            // 
            // tmrFetcher
            // 
            this.tmrFetcher.Enabled = true;
            this.tmrFetcher.Tick += new System.EventHandler(this.tmrFetcher_Tick);
            // 
            // tmrDetector
            // 
            this.tmrDetector.Enabled = true;
            this.tmrDetector.Tick += new System.EventHandler(this.tmrDetector_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 550);
            this.Controls.Add(this.tlpMain);
            this.Name = "frmMain";
            this.Text = "Anime Downloader";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVideos)).EndInit();
            this.tlpButtons.ResumeLayout(false);
            this.tlpButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSeries;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Button btnFetch;
        private System.Windows.Forms.DataGridView dgvVideos;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvcLinkSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcTitle;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvcType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcStatus;
        private System.Windows.Forms.DataGridViewButtonColumn dgvcDownload;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcVideo;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Timer tmrDownloader;
        private System.Windows.Forms.Timer tmrFetcher;
        private System.Windows.Forms.Timer tmrDetector;
    }
}

