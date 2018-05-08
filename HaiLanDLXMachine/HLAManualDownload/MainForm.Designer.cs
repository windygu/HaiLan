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
            this.matFileDownButton = new System.Windows.Forms.Button();
            this.matFileSelButton = new System.Windows.Forms.Button();
            this.selFileNameTextBox = new DMSkin.Metro.Controls.MetroTextBox();
            this.button1_downloadmat = new System.Windows.Forms.Button();
            this.button1_allmat = new System.Windows.Forms.Button();
            this.label1_hora = new System.Windows.Forms.Label();
            this.button1_alltag = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dateMatTagButton = new System.Windows.Forms.Button();
            this.eDateTime = new DMSkin.Metro.Controls.MetroDateTime();
            this.sDateTime = new DMSkin.Metro.Controls.MetroDateTime();
            this.matLogLabel = new System.Windows.Forms.Label();
            this.matProgressBar = new DMSkin.Metro.Controls.MetroProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1_singlemat = new System.Windows.Forms.Button();
            this.metroTextBox1_singlemat = new DMSkin.Metro.Controls.MetroTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.downloadTabControl.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.grouper4.SuspendLayout();
            this.grouper3.SuspendLayout();
            this.grouper2.SuspendLayout();
            this.grouper1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
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
            this.grouper4.BorderColor = System.Drawing.Color.Teal;
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
            this.button3.BackColor = System.Drawing.Color.Teal;
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
            this.returnTypeutton.BackColor = System.Drawing.Color.Teal;
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
            this.grouper3.BorderColor = System.Drawing.Color.Teal;
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
            this.inventoryButton.BackColor = System.Drawing.Color.Teal;
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
            this.grouper2.BorderColor = System.Drawing.Color.Teal;
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
            this.shipButton.BackColor = System.Drawing.Color.Teal;
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
            this.grouper1.BorderColor = System.Drawing.Color.Teal;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.groupBox4);
            this.grouper1.Controls.Add(this.groupBox3);
            this.grouper1.Controls.Add(this.groupBox2);
            this.grouper1.Controls.Add(this.groupBox1);
            this.grouper1.Controls.Add(this.label1_hora);
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
            // matFileDownButton
            // 
            this.matFileDownButton.BackColor = System.Drawing.Color.Teal;
            this.matFileDownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.matFileDownButton.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.matFileDownButton.ForeColor = System.Drawing.Color.White;
            this.matFileDownButton.Location = new System.Drawing.Point(181, 20);
            this.matFileDownButton.Name = "matFileDownButton";
            this.matFileDownButton.Size = new System.Drawing.Size(92, 40);
            this.matFileDownButton.TabIndex = 73;
            this.matFileDownButton.Text = "开始下载";
            this.matFileDownButton.UseVisualStyleBackColor = false;
            this.matFileDownButton.Click += new System.EventHandler(this.matFileDownButton_Click);
            // 
            // matFileSelButton
            // 
            this.matFileSelButton.BackColor = System.Drawing.Color.Teal;
            this.matFileSelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.matFileSelButton.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.matFileSelButton.ForeColor = System.Drawing.Color.White;
            this.matFileSelButton.Location = new System.Drawing.Point(119, 20);
            this.matFileSelButton.Name = "matFileSelButton";
            this.matFileSelButton.Size = new System.Drawing.Size(56, 40);
            this.matFileSelButton.TabIndex = 72;
            this.matFileSelButton.Text = "选择";
            this.matFileSelButton.UseVisualStyleBackColor = false;
            this.matFileSelButton.Click += new System.EventHandler(this.matFileSelButton_Click);
            // 
            // selFileNameTextBox
            // 
            this.selFileNameTextBox.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Medium;
            this.selFileNameTextBox.DM_UseSelectable = true;
            this.selFileNameTextBox.Lines = new string[0];
            this.selFileNameTextBox.Location = new System.Drawing.Point(15, 27);
            this.selFileNameTextBox.MaxLength = 32767;
            this.selFileNameTextBox.Name = "selFileNameTextBox";
            this.selFileNameTextBox.PasswordChar = '\0';
            this.selFileNameTextBox.PromptText = "产品编码文件";
            this.selFileNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.selFileNameTextBox.SelectedText = "";
            this.selFileNameTextBox.Size = new System.Drawing.Size(98, 30);
            this.selFileNameTextBox.TabIndex = 71;
            this.selFileNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1_downloadmat
            // 
            this.button1_downloadmat.BackColor = System.Drawing.Color.Teal;
            this.button1_downloadmat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1_downloadmat.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.button1_downloadmat.ForeColor = System.Drawing.Color.White;
            this.button1_downloadmat.Location = new System.Drawing.Point(196, 151);
            this.button1_downloadmat.Name = "button1_downloadmat";
            this.button1_downloadmat.Size = new System.Drawing.Size(130, 40);
            this.button1_downloadmat.TabIndex = 70;
            this.button1_downloadmat.Text = "开始下载物料";
            this.button1_downloadmat.UseVisualStyleBackColor = false;
            this.button1_downloadmat.Click += new System.EventHandler(this.button1_downloadmat_Click);
            // 
            // button1_allmat
            // 
            this.button1_allmat.BackColor = System.Drawing.Color.Teal;
            this.button1_allmat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1_allmat.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.button1_allmat.ForeColor = System.Drawing.Color.White;
            this.button1_allmat.Location = new System.Drawing.Point(29, 117);
            this.button1_allmat.Name = "button1_allmat";
            this.button1_allmat.Size = new System.Drawing.Size(204, 40);
            this.button1_allmat.TabIndex = 69;
            this.button1_allmat.Text = "下载所有物料";
            this.button1_allmat.UseVisualStyleBackColor = false;
            this.button1_allmat.Click += new System.EventHandler(this.button1_allmat_Click);
            // 
            // label1_hora
            // 
            this.label1_hora.AutoSize = true;
            this.label1_hora.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1_hora.Location = new System.Drawing.Point(1106, 20);
            this.label1_hora.Name = "label1_hora";
            this.label1_hora.Size = new System.Drawing.Size(75, 20);
            this.label1_hora.TabIndex = 68;
            this.label1_hora.Text = "label1";
            // 
            // button1_alltag
            // 
            this.button1_alltag.BackColor = System.Drawing.Color.Teal;
            this.button1_alltag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1_alltag.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.button1_alltag.ForeColor = System.Drawing.Color.White;
            this.button1_alltag.Location = new System.Drawing.Point(29, 20);
            this.button1_alltag.Name = "button1_alltag";
            this.button1_alltag.Size = new System.Drawing.Size(204, 40);
            this.button1_alltag.TabIndex = 67;
            this.button1_alltag.Text = "下载所有吊牌";
            this.button1_alltag.UseVisualStyleBackColor = false;
            this.button1_alltag.Click += new System.EventHandler(this.button1_alltag_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(20, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 66;
            this.label7.Text = "结束日期：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(20, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 65;
            this.label6.Text = "开始日期：";
            // 
            // dateMatTagButton
            // 
            this.dateMatTagButton.BackColor = System.Drawing.Color.Teal;
            this.dateMatTagButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dateMatTagButton.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.dateMatTagButton.ForeColor = System.Drawing.Color.White;
            this.dateMatTagButton.Location = new System.Drawing.Point(23, 151);
            this.dateMatTagButton.Name = "dateMatTagButton";
            this.dateMatTagButton.Size = new System.Drawing.Size(130, 40);
            this.dateMatTagButton.TabIndex = 64;
            this.dateMatTagButton.Text = "开始下载吊牌";
            this.dateMatTagButton.UseVisualStyleBackColor = false;
            this.dateMatTagButton.Click += new System.EventHandler(this.dateMatTagButton_Click);
            // 
            // eDateTime
            // 
            this.eDateTime.Location = new System.Drawing.Point(109, 76);
            this.eDateTime.MinimumSize = new System.Drawing.Size(0, 30);
            this.eDateTime.Name = "eDateTime";
            this.eDateTime.Size = new System.Drawing.Size(217, 30);
            this.eDateTime.TabIndex = 63;
            // 
            // sDateTime
            // 
            this.sDateTime.Location = new System.Drawing.Point(109, 15);
            this.sDateTime.MinimumSize = new System.Drawing.Size(0, 30);
            this.sDateTime.Name = "sDateTime";
            this.sDateTime.Size = new System.Drawing.Size(217, 30);
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
            // button1_singlemat
            // 
            this.button1_singlemat.BackColor = System.Drawing.Color.Teal;
            this.button1_singlemat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1_singlemat.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.button1_singlemat.ForeColor = System.Drawing.Color.White;
            this.button1_singlemat.Location = new System.Drawing.Point(181, 22);
            this.button1_singlemat.Name = "button1_singlemat";
            this.button1_singlemat.Size = new System.Drawing.Size(92, 40);
            this.button1_singlemat.TabIndex = 75;
            this.button1_singlemat.Text = "开始下载";
            this.button1_singlemat.UseVisualStyleBackColor = false;
            this.button1_singlemat.Click += new System.EventHandler(this.button1_singlemat_Click);
            // 
            // metroTextBox1_singlemat
            // 
            this.metroTextBox1_singlemat.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Medium;
            this.metroTextBox1_singlemat.DM_UseSelectable = true;
            this.metroTextBox1_singlemat.Lines = new string[0];
            this.metroTextBox1_singlemat.Location = new System.Drawing.Point(15, 29);
            this.metroTextBox1_singlemat.MaxLength = 32767;
            this.metroTextBox1_singlemat.Name = "metroTextBox1_singlemat";
            this.metroTextBox1_singlemat.PasswordChar = '\0';
            this.metroTextBox1_singlemat.PromptText = "产品编码";
            this.metroTextBox1_singlemat.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox1_singlemat.SelectedText = "";
            this.metroTextBox1_singlemat.Size = new System.Drawing.Size(160, 30);
            this.metroTextBox1_singlemat.TabIndex = 74;
            this.metroTextBox1_singlemat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1_alltag);
            this.groupBox1.Controls.Add(this.button1_allmat);
            this.groupBox1.Location = new System.Drawing.Point(416, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 190);
            this.groupBox1.TabIndex = 76;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1_downloadmat);
            this.groupBox2.Controls.Add(this.sDateTime);
            this.groupBox2.Controls.Add(this.eDateTime);
            this.groupBox2.Controls.Add(this.dateMatTagButton);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(23, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(345, 203);
            this.groupBox2.TabIndex = 77;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.matFileDownButton);
            this.groupBox3.Controls.Add(this.selFileNameTextBox);
            this.groupBox3.Controls.Add(this.matFileSelButton);
            this.groupBox3.Location = new System.Drawing.Point(738, 35);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(299, 79);
            this.groupBox3.TabIndex = 78;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1_singlemat);
            this.groupBox4.Controls.Add(this.metroTextBox1_singlemat);
            this.groupBox4.Location = new System.Drawing.Point(738, 126);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(299, 95);
            this.groupBox4.TabIndex = 79;
            this.groupBox4.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 730);
            this.Controls.Add(this.downloadTabControl);
            this.DisplayHeader = false;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Text = "手动下载";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.downloadTabControl.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.grouper4.ResumeLayout(false);
            this.grouper3.ResumeLayout(false);
            this.grouper2.ResumeLayout(false);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.Button button1_alltag;
        private System.Windows.Forms.Label label1_hora;
        private System.Windows.Forms.Button button1_allmat;
        private System.Windows.Forms.Button button1_downloadmat;
        private System.Windows.Forms.Button matFileDownButton;
        private System.Windows.Forms.Button matFileSelButton;
        private DMSkin.Metro.Controls.MetroTextBox selFileNameTextBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button1_singlemat;
        private DMSkin.Metro.Controls.MetroTextBox metroTextBox1_singlemat;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

