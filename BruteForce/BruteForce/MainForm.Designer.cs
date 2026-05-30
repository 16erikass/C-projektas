namespace BruteForce
{
    partial class MainForm
    {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code


        private void InitializeComponent()
        {
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnStartSingle = new System.Windows.Forms.Button();
            this.btnStartMulti = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblTargetHash = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblElapsedTime = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();

            this.btnGenerate.Location = new System.Drawing.Point(20, 20);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(140, 30);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "1. Generate Target";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);

            this.btnStartSingle.Enabled = false;
            this.btnStartSingle.Location = new System.Drawing.Point(170, 20);
            this.btnStartSingle.Name = "btnStartSingle";
            this.btnStartSingle.Size = new System.Drawing.Size(110, 30);
            this.btnStartSingle.TabIndex = 1;
            this.btnStartSingle.Text = "2. Single-Thread";
            this.btnStartSingle.UseVisualStyleBackColor = true;
            this.btnStartSingle.Click += new System.EventHandler(this.BtnStartSingle_Click);

            this.btnStartMulti.Enabled = false;
            this.btnStartMulti.Location = new System.Drawing.Point(290, 20);
            this.btnStartMulti.Name = "btnStartMulti";
            this.btnStartMulti.Size = new System.Drawing.Size(110, 30);
            this.btnStartMulti.TabIndex = 2;
            this.btnStartMulti.Text = "3. Multi-Thread";
            this.btnStartMulti.UseVisualStyleBackColor = true;
            this.btnStartMulti.Click += new System.EventHandler(this.BtnStartMulti_Click);

            this.btnStop.BackColor = System.Drawing.Color.LightCoral;
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(410, 20);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(50, 30);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);

            this.lblTargetHash.Location = new System.Drawing.Point(20, 70);
            this.lblTargetHash.Name = "lblTargetHash";
            this.lblTargetHash.Size = new System.Drawing.Size(440, 20);
            this.lblTargetHash.TabIndex = 4;
            this.lblTargetHash.Text = "Target Hash: (None)";

            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(20, 100);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(200, 20);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status: Idle";

            this.lblElapsedTime.Location = new System.Drawing.Point(250, 100);
            this.lblElapsedTime.Name = "lblElapsedTime";
            this.lblElapsedTime.Size = new System.Drawing.Size(200, 20);
            this.lblElapsedTime.TabIndex = 6;
            this.lblElapsedTime.Text = "Elapsed Time: 0.00 s";
            this.lblElapsedTime.TextAlign = System.Drawing.ContentAlignment.TopRight;

            this.progressBar.Location = new System.Drawing.Point(20, 130);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(440, 20);
            this.progressBar.TabIndex = 7;

            this.txtLog.Location = new System.Drawing.Point(20, 170);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(440, 220);
            this.txtLog.TabIndex = 8;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 411);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblElapsedTime);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblTargetHash);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStartMulti);
            this.Controls.Add(this.btnStartSingle);
            this.Controls.Add(this.btnGenerate);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Brute Force Password Cracker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnStartSingle;
        private System.Windows.Forms.Button btnStartMulti;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblTargetHash;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblElapsedTime;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox txtLog;
    }
}