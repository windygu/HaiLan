namespace HLAPackingBoxChannelMachine.DialogForms
{
    partial class DocBoxDetailForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grid = new DMSkin.Metro.Controls.MetroGrid();
            this.btnClose = new DMSkin.Controls.DMButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.uploadAgainButton = new System.Windows.Forms.Button();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Datetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PACKRESULT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZSATNR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZCOLSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZSIZTX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MSG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.ColumnHeadersHeight = 65;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.Datetime,
            this.LH,
            this.HU,
            this.PACKRESULT,
            this.ZSATNR,
            this.ZCOLSN,
            this.ZSIZTX,
            this.MSG});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle3;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.GridColor = System.Drawing.Color.DarkGray;
            this.grid.Location = new System.Drawing.Point(20, 60);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 43;
            this.grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grid.RowTemplate.Height = 43;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grid.Size = new System.Drawing.Size(975, 442);
            this.grid.TabIndex = 8;
            this.grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellClick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoEllipsis = true;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnClose.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(0)))));
            this.btnClose.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(60)))), ((int)(((byte)(0)))));
            this.btnClose.DM_NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            this.btnClose.DM_Radius = 1;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑 Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = null;
            this.btnClose.Location = new System.Drawing.Point(340, 517);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(131, 49);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Visible = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.button1.Location = new System.Drawing.Point(485, 512);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 54);
            this.button1.TabIndex = 16;
            this.button1.Text = "清除异常";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.button2.Location = new System.Drawing.Point(847, 512);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(148, 54);
            this.button2.TabIndex = 17;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // uploadAgainButton
            // 
            this.uploadAgainButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uploadAgainButton.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.uploadAgainButton.Location = new System.Drawing.Point(672, 512);
            this.uploadAgainButton.Name = "uploadAgainButton";
            this.uploadAgainButton.Size = new System.Drawing.Size(148, 54);
            this.uploadAgainButton.TabIndex = 18;
            this.uploadAgainButton.Text = "重新上传";
            this.uploadAgainButton.UseVisualStyleBackColor = true;
            this.uploadAgainButton.Click += new System.EventHandler(this.uploadAgainButton_Click);
            // 
            // check
            // 
            this.check.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.check.FillWeight = 5.583755F;
            this.check.HeaderText = "";
            this.check.Name = "check";
            this.check.ReadOnly = true;
            this.check.Width = 5;
            // 
            // Datetime
            // 
            this.Datetime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Datetime.FillWeight = 13.05203F;
            this.Datetime.HeaderText = "日期";
            this.Datetime.Name = "Datetime";
            this.Datetime.ReadOnly = true;
            this.Datetime.Width = 70;
            // 
            // LH
            // 
            this.LH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.LH.FillWeight = 6.264975F;
            this.LH.HeaderText = "交接楼号";
            this.LH.Name = "LH";
            this.LH.ReadOnly = true;
            this.LH.Width = 88;
            // 
            // HU
            // 
            this.HU.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.HU.FillWeight = 12.00787F;
            this.HU.HeaderText = "箱号";
            this.HU.Name = "HU";
            this.HU.ReadOnly = true;
            this.HU.Width = 70;
            // 
            // PACKRESULT
            // 
            this.PACKRESULT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.PACKRESULT.FillWeight = 6.264975F;
            this.PACKRESULT.HeaderText = "包装状态";
            this.PACKRESULT.Name = "PACKRESULT";
            this.PACKRESULT.ReadOnly = true;
            this.PACKRESULT.Width = 88;
            // 
            // ZSATNR
            // 
            this.ZSATNR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ZSATNR.FillWeight = 16.7066F;
            this.ZSATNR.HeaderText = "品号";
            this.ZSATNR.Name = "ZSATNR";
            this.ZSATNR.ReadOnly = true;
            this.ZSATNR.Width = 70;
            // 
            // ZCOLSN
            // 
            this.ZCOLSN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ZCOLSN.FillWeight = 6.264975F;
            this.ZCOLSN.HeaderText = "色号";
            this.ZCOLSN.Name = "ZCOLSN";
            this.ZCOLSN.ReadOnly = true;
            this.ZCOLSN.Width = 70;
            // 
            // ZSIZTX
            // 
            this.ZSIZTX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ZSIZTX.FillWeight = 13.57411F;
            this.ZSIZTX.HeaderText = "规格";
            this.ZSIZTX.Name = "ZSIZTX";
            this.ZSIZTX.ReadOnly = true;
            this.ZSIZTX.Width = 70;
            // 
            // MSG
            // 
            this.MSG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.MSG.DefaultCellStyle = dataGridViewCellStyle2;
            this.MSG.FillWeight = 30.28071F;
            this.MSG.HeaderText = "返回信息";
            this.MSG.Name = "MSG";
            this.MSG.ReadOnly = true;
            this.MSG.Width = 88;
            // 
            // DocBoxDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 593);
            this.Controls.Add(this.uploadAgainButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grid);
            this.MinimizeBox = false;
            this.Name = "DocBoxDetailForm";
            this.Text = "异常箱明细";
            this.TextAlign = DMSkin.MetroFormTextAlign.Center;
            this.Load += new System.EventHandler(this.DocBoxDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DMSkin.Metro.Controls.MetroGrid grid;
        private DMSkin.Controls.DMButton btnClose;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button uploadAgainButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn Datetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn LH;
        private System.Windows.Forms.DataGridViewTextBoxColumn HU;
        private System.Windows.Forms.DataGridViewTextBoxColumn PACKRESULT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZSATNR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZCOLSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZSIZTX;
        private System.Windows.Forms.DataGridViewTextBoxColumn MSG;
    }
}