namespace HLADeliverChannelMachine.DialogForms
{
    partial class OutConfirmForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblLogin = new DMSkin.Controls.DMLabel();
            this.btnKeyboard = new DMSkin.Controls.DMLabel();
            this.btnCancel = new DMSkin.Metro.Controls.MetroButton();
            this.btnConfirm = new DMSkin.Metro.Controls.MetroButton();
            this.txtPwd = new DMSkin.Metro.Controls.MetroTextBox();
            this.txtUser = new DMSkin.Metro.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 26F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label1.Location = new System.Drawing.Point(233, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 46);
            this.label1.TabIndex = 13;
            this.label1.Text = "短拣确认";
            // 
            // lblLogin
            // 
            this.lblLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLogin.BackColor = System.Drawing.Color.Transparent;
            this.lblLogin.DM_Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblLogin.DM_Font_Size = 30F;
            this.lblLogin.DM_Key = DMSkin.Controls.DMLabelKey.正确;
            this.lblLogin.DM_Text = "";
            this.lblLogin.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblLogin.Location = new System.Drawing.Point(182, 50);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(43, 42);
            this.lblLogin.TabIndex = 12;
            // 
            // btnKeyboard
            // 
            this.btnKeyboard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnKeyboard.BackColor = System.Drawing.Color.Transparent;
            this.btnKeyboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKeyboard.DM_Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnKeyboard.DM_Font_Size = 30F;
            this.btnKeyboard.DM_Key = DMSkin.Controls.DMLabelKey.键盘;
            this.btnKeyboard.DM_Text = "";
            this.btnKeyboard.Location = new System.Drawing.Point(440, 114);
            this.btnKeyboard.Name = "btnKeyboard";
            this.btnKeyboard.Size = new System.Drawing.Size(50, 49);
            this.btnKeyboard.TabIndex = 11;
            this.btnKeyboard.Text = "dmLabel1";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.DM_DM_DisplayFocus = true;
            this.btnCancel.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnCancel.DM_UseSelectable = true;
            this.btnCancel.Location = new System.Drawing.Point(296, 223);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(138, 35);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnConfirm.DM_DM_DisplayFocus = true;
            this.btnConfirm.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnConfirm.DM_UseSelectable = true;
            this.btnConfirm.Location = new System.Drawing.Point(134, 223);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(138, 35);
            this.btnConfirm.TabIndex = 9;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPwd.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Tall;
            this.txtPwd.DM_UseSelectable = true;
            this.txtPwd.Lines = new string[0];
            this.txtPwd.Location = new System.Drawing.Point(134, 172);
            this.txtPwd.MaxLength = 32767;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '●';
            this.txtPwd.PromptText = "密    码";
            this.txtPwd.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPwd.SelectedText = "";
            this.txtPwd.Size = new System.Drawing.Size(300, 35);
            this.txtPwd.TabIndex = 8;
            this.txtPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPwd.UseSystemPasswordChar = true;
            // 
            // txtUser
            // 
            this.txtUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUser.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Tall;
            this.txtUser.DM_UseSelectable = true;
            this.txtUser.Lines = new string[0];
            this.txtUser.Location = new System.Drawing.Point(134, 120);
            this.txtUser.MaxLength = 32767;
            this.txtUser.Name = "txtUser";
            this.txtUser.PasswordChar = '\0';
            this.txtUser.PromptText = "工    号";
            this.txtUser.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUser.SelectedText = "";
            this.txtUser.Size = new System.Drawing.Size(300, 35);
            this.txtUser.TabIndex = 7;
            this.txtUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OutConfirmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(584, 353);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.btnKeyboard);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtUser);
            this.DM_EffectCaption = DMSkin.TitleType.None;
            this.DM_howBorder = false;
            this.DM_ShowDrawIcon = false;
            this.Name = "OutConfirmForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登    录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DMSkin.Controls.DMLabel lblLogin;
        private DMSkin.Controls.DMLabel btnKeyboard;
        private DMSkin.Metro.Controls.MetroButton btnCancel;
        private DMSkin.Metro.Controls.MetroButton btnConfirm;
        private DMSkin.Metro.Controls.MetroTextBox txtPwd;
        private DMSkin.Metro.Controls.MetroTextBox txtUser;
    }
}