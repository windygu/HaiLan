namespace HLAJiaoJieCheckChannelMachine
{
    partial class jiaojiedan
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
            this.textBox1_docno = new System.Windows.Forms.TextBox();
            this.button1_ok = new System.Windows.Forms.Button();
            this.button2_cancel = new System.Windows.Forms.Button();
            this.label2_downStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(46, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "交接单号：";
            // 
            // textBox1_docno
            // 
            this.textBox1_docno.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1_docno.Location = new System.Drawing.Point(172, 47);
            this.textBox1_docno.Name = "textBox1_docno";
            this.textBox1_docno.Size = new System.Drawing.Size(279, 32);
            this.textBox1_docno.TabIndex = 1;
            // 
            // button1_ok
            // 
            this.button1_ok.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1_ok.Location = new System.Drawing.Point(50, 174);
            this.button1_ok.Name = "button1_ok";
            this.button1_ok.Size = new System.Drawing.Size(116, 52);
            this.button1_ok.TabIndex = 2;
            this.button1_ok.Text = "确认";
            this.button1_ok.UseVisualStyleBackColor = true;
            this.button1_ok.Click += new System.EventHandler(this.button1_ok_Click);
            // 
            // button2_cancel
            // 
            this.button2_cancel.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2_cancel.Location = new System.Drawing.Point(335, 174);
            this.button2_cancel.Name = "button2_cancel";
            this.button2_cancel.Size = new System.Drawing.Size(116, 52);
            this.button2_cancel.TabIndex = 3;
            this.button2_cancel.Text = "取消";
            this.button2_cancel.UseVisualStyleBackColor = true;
            this.button2_cancel.Click += new System.EventHandler(this.button2_cancel_Click);
            // 
            // label2_downStatus
            // 
            this.label2_downStatus.AutoSize = true;
            this.label2_downStatus.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2_downStatus.Location = new System.Drawing.Point(353, 108);
            this.label2_downStatus.Name = "label2_downStatus";
            this.label2_downStatus.Size = new System.Drawing.Size(98, 22);
            this.label2_downStatus.TabIndex = 4;
            this.label2_downStatus.Text = "下载状态";
            // 
            // jiaojiedan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(526, 274);
            this.Controls.Add(this.label2_downStatus);
            this.Controls.Add(this.button2_cancel);
            this.Controls.Add(this.button1_ok);
            this.Controls.Add(this.textBox1_docno);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "jiaojiedan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "交接单";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1_docno;
        private System.Windows.Forms.Button button1_ok;
        private System.Windows.Forms.Button button2_cancel;
        private System.Windows.Forms.Label label2_downStatus;
    }
}