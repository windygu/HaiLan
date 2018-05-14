namespace HLACancelCheckChannelMachine
{
    partial class DocSel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1_docBox = new System.Windows.Forms.DataGridView();
            this.DocNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoxNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1_bocihao = new System.Windows.Forms.TextBox();
            this.button3_shanxuan = new System.Windows.Forms.Button();
            this.button2_cancel = new System.Windows.Forms.Button();
            this.button1_selOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1_docBox)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1_docBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox1_bocihao);
            this.splitContainer1.Panel2.Controls.Add(this.button3_shanxuan);
            this.splitContainer1.Panel2.Controls.Add(this.button2_cancel);
            this.splitContainer1.Panel2.Controls.Add(this.button1_selOK);
            this.splitContainer1.Size = new System.Drawing.Size(955, 495);
            this.splitContainer1.SplitterDistance = 414;
            this.splitContainer1.TabIndex = 0;
            // 
            // dataGridView1_docBox
            // 
            this.dataGridView1_docBox.BackgroundColor = System.Drawing.Color.Teal;
            this.dataGridView1_docBox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1_docBox.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DocNo,
            this.BoxNum});
            this.dataGridView1_docBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1_docBox.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1_docBox.MultiSelect = false;
            this.dataGridView1_docBox.Name = "dataGridView1_docBox";
            this.dataGridView1_docBox.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1_docBox.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1_docBox.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1_docBox.RowTemplate.Height = 23;
            this.dataGridView1_docBox.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1_docBox.Size = new System.Drawing.Size(955, 414);
            this.dataGridView1_docBox.TabIndex = 0;
            this.dataGridView1_docBox.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_docBox_CellClick);
            // 
            // DocNo
            // 
            this.DocNo.HeaderText = "点数波次号";
            this.DocNo.Name = "DocNo";
            this.DocNo.ReadOnly = true;
            this.DocNo.Width = 400;
            // 
            // BoxNum
            // 
            this.BoxNum.HeaderText = "总箱数";
            this.BoxNum.Name = "BoxNum";
            this.BoxNum.ReadOnly = true;
            this.BoxNum.Width = 300;
            // 
            // textBox1_bocihao
            // 
            this.textBox1_bocihao.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1_bocihao.Location = new System.Drawing.Point(461, 28);
            this.textBox1_bocihao.Name = "textBox1_bocihao";
            this.textBox1_bocihao.Size = new System.Drawing.Size(229, 29);
            this.textBox1_bocihao.TabIndex = 3;
            // 
            // button3_shanxuan
            // 
            this.button3_shanxuan.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3_shanxuan.Location = new System.Drawing.Point(763, 9);
            this.button3_shanxuan.Name = "button3_shanxuan";
            this.button3_shanxuan.Size = new System.Drawing.Size(180, 62);
            this.button3_shanxuan.TabIndex = 2;
            this.button3_shanxuan.Text = "筛选确认";
            this.button3_shanxuan.UseVisualStyleBackColor = true;
            this.button3_shanxuan.Click += new System.EventHandler(this.button3_shanxuan_Click);
            // 
            // button2_cancel
            // 
            this.button2_cancel.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2_cancel.Location = new System.Drawing.Point(212, 9);
            this.button2_cancel.Name = "button2_cancel";
            this.button2_cancel.Size = new System.Drawing.Size(172, 62);
            this.button2_cancel.TabIndex = 1;
            this.button2_cancel.Text = "退出选择";
            this.button2_cancel.UseVisualStyleBackColor = true;
            this.button2_cancel.Click += new System.EventHandler(this.button2_cancel_Click);
            // 
            // button1_selOK
            // 
            this.button1_selOK.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1_selOK.Location = new System.Drawing.Point(12, 9);
            this.button1_selOK.Name = "button1_selOK";
            this.button1_selOK.Size = new System.Drawing.Size(162, 62);
            this.button1_selOK.TabIndex = 0;
            this.button1_selOK.Text = "选择完成";
            this.button1_selOK.UseVisualStyleBackColor = true;
            this.button1_selOK.Click += new System.EventHandler(this.button1_selOK_Click);
            // 
            // DocSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 495);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Name = "DocSel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "点数波次选择";
            this.Load += new System.EventHandler(this.DocSel_Load);
            this.Shown += new System.EventHandler(this.DocSel_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1_docBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1_docBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BoxNum;
        private System.Windows.Forms.TextBox textBox1_bocihao;
        private System.Windows.Forms.Button button3_shanxuan;
        private System.Windows.Forms.Button button2_cancel;
        private System.Windows.Forms.Button button1_selOK;
    }
}