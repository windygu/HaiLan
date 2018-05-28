using DMSkin;

namespace HLAYKChannelMachine
{
    partial class InventoryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dmButtonStart = new DMSkin.Controls.DMButton();
            this.dmButtonStop = new DMSkin.Controls.DMButton();
            this.label17 = new System.Windows.Forms.Label();
            this.lblCheckSku = new DMSkin.Controls.DMLabel();
            this.btnCheckSku = new DMSkin.Controls.DMButton();
            this.cboPxmat = new DMSkin.Metro.Controls.MetroComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cboTarget = new DMSkin.Metro.Controls.MetroComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCheckPinSe = new DMSkin.Controls.DMLabel();
            this.lblUsePrint = new DMSkin.Controls.DMLabel();
            this.lblUseBoxStandard = new DMSkin.Controls.DMLabel();
            this.lblUseSize = new DMSkin.Controls.DMLabel();
            this.lblUsePs = new DMSkin.Controls.DMLabel();
            this.btnGenerateDoc = new DMSkin.Controls.DMButton();
            this.btnGx = new DMSkin.Controls.DMButton();
            this.btnCheckPinSe = new DMSkin.Controls.DMButton();
            this.btnUsePrint = new DMSkin.Controls.DMButton();
            this.btnUseBoxStandard = new DMSkin.Controls.DMButton();
            this.btnUseSize = new DMSkin.Controls.DMButton();
            this.btnUsePs = new DMSkin.Controls.DMButton();
            this.cboSource = new DMSkin.Metro.Controls.MetroComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblHu = new System.Windows.Forms.Label();
            this.lblWorkStatus = new System.Windows.Forms.Label();
            this.lblLouceng = new System.Windows.Forms.Label();
            this.lblDeviceNo = new System.Windows.Forms.Label();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new DMSkin.Controls.DMButton();
            this.btnErrorBox = new DMSkin.Controls.DMButton();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPlc = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblReader = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPrinter = new DMSkin.Metro.Controls.MetroTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblTotalBoxNum = new System.Windows.Forms.Label();
            this.lblTotalNum = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panelDebug = new DMSkin.Metro.Controls.MetroPanel();
            this.metroTextBox2 = new DMSkin.Metro.Controls.MetroTextBox();
            this.metroTextBox1 = new DMSkin.Metro.Controls.MetroTextBox();
            this.dmButton3 = new DMSkin.Controls.DMButton();
            this.dmButton5 = new DMSkin.Controls.DMButton();
            this.dmButton2 = new DMSkin.Controls.DMButton();
            this.dmButton4 = new DMSkin.Controls.DMButton();
            this.btnDebug = new DMSkin.Controls.DMButton();
            this.label16 = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.grid = new DMSkin.Metro.Controls.MetroGrid();
            this.SOURCE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TARGET = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZSATNR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZCOLSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZSIZTX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MSG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelDebug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 30);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Teal;
            this.splitContainer1.Panel1.Controls.Add(this.dmButtonStart);
            this.splitContainer1.Panel1.Controls.Add(this.dmButtonStop);
            this.splitContainer1.Panel1.Controls.Add(this.label17);
            this.splitContainer1.Panel1.Controls.Add(this.lblCheckSku);
            this.splitContainer1.Panel1.Controls.Add(this.btnCheckSku);
            this.splitContainer1.Panel1.Controls.Add(this.cboPxmat);
            this.splitContainer1.Panel1.Controls.Add(this.label15);
            this.splitContainer1.Panel1.Controls.Add(this.cboTarget);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.lblCheckPinSe);
            this.splitContainer1.Panel1.Controls.Add(this.lblUsePrint);
            this.splitContainer1.Panel1.Controls.Add(this.lblUseBoxStandard);
            this.splitContainer1.Panel1.Controls.Add(this.lblUseSize);
            this.splitContainer1.Panel1.Controls.Add(this.lblUsePs);
            this.splitContainer1.Panel1.Controls.Add(this.btnGenerateDoc);
            this.splitContainer1.Panel1.Controls.Add(this.btnGx);
            this.splitContainer1.Panel1.Controls.Add(this.btnCheckPinSe);
            this.splitContainer1.Panel1.Controls.Add(this.btnUsePrint);
            this.splitContainer1.Panel1.Controls.Add(this.btnUseBoxStandard);
            this.splitContainer1.Panel1.Controls.Add(this.btnUseSize);
            this.splitContainer1.Panel1.Controls.Add(this.btnUsePs);
            this.splitContainer1.Panel1.Controls.Add(this.cboSource);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.lblHu);
            this.splitContainer1.Panel1.Controls.Add(this.lblWorkStatus);
            this.splitContainer1.Panel1.Controls.Add(this.lblLouceng);
            this.splitContainer1.Panel1.Controls.Add(this.lblDeviceNo);
            this.splitContainer1.Panel1.Controls.Add(this.lblCurrentUser);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.btnErrorBox);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.lblPlc);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.lblReader);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.lblResult);
            this.splitContainer1.Panel1.Controls.Add(this.label11);
            this.splitContainer1.Panel1.Controls.Add(this.txtPrinter);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblTotalBoxNum);
            this.splitContainer1.Panel2.Controls.Add(this.lblTotalNum);
            this.splitContainer1.Panel2.Controls.Add(this.label14);
            this.splitContainer1.Panel2.Controls.Add(this.panelDebug);
            this.splitContainer1.Panel2.Controls.Add(this.label16);
            this.splitContainer1.Panel2.Controls.Add(this.lblQty);
            this.splitContainer1.Panel2.Controls.Add(this.grid);
            this.splitContainer1.Panel2.Controls.Add(this.label13);
            this.splitContainer1.Size = new System.Drawing.Size(1221, 712);
            this.splitContainer1.SplitterDistance = 340;
            this.splitContainer1.TabIndex = 1;
            // 
            // dmButtonStart
            // 
            this.dmButtonStart.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dmButtonStart.AutoEllipsis = true;
            this.dmButtonStart.BackColor = System.Drawing.Color.Transparent;
            this.dmButtonStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dmButtonStart.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.dmButtonStart.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.dmButtonStart.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.dmButtonStart.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.dmButtonStart.DM_Radius = 1;
            this.dmButtonStart.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.dmButtonStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dmButtonStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.dmButtonStart.ForeColor = System.Drawing.Color.Teal;
            this.dmButtonStart.Image = null;
            this.dmButtonStart.Location = new System.Drawing.Point(10, 571);
            this.dmButtonStart.Margin = new System.Windows.Forms.Padding(0);
            this.dmButtonStart.Name = "dmButtonStart";
            this.dmButtonStart.Size = new System.Drawing.Size(155, 40);
            this.dmButtonStart.TabIndex = 55;
            this.dmButtonStart.Text = "开始";
            this.dmButtonStart.UseVisualStyleBackColor = true;
            this.dmButtonStart.Click += new System.EventHandler(this.dmButtonStart_Click);
            // 
            // dmButtonStop
            // 
            this.dmButtonStop.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dmButtonStop.AutoEllipsis = true;
            this.dmButtonStop.BackColor = System.Drawing.Color.Transparent;
            this.dmButtonStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dmButtonStop.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.dmButtonStop.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.dmButtonStop.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.dmButtonStop.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.dmButtonStop.DM_Radius = 1;
            this.dmButtonStop.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.dmButtonStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dmButtonStop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.dmButtonStop.ForeColor = System.Drawing.Color.Teal;
            this.dmButtonStop.Image = null;
            this.dmButtonStop.Location = new System.Drawing.Point(175, 571);
            this.dmButtonStop.Margin = new System.Windows.Forms.Padding(0);
            this.dmButtonStop.Name = "dmButtonStop";
            this.dmButtonStop.Size = new System.Drawing.Size(155, 40);
            this.dmButtonStop.TabIndex = 56;
            this.dmButtonStop.Text = "暂停";
            this.dmButtonStop.UseVisualStyleBackColor = true;
            this.dmButtonStop.Click += new System.EventHandler(this.dmButtonStop_Click);
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.White;
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.Teal;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(338, 40);
            this.label17.TabIndex = 0;
            this.label17.Text = "状态监控";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCheckSku
            // 
            this.lblCheckSku.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCheckSku.BackColor = System.Drawing.Color.White;
            this.lblCheckSku.DM_Color = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(163)))), ((int)(((byte)(203)))));
            this.lblCheckSku.DM_Font_Size = 10F;
            this.lblCheckSku.DM_Key = DMSkin.Controls.DMLabelKey.错误;
            this.lblCheckSku.DM_Text = "";
            this.lblCheckSku.Location = new System.Drawing.Point(201, 533);
            this.lblCheckSku.Name = "lblCheckSku";
            this.lblCheckSku.Size = new System.Drawing.Size(15, 15);
            this.lblCheckSku.TabIndex = 18;
            this.lblCheckSku.Text = "dmLabel1";
            // 
            // btnCheckSku
            // 
            this.btnCheckSku.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCheckSku.AutoEllipsis = true;
            this.btnCheckSku.BackColor = System.Drawing.Color.Transparent;
            this.btnCheckSku.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckSku.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnCheckSku.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.btnCheckSku.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.btnCheckSku.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnCheckSku.DM_Radius = 10;
            this.btnCheckSku.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCheckSku.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheckSku.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnCheckSku.ForeColor = System.Drawing.Color.Black;
            this.btnCheckSku.Image = null;
            this.btnCheckSku.Location = new System.Drawing.Point(175, 521);
            this.btnCheckSku.Margin = new System.Windows.Forms.Padding(0);
            this.btnCheckSku.Name = "btnCheckSku";
            this.btnCheckSku.Size = new System.Drawing.Size(155, 40);
            this.btnCheckSku.TabIndex = 14;
            this.btnCheckSku.Text = "    验证商品数";
            this.btnCheckSku.UseVisualStyleBackColor = true;
            this.btnCheckSku.Click += new System.EventHandler(this.btnCheckSku_Click);
            // 
            // cboPxmat
            // 
            this.cboPxmat.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboPxmat.DM_UseSelectable = true;
            this.cboPxmat.FormattingEnabled = true;
            this.cboPxmat.ItemHeight = 24;
            this.cboPxmat.Location = new System.Drawing.Point(120, 396);
            this.cboPxmat.Name = "cboPxmat";
            this.cboPxmat.Size = new System.Drawing.Size(210, 30);
            this.cboPxmat.TabIndex = 20;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label15.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(10, 396);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(111, 30);
            this.label15.TabIndex = 19;
            this.label15.Text = "包装材料：";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboTarget
            // 
            this.cboTarget.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboTarget.DM_UseSelectable = true;
            this.cboTarget.FormattingEnabled = true;
            this.cboTarget.ItemHeight = 24;
            this.cboTarget.Location = new System.Drawing.Point(120, 363);
            this.cboTarget.Name = "cboTarget";
            this.cboTarget.Size = new System.Drawing.Size(210, 30);
            this.cboTarget.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(10, 363);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 30);
            this.label10.TabIndex = 19;
            this.label10.Text = "目标存储类型：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCheckPinSe
            // 
            this.lblCheckPinSe.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCheckPinSe.BackColor = System.Drawing.Color.White;
            this.lblCheckPinSe.DM_Color = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(163)))), ((int)(((byte)(203)))));
            this.lblCheckPinSe.DM_Font_Size = 10F;
            this.lblCheckPinSe.DM_Key = DMSkin.Controls.DMLabelKey.错误;
            this.lblCheckPinSe.DM_Text = "";
            this.lblCheckPinSe.Location = new System.Drawing.Point(36, 488);
            this.lblCheckPinSe.Name = "lblCheckPinSe";
            this.lblCheckPinSe.Size = new System.Drawing.Size(15, 15);
            this.lblCheckPinSe.TabIndex = 18;
            this.lblCheckPinSe.Text = "dmLabel1";
            this.lblCheckPinSe.Click += new System.EventHandler(this.lblCheckPinSe_Click);
            // 
            // lblUsePrint
            // 
            this.lblUsePrint.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUsePrint.BackColor = System.Drawing.Color.White;
            this.lblUsePrint.DM_Color = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(163)))), ((int)(((byte)(203)))));
            this.lblUsePrint.DM_Font_Size = 10F;
            this.lblUsePrint.DM_Key = DMSkin.Controls.DMLabelKey.错误;
            this.lblUsePrint.DM_Text = "";
            this.lblUsePrint.Location = new System.Drawing.Point(36, 534);
            this.lblUsePrint.Name = "lblUsePrint";
            this.lblUsePrint.Size = new System.Drawing.Size(15, 15);
            this.lblUsePrint.TabIndex = 18;
            this.lblUsePrint.Text = "dmLabel1";
            this.lblUsePrint.Click += new System.EventHandler(this.lblUsePrint_Click);
            // 
            // lblUseBoxStandard
            // 
            this.lblUseBoxStandard.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUseBoxStandard.BackColor = System.Drawing.Color.White;
            this.lblUseBoxStandard.DM_Color = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(163)))), ((int)(((byte)(203)))));
            this.lblUseBoxStandard.DM_Font_Size = 10F;
            this.lblUseBoxStandard.DM_Key = DMSkin.Controls.DMLabelKey.错误;
            this.lblUseBoxStandard.DM_Text = "";
            this.lblUseBoxStandard.Location = new System.Drawing.Point(201, 489);
            this.lblUseBoxStandard.Name = "lblUseBoxStandard";
            this.lblUseBoxStandard.Size = new System.Drawing.Size(15, 15);
            this.lblUseBoxStandard.TabIndex = 18;
            this.lblUseBoxStandard.Text = "dmLabel1";
            this.lblUseBoxStandard.Click += new System.EventHandler(this.lblUseBoxStandard_Click);
            // 
            // lblUseSize
            // 
            this.lblUseSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUseSize.BackColor = System.Drawing.Color.White;
            this.lblUseSize.DM_Color = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(163)))), ((int)(((byte)(203)))));
            this.lblUseSize.DM_Font_Size = 10F;
            this.lblUseSize.DM_Key = DMSkin.Controls.DMLabelKey.错误;
            this.lblUseSize.DM_Text = "";
            this.lblUseSize.Location = new System.Drawing.Point(201, 443);
            this.lblUseSize.Name = "lblUseSize";
            this.lblUseSize.Size = new System.Drawing.Size(15, 15);
            this.lblUseSize.TabIndex = 18;
            this.lblUseSize.Text = "dmLabel1";
            this.lblUseSize.Click += new System.EventHandler(this.lblUseSize_Click);
            // 
            // lblUsePs
            // 
            this.lblUsePs.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUsePs.BackColor = System.Drawing.Color.White;
            this.lblUsePs.DM_Color = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(163)))), ((int)(((byte)(203)))));
            this.lblUsePs.DM_Font_Size = 10F;
            this.lblUsePs.DM_Key = DMSkin.Controls.DMLabelKey.错误;
            this.lblUsePs.DM_Text = "";
            this.lblUsePs.Location = new System.Drawing.Point(36, 443);
            this.lblUsePs.Name = "lblUsePs";
            this.lblUsePs.Size = new System.Drawing.Size(15, 15);
            this.lblUsePs.TabIndex = 18;
            this.lblUsePs.Text = "dmLabel1";
            this.lblUsePs.Click += new System.EventHandler(this.lblUsePs_Click);
            // 
            // btnGenerateDoc
            // 
            this.btnGenerateDoc.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGenerateDoc.AutoEllipsis = true;
            this.btnGenerateDoc.BackColor = System.Drawing.Color.Transparent;
            this.btnGenerateDoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerateDoc.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnGenerateDoc.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.btnGenerateDoc.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.btnGenerateDoc.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnGenerateDoc.DM_Radius = 1;
            this.btnGenerateDoc.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnGenerateDoc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGenerateDoc.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnGenerateDoc.ForeColor = System.Drawing.Color.Teal;
            this.btnGenerateDoc.Image = null;
            this.btnGenerateDoc.Location = new System.Drawing.Point(175, 619);
            this.btnGenerateDoc.Margin = new System.Windows.Forms.Padding(0);
            this.btnGenerateDoc.Name = "btnGenerateDoc";
            this.btnGenerateDoc.Size = new System.Drawing.Size(155, 40);
            this.btnGenerateDoc.TabIndex = 14;
            this.btnGenerateDoc.Text = "交接";
            this.btnGenerateDoc.UseVisualStyleBackColor = true;
            this.btnGenerateDoc.Click += new System.EventHandler(this.btnGenerateDoc_Click);
            // 
            // btnGx
            // 
            this.btnGx.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGx.AutoEllipsis = true;
            this.btnGx.BackColor = System.Drawing.Color.Transparent;
            this.btnGx.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGx.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnGx.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.btnGx.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.btnGx.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnGx.DM_Radius = 1;
            this.btnGx.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnGx.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGx.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnGx.ForeColor = System.Drawing.Color.Teal;
            this.btnGx.Image = null;
            this.btnGx.Location = new System.Drawing.Point(10, 667);
            this.btnGx.Margin = new System.Windows.Forms.Padding(0);
            this.btnGx.Name = "btnGx";
            this.btnGx.Size = new System.Drawing.Size(155, 40);
            this.btnGx.TabIndex = 14;
            this.btnGx.Text = "操作组详情";
            this.btnGx.UseVisualStyleBackColor = true;
            this.btnGx.Click += new System.EventHandler(this.btnGx_Click);
            // 
            // btnCheckPinSe
            // 
            this.btnCheckPinSe.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCheckPinSe.AutoEllipsis = true;
            this.btnCheckPinSe.BackColor = System.Drawing.Color.Transparent;
            this.btnCheckPinSe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckPinSe.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnCheckPinSe.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.btnCheckPinSe.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.btnCheckPinSe.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnCheckPinSe.DM_Radius = 10;
            this.btnCheckPinSe.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCheckPinSe.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheckPinSe.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnCheckPinSe.ForeColor = System.Drawing.Color.Black;
            this.btnCheckPinSe.Image = null;
            this.btnCheckPinSe.Location = new System.Drawing.Point(10, 477);
            this.btnCheckPinSe.Margin = new System.Windows.Forms.Padding(0);
            this.btnCheckPinSe.Name = "btnCheckPinSe";
            this.btnCheckPinSe.Size = new System.Drawing.Size(155, 40);
            this.btnCheckPinSe.TabIndex = 14;
            this.btnCheckPinSe.Text = "    按品色验证";
            this.btnCheckPinSe.UseVisualStyleBackColor = true;
            this.btnCheckPinSe.Click += new System.EventHandler(this.btnCheckPinSe_Click);
            // 
            // btnUsePrint
            // 
            this.btnUsePrint.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUsePrint.AutoEllipsis = true;
            this.btnUsePrint.BackColor = System.Drawing.Color.Transparent;
            this.btnUsePrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUsePrint.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnUsePrint.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.btnUsePrint.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.btnUsePrint.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnUsePrint.DM_Radius = 10;
            this.btnUsePrint.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnUsePrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUsePrint.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnUsePrint.ForeColor = System.Drawing.Color.Black;
            this.btnUsePrint.Image = null;
            this.btnUsePrint.Location = new System.Drawing.Point(10, 522);
            this.btnUsePrint.Margin = new System.Windows.Forms.Padding(0);
            this.btnUsePrint.Name = "btnUsePrint";
            this.btnUsePrint.Size = new System.Drawing.Size(155, 40);
            this.btnUsePrint.TabIndex = 14;
            this.btnUsePrint.Text = "    打印箱标签";
            this.btnUsePrint.UseVisualStyleBackColor = true;
            this.btnUsePrint.Click += new System.EventHandler(this.btnUsePrint_Click);
            // 
            // btnUseBoxStandard
            // 
            this.btnUseBoxStandard.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUseBoxStandard.AutoEllipsis = true;
            this.btnUseBoxStandard.BackColor = System.Drawing.Color.Transparent;
            this.btnUseBoxStandard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUseBoxStandard.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnUseBoxStandard.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.btnUseBoxStandard.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.btnUseBoxStandard.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnUseBoxStandard.DM_Radius = 10;
            this.btnUseBoxStandard.Enabled = false;
            this.btnUseBoxStandard.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnUseBoxStandard.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUseBoxStandard.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnUseBoxStandard.ForeColor = System.Drawing.Color.Black;
            this.btnUseBoxStandard.Image = null;
            this.btnUseBoxStandard.Location = new System.Drawing.Point(175, 476);
            this.btnUseBoxStandard.Margin = new System.Windows.Forms.Padding(0);
            this.btnUseBoxStandard.Name = "btnUseBoxStandard";
            this.btnUseBoxStandard.Size = new System.Drawing.Size(155, 40);
            this.btnUseBoxStandard.TabIndex = 14;
            this.btnUseBoxStandard.Text = "    按箱规收货";
            this.btnUseBoxStandard.UseVisualStyleBackColor = true;
            this.btnUseBoxStandard.Click += new System.EventHandler(this.btnUseBoxStandard_Click);
            // 
            // btnUseSize
            // 
            this.btnUseSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUseSize.AutoEllipsis = true;
            this.btnUseSize.BackColor = System.Drawing.Color.Transparent;
            this.btnUseSize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUseSize.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnUseSize.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.btnUseSize.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.btnUseSize.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnUseSize.DM_Radius = 10;
            this.btnUseSize.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnUseSize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUseSize.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnUseSize.ForeColor = System.Drawing.Color.Black;
            this.btnUseSize.Image = null;
            this.btnUseSize.Location = new System.Drawing.Point(175, 430);
            this.btnUseSize.Margin = new System.Windows.Forms.Padding(0);
            this.btnUseSize.Name = "btnUseSize";
            this.btnUseSize.Size = new System.Drawing.Size(155, 40);
            this.btnUseSize.TabIndex = 14;
            this.btnUseSize.Text = "    按规格装箱";
            this.btnUseSize.UseVisualStyleBackColor = true;
            this.btnUseSize.Click += new System.EventHandler(this.btnUseSize_Click);
            // 
            // btnUsePs
            // 
            this.btnUsePs.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUsePs.AutoEllipsis = true;
            this.btnUsePs.BackColor = System.Drawing.Color.Transparent;
            this.btnUsePs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUsePs.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnUsePs.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.btnUsePs.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.btnUsePs.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnUsePs.DM_Radius = 10;
            this.btnUsePs.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnUsePs.FlatAppearance.BorderSize = 0;
            this.btnUsePs.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUsePs.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnUsePs.ForeColor = System.Drawing.Color.Black;
            this.btnUsePs.Image = null;
            this.btnUsePs.Location = new System.Drawing.Point(10, 430);
            this.btnUsePs.Margin = new System.Windows.Forms.Padding(0);
            this.btnUsePs.Name = "btnUsePs";
            this.btnUsePs.Size = new System.Drawing.Size(155, 40);
            this.btnUsePs.TabIndex = 14;
            this.btnUsePs.Text = "    按品色装箱";
            this.btnUsePs.UseVisualStyleBackColor = false;
            this.btnUsePs.Click += new System.EventHandler(this.btnUsePs_Click);
            // 
            // cboSource
            // 
            this.cboSource.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboSource.DM_UseSelectable = true;
            this.cboSource.FormattingEnabled = true;
            this.cboSource.ItemHeight = 24;
            this.cboSource.Location = new System.Drawing.Point(120, 329);
            this.cboSource.Name = "cboSource";
            this.cboSource.Size = new System.Drawing.Size(210, 30);
            this.cboSource.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(10, 329);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 30);
            this.label9.TabIndex = 6;
            this.label9.Text = "源存储类型：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(10, 293);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 32);
            this.label7.TabIndex = 6;
            this.label7.Text = "当前箱号：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(10, 220);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 32);
            this.label6.TabIndex = 6;
            this.label6.Text = "工作状态：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(10, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 32);
            this.label3.TabIndex = 6;
            this.label3.Text = "作业楼层：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(10, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 32);
            this.label2.TabIndex = 6;
            this.label2.Text = "设备号：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHu
            // 
            this.lblHu.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblHu.BackColor = System.Drawing.Color.White;
            this.lblHu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHu.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblHu.Location = new System.Drawing.Point(120, 293);
            this.lblHu.Name = "lblHu";
            this.lblHu.Size = new System.Drawing.Size(210, 32);
            this.lblHu.TabIndex = 6;
            this.lblHu.Text = "40008888";
            this.lblHu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWorkStatus
            // 
            this.lblWorkStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblWorkStatus.BackColor = System.Drawing.Color.White;
            this.lblWorkStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWorkStatus.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblWorkStatus.Location = new System.Drawing.Point(120, 220);
            this.lblWorkStatus.Name = "lblWorkStatus";
            this.lblWorkStatus.Size = new System.Drawing.Size(210, 32);
            this.lblWorkStatus.TabIndex = 6;
            this.lblWorkStatus.Text = "未开始工作";
            this.lblWorkStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLouceng
            // 
            this.lblLouceng.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLouceng.BackColor = System.Drawing.Color.White;
            this.lblLouceng.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLouceng.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblLouceng.Location = new System.Drawing.Point(120, 185);
            this.lblLouceng.Name = "lblLouceng";
            this.lblLouceng.Size = new System.Drawing.Size(210, 32);
            this.lblLouceng.TabIndex = 6;
            this.lblLouceng.Text = "141";
            this.lblLouceng.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDeviceNo
            // 
            this.lblDeviceNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblDeviceNo.BackColor = System.Drawing.Color.White;
            this.lblDeviceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDeviceNo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblDeviceNo.Location = new System.Drawing.Point(120, 149);
            this.lblDeviceNo.Name = "lblDeviceNo";
            this.lblDeviceNo.Size = new System.Drawing.Size(210, 32);
            this.lblDeviceNo.TabIndex = 6;
            this.lblDeviceNo.Text = "89757";
            this.lblDeviceNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCurrentUser.BackColor = System.Drawing.Color.White;
            this.lblCurrentUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentUser.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblCurrentUser.Location = new System.Drawing.Point(120, 114);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(210, 32);
            this.lblCurrentUser.TabIndex = 6;
            this.lblCurrentUser.Text = "JRZHUANG";
            this.lblCurrentUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(10, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 32);
            this.label1.TabIndex = 6;
            this.label1.Text = "登录工号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.AllowDrop = true;
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnClose.AutoEllipsis = true;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnClose.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(0)))));
            this.btnClose.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(60)))), ((int)(((byte)(0)))));
            this.btnClose.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnClose.DM_Radius = 1;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Teal;
            this.btnClose.Image = null;
            this.btnClose.Location = new System.Drawing.Point(175, 667);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(155, 40);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnErrorBox
            // 
            this.btnErrorBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnErrorBox.AutoEllipsis = true;
            this.btnErrorBox.BackColor = System.Drawing.Color.Transparent;
            this.btnErrorBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnErrorBox.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnErrorBox.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.btnErrorBox.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.btnErrorBox.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnErrorBox.DM_Radius = 1;
            this.btnErrorBox.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnErrorBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnErrorBox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnErrorBox.ForeColor = System.Drawing.Color.Teal;
            this.btnErrorBox.Image = null;
            this.btnErrorBox.Location = new System.Drawing.Point(10, 619);
            this.btnErrorBox.Margin = new System.Windows.Forms.Padding(0);
            this.btnErrorBox.Name = "btnErrorBox";
            this.btnErrorBox.Size = new System.Drawing.Size(155, 40);
            this.btnErrorBox.TabIndex = 14;
            this.btnErrorBox.Text = "异常箱明细";
            this.btnErrorBox.UseVisualStyleBackColor = true;
            this.btnErrorBox.Click += new System.EventHandler(this.btnErrorBox_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(10, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 32);
            this.label4.TabIndex = 6;
            this.label4.Text = "PLC状态：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // lblPlc
            // 
            this.lblPlc.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblPlc.BackColor = System.Drawing.Color.White;
            this.lblPlc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPlc.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblPlc.Location = new System.Drawing.Point(120, 79);
            this.lblPlc.Name = "lblPlc";
            this.lblPlc.Size = new System.Drawing.Size(210, 32);
            this.lblPlc.TabIndex = 6;
            this.lblPlc.Text = "连接中...";
            this.lblPlc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPlc.Click += new System.EventHandler(this.lblPlc_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(10, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 32);
            this.label5.TabIndex = 6;
            this.label5.Text = "读写器：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReader
            // 
            this.lblReader.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblReader.BackColor = System.Drawing.Color.White;
            this.lblReader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblReader.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblReader.Location = new System.Drawing.Point(120, 44);
            this.lblReader.Name = "lblReader";
            this.lblReader.Size = new System.Drawing.Size(210, 32);
            this.lblReader.TabIndex = 6;
            this.lblReader.Text = "连接中...";
            this.lblReader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(10, 256);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 32);
            this.label8.TabIndex = 6;
            this.label8.Text = "扫描结果：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResult
            // 
            this.lblResult.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblResult.BackColor = System.Drawing.Color.White;
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblResult.Location = new System.Drawing.Point(120, 256);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(210, 32);
            this.lblResult.TabIndex = 6;
            this.lblResult.Text = "正常";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label11.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(10, 256);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 32);
            this.label11.TabIndex = 16;
            this.label11.Text = "当前箱数量";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label11.Visible = false;
            // 
            // txtPrinter
            // 
            this.txtPrinter.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Tall;
            this.txtPrinter.DM_UseSelectable = true;
            this.txtPrinter.Lines = new string[0];
            this.txtPrinter.Location = new System.Drawing.Point(120, 396);
            this.txtPrinter.MaxLength = 32767;
            this.txtPrinter.Name = "txtPrinter";
            this.txtPrinter.PasswordChar = '\0';
            this.txtPrinter.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPrinter.SelectedText = "";
            this.txtPrinter.Size = new System.Drawing.Size(210, 30);
            this.txtPrinter.TabIndex = 54;
            this.txtPrinter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPrinter.Visible = false;
            this.txtPrinter.TextChanged += new System.EventHandler(this.txtPrinter_TextChanged);
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(10, 396);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(111, 30);
            this.label12.TabIndex = 53;
            this.label12.Text = "打印机名称";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label12.Visible = false;
            // 
            // lblTotalBoxNum
            // 
            this.lblTotalBoxNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalBoxNum.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblTotalBoxNum.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblTotalBoxNum.ForeColor = System.Drawing.Color.Red;
            this.lblTotalBoxNum.Location = new System.Drawing.Point(650, 5);
            this.lblTotalBoxNum.Name = "lblTotalBoxNum";
            this.lblTotalBoxNum.Size = new System.Drawing.Size(60, 32);
            this.lblTotalBoxNum.TabIndex = 6;
            this.lblTotalBoxNum.Text = "0";
            this.lblTotalBoxNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalNum
            // 
            this.lblTotalNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalNum.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblTotalNum.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblTotalNum.ForeColor = System.Drawing.Color.Red;
            this.lblTotalNum.Location = new System.Drawing.Point(791, 5);
            this.lblTotalNum.Name = "lblTotalNum";
            this.lblTotalNum.Size = new System.Drawing.Size(80, 32);
            this.lblTotalNum.TabIndex = 6;
            this.lblTotalNum.Text = "0";
            this.lblTotalNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.BackColor = System.Drawing.Color.Gainsboro;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(574, 5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 32);
            this.label14.TabIndex = 6;
            this.label14.Text = "总箱数：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelDebug
            // 
            this.panelDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDebug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDebug.Controls.Add(this.metroTextBox2);
            this.panelDebug.Controls.Add(this.metroTextBox1);
            this.panelDebug.Controls.Add(this.dmButton3);
            this.panelDebug.Controls.Add(this.dmButton5);
            this.panelDebug.Controls.Add(this.dmButton2);
            this.panelDebug.Controls.Add(this.dmButton4);
            this.panelDebug.Controls.Add(this.btnDebug);
            this.panelDebug.DM_HorizontalScrollbarBarColor = true;
            this.panelDebug.DM_HorizontalScrollbarDM_HighlightOnWheel = false;
            this.panelDebug.DM_HorizontalScrollbarSize = 10;
            this.panelDebug.DM_ThumbColor = System.Drawing.Color.Gray;
            this.panelDebug.DM_ThumbNormalColor = System.Drawing.Color.Gray;
            this.panelDebug.DM_UseBarColor = true;
            this.panelDebug.DM_VerticalScrollbarBarColor = true;
            this.panelDebug.DM_VerticalScrollbarDM_HighlightOnWheel = false;
            this.panelDebug.DM_VerticalScrollbarSize = 10;
            this.panelDebug.Location = new System.Drawing.Point(545, 532);
            this.panelDebug.Name = "panelDebug";
            this.panelDebug.Size = new System.Drawing.Size(316, 154);
            this.panelDebug.TabIndex = 15;
            this.panelDebug.Visible = false;
            // 
            // metroTextBox2
            // 
            this.metroTextBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroTextBox2.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Medium;
            this.metroTextBox2.DM_UseSelectable = true;
            this.metroTextBox2.Lines = new string[0];
            this.metroTextBox2.Location = new System.Drawing.Point(18, 59);
            this.metroTextBox2.MaxLength = 32767;
            this.metroTextBox2.Name = "metroTextBox2";
            this.metroTextBox2.PasswordChar = '\0';
            this.metroTextBox2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox2.SelectedText = "";
            this.metroTextBox2.Size = new System.Drawing.Size(169, 35);
            this.metroTextBox2.TabIndex = 15;
            // 
            // metroTextBox1
            // 
            this.metroTextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroTextBox1.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Medium;
            this.metroTextBox1.DM_UseSelectable = true;
            this.metroTextBox1.Lines = new string[0];
            this.metroTextBox1.Location = new System.Drawing.Point(18, 13);
            this.metroTextBox1.MaxLength = 32767;
            this.metroTextBox1.Name = "metroTextBox1";
            this.metroTextBox1.PasswordChar = '\0';
            this.metroTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox1.SelectedText = "";
            this.metroTextBox1.Size = new System.Drawing.Size(169, 35);
            this.metroTextBox1.TabIndex = 15;
            // 
            // dmButton3
            // 
            this.dmButton3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dmButton3.AutoEllipsis = true;
            this.dmButton3.BackColor = System.Drawing.Color.Transparent;
            this.dmButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dmButton3.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.dmButton3.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.dmButton3.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.dmButton3.DM_NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(163)))), ((int)(((byte)(203)))));
            this.dmButton3.DM_Radius = 1;
            this.dmButton3.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.dmButton3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dmButton3.Font = new System.Drawing.Font("微软雅黑 Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dmButton3.ForeColor = System.Drawing.Color.White;
            this.dmButton3.Image = null;
            this.dmButton3.Location = new System.Drawing.Point(16, 105);
            this.dmButton3.Margin = new System.Windows.Forms.Padding(0);
            this.dmButton3.Name = "dmButton3";
            this.dmButton3.Size = new System.Drawing.Size(92, 39);
            this.dmButton3.TabIndex = 14;
            this.dmButton3.Text = "START";
            this.dmButton3.UseVisualStyleBackColor = true;
            this.dmButton3.Click += new System.EventHandler(this.dmButton3_Click);
            // 
            // dmButton5
            // 
            this.dmButton5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dmButton5.AutoEllipsis = true;
            this.dmButton5.BackColor = System.Drawing.Color.Transparent;
            this.dmButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dmButton5.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.dmButton5.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.dmButton5.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.dmButton5.DM_NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(163)))), ((int)(((byte)(203)))));
            this.dmButton5.DM_Radius = 1;
            this.dmButton5.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.dmButton5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dmButton5.Font = new System.Drawing.Font("微软雅黑 Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dmButton5.ForeColor = System.Drawing.Color.White;
            this.dmButton5.Image = null;
            this.dmButton5.Location = new System.Drawing.Point(202, 57);
            this.dmButton5.Margin = new System.Windows.Forms.Padding(0);
            this.dmButton5.Name = "dmButton5";
            this.dmButton5.Size = new System.Drawing.Size(92, 39);
            this.dmButton5.TabIndex = 14;
            this.dmButton5.Text = "插入EPC";
            this.dmButton5.UseVisualStyleBackColor = true;
            this.dmButton5.Click += new System.EventHandler(this.dmButton5_Click);
            // 
            // dmButton2
            // 
            this.dmButton2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dmButton2.AutoEllipsis = true;
            this.dmButton2.BackColor = System.Drawing.Color.Transparent;
            this.dmButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dmButton2.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.dmButton2.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.dmButton2.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.dmButton2.DM_NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(163)))), ((int)(((byte)(203)))));
            this.dmButton2.DM_Radius = 1;
            this.dmButton2.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.dmButton2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dmButton2.Font = new System.Drawing.Font("微软雅黑 Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dmButton2.ForeColor = System.Drawing.Color.White;
            this.dmButton2.Image = null;
            this.dmButton2.Location = new System.Drawing.Point(108, 105);
            this.dmButton2.Margin = new System.Windows.Forms.Padding(0);
            this.dmButton2.Name = "dmButton2";
            this.dmButton2.Size = new System.Drawing.Size(92, 39);
            this.dmButton2.TabIndex = 14;
            this.dmButton2.Text = "STOP";
            this.dmButton2.UseVisualStyleBackColor = true;
            this.dmButton2.Click += new System.EventHandler(this.dmButton2_Click);
            // 
            // dmButton4
            // 
            this.dmButton4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dmButton4.AutoEllipsis = true;
            this.dmButton4.BackColor = System.Drawing.Color.Transparent;
            this.dmButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dmButton4.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.dmButton4.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.dmButton4.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.dmButton4.DM_NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(163)))), ((int)(((byte)(203)))));
            this.dmButton4.DM_Radius = 1;
            this.dmButton4.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.dmButton4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dmButton4.Font = new System.Drawing.Font("微软雅黑 Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dmButton4.ForeColor = System.Drawing.Color.White;
            this.dmButton4.Image = null;
            this.dmButton4.Location = new System.Drawing.Point(202, 11);
            this.dmButton4.Margin = new System.Windows.Forms.Padding(0);
            this.dmButton4.Name = "dmButton4";
            this.dmButton4.Size = new System.Drawing.Size(92, 39);
            this.dmButton4.TabIndex = 14;
            this.dmButton4.Text = "插入箱码";
            this.dmButton4.UseVisualStyleBackColor = true;
            this.dmButton4.Click += new System.EventHandler(this.dmButton4_Click);
            // 
            // btnDebug
            // 
            this.btnDebug.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDebug.AutoEllipsis = true;
            this.btnDebug.BackColor = System.Drawing.Color.Transparent;
            this.btnDebug.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDebug.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnDebug.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.btnDebug.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.btnDebug.DM_NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(163)))), ((int)(((byte)(203)))));
            this.btnDebug.DM_Radius = 1;
            this.btnDebug.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnDebug.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDebug.Font = new System.Drawing.Font("微软雅黑 Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDebug.ForeColor = System.Drawing.Color.White;
            this.btnDebug.Image = null;
            this.btnDebug.Location = new System.Drawing.Point(200, 105);
            this.btnDebug.Margin = new System.Windows.Forms.Padding(0);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(92, 39);
            this.btnDebug.TabIndex = 14;
            this.btnDebug.Text = "DEBUG";
            this.btnDebug.UseVisualStyleBackColor = false;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.BackColor = System.Drawing.Color.Gainsboro;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(715, 5);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(75, 32);
            this.label16.TabIndex = 6;
            this.label16.Text = "总件数：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblQty
            // 
            this.lblQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblQty.AutoSize = true;
            this.lblQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblQty.Font = new System.Drawing.Font("微软雅黑", 100F);
            this.lblQty.ForeColor = System.Drawing.Color.Teal;
            this.lblQty.Location = new System.Drawing.Point(-1, 536);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(154, 175);
            this.lblQty.TabIndex = 17;
            this.lblQty.Text = "0";
            this.lblQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            this.grid.Location = new System.Drawing.Point(0, 40);
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
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.grid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.RowTemplate.Height = 43;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(875, 670);
            this.grid.TabIndex = 18;
            // 
            // SOURCE
            // 
            this.SOURCE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.SOURCE.FillWeight = 15F;
            this.SOURCE.HeaderText = "源存储类型";
            this.SOURCE.Name = "SOURCE";
            this.SOURCE.ReadOnly = true;
            this.SOURCE.Width = 106;
            // 
            // TARGET
            // 
            this.TARGET.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.TARGET.FillWeight = 15F;
            this.TARGET.HeaderText = "目标存储类型";
            this.TARGET.Name = "TARGET";
            this.TARGET.ReadOnly = true;
            this.TARGET.Width = 106;
            // 
            // HU
            // 
            this.HU.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.HU.FillWeight = 13F;
            this.HU.HeaderText = "箱号";
            this.HU.Name = "HU";
            this.HU.ReadOnly = true;
            this.HU.Width = 70;
            // 
            // ZSATNR
            // 
            this.ZSATNR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ZSATNR.FillWeight = 15F;
            this.ZSATNR.HeaderText = "品号";
            this.ZSATNR.Name = "ZSATNR";
            this.ZSATNR.ReadOnly = true;
            this.ZSATNR.Width = 70;
            // 
            // ZCOLSN
            // 
            this.ZCOLSN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ZCOLSN.FillWeight = 7F;
            this.ZCOLSN.HeaderText = "色号";
            this.ZCOLSN.Name = "ZCOLSN";
            this.ZCOLSN.ReadOnly = true;
            this.ZCOLSN.Width = 70;
            // 
            // ZSIZTX
            // 
            this.ZSIZTX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ZSIZTX.FillWeight = 13F;
            this.ZSIZTX.HeaderText = "规格";
            this.ZSIZTX.Name = "ZSIZTX";
            this.ZSIZTX.ReadOnly = true;
            this.ZSIZTX.Width = 70;
            // 
            // QTY
            // 
            this.QTY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.QTY.FillWeight = 7F;
            this.QTY.HeaderText = "数量";
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            this.QTY.Width = 70;
            // 
            // MSG
            // 
            this.MSG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.MSG.DefaultCellStyle = dataGridViewCellStyle2;
            this.MSG.FillWeight = 38F;
            this.MSG.HeaderText = "状态";
            this.MSG.Name = "MSG";
            this.MSG.ReadOnly = true;
            this.MSG.Width = 70;
            // 
            // label13
            // 
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Teal;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(875, 40);
            this.label13.TabIndex = 19;
            this.label13.Text = "复核明细";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 772);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "InventoryForm";
            this.Text = "RFID移库装箱系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InventoryForm_FormClosing);
            this.Load += new System.EventHandler(this.InventoryForm_Load);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelDebug.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DMSkin.Controls.DMLabel lblUsePs;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblQty;
        private DMSkin.Controls.DMButton btnGenerateDoc;
        private DMSkin.Controls.DMButton btnGx;
        private DMSkin.Controls.DMButton btnUsePs;
        private DMSkin.Metro.Controls.MetroComboBox cboSource;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblHu;
        private System.Windows.Forms.Label lblWorkStatus;
        private System.Windows.Forms.Label lblReader;
        private System.Windows.Forms.Label lblPlc;
        private System.Windows.Forms.Label lblLouceng;
        private System.Windows.Forms.Label lblDeviceNo;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.Label label1;
        private DMSkin.Controls.DMButton btnClose;
        private DMSkin.Controls.DMButton btnErrorBox;
        private DMSkin.Metro.Controls.MetroPanel panelDebug;
        private DMSkin.Metro.Controls.MetroTextBox metroTextBox2;
        private DMSkin.Metro.Controls.MetroTextBox metroTextBox1;
        private DMSkin.Controls.DMButton dmButton3;
        private DMSkin.Controls.DMButton dmButton5;
        private DMSkin.Controls.DMButton dmButton2;
        private DMSkin.Controls.DMButton dmButton4;
        private DMSkin.Controls.DMButton btnDebug;
        private DMSkin.Metro.Controls.MetroComboBox cboTarget;
        private System.Windows.Forms.Label label10;
        private DMSkin.Controls.DMLabel lblUseBoxStandard;
        private DMSkin.Controls.DMLabel lblUseSize;
        private DMSkin.Controls.DMButton btnUseBoxStandard;
        private DMSkin.Controls.DMButton btnUseSize;
        private DMSkin.Controls.DMLabel lblCheckSku;
        private DMSkin.Controls.DMButton btnCheckSku;
        private DMSkin.Controls.DMLabel lblUsePrint;
        private DMSkin.Controls.DMButton btnUsePrint;
        private DMSkin.Metro.Controls.MetroTextBox txtPrinter;
        private System.Windows.Forms.Label label12;
        private DMSkin.Controls.DMLabel lblCheckPinSe;
        private DMSkin.Controls.DMButton btnCheckPinSe;
        private DMSkin.Metro.Controls.MetroGrid grid;
        private DMSkin.Metro.Controls.MetroComboBox cboPxmat;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblTotalBoxNum;
        private System.Windows.Forms.Label lblTotalNum;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private DMSkin.Controls.DMButton dmButtonStart;
        private DMSkin.Controls.DMButton dmButtonStop;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOURCE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TARGET;
        private System.Windows.Forms.DataGridViewTextBoxColumn HU;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZSATNR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZCOLSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZSIZTX;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn MSG;
    }
}

