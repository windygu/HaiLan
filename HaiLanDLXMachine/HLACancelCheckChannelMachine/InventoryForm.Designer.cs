namespace HLACancelCheckChannelMachine
{
    partial class InventoryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ComboBox_Boci = new System.Windows.Forms.ComboBox();
            this.dmButton3 = new DMSkin.Controls.DMButton();
            this.dmButton1_exception_query = new DMSkin.Controls.DMButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11_deviceNo = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9_boxNo = new System.Windows.Forms.Label();
            this.btnStart = new DMSkin.Controls.DMButton();
            this.btnPause = new DMSkin.Controls.DMButton();
            this.btnClose = new DMSkin.Controls.DMButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblWorkStatus = new System.Windows.Forms.Label();
            this.lblLouceng = new System.Windows.Forms.Label();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPlc = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblReader = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grid = new DMSkin.Metro.Controls.MetroGrid();
            this.HU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAINBAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADDBAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAINDIF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADDDIF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MSG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 30);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Teal;
            this.splitContainer1.Panel1.Controls.Add(this.ComboBox_Boci);
            this.splitContainer1.Panel1.Controls.Add(this.dmButton3);
            this.splitContainer1.Panel1.Controls.Add(this.dmButton1_exception_query);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.label11_deviceNo);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label9_boxNo);
            this.splitContainer1.Panel1.Controls.Add(this.btnStart);
            this.splitContainer1.Panel1.Controls.Add(this.btnPause);
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.lblWorkStatus);
            this.splitContainer1.Panel1.Controls.Add(this.lblLouceng);
            this.splitContainer1.Panel1.Controls.Add(this.lblCurrentUser);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.lblPlc);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.lblReader);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grid);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(1221, 712);
            this.splitContainer1.SplitterDistance = 289;
            this.splitContainer1.TabIndex = 1;
            // 
            // ComboBox_Boci
            // 
            this.ComboBox_Boci.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ComboBox_Boci.BackColor = System.Drawing.Color.White;
            this.ComboBox_Boci.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Boci.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.ComboBox_Boci.FormattingEnabled = true;
            this.ComboBox_Boci.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10"});
            this.ComboBox_Boci.Location = new System.Drawing.Point(110, 254);
            this.ComboBox_Boci.Name = "ComboBox_Boci";
            this.ComboBox_Boci.Size = new System.Drawing.Size(170, 33);
            this.ComboBox_Boci.TabIndex = 53;
            this.ComboBox_Boci.SelectionChangeCommitted += new System.EventHandler(this.ComboBox_Boci_SelectionChangeCommitted);
            // 
            // dmButton3
            // 
            this.dmButton3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dmButton3.AutoEllipsis = true;
            this.dmButton3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dmButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dmButton3.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.dmButton3.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.dmButton3.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.dmButton3.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.dmButton3.DM_Radius = 1;
            this.dmButton3.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.dmButton3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dmButton3.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.dmButton3.ForeColor = System.Drawing.Color.Teal;
            this.dmButton3.Image = null;
            this.dmButton3.Location = new System.Drawing.Point(13, 603);
            this.dmButton3.Margin = new System.Windows.Forms.Padding(0);
            this.dmButton3.Name = "dmButton3";
            this.dmButton3.Size = new System.Drawing.Size(250, 40);
            this.dmButton3.TabIndex = 31;
            this.dmButton3.Text = "操作组详情";
            this.dmButton3.UseVisualStyleBackColor = false;
            this.dmButton3.Click += new System.EventHandler(this.dmButton3_Click);
            // 
            // dmButton1_exception_query
            // 
            this.dmButton1_exception_query.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dmButton1_exception_query.AutoEllipsis = true;
            this.dmButton1_exception_query.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dmButton1_exception_query.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dmButton1_exception_query.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.dmButton1_exception_query.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.dmButton1_exception_query.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.dmButton1_exception_query.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.dmButton1_exception_query.DM_Radius = 1;
            this.dmButton1_exception_query.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.dmButton1_exception_query.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dmButton1_exception_query.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.dmButton1_exception_query.ForeColor = System.Drawing.Color.Teal;
            this.dmButton1_exception_query.Image = null;
            this.dmButton1_exception_query.Location = new System.Drawing.Point(13, 495);
            this.dmButton1_exception_query.Margin = new System.Windows.Forms.Padding(0);
            this.dmButton1_exception_query.Name = "dmButton1_exception_query";
            this.dmButton1_exception_query.Size = new System.Drawing.Size(250, 40);
            this.dmButton1_exception_query.TabIndex = 29;
            this.dmButton1_exception_query.Text = "异常查询";
            this.dmButton1_exception_query.UseVisualStyleBackColor = false;
            this.dmButton1_exception_query.Click += new System.EventHandler(this.dmButton1_exception_query_Click);
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(4, 254);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 32);
            this.label12.TabIndex = 27;
            this.label12.Text = "点数波次：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(4, 334);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 32);
            this.label10.TabIndex = 25;
            this.label10.Text = "工作状态：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11_deviceNo
            // 
            this.label11_deviceNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label11_deviceNo.BackColor = System.Drawing.Color.White;
            this.label11_deviceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11_deviceNo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label11_deviceNo.Location = new System.Drawing.Point(110, 214);
            this.label11_deviceNo.Name = "label11_deviceNo";
            this.label11_deviceNo.Size = new System.Drawing.Size(170, 32);
            this.label11_deviceNo.TabIndex = 26;
            this.label11_deviceNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(5, 296);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 32);
            this.label8.TabIndex = 23;
            this.label8.Text = "当前箱号：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9_boxNo
            // 
            this.label9_boxNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9_boxNo.BackColor = System.Drawing.Color.White;
            this.label9_boxNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9_boxNo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label9_boxNo.Location = new System.Drawing.Point(110, 295);
            this.label9_boxNo.Name = "label9_boxNo";
            this.label9_boxNo.Size = new System.Drawing.Size(170, 32);
            this.label9_boxNo.TabIndex = 24;
            this.label9_boxNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnStart.AutoEllipsis = true;
            this.btnStart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnStart.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.btnStart.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.btnStart.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnStart.DM_Radius = 1;
            this.btnStart.Enabled = false;
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.btnStart.ForeColor = System.Drawing.Color.Teal;
            this.btnStart.Image = null;
            this.btnStart.Location = new System.Drawing.Point(13, 383);
            this.btnStart.Margin = new System.Windows.Forms.Padding(0);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(250, 40);
            this.btnStart.TabIndex = 19;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPause
            // 
            this.btnPause.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnPause.AutoEllipsis = true;
            this.btnPause.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPause.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnPause.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.btnPause.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.btnPause.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnPause.DM_Radius = 1;
            this.btnPause.Enabled = false;
            this.btnPause.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPause.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.btnPause.ForeColor = System.Drawing.Color.Teal;
            this.btnPause.Image = null;
            this.btnPause.Location = new System.Drawing.Point(13, 440);
            this.btnPause.Margin = new System.Windows.Forms.Padding(0);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(250, 40);
            this.btnPause.TabIndex = 20;
            this.btnPause.Text = "暂停";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnClose.AutoEllipsis = true;
            this.btnClose.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnClose.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(0)))));
            this.btnClose.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(60)))), ((int)(((byte)(0)))));
            this.btnClose.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnClose.DM_Radius = 1;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Teal;
            this.btnClose.Image = null;
            this.btnClose.Location = new System.Drawing.Point(13, 661);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(250, 40);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(4, 214);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 32);
            this.label6.TabIndex = 7;
            this.label6.Text = "设备号：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(4, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 32);
            this.label3.TabIndex = 8;
            this.label3.Text = "作业楼层：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWorkStatus
            // 
            this.lblWorkStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblWorkStatus.BackColor = System.Drawing.Color.White;
            this.lblWorkStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWorkStatus.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblWorkStatus.Location = new System.Drawing.Point(110, 334);
            this.lblWorkStatus.Name = "lblWorkStatus";
            this.lblWorkStatus.Size = new System.Drawing.Size(170, 32);
            this.lblWorkStatus.TabIndex = 9;
            this.lblWorkStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLouceng
            // 
            this.lblLouceng.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLouceng.BackColor = System.Drawing.Color.White;
            this.lblLouceng.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLouceng.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblLouceng.Location = new System.Drawing.Point(109, 170);
            this.lblLouceng.Name = "lblLouceng";
            this.lblLouceng.Size = new System.Drawing.Size(170, 32);
            this.lblLouceng.TabIndex = 10;
            this.lblLouceng.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCurrentUser.BackColor = System.Drawing.Color.White;
            this.lblCurrentUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentUser.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblCurrentUser.Location = new System.Drawing.Point(109, 129);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(170, 32);
            this.lblCurrentUser.TabIndex = 11;
            this.lblCurrentUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(4, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 32);
            this.label4.TabIndex = 12;
            this.label4.Text = "登录工号：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(4, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 32);
            this.label5.TabIndex = 13;
            this.label5.Text = "PLC状态：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPlc
            // 
            this.lblPlc.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblPlc.BackColor = System.Drawing.Color.White;
            this.lblPlc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPlc.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblPlc.Location = new System.Drawing.Point(109, 89);
            this.lblPlc.Name = "lblPlc";
            this.lblPlc.Size = new System.Drawing.Size(170, 32);
            this.lblPlc.TabIndex = 14;
            this.lblPlc.Text = "连接中...";
            this.lblPlc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(4, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 32);
            this.label7.TabIndex = 15;
            this.label7.Text = "读写器：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReader
            // 
            this.lblReader.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblReader.BackColor = System.Drawing.Color.White;
            this.lblReader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblReader.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblReader.Location = new System.Drawing.Point(109, 49);
            this.lblReader.Name = "lblReader";
            this.lblReader.Size = new System.Drawing.Size(170, 32);
            this.lblReader.TabIndex = 16;
            this.lblReader.Text = "连接中...";
            this.lblReader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "状态监控";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.ColumnHeadersHeight = 65;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HU,
            this.MAINBAR,
            this.ADDBAR,
            this.QTY,
            this.MAINDIF,
            this.ADDDIF,
            this.MSG});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle3;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.GridColor = System.Drawing.Color.DarkGray;
            this.grid.Location = new System.Drawing.Point(0, 40);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 43;
            this.grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.grid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.RowTemplate.Height = 43;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(928, 672);
            this.grid.TabIndex = 19;
            // 
            // HU
            // 
            this.HU.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.HU.FillWeight = 13F;
            this.HU.HeaderText = "箱号";
            this.HU.Name = "HU";
            this.HU.ReadOnly = true;
            this.HU.Width = 70;
            // 
            // MAINBAR
            // 
            this.MAINBAR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.MAINBAR.FillWeight = 15F;
            this.MAINBAR.HeaderText = "主条码";
            this.MAINBAR.Name = "MAINBAR";
            this.MAINBAR.ReadOnly = true;
            this.MAINBAR.Width = 88;
            // 
            // ADDBAR
            // 
            this.ADDBAR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ADDBAR.FillWeight = 7F;
            this.ADDBAR.HeaderText = "辐条码";
            this.ADDBAR.Name = "ADDBAR";
            this.ADDBAR.ReadOnly = true;
            this.ADDBAR.Width = 88;
            // 
            // QTY
            // 
            this.QTY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.QTY.FillWeight = 13F;
            this.QTY.HeaderText = "应扫数量";
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            this.QTY.Width = 88;
            // 
            // MAINDIF
            // 
            this.MAINDIF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.MAINDIF.FillWeight = 7F;
            this.MAINDIF.HeaderText = "主差异";
            this.MAINDIF.Name = "MAINDIF";
            this.MAINDIF.ReadOnly = true;
            this.MAINDIF.Width = 88;
            // 
            // ADDDIF
            // 
            this.ADDDIF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ADDDIF.HeaderText = "辐差异";
            this.ADDDIF.Name = "ADDDIF";
            this.ADDDIF.ReadOnly = true;
            this.ADDDIF.Width = 88;
            // 
            // MSG
            // 
            this.MSG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.MSG.DefaultCellStyle = dataGridViewCellStyle2;
            this.MSG.FillWeight = 38F;
            this.MSG.HeaderText = "状态";
            this.MSG.Name = "MSG";
            this.MSG.ReadOnly = true;
            this.MSG.Width = 70;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Teal;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(928, 40);
            this.label2.TabIndex = 1;
            this.label2.Text = "点数复核明细";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 772);
            this.Controls.Add(this.splitContainer1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "InventoryForm";
            this.Text = "InventoryForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InventoryForm_FormClosing);
            this.Load += new System.EventHandler(this.InventoryForm_Load);
            this.Shown += new System.EventHandler(this.InventoryForm_Shown);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblWorkStatus;
        private System.Windows.Forms.Label lblLouceng;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPlc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblReader;
        private DMSkin.Controls.DMButton btnStart;
        private DMSkin.Controls.DMButton btnPause;
        private DMSkin.Controls.DMButton btnClose;
        private DMSkin.Metro.Controls.MetroGrid grid;
        private DMSkin.Controls.DMButton dmButton3;
        private DMSkin.Controls.DMButton dmButton1_exception_query;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11_deviceNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9_boxNo;
        private System.Windows.Forms.ComboBox ComboBox_Boci;
        private System.Windows.Forms.DataGridViewTextBoxColumn HU;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAINBAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADDBAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAINDIF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADDDIF;
        private System.Windows.Forms.DataGridViewTextBoxColumn MSG;
    }
}