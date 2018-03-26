﻿namespace HLAManualDownload
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpDate = new DMSkin.Metro.Controls.MetroDateTime();
            this.pbgShippingLabel = new DMSkin.Metro.Controls.MetroProgressBar();
            this.pbgOutlog = new DMSkin.Metro.Controls.MetroProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDownloadOutLog = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.downloadTabControl = new DMSkin.Metro.Controls.MetroTabControl();
            this.metroTabPage1 = new DMSkin.Metro.Controls.MetroTabPage();
            this.grouper4 = new CodeVendor.Controls.Grouper();
            this.button3 = new System.Windows.Forms.Button();
            this.returnTypeLabel = new System.Windows.Forms.Label();
            this.returnTypeProgressBar = new DMSkin.Metro.Controls.MetroProgressBar();
            this.returnTypeutton = new System.Windows.Forms.Button();
            this.returnTypeCheckBox = new DMSkin.Controls.DMCheckBox();
            this.grouper3 = new CodeVendor.Controls.Grouper();
            this.dmCheckBox1_resetIfExist = new DMSkin.Controls.DMCheckBox();
            this.inventoryLogLabel = new System.Windows.Forms.Label();
            this.inventoryStoreTextBox = new DMSkin.Metro.Controls.MetroTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.inventoryProgressBar = new DMSkin.Metro.Controls.MetroProgressBar();
            this.inventoryButton = new System.Windows.Forms.Button();
            this.inventoryDateTime = new DMSkin.Metro.Controls.MetroDateTime();
            this.label2 = new System.Windows.Forms.Label();
            this.grouper2 = new CodeVendor.Controls.Grouper();
            this.shipLogLabel = new System.Windows.Forms.Label();
            this.shipProgressBar = new DMSkin.Metro.Controls.MetroProgressBar();
            this.shipButton = new System.Windows.Forms.Button();
            this.shipDateTime = new DMSkin.Metro.Controls.MetroDateTime();
            this.label9 = new System.Windows.Forms.Label();
            this.grouper1 = new CodeVendor.Controls.Grouper();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dateMatTagButton = new System.Windows.Forms.Button();
            this.eDateTime = new DMSkin.Metro.Controls.MetroDateTime();
            this.sDateTime = new DMSkin.Metro.Controls.MetroDateTime();
            this.matLogLabel = new System.Windows.Forms.Label();
            this.matProgressBar = new DMSkin.Metro.Controls.MetroProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.downloadTabControl.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.grouper4.SuspendLayout();
            this.grouper3.SuspendLayout();
            this.grouper2.SuspendLayout();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.pbgShippingLabel);
            this.groupBox1.Controls.Add(this.pbgOutlog);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnDownloadOutLog);
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
            // pbgShippingLabel
            // 
            this.pbgShippingLabel.Location = new System.Drawing.Point(153, 125);
            this.pbgShippingLabel.Name = "pbgShippingLabel";
            this.pbgShippingLabel.Size = new System.Drawing.Size(342, 30);
            this.pbgShippingLabel.Style = DMSkin.Metro.MetroColorStyle.Blue;
            this.pbgShippingLabel.TabIndex = 54;
            // 
            // pbgOutlog
            // 
            this.pbgOutlog.Location = new System.Drawing.Point(153, 83);
            this.pbgOutlog.Name = "pbgOutlog";
            this.pbgOutlog.Size = new System.Drawing.Size(342, 30);
            this.pbgOutlog.TabIndex = 54;
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
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDownloadOutLog
            // 
            this.btnDownloadOutLog.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDownloadOutLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadOutLog.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDownloadOutLog.ForeColor = System.Drawing.Color.White;
            this.btnDownloadOutLog.Location = new System.Drawing.Point(64, 187);
            this.btnDownloadOutLog.Name = "btnDownloadOutLog";
            this.btnDownloadOutLog.Size = new System.Drawing.Size(139, 40);
            this.btnDownloadOutLog.TabIndex = 46;
            this.btnDownloadOutLog.Text = "同步下架单";
            this.btnDownloadOutLog.UseVisualStyleBackColor = false;
            this.btnDownloadOutLog.Click += new System.EventHandler(this.btnDownloadOutLog_Click);
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
            // downloadTabControl
            // 
            this.downloadTabControl.Controls.Add(this.metroTabPage1);
            this.downloadTabControl.DM_UseSelectable = true;
            this.downloadTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downloadTabControl.Location = new System.Drawing.Point(20, 30);
            this.downloadTabControl.Name = "downloadTabControl";
            this.downloadTabControl.SelectedIndex = 0;
            this.downloadTabControl.Size = new System.Drawing.Size(1240, 680);
            this.downloadTabControl.TabIndex = 2;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.grouper4);
            this.metroTabPage1.Controls.Add(this.grouper3);
            this.metroTabPage1.Controls.Add(this.grouper2);
            this.metroTabPage1.Controls.Add(this.grouper1);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarDM_HighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 39);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(1232, 637);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "手动下载";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarDM_HighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // grouper4
            // 
            this.grouper4.BackgroundColor = System.Drawing.Color.White;
            this.grouper4.BackgroundGradientColor = System.Drawing.Color.SteelBlue;
            this.grouper4.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouper4.BorderColor = System.Drawing.Color.SkyBlue;
            this.grouper4.BorderThickness = 1F;
            this.grouper4.Controls.Add(this.button3);
            this.grouper4.Controls.Add(this.returnTypeLabel);
            this.grouper4.Controls.Add(this.returnTypeProgressBar);
            this.grouper4.Controls.Add(this.returnTypeutton);
            this.grouper4.Controls.Add(this.returnTypeCheckBox);
            this.grouper4.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper4.GroupImage = null;
            this.grouper4.GroupTitle = "退货类型下载";
            this.grouper4.Location = new System.Drawing.Point(622, 343);
            this.grouper4.Name = "grouper4";
            this.grouper4.Padding = new System.Windows.Forms.Padding(20);
            this.grouper4.PaintGroupBox = false;
            this.grouper4.RoundCorners = 10;
            this.grouper4.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper4.ShadowControl = false;
            this.grouper4.ShadowThickness = 3;
            this.grouper4.Size = new System.Drawing.Size(610, 149);
            this.grouper4.TabIndex = 5;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DodgerBlue;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(341, 21);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(108, 40);
            this.button3.TabIndex = 62;
            this.button3.Text = "全部清空";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // returnTypeLabel
            // 
            this.returnTypeLabel.BackColor = System.Drawing.Color.White;
            this.returnTypeLabel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.returnTypeLabel.Location = new System.Drawing.Point(23, 113);
            this.returnTypeLabel.Name = "returnTypeLabel";
            this.returnTypeLabel.Size = new System.Drawing.Size(573, 21);
            this.returnTypeLabel.TabIndex = 61;
            this.returnTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // returnTypeProgressBar
            // 
            this.returnTypeProgressBar.Location = new System.Drawing.Point(23, 68);
            this.returnTypeProgressBar.Name = "returnTypeProgressBar";
            this.returnTypeProgressBar.Size = new System.Drawing.Size(573, 30);
            this.returnTypeProgressBar.TabIndex = 61;
            // 
            // returnTypeutton
            // 
            this.returnTypeutton.BackColor = System.Drawing.Color.DodgerBlue;
            this.returnTypeutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.returnTypeutton.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.returnTypeutton.ForeColor = System.Drawing.Color.White;
            this.returnTypeutton.Location = new System.Drawing.Point(488, 21);
            this.returnTypeutton.Name = "returnTypeutton";
            this.returnTypeutton.Size = new System.Drawing.Size(108, 40);
            this.returnTypeutton.TabIndex = 61;
            this.returnTypeutton.Text = "开始下载";
            this.returnTypeutton.UseVisualStyleBackColor = false;
            this.returnTypeutton.Click += new System.EventHandler(this.returnTypeutton_Click);
            // 
            // returnTypeCheckBox
            // 
            this.returnTypeCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.returnTypeCheckBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.returnTypeCheckBox.Checked = false;
            this.returnTypeCheckBox.Location = new System.Drawing.Point(23, 35);
            this.returnTypeCheckBox.Name = "returnTypeCheckBox";
            this.returnTypeCheckBox.Size = new System.Drawing.Size(102, 17);
            this.returnTypeCheckBox.TabIndex = 0;
            this.returnTypeCheckBox.Text = "是否全量下载";
            // 
            // grouper3
            // 
            this.grouper3.BackgroundColor = System.Drawing.Color.White;
            this.grouper3.BackgroundGradientColor = System.Drawing.Color.SteelBlue;
            this.grouper3.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouper3.BorderColor = System.Drawing.Color.SkyBlue;
            this.grouper3.BorderThickness = 1F;
            this.grouper3.Controls.Add(this.dmCheckBox1_resetIfExist);
            this.grouper3.Controls.Add(this.inventoryLogLabel);
            this.grouper3.Controls.Add(this.inventoryStoreTextBox);
            this.grouper3.Controls.Add(this.label3);
            this.grouper3.Controls.Add(this.inventoryProgressBar);
            this.grouper3.Controls.Add(this.inventoryButton);
            this.grouper3.Controls.Add(this.inventoryDateTime);
            this.grouper3.Controls.Add(this.label2);
            this.grouper3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper3.GroupImage = null;
            this.grouper3.GroupTitle = "下架单数据下载";
            this.grouper3.Location = new System.Drawing.Point(0, 492);
            this.grouper3.Name = "grouper3";
            this.grouper3.Padding = new System.Windows.Forms.Padding(20);
            this.grouper3.PaintGroupBox = false;
            this.grouper3.RoundCorners = 10;
            this.grouper3.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper3.ShadowControl = false;
            this.grouper3.ShadowThickness = 3;
            this.grouper3.Size = new System.Drawing.Size(1232, 145);
            this.grouper3.TabIndex = 4;
            // 
            // dmCheckBox1_resetIfExist
            // 
            this.dmCheckBox1_resetIfExist.BackColor = System.Drawing.Color.Transparent;
            this.dmCheckBox1_resetIfExist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.dmCheckBox1_resetIfExist.Checked = false;
            this.dmCheckBox1_resetIfExist.Location = new System.Drawing.Point(642, 35);
            this.dmCheckBox1_resetIfExist.Name = "dmCheckBox1_resetIfExist";
            this.dmCheckBox1_resetIfExist.Size = new System.Drawing.Size(155, 17);
            this.dmCheckBox1_resetIfExist.TabIndex = 1;
            this.dmCheckBox1_resetIfExist.Text = "已有的下架单是否重置";
            // 
            // inventoryLogLabel
            // 
            this.inventoryLogLabel.BackColor = System.Drawing.Color.White;
            this.inventoryLogLabel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.inventoryLogLabel.Location = new System.Drawing.Point(23, 113);
            this.inventoryLogLabel.Name = "inventoryLogLabel";
            this.inventoryLogLabel.Size = new System.Drawing.Size(640, 21);
            this.inventoryLogLabel.TabIndex = 65;
            this.inventoryLogLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // inventoryStoreTextBox
            // 
            this.inventoryStoreTextBox.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Medium;
            this.inventoryStoreTextBox.DM_UseSelectable = true;
            this.inventoryStoreTextBox.Lines = new string[0];
            this.inventoryStoreTextBox.Location = new System.Drawing.Point(383, 27);
            this.inventoryStoreTextBox.MaxLength = 32767;
            this.inventoryStoreTextBox.Name = "inventoryStoreTextBox";
            this.inventoryStoreTextBox.PasswordChar = '\0';
            this.inventoryStoreTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.inventoryStoreTextBox.SelectedText = "";
            this.inventoryStoreTextBox.Size = new System.Drawing.Size(92, 30);
            this.inventoryStoreTextBox.TabIndex = 64;
            this.inventoryStoreTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label3.Location = new System.Drawing.Point(280, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 21);
            this.label3.TabIndex = 63;
            this.label3.Text = "存储类型：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // inventoryProgressBar
            // 
            this.inventoryProgressBar.Location = new System.Drawing.Point(23, 71);
            this.inventoryProgressBar.Name = "inventoryProgressBar";
            this.inventoryProgressBar.Size = new System.Drawing.Size(1206, 30);
            this.inventoryProgressBar.TabIndex = 62;
            // 
            // inventoryButton
            // 
            this.inventoryButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.inventoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inventoryButton.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.inventoryButton.ForeColor = System.Drawing.Color.White;
            this.inventoryButton.Location = new System.Drawing.Point(491, 23);
            this.inventoryButton.Name = "inventoryButton";
            this.inventoryButton.Size = new System.Drawing.Size(108, 40);
            this.inventoryButton.TabIndex = 61;
            this.inventoryButton.Text = "开始下载";
            this.inventoryButton.UseVisualStyleBackColor = false;
            this.inventoryButton.Click += new System.EventHandler(this.inventoryButton_Click);
            // 
            // inventoryDateTime
            // 
            this.inventoryDateTime.Location = new System.Drawing.Point(127, 27);
            this.inventoryDateTime.MinimumSize = new System.Drawing.Size(0, 30);
            this.inventoryDateTime.Name = "inventoryDateTime";
            this.inventoryDateTime.Size = new System.Drawing.Size(132, 30);
            this.inventoryDateTime.TabIndex = 60;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(29, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 21);
            this.label2.TabIndex = 59;
            this.label2.Text = "发运日期：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grouper2
            // 
            this.grouper2.BackgroundColor = System.Drawing.Color.White;
            this.grouper2.BackgroundGradientColor = System.Drawing.Color.SteelBlue;
            this.grouper2.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouper2.BorderColor = System.Drawing.Color.SkyBlue;
            this.grouper2.BorderThickness = 1F;
            this.grouper2.Controls.Add(this.shipLogLabel);
            this.grouper2.Controls.Add(this.shipProgressBar);
            this.grouper2.Controls.Add(this.shipButton);
            this.grouper2.Controls.Add(this.shipDateTime);
            this.grouper2.Controls.Add(this.label9);
            this.grouper2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper2.GroupImage = null;
            this.grouper2.GroupTitle = "货运主数据下载";
            this.grouper2.Location = new System.Drawing.Point(0, 343);
            this.grouper2.Name = "grouper2";
            this.grouper2.Padding = new System.Windows.Forms.Padding(20);
            this.grouper2.PaintGroupBox = false;
            this.grouper2.RoundCorners = 10;
            this.grouper2.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper2.ShadowControl = false;
            this.grouper2.ShadowThickness = 3;
            this.grouper2.Size = new System.Drawing.Size(616, 149);
            this.grouper2.TabIndex = 3;
            // 
            // shipLogLabel
            // 
            this.shipLogLabel.BackColor = System.Drawing.Color.White;
            this.shipLogLabel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.shipLogLabel.Location = new System.Drawing.Point(23, 113);
            this.shipLogLabel.Name = "shipLogLabel";
            this.shipLogLabel.Size = new System.Drawing.Size(576, 21);
            this.shipLogLabel.TabIndex = 60;
            this.shipLogLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // shipProgressBar
            // 
            this.shipProgressBar.Location = new System.Drawing.Point(23, 68);
            this.shipProgressBar.Name = "shipProgressBar";
            this.shipProgressBar.Size = new System.Drawing.Size(576, 30);
            this.shipProgressBar.TabIndex = 58;
            // 
            // shipButton
            // 
            this.shipButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.shipButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shipButton.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.shipButton.ForeColor = System.Drawing.Color.White;
            this.shipButton.Location = new System.Drawing.Point(491, 21);
            this.shipButton.Name = "shipButton";
            this.shipButton.Size = new System.Drawing.Size(108, 40);
            this.shipButton.TabIndex = 57;
            this.shipButton.Text = "开始下载";
            this.shipButton.UseVisualStyleBackColor = false;
            this.shipButton.Click += new System.EventHandler(this.shipButton_Click);
            // 
            // shipDateTime
            // 
            this.shipDateTime.Location = new System.Drawing.Point(127, 27);
            this.shipDateTime.MinimumSize = new System.Drawing.Size(0, 30);
            this.shipDateTime.Name = "shipDateTime";
            this.shipDateTime.Size = new System.Drawing.Size(132, 30);
            this.shipDateTime.TabIndex = 56;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label9.Location = new System.Drawing.Point(17, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 21);
            this.label9.TabIndex = 42;
            this.label9.Text = "发运日期：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.White;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.SteelBlue;
            this.grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.SkyBlue;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.label7);
            this.grouper1.Controls.Add(this.label6);
            this.grouper1.Controls.Add(this.dateMatTagButton);
            this.grouper1.Controls.Add(this.eDateTime);
            this.grouper1.Controls.Add(this.sDateTime);
            this.grouper1.Controls.Add(this.matLogLabel);
            this.grouper1.Controls.Add(this.matProgressBar);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.Dock = System.Windows.Forms.DockStyle.Top;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "吊牌主数据下载";
            this.grouper1.Location = new System.Drawing.Point(0, 0);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 10;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(1232, 343);
            this.grouper1.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(436, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 66;
            this.label7.Text = "结束日期：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(24, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 65;
            this.label6.Text = "开始日期：";
            // 
            // dateMatTagButton
            // 
            this.dateMatTagButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.dateMatTagButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dateMatTagButton.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.dateMatTagButton.ForeColor = System.Drawing.Color.White;
            this.dateMatTagButton.Location = new System.Drawing.Point(742, 37);
            this.dateMatTagButton.Name = "dateMatTagButton";
            this.dateMatTagButton.Size = new System.Drawing.Size(91, 40);
            this.dateMatTagButton.TabIndex = 64;
            this.dateMatTagButton.Text = "开始下载";
            this.dateMatTagButton.UseVisualStyleBackColor = false;
            this.dateMatTagButton.Click += new System.EventHandler(this.dateMatTagButton_Click);
            // 
            // eDateTime
            // 
            this.eDateTime.Location = new System.Drawing.Point(510, 45);
            this.eDateTime.MinimumSize = new System.Drawing.Size(0, 30);
            this.eDateTime.Name = "eDateTime";
            this.eDateTime.Size = new System.Drawing.Size(132, 30);
            this.eDateTime.TabIndex = 63;
            // 
            // sDateTime
            // 
            this.sDateTime.Location = new System.Drawing.Point(113, 45);
            this.sDateTime.MinimumSize = new System.Drawing.Size(0, 30);
            this.sDateTime.Name = "sDateTime";
            this.sDateTime.Size = new System.Drawing.Size(132, 30);
            this.sDateTime.TabIndex = 62;
            // 
            // matLogLabel
            // 
            this.matLogLabel.BackColor = System.Drawing.Color.White;
            this.matLogLabel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.matLogLabel.Location = new System.Drawing.Point(23, 308);
            this.matLogLabel.Name = "matLogLabel";
            this.matLogLabel.Size = new System.Drawing.Size(642, 21);
            this.matLogLabel.TabIndex = 61;
            this.matLogLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // matProgressBar
            // 
            this.matProgressBar.Location = new System.Drawing.Point(23, 269);
            this.matProgressBar.Name = "matProgressBar";
            this.matProgressBar.Size = new System.Drawing.Size(1206, 30);
            this.matProgressBar.TabIndex = 58;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 730);
            this.Controls.Add(this.downloadTabControl);
            this.Controls.Add(this.groupBox1);
            this.DisplayHeader = false;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Text = "手动下载";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.downloadTabControl.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.grouper4.ResumeLayout(false);
            this.grouper3.ResumeLayout(false);
            this.grouper2.ResumeLayout(false);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDownloadOutLog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private DMSkin.Metro.Controls.MetroDateTime dtpDate;
        private DMSkin.Metro.Controls.MetroProgressBar pbgShippingLabel;
        private DMSkin.Metro.Controls.MetroProgressBar pbgOutlog;
        private DMSkin.Metro.Controls.MetroTabControl downloadTabControl;
        private DMSkin.Metro.Controls.MetroTabPage metroTabPage1;
        private CodeVendor.Controls.Grouper grouper1;
        private CodeVendor.Controls.Grouper grouper3;
        private CodeVendor.Controls.Grouper grouper2;
        private System.Windows.Forms.Label label9;
        private DMSkin.Metro.Controls.MetroTextBox inventoryStoreTextBox;
        private System.Windows.Forms.Label label3;
        private DMSkin.Metro.Controls.MetroProgressBar inventoryProgressBar;
        private System.Windows.Forms.Button inventoryButton;
        private DMSkin.Metro.Controls.MetroDateTime inventoryDateTime;
        private System.Windows.Forms.Label label2;
        private DMSkin.Metro.Controls.MetroProgressBar shipProgressBar;
        private System.Windows.Forms.Button shipButton;
        private DMSkin.Metro.Controls.MetroDateTime shipDateTime;
        private DMSkin.Metro.Controls.MetroProgressBar matProgressBar;
        private System.Windows.Forms.Label shipLogLabel;
        private System.Windows.Forms.Label inventoryLogLabel;
        private System.Windows.Forms.Label matLogLabel;
        private CodeVendor.Controls.Grouper grouper4;
        private DMSkin.Metro.Controls.MetroProgressBar returnTypeProgressBar;
        private System.Windows.Forms.Button returnTypeutton;
        private DMSkin.Controls.DMCheckBox returnTypeCheckBox;
        private System.Windows.Forms.Label returnTypeLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button dateMatTagButton;
        private DMSkin.Metro.Controls.MetroDateTime eDateTime;
        private DMSkin.Metro.Controls.MetroDateTime sDateTime;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button3;
        private DMSkin.Controls.DMCheckBox dmCheckBox1_resetIfExist;
    }
}

