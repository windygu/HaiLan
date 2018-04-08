namespace HLAChannelMachine
{
    partial class BoxQtyConfigFormNew
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
            this.grouper1 = new CodeVendor.Controls.Grouper();
            this.btnKeyboard = new DMSkin.Controls.DMLabel();
            this.boxQtyTextBox = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1_close = new System.Windows.Forms.Button();
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
            this.grouper1.Controls.Add(this.button1_close);
            this.grouper1.Controls.Add(this.btnKeyboard);
            this.grouper1.Controls.Add(this.boxQtyTextBox);
            this.grouper1.Controls.Add(this.btnOk);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "箱规设置";
            this.grouper1.Location = new System.Drawing.Point(162, 145);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 10;
            this.grouper1.ShadowColor = System.Drawing.Color.SkyBlue;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(486, 228);
            this.grouper1.TabIndex = 2;
            // 
            // btnKeyboard
            // 
            this.btnKeyboard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnKeyboard.BackColor = System.Drawing.Color.Transparent;
            this.btnKeyboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKeyboard.DM_Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnKeyboard.DM_Font_Size = 25F;
            this.btnKeyboard.DM_Key = DMSkin.Controls.DMLabelKey.键盘;
            this.btnKeyboard.DM_Text = "";
            this.btnKeyboard.Location = new System.Drawing.Point(409, 80);
            this.btnKeyboard.Name = "btnKeyboard";
            this.btnKeyboard.Size = new System.Drawing.Size(41, 28);
            this.btnKeyboard.TabIndex = 12;
            this.btnKeyboard.Text = "dmLabel1";
            this.btnKeyboard.Click += new System.EventHandler(this.btnKeyboard_Click);
            // 
            // boxQtyTextBox
            // 
            this.boxQtyTextBox.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.boxQtyTextBox.Location = new System.Drawing.Point(136, 79);
            this.boxQtyTextBox.Name = "boxQtyTextBox";
            this.boxQtyTextBox.Size = new System.Drawing.Size(327, 34);
            this.boxQtyTextBox.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.BackColor = System.Drawing.Color.Teal;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(136, 151);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(150, 45);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.label1.Location = new System.Drawing.Point(15, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 27);
            this.label1.TabIndex = 5;
            this.label1.Text = "输入箱规：";
            // 
            // button1_close
            // 
            this.button1_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1_close.BackColor = System.Drawing.Color.Teal;
            this.button1_close.FlatAppearance.BorderSize = 0;
            this.button1_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.button1_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1_close.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.button1_close.ForeColor = System.Drawing.Color.White;
            this.button1_close.Location = new System.Drawing.Point(313, 151);
            this.button1_close.Name = "button1_close";
            this.button1_close.Size = new System.Drawing.Size(150, 45);
            this.button1_close.TabIndex = 13;
            this.button1_close.Text = "关闭";
            this.button1_close.UseVisualStyleBackColor = false;
            this.button1_close.Click += new System.EventHandler(this.button1_close_Click);
            // 
            // BoxQtyConfigFormNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(810, 522);
            this.Controls.Add(this.grouper1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BoxQtyConfigFormNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "海澜之家收货系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CodeVendor.Controls.Grouper grouper1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox boxQtyTextBox;
        private DMSkin.Controls.DMLabel btnKeyboard;
        private System.Windows.Forms.Button button1_close;
    }
}