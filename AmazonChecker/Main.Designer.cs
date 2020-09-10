namespace AmazonChecker
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.openSelectFileExcel = new System.Windows.Forms.OpenFileDialog();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.tabGoogleSheets = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLinkGoogleSheets = new System.Windows.Forms.TextBox();
            this.tabRunning = new System.Windows.Forms.TabControl();
            this.tabFileExcel = new System.Windows.Forms.TabPage();
            this.tbLinkFileExcel = new System.Windows.Forms.TextBox();
            this.btnSelectFileExcel = new System.Windows.Forms.Button();
            this.progressStatus = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblEnvironmentRun = new System.Windows.Forms.Label();
            this.cbHideBrowser = new System.Windows.Forms.CheckBox();
            this.pnButton = new System.Windows.Forms.Panel();
            this.tabGoogleSheets.SuspendLayout();
            this.tabRunning.SuspendLayout();
            this.tabFileExcel.SuspendLayout();
            this.pnButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // openSelectFileExcel
            // 
            this.openSelectFileExcel.FileName = "openSelectFileExcel";
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRun.Font = new System.Drawing.Font(".VnArialH", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.Location = new System.Drawing.Point(0, 4);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(122, 53);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Gray;
            this.btnStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font(".VnArialH", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.Transparent;
            this.btnStop.Location = new System.Drawing.Point(130, 4);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(117, 53);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // tabGoogleSheets
            // 
            this.tabGoogleSheets.Controls.Add(this.label1);
            this.tabGoogleSheets.Controls.Add(this.tbLinkGoogleSheets);
            this.tabGoogleSheets.Location = new System.Drawing.Point(4, 25);
            this.tabGoogleSheets.Name = "tabGoogleSheets";
            this.tabGoogleSheets.Padding = new System.Windows.Forms.Padding(3);
            this.tabGoogleSheets.Size = new System.Drawing.Size(657, 73);
            this.tabGoogleSheets.TabIndex = 1;
            this.tabGoogleSheets.Text = "Google Sheets";
            this.tabGoogleSheets.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Link GoogleSheets";
            // 
            // tbLinkGoogleSheets
            // 
            this.tbLinkGoogleSheets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLinkGoogleSheets.Location = new System.Drawing.Point(117, 23);
            this.tbLinkGoogleSheets.Name = "tbLinkGoogleSheets";
            this.tbLinkGoogleSheets.Size = new System.Drawing.Size(538, 23);
            this.tbLinkGoogleSheets.TabIndex = 0;
            this.tbLinkGoogleSheets.TextChanged += new System.EventHandler(this.tbLinktbGoogleSheets_TextChanged);
            // 
            // tabRunning
            // 
            this.tabRunning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabRunning.Controls.Add(this.tabFileExcel);
            this.tabRunning.Controls.Add(this.tabGoogleSheets);
            this.tabRunning.Location = new System.Drawing.Point(6, 13);
            this.tabRunning.Name = "tabRunning";
            this.tabRunning.SelectedIndex = 0;
            this.tabRunning.Size = new System.Drawing.Size(665, 102);
            this.tabRunning.TabIndex = 6;
            this.tabRunning.SelectedIndexChanged += new System.EventHandler(this.tabFileExccel_SelectedIndexChanged);
            // 
            // tabFileExcel
            // 
            this.tabFileExcel.Controls.Add(this.tbLinkFileExcel);
            this.tabFileExcel.Controls.Add(this.btnSelectFileExcel);
            this.tabFileExcel.Location = new System.Drawing.Point(4, 25);
            this.tabFileExcel.Name = "tabFileExcel";
            this.tabFileExcel.Padding = new System.Windows.Forms.Padding(3);
            this.tabFileExcel.Size = new System.Drawing.Size(657, 73);
            this.tabFileExcel.TabIndex = 0;
            this.tabFileExcel.Text = "File Excel";
            this.tabFileExcel.UseVisualStyleBackColor = true;
            // 
            // tbLinkFileExcel
            // 
            this.tbLinkFileExcel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLinkFileExcel.Location = new System.Drawing.Point(120, 21);
            this.tbLinkFileExcel.Margin = new System.Windows.Forms.Padding(4);
            this.tbLinkFileExcel.Name = "tbLinkFileExcel";
            this.tbLinkFileExcel.ReadOnly = true;
            this.tbLinkFileExcel.Size = new System.Drawing.Size(530, 23);
            this.tbLinkFileExcel.TabIndex = 1;
            this.tbLinkFileExcel.TextChanged += new System.EventHandler(this.tbLinkFileExcel_TextChanged);
            this.tbLinkFileExcel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbLinkFileExcel_MouseDoubleClick);
            // 
            // btnSelectFileExcel
            // 
            this.btnSelectFileExcel.Location = new System.Drawing.Point(7, 21);
            this.btnSelectFileExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectFileExcel.Name = "btnSelectFileExcel";
            this.btnSelectFileExcel.Size = new System.Drawing.Size(105, 25);
            this.btnSelectFileExcel.TabIndex = 0;
            this.btnSelectFileExcel.Text = "Chọn file excel";
            this.btnSelectFileExcel.UseVisualStyleBackColor = true;
            this.btnSelectFileExcel.Click += new System.EventHandler(this.btnSelectFileExcel_Click);
            // 
            // progressStatus
            // 
            this.progressStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressStatus.Location = new System.Drawing.Point(6, 178);
            this.progressStatus.Name = "progressStatus";
            this.progressStatus.Size = new System.Drawing.Size(665, 35);
            this.progressStatus.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(6, 154);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(70, 21);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "lblStatus";
            // 
            // lblEnvironmentRun
            // 
            this.lblEnvironmentRun.AutoSize = true;
            this.lblEnvironmentRun.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnvironmentRun.Location = new System.Drawing.Point(270, 244);
            this.lblEnvironmentRun.Name = "lblEnvironmentRun";
            this.lblEnvironmentRun.Size = new System.Drawing.Size(144, 21);
            this.lblEnvironmentRun.TabIndex = 7;
            this.lblEnvironmentRun.Text = "lblEnvironmentRun";
            // 
            // cbShowBrowser
            // 
            this.cbHideBrowser.AutoSize = true;
            this.cbHideBrowser.Checked = true;
            this.cbHideBrowser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHideBrowser.Location = new System.Drawing.Point(6, 121);
            this.cbHideBrowser.Name = "cbShowBrowser";
            this.cbHideBrowser.Size = new System.Drawing.Size(103, 20);
            this.cbHideBrowser.TabIndex = 8;
            this.cbHideBrowser.Text = "Ẩn trình duyệt";
            this.cbHideBrowser.UseVisualStyleBackColor = true;
            this.cbHideBrowser.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // pnButton
            // 
            this.pnButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnButton.Controls.Add(this.btnRun);
            this.pnButton.Controls.Add(this.btnStop);
            this.pnButton.Location = new System.Drawing.Point(6, 219);
            this.pnButton.Name = "pnButton";
            this.pnButton.Size = new System.Drawing.Size(252, 63);
            this.pnButton.TabIndex = 9;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 289);
            this.Controls.Add(this.pnButton);
            this.Controls.Add(this.cbHideBrowser);
            this.Controls.Add(this.lblEnvironmentRun);
            this.Controls.Add(this.tabRunning);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressStatus);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "FormAmazonChecker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAmazonChecker_FormClosing);
            this.tabGoogleSheets.ResumeLayout(false);
            this.tabGoogleSheets.PerformLayout();
            this.tabRunning.ResumeLayout(false);
            this.tabFileExcel.ResumeLayout(false);
            this.tabFileExcel.PerformLayout();
            this.pnButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openSelectFileExcel;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TabPage tabGoogleSheets;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLinkGoogleSheets;
        private System.Windows.Forms.TabControl tabRunning;
        private System.Windows.Forms.TabPage tabFileExcel;
        private System.Windows.Forms.TextBox tbLinkFileExcel;
        private System.Windows.Forms.Button btnSelectFileExcel;
        private System.Windows.Forms.ProgressBar progressStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblEnvironmentRun;
        private System.Windows.Forms.CheckBox cbHideBrowser;
        private System.Windows.Forms.Panel pnButton;
    }
}

