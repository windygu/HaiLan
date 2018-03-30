namespace HLAZZZTest
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.grid = new DMSkin.Metro.Controls.MetroGrid();
            this.EB_PARTNER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EB_HU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EB_PICK_TASK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EB_ZSATNR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EB_ZCOLSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EB_ZSIZTX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EB_REAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EB_DIFF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EB_REMARK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 30);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grid);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(1075, 608);
            this.splitContainer1.SplitterDistance = 358;
            this.splitContainer1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(107, 148);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(713, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "zhuzhuzhu";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.ColumnHeadersHeight = 43;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EB_PARTNER,
            this.EB_HU,
            this.EB_PICK_TASK,
            this.EB_ZSATNR,
            this.EB_ZCOLSN,
            this.EB_ZSIZTX,
            this.EB_REAL,
            this.EB_DIFF,
            this.EB_REMARK});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.DefaultCellStyle = dataGridViewCellStyle3;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.GridColor = System.Drawing.Color.DarkGray;
            this.grid.Location = new System.Drawing.Point(0, 12);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 43;
            this.grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grid.RowTemplate.Height = 43;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(713, 596);
            this.grid.TabIndex = 4;
            // 
            // EB_PARTNER
            // 
            this.EB_PARTNER.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EB_PARTNER.FillWeight = 70F;
            this.EB_PARTNER.HeaderText = "门店";
            this.EB_PARTNER.Name = "EB_PARTNER";
            this.EB_PARTNER.ReadOnly = true;
            this.EB_PARTNER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EB_HU
            // 
            this.EB_HU.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EB_HU.HeaderText = "箱号";
            this.EB_HU.Name = "EB_HU";
            this.EB_HU.ReadOnly = true;
            this.EB_HU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EB_PICK_TASK
            // 
            this.EB_PICK_TASK.HeaderText = "下架单号";
            this.EB_PICK_TASK.Name = "EB_PICK_TASK";
            this.EB_PICK_TASK.ReadOnly = true;
            // 
            // EB_ZSATNR
            // 
            this.EB_ZSATNR.HeaderText = "品号";
            this.EB_ZSATNR.Name = "EB_ZSATNR";
            this.EB_ZSATNR.ReadOnly = true;
            this.EB_ZSATNR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EB_ZCOLSN
            // 
            this.EB_ZCOLSN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EB_ZCOLSN.FillWeight = 70F;
            this.EB_ZCOLSN.HeaderText = "色号";
            this.EB_ZCOLSN.Name = "EB_ZCOLSN";
            this.EB_ZCOLSN.ReadOnly = true;
            this.EB_ZCOLSN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EB_ZSIZTX
            // 
            this.EB_ZSIZTX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EB_ZSIZTX.HeaderText = "规格";
            this.EB_ZSIZTX.Name = "EB_ZSIZTX";
            this.EB_ZSIZTX.ReadOnly = true;
            this.EB_ZSIZTX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EB_REAL
            // 
            this.EB_REAL.FillWeight = 60F;
            this.EB_REAL.HeaderText = "实发";
            this.EB_REAL.Name = "EB_REAL";
            this.EB_REAL.ReadOnly = true;
            // 
            // EB_DIFF
            // 
            this.EB_DIFF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EB_DIFF.FillWeight = 60F;
            this.EB_DIFF.HeaderText = "差异";
            this.EB_DIFF.Name = "EB_DIFF";
            this.EB_DIFF.ReadOnly = true;
            this.EB_DIFF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EB_REMARK
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EB_REMARK.DefaultCellStyle = dataGridViewCellStyle2;
            this.EB_REMARK.HeaderText = "备注";
            this.EB_REMARK.Name = "EB_REMARK";
            this.EB_REMARK.ReadOnly = true;
            this.EB_REMARK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 668);
            this.Controls.Add(this.splitContainer1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private DMSkin.Metro.Controls.MetroGrid grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_PARTNER;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_HU;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_PICK_TASK;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_ZSATNR;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_ZCOLSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_ZSIZTX;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_REAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_DIFF;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_REMARK;
    }
}

