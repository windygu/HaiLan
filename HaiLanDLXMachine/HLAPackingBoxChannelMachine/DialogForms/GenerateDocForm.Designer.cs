namespace HLAPackingBoxChannelMachine.DialogForms
{
    partial class GenerateDocForm
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNo = new DMSkin.Controls.DMButton();
            this.btnYes = new DMSkin.Controls.DMButton();
            this.label2 = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.lblRight = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PACKDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PACKMSG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.ColumnHeadersHeight = 43;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PACKDATE,
            this.LH,
            this.HU,
            this.PACKMSG});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle3;
            this.grid.Dock = System.Windows.Forms.DockStyle.Top;
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
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(736, 316);
            this.grid.TabIndex = 8;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(163)))), ((int)(((byte)(203)))));
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatus.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(20, 376);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(736, 33);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "包装状态无异常，是否生成交接单?";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnNo);
            this.panel1.Controls.Add(this.btnYes);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblError);
            this.panel1.Controls.Add(this.lblRight);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 409);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(736, 84);
            this.panel1.TabIndex = 10;
            // 
            // btnNo
            // 
            this.btnNo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnNo.AutoEllipsis = true;
            this.btnNo.BackColor = System.Drawing.Color.Transparent;
            this.btnNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNo.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnNo.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btnNo.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(78)))), ((int)(((byte)(34)))));
            this.btnNo.DM_NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(122)))), ((int)(((byte)(34)))));
            this.btnNo.DM_Radius = 1;
            this.btnNo.Enabled = false;
            this.btnNo.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNo.Font = new System.Drawing.Font("微软雅黑 Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNo.ForeColor = System.Drawing.Color.White;
            this.btnNo.Image = null;
            this.btnNo.Location = new System.Drawing.Point(542, 11);
            this.btnNo.Margin = new System.Windows.Forms.Padding(0);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(180, 60);
            this.btnNo.TabIndex = 15;
            this.btnNo.Text = "否";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnYes
            // 
            this.btnYes.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnYes.AutoEllipsis = true;
            this.btnYes.BackColor = System.Drawing.Color.Transparent;
            this.btnYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYes.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnYes.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.btnYes.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.btnYes.DM_NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(163)))), ((int)(((byte)(203)))));
            this.btnYes.DM_Radius = 1;
            this.btnYes.Enabled = false;
            this.btnYes.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnYes.Font = new System.Drawing.Font("微软雅黑 Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnYes.ForeColor = System.Drawing.Color.White;
            this.btnYes.Image = null;
            this.btnYes.Location = new System.Drawing.Point(352, 11);
            this.btnYes.Margin = new System.Windows.Forms.Padding(0);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(180, 60);
            this.btnYes.TabIndex = 15;
            this.btnYes.Text = "是";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label2.Location = new System.Drawing.Point(20, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "异常箱数";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblError.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblError.Location = new System.Drawing.Point(114, 46);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(23, 25);
            this.lblError.TabIndex = 0;
            this.lblError.Text = "0";
            // 
            // lblRight
            // 
            this.lblRight.AutoSize = true;
            this.lblRight.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(163)))), ((int)(((byte)(203)))));
            this.lblRight.Location = new System.Drawing.Point(114, 12);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(23, 25);
            this.lblRight.TabIndex = 0;
            this.lblRight.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label1.Location = new System.Drawing.Point(20, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "正常箱数";
            // 
            // PACKDATE
            // 
            this.PACKDATE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.PACKDATE.FillWeight = 40F;
            this.PACKDATE.HeaderText = "日期";
            this.PACKDATE.Name = "PACKDATE";
            this.PACKDATE.ReadOnly = true;
            this.PACKDATE.Width = 76;
            // 
            // LH
            // 
            this.LH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.LH.FillWeight = 40F;
            this.LH.HeaderText = "交接楼号";
            this.LH.Name = "LH";
            this.LH.ReadOnly = true;
            this.LH.Width = 116;
            // 
            // HU
            // 
            this.HU.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.HU.FillWeight = 50F;
            this.HU.HeaderText = "箱号";
            this.HU.Name = "HU";
            this.HU.ReadOnly = true;
            this.HU.Width = 76;
            // 
            // PACKMSG
            // 
            this.PACKMSG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.PACKMSG.DefaultCellStyle = dataGridViewCellStyle2;
            this.PACKMSG.HeaderText = "返回信息";
            this.PACKMSG.Name = "PACKMSG";
            this.PACKMSG.ReadOnly = true;
            this.PACKMSG.Width = 116;
            // 
            // GenerateDocForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 513);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.grid);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenerateDocForm";
            this.Text = "生成交接单";
            this.TextAlign = DMSkin.MetroFormTextAlign.Center;
            this.Load += new System.EventHandler(this.GenerateDocForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DMSkin.Metro.Controls.MetroGrid grid;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Label label1;
        private DMSkin.Controls.DMButton btnNo;
        private DMSkin.Controls.DMButton btnYes;
        private System.Windows.Forms.DataGridViewTextBoxColumn PACKDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn LH;
        private System.Windows.Forms.DataGridViewTextBoxColumn HU;
        private System.Windows.Forms.DataGridViewTextBoxColumn PACKMSG;
    }
}