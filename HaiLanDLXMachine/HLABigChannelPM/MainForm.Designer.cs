namespace HLABigChannel
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnMain = new System.Windows.Forms.Panel();
            this.dmButton9_Cancel = new DMSkin.Controls.DMButton();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblSayHello = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.btnDeliver = new DMSkin.Controls.DMButton();
            this.btnReciever = new DMSkin.Controls.DMButton();
            this.manualDownloadButton = new DMSkin.Controls.DMButton();
            this.label1 = new System.Windows.Forms.Label();
            this.netStatusButton = new DMSkin.Controls.DMButton();
            this.dmButton3 = new DMSkin.Controls.DMButton();
            this.label2_IP = new System.Windows.Forms.Label();
            this.btnDJJ = new DMSkin.Controls.DMButton();
            this.pnMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnMain
            // 
            this.pnMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnMain.Controls.Add(this.dmButton9_Cancel);
            this.pnMain.Controls.Add(this.lblTime);
            this.pnMain.Controls.Add(this.lblSayHello);
            this.pnMain.Controls.Add(this.label4);
            this.pnMain.Controls.Add(this.lblDate);
            this.pnMain.Controls.Add(this.btnDJJ);
            this.pnMain.Controls.Add(this.btnDeliver);
            this.pnMain.Controls.Add(this.btnReciever);
            this.pnMain.Location = new System.Drawing.Point(12, 159);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(1044, 343);
            this.pnMain.TabIndex = 12;
            // 
            // dmButton9_Cancel
            // 
            this.dmButton9_Cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dmButton9_Cancel.AutoEllipsis = true;
            this.dmButton9_Cancel.BackColor = System.Drawing.Color.Transparent;
            this.dmButton9_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dmButton9_Cancel.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.dmButton9_Cancel.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(124)))), ((int)(((byte)(16)))));
            this.dmButton9_Cancel.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(212)))), ((int)(((byte)(100)))));
            this.dmButton9_Cancel.DM_NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dmButton9_Cancel.DM_Radius = 8;
            this.dmButton9_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.dmButton9_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dmButton9_Cancel.Font = new System.Drawing.Font("微软雅黑", 24F);
            this.dmButton9_Cancel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.dmButton9_Cancel.Image = null;
            this.dmButton9_Cancel.Location = new System.Drawing.Point(780, 14);
            this.dmButton9_Cancel.Margin = new System.Windows.Forms.Padding(0);
            this.dmButton9_Cancel.Name = "dmButton9_Cancel";
            this.dmButton9_Cancel.Size = new System.Drawing.Size(174, 62);
            this.dmButton9_Cancel.TabIndex = 21;
            this.dmButton9_Cancel.Text = "退货点数";
            this.dmButton9_Cancel.UseVisualStyleBackColor = true;
            this.dmButton9_Cancel.Click += new System.EventHandler(this.dmButton9_Cancel_Click);
            // 
            // lblTime
            // 
            this.lblTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 60F);
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(17, 349);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(400, 110);
            this.lblTime.TabIndex = 17;
            this.lblTime.Text = "00:00:00";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTime.Visible = false;
            // 
            // lblSayHello
            // 
            this.lblSayHello.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSayHello.Font = new System.Drawing.Font("微软雅黑", 35F);
            this.lblSayHello.ForeColor = System.Drawing.Color.White;
            this.lblSayHello.Location = new System.Drawing.Point(-10, 372);
            this.lblSayHello.Name = "lblSayHello";
            this.lblSayHello.Size = new System.Drawing.Size(400, 69);
            this.lblSayHello.TabIndex = 17;
            this.lblSayHello.Text = "早上好";
            this.lblSayHello.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSayHello.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 35F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(17, 370);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(400, 71);
            this.label4.TabIndex = 17;
            this.label4.Text = "欢迎使用";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDate.Font = new System.Drawing.Font("微软雅黑", 35F);
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.Location = new System.Drawing.Point(3, 367);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(400, 74);
            this.lblDate.TabIndex = 17;
            this.lblDate.Text = "2015年12月4日";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDate.Visible = false;
            // 
            // btnDeliver
            // 
            this.btnDeliver.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDeliver.AutoEllipsis = true;
            this.btnDeliver.BackColor = System.Drawing.Color.Transparent;
            this.btnDeliver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeliver.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnDeliver.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(124)))), ((int)(((byte)(16)))));
            this.btnDeliver.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(212)))), ((int)(((byte)(100)))));
            this.btnDeliver.DM_NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDeliver.DM_Radius = 8;
            this.btnDeliver.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnDeliver.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeliver.Font = new System.Drawing.Font("微软雅黑", 24F);
            this.btnDeliver.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnDeliver.Image = null;
            this.btnDeliver.Location = new System.Drawing.Point(423, 14);
            this.btnDeliver.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeliver.Name = "btnDeliver";
            this.btnDeliver.Size = new System.Drawing.Size(221, 62);
            this.btnDeliver.TabIndex = 13;
            this.btnDeliver.Text = "发货";
            this.btnDeliver.UseVisualStyleBackColor = true;
            this.btnDeliver.Click += new System.EventHandler(this.btnDeliver_Click);
            // 
            // btnReciever
            // 
            this.btnReciever.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReciever.AutoEllipsis = true;
            this.btnReciever.BackColor = System.Drawing.Color.Transparent;
            this.btnReciever.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReciever.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnReciever.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(124)))), ((int)(((byte)(16)))));
            this.btnReciever.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(212)))), ((int)(((byte)(100)))));
            this.btnReciever.DM_NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReciever.DM_Radius = 8;
            this.btnReciever.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnReciever.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReciever.Font = new System.Drawing.Font("微软雅黑", 24F);
            this.btnReciever.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnReciever.Image = null;
            this.btnReciever.Location = new System.Drawing.Point(89, 14);
            this.btnReciever.Margin = new System.Windows.Forms.Padding(0);
            this.btnReciever.Name = "btnReciever";
            this.btnReciever.Size = new System.Drawing.Size(203, 62);
            this.btnReciever.TabIndex = 13;
            this.btnReciever.Text = "收货";
            this.btnReciever.UseVisualStyleBackColor = true;
            this.btnReciever.Click += new System.EventHandler(this.btnReciever_Click);
            // 
            // manualDownloadButton
            // 
            this.manualDownloadButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.manualDownloadButton.AutoEllipsis = true;
            this.manualDownloadButton.BackColor = System.Drawing.Color.BurlyWood;
            this.manualDownloadButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.manualDownloadButton.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.manualDownloadButton.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(124)))), ((int)(((byte)(16)))));
            this.manualDownloadButton.DM_MoveColor = System.Drawing.Color.Transparent;
            this.manualDownloadButton.DM_NormalColor = System.Drawing.Color.Transparent;
            this.manualDownloadButton.DM_Radius = 8;
            this.manualDownloadButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.manualDownloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.manualDownloadButton.Font = new System.Drawing.Font("Iaxurefont", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manualDownloadButton.ForeColor = System.Drawing.Color.Yellow;
            this.manualDownloadButton.Image = null;
            this.manualDownloadButton.Location = new System.Drawing.Point(101, 552);
            this.manualDownloadButton.Margin = new System.Windows.Forms.Padding(0);
            this.manualDownloadButton.Name = "manualDownloadButton";
            this.manualDownloadButton.Size = new System.Drawing.Size(174, 57);
            this.manualDownloadButton.TabIndex = 18;
            this.manualDownloadButton.Text = "主数据下载";
            this.manualDownloadButton.UseVisualStyleBackColor = false;
            this.manualDownloadButton.Click += new System.EventHandler(this.manualDownloadButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1044, 119);
            this.label1.TabIndex = 17;
            this.label1.Text = "HLA单检机智能系统";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // netStatusButton
            // 
            this.netStatusButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.netStatusButton.AutoEllipsis = true;
            this.netStatusButton.BackColor = System.Drawing.Color.BurlyWood;
            this.netStatusButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.netStatusButton.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.netStatusButton.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(124)))), ((int)(((byte)(16)))));
            this.netStatusButton.DM_MoveColor = System.Drawing.Color.Transparent;
            this.netStatusButton.DM_NormalColor = System.Drawing.Color.Transparent;
            this.netStatusButton.DM_Radius = 8;
            this.netStatusButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.netStatusButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.netStatusButton.Font = new System.Drawing.Font("Iaxurefont", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.netStatusButton.ForeColor = System.Drawing.Color.Yellow;
            this.netStatusButton.Image = null;
            this.netStatusButton.Location = new System.Drawing.Point(435, 552);
            this.netStatusButton.Margin = new System.Windows.Forms.Padding(0);
            this.netStatusButton.Name = "netStatusButton";
            this.netStatusButton.Size = new System.Drawing.Size(164, 57);
            this.netStatusButton.TabIndex = 22;
            this.netStatusButton.Text = "网络状态";
            this.netStatusButton.UseVisualStyleBackColor = false;
            this.netStatusButton.Click += new System.EventHandler(this.netStatusButton_Click);
            // 
            // dmButton3
            // 
            this.dmButton3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dmButton3.AutoEllipsis = true;
            this.dmButton3.BackColor = System.Drawing.Color.BurlyWood;
            this.dmButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dmButton3.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.dmButton3.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(124)))), ((int)(((byte)(16)))));
            this.dmButton3.DM_MoveColor = System.Drawing.Color.Transparent;
            this.dmButton3.DM_NormalColor = System.Drawing.Color.Transparent;
            this.dmButton3.DM_Radius = 8;
            this.dmButton3.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.dmButton3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dmButton3.Font = new System.Drawing.Font("Iaxurefont", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dmButton3.ForeColor = System.Drawing.Color.Yellow;
            this.dmButton3.Image = null;
            this.dmButton3.Location = new System.Drawing.Point(797, 552);
            this.dmButton3.Margin = new System.Windows.Forms.Padding(0);
            this.dmButton3.Name = "dmButton3";
            this.dmButton3.Size = new System.Drawing.Size(169, 57);
            this.dmButton3.TabIndex = 28;
            this.dmButton3.Text = "关闭";
            this.dmButton3.UseVisualStyleBackColor = false;
            this.dmButton3.Click += new System.EventHandler(this.dmButton3_Click_1);
            // 
            // label2_IP
            // 
            this.label2_IP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2_IP.Font = new System.Drawing.Font("微软雅黑", 25F, System.Drawing.FontStyle.Bold);
            this.label2_IP.ForeColor = System.Drawing.Color.Black;
            this.label2_IP.Location = new System.Drawing.Point(193, 105);
            this.label2_IP.Name = "label2_IP";
            this.label2_IP.Size = new System.Drawing.Size(666, 69);
            this.label2_IP.TabIndex = 34;
            this.label2_IP.Text = "IP";
            this.label2_IP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDJJ
            // 
            this.btnDJJ.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDJJ.AutoEllipsis = true;
            this.btnDJJ.BackColor = System.Drawing.Color.Transparent;
            this.btnDJJ.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDJJ.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnDJJ.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(124)))), ((int)(((byte)(16)))));
            this.btnDJJ.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(212)))), ((int)(((byte)(100)))));
            this.btnDJJ.DM_NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDJJ.DM_Radius = 8;
            this.btnDJJ.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnDJJ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDJJ.Font = new System.Drawing.Font("微软雅黑", 24F);
            this.btnDJJ.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnDJJ.Image = null;
            this.btnDJJ.Location = new System.Drawing.Point(89, 106);
            this.btnDJJ.Margin = new System.Windows.Forms.Padding(0);
            this.btnDJJ.Name = "btnDJJ";
            this.btnDJJ.Size = new System.Drawing.Size(203, 62);
            this.btnDJJ.TabIndex = 16;
            this.btnDJJ.Text = "单检机";
            this.btnDJJ.UseVisualStyleBackColor = true;
            this.btnDJJ.Click += new System.EventHandler(this.btnYk_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(1068, 649);
            this.Controls.Add(this.label2_IP);
            this.Controls.Add(this.dmButton3);
            this.Controls.Add(this.netStatusButton);
            this.Controls.Add(this.manualDownloadButton);
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "海澜之家系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblSayHello;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label1;
        private DMSkin.Controls.DMButton btnDeliver;
        private DMSkin.Controls.DMButton btnReciever;
        private DMSkin.Controls.DMButton manualDownloadButton;
        private DMSkin.Controls.DMButton netStatusButton;
        private System.Windows.Forms.Panel pnMain;
        private DMSkin.Controls.DMButton dmButton3;
        private DMSkin.Controls.DMButton dmButton9_Cancel;
        private System.Windows.Forms.Label label2_IP;
        private DMSkin.Controls.DMButton btnDJJ;
    }
}

