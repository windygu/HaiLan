namespace HLAChannelMachine
{
    partial class OldLoginForm
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
            this.grouper1 = new CodeVendor.Controls.Grouper();
            this.btnKeyboard = new DMSkin.Controls.DMLabel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtUserNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grouper1
            // 
            this.grouper1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grouper1.BackgroundColor = System.Drawing.Color.White;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.Vertical;
            this.grouper1.BorderColor = System.Drawing.Color.SkyBlue;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.btnKeyboard);
            this.grouper1.Controls.Add(this.txtPassword);
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.btnSet);
            this.grouper1.Controls.Add(this.btnClose);
            this.grouper1.Controls.Add(this.btnReset);
            this.grouper1.Controls.Add(this.btnLogin);
            this.grouper1.Controls.Add(this.txtUserNo);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "工号登录";
            this.grouper1.Location = new System.Drawing.Point(131, 65);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 10;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(447, 293);
            this.grouper1.TabIndex = 1;
            // 
            // btnKeyboard
            // 
            this.btnKeyboard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnKeyboard.BackColor = System.Drawing.Color.White;
            this.btnKeyboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKeyboard.DM_Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnKeyboard.DM_Font_Size = 20F;
            this.btnKeyboard.DM_Key = DMSkin.Controls.DMLabelKey.键盘;
            this.btnKeyboard.DM_Text = "";
            this.btnKeyboard.Location = new System.Drawing.Point(350, 59);
            this.btnKeyboard.Name = "btnKeyboard";
            this.btnKeyboard.Size = new System.Drawing.Size(37, 26);
            this.btnKeyboard.TabIndex = 5;
            this.btnKeyboard.Text = "dmLabel1";
            this.btnKeyboard.Click += new System.EventHandler(this.btnKeyboard_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.txtPassword.Location = new System.Drawing.Point(140, 100);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(249, 34);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(58, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 27);
            this.label2.TabIndex = 12;
            this.label2.Text = "密码：";
            // 
            // btnSet
            // 
            this.btnSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.btnSet.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.btnSet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.btnSet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(189)))), ((int)(((byte)(239)))));
            this.btnSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSet.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSet.ForeColor = System.Drawing.Color.White;
            this.btnSet.Location = new System.Drawing.Point(63, 204);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(160, 43);
            this.btnSet.TabIndex = 10;
            this.btnSet.Text = "配置";
            this.btnSet.UseVisualStyleBackColor = false;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.OrangeRed;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.OrangeRed;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(229, 204);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 43);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(189)))), ((int)(((byte)(239)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(229, 155);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(160, 43);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(189)))), ((int)(((byte)(239)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(63, 155);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(160, 43);
            this.btnLogin.TabIndex = 8;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtUserNo
            // 
            this.txtUserNo.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.txtUserNo.Location = new System.Drawing.Point(140, 57);
            this.txtUserNo.Name = "txtUserNo";
            this.txtUserNo.Size = new System.Drawing.Size(249, 34);
            this.txtUserNo.TabIndex = 1;
            this.txtUserNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserNo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(58, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 27);
            this.label1.TabIndex = 6;
            this.label1.Text = "工号：";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.ClientSize = new System.Drawing.Size(714, 426);
            this.Controls.Add(this.grouper1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "海澜之家收货系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CodeVendor.Controls.Grouper grouper1;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtUserNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private DMSkin.Controls.DMLabel btnKeyboard;

    }
}

