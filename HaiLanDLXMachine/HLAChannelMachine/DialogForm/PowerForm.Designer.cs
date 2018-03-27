namespace HLAChannelMachine
{
    partial class PowerForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.txtDelayTime1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtPower1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDelayTime2 = new System.Windows.Forms.TextBox();
            this.txtPower2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDelayTime3 = new System.Windows.Forms.TextBox();
            this.txtPower3 = new System.Windows.Forms.TextBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grouper1
            // 
            this.grouper1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grouper1.BackgroundColor = System.Drawing.Color.White;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.SteelBlue;
            this.grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.Vertical;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.lblMsg);
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.txtDelayTime3);
            this.grouper1.Controls.Add(this.txtPower3);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.Controls.Add(this.txtDelayTime2);
            this.grouper1.Controls.Add(this.txtPower2);
            this.grouper1.Controls.Add(this.label5);
            this.grouper1.Controls.Add(this.txtDelayTime1);
            this.grouper1.Controls.Add(this.label8);
            this.grouper1.Controls.Add(this.btnCancel);
            this.grouper1.Controls.Add(this.btnOk);
            this.grouper1.Controls.Add(this.txtPower1);
            this.grouper1.Controls.Add(this.label4);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "配置";
            this.grouper1.Location = new System.Drawing.Point(48, 12);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 10;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(577, 466);
            this.grouper1.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(43, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 27);
            this.label5.TabIndex = 44;
            this.label5.Text = "箱规<20";
            // 
            // txtDelayTime1
            // 
            this.txtDelayTime1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDelayTime1.Location = new System.Drawing.Point(368, 104);
            this.txtDelayTime1.Name = "txtDelayTime1";
            this.txtDelayTime1.Size = new System.Drawing.Size(159, 34);
            this.txtDelayTime1.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(378, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(160, 27);
            this.label8.TabIndex = 40;
            this.label8.Text = "延迟开门（ms）";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(346, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(114, 41);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(125, 360);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(114, 41);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // txtPower1
            // 
            this.txtPower1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPower1.Location = new System.Drawing.Point(178, 104);
            this.txtPower1.Name = "txtPower1";
            this.txtPower1.Size = new System.Drawing.Size(159, 34);
            this.txtPower1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(228, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 27);
            this.label4.TabIndex = 33;
            this.label4.Text = "功率";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(43, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 27);
            this.label1.TabIndex = 47;
            this.label1.Text = "20≤箱规≤80";
            // 
            // txtDelayTime2
            // 
            this.txtDelayTime2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDelayTime2.Location = new System.Drawing.Point(368, 166);
            this.txtDelayTime2.Name = "txtDelayTime2";
            this.txtDelayTime2.Size = new System.Drawing.Size(159, 34);
            this.txtDelayTime2.TabIndex = 4;
            // 
            // txtPower2
            // 
            this.txtPower2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPower2.Location = new System.Drawing.Point(178, 166);
            this.txtPower2.Name = "txtPower2";
            this.txtPower2.Size = new System.Drawing.Size(159, 34);
            this.txtPower2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(43, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 27);
            this.label2.TabIndex = 50;
            this.label2.Text = "箱规>80";
            // 
            // txtDelayTime3
            // 
            this.txtDelayTime3.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDelayTime3.Location = new System.Drawing.Point(368, 232);
            this.txtDelayTime3.Name = "txtDelayTime3";
            this.txtDelayTime3.Size = new System.Drawing.Size(159, 34);
            this.txtDelayTime3.TabIndex = 6;
            // 
            // txtPower3
            // 
            this.txtPower3.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPower3.Location = new System.Drawing.Point(178, 232);
            this.txtPower3.Name = "txtPower3";
            this.txtPower3.Size = new System.Drawing.Size(159, 34);
            this.txtPower3.TabIndex = 5;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMsg.Location = new System.Drawing.Point(43, 295);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(178, 27);
            this.lblMsg.TabIndex = 51;
            this.lblMsg.Text = "当前最大箱规：{0}";
            // 
            // PowerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(56)))), ((int)(((byte)(91)))));
            this.ClientSize = new System.Drawing.Size(663, 510);
            this.Controls.Add(this.grouper1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PowerForm";
            this.Text = "PowerForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CodeVendor.Controls.Grouper grouper1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDelayTime1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtPower1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDelayTime3;
        private System.Windows.Forms.TextBox txtPower3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDelayTime2;
        private System.Windows.Forms.TextBox txtPower2;
    }
}