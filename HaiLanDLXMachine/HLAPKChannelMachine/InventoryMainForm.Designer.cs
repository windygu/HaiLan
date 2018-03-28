﻿namespace HLAPKChannelMachine
{
    partial class InventoryMainForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryMainForm));
            this.gridDeliverErrorBox = new DMSkin.Metro.Controls.MetroGrid();
            this.EB_PARTNER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EB_HU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EB_PICK_TASK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EB_ZSATNR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EB_ZCOLSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EB_ZSIZTX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EB_REAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EB_DIFF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EB_REMARK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblShipDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDeviceNo = new System.Windows.Forms.Label();
            this.lblLoginNo = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDetail = new DMSkin.Metro.Controls.MetroButton();
            this.btnClose = new DMSkin.Metro.Controls.MetroButton();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblScanNum = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblErrorNum = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblRightNum = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblHU = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pbLoading = new System.Windows.Forms.PictureBox();
            this.lblLoading = new System.Windows.Forms.Label();
            this.panelLoading = new System.Windows.Forms.Panel();
            this.gbTestPanel = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.txtTestEpc = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.txtTestBoxNo = new System.Windows.Forms.TextBox();
            this.btnTestStop = new System.Windows.Forms.Button();
            this.btnStop = new DMSkin.Metro.Controls.MetroButton();
            this.btnTestStart = new System.Windows.Forms.Button();
            this.btnSearch = new DMSkin.Metro.Controls.MetroButton();
            this.txtImportBoxNo = new DMSkin.Metro.Controls.MetroTextBox();
            this.btnGetData = new DMSkin.Metro.Controls.MetroButton();
            this.metroTile1 = new DMSkin.Metro.Controls.MetroTile();
            this.metroTile2 = new DMSkin.Metro.Controls.MetroTile();
            this.btnStart = new DMSkin.Metro.Controls.MetroButton();
            this.lblLOUCENG = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblReaderStatus = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblPLCStatus = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblResultMessage = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1_dj = new System.Windows.Forms.Button();
            this.uploadButton = new DMSkin.Metro.Controls.MetroButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridDeliverErrorBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).BeginInit();
            this.panelLoading.SuspendLayout();
            this.gbTestPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridDeliverErrorBox
            // 
            this.gridDeliverErrorBox.AllowUserToAddRows = false;
            this.gridDeliverErrorBox.AllowUserToDeleteRows = false;
            this.gridDeliverErrorBox.AllowUserToResizeRows = false;
            this.gridDeliverErrorBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDeliverErrorBox.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridDeliverErrorBox.BackgroundColor = System.Drawing.Color.White;
            this.gridDeliverErrorBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridDeliverErrorBox.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.gridDeliverErrorBox.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridDeliverErrorBox.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridDeliverErrorBox.ColumnHeadersHeight = 43;
            this.gridDeliverErrorBox.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
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
            this.gridDeliverErrorBox.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridDeliverErrorBox.EnableHeadersVisualStyles = false;
            this.gridDeliverErrorBox.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridDeliverErrorBox.GridColor = System.Drawing.Color.DarkGray;
            this.gridDeliverErrorBox.Location = new System.Drawing.Point(263, 43);
            this.gridDeliverErrorBox.MultiSelect = false;
            this.gridDeliverErrorBox.Name = "gridDeliverErrorBox";
            this.gridDeliverErrorBox.ReadOnly = true;
            this.gridDeliverErrorBox.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.gridDeliverErrorBox.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridDeliverErrorBox.RowHeadersVisible = false;
            this.gridDeliverErrorBox.RowHeadersWidth = 43;
            this.gridDeliverErrorBox.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridDeliverErrorBox.RowTemplate.Height = 43;
            this.gridDeliverErrorBox.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridDeliverErrorBox.Size = new System.Drawing.Size(753, 715);
            this.gridDeliverErrorBox.TabIndex = 3;
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
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(255, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(2, 763);
            this.label1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(6, 254);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 40);
            this.label2.TabIndex = 5;
            this.label2.Text = "发运日期：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = false;
            // 
            // lblShipDate
            // 
            this.lblShipDate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblShipDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblShipDate.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblShipDate.Location = new System.Drawing.Point(107, 254);
            this.lblShipDate.Name = "lblShipDate";
            this.lblShipDate.Size = new System.Drawing.Size(140, 40);
            this.lblShipDate.TabIndex = 6;
            this.lblShipDate.Text = "XXX";
            this.lblShipDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblShipDate.Visible = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label4.Location = new System.Drawing.Point(6, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 40);
            this.label4.TabIndex = 7;
            this.label4.Text = "设备号：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDeviceNo
            // 
            this.lblDeviceNo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblDeviceNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDeviceNo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblDeviceNo.Location = new System.Drawing.Point(107, 158);
            this.lblDeviceNo.Name = "lblDeviceNo";
            this.lblDeviceNo.Size = new System.Drawing.Size(140, 40);
            this.lblDeviceNo.TabIndex = 8;
            this.lblDeviceNo.Text = "XXX";
            this.lblDeviceNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLoginNo
            // 
            this.lblLoginNo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblLoginNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLoginNo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblLoginNo.Location = new System.Drawing.Point(107, 110);
            this.lblLoginNo.Name = "lblLoginNo";
            this.lblLoginNo.Size = new System.Drawing.Size(140, 40);
            this.lblLoginNo.TabIndex = 10;
            this.lblLoginNo.Text = "XXX";
            this.lblLoginNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label7.Location = new System.Drawing.Point(6, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 40);
            this.label7.TabIndex = 9;
            this.label7.Text = "登录工号：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDetail
            // 
            this.btnDetail.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDetail.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnDetail.DM_UseCustomBackColor = true;
            this.btnDetail.DM_UseCustomForeColor = true;
            this.btnDetail.DM_UseSelectable = true;
            this.btnDetail.ForeColor = System.Drawing.Color.Teal;
            this.btnDetail.Location = new System.Drawing.Point(6, 566);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(240, 41);
            this.btnDetail.TabIndex = 11;
            this.btnDetail.Text = "操作组详情";
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnClose.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnClose.DM_UseCustomBackColor = true;
            this.btnClose.DM_UseCustomForeColor = true;
            this.btnClose.DM_UseSelectable = true;
            this.btnClose.ForeColor = System.Drawing.Color.Teal;
            this.btnClose.Location = new System.Drawing.Point(6, 618);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(240, 40);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "退出";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblStatus.Location = new System.Drawing.Point(107, 254);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(140, 40);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "XXXX";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label5.Location = new System.Drawing.Point(6, 254);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 40);
            this.label5.TabIndex = 14;
            this.label5.Text = "工作状态：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label3.Location = new System.Drawing.Point(197, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "扫描数量：";
            this.label3.Visible = false;
            // 
            // lblScanNum
            // 
            this.lblScanNum.AutoSize = true;
            this.lblScanNum.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.lblScanNum.Location = new System.Drawing.Point(266, 170);
            this.lblScanNum.Name = "lblScanNum";
            this.lblScanNum.Size = new System.Drawing.Size(14, 16);
            this.lblScanNum.TabIndex = 15;
            this.lblScanNum.Text = "0";
            this.lblScanNum.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label6.Location = new System.Drawing.Point(197, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "非法数量：";
            // 
            // lblErrorNum
            // 
            this.lblErrorNum.AutoSize = true;
            this.lblErrorNum.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.lblErrorNum.Location = new System.Drawing.Point(266, 186);
            this.lblErrorNum.Name = "lblErrorNum";
            this.lblErrorNum.Size = new System.Drawing.Size(14, 16);
            this.lblErrorNum.TabIndex = 19;
            this.lblErrorNum.Text = "0";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label9.Location = new System.Drawing.Point(6, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 40);
            this.label9.TabIndex = 18;
            this.label9.Text = "PLC状态：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRightNum
            // 
            this.lblRightNum.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.lblRightNum.Location = new System.Drawing.Point(211, 209);
            this.lblRightNum.Name = "lblRightNum";
            this.lblRightNum.Size = new System.Drawing.Size(67, 35);
            this.lblRightNum.TabIndex = 17;
            this.lblRightNum.Text = "0";
            this.lblRightNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label8.Location = new System.Drawing.Point(6, 353);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 40);
            this.label8.TabIndex = 22;
            this.label8.Text = "当前箱号：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHU
            // 
            this.lblHU.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblHU.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHU.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblHU.Location = new System.Drawing.Point(107, 353);
            this.lblHU.Name = "lblHU";
            this.lblHU.Size = new System.Drawing.Size(140, 40);
            this.lblHU.TabIndex = 21;
            this.lblHU.Text = "XXX";
            this.lblHU.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // pbLoading
            // 
            this.pbLoading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLoading.BackColor = System.Drawing.Color.Transparent;
            this.pbLoading.Image = ((System.Drawing.Image)(resources.GetObject("pbLoading.Image")));
            this.pbLoading.Location = new System.Drawing.Point(111, 2);
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(33, 31);
            this.pbLoading.TabIndex = 7;
            this.pbLoading.TabStop = false;
            // 
            // lblLoading
            // 
            this.lblLoading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLoading.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblLoading.Location = new System.Drawing.Point(3, 41);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(233, 23);
            this.lblLoading.TabIndex = 8;
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelLoading
            // 
            this.panelLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelLoading.BackColor = System.Drawing.Color.Teal;
            this.panelLoading.Controls.Add(this.pbLoading);
            this.panelLoading.Controls.Add(this.lblLoading);
            this.panelLoading.Location = new System.Drawing.Point(6, 661);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.Size = new System.Drawing.Size(239, 65);
            this.panelLoading.TabIndex = 23;
            this.panelLoading.Visible = false;
            // 
            // gbTestPanel
            // 
            this.gbTestPanel.Controls.Add(this.label10);
            this.gbTestPanel.Controls.Add(this.button4);
            this.gbTestPanel.Controls.Add(this.txtTestEpc);
            this.gbTestPanel.Controls.Add(this.button3);
            this.gbTestPanel.Controls.Add(this.txtTestBoxNo);
            this.gbTestPanel.Controls.Add(this.btnTestStop);
            this.gbTestPanel.Controls.Add(this.btnStop);
            this.gbTestPanel.Controls.Add(this.btnTestStart);
            this.gbTestPanel.Controls.Add(this.btnSearch);
            this.gbTestPanel.Controls.Add(this.txtImportBoxNo);
            this.gbTestPanel.Controls.Add(this.label3);
            this.gbTestPanel.Controls.Add(this.lblScanNum);
            this.gbTestPanel.Controls.Add(this.label6);
            this.gbTestPanel.Controls.Add(this.lblErrorNum);
            this.gbTestPanel.Controls.Add(this.lblRightNum);
            this.gbTestPanel.Location = new System.Drawing.Point(582, 263);
            this.gbTestPanel.Name = "gbTestPanel";
            this.gbTestPanel.Size = new System.Drawing.Size(284, 387);
            this.gbTestPanel.TabIndex = 26;
            this.gbTestPanel.TabStop = false;
            this.gbTestPanel.Text = "模拟数据";
            this.gbTestPanel.Visible = false;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label10.Location = new System.Drawing.Point(6, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 35);
            this.label10.TabIndex = 34;
            this.label10.Text = "MOVE";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label10.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label10_MouseDown);
            this.label10.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label10_MouseMove);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(200, 102);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(62, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "读取epc";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtTestEpc
            // 
            this.txtTestEpc.Location = new System.Drawing.Point(15, 94);
            this.txtTestEpc.Multiline = true;
            this.txtTestEpc.Name = "txtTestEpc";
            this.txtTestEpc.Size = new System.Drawing.Size(179, 231);
            this.txtTestEpc.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(198, 67);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(62, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "插入箱码";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtTestBoxNo
            // 
            this.txtTestBoxNo.Location = new System.Drawing.Point(15, 67);
            this.txtTestBoxNo.Name = "txtTestBoxNo";
            this.txtTestBoxNo.Size = new System.Drawing.Size(177, 21);
            this.txtTestBoxNo.TabIndex = 2;
            // 
            // btnTestStop
            // 
            this.btnTestStop.Location = new System.Drawing.Point(192, 18);
            this.btnTestStop.Name = "btnTestStop";
            this.btnTestStop.Size = new System.Drawing.Size(68, 36);
            this.btnTestStop.TabIndex = 1;
            this.btnTestStop.Text = "停止盘点";
            this.btnTestStop.UseVisualStyleBackColor = true;
            this.btnTestStop.Click += new System.EventHandler(this.btnTestStop_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnStop.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnStop.DM_UseCustomBackColor = true;
            this.btnStop.DM_UseCustomForeColor = true;
            this.btnStop.DM_UseSelectable = true;
            this.btnStop.Enabled = false;
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(200, 144);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(62, 23);
            this.btnStop.TabIndex = 33;
            this.btnStop.Text = "暂停";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnTestStart
            // 
            this.btnTestStart.Location = new System.Drawing.Point(106, 18);
            this.btnTestStart.Name = "btnTestStart";
            this.btnTestStart.Size = new System.Drawing.Size(68, 36);
            this.btnTestStart.TabIndex = 0;
            this.btnTestStart.Text = "开始盘点";
            this.btnTestStart.UseVisualStyleBackColor = true;
            this.btnTestStart.Click += new System.EventHandler(this.btnTestStart_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSearch.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnSearch.DM_UseCustomBackColor = true;
            this.btnSearch.DM_UseCustomForeColor = true;
            this.btnSearch.DM_UseSelectable = true;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(168, 345);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 36);
            this.btnSearch.TabIndex = 28;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtImportBoxNo
            // 
            this.txtImportBoxNo.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Medium;
            this.txtImportBoxNo.DM_UseSelectable = true;
            this.txtImportBoxNo.Lines = new string[0];
            this.txtImportBoxNo.Location = new System.Drawing.Point(15, 345);
            this.txtImportBoxNo.MaxLength = 32767;
            this.txtImportBoxNo.Name = "txtImportBoxNo";
            this.txtImportBoxNo.PasswordChar = '\0';
            this.txtImportBoxNo.PromptText = "扫描箱码";
            this.txtImportBoxNo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtImportBoxNo.SelectedText = "";
            this.txtImportBoxNo.Size = new System.Drawing.Size(126, 30);
            this.txtImportBoxNo.TabIndex = 27;
            this.txtImportBoxNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtImportBoxNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtImportBoxNo_KeyPress);
            // 
            // btnGetData
            // 
            this.btnGetData.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnGetData.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnGetData.DM_UseCustomBackColor = true;
            this.btnGetData.DM_UseCustomForeColor = true;
            this.btnGetData.DM_UseSelectable = true;
            this.btnGetData.ForeColor = System.Drawing.Color.Teal;
            this.btnGetData.Location = new System.Drawing.Point(135, 402);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(112, 44);
            this.btnGetData.TabIndex = 32;
            this.btnGetData.Text = "货运数据获取";
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // metroTile1
            // 
            this.metroTile1.ActiveControl = null;
            this.metroTile1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroTile1.BackColor = System.Drawing.Color.White;
            this.metroTile1.DM_TileTextDM_FontSize = DMSkin.Metro.MetroTileTextSize.Tall;
            this.metroTile1.DM_TileTextDM_FontWeight = DMSkin.Metro.MetroTileTextWeight.Bold;
            this.metroTile1.DM_UseCustomBackColor = true;
            this.metroTile1.DM_UseCustomForeColor = true;
            this.metroTile1.DM_UseSelectable = true;
            this.metroTile1.DM_UseStyleColors = true;
            this.metroTile1.ForeColor = System.Drawing.Color.Teal;
            this.metroTile1.Location = new System.Drawing.Point(257, 5);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(769, 32);
            this.metroTile1.TabIndex = 29;
            this.metroTile1.Text = "发货包装明细";
            this.metroTile1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroTile2
            // 
            this.metroTile2.ActiveControl = null;
            this.metroTile2.BackColor = System.Drawing.Color.White;
            this.metroTile2.DM_TileTextDM_FontSize = DMSkin.Metro.MetroTileTextSize.Tall;
            this.metroTile2.DM_TileTextDM_FontWeight = DMSkin.Metro.MetroTileTextWeight.Bold;
            this.metroTile2.DM_UseCustomBackColor = true;
            this.metroTile2.DM_UseCustomForeColor = true;
            this.metroTile2.DM_UseSelectable = true;
            this.metroTile2.DM_UseStyleColors = true;
            this.metroTile2.ForeColor = System.Drawing.Color.Teal;
            this.metroTile2.Location = new System.Drawing.Point(0, 5);
            this.metroTile2.Name = "metroTile2";
            this.metroTile2.Size = new System.Drawing.Size(255, 32);
            this.metroTile2.TabIndex = 30;
            this.metroTile2.Text = "状态监控";
            this.metroTile2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnStart.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnStart.DM_UseCustomBackColor = true;
            this.btnStart.DM_UseCustomForeColor = true;
            this.btnStart.DM_UseSelectable = true;
            this.btnStart.DM_UseStyleColors = true;
            this.btnStart.ForeColor = System.Drawing.Color.Teal;
            this.btnStart.Location = new System.Drawing.Point(6, 402);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(111, 44);
            this.btnStart.TabIndex = 34;
            this.btnStart.Text = "开始";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblLOUCENG
            // 
            this.lblLOUCENG.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblLOUCENG.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLOUCENG.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblLOUCENG.Location = new System.Drawing.Point(107, 204);
            this.lblLOUCENG.Name = "lblLOUCENG";
            this.lblLOUCENG.Size = new System.Drawing.Size(140, 40);
            this.lblLOUCENG.TabIndex = 36;
            this.lblLOUCENG.Text = "XXX";
            this.lblLOUCENG.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label12.Location = new System.Drawing.Point(6, 204);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 40);
            this.label12.TabIndex = 35;
            this.label12.Text = "作业楼层：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReaderStatus
            // 
            this.lblReaderStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblReaderStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblReaderStatus.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblReaderStatus.Location = new System.Drawing.Point(107, 12);
            this.lblReaderStatus.Name = "lblReaderStatus";
            this.lblReaderStatus.Size = new System.Drawing.Size(140, 40);
            this.lblReaderStatus.TabIndex = 37;
            this.lblReaderStatus.Text = "XXXX";
            this.lblReaderStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label14.Location = new System.Drawing.Point(6, 12);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(95, 40);
            this.label14.TabIndex = 38;
            this.label14.Text = "读写器：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPLCStatus
            // 
            this.lblPLCStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblPLCStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPLCStatus.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblPLCStatus.Location = new System.Drawing.Point(107, 60);
            this.lblPLCStatus.Name = "lblPLCStatus";
            this.lblPLCStatus.Size = new System.Drawing.Size(140, 40);
            this.lblPLCStatus.TabIndex = 39;
            this.lblPLCStatus.Text = "XXXX";
            this.lblPLCStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label16.Location = new System.Drawing.Point(6, 304);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(95, 40);
            this.label16.TabIndex = 41;
            this.label16.Text = "扫描结果：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResultMessage
            // 
            this.lblResultMessage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblResultMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResultMessage.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblResultMessage.Location = new System.Drawing.Point(107, 304);
            this.lblResultMessage.Name = "lblResultMessage";
            this.lblResultMessage.Size = new System.Drawing.Size(140, 40);
            this.lblResultMessage.TabIndex = 40;
            this.lblResultMessage.Text = "XXXX";
            this.lblResultMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Teal;
            this.groupBox1.Controls.Add(this.button1_dj);
            this.groupBox1.Controls.Add(this.uploadButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblResultMessage);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblPLCStatus);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblReaderStatus);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblLOUCENG);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.btnGetData);
            this.groupBox1.Controls.Add(this.lblDeviceNo);
            this.groupBox1.Controls.Add(this.lblLoginNo);
            this.groupBox1.Controls.Add(this.panelLoading);
            this.groupBox1.Controls.Add(this.btnDetail);
            this.groupBox1.Controls.Add(this.lblHU);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.lblShipDate);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Location = new System.Drawing.Point(0, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 726);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            // 
            // button1_dj
            // 
            this.button1_dj.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1_dj.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1_dj.ForeColor = System.Drawing.Color.Teal;
            this.button1_dj.Location = new System.Drawing.Point(6, 506);
            this.button1_dj.Name = "button1_dj";
            this.button1_dj.Size = new System.Drawing.Size(241, 50);
            this.button1_dj.TabIndex = 45;
            this.button1_dj.Text = "短拣";
            this.button1_dj.UseVisualStyleBackColor = false;
            this.button1_dj.Click += new System.EventHandler(this.button1_dj_Click);
            // 
            // uploadButton
            // 
            this.uploadButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.uploadButton.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.uploadButton.DM_UseCustomBackColor = true;
            this.uploadButton.DM_UseCustomForeColor = true;
            this.uploadButton.DM_UseSelectable = true;
            this.uploadButton.DM_UseStyleColors = true;
            this.uploadButton.ForeColor = System.Drawing.Color.Teal;
            this.uploadButton.Location = new System.Drawing.Point(6, 454);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(241, 44);
            this.uploadButton.TabIndex = 43;
            this.uploadButton.Text = "上传列表";
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // InventoryMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbTestPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.metroTile2);
            this.Controls.Add(this.metroTile1);
            this.Controls.Add(this.gridDeliverErrorBox);
            this.Name = "InventoryMainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InventoryMainForm_FormClosing);
            this.Load += new System.EventHandler(this.InventoryMainForm_Load);
            this.Shown += new System.EventHandler(this.InventoryMainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gridDeliverErrorBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).EndInit();
            this.panelLoading.ResumeLayout(false);
            this.gbTestPanel.ResumeLayout(false);
            this.gbTestPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DMSkin.Metro.Controls.MetroGrid gridDeliverErrorBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblShipDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDeviceNo;
        private System.Windows.Forms.Label lblLoginNo;
        private System.Windows.Forms.Label label7;
        private DMSkin.Metro.Controls.MetroButton btnDetail;
        private DMSkin.Metro.Controls.MetroButton btnClose;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblScanNum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblErrorNum;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblRightNum;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblHU;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox pbLoading;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.GroupBox gbTestPanel;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtTestEpc;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtTestBoxNo;
        private System.Windows.Forms.Button btnTestStop;
        private System.Windows.Forms.Button btnTestStart;
        private DMSkin.Metro.Controls.MetroTextBox txtImportBoxNo;
        private DMSkin.Metro.Controls.MetroButton btnSearch;
        private DMSkin.Metro.Controls.MetroTile metroTile1;
        private DMSkin.Metro.Controls.MetroTile metroTile2;
        private DMSkin.Metro.Controls.MetroButton btnGetData;
        private DMSkin.Metro.Controls.MetroButton btnStart;
        private DMSkin.Metro.Controls.MetroButton btnStop;
        private System.Windows.Forms.Label lblLOUCENG;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblReaderStatus;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblPLCStatus;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblResultMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_PARTNER;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_HU;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_PICK_TASK;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_ZSATNR;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_ZCOLSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_ZSIZTX;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_REAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_DIFF;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_REMARK;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer timer1;
        private DMSkin.Metro.Controls.MetroButton uploadButton;
        private System.Windows.Forms.Button button1_dj;
    }
}

