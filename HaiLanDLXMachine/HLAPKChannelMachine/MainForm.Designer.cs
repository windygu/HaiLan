namespace HLAPKChannelMachine
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl = new DMSkin.Metro.Controls.MetroTabControl();
            this.page3 = new DMSkin.Metro.Controls.MetroTabPage();
            this.faTotalBoxNum = new System.Windows.Forms.Label();
            this.saoTotalNum = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtShortHU = new DMSkin.Metro.Controls.MetroTextBox();
            this.btnShortConfirm = new DMSkin.Controls.DMButton();
            this.btnQueryShortPick = new DMSkin.Controls.DMButton();
            this.gridShort = new DMSkin.Metro.Controls.MetroGrid();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PICKTASK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsLastBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.page4 = new DMSkin.Metro.Controls.MetroTabPage();
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
            this.txtDeliverHu = new DMSkin.Metro.Controls.MetroTextBox();
            this.btnQueryDeliverBox = new DMSkin.Controls.DMButton();
            this.panelLoading = new System.Windows.Forms.Panel();
            this.metroProgressSpinner1 = new DMSkin.Metro.Controls.MetroProgressSpinner();
            this.lblLoading = new System.Windows.Forms.Label();
            this.btnDeliver = new DMSkin.Controls.DMButton();
            this.tabControl.SuspendLayout();
            this.page3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridShort)).BeginInit();
            this.page4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDeliverErrorBox)).BeginInit();
            this.panelLoading.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.page3);
            this.tabControl.Controls.Add(this.page4);
            this.tabControl.DM_FontSize = DMSkin.Metro.MetroTabControlSize.Tall;
            this.tabControl.DM_UseSelectable = true;
            this.tabControl.Location = new System.Drawing.Point(0, 30);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(893, 437);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // page3
            // 
            this.page3.Controls.Add(this.faTotalBoxNum);
            this.page3.Controls.Add(this.saoTotalNum);
            this.page3.Controls.Add(this.label14);
            this.page3.Controls.Add(this.label16);
            this.page3.Controls.Add(this.txtShortHU);
            this.page3.Controls.Add(this.btnShortConfirm);
            this.page3.Controls.Add(this.btnQueryShortPick);
            this.page3.Controls.Add(this.gridShort);
            this.page3.HorizontalScrollbarBarColor = true;
            this.page3.HorizontalScrollbarDM_HighlightOnWheel = false;
            this.page3.HorizontalScrollbarSize = 10;
            this.page3.Location = new System.Drawing.Point(4, 43);
            this.page3.Name = "page3";
            this.page3.Size = new System.Drawing.Size(885, 390);
            this.page3.TabIndex = 2;
            this.page3.Text = "发货短拣";
            this.page3.VerticalScrollbarBarColor = true;
            this.page3.VerticalScrollbarDM_HighlightOnWheel = false;
            this.page3.VerticalScrollbarSize = 10;
            // 
            // faTotalBoxNum
            // 
            this.faTotalBoxNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.faTotalBoxNum.BackColor = System.Drawing.Color.WhiteSmoke;
            this.faTotalBoxNum.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.faTotalBoxNum.ForeColor = System.Drawing.Color.Red;
            this.faTotalBoxNum.Location = new System.Drawing.Point(594, 315);
            this.faTotalBoxNum.Name = "faTotalBoxNum";
            this.faTotalBoxNum.Size = new System.Drawing.Size(86, 32);
            this.faTotalBoxNum.TabIndex = 19;
            this.faTotalBoxNum.Text = "0";
            this.faTotalBoxNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saoTotalNum
            // 
            this.saoTotalNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saoTotalNum.BackColor = System.Drawing.Color.WhiteSmoke;
            this.saoTotalNum.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.saoTotalNum.ForeColor = System.Drawing.Color.Red;
            this.saoTotalNum.Location = new System.Drawing.Point(802, 314);
            this.saoTotalNum.Name = "saoTotalNum";
            this.saoTotalNum.Size = new System.Drawing.Size(80, 32);
            this.saoTotalNum.TabIndex = 20;
            this.saoTotalNum.Text = "0";
            this.saoTotalNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.BackColor = System.Drawing.Color.Gainsboro;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(492, 314);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 32);
            this.label14.TabIndex = 21;
            this.label14.Text = "应发总数：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.BackColor = System.Drawing.Color.Gainsboro;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(706, 314);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(95, 32);
            this.label16.TabIndex = 22;
            this.label16.Text = "实扫总数：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtShortHU
            // 
            this.txtShortHU.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtShortHU.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Tall;
            this.txtShortHU.DM_UseSelectable = true;
            this.txtShortHU.Lines = new string[0];
            this.txtShortHU.Location = new System.Drawing.Point(165, 357);
            this.txtShortHU.MaxLength = 32767;
            this.txtShortHU.Name = "txtShortHU";
            this.txtShortHU.PasswordChar = '\0';
            this.txtShortHU.PromptText = "请扫描箱码";
            this.txtShortHU.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtShortHU.SelectedText = "";
            this.txtShortHU.Size = new System.Drawing.Size(318, 30);
            this.txtShortHU.TabIndex = 17;
            this.txtShortHU.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtShortHU.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtShortHU_KeyPress);
            // 
            // btnShortConfirm
            // 
            this.btnShortConfirm.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnShortConfirm.AutoEllipsis = true;
            this.btnShortConfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnShortConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShortConfirm.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnShortConfirm.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(81)))), ((int)(((byte)(222)))));
            this.btnShortConfirm.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(121)))), ((int)(((byte)(222)))));
            this.btnShortConfirm.DM_NormalColor = System.Drawing.Color.Teal;
            this.btnShortConfirm.DM_Radius = 5;
            this.btnShortConfirm.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnShortConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShortConfirm.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnShortConfirm.ForeColor = System.Drawing.Color.White;
            this.btnShortConfirm.Image = null;
            this.btnShortConfirm.Location = new System.Drawing.Point(603, 354);
            this.btnShortConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.btnShortConfirm.Name = "btnShortConfirm";
            this.btnShortConfirm.Size = new System.Drawing.Size(110, 35);
            this.btnShortConfirm.TabIndex = 16;
            this.btnShortConfirm.Text = "短拣确认";
            this.btnShortConfirm.UseVisualStyleBackColor = false;
            this.btnShortConfirm.Click += new System.EventHandler(this.btnShortConfirm_Click);
            // 
            // btnQueryShortPick
            // 
            this.btnQueryShortPick.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnQueryShortPick.AutoEllipsis = true;
            this.btnQueryShortPick.BackColor = System.Drawing.Color.Transparent;
            this.btnQueryShortPick.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQueryShortPick.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnQueryShortPick.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(81)))), ((int)(((byte)(222)))));
            this.btnQueryShortPick.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(121)))), ((int)(((byte)(222)))));
            this.btnQueryShortPick.DM_NormalColor = System.Drawing.Color.Teal;
            this.btnQueryShortPick.DM_Radius = 5;
            this.btnQueryShortPick.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnQueryShortPick.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnQueryShortPick.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnQueryShortPick.ForeColor = System.Drawing.Color.White;
            this.btnQueryShortPick.Image = null;
            this.btnQueryShortPick.Location = new System.Drawing.Point(489, 354);
            this.btnQueryShortPick.Margin = new System.Windows.Forms.Padding(0);
            this.btnQueryShortPick.Name = "btnQueryShortPick";
            this.btnQueryShortPick.Size = new System.Drawing.Size(110, 35);
            this.btnQueryShortPick.TabIndex = 16;
            this.btnQueryShortPick.Text = "查询";
            this.btnQueryShortPick.UseVisualStyleBackColor = false;
            this.btnQueryShortPick.Click += new System.EventHandler(this.btnQueryShortPick_Click);
            // 
            // gridShort
            // 
            this.gridShort.AllowUserToAddRows = false;
            this.gridShort.AllowUserToDeleteRows = false;
            this.gridShort.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gridShort.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridShort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridShort.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridShort.BackgroundColor = System.Drawing.Color.White;
            this.gridShort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridShort.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.gridShort.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridShort.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridShort.ColumnHeadersHeight = 43;
            this.gridShort.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.PICKTASK,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.IsLastBox});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridShort.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridShort.EnableHeadersVisualStyles = false;
            this.gridShort.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridShort.GridColor = System.Drawing.Color.DarkGray;
            this.gridShort.Location = new System.Drawing.Point(0, 0);
            this.gridShort.MultiSelect = false;
            this.gridShort.Name = "gridShort";
            this.gridShort.ReadOnly = true;
            this.gridShort.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridShort.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridShort.RowHeadersVisible = false;
            this.gridShort.RowHeadersWidth = 43;
            this.gridShort.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridShort.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gridShort.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridShort.RowTemplate.Height = 43;
            this.gridShort.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridShort.Size = new System.Drawing.Size(885, 303);
            this.gridShort.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "箱号";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // PICKTASK
            // 
            this.PICKTASK.HeaderText = "下架单号";
            this.PICKTASK.Name = "PICKTASK";
            this.PICKTASK.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "品号";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.FillWeight = 60F;
            this.dataGridViewTextBoxColumn7.HeaderText = "色号";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn8.HeaderText = "规格";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.FillWeight = 60F;
            this.dataGridViewTextBoxColumn9.HeaderText = "应发数量";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.FillWeight = 60F;
            this.dataGridViewTextBoxColumn10.HeaderText = "实扫数量";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.FillWeight = 60F;
            this.dataGridViewTextBoxColumn11.HeaderText = "短拣数量";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // IsLastBox
            // 
            this.IsLastBox.HeaderText = "是否最后一箱";
            this.IsLastBox.Name = "IsLastBox";
            this.IsLastBox.ReadOnly = true;
            // 
            // page4
            // 
            this.page4.Controls.Add(this.gridDeliverErrorBox);
            this.page4.Controls.Add(this.txtDeliverHu);
            this.page4.Controls.Add(this.btnQueryDeliverBox);
            this.page4.HorizontalScrollbarBarColor = true;
            this.page4.HorizontalScrollbarDM_HighlightOnWheel = false;
            this.page4.HorizontalScrollbarSize = 10;
            this.page4.Location = new System.Drawing.Point(4, 43);
            this.page4.Name = "page4";
            this.page4.Size = new System.Drawing.Size(885, 390);
            this.page4.TabIndex = 3;
            this.page4.Text = "发货历史记录查询";
            this.page4.VerticalScrollbarBarColor = true;
            this.page4.VerticalScrollbarDM_HighlightOnWheel = false;
            this.page4.VerticalScrollbarSize = 10;
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
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridDeliverErrorBox.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
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
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridDeliverErrorBox.DefaultCellStyle = dataGridViewCellStyle8;
            this.gridDeliverErrorBox.EnableHeadersVisualStyles = false;
            this.gridDeliverErrorBox.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridDeliverErrorBox.GridColor = System.Drawing.Color.DarkGray;
            this.gridDeliverErrorBox.Location = new System.Drawing.Point(0, 0);
            this.gridDeliverErrorBox.MultiSelect = false;
            this.gridDeliverErrorBox.Name = "gridDeliverErrorBox";
            this.gridDeliverErrorBox.ReadOnly = true;
            this.gridDeliverErrorBox.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            this.gridDeliverErrorBox.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.gridDeliverErrorBox.RowHeadersVisible = false;
            this.gridDeliverErrorBox.RowHeadersWidth = 43;
            this.gridDeliverErrorBox.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridDeliverErrorBox.RowTemplate.Height = 43;
            this.gridDeliverErrorBox.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridDeliverErrorBox.Size = new System.Drawing.Size(885, 352);
            this.gridDeliverErrorBox.TabIndex = 22;
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
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EB_REMARK.DefaultCellStyle = dataGridViewCellStyle7;
            this.EB_REMARK.HeaderText = "备注";
            this.EB_REMARK.Name = "EB_REMARK";
            this.EB_REMARK.ReadOnly = true;
            this.EB_REMARK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtDeliverHu
            // 
            this.txtDeliverHu.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtDeliverHu.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Tall;
            this.txtDeliverHu.DM_UseSelectable = true;
            this.txtDeliverHu.Lines = new string[0];
            this.txtDeliverHu.Location = new System.Drawing.Point(165, 358);
            this.txtDeliverHu.MaxLength = 32767;
            this.txtDeliverHu.Name = "txtDeliverHu";
            this.txtDeliverHu.PasswordChar = '\0';
            this.txtDeliverHu.PromptText = "请扫描箱码";
            this.txtDeliverHu.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDeliverHu.SelectedText = "";
            this.txtDeliverHu.Size = new System.Drawing.Size(318, 30);
            this.txtDeliverHu.TabIndex = 21;
            this.txtDeliverHu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnQueryDeliverBox
            // 
            this.btnQueryDeliverBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnQueryDeliverBox.AutoEllipsis = true;
            this.btnQueryDeliverBox.BackColor = System.Drawing.Color.Transparent;
            this.btnQueryDeliverBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQueryDeliverBox.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnQueryDeliverBox.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(81)))), ((int)(((byte)(222)))));
            this.btnQueryDeliverBox.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(121)))), ((int)(((byte)(222)))));
            this.btnQueryDeliverBox.DM_NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.btnQueryDeliverBox.DM_Radius = 5;
            this.btnQueryDeliverBox.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnQueryDeliverBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnQueryDeliverBox.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnQueryDeliverBox.ForeColor = System.Drawing.Color.White;
            this.btnQueryDeliverBox.Image = null;
            this.btnQueryDeliverBox.Location = new System.Drawing.Point(489, 355);
            this.btnQueryDeliverBox.Margin = new System.Windows.Forms.Padding(0);
            this.btnQueryDeliverBox.Name = "btnQueryDeliverBox";
            this.btnQueryDeliverBox.Size = new System.Drawing.Size(110, 35);
            this.btnQueryDeliverBox.TabIndex = 20;
            this.btnQueryDeliverBox.Text = "查询";
            this.btnQueryDeliverBox.UseVisualStyleBackColor = false;
            this.btnQueryDeliverBox.Click += new System.EventHandler(this.btnQueryDeliverBox_Click);
            // 
            // panelLoading
            // 
            this.panelLoading.BackColor = System.Drawing.Color.White;
            this.panelLoading.Controls.Add(this.metroProgressSpinner1);
            this.panelLoading.Controls.Add(this.lblLoading);
            this.panelLoading.Controls.Add(this.btnDeliver);
            this.panelLoading.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLoading.Location = new System.Drawing.Point(0, 467);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.Size = new System.Drawing.Size(893, 43);
            this.panelLoading.TabIndex = 12;
            this.panelLoading.Visible = false;
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroProgressSpinner1.BackColor = System.Drawing.Color.Teal;
            this.metroProgressSpinner1.DM_Maximum = 100;
            this.metroProgressSpinner1.DM_UseCustomBackColor = true;
            this.metroProgressSpinner1.DM_UseSelectable = true;
            this.metroProgressSpinner1.DM_Value = 90;
            this.metroProgressSpinner1.Location = new System.Drawing.Point(11, 5);
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(35, 35);
            this.metroProgressSpinner1.Style = DMSkin.Metro.MetroColorStyle.White;
            this.metroProgressSpinner1.TabIndex = 10;
            // 
            // lblLoading
            // 
            this.lblLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLoading.BackColor = System.Drawing.Color.Teal;
            this.lblLoading.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblLoading.ForeColor = System.Drawing.Color.White;
            this.lblLoading.Location = new System.Drawing.Point(53, 9);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(835, 26);
            this.lblLoading.TabIndex = 8;
            this.lblLoading.Text = "Loading";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDeliver
            // 
            this.btnDeliver.AutoEllipsis = true;
            this.btnDeliver.BackColor = System.Drawing.Color.Transparent;
            this.btnDeliver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeliver.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.btnDeliver.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.btnDeliver.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.btnDeliver.DM_NormalColor = System.Drawing.Color.Teal;
            this.btnDeliver.DM_Radius = 1;
            this.btnDeliver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeliver.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnDeliver.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeliver.Font = new System.Drawing.Font("微软雅黑", 24F);
            this.btnDeliver.ForeColor = System.Drawing.Color.White;
            this.btnDeliver.Image = null;
            this.btnDeliver.Location = new System.Drawing.Point(0, 0);
            this.btnDeliver.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeliver.Name = "btnDeliver";
            this.btnDeliver.Size = new System.Drawing.Size(893, 43);
            this.btnDeliver.TabIndex = 15;
            this.btnDeliver.Text = "";
            this.btnDeliver.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 510);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelLoading);
            this.DisplayHeader = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.Resizable = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl.ResumeLayout(false);
            this.page3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridShort)).EndInit();
            this.page4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDeliverErrorBox)).EndInit();
            this.panelLoading.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DMSkin.Metro.Controls.MetroTabControl tabControl;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.Label lblLoading;
        private DMSkin.Metro.Controls.MetroProgressSpinner metroProgressSpinner1;
        private DMSkin.Controls.DMButton btnDeliver;
        private DMSkin.Metro.Controls.MetroTabPage page3;
        private DMSkin.Metro.Controls.MetroGrid gridShort;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn PICKTASK;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsLastBox;
        private DMSkin.Controls.DMButton btnQueryShortPick;
        private DMSkin.Metro.Controls.MetroTextBox txtShortHU;
        private DMSkin.Controls.DMButton btnShortConfirm;
        private DMSkin.Metro.Controls.MetroTabPage page4;
        private DMSkin.Metro.Controls.MetroTextBox txtDeliverHu;
        private DMSkin.Controls.DMButton btnQueryDeliverBox;
        private DMSkin.Metro.Controls.MetroGrid gridDeliverErrorBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_PARTNER;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_HU;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_PICK_TASK;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_ZSATNR;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_ZCOLSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_ZSIZTX;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_REAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_DIFF;
        private System.Windows.Forms.DataGridViewTextBoxColumn EB_REMARK;
        private System.Windows.Forms.Label faTotalBoxNum;
        private System.Windows.Forms.Label saoTotalNum;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
    }
}