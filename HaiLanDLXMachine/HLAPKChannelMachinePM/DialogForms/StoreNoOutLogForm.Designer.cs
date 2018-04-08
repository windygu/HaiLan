namespace HLADeliverChannelMachine.DialogForms
{
    partial class StoreNoOutLogForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblTotalCount = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel2 = new DMSkin.Metro.Controls.MetroLabel();
            this.lblOutLogCount = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel15 = new DMSkin.Metro.Controls.MetroLabel();
            this.btnCancel = new DMSkin.Metro.Controls.MetroButton();
            this.PICK_TASK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SHIP_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LGTYP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LGTYP_R = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PLANQTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.PICK_TASK,
            this.SHIP_DATE,
            this.LGTYP,
            this.LGTYP_R,
            this.PLANQTY});
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
            this.grid.Size = new System.Drawing.Size(735, 323);
            this.grid.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(20, 60);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblTotalCount);
            this.splitContainer1.Panel2.Controls.Add(this.metroLabel2);
            this.splitContainer1.Panel2.Controls.Add(this.lblOutLogCount);
            this.splitContainer1.Panel2.Controls.Add(this.metroLabel15);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Size = new System.Drawing.Size(735, 372);
            this.splitContainer1.SplitterDistance = 323;
            this.splitContainer1.TabIndex = 4;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblTotalCount.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblTotalCount.Location = new System.Drawing.Point(240, 11);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(21, 24);
            this.lblTotalCount.TabIndex = 8;
            this.lblTotalCount.Text = "0";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel2.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel2.Location = new System.Drawing.Point(166, 11);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(68, 24);
            this.metroLabel2.TabIndex = 9;
            this.metroLabel2.Text = "总数量:";
            // 
            // lblOutLogCount
            // 
            this.lblOutLogCount.AutoSize = true;
            this.lblOutLogCount.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblOutLogCount.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblOutLogCount.Location = new System.Drawing.Point(98, 11);
            this.lblOutLogCount.Name = "lblOutLogCount";
            this.lblOutLogCount.Size = new System.Drawing.Size(21, 24);
            this.lblOutLogCount.TabIndex = 6;
            this.lblOutLogCount.Text = "0";
            // 
            // metroLabel15
            // 
            this.metroLabel15.AutoSize = true;
            this.metroLabel15.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel15.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel15.Location = new System.Drawing.Point(6, 11);
            this.metroLabel15.Name = "metroLabel15";
            this.metroLabel15.Size = new System.Drawing.Size(86, 24);
            this.metroLabel15.TabIndex = 7;
            this.metroLabel15.Text = "单据总数:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnCancel.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnCancel.DM_UseCustomBackColor = true;
            this.btnCancel.DM_UseCustomForeColor = true;
            this.btnCancel.DM_UseSelectable = true;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(645, 8);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "返回";
            this.btnCancel.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PICK_TASK
            // 
            this.PICK_TASK.HeaderText = "单号";
            this.PICK_TASK.Name = "PICK_TASK";
            this.PICK_TASK.ReadOnly = true;
            // 
            // SHIP_DATE
            // 
            this.SHIP_DATE.HeaderText = "单据日期";
            this.SHIP_DATE.Name = "SHIP_DATE";
            this.SHIP_DATE.ReadOnly = true;
            // 
            // LGTYP
            // 
            this.LGTYP.HeaderText = "原存储类型";
            this.LGTYP.Name = "LGTYP";
            this.LGTYP.ReadOnly = true;
            // 
            // LGTYP_R
            // 
            this.LGTYP_R.DataPropertyName = "LGTYP_R";
            this.LGTYP_R.HeaderText = "实际存储类型";
            this.LGTYP_R.Name = "LGTYP_R";
            this.LGTYP_R.ReadOnly = true;
            // 
            // PLANQTY
            // 
            this.PLANQTY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PLANQTY.HeaderText = "预计数量";
            this.PLANQTY.Name = "PLANQTY";
            this.PLANQTY.ReadOnly = true;
            // 
            // StoreNoOutLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 452);
            this.Controls.Add(this.splitContainer1);
            this.Name = "StoreNoOutLogForm";
            this.Text = "[{0}]门店未完成的下架单";
            this.Load += new System.EventHandler(this.StoreNoOutLogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DMSkin.Metro.Controls.MetroGrid grid;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DMSkin.Metro.Controls.MetroButton btnCancel;
        private DMSkin.Metro.Controls.MetroLabel lblTotalCount;
        private DMSkin.Metro.Controls.MetroLabel metroLabel2;
        private DMSkin.Metro.Controls.MetroLabel lblOutLogCount;
        private DMSkin.Metro.Controls.MetroLabel metroLabel15;
        private System.Windows.Forms.DataGridViewTextBoxColumn PICK_TASK;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHIP_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn LGTYP;
        private System.Windows.Forms.DataGridViewTextBoxColumn LGTYP_R;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLANQTY;
    }
}