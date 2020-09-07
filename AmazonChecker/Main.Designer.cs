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
            this.btnSelectFileExcel = new System.Windows.Forms.Button();
            this.openSelectFileExcel = new System.Windows.Forms.OpenFileDialog();
            this.tbLinkFileExcel = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.progressStatus = new System.Windows.Forms.ProgressBar();
            this.lbStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSelectFileExcel
            // 
            this.btnSelectFileExcel.Location = new System.Drawing.Point(14, 22);
            this.btnSelectFileExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectFileExcel.Name = "btnSelectFileExcel";
            this.btnSelectFileExcel.Size = new System.Drawing.Size(123, 25);
            this.btnSelectFileExcel.TabIndex = 0;
            this.btnSelectFileExcel.Text = "Chọn file excel";
            this.btnSelectFileExcel.UseVisualStyleBackColor = true;
            this.btnSelectFileExcel.Click += new System.EventHandler(this.btnSelectFileExcel_Click);
            // 
            // openSelectFileExcel
            // 
            this.openSelectFileExcel.FileName = "openSelectFileExcel";
            // 
            // tbLinkFileExcel
            // 
            this.tbLinkFileExcel.Location = new System.Drawing.Point(157, 23);
            this.tbLinkFileExcel.Margin = new System.Windows.Forms.Padding(4);
            this.tbLinkFileExcel.Name = "tbLinkFileExcel";
            this.tbLinkFileExcel.ReadOnly = true;
            this.tbLinkFileExcel.Size = new System.Drawing.Size(376, 23);
            this.tbLinkFileExcel.TabIndex = 1;
            this.tbLinkFileExcel.TextChanged += new System.EventHandler(this.tbLinkFileExcel_TextChanged);
            this.tbLinkFileExcel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbLinkFileExcel_MouseDoubleClick);
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRun.Font = new System.Drawing.Font(".VnArialH", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.Location = new System.Drawing.Point(105, 195);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(160, 59);
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
            this.btnStop.Location = new System.Drawing.Point(291, 195);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(160, 59);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // progressStatus
            // 
            this.progressStatus.Location = new System.Drawing.Point(12, 86);
            this.progressStatus.Name = "progressStatus";
            this.progressStatus.Size = new System.Drawing.Size(519, 23);
            this.progressStatus.TabIndex = 4;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(249, 64);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(0, 16);
            this.lbStatus.TabIndex = 5;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 267);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.progressStatus);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.tbLinkFileExcel);
            this.Controls.Add(this.btnSelectFileExcel);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "FormAmazonChecker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAmazonChecker_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFileExcel;
        private System.Windows.Forms.OpenFileDialog openSelectFileExcel;
        private System.Windows.Forms.TextBox tbLinkFileExcel;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ProgressBar progressStatus;
        private System.Windows.Forms.Label lbStatus;
    }
}

