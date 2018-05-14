namespace HLACancelCheckChannelMachine
{
    partial class ExceptionForm
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
            this.textBox1_Hu = new System.Windows.Forms.TextBox();
            this.button2_sure = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1_Exp = new System.Windows.Forms.DataGridView();
            this.Hu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.main = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Add = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Should = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainDiff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddDiff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.button3_cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1_Exp)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1_Hu
            // 
            this.textBox1_Hu.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1_Hu.Location = new System.Drawing.Point(141, 28);
            this.textBox1_Hu.Name = "textBox1_Hu";
            this.textBox1_Hu.Size = new System.Drawing.Size(229, 29);
            this.textBox1_Hu.TabIndex = 3;
            // 
            // button2_sure
            // 
            this.button2_sure.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2_sure.Location = new System.Drawing.Point(376, 17);
            this.button2_sure.Name = "button2_sure";
            this.button2_sure.Size = new System.Drawing.Size(172, 49);
            this.button2_sure.TabIndex = 1;
            this.button2_sure.Text = "查询";
            this.button2_sure.UseVisualStyleBackColor = true;
            this.button2_sure.Click += new System.EventHandler(this.button2_sure_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1_Exp);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.textBox1_Hu);
            this.splitContainer1.Panel2.Controls.Add(this.button3_cancel);
            this.splitContainer1.Panel2.Controls.Add(this.button2_sure);
            this.splitContainer1.Size = new System.Drawing.Size(899, 496);
            this.splitContainer1.SplitterDistance = 414;
            this.splitContainer1.TabIndex = 1;
            // 
            // dataGridView1_Exp
            // 
            this.dataGridView1_Exp.BackgroundColor = System.Drawing.Color.Teal;
            this.dataGridView1_Exp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1_Exp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Hu,
            this.main,
            this.Add,
            this.Should,
            this.MainDiff,
            this.AddDiff});
            this.dataGridView1_Exp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1_Exp.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1_Exp.MultiSelect = false;
            this.dataGridView1_Exp.Name = "dataGridView1_Exp";
            this.dataGridView1_Exp.ReadOnly = true;
            this.dataGridView1_Exp.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1_Exp.RowTemplate.Height = 23;
            this.dataGridView1_Exp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1_Exp.Size = new System.Drawing.Size(899, 414);
            this.dataGridView1_Exp.TabIndex = 0;
            // 
            // Hu
            // 
            this.Hu.HeaderText = "箱号";
            this.Hu.Name = "Hu";
            this.Hu.ReadOnly = true;
            // 
            // main
            // 
            this.main.HeaderText = "主条码";
            this.main.Name = "main";
            this.main.ReadOnly = true;
            // 
            // Add
            // 
            this.Add.HeaderText = "辐条码";
            this.Add.Name = "Add";
            this.Add.ReadOnly = true;
            // 
            // Should
            // 
            this.Should.HeaderText = "应扫数量";
            this.Should.Name = "Should";
            this.Should.ReadOnly = true;
            // 
            // MainDiff
            // 
            this.MainDiff.HeaderText = "主差异";
            this.MainDiff.Name = "MainDiff";
            this.MainDiff.ReadOnly = true;
            // 
            // AddDiff
            // 
            this.AddDiff.HeaderText = "辐差异";
            this.AddDiff.Name = "AddDiff";
            this.AddDiff.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(26, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "箱号输入：";
            // 
            // button3_cancel
            // 
            this.button3_cancel.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3_cancel.Location = new System.Drawing.Point(763, 9);
            this.button3_cancel.Name = "button3_cancel";
            this.button3_cancel.Size = new System.Drawing.Size(124, 62);
            this.button3_cancel.TabIndex = 2;
            this.button3_cancel.Text = "退出";
            this.button3_cancel.UseVisualStyleBackColor = true;
            this.button3_cancel.Click += new System.EventHandler(this.button3_cancel_Click);
            // 
            // ExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 496);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ExceptionForm";
            this.Text = "Exception";
            this.Load += new System.EventHandler(this.Exception_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1_Exp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1_Hu;
        private System.Windows.Forms.Button button2_sure;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1_Exp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hu;
        private System.Windows.Forms.DataGridViewTextBoxColumn main;
        private System.Windows.Forms.DataGridViewTextBoxColumn Add;
        private System.Windows.Forms.DataGridViewTextBoxColumn Should;
        private System.Windows.Forms.DataGridViewTextBoxColumn MainDiff;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddDiff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3_cancel;
    }
}