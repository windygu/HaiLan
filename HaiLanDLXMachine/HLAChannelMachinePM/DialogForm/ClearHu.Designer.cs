namespace HLAChannelMachine.DialogForm
{
    partial class ClearHu
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
            this.textBox1_hu = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1_type = new System.Windows.Forms.ComboBox();
            this.button3_clearDoc = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1_docno = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(401, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "清空箱数据（需要重启软件）";
            // 
            // textBox1_hu
            // 
            this.textBox1_hu.Location = new System.Drawing.Point(230, 171);
            this.textBox1_hu.Name = "textBox1_hu";
            this.textBox1_hu.Size = new System.Drawing.Size(387, 30);
            this.textBox1_hu.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(152, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "箱号：";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(665, 165);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(188, 41);
            this.button1.TabIndex = 3;
            this.button1.Text = "清除箱号数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(665, 456);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(188, 41);
            this.button2.TabIndex = 4;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1_type
            // 
            this.comboBox1_type.FormattingEnabled = true;
            this.comboBox1_type.Items.AddRange(new object[] {
            "交货单",
            "交接单"});
            this.comboBox1_type.Location = new System.Drawing.Point(230, 88);
            this.comboBox1_type.Name = "comboBox1_type";
            this.comboBox1_type.Size = new System.Drawing.Size(121, 28);
            this.comboBox1_type.TabIndex = 5;
            // 
            // button3_clearDoc
            // 
            this.button3_clearDoc.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3_clearDoc.Location = new System.Drawing.Point(665, 274);
            this.button3_clearDoc.Name = "button3_clearDoc";
            this.button3_clearDoc.Size = new System.Drawing.Size(188, 41);
            this.button3_clearDoc.TabIndex = 8;
            this.button3_clearDoc.Text = "清除单号数据";
            this.button3_clearDoc.UseVisualStyleBackColor = true;
            this.button3_clearDoc.Click += new System.EventHandler(this.button3_clearDoc_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(152, 283);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "单号：";
            // 
            // textBox1_docno
            // 
            this.textBox1_docno.Location = new System.Drawing.Point(230, 280);
            this.textBox1_docno.Name = "textBox1_docno";
            this.textBox1_docno.Size = new System.Drawing.Size(387, 30);
            this.textBox1_docno.TabIndex = 6;
            // 
            // ClearHu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(898, 509);
            this.ControlBox = false;
            this.Controls.Add(this.button3_clearDoc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1_docno);
            this.Controls.Add(this.comboBox1_type);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1_hu);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ClearHu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ClearHu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1_hu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1_type;
        private System.Windows.Forms.Button button3_clearDoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1_docno;
    }
}