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
            this.lbStatus = new System.Windows.Forms.Label();
            this.lbEnvironmentRun = new System.Windows.Forms.Label();
            this.tabGoogleSheets.SuspendLayout();
            this.tabRunning.SuspendLayout();
            this.tabFileExcel.SuspendLayout();
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
            this.btnRun.Location = new System.Drawing.Point(212, 262);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(122, 37);
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
            this.btnStop.Location = new System.Drawing.Point(372, 262);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(117, 37);
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
            this.tabGoogleSheets.Size = new System.Drawing.Size(647, 73);
            this.tabGoogleSheets.TabIndex = 1;
            this.tabGoogleSheets.Text = "Google Sheets";
            this.tabGoogleSheets.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Link GoogleSheets";
            // 
            // tbLinkGoogleSheets
            // 
            this.tbLinkGoogleSheets.Location = new System.Drawing.Point(130, 23);
            this.tbLinkGoogleSheets.Name = "tbLinkGoogleSheets";
            this.tbLinkGoogleSheets.Size = new System.Drawing.Size(500, 23);
            this.tbLinkGoogleSheets.TabIndex = 0;
            this.tbLinkGoogleSheets.TextChanged += new System.EventHandler(this.tbLinktbGoogleSheets_TextChanged);
            // 
            // tabRunning
            // 
            this.tabRunning.Controls.Add(this.tabFileExcel);
            this.tabRunning.Controls.Add(this.tabGoogleSheets);
            this.tabRunning.Location = new System.Drawing.Point(17, 13);
            this.tabRunning.Name = "tabRunning";
            this.tabRunning.SelectedIndex = 0;
            this.tabRunning.Size = new System.Drawing.Size(655, 102);
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
            this.tabFileExcel.Size = new System.Drawing.Size(647, 73);
            this.tabFileExcel.TabIndex = 0;
            this.tabFileExcel.Text = "File Excel";
            this.tabFileExcel.UseVisualStyleBackColor = true;
            // 
            // tbLinkFileExcel
            // 
            this.tbLinkFileExcel.Location = new System.Drawing.Point(136, 21);
            this.tbLinkFileExcel.Margin = new System.Windows.Forms.Padding(4);
            this.tbLinkFileExcel.Name = "tbLinkFileExcel";
            this.tbLinkFileExcel.ReadOnly = true;
            this.tbLinkFileExcel.Size = new System.Drawing.Size(495, 23);
            this.tbLinkFileExcel.TabIndex = 1;
            this.tbLinkFileExcel.TextChanged += new System.EventHandler(this.tbLinkFileExcel_TextChanged);
            this.tbLinkFileExcel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbLinkFileExcel_MouseDoubleClick);
            // 
            // btnSelectFileExcel
            // 
            this.btnSelectFileExcel.Location = new System.Drawing.Point(9, 19);
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
            this.progressStatus.Location = new System.Drawing.Point(21, 172);
            this.progressStatus.Name = "progressStatus";
            this.progressStatus.Size = new System.Drawing.Size(651, 23);
            this.progressStatus.TabIndex = 4;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(350, 147);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(0, 16);
            this.lbStatus.TabIndex = 5;
            // 
            // lbEnvironmentRun
            // 
            this.lbEnvironmentRun.AutoSize = true;
            this.lbEnvironmentRun.Location = new System.Drawing.Point(21, 148);
            this.lbEnvironmentRun.Name = "lbEnvironmentRun";
            this.lbEnvironmentRun.Size = new System.Drawing.Size(0, 16);
            this.lbEnvironmentRun.TabIndex = 7;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 319);
            this.Controls.Add(this.lbEnvironmentRun);
            this.Controls.Add(this.tabRunning);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRun);
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
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label lbEnvironmentRun;
    }
}

