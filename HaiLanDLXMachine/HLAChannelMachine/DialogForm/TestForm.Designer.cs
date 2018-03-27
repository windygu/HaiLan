namespace HLAChannelMachine
{
    partial class TestForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 48F);
            this.label1.Location = new System.Drawing.Point(107, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 83);
            this.label1.TabIndex = 0;
            this.label1.Text = "null";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.label3.Location = new System.Drawing.Point(86, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 35);
            this.label3.TabIndex = 1;
            this.label3.Text = "未读取箱数:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.label4.Location = new System.Drawing.Point(140, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 35);
            this.label4.TabIndex = 1;
            this.label4.Text = "总箱数:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.label5.Location = new System.Drawing.Point(59, 313);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 35);
            this.label5.TabIndex = 1;
            this.label5.Text = "读取多次箱数:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.label6.Location = new System.Drawing.Point(317, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 35);
            this.label6.TabIndex = 1;
            this.label6.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.label7.Location = new System.Drawing.Point(317, 252);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 35);
            this.label7.TabIndex = 1;
            this.label7.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.label8.Location = new System.Drawing.Point(317, 313);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 35);
            this.label8.TabIndex = 1;
            this.label8.Text = "0";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 420);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "TestForm";
            this.Text = "测试窗口";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}