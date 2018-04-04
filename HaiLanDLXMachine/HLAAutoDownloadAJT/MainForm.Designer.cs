namespace HLAManualDownload
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpDate = new DMSkin.Metro.Controls.MetroDateTime();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pbgShippingLabel = new DMSkin.Metro.Controls.MetroProgressBar();
            this.pbgOutlog = new DMSkin.Metro.Controls.MetroProgressBar();
            this.btnDownloadShippingLabel = new System.Windows.Forms.Button();
            this.btnDownloadOutLog = new System.Windows.Forms.Button();
            this.downloadTabControl = new DMSkin.Metro.Controls.MetroTabControl();
            this.metroTabPage3 = new DMSkin.Metro.Controls.MetroTabPage();
            this.pgbDeliverEpc = new DMSkin.Metro.Controls.MetroProgressBar();
            this.btnUploadDeliverEpc = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.pgbTagInfo = new DMSkin.Metro.Controls.MetroProgressBar();
            this.pgbMaterialInfo = new DMSkin.Metro.Controls.MetroProgressBar();
            this.btnDownloadMaterials = new System.Windows.Forms.Button();
            this.btnTagInfo = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pgbEpcInfo = new DMSkin.Metro.Controls.MetroProgressBar();
            this.label8 = new System.Windows.Forms.Label();
            this.button1_UPLOADEPC = new System.Windows.Forms.Button();
            this.metroTabPage4_log = new DMSkin.Metro.Controls.MetroTabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.metroTabPage2 = new DMSkin.Metro.Controls.MetroTabPage();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.downloadTabControl.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            this.metroTabPage4_log.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(723, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(557, 263);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(153, 41);
            this.dtpDate.MinimumSize = new System.Drawing.Size(4, 30);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(341, 30);
            this.dtpDate.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.Location = new System.Drawing.Point(60, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 52;
            this.label1.Text = "选择日期";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Tomato;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(354, 187);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(141, 40);
            this.btnCancel.TabIndex = 47;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label5.Location = new System.Drawing.Point(60, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 21);
            this.label5.TabIndex = 42;
            this.label5.Text = "发运标签";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label4.Location = new System.Drawing.Point(60, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 21);
            this.label4.TabIndex = 42;
            this.label4.Text = "下架单";
            // 
            // pbgShippingLabel
            // 
            this.pbgShippingLabel.Location = new System.Drawing.Point(106, 261);
            this.pbgShippingLabel.Name = "pbgShippingLabel";
            this.pbgShippingLabel.Size = new System.Drawing.Size(342, 25);
            this.pbgShippingLabel.Style = DMSkin.Metro.MetroColorStyle.Blue;
            this.pbgShippingLabel.TabIndex = 54;
            // 
            // pbgOutlog
            // 
            this.pbgOutlog.Location = new System.Drawing.Point(106, 196);
            this.pbgOutlog.Name = "pbgOutlog";
            this.pbgOutlog.Size = new System.Drawing.Size(342, 26);
            this.pbgOutlog.TabIndex = 54;
            // 
            // btnDownloadShippingLabel
            // 
            this.btnDownloadShippingLabel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDownloadShippingLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadShippingLabel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDownloadShippingLabel.ForeColor = System.Drawing.Color.White;
            this.btnDownloadShippingLabel.Location = new System.Drawing.Point(472, 251);
            this.btnDownloadShippingLabel.Name = "btnDownloadShippingLabel";
            this.btnDownloadShippingLabel.Size = new System.Drawing.Size(140, 44);
            this.btnDownloadShippingLabel.TabIndex = 48;
            this.btnDownloadShippingLabel.Text = "同步发运标签";
            this.btnDownloadShippingLabel.UseVisualStyleBackColor = false;
            this.btnDownloadShippingLabel.Click += new System.EventHandler(this.btnDownloadShippingLabel_Click);
            // 
            // btnDownloadOutLog
            // 
            this.btnDownloadOutLog.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDownloadOutLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadOutLog.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDownloadOutLog.ForeColor = System.Drawing.Color.White;
            this.btnDownloadOutLog.Location = new System.Drawing.Point(473, 185);
            this.btnDownloadOutLog.Name = "btnDownloadOutLog";
            this.btnDownloadOutLog.Size = new System.Drawing.Size(139, 44);
            this.btnDownloadOutLog.TabIndex = 46;
            this.btnDownloadOutLog.Text = "同步下架单";
            this.btnDownloadOutLog.UseVisualStyleBackColor = false;
            this.btnDownloadOutLog.Click += new System.EventHandler(this.btnDownloadOutLog_Click);
            // 
            // downloadTabControl
            // 
            this.downloadTabControl.Controls.Add(this.metroTabPage3);
            this.downloadTabControl.Controls.Add(this.metroTabPage4_log);
            this.downloadTabControl.Controls.Add(this.metroTabPage2);
            this.downloadTabControl.DM_UseSelectable = true;
            this.downloadTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downloadTabControl.Location = new System.Drawing.Point(20, 30);
            this.downloadTabControl.Name = "downloadTabControl";
            this.downloadTabControl.SelectedIndex = 0;
            this.downloadTabControl.Size = new System.Drawing.Size(1240, 680);
            this.downloadTabControl.TabIndex = 2;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.pgbDeliverEpc);
            this.metroTabPage3.Controls.Add(this.btnUploadDeliverEpc);
            this.metroTabPage3.Controls.Add(this.label7);
            this.metroTabPage3.Controls.Add(this.pbgShippingLabel);
            this.metroTabPage3.Controls.Add(this.pbgOutlog);
            this.metroTabPage3.Controls.Add(this.btnDownloadShippingLabel);
            this.metroTabPage3.Controls.Add(this.btnDownloadOutLog);
            this.metroTabPage3.Controls.Add(this.pgbTagInfo);
            this.metroTabPage3.Controls.Add(this.pgbMaterialInfo);
            this.metroTabPage3.Controls.Add(this.btnDownloadMaterials);
            this.metroTabPage3.Controls.Add(this.btnTagInfo);
            this.metroTabPage3.Controls.Add(this.label10);
            this.metroTabPage3.Controls.Add(this.label11);
            this.metroTabPage3.Controls.Add(this.pgbEpcInfo);
            this.metroTabPage3.Controls.Add(this.label8);
            this.metroTabPage3.Controls.Add(this.button1_UPLOADEPC);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarDM_HighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 10;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 39);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(1232, 637);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "自动";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarDM_HighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 10;
            // 
            // pgbDeliverEpc
            // 
            this.pgbDeliverEpc.Location = new System.Drawing.Point(106, 322);
            this.pgbDeliverEpc.Name = "pgbDeliverEpc";
            this.pgbDeliverEpc.Size = new System.Drawing.Size(342, 23);
            this.pgbDeliverEpc.TabIndex = 65;
            // 
            // btnUploadDeliverEpc
            // 
            this.btnUploadDeliverEpc.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnUploadDeliverEpc.FlatAppearance.BorderSize = 0;
            this.btnUploadDeliverEpc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadDeliverEpc.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnUploadDeliverEpc.ForeColor = System.Drawing.Color.White;
            this.btnUploadDeliverEpc.Location = new System.Drawing.Point(472, 312);
            this.btnUploadDeliverEpc.Name = "btnUploadDeliverEpc";
            this.btnUploadDeliverEpc.Size = new System.Drawing.Size(140, 40);
            this.btnUploadDeliverEpc.TabIndex = 63;
            this.btnUploadDeliverEpc.Text = "上传发运EPC";
            this.btnUploadDeliverEpc.UseVisualStyleBackColor = false;
            this.btnUploadDeliverEpc.Click += new System.EventHandler(this.btnUploadDeliverEpc_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label7.Location = new System.Drawing.Point(-7, 322);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 21);
            this.label7.TabIndex = 64;
            this.label7.Text = "发运EPC上传";
            // 
            // pgbTagInfo
            // 
            this.pgbTagInfo.Location = new System.Drawing.Point(106, 128);
            this.pgbTagInfo.Name = "pgbTagInfo";
            this.pgbTagInfo.Size = new System.Drawing.Size(342, 23);
            this.pgbTagInfo.TabIndex = 61;
            // 
            // pgbMaterialInfo
            // 
            this.pgbMaterialInfo.Location = new System.Drawing.Point(106, 75);
            this.pgbMaterialInfo.Name = "pgbMaterialInfo";
            this.pgbMaterialInfo.Size = new System.Drawing.Size(342, 23);
            this.pgbMaterialInfo.TabIndex = 62;
            // 
            // btnDownloadMaterials
            // 
            this.btnDownloadMaterials.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDownloadMaterials.FlatAppearance.BorderSize = 0;
            this.btnDownloadMaterials.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadMaterials.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDownloadMaterials.ForeColor = System.Drawing.Color.White;
            this.btnDownloadMaterials.Location = new System.Drawing.Point(473, 65);
            this.btnDownloadMaterials.Name = "btnDownloadMaterials";
            this.btnDownloadMaterials.Size = new System.Drawing.Size(140, 40);
            this.btnDownloadMaterials.TabIndex = 60;
            this.btnDownloadMaterials.Text = "下载物料";
            this.btnDownloadMaterials.UseVisualStyleBackColor = false;
            this.btnDownloadMaterials.Click += new System.EventHandler(this.btnDownloadMaterials_Click);
            // 
            // btnTagInfo
            // 
            this.btnTagInfo.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnTagInfo.FlatAppearance.BorderSize = 0;
            this.btnTagInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTagInfo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnTagInfo.ForeColor = System.Drawing.Color.White;
            this.btnTagInfo.Location = new System.Drawing.Point(473, 120);
            this.btnTagInfo.Name = "btnTagInfo";
            this.btnTagInfo.Size = new System.Drawing.Size(140, 40);
            this.btnTagInfo.TabIndex = 59;
            this.btnTagInfo.Text = "下载吊牌";
            this.btnTagInfo.UseVisualStyleBackColor = false;
            this.btnTagInfo.Click += new System.EventHandler(this.btnTagInfo_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label10.Location = new System.Drawing.Point(0, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 21);
            this.label10.TabIndex = 57;
            this.label10.Text = "物料主数据";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label11.Location = new System.Drawing.Point(8, 130);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 21);
            this.label11.TabIndex = 58;
            this.label11.Text = "吊牌信息";
            // 
            // pgbEpcInfo
            // 
            this.pgbEpcInfo.Location = new System.Drawing.Point(106, 26);
            this.pgbEpcInfo.Name = "pgbEpcInfo";
            this.pgbEpcInfo.Size = new System.Drawing.Size(342, 23);
            this.pgbEpcInfo.TabIndex = 56;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label8.Location = new System.Drawing.Point(9, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 21);
            this.label8.TabIndex = 55;
            this.label8.Text = "EPC上传";
            // 
            // button1_UPLOADEPC
            // 
            this.button1_UPLOADEPC.Location = new System.Drawing.Point(494, 26);
            this.button1_UPLOADEPC.Name = "button1_UPLOADEPC";
            this.button1_UPLOADEPC.Size = new System.Drawing.Size(75, 23);
            this.button1_UPLOADEPC.TabIndex = 2;
            this.button1_UPLOADEPC.Text = "上传EPC";
            this.button1_UPLOADEPC.UseVisualStyleBackColor = true;
            this.button1_UPLOADEPC.Click += new System.EventHandler(this.button1_UPLOADEPC_Click);
            // 
            // metroTabPage4_log
            // 
            this.metroTabPage4_log.Controls.Add(this.txtLog);
            this.metroTabPage4_log.HorizontalScrollbarBarColor = true;
            this.metroTabPage4_log.HorizontalScrollbarDM_HighlightOnWheel = false;
            this.metroTabPage4_log.HorizontalScrollbarSize = 10;
            this.metroTabPage4_log.Location = new System.Drawing.Point(4, 39);
            this.metroTabPage4_log.Name = "metroTabPage4_log";
            this.metroTabPage4_log.Size = new System.Drawing.Size(1232, 637);
            this.metroTabPage4_log.TabIndex = 3;
            this.metroTabPage4_log.Text = "日志";
            this.metroTabPage4_log.VerticalScrollbarBarColor = true;
            this.metroTabPage4_log.VerticalScrollbarDM_HighlightOnWheel = false;
            this.metroTabPage4_log.VerticalScrollbarSize = 10;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(1232, 637);
            this.txtLog.TabIndex = 2;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarDM_HighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 39);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(1232, 637);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "配置";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarDM_HighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 730);
            this.Controls.Add(this.downloadTabControl);
            this.Controls.Add(this.groupBox1);
            this.DisplayHeader = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Text = "爱居兔自动下载";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.downloadTabControl.ResumeLayout(false);
            this.metroTabPage3.ResumeLayout(false);
            this.metroTabPage3.PerformLayout();
            this.metroTabPage4_log.ResumeLayout(false);
            this.metroTabPage4_log.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDownloadShippingLabel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDownloadOutLog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private DMSkin.Metro.Controls.MetroDateTime dtpDate;
        private DMSkin.Metro.Controls.MetroProgressBar pbgShippingLabel;
        private DMSkin.Metro.Controls.MetroProgressBar pbgOutlog;
        private DMSkin.Metro.Controls.MetroTabControl downloadTabControl;
        private DMSkin.Metro.Controls.MetroTabPage metroTabPage2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DMSkin.Metro.Controls.MetroTabPage metroTabPage3;
        private System.Windows.Forms.Button button1_UPLOADEPC;
        private DMSkin.Metro.Controls.MetroProgressBar pgbEpcInfo;
        private System.Windows.Forms.Label label8;
        private DMSkin.Metro.Controls.MetroProgressBar pgbTagInfo;
        private DMSkin.Metro.Controls.MetroProgressBar pgbMaterialInfo;
        private System.Windows.Forms.Button btnDownloadMaterials;
        private System.Windows.Forms.Button btnTagInfo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private DMSkin.Metro.Controls.MetroTabPage metroTabPage4_log;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private DMSkin.Metro.Controls.MetroProgressBar pgbDeliverEpc;
        private System.Windows.Forms.Button btnUploadDeliverEpc;
        private System.Windows.Forms.Label label7;
    }
}

