namespace HLAAutoDownload
{
    partial class DownloadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadForm));
            this.label7 = new System.Windows.Forms.Label();
            this.btnUploadDeliverEpc = new System.Windows.Forms.Button();
            this.btnDownloadShippingLabel = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDownloadEbBox = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metroTabControl1 = new DMSkin.Metro.Controls.MetroTabControl();
            this.metroTabPage1 = new DMSkin.Metro.Controls.MetroTabPage();
            this.EbXCheckBox = new System.Windows.Forms.CheckBox();
            this.pbReturnType = new DMSkin.Metro.Controls.MetroProgressBar();
            this.pgbDeliverEpc = new DMSkin.Metro.Controls.MetroProgressBar();
            this.pbEbBox = new DMSkin.Metro.Controls.MetroProgressBar();
            this.pbgShippingLabel = new DMSkin.Metro.Controls.MetroProgressBar();
            this.pbgOutlog = new DMSkin.Metro.Controls.MetroProgressBar();
            this.pgbEpcInfo = new DMSkin.Metro.Controls.MetroProgressBar();
            this.pgbTagInfo = new DMSkin.Metro.Controls.MetroProgressBar();
            this.pgbMaterialInfo = new DMSkin.Metro.Controls.MetroProgressBar();
            this.label8 = new System.Windows.Forms.Label();
            this.metroTabPage2 = new DMSkin.Metro.Controls.MetroTabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblSAP = new System.Windows.Forms.Label();
            this.btnReturnType = new System.Windows.Forms.Button();
            this.btnDownloadOutLog = new System.Windows.Forms.Button();
            this.btnTagInfo = new System.Windows.Forms.Button();
            this.btnDownloadMaterials = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label7.Location = new System.Drawing.Point(3, 264);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 21);
            this.label7.TabIndex = 52;
            this.label7.Text = "发运EPC上传";
            // 
            // btnUploadDeliverEpc
            // 
            this.btnUploadDeliverEpc.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnUploadDeliverEpc.FlatAppearance.BorderSize = 0;
            this.btnUploadDeliverEpc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadDeliverEpc.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnUploadDeliverEpc.ForeColor = System.Drawing.Color.White;
            this.btnUploadDeliverEpc.Location = new System.Drawing.Point(483, 64);
            this.btnUploadDeliverEpc.Name = "btnUploadDeliverEpc";
            this.btnUploadDeliverEpc.Size = new System.Drawing.Size(140, 40);
            this.btnUploadDeliverEpc.TabIndex = 51;
            this.btnUploadDeliverEpc.Text = "上传发运EPC";
            this.btnUploadDeliverEpc.UseVisualStyleBackColor = false;
            this.btnUploadDeliverEpc.Click += new System.EventHandler(this.btnUploadDeliverEpc_Click);
            // 
            // btnDownloadShippingLabel
            // 
            this.btnDownloadShippingLabel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDownloadShippingLabel.FlatAppearance.BorderSize = 0;
            this.btnDownloadShippingLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadShippingLabel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDownloadShippingLabel.ForeColor = System.Drawing.Color.White;
            this.btnDownloadShippingLabel.Location = new System.Drawing.Point(483, 110);
            this.btnDownloadShippingLabel.Name = "btnDownloadShippingLabel";
            this.btnDownloadShippingLabel.Size = new System.Drawing.Size(140, 40);
            this.btnDownloadShippingLabel.TabIndex = 48;
            this.btnDownloadShippingLabel.Text = "同步发运标签";
            this.btnDownloadShippingLabel.UseVisualStyleBackColor = false;
            this.btnDownloadShippingLabel.Click += new System.EventHandler(this.btnDownloadShippingInfo_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Tomato;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(483, 248);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(286, 76);
            this.btnCancel.TabIndex = 47;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDownloadEbBox
            // 
            this.btnDownloadEbBox.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDownloadEbBox.FlatAppearance.BorderSize = 0;
            this.btnDownloadEbBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadEbBox.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDownloadEbBox.ForeColor = System.Drawing.Color.White;
            this.btnDownloadEbBox.Location = new System.Drawing.Point(483, 156);
            this.btnDownloadEbBox.Name = "btnDownloadEbBox";
            this.btnDownloadEbBox.Size = new System.Drawing.Size(210, 40);
            this.btnDownloadEbBox.TabIndex = 46;
            this.btnDownloadEbBox.Text = "下载16#分拣复核装箱信息";
            this.btnDownloadEbBox.UseVisualStyleBackColor = false;
            this.btnDownloadEbBox.Click += new System.EventHandler(this.btnDownloadEbBox_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDownload.FlatAppearance.BorderSize = 0;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDownload.ForeColor = System.Drawing.Color.White;
            this.btnDownload.Location = new System.Drawing.Point(629, 110);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(140, 40);
            this.btnDownload.TabIndex = 46;
            this.btnDownload.Text = "上传EPC";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label6.Location = new System.Drawing.Point(12, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 21);
            this.label6.TabIndex = 42;
            this.label6.Text = "16#箱数据";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label5.Location = new System.Drawing.Point(18, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 21);
            this.label5.TabIndex = 42;
            this.label5.Text = "发运标签";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label4.Location = new System.Drawing.Point(26, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 21);
            this.label4.TabIndex = 42;
            this.label4.Text = "下架单";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label3.Location = new System.Drawing.Point(19, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 21);
            this.label3.TabIndex = 42;
            this.label3.Text = "EPC上传";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.Location = new System.Drawing.Point(18, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 41;
            this.label1.Text = "吊牌信息";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(10, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 40;
            this.label2.Text = "物料主数据";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "自动上传下载";
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
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.DM_FontSize = DMSkin.Metro.MetroTabControlSize.Tall;
            this.metroTabControl1.DM_UseSelectable = true;
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(10, 30);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(787, 380);
            this.metroTabControl1.TabIndex = 45;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.BackColor = System.Drawing.Color.White;
            this.metroTabPage1.Controls.Add(this.EbXCheckBox);
            this.metroTabPage1.Controls.Add(this.pbReturnType);
            this.metroTabPage1.Controls.Add(this.pgbDeliverEpc);
            this.metroTabPage1.Controls.Add(this.pbEbBox);
            this.metroTabPage1.Controls.Add(this.pbgShippingLabel);
            this.metroTabPage1.Controls.Add(this.pbgOutlog);
            this.metroTabPage1.Controls.Add(this.pgbEpcInfo);
            this.metroTabPage1.Controls.Add(this.pgbTagInfo);
            this.metroTabPage1.Controls.Add(this.pgbMaterialInfo);
            this.metroTabPage1.Controls.Add(this.btnUploadDeliverEpc);
            this.metroTabPage1.Controls.Add(this.btnReturnType);
            this.metroTabPage1.Controls.Add(this.btnDownloadEbBox);
            this.metroTabPage1.Controls.Add(this.btnDownloadMaterials);
            this.metroTabPage1.Controls.Add(this.btnTagInfo);
            this.metroTabPage1.Controls.Add(this.label8);
            this.metroTabPage1.Controls.Add(this.btnDownloadShippingLabel);
            this.metroTabPage1.Controls.Add(this.label7);
            this.metroTabPage1.Controls.Add(this.btnCancel);
            this.metroTabPage1.Controls.Add(this.btnDownloadOutLog);
            this.metroTabPage1.Controls.Add(this.btnDownload);
            this.metroTabPage1.Controls.Add(this.label2);
            this.metroTabPage1.Controls.Add(this.label1);
            this.metroTabPage1.Controls.Add(this.label3);
            this.metroTabPage1.Controls.Add(this.label4);
            this.metroTabPage1.Controls.Add(this.label5);
            this.metroTabPage1.Controls.Add(this.label6);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarDM_HighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 43);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(779, 333);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "数据服务";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarDM_HighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // EbXCheckBox
            // 
            this.EbXCheckBox.AutoSize = true;
            this.EbXCheckBox.Location = new System.Drawing.Point(721, 168);
            this.EbXCheckBox.Name = "EbXCheckBox";
            this.EbXCheckBox.Size = new System.Drawing.Size(48, 16);
            this.EbXCheckBox.TabIndex = 55;
            this.EbXCheckBox.Text = "强制";
            this.EbXCheckBox.UseVisualStyleBackColor = true;
            // 
            // pbReturnType
            // 
            this.pbReturnType.Location = new System.Drawing.Point(116, 301);
            this.pbReturnType.Name = "pbReturnType";
            this.pbReturnType.Size = new System.Drawing.Size(342, 23);
            this.pbReturnType.TabIndex = 54;
            // 
            // pgbDeliverEpc
            // 
            this.pgbDeliverEpc.Location = new System.Drawing.Point(116, 264);
            this.pgbDeliverEpc.Name = "pgbDeliverEpc";
            this.pgbDeliverEpc.Size = new System.Drawing.Size(342, 23);
            this.pgbDeliverEpc.TabIndex = 54;
            // 
            // pbEbBox
            // 
            this.pbEbBox.Location = new System.Drawing.Point(116, 222);
            this.pbEbBox.Name = "pbEbBox";
            this.pbEbBox.Size = new System.Drawing.Size(342, 23);
            this.pbEbBox.TabIndex = 54;
            // 
            // pbgShippingLabel
            // 
            this.pbgShippingLabel.Location = new System.Drawing.Point(116, 182);
            this.pbgShippingLabel.Name = "pbgShippingLabel";
            this.pbgShippingLabel.Size = new System.Drawing.Size(342, 23);
            this.pbgShippingLabel.TabIndex = 54;
            // 
            // pbgOutlog
            // 
            this.pbgOutlog.Location = new System.Drawing.Point(116, 141);
            this.pbgOutlog.Name = "pbgOutlog";
            this.pbgOutlog.Size = new System.Drawing.Size(342, 23);
            this.pbgOutlog.TabIndex = 54;
            // 
            // pgbEpcInfo
            // 
            this.pgbEpcInfo.Location = new System.Drawing.Point(116, 100);
            this.pgbEpcInfo.Name = "pgbEpcInfo";
            this.pgbEpcInfo.Size = new System.Drawing.Size(342, 23);
            this.pgbEpcInfo.TabIndex = 54;
            // 
            // pgbTagInfo
            // 
            this.pgbTagInfo.Location = new System.Drawing.Point(116, 59);
            this.pgbTagInfo.Name = "pgbTagInfo";
            this.pgbTagInfo.Size = new System.Drawing.Size(342, 23);
            this.pgbTagInfo.TabIndex = 54;
            // 
            // pgbMaterialInfo
            // 
            this.pgbMaterialInfo.Location = new System.Drawing.Point(116, 20);
            this.pgbMaterialInfo.Name = "pgbMaterialInfo";
            this.pgbMaterialInfo.Size = new System.Drawing.Size(342, 23);
            this.pgbMaterialInfo.TabIndex = 54;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label8.Location = new System.Drawing.Point(18, 303);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 21);
            this.label8.TabIndex = 52;
            this.label8.Text = "退货类型";
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.txtLog);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarDM_HighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 43);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(779, 333);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "服务日志";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarDM_HighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(779, 333);
            this.txtLog.TabIndex = 2;
            // 
            // lblSAP
            // 
            this.lblSAP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSAP.BackColor = System.Drawing.Color.White;
            this.lblSAP.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblSAP.Location = new System.Drawing.Point(271, 42);
            this.lblSAP.Name = "lblSAP";
            this.lblSAP.Size = new System.Drawing.Size(519, 21);
            this.lblSAP.TabIndex = 52;
            this.lblSAP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnReturnType
            // 
            this.btnReturnType.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnReturnType.FlatAppearance.BorderSize = 0;
            this.btnReturnType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturnType.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnReturnType.ForeColor = System.Drawing.Color.White;
            this.btnReturnType.Location = new System.Drawing.Point(483, 202);
            this.btnReturnType.Name = "btnReturnType";
            this.btnReturnType.Size = new System.Drawing.Size(286, 40);
            this.btnReturnType.TabIndex = 46;
            this.btnReturnType.Text = "下载整理库退货类型";
            this.btnReturnType.UseVisualStyleBackColor = false;
            this.btnReturnType.Click += new System.EventHandler(this.btnReturnType_Click);
            // 
            // btnDownloadOutLog
            // 
            this.btnDownloadOutLog.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDownloadOutLog.FlatAppearance.BorderSize = 0;
            this.btnDownloadOutLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadOutLog.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDownloadOutLog.ForeColor = System.Drawing.Color.White;
            this.btnDownloadOutLog.Location = new System.Drawing.Point(629, 64);
            this.btnDownloadOutLog.Name = "btnDownloadOutLog";
            this.btnDownloadOutLog.Size = new System.Drawing.Size(140, 40);
            this.btnDownloadOutLog.TabIndex = 46;
            this.btnDownloadOutLog.Text = "同步下架单";
            this.btnDownloadOutLog.UseVisualStyleBackColor = false;
            this.btnDownloadOutLog.Click += new System.EventHandler(this.btnDownloadOutLog_Click);
            // 
            // btnTagInfo
            // 
            this.btnTagInfo.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnTagInfo.FlatAppearance.BorderSize = 0;
            this.btnTagInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTagInfo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnTagInfo.ForeColor = System.Drawing.Color.White;
            this.btnTagInfo.Location = new System.Drawing.Point(629, 18);
            this.btnTagInfo.Name = "btnTagInfo";
            this.btnTagInfo.Size = new System.Drawing.Size(140, 40);
            this.btnTagInfo.TabIndex = 49;
            this.btnTagInfo.Text = "下载吊牌";
            this.btnTagInfo.UseVisualStyleBackColor = false;
            this.btnTagInfo.Click += new System.EventHandler(this.btnTagInfo_Click);
            // 
            // btnDownloadMaterials
            // 
            this.btnDownloadMaterials.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDownloadMaterials.FlatAppearance.BorderSize = 0;
            this.btnDownloadMaterials.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadMaterials.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDownloadMaterials.ForeColor = System.Drawing.Color.White;
            this.btnDownloadMaterials.Location = new System.Drawing.Point(483, 18);
            this.btnDownloadMaterials.Name = "btnDownloadMaterials";
            this.btnDownloadMaterials.Size = new System.Drawing.Size(140, 40);
            this.btnDownloadMaterials.TabIndex = 50;
            this.btnDownloadMaterials.Text = "下载物料";
            this.btnDownloadMaterials.UseVisualStyleBackColor = false;
            this.btnDownloadMaterials.Click += new System.EventHandler(this.btnDownloadMaterials_Click);
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 420);
            this.Controls.Add(this.lblSAP);
            this.Controls.Add(this.metroTabControl1);
            this.DisplayHeader = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadForm";
            this.Padding = new System.Windows.Forms.Padding(10, 30, 10, 10);
            this.ShowInTaskbar = false;
            this.Text = "上传下载";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadForm_FormClosing);
            this.Load += new System.EventHandler(this.DownloadForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.Button btnDownloadShippingLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDownloadEbBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnUploadDeliverEpc;
        private System.Windows.Forms.Label label7;
        private DMSkin.Metro.Controls.MetroTabControl metroTabControl1;
        private DMSkin.Metro.Controls.MetroTabPage metroTabPage1;
        private DMSkin.Metro.Controls.MetroTabPage metroTabPage2;
        private System.Windows.Forms.Label lblSAP;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtLog;
        private DMSkin.Metro.Controls.MetroProgressBar pbReturnType;
        private DMSkin.Metro.Controls.MetroProgressBar pgbDeliverEpc;
        private DMSkin.Metro.Controls.MetroProgressBar pbEbBox;
        private DMSkin.Metro.Controls.MetroProgressBar pbgShippingLabel;
        private DMSkin.Metro.Controls.MetroProgressBar pbgOutlog;
        private DMSkin.Metro.Controls.MetroProgressBar pgbEpcInfo;
        private DMSkin.Metro.Controls.MetroProgressBar pgbTagInfo;
        private DMSkin.Metro.Controls.MetroProgressBar pgbMaterialInfo;
        private System.Windows.Forms.CheckBox EbXCheckBox;
        private System.Windows.Forms.Button btnReturnType;
        private System.Windows.Forms.Button btnDownloadMaterials;
        private System.Windows.Forms.Button btnTagInfo;
        private System.Windows.Forms.Button btnDownloadOutLog;
    }
}