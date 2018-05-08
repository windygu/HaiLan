namespace HLACommonView.Views
{
    partial class CommonLoginFormPM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommonLoginForm));
            this.btnKeyboard = new DMSkin.Controls.DMLabel();
            this.lblLogin = new DMSkin.Controls.DMLabel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelLoading = new System.Windows.Forms.Panel();
            this.metroProgressSpinner1 = new DMSkin.Metro.Controls.MetroProgressSpinner();
            this.btnDeliver = new DMSkin.Controls.DMButton();
            this.dmLabel1 = new DMSkin.Controls.DMLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.btnLogin = new DMSkin.Controls.DMButton();
            this.btnExit = new DMSkin.Controls.DMButton();
            this.btnConfig = new DMSkin.Controls.DMLabel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnRestart = new DMSkin.Controls.DMButton();
            this.label2 = new System.Windows.Forms.Label();
            this.dmLabel2 = new DMSkin.Controls.DMLabel();
            this.cboLouceng = new System.Windows.Forms.ComboBox();
            this.panelLoading.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnKeyboard
            // 
            this.btnKeyboard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnKeyboard.BackColor = System.Drawing.Color.White;
            this.btnKeyboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKeyboard.DM_Color = System.Drawing.Color.Teal;
            this.btnKeyboard.DM_Font_Size = 22F;
            this.btnKeyboard.DM_Key = DMSkin.Controls.DMLabelKey.键盘;
            this.btnKeyboard.DM_Text = "";
            this.btnKeyboard.Location = new System.Drawing.Point(522, 168);
            this.btnKeyboard.Name = "btnKeyboard";
            this.btnKeyboard.Size = new System.Drawing.Size(39, 26);
            this.btnKeyboard.TabIndex = 4;
            this.btnKeyboard.Text = "dmLabel1";
            this.btnKeyboard.Click += new System.EventHandler(this.btnKeyboard_Click);
            // 
            // lblLogin
            // 
            this.lblLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLogin.BackColor = System.Drawing.Color.White;
            this.lblLogin.DM_Color = System.Drawing.Color.Teal;
            this.lblLogin.DM_Font_Size = 22F;
            this.lblLogin.DM_Key = DMSkin.Controls.DMLabelKey.用户;
            this.lblLogin.DM_Text = "";
            this.lblLogin.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblLogin.Location = new System.Drawing.Point(202, 168);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(30, 29);
            this.lblLogin.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(3, 39);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(390, 23);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "loading...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelLoading
            // 
            this.panelLoading.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panelLoading.Controls.Add(this.metroProgressSpinner1);
            this.panelLoading.Controls.Add(this.lblStatus);
            this.panelLoading.Location = new System.Drawing.Point(180, 389);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.Size = new System.Drawing.Size(396, 63);
            this.panelLoading.TabIndex = 9;
            this.panelLoading.Visible = false;
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.DM_Maximum = 100;
            this.metroProgressSpinner1.DM_UseCustomBackColor = true;
            this.metroProgressSpinner1.DM_UseSelectable = true;
            this.metroProgressSpinner1.DM_Value = 90;
            this.metroProgressSpinner1.Location = new System.Drawing.Point(183, 4);
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(31, 31);
            this.metroProgressSpinner1.Style = DMSkin.Metro.MetroColorStyle.White;
            this.metroProgressSpinner1.TabIndex = 9;
            // 
            // btnDeliver
            // 
            this.btnDeliver.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDeliver.AutoEllipsis = true;
            this.btnDeliver.BackColor = System.Drawing.Color.Transparent;
            this.btnDeliver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeliver.DM_DisabledColor = System.Drawing.Color.White;
            this.btnDeliver.DM_DownColor = System.Drawing.Color.White;
            this.btnDeliver.DM_MoveColor = System.Drawing.Color.White;
            this.btnDeliver.DM_NormalColor = System.Drawing.Color.White;
            this.btnDeliver.DM_Radius = 8;
            this.btnDeliver.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnDeliver.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeliver.Font = new System.Drawing.Font("微软雅黑", 24F);
            this.btnDeliver.ForeColor = System.Drawing.Color.White;
            this.btnDeliver.Image = null;
            this.btnDeliver.Location = new System.Drawing.Point(180, 148);
            this.btnDeliver.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeliver.Name = "btnDeliver";
            this.btnDeliver.Size = new System.Drawing.Size(396, 168);
            this.btnDeliver.TabIndex = 14;
            this.btnDeliver.Text = "";
            this.btnDeliver.UseVisualStyleBackColor = true;
            // 
            // dmLabel1
            // 
            this.dmLabel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dmLabel1.BackColor = System.Drawing.Color.White;
            this.dmLabel1.DM_Color = System.Drawing.Color.Teal;
            this.dmLabel1.DM_Font_Size = 22F;
            this.dmLabel1.DM_Key = DMSkin.Controls.DMLabelKey.锁;
            this.dmLabel1.DM_Text = "";
            this.dmLabel1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.dmLabel1.Location = new System.Drawing.Point(204, 217);
            this.dmLabel1.Name = "dmLabel1";
            this.dmLabel1.Size = new System.Drawing.Size(30, 29);
            this.dmLabel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.label1.Location = new System.Drawing.Point(190, 196);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 18);
            this.label1.TabIndex = 15;
            this.label1.Text = "————————————————————————————";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtUser
            // 
            this.txtUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUser.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.txtUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.txtUser.Location = new System.Drawing.Point(256, 169);
            this.txtUser.Margin = new System.Windows.Forms.Padding(0);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(268, 29);
            this.txtUser.TabIndex = 0;
            this.txtUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUser_KeyPress);
            // 
            // txtPwd
            // 
            this.txtPwd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPwd.BackColor = System.Drawing.Color.White;
            this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPwd.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.txtPwd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.txtPwd.Location = new System.Drawing.Point(256, 215);
            this.txtPwd.Margin = new System.Windows.Forms.Padding(0);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(305, 32);
            this.txtPwd.TabIndex = 1;
            this.txtPwd.UseSystemPasswordChar = true;
            this.txtPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPwd_KeyPress);
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLogin.AutoEllipsis = true;
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnLogin.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(124)))), ((int)(((byte)(16)))));
            this.btnLogin.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(212)))), ((int)(((byte)(100)))));
            this.btnLogin.DM_NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(198)))), ((int)(((byte)(48)))));
            this.btnLogin.DM_Radius = 8;
            this.btnLogin.Enabled = false;
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogin.Font = new System.Drawing.Font("微软雅黑 Light", 16F);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Image = null;
            this.btnLogin.Location = new System.Drawing.Point(180, 326);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(0);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(219, 49);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExit.AutoEllipsis = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnExit.DM_DownColor = System.Drawing.Color.Maroon;
            this.btnExit.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnExit.DM_NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(91)))), ((int)(((byte)(48)))));
            this.btnExit.DM_Radius = 8;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑 Light", 16F);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Image = null;
            this.btnExit.Location = new System.Drawing.Point(411, 326);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(165, 49);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnConfig.BackColor = System.Drawing.Color.Transparent;
            this.btnConfig.DM_Color = System.Drawing.Color.White;
            this.btnConfig.DM_Font_Size = 22F;
            this.btnConfig.DM_Key = DMSkin.Controls.DMLabelKey.设置;
            this.btnConfig.DM_Text = "";
            this.btnConfig.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnConfig.Location = new System.Drawing.Point(588, 169);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(35, 29);
            this.btnConfig.TabIndex = 4;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.lblTitle.ForeColor = System.Drawing.Color.Transparent;
            this.lblTitle.Location = new System.Drawing.Point(11, 83);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(734, 53);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "海澜之家智能供应链管理系统";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Visible = false;
            // 
            // btnRestart
            // 
            this.btnRestart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRestart.AutoEllipsis = true;
            this.btnRestart.BackColor = System.Drawing.Color.Transparent;
            this.btnRestart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestart.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnRestart.DM_DownColor = System.Drawing.Color.Maroon;
            this.btnRestart.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnRestart.DM_NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(91)))), ((int)(((byte)(48)))));
            this.btnRestart.DM_Radius = 8;
            this.btnRestart.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRestart.Font = new System.Drawing.Font("微软雅黑 Light", 14F);
            this.btnRestart.ForeColor = System.Drawing.Color.White;
            this.btnRestart.Image = null;
            this.btnRestart.Location = new System.Drawing.Point(180, 326);
            this.btnRestart.Margin = new System.Windows.Forms.Padding(0);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(396, 49);
            this.btnRestart.TabIndex = 18;
            this.btnRestart.Text = "SAP未维护设备信息，请维护后点击重启";
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Visible = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.label2.Location = new System.Drawing.Point(190, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(380, 18);
            this.label2.TabIndex = 15;
            this.label2.Text = "————————————————————————————";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dmLabel2
            // 
            this.dmLabel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dmLabel2.BackColor = System.Drawing.Color.White;
            this.dmLabel2.DM_Color = System.Drawing.Color.Teal;
            this.dmLabel2.DM_Font_Size = 22F;
            this.dmLabel2.DM_Key = DMSkin.Controls.DMLabelKey.主页;
            this.dmLabel2.DM_Text = "";
            this.dmLabel2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.dmLabel2.Location = new System.Drawing.Point(201, 267);
            this.dmLabel2.Name = "dmLabel2";
            this.dmLabel2.Size = new System.Drawing.Size(39, 29);
            this.dmLabel2.TabIndex = 5;
            // 
            // cboLouceng
            // 
            this.cboLouceng.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboLouceng.BackColor = System.Drawing.Color.White;
            this.cboLouceng.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboLouceng.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.cboLouceng.FormattingEnabled = true;
            this.cboLouceng.Location = new System.Drawing.Point(256, 265);
            this.cboLouceng.Name = "cboLouceng";
            this.cboLouceng.Size = new System.Drawing.Size(305, 33);
            this.cboLouceng.TabIndex = 20;
            // 
            // CommonLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(746, 459);
            this.Controls.Add(this.cboLouceng);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.panelLoading);
            this.Controls.Add(this.dmLabel2);
            this.Controls.Add(this.dmLabel1);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.btnKeyboard);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDeliver);
            this.DM_EffectCaption = DMSkin.TitleType.None;
            this.DM_howBorder = false;
            this.DM_ShowDrawIcon = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CommonLoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登    录";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.panelLoading.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DMSkin.Controls.DMLabel btnKeyboard;
        private DMSkin.Controls.DMLabel lblLogin;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelLoading;
        private DMSkin.Controls.DMButton btnDeliver;
        private DMSkin.Controls.DMLabel dmLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPwd;
        private DMSkin.Metro.Controls.MetroProgressSpinner metroProgressSpinner1;
        private DMSkin.Controls.DMButton btnLogin;
        private DMSkin.Controls.DMButton btnExit;
        private DMSkin.Controls.DMLabel btnConfig;
        private System.Windows.Forms.Label lblTitle;
        private DMSkin.Controls.DMButton btnRestart;
        private System.Windows.Forms.Label label2;
        private DMSkin.Controls.DMLabel dmLabel2;
        public System.Windows.Forms.ComboBox cboLouceng;
    }
}