namespace HLAYKChannelMachine.DialogForms
{
    partial class ErrorBoxForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grid = new DMSkin.Metro.Controls.MetroGrid();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SOURCE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TARGET = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZSATNR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZCOLSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZSIZTX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MSG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uploadAgainButton = new DMSkin.Controls.DMButton();
            this.btnClear = new DMSkin.Controls.DMButton();
            this.btnClose = new DMSkin.Controls.DMButton();
            this.allSelButton = new DMSkin.Controls.DMButton();
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
            this.splitContainer1.Panel2.Controls.Add(this.allSelButton);
            this.splitContainer1.Panel2.Controls.Add(this.uploadAgainButton);
            this.splitContainer1.Panel2.Controls.Add(this.btnClear);
            this.splitContainer1.Panel2.Controls.Add(this.btnClose);
            this.splitContainer1.Size = new System.Drawing.Size(1155, 647);
            this.splitContainer1.SplitterDistance = 564;
            this.splitContainer1.TabIndex = 0;
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
            this.grid.ColumnHeadersHeight = 65;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.SOURCE,
            this.TARGET,
            this.HU,
            this.ZSATNR,
            this.ZCOLSN,
            this.ZSIZTX,
            this.QTY,
            this.MSG});
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
            this.grid.RowTemplate.Height = 43;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(1155, 564);
            this.grid.TabIndex = 8;
            this.grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellClick);
            // 
            // check
            // 
            this.check.FillWeight = 11.3198F;
            this.check.HeaderText = "";
            this.check.Name = "check";
            this.check.ReadOnly = true;
            // 
            // SOURCE
            // 
            this.SOURCE.FillWeight = 25.81466F;
            this.SOURCE.HeaderText = "源存储类型";
            this.SOURCE.Name = "SOURCE";
            this.SOURCE.ReadOnly = true;
            // 
            // TARGET
            // 
            this.TARGET.FillWeight = 25.81466F;
            this.TARGET.HeaderText = "目标存储类型";
            this.TARGET.Name = "TARGET";
            this.TARGET.ReadOnly = true;
            // 
            // HU
            // 
            this.HU.FillWeight = 22.37271F;
            this.HU.HeaderText = "箱号";
            this.HU.Name = "HU";
            this.HU.ReadOnly = true;
            // 
            // ZSATNR
            // 
            this.ZSATNR.FillWeight = 25.81466F;
            this.ZSATNR.HeaderText = "品号";
            this.ZSATNR.Name = "ZSATNR";
            this.ZSATNR.ReadOnly = true;
            // 
            // ZCOLSN
            // 
            this.ZCOLSN.FillWeight = 12.04684F;
            this.ZCOLSN.HeaderText = "色号";
            this.ZCOLSN.Name = "ZCOLSN";
            this.ZCOLSN.ReadOnly = true;
            // 
            // ZSIZTX
            // 
            this.ZSIZTX.FillWeight = 22.37271F;
            this.ZSIZTX.HeaderText = "规格";
            this.ZSIZTX.Name = "ZSIZTX";
            this.ZSIZTX.ReadOnly = true;
            // 
            // QTY
            // 
            this.QTY.FillWeight = 12.04684F;
            this.QTY.HeaderText = "数量";
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            // 
            // MSG
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.MSG.DefaultCellStyle = dataGridViewCellStyle2;
            this.MSG.FillWeight = 65.39714F;
            this.MSG.HeaderText = "异常信息";
            this.MSG.Name = "MSG";
            this.MSG.ReadOnly = true;
            // 
            // uploadAgainButton
            // 
            this.uploadAgainButton.AutoEllipsis = true;
            this.uploadAgainButton.BackColor = System.Drawing.Color.Transparent;
            this.uploadAgainButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uploadAgainButton.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.uploadAgainButton.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.uploadAgainButton.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.uploadAgainButton.DM_NormalColor = System.Drawing.Color.Gray;
            this.uploadAgainButton.DM_Radius = 1;
            this.uploadAgainButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.uploadAgainButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.uploadAgainButton.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.uploadAgainButton.ForeColor = System.Drawing.Color.DarkBlue;
            this.uploadAgainButton.Image = null;
            this.uploadAgainButton.Location = new System.Drawing.Point(762, -1);
            this.uploadAgainButton.Margin = new System.Windows.Forms.Padding(0);
            this.uploadAgainButton.Name = "uploadAgainButton";
            this.uploadAgainButton.Size = new System.Drawing.Size(159, 79);
            this.uploadAgainButton.TabIndex = 17;
            this.uploadAgainButton.Text = "重新上传";
            this.uploadAgainButton.UseVisualStyleBackColor = true;
            this.uploadAgainButton.Click += new System.EventHandler(this.uploadAgainButton_Click);
            // 
            // btnClear
            // 
            this.btnClear.AutoEllipsis = true;
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnClear.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.btnClear.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.btnClear.DM_NormalColor = System.Drawing.Color.Gray;
            this.btnClear.DM_Radius = 1;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnClear.Image = null;
            this.btnClear.Location = new System.Drawing.Point(0, 0);
            this.btnClear.Margin = new System.Windows.Forms.Padding(0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(188, 79);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "清除异常";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.AutoEllipsis = true;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnClose.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(0)))));
            this.btnClose.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(60)))), ((int)(((byte)(0)))));
            this.btnClose.DM_NormalColor = System.Drawing.Color.Gray;
            this.btnClose.DM_Radius = 1;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnClose.Image = null;
            this.btnClose.Location = new System.Drawing.Point(1002, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(153, 79);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // allSelButton
            // 
            this.allSelButton.AutoEllipsis = true;
            this.allSelButton.BackColor = System.Drawing.Color.Transparent;
            this.allSelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.allSelButton.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.allSelButton.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.allSelButton.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.allSelButton.DM_NormalColor = System.Drawing.Color.Gray;
            this.allSelButton.DM_Radius = 1;
            this.allSelButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.allSelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.allSelButton.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.allSelButton.ForeColor = System.Drawing.Color.DarkBlue;
            this.allSelButton.Image = null;
            this.allSelButton.Location = new System.Drawing.Point(262, 0);
            this.allSelButton.Margin = new System.Windows.Forms.Padding(0);
            this.allSelButton.Name = "allSelButton";
            this.allSelButton.Size = new System.Drawing.Size(159, 79);
            this.allSelButton.TabIndex = 18;
            this.allSelButton.Text = "全选";
            this.allSelButton.UseVisualStyleBackColor = true;
            this.allSelButton.Click += new System.EventHandler(this.allSelButton_Click);
            // 
            // ErrorBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 727);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorBoxForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "异常箱明细";
            this.TextAlign = DMSkin.MetroFormTextAlign.Center;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ErrorBoxForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DMSkin.Metro.Controls.MetroGrid grid;
        private DMSkin.Controls.DMButton btnClose;
        private DMSkin.Controls.DMButton btnClear;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOURCE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TARGET;
        private System.Windows.Forms.DataGridViewTextBoxColumn HU;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZSATNR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZCOLSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZSIZTX;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn MSG;
        private DMSkin.Controls.DMButton uploadAgainButton;
        private DMSkin.Controls.DMButton allSelButton;
    }
}