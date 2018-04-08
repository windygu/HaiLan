namespace HLADeliverChannelMachine.DialogForms
{
    partial class OutLogDetailForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grid = new DMSkin.Metro.Controls.MetroGrid();
            this.ZSATNR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZCOLSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZSIZTX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PLANQTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REALQTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DIFFERENT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new DMSkin.Metro.Controls.MetroButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.metroLabel2 = new DMSkin.Metro.Controls.MetroLabel();
            this.lblDate = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel7 = new DMSkin.Metro.Controls.MetroLabel();
            this.lblPlanCount = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel13 = new DMSkin.Metro.Controls.MetroLabel();
            this.lblDifferent = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel11 = new DMSkin.Metro.Controls.MetroLabel();
            this.lblRealCount = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel9 = new DMSkin.Metro.Controls.MetroLabel();
            this.lblLGNUM = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel5 = new DMSkin.Metro.Controls.MetroLabel();
            this.lblStore = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel3 = new DMSkin.Metro.Controls.MetroLabel();
            this.lblOutlog = new DMSkin.Metro.Controls.MetroLabel();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grid.ColumnHeadersHeight = 43;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ZSATNR,
            this.ZCOLSN,
            this.ZSIZTX,
            this.PLANQTY,
            this.REALQTY,
            this.DIFFERENT,
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle3;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.GridColor = System.Drawing.Color.DarkGray;
            this.grid.Location = new System.Drawing.Point(0, 0);
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
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.grid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.RowTemplate.Height = 43;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(778, 358);
            this.grid.TabIndex = 2;
            // 
            // ZSATNR
            // 
            this.ZSATNR.HeaderText = "品号";
            this.ZSATNR.Name = "ZSATNR";
            this.ZSATNR.ReadOnly = true;
            // 
            // ZCOLSN
            // 
            this.ZCOLSN.HeaderText = "色号";
            this.ZCOLSN.Name = "ZCOLSN";
            this.ZCOLSN.ReadOnly = true;
            // 
            // ZSIZTX
            // 
            this.ZSIZTX.HeaderText = "规格";
            this.ZSIZTX.Name = "ZSIZTX";
            this.ZSIZTX.ReadOnly = true;
            // 
            // PLANQTY
            // 
            this.PLANQTY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PLANQTY.HeaderText = "预计";
            this.PLANQTY.Name = "PLANQTY";
            this.PLANQTY.ReadOnly = true;
            // 
            // REALQTY
            // 
            this.REALQTY.HeaderText = "实际";
            this.REALQTY.Name = "REALQTY";
            this.REALQTY.ReadOnly = true;
            // 
            // DIFFERENT
            // 
            this.DIFFERENT.HeaderText = "差异";
            this.DIFFERENT.Name = "DIFFERENT";
            this.DIFFERENT.ReadOnly = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnCancel.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnCancel.DM_UseCustomBackColor = true;
            this.btnCancel.DM_UseCustomForeColor = true;
            this.btnCancel.DM_UseSelectable = true;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(688, 325);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 33);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "返回";
            this.btnCancel.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(20, 65);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel2);
            this.splitContainer1.Panel1.Controls.Add(this.lblDate);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel7);
            this.splitContainer1.Panel1.Controls.Add(this.lblPlanCount);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel13);
            this.splitContainer1.Panel1.Controls.Add(this.lblDifferent);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel11);
            this.splitContainer1.Panel1.Controls.Add(this.lblRealCount);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel9);
            this.splitContainer1.Panel1.Controls.Add(this.lblLGNUM);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel5);
            this.splitContainer1.Panel1.Controls.Add(this.lblStore);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel3);
            this.splitContainer1.Panel1.Controls.Add(this.lblOutlog);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel2.Controls.Add(this.grid);
            this.splitContainer1.Size = new System.Drawing.Size(778, 500);
            this.splitContainer1.SplitterDistance = 138;
            this.splitContainer1.TabIndex = 6;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel2.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel2.Location = new System.Drawing.Point(289, 24);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(86, 24);
            this.metroLabel2.TabIndex = 1;
            this.metroLabel2.Text = "单据日期:";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblDate.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblDate.Location = new System.Drawing.Point(381, 24);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(114, 24);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "2015-09-10";
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel7.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel7.Location = new System.Drawing.Point(24, 100);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(86, 24);
            this.metroLabel7.TabIndex = 1;
            this.metroLabel7.Text = "预计数量:";
            // 
            // lblPlanCount
            // 
            this.lblPlanCount.AutoSize = true;
            this.lblPlanCount.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblPlanCount.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblPlanCount.Location = new System.Drawing.Point(116, 100);
            this.lblPlanCount.Name = "lblPlanCount";
            this.lblPlanCount.Size = new System.Drawing.Size(32, 24);
            this.lblPlanCount.TabIndex = 2;
            this.lblPlanCount.Text = "13";
            // 
            // metroLabel13
            // 
            this.metroLabel13.AutoSize = true;
            this.metroLabel13.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel13.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel13.Location = new System.Drawing.Point(560, 100);
            this.metroLabel13.Name = "metroLabel13";
            this.metroLabel13.Size = new System.Drawing.Size(50, 24);
            this.metroLabel13.TabIndex = 1;
            this.metroLabel13.Text = "差异:";
            // 
            // lblDifferent
            // 
            this.lblDifferent.AutoSize = true;
            this.lblDifferent.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblDifferent.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblDifferent.Location = new System.Drawing.Point(616, 100);
            this.lblDifferent.Name = "lblDifferent";
            this.lblDifferent.Size = new System.Drawing.Size(21, 24);
            this.lblDifferent.TabIndex = 2;
            this.lblDifferent.Text = "0";
            // 
            // metroLabel11
            // 
            this.metroLabel11.AutoSize = true;
            this.metroLabel11.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel11.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel11.Location = new System.Drawing.Point(289, 100);
            this.metroLabel11.Name = "metroLabel11";
            this.metroLabel11.Size = new System.Drawing.Size(86, 24);
            this.metroLabel11.TabIndex = 1;
            this.metroLabel11.Text = "实际数量:";
            // 
            // lblRealCount
            // 
            this.lblRealCount.AutoSize = true;
            this.lblRealCount.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblRealCount.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblRealCount.Location = new System.Drawing.Point(381, 100);
            this.lblRealCount.Name = "lblRealCount";
            this.lblRealCount.Size = new System.Drawing.Size(32, 24);
            this.lblRealCount.TabIndex = 2;
            this.lblRealCount.Text = "13";
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel9.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel9.Location = new System.Drawing.Point(289, 62);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(68, 24);
            this.metroLabel9.TabIndex = 1;
            this.metroLabel9.Text = "发货方:";
            // 
            // lblLGNUM
            // 
            this.lblLGNUM.AutoSize = true;
            this.lblLGNUM.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblLGNUM.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblLGNUM.Location = new System.Drawing.Point(381, 62);
            this.lblLGNUM.Name = "lblLGNUM";
            this.lblLGNUM.Size = new System.Drawing.Size(55, 24);
            this.lblLGNUM.TabIndex = 2;
            this.lblLGNUM.Text = "HL01";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel5.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel5.Location = new System.Drawing.Point(24, 62);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(68, 24);
            this.metroLabel5.TabIndex = 1;
            this.metroLabel5.Text = "收货方:";
            // 
            // lblStore
            // 
            this.lblStore.AutoSize = true;
            this.lblStore.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblStore.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblStore.Location = new System.Drawing.Point(116, 62);
            this.lblStore.Name = "lblStore";
            this.lblStore.Size = new System.Drawing.Size(57, 24);
            this.lblStore.TabIndex = 2;
            this.lblStore.Text = "H20B";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel3.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel3.Location = new System.Drawing.Point(24, 24);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(86, 24);
            this.metroLabel3.TabIndex = 1;
            this.metroLabel3.Text = "下架单号:";
            // 
            // lblOutlog
            // 
            this.lblOutlog.AutoSize = true;
            this.lblOutlog.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblOutlog.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblOutlog.Location = new System.Drawing.Point(116, 24);
            this.lblOutlog.Name = "lblOutlog";
            this.lblOutlog.Size = new System.Drawing.Size(120, 24);
            this.lblOutlog.TabIndex = 2;
            this.lblOutlog.Text = "1016045363";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "预计(辅物料)";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "实际(辅物料)";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "差异(辅物料)";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // OutLogDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 587);
            this.Controls.Add(this.splitContainer1);
            this.Name = "OutLogDetailForm";
            this.Padding = new System.Windows.Forms.Padding(20, 65, 20, 22);
            this.Text = "单据信息";
            this.Load += new System.EventHandler(this.OutLogDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DMSkin.Metro.Controls.MetroGrid grid;
        private DMSkin.Metro.Controls.MetroButton btnCancel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DMSkin.Metro.Controls.MetroLabel metroLabel2;
        private DMSkin.Metro.Controls.MetroLabel lblDate;
        private DMSkin.Metro.Controls.MetroLabel metroLabel7;
        private DMSkin.Metro.Controls.MetroLabel lblPlanCount;
        private DMSkin.Metro.Controls.MetroLabel metroLabel13;
        private DMSkin.Metro.Controls.MetroLabel lblDifferent;
        private DMSkin.Metro.Controls.MetroLabel metroLabel11;
        private DMSkin.Metro.Controls.MetroLabel lblRealCount;
        private DMSkin.Metro.Controls.MetroLabel metroLabel9;
        private DMSkin.Metro.Controls.MetroLabel lblLGNUM;
        private DMSkin.Metro.Controls.MetroLabel metroLabel5;
        private DMSkin.Metro.Controls.MetroLabel lblStore;
        private DMSkin.Metro.Controls.MetroLabel metroLabel3;
        private DMSkin.Metro.Controls.MetroLabel lblOutlog;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZSATNR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZCOLSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZSIZTX;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLANQTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn REALQTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn DIFFERENT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}