namespace HLAPackingBoxChannelMachine.DialogForms
{
    partial class JiaoJiaoHandle
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
            this.metroButton1_del = new DMSkin.Metro.Controls.MetroButton();
            this.metroTextBox1_hu = new DMSkin.Metro.Controls.MetroTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // metroButton1_del
            // 
            this.metroButton1_del.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.metroButton1_del.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.metroButton1_del.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.metroButton1_del.DM_UseCustomBackColor = true;
            this.metroButton1_del.DM_UseCustomForeColor = true;
            this.metroButton1_del.DM_UseSelectable = true;
            this.metroButton1_del.ForeColor = System.Drawing.Color.White;
            this.metroButton1_del.Location = new System.Drawing.Point(490, 91);
            this.metroButton1_del.Margin = new System.Windows.Forms.Padding(0);
            this.metroButton1_del.Name = "metroButton1_del";
            this.metroButton1_del.Size = new System.Drawing.Size(163, 47);
            this.metroButton1_del.TabIndex = 20;
            this.metroButton1_del.Text = "删除";
            this.metroButton1_del.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.metroButton1_del.Click += new System.EventHandler(this.metroButton1_del_Click);
            // 
            // metroTextBox1_hu
            // 
            this.metroTextBox1_hu.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.metroTextBox1_hu.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Tall;
            this.metroTextBox1_hu.DM_FontWeight = DMSkin.Metro.MetroTextBoxWeight.Bold;
            this.metroTextBox1_hu.DM_UseSelectable = true;
            this.metroTextBox1_hu.Lines = new string[0];
            this.metroTextBox1_hu.Location = new System.Drawing.Point(45, 100);
            this.metroTextBox1_hu.MaxLength = 32767;
            this.metroTextBox1_hu.Name = "metroTextBox1_hu";
            this.metroTextBox1_hu.PasswordChar = '\0';
            this.metroTextBox1_hu.PromptText = "请输入箱号";
            this.metroTextBox1_hu.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox1_hu.SelectedText = "";
            this.metroTextBox1_hu.Size = new System.Drawing.Size(373, 30);
            this.metroTextBox1_hu.TabIndex = 19;
            this.metroTextBox1_hu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(299, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 31);
            this.label2.TabIndex = 21;
            this.label2.Text = "交接单处理";
            // 
            // JiaoJiaoHandle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 483);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.metroButton1_del);
            this.Controls.Add(this.metroTextBox1_hu);
            this.Name = "JiaoJiaoHandle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DMSkin.Metro.Controls.MetroButton metroButton1_del;
        private DMSkin.Metro.Controls.MetroTextBox metroTextBox1_hu;
        private System.Windows.Forms.Label label2;
    }
}