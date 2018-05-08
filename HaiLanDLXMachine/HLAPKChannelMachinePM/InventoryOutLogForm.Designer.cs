using System.Drawing;
namespace HLADeliverChannelMachine
{
    partial class InventoryOutLogForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.metroLabel1 = new DMSkin.Metro.Controls.MetroLabel();
            this.togHistory = new DMSkin.Metro.Controls.MetroToggle();
            this.txtOutlog = new DMSkin.Metro.Controls.MetroTextBox();
            this.metroLabel2 = new DMSkin.Metro.Controls.MetroLabel();
            this.togAutoprint = new DMSkin.Metro.Controls.MetroToggle();
            this.btnQuery = new DMSkin.Metro.Controls.MetroButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.LGTYPCheckedComboBox = new HLACommonView.Views.CheckedComboBox();
            this.metroLabel6 = new DMSkin.Metro.Controls.MetroLabel();
            this.togLocalprint = new DMSkin.Metro.Controls.MetroToggle();
            this.lbcurrentuser = new DMSkin.Metro.Controls.MetroLabel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnSetDefault = new DMSkin.Metro.Controls.MetroButton();
            this.lblPinXiang = new DMSkin.Metro.Controls.MetroLabel();
            this.metroPanel1 = new DMSkin.Metro.Controls.MetroPanel();
            this.lblAuRealcount = new DMSkin.Metro.Controls.MetroLabel();
            this.lblAuPlancount = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel10 = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel17 = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel13 = new DMSkin.Metro.Controls.MetroLabel();
            this.lblAuDifferentcount = new DMSkin.Metro.Controls.MetroLabel();
            this.btnStoreOutLogDetail = new DMSkin.Metro.Controls.MetroButton();
            this.btnImportBox = new DMSkin.Metro.Controls.MetroButton();
            this.cboBoxstyle = new DMSkin.Metro.Controls.MetroComboBox();
            this.txtPrinter = new DMSkin.Metro.Controls.MetroTextBox();
            this.txtImportBoxNo = new DMSkin.Metro.Controls.MetroTextBox();
            this.txtFloor = new DMSkin.Metro.Controls.MetroTextBox();
            this.metroTile3 = new DMSkin.Metro.Controls.MetroTile();
            this.metroTile2 = new DMSkin.Metro.Controls.MetroTile();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.metroTile1 = new DMSkin.Metro.Controls.MetroTile();
            this.metroLabel3 = new DMSkin.Metro.Controls.MetroLabel();
            this.lblPlancount = new DMSkin.Metro.Controls.MetroLabel();
            this.lblStore = new DMSkin.Metro.Controls.MetroLabel();
            this.lblCurrentcount = new DMSkin.Metro.Controls.MetroLabel();
            this.lblRealcount = new DMSkin.Metro.Controls.MetroLabel();
            this.lblNooutcount = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel22 = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel15 = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel14 = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel5 = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel9 = new DMSkin.Metro.Controls.MetroLabel();
            this.lblDifferentcount = new DMSkin.Metro.Controls.MetroLabel();
            this.lbIsRFID = new DMSkin.Metro.Controls.MetroLabel();
            this.lblOutlog = new DMSkin.Metro.Controls.MetroLabel();
            this.lblTotalcount = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel23 = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel21 = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel19 = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel12 = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel18 = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel11 = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel7 = new DMSkin.Metro.Controls.MetroLabel();
            this.metroPanel2 = new DMSkin.Metro.Controls.MetroPanel();
            this.txtTestEpc = new DMSkin.Metro.Controls.MetroTextBox();
            this.btnTest = new DMSkin.Metro.Controls.MetroButton();
            this.btnSetnoscanbox = new DMSkin.Metro.Controls.MetroButton();
            this.lblIsScan = new DMSkin.Metro.Controls.MetroLabel();
            this.lblBoxvalue = new DMSkin.Metro.Controls.MetroLabel();
            this.lblBoxstyle = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel25 = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel4 = new DMSkin.Metro.Controls.MetroLabel();
            this.btnBarcode = new DMSkin.Metro.Controls.MetroButton();
            this.txtBarcode = new DMSkin.Metro.Controls.MetroTextBox();
            this.lblInfo = new DMSkin.Metro.Controls.MetroLabel();
            this.metroLabel24 = new DMSkin.Metro.Controls.MetroLabel();
            this.lblCurrentcountBig = new System.Windows.Forms.Label();
            this.grid = new DMSkin.Metro.Controls.MetroGrid();
            this.NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoxNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boxstyle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Del = new System.Windows.Forms.DataGridViewImageColumn();
            this.Detail = new System.Windows.Forms.DataGridViewImageColumn();
            this.Print = new System.Windows.Forms.DataGridViewImageColumn();
            this.FullBox = new System.Windows.Forms.DataGridViewImageColumn();
            this.BtnSetFullBox = new DMSkin.Metro.Controls.MetroButton();
            this.btnInventory = new DMSkin.Metro.Controls.MetroButton();
            this.btnGetBoxNo = new DMSkin.Metro.Controls.MetroButton();
            this.btnOutConfirm = new DMSkin.Metro.Controls.MetroButton();
            this.btnTempSave = new DMSkin.Metro.Controls.MetroButton();
            this.btnCancel = new DMSkin.Metro.Controls.MetroButton();
            this.btnHide = new DMSkin.Controls.DMLabel();
            this.btnOutLogDetail = new DMSkin.Controls.DMLabel();
            this.btnCheckBox = new DMSkin.Controls.DMLabel();
            this.btnUnUpload = new DMSkin.Controls.DMLabel();
            this.btnKeyboard = new DMSkin.Controls.DMLabel();
            this.tmeStop = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel1.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel1.Location = new System.Drawing.Point(8, 4);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(86, 24);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "历史单号:";
            // 
            // togHistory
            // 
            this.togHistory.DisplayStatus = false;
            this.togHistory.DM_UseSelectable = true;
            this.togHistory.Location = new System.Drawing.Point(97, 1);
            this.togHistory.Name = "togHistory";
            this.togHistory.Size = new System.Drawing.Size(45, 30);
            this.togHistory.TabIndex = 1;
            this.togHistory.Text = "Off";
            this.togHistory.CheckedChanged += new System.EventHandler(this.togHistory_CheckedChanged);
            // 
            // txtOutlog
            // 
            this.txtOutlog.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Tall;
            this.txtOutlog.DM_UseSelectable = true;
            this.txtOutlog.Lines = new string[0];
            this.txtOutlog.Location = new System.Drawing.Point(151, 0);
            this.txtOutlog.MaxLength = 32767;
            this.txtOutlog.Name = "txtOutlog";
            this.txtOutlog.PasswordChar = '\0';
            this.txtOutlog.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtOutlog.SelectedText = "";
            this.txtOutlog.Size = new System.Drawing.Size(197, 30);
            this.txtOutlog.TabIndex = 2;
            this.txtOutlog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtOutlog.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOutlog_KeyPress);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel2.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel2.Location = new System.Drawing.Point(444, 3);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(122, 24);
            this.metroLabel2.TabIndex = 0;
            this.metroLabel2.Text = "满箱自动打印:";
            // 
            // togAutoprint
            // 
            this.togAutoprint.Checked = true;
            this.togAutoprint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.togAutoprint.DisplayStatus = false;
            this.togAutoprint.DM_UseSelectable = true;
            this.togAutoprint.Location = new System.Drawing.Point(574, 0);
            this.togAutoprint.Name = "togAutoprint";
            this.togAutoprint.Size = new System.Drawing.Size(45, 30);
            this.togAutoprint.TabIndex = 1;
            this.togAutoprint.Text = "On";
            this.togAutoprint.CheckedChanged += new System.EventHandler(this.togAutoprint_CheckedChanged);
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnQuery.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnQuery.DM_UseCustomBackColor = true;
            this.btnQuery.DM_UseCustomForeColor = true;
            this.btnQuery.DM_UseSelectable = true;
            this.btnQuery.ForeColor = System.Drawing.Color.White;
            this.btnQuery.Location = new System.Drawing.Point(359, 0);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(0);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 30);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "查询";
            this.btnQuery.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(4, 28);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.LGTYPCheckedComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.btnQuery);
            this.splitContainer1.Panel1.Controls.Add(this.txtOutlog);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel1);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel6);
            this.splitContainer1.Panel1.Controls.Add(this.togLocalprint);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel2);
            this.splitContainer1.Panel1.Controls.Add(this.togAutoprint);
            this.splitContainer1.Panel1.Controls.Add(this.togHistory);
            this.splitContainer1.Panel1.Controls.Add(this.lbcurrentuser);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1358, 695);
            this.splitContainer1.SplitterDistance = 35;
            this.splitContainer1.TabIndex = 5;
            // 
            // LGTYPCheckedComboBox
            // 
            this.LGTYPCheckedComboBox.CheckOnClick = true;
            this.LGTYPCheckedComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.LGTYPCheckedComboBox.DropDownHeight = 1;
            this.LGTYPCheckedComboBox.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold);
            this.LGTYPCheckedComboBox.FormattingEnabled = true;
            this.LGTYPCheckedComboBox.IntegralHeight = false;
            this.LGTYPCheckedComboBox.Location = new System.Drawing.Point(832, 1);
            this.LGTYPCheckedComboBox.Name = "LGTYPCheckedComboBox";
            this.LGTYPCheckedComboBox.Size = new System.Drawing.Size(270, 28);
            this.LGTYPCheckedComboBox.TabIndex = 4;
            this.LGTYPCheckedComboBox.ValueSeparator = ", ";
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel6.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel6.Location = new System.Drawing.Point(630, 3);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(122, 24);
            this.metroLabel6.TabIndex = 0;
            this.metroLabel6.Text = "启用本地打印:";
            // 
            // togLocalprint
            // 
            this.togLocalprint.DisplayStatus = false;
            this.togLocalprint.DM_UseSelectable = true;
            this.togLocalprint.Location = new System.Drawing.Point(760, 0);
            this.togLocalprint.Name = "togLocalprint";
            this.togLocalprint.Size = new System.Drawing.Size(45, 30);
            this.togLocalprint.TabIndex = 1;
            this.togLocalprint.Text = "Off";
            this.togLocalprint.CheckedChanged += new System.EventHandler(this.togLocalprint_CheckedChanged);
            // 
            // lbcurrentuser
            // 
            this.lbcurrentuser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbcurrentuser.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lbcurrentuser.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lbcurrentuser.DM_UseCustomForeColor = true;
            this.lbcurrentuser.DM_WrapToLine = true;
            this.lbcurrentuser.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbcurrentuser.Location = new System.Drawing.Point(1122, 3);
            this.lbcurrentuser.Name = "lbcurrentuser";
            this.lbcurrentuser.Size = new System.Drawing.Size(228, 24);
            this.lbcurrentuser.TabIndex = 0;
            this.lbcurrentuser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbcurrentuser.Click += new System.EventHandler(this.lbcurrentuser_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.BtnSetFullBox);
            this.splitContainer3.Panel2.Controls.Add(this.btnInventory);
            this.splitContainer3.Panel2.Controls.Add(this.btnGetBoxNo);
            this.splitContainer3.Panel2.Controls.Add(this.btnOutConfirm);
            this.splitContainer3.Panel2.Controls.Add(this.btnTempSave);
            this.splitContainer3.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer3.Panel2.Controls.Add(this.btnHide);
            this.splitContainer3.Panel2.Controls.Add(this.btnOutLogDetail);
            this.splitContainer3.Panel2.Controls.Add(this.btnCheckBox);
            this.splitContainer3.Panel2.Controls.Add(this.btnUnUpload);
            this.splitContainer3.Panel2.Controls.Add(this.btnKeyboard);
            this.splitContainer3.Size = new System.Drawing.Size(1358, 656);
            this.splitContainer3.SplitterDistance = 566;
            this.splitContainer3.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnSetDefault);
            this.splitContainer2.Panel1.Controls.Add(this.lblPinXiang);
            this.splitContainer2.Panel1.Controls.Add(this.metroPanel1);
            this.splitContainer2.Panel1.Controls.Add(this.btnStoreOutLogDetail);
            this.splitContainer2.Panel1.Controls.Add(this.btnImportBox);
            this.splitContainer2.Panel1.Controls.Add(this.cboBoxstyle);
            this.splitContainer2.Panel1.Controls.Add(this.txtPrinter);
            this.splitContainer2.Panel1.Controls.Add(this.txtImportBoxNo);
            this.splitContainer2.Panel1.Controls.Add(this.txtFloor);
            this.splitContainer2.Panel1.Controls.Add(this.metroTile3);
            this.splitContainer2.Panel1.Controls.Add(this.metroTile2);
            this.splitContainer2.Panel1.Controls.Add(this.splitter1);
            this.splitContainer2.Panel1.Controls.Add(this.metroTile1);
            this.splitContainer2.Panel1.Controls.Add(this.metroLabel3);
            this.splitContainer2.Panel1.Controls.Add(this.lblPlancount);
            this.splitContainer2.Panel1.Controls.Add(this.lblStore);
            this.splitContainer2.Panel1.Controls.Add(this.lblCurrentcount);
            this.splitContainer2.Panel1.Controls.Add(this.lblRealcount);
            this.splitContainer2.Panel1.Controls.Add(this.lblNooutcount);
            this.splitContainer2.Panel1.Controls.Add(this.metroLabel22);
            this.splitContainer2.Panel1.Controls.Add(this.metroLabel15);
            this.splitContainer2.Panel1.Controls.Add(this.metroLabel14);
            this.splitContainer2.Panel1.Controls.Add(this.metroLabel5);
            this.splitContainer2.Panel1.Controls.Add(this.metroLabel9);
            this.splitContainer2.Panel1.Controls.Add(this.lblDifferentcount);
            this.splitContainer2.Panel1.Controls.Add(this.lbIsRFID);
            this.splitContainer2.Panel1.Controls.Add(this.lblOutlog);
            this.splitContainer2.Panel1.Controls.Add(this.lblTotalcount);
            this.splitContainer2.Panel1.Controls.Add(this.metroLabel23);
            this.splitContainer2.Panel1.Controls.Add(this.metroLabel21);
            this.splitContainer2.Panel1.Controls.Add(this.metroLabel19);
            this.splitContainer2.Panel1.Controls.Add(this.metroLabel12);
            this.splitContainer2.Panel1.Controls.Add(this.metroLabel18);
            this.splitContainer2.Panel1.Controls.Add(this.metroLabel11);
            this.splitContainer2.Panel1.Controls.Add(this.metroLabel7);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer2.Panel2.Controls.Add(this.metroPanel2);
            this.splitContainer2.Panel2.Controls.Add(this.btnSetnoscanbox);
            this.splitContainer2.Panel2.Controls.Add(this.lblIsScan);
            this.splitContainer2.Panel2.Controls.Add(this.lblBoxvalue);
            this.splitContainer2.Panel2.Controls.Add(this.lblBoxstyle);
            this.splitContainer2.Panel2.Controls.Add(this.metroLabel25);
            this.splitContainer2.Panel2.Controls.Add(this.metroLabel4);
            this.splitContainer2.Panel2.Controls.Add(this.btnBarcode);
            this.splitContainer2.Panel2.Controls.Add(this.txtBarcode);
            this.splitContainer2.Panel2.Controls.Add(this.lblInfo);
            this.splitContainer2.Panel2.Controls.Add(this.metroLabel24);
            this.splitContainer2.Panel2.Controls.Add(this.lblCurrentcountBig);
            this.splitContainer2.Panel2.Controls.Add(this.grid);
            this.splitContainer2.Size = new System.Drawing.Size(1358, 566);
            this.splitContainer2.SplitterDistance = 404;
            this.splitContainer2.TabIndex = 0;
            // 
            // btnSetDefault
            // 
            this.btnSetDefault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnSetDefault.DM_FontSize = DMSkin.Metro.MetroButtonSize.Medium;
            this.btnSetDefault.DM_UseCustomBackColor = true;
            this.btnSetDefault.DM_UseCustomForeColor = true;
            this.btnSetDefault.DM_UseSelectable = true;
            this.btnSetDefault.ForeColor = System.Drawing.Color.White;
            this.btnSetDefault.Location = new System.Drawing.Point(324, 418);
            this.btnSetDefault.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetDefault.Name = "btnSetDefault";
            this.btnSetDefault.Size = new System.Drawing.Size(75, 34);
            this.btnSetDefault.TabIndex = 15;
            this.btnSetDefault.Text = "设为默认";
            this.btnSetDefault.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnSetDefault.Click += new System.EventHandler(this.btnSetDefault_Click);
            // 
            // lblPinXiang
            // 
            this.lblPinXiang.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblPinXiang.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblPinXiang.DM_UseCustomForeColor = true;
            this.lblPinXiang.ForeColor = System.Drawing.Color.Red;
            this.lblPinXiang.Location = new System.Drawing.Point(172, 81);
            this.lblPinXiang.Name = "lblPinXiang";
            this.lblPinXiang.Size = new System.Drawing.Size(57, 24);
            this.lblPinXiang.TabIndex = 14;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.lblAuRealcount);
            this.metroPanel1.Controls.Add(this.lblAuPlancount);
            this.metroPanel1.Controls.Add(this.metroLabel10);
            this.metroPanel1.Controls.Add(this.metroLabel17);
            this.metroPanel1.Controls.Add(this.metroLabel13);
            this.metroPanel1.Controls.Add(this.lblAuDifferentcount);
            this.metroPanel1.DM_HorizontalScrollbarBarColor = true;
            this.metroPanel1.DM_HorizontalScrollbarDM_HighlightOnWheel = false;
            this.metroPanel1.DM_HorizontalScrollbarSize = 10;
            this.metroPanel1.DM_ThumbColor = System.Drawing.Color.Gray;
            this.metroPanel1.DM_ThumbNormalColor = System.Drawing.Color.Gray;
            this.metroPanel1.DM_UseBarColor = true;
            this.metroPanel1.DM_VerticalScrollbarBarColor = true;
            this.metroPanel1.DM_VerticalScrollbarDM_HighlightOnWheel = false;
            this.metroPanel1.DM_VerticalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(0, 201);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(402, 36);
            this.metroPanel1.TabIndex = 13;
            // 
            // lblAuRealcount
            // 
            this.lblAuRealcount.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblAuRealcount.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblAuRealcount.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblAuRealcount.Location = new System.Drawing.Point(259, 3);
            this.lblAuRealcount.Name = "lblAuRealcount";
            this.lblAuRealcount.Size = new System.Drawing.Size(50, 24);
            this.lblAuRealcount.TabIndex = 18;
            this.lblAuRealcount.Text = "13";
            // 
            // lblAuPlancount
            // 
            this.lblAuPlancount.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblAuPlancount.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblAuPlancount.Location = new System.Drawing.Point(109, 3);
            this.lblAuPlancount.Name = "lblAuPlancount";
            this.lblAuPlancount.Size = new System.Drawing.Size(54, 24);
            this.lblAuPlancount.TabIndex = 17;
            this.lblAuPlancount.Text = "13";
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel10.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel10.Location = new System.Drawing.Point(8, 3);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(104, 24);
            this.metroLabel10.TabIndex = 19;
            this.metroLabel10.Text = "辅预计数量:";
            // 
            // metroLabel17
            // 
            this.metroLabel17.AutoSize = true;
            this.metroLabel17.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel17.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel17.Location = new System.Drawing.Point(158, 3);
            this.metroLabel17.Name = "metroLabel17";
            this.metroLabel17.Size = new System.Drawing.Size(104, 24);
            this.metroLabel17.TabIndex = 16;
            this.metroLabel17.Text = "辅实际数量:";
            // 
            // metroLabel13
            // 
            this.metroLabel13.AutoSize = true;
            this.metroLabel13.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel13.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel13.Location = new System.Drawing.Point(307, 3);
            this.metroLabel13.Name = "metroLabel13";
            this.metroLabel13.Size = new System.Drawing.Size(50, 24);
            this.metroLabel13.TabIndex = 14;
            this.metroLabel13.Text = "差异:";
            // 
            // lblAuDifferentcount
            // 
            this.lblAuDifferentcount.AutoSize = true;
            this.lblAuDifferentcount.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblAuDifferentcount.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblAuDifferentcount.Location = new System.Drawing.Point(363, 3);
            this.lblAuDifferentcount.Name = "lblAuDifferentcount";
            this.lblAuDifferentcount.Size = new System.Drawing.Size(21, 24);
            this.lblAuDifferentcount.TabIndex = 15;
            this.lblAuDifferentcount.Text = "0";
            // 
            // btnStoreOutLogDetail
            // 
            this.btnStoreOutLogDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnStoreOutLogDetail.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnStoreOutLogDetail.DM_UseCustomBackColor = true;
            this.btnStoreOutLogDetail.DM_UseCustomForeColor = true;
            this.btnStoreOutLogDetail.DM_UseSelectable = true;
            this.btnStoreOutLogDetail.ForeColor = System.Drawing.Color.White;
            this.btnStoreOutLogDetail.Location = new System.Drawing.Point(276, 81);
            this.btnStoreOutLogDetail.Margin = new System.Windows.Forms.Padding(0);
            this.btnStoreOutLogDetail.Name = "btnStoreOutLogDetail";
            this.btnStoreOutLogDetail.Size = new System.Drawing.Size(123, 54);
            this.btnStoreOutLogDetail.TabIndex = 13;
            this.btnStoreOutLogDetail.Text = "明细";
            this.btnStoreOutLogDetail.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnStoreOutLogDetail.Click += new System.EventHandler(this.btnStoreOutLogDetail_Click);
            // 
            // btnImportBox
            // 
            this.btnImportBox.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnImportBox.DM_UseSelectable = true;
            this.btnImportBox.Location = new System.Drawing.Point(298, 510);
            this.btnImportBox.Margin = new System.Windows.Forms.Padding(0);
            this.btnImportBox.Name = "btnImportBox";
            this.btnImportBox.Size = new System.Drawing.Size(101, 40);
            this.btnImportBox.TabIndex = 8;
            this.btnImportBox.Text = "导入";
            this.btnImportBox.Click += new System.EventHandler(this.btnImportBox_Click);
            // 
            // cboBoxstyle
            // 
            this.cboBoxstyle.DM_FontSize = DMSkin.Metro.MetroComboBoxSize.Tall;
            this.cboBoxstyle.DM_UseSelectable = true;
            this.cboBoxstyle.FormattingEnabled = true;
            this.cboBoxstyle.ItemHeight = 28;
            this.cboBoxstyle.Location = new System.Drawing.Point(100, 418);
            this.cboBoxstyle.Name = "cboBoxstyle";
            this.cboBoxstyle.Size = new System.Drawing.Size(221, 34);
            this.cboBoxstyle.TabIndex = 7;
            this.cboBoxstyle.SelectionChangeCommitted += new System.EventHandler(this.cboBoxstyle_SelectionChangeCommitted);
            // 
            // txtPrinter
            // 
            this.txtPrinter.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Tall;
            this.txtPrinter.DM_UseSelectable = true;
            this.txtPrinter.Lines = new string[0];
            this.txtPrinter.Location = new System.Drawing.Point(100, 382);
            this.txtPrinter.MaxLength = 32767;
            this.txtPrinter.Name = "txtPrinter";
            this.txtPrinter.PasswordChar = '\0';
            this.txtPrinter.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPrinter.SelectedText = "";
            this.txtPrinter.Size = new System.Drawing.Size(299, 30);
            this.txtPrinter.TabIndex = 2;
            this.txtPrinter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPrinter.Visible = false;
            this.txtPrinter.TextChanged += new System.EventHandler(this.txtPrinter_TextChanged);
            // 
            // txtImportBoxNo
            // 
            this.txtImportBoxNo.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Tall;
            this.txtImportBoxNo.DM_UseSelectable = true;
            this.txtImportBoxNo.Lines = new string[0];
            this.txtImportBoxNo.Location = new System.Drawing.Point(8, 510);
            this.txtImportBoxNo.MaxLength = 32767;
            this.txtImportBoxNo.Name = "txtImportBoxNo";
            this.txtImportBoxNo.PasswordChar = '\0';
            this.txtImportBoxNo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtImportBoxNo.SelectedText = "";
            this.txtImportBoxNo.Size = new System.Drawing.Size(280, 40);
            this.txtImportBoxNo.TabIndex = 2;
            this.txtImportBoxNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtFloor
            // 
            this.txtFloor.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Tall;
            this.txtFloor.DM_UseSelectable = true;
            this.txtFloor.Enabled = false;
            this.txtFloor.Lines = new string[0];
            this.txtFloor.Location = new System.Drawing.Point(100, 346);
            this.txtFloor.MaxLength = 32767;
            this.txtFloor.Name = "txtFloor";
            this.txtFloor.PasswordChar = '\0';
            this.txtFloor.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtFloor.SelectedText = "";
            this.txtFloor.Size = new System.Drawing.Size(299, 30);
            this.txtFloor.TabIndex = 2;
            this.txtFloor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // metroTile3
            // 
            this.metroTile3.ActiveControl = null;
            this.metroTile3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroTile3.DM_TileTextDM_FontSize = DMSkin.Metro.MetroTileTextSize.Tall;
            this.metroTile3.DM_UseSelectable = true;
            this.metroTile3.Location = new System.Drawing.Point(3, 459);
            this.metroTile3.Name = "metroTile3";
            this.metroTile3.Size = new System.Drawing.Size(396, 41);
            this.metroTile3.TabIndex = 6;
            this.metroTile3.Text = "箱导入";
            this.metroTile3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // metroTile2
            // 
            this.metroTile2.ActiveControl = null;
            this.metroTile2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroTile2.DM_TileTextDM_FontSize = DMSkin.Metro.MetroTileTextSize.Tall;
            this.metroTile2.DM_UseSelectable = true;
            this.metroTile2.Location = new System.Drawing.Point(3, 298);
            this.metroTile2.Name = "metroTile2";
            this.metroTile2.Size = new System.Drawing.Size(396, 41);
            this.metroTile2.TabIndex = 6;
            this.metroTile2.Text = "配置信息";
            this.metroTile2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitter1
            // 
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 563);
            this.splitter1.MinExtra = 10;
            this.splitter1.MinSize = 10;
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(402, 1);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // metroTile1
            // 
            this.metroTile1.ActiveControl = null;
            this.metroTile1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroTile1.DM_TileTextDM_FontSize = DMSkin.Metro.MetroTileTextSize.Tall;
            this.metroTile1.DM_UseSelectable = true;
            this.metroTile1.Location = new System.Drawing.Point(3, 3);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(396, 41);
            this.metroTile1.TabIndex = 4;
            this.metroTile1.Text = "单据信息";
            this.metroTile1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel3.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel3.Location = new System.Drawing.Point(8, 51);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(86, 24);
            this.metroLabel3.TabIndex = 0;
            this.metroLabel3.Text = "下架单号:";
            // 
            // lblPlancount
            // 
            this.lblPlancount.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblPlancount.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblPlancount.Location = new System.Drawing.Point(100, 165);
            this.lblPlancount.Name = "lblPlancount";
            this.lblPlancount.Size = new System.Drawing.Size(57, 24);
            this.lblPlancount.TabIndex = 0;
            this.lblPlancount.Text = "13";
            // 
            // lblStore
            // 
            this.lblStore.AutoSize = true;
            this.lblStore.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblStore.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblStore.Location = new System.Drawing.Point(100, 81);
            this.lblStore.Name = "lblStore";
            this.lblStore.Size = new System.Drawing.Size(57, 24);
            this.lblStore.TabIndex = 0;
            this.lblStore.Text = "H20B";
            // 
            // lblCurrentcount
            // 
            this.lblCurrentcount.AutoSize = true;
            this.lblCurrentcount.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblCurrentcount.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblCurrentcount.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblCurrentcount.Location = new System.Drawing.Point(100, 264);
            this.lblCurrentcount.Name = "lblCurrentcount";
            this.lblCurrentcount.Size = new System.Drawing.Size(21, 24);
            this.lblCurrentcount.TabIndex = 0;
            this.lblCurrentcount.Text = "0";
            // 
            // lblRealcount
            // 
            this.lblRealcount.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblRealcount.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblRealcount.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblRealcount.Location = new System.Drawing.Point(249, 165);
            this.lblRealcount.Name = "lblRealcount";
            this.lblRealcount.Size = new System.Drawing.Size(57, 24);
            this.lblRealcount.TabIndex = 0;
            this.lblRealcount.Text = "13";
            // 
            // lblNooutcount
            // 
            this.lblNooutcount.AutoSize = true;
            this.lblNooutcount.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblNooutcount.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblNooutcount.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblNooutcount.Location = new System.Drawing.Point(100, 111);
            this.lblNooutcount.Name = "lblNooutcount";
            this.lblNooutcount.Size = new System.Drawing.Size(21, 24);
            this.lblNooutcount.TabIndex = 0;
            this.lblNooutcount.Text = "0";
            // 
            // metroLabel22
            // 
            this.metroLabel22.AutoSize = true;
            this.metroLabel22.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel22.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel22.Location = new System.Drawing.Point(8, 349);
            this.metroLabel22.Name = "metroLabel22";
            this.metroLabel22.Size = new System.Drawing.Size(68, 24);
            this.metroLabel22.TabIndex = 0;
            this.metroLabel22.Text = "楼层号:";
            // 
            // metroLabel15
            // 
            this.metroLabel15.AutoSize = true;
            this.metroLabel15.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel15.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel15.Location = new System.Drawing.Point(8, 165);
            this.metroLabel15.Name = "metroLabel15";
            this.metroLabel15.Size = new System.Drawing.Size(86, 24);
            this.metroLabel15.TabIndex = 0;
            this.metroLabel15.Text = "预计数量:";
            // 
            // metroLabel14
            // 
            this.metroLabel14.AutoSize = true;
            this.metroLabel14.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel14.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel14.Location = new System.Drawing.Point(307, 165);
            this.metroLabel14.Name = "metroLabel14";
            this.metroLabel14.Size = new System.Drawing.Size(50, 24);
            this.metroLabel14.TabIndex = 0;
            this.metroLabel14.Text = "差异:";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel5.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel5.Location = new System.Drawing.Point(8, 81);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(86, 24);
            this.metroLabel5.TabIndex = 0;
            this.metroLabel5.Text = "门店代码:";
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel9.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel9.Location = new System.Drawing.Point(158, 111);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(50, 24);
            this.metroLabel9.TabIndex = 0;
            this.metroLabel9.Text = "总数:";
            // 
            // lblDifferentcount
            // 
            this.lblDifferentcount.AutoSize = true;
            this.lblDifferentcount.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblDifferentcount.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblDifferentcount.Location = new System.Drawing.Point(363, 165);
            this.lblDifferentcount.Name = "lblDifferentcount";
            this.lblDifferentcount.Size = new System.Drawing.Size(21, 24);
            this.lblDifferentcount.TabIndex = 0;
            this.lblDifferentcount.Text = "0";
            // 
            // lbIsRFID
            // 
            this.lbIsRFID.BackColor = System.Drawing.Color.Transparent;
            this.lbIsRFID.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lbIsRFID.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lbIsRFID.DM_UseCustomBackColor = true;
            this.lbIsRFID.DM_UseCustomForeColor = true;
            this.lbIsRFID.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbIsRFID.Location = new System.Drawing.Point(276, 51);
            this.lbIsRFID.Name = "lbIsRFID";
            this.lbIsRFID.Size = new System.Drawing.Size(123, 24);
            this.lbIsRFID.Style = DMSkin.Metro.MetroColorStyle.Blue;
            this.lbIsRFID.TabIndex = 0;
            this.lbIsRFID.Text = "RFID";
            this.lbIsRFID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOutlog
            // 
            this.lblOutlog.AutoSize = true;
            this.lblOutlog.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblOutlog.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblOutlog.Location = new System.Drawing.Point(100, 51);
            this.lblOutlog.Name = "lblOutlog";
            this.lblOutlog.Size = new System.Drawing.Size(120, 24);
            this.lblOutlog.TabIndex = 0;
            this.lblOutlog.Text = "1016045363";
            // 
            // lblTotalcount
            // 
            this.lblTotalcount.AutoSize = true;
            this.lblTotalcount.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblTotalcount.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblTotalcount.Location = new System.Drawing.Point(217, 111);
            this.lblTotalcount.Name = "lblTotalcount";
            this.lblTotalcount.Size = new System.Drawing.Size(21, 24);
            this.lblTotalcount.TabIndex = 0;
            this.lblTotalcount.Text = "0";
            // 
            // metroLabel23
            // 
            this.metroLabel23.AutoSize = true;
            this.metroLabel23.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel23.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel23.Location = new System.Drawing.Point(8, 421);
            this.metroLabel23.Name = "metroLabel23";
            this.metroLabel23.Size = new System.Drawing.Size(50, 24);
            this.metroLabel23.TabIndex = 0;
            this.metroLabel23.Text = "箱型:";
            // 
            // metroLabel21
            // 
            this.metroLabel21.AutoSize = true;
            this.metroLabel21.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel21.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel21.Location = new System.Drawing.Point(8, 385);
            this.metroLabel21.Name = "metroLabel21";
            this.metroLabel21.Size = new System.Drawing.Size(86, 24);
            this.metroLabel21.TabIndex = 0;
            this.metroLabel21.Text = "打印机号:";
            this.metroLabel21.Visible = false;
            // 
            // metroLabel19
            // 
            this.metroLabel19.AutoSize = true;
            this.metroLabel19.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel19.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel19.Location = new System.Drawing.Point(8, 264);
            this.metroLabel19.Name = "metroLabel19";
            this.metroLabel19.Size = new System.Drawing.Size(68, 24);
            this.metroLabel19.TabIndex = 0;
            this.metroLabel19.Text = "当前箱:";
            // 
            // metroLabel12
            // 
            this.metroLabel12.AutoSize = true;
            this.metroLabel12.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel12.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel12.Location = new System.Drawing.Point(157, 165);
            this.metroLabel12.Name = "metroLabel12";
            this.metroLabel12.Size = new System.Drawing.Size(86, 24);
            this.metroLabel12.TabIndex = 0;
            this.metroLabel12.Text = "实际数量:";
            // 
            // metroLabel18
            // 
            this.metroLabel18.AutoSize = true;
            this.metroLabel18.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel18.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel18.Location = new System.Drawing.Point(-5, 239);
            this.metroLabel18.Name = "metroLabel18";
            this.metroLabel18.Size = new System.Drawing.Size(447, 24);
            this.metroLabel18.TabIndex = 0;
            this.metroLabel18.Text = "———————————————————————";
            // 
            // metroLabel11
            // 
            this.metroLabel11.AutoSize = true;
            this.metroLabel11.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel11.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel11.Location = new System.Drawing.Point(-4, 132);
            this.metroLabel11.Name = "metroLabel11";
            this.metroLabel11.Size = new System.Drawing.Size(447, 24);
            this.metroLabel11.TabIndex = 0;
            this.metroLabel11.Text = "———————————————————————";
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel7.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel7.Location = new System.Drawing.Point(8, 111);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(86, 24);
            this.metroLabel7.TabIndex = 0;
            this.metroLabel7.Text = "未下架单:";
            // 
            // metroPanel2
            // 
            this.metroPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel2.Controls.Add(this.txtTestEpc);
            this.metroPanel2.Controls.Add(this.btnTest);
            this.metroPanel2.DM_HorizontalScrollbarBarColor = true;
            this.metroPanel2.DM_HorizontalScrollbarDM_HighlightOnWheel = false;
            this.metroPanel2.DM_HorizontalScrollbarSize = 10;
            this.metroPanel2.DM_ThumbColor = System.Drawing.Color.Gray;
            this.metroPanel2.DM_ThumbNormalColor = System.Drawing.Color.Gray;
            this.metroPanel2.DM_UseBarColor = true;
            this.metroPanel2.DM_VerticalScrollbarBarColor = true;
            this.metroPanel2.DM_VerticalScrollbarDM_HighlightOnWheel = false;
            this.metroPanel2.DM_VerticalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(404, 307);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(289, 89);
            this.metroPanel2.TabIndex = 13;
            this.metroPanel2.Visible = false;
            // 
            // txtTestEpc
            // 
            this.txtTestEpc.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Tall;
            this.txtTestEpc.DM_UseSelectable = true;
            this.txtTestEpc.Lines = new string[0];
            this.txtTestEpc.Location = new System.Drawing.Point(7, 11);
            this.txtTestEpc.MaxLength = 32767;
            this.txtTestEpc.Name = "txtTestEpc";
            this.txtTestEpc.PasswordChar = '\0';
            this.txtTestEpc.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTestEpc.SelectedText = "";
            this.txtTestEpc.Size = new System.Drawing.Size(275, 30);
            this.txtTestEpc.TabIndex = 2;
            this.txtTestEpc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnTest
            // 
            this.btnTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnTest.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnTest.DM_UseCustomBackColor = true;
            this.btnTest.DM_UseCustomForeColor = true;
            this.btnTest.DM_UseSelectable = true;
            this.btnTest.ForeColor = System.Drawing.Color.White;
            this.btnTest.Location = new System.Drawing.Point(7, 47);
            this.btnTest.Margin = new System.Windows.Forms.Padding(0);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(275, 30);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "我要上传这个EPC";
            this.btnTest.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnSetnoscanbox
            // 
            this.btnSetnoscanbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetnoscanbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnSetnoscanbox.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnSetnoscanbox.DM_UseCustomBackColor = true;
            this.btnSetnoscanbox.DM_UseCustomForeColor = true;
            this.btnSetnoscanbox.DM_UseSelectable = true;
            this.btnSetnoscanbox.ForeColor = System.Drawing.Color.White;
            this.btnSetnoscanbox.Location = new System.Drawing.Point(453, 529);
            this.btnSetnoscanbox.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetnoscanbox.Name = "btnSetnoscanbox";
            this.btnSetnoscanbox.Size = new System.Drawing.Size(148, 30);
            this.btnSetnoscanbox.TabIndex = 3;
            this.btnSetnoscanbox.Text = "设置为扫描箱";
            this.btnSetnoscanbox.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnSetnoscanbox.Click += new System.EventHandler(this.btnSetnoscanbox_Click);
            // 
            // lblIsScan
            // 
            this.lblIsScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblIsScan.AutoSize = true;
            this.lblIsScan.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblIsScan.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblIsScan.Location = new System.Drawing.Point(290, 532);
            this.lblIsScan.Name = "lblIsScan";
            this.lblIsScan.Size = new System.Drawing.Size(148, 24);
            this.lblIsScan.TabIndex = 12;
            this.lblIsScan.Text = "扫描箱[非扫描箱]";
            // 
            // lblBoxvalue
            // 
            this.lblBoxvalue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBoxvalue.AutoSize = true;
            this.lblBoxvalue.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblBoxvalue.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblBoxvalue.Location = new System.Drawing.Point(752, 532);
            this.lblBoxvalue.Name = "lblBoxvalue";
            this.lblBoxvalue.Size = new System.Drawing.Size(90, 24);
            this.lblBoxvalue.TabIndex = 10;
            this.lblBoxvalue.Text = "boxValue";
            this.lblBoxvalue.Visible = false;
            // 
            // lblBoxstyle
            // 
            this.lblBoxstyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBoxstyle.AutoSize = true;
            this.lblBoxstyle.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblBoxstyle.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblBoxstyle.Location = new System.Drawing.Point(72, 532);
            this.lblBoxstyle.Name = "lblBoxstyle";
            this.lblBoxstyle.Size = new System.Drawing.Size(194, 24);
            this.lblBoxstyle.TabIndex = 10;
            this.lblBoxstyle.Text = "衬衫裤子箱[59*39*39]";
            // 
            // metroLabel25
            // 
            this.metroLabel25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.metroLabel25.AutoSize = true;
            this.metroLabel25.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel25.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel25.Location = new System.Drawing.Point(16, 532);
            this.metroLabel25.Name = "metroLabel25";
            this.metroLabel25.Size = new System.Drawing.Size(50, 24);
            this.metroLabel25.TabIndex = 11;
            this.metroLabel25.Text = "箱型:";
            // 
            // metroLabel4
            // 
            this.metroLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel4.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel4.DM_UseCustomForeColor = true;
            this.metroLabel4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroLabel4.Location = new System.Drawing.Point(-28, 510);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(2670, 24);
            this.metroLabel4.TabIndex = 4;
            this.metroLabel4.Text = "—————————————————————————————————————————————————————————————————————————————————" +
    "———————————————————————————————————————————————————————————";
            // 
            // btnBarcode
            // 
            this.btnBarcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnBarcode.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnBarcode.DM_UseCustomBackColor = true;
            this.btnBarcode.DM_UseCustomForeColor = true;
            this.btnBarcode.DM_UseSelectable = true;
            this.btnBarcode.ForeColor = System.Drawing.Color.White;
            this.btnBarcode.Location = new System.Drawing.Point(343, 5);
            this.btnBarcode.Margin = new System.Windows.Forms.Padding(0);
            this.btnBarcode.Name = "btnBarcode";
            this.btnBarcode.Size = new System.Drawing.Size(75, 30);
            this.btnBarcode.TabIndex = 3;
            this.btnBarcode.Text = "扫描";
            this.btnBarcode.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnBarcode.Click += new System.EventHandler(this.btnBarcode_Click);
            // 
            // txtBarcode
            // 
            this.txtBarcode.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Tall;
            this.txtBarcode.DM_UseSelectable = true;
            this.txtBarcode.Lines = new string[0];
            this.txtBarcode.Location = new System.Drawing.Point(57, 5);
            this.txtBarcode.MaxLength = 32767;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.PasswordChar = '\0';
            this.txtBarcode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBarcode.SelectedText = "";
            this.txtBarcode.Size = new System.Drawing.Size(275, 30);
            this.txtBarcode.TabIndex = 2;
            this.txtBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_KeyPress);
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblInfo.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblInfo.DM_UseCustomForeColor = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(421, 8);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(520, 24);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // metroLabel24
            // 
            this.metroLabel24.AutoSize = true;
            this.metroLabel24.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel24.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel24.Location = new System.Drawing.Point(3, 8);
            this.metroLabel24.Name = "metroLabel24";
            this.metroLabel24.Size = new System.Drawing.Size(50, 24);
            this.metroLabel24.TabIndex = 0;
            this.metroLabel24.Text = "条码:";
            // 
            // lblCurrentcountBig
            // 
            this.lblCurrentcountBig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCurrentcountBig.AutoSize = true;
            this.lblCurrentcountBig.Font = new System.Drawing.Font("微软雅黑", 100F);
            this.lblCurrentcountBig.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblCurrentcountBig.Location = new System.Drawing.Point(3, 325);
            this.lblCurrentcountBig.Name = "lblCurrentcountBig";
            this.lblCurrentcountBig.Size = new System.Drawing.Size(125, 196);
            this.lblCurrentcountBig.TabIndex = 2;
            this.lblCurrentcountBig.Text = "0";
            this.lblCurrentcountBig.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCurrentcountBig.UseCompatibleTextRendering = true;
            this.lblCurrentcountBig.Click += new System.EventHandler(this.lblCurrentcountBig_Click);
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.NO,
            this.BoxNO,
            this.QTY,
            this.boxstyle,
            this.Del,
            this.Detail,
            this.Print,
            this.FullBox});
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
            this.grid.Location = new System.Drawing.Point(0, 39);
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
            this.grid.Size = new System.Drawing.Size(948, 482);
            this.grid.TabIndex = 0;
            this.grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellClick);
            this.grid.SelectionChanged += new System.EventHandler(this.grid_SelectionChanged);
            this.grid.Click += new System.EventHandler(this.grid_Click);
            // 
            // NO
            // 
            this.NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NO.FillWeight = 50F;
            this.NO.HeaderText = "序号";
            this.NO.Name = "NO";
            this.NO.ReadOnly = true;
            // 
            // BoxNO
            // 
            this.BoxNO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BoxNO.FillWeight = 120F;
            this.BoxNO.HeaderText = "箱号";
            this.BoxNO.Name = "BoxNO";
            this.BoxNO.ReadOnly = true;
            // 
            // QTY
            // 
            this.QTY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.QTY.FillWeight = 60F;
            this.QTY.HeaderText = "件数";
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            // 
            // boxstyle
            // 
            this.boxstyle.FillWeight = 150F;
            this.boxstyle.HeaderText = "箱型";
            this.boxstyle.Name = "boxstyle";
            this.boxstyle.ReadOnly = true;
            // 
            // Del
            // 
            this.Del.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Del.FillWeight = 50F;
            this.Del.HeaderText = "删除";
            this.Del.Name = "Del";
            this.Del.ReadOnly = true;
            // 
            // Detail
            // 
            this.Detail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Detail.FillWeight = 50F;
            this.Detail.HeaderText = "明细";
            this.Detail.Name = "Detail";
            this.Detail.ReadOnly = true;
            // 
            // Print
            // 
            this.Print.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Print.FillWeight = 50F;
            this.Print.HeaderText = "打印";
            this.Print.Name = "Print";
            this.Print.ReadOnly = true;
            // 
            // FullBox
            // 
            this.FullBox.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FullBox.FillWeight = 50F;
            this.FullBox.HeaderText = "满箱";
            this.FullBox.Name = "FullBox";
            this.FullBox.ReadOnly = true;
            // 
            // BtnSetFullBox
            // 
            this.BtnSetFullBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSetFullBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.BtnSetFullBox.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.BtnSetFullBox.DM_UseCustomBackColor = true;
            this.BtnSetFullBox.DM_UseCustomForeColor = true;
            this.BtnSetFullBox.DM_UseSelectable = true;
            this.BtnSetFullBox.ForeColor = System.Drawing.Color.White;
            this.BtnSetFullBox.Location = new System.Drawing.Point(833, 12);
            this.BtnSetFullBox.Margin = new System.Windows.Forms.Padding(0);
            this.BtnSetFullBox.Name = "BtnSetFullBox";
            this.BtnSetFullBox.Size = new System.Drawing.Size(120, 60);
            this.BtnSetFullBox.TabIndex = 3;
            this.BtnSetFullBox.Text = "设置为满箱";
            this.BtnSetFullBox.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.BtnSetFullBox.Click += new System.EventHandler(this.BtnSetFullBox_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnInventory.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnInventory.DM_UseCustomBackColor = true;
            this.btnInventory.DM_UseCustomForeColor = true;
            this.btnInventory.DM_UseSelectable = true;
            this.btnInventory.ForeColor = System.Drawing.Color.White;
            this.btnInventory.Location = new System.Drawing.Point(571, 12);
            this.btnInventory.Margin = new System.Windows.Forms.Padding(0);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(120, 60);
            this.btnInventory.TabIndex = 3;
            this.btnInventory.Text = "开始扫描";
            this.btnInventory.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnInventory.Click += new System.EventHandler(this.btnStopInventory_Click);
            // 
            // btnGetBoxNo
            // 
            this.btnGetBoxNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetBoxNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnGetBoxNo.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnGetBoxNo.DM_UseCustomBackColor = true;
            this.btnGetBoxNo.DM_UseCustomForeColor = true;
            this.btnGetBoxNo.DM_UseSelectable = true;
            this.btnGetBoxNo.Enabled = false;
            this.btnGetBoxNo.ForeColor = System.Drawing.Color.White;
            this.btnGetBoxNo.Location = new System.Drawing.Point(702, 12);
            this.btnGetBoxNo.Margin = new System.Windows.Forms.Padding(0);
            this.btnGetBoxNo.Name = "btnGetBoxNo";
            this.btnGetBoxNo.Size = new System.Drawing.Size(120, 60);
            this.btnGetBoxNo.TabIndex = 3;
            this.btnGetBoxNo.Text = "获取箱号";
            this.btnGetBoxNo.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnGetBoxNo.Click += new System.EventHandler(this.btnGetBoxNo_Click);
            // 
            // btnOutConfirm
            // 
            this.btnOutConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnOutConfirm.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnOutConfirm.DM_UseCustomBackColor = true;
            this.btnOutConfirm.DM_UseCustomForeColor = true;
            this.btnOutConfirm.DM_UseSelectable = true;
            this.btnOutConfirm.ForeColor = System.Drawing.Color.White;
            this.btnOutConfirm.Location = new System.Drawing.Point(964, 12);
            this.btnOutConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.btnOutConfirm.Name = "btnOutConfirm";
            this.btnOutConfirm.Size = new System.Drawing.Size(120, 60);
            this.btnOutConfirm.TabIndex = 3;
            this.btnOutConfirm.Text = "下架确认";
            this.btnOutConfirm.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnOutConfirm.Click += new System.EventHandler(this.btnOutConfirm_Click);
            // 
            // btnTempSave
            // 
            this.btnTempSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTempSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnTempSave.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnTempSave.DM_UseCustomBackColor = true;
            this.btnTempSave.DM_UseCustomForeColor = true;
            this.btnTempSave.DM_UseSelectable = true;
            this.btnTempSave.ForeColor = System.Drawing.Color.White;
            this.btnTempSave.Location = new System.Drawing.Point(1095, 12);
            this.btnTempSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnTempSave.Name = "btnTempSave";
            this.btnTempSave.Size = new System.Drawing.Size(120, 60);
            this.btnTempSave.TabIndex = 3;
            this.btnTempSave.Text = "中途暂存";
            this.btnTempSave.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnTempSave.Click += new System.EventHandler(this.btnTempSave_Click);
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
            this.btnCancel.Location = new System.Drawing.Point(1226, 12);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 60);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "返回";
            this.btnCancel.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnHide
            // 
            this.btnHide.BackColor = System.Drawing.Color.Transparent;
            this.btnHide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHide.DM_Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnHide.DM_Font_Size = 40F;
            this.btnHide.DM_Key = DMSkin.Controls.DMLabelKey.狙星;
            this.btnHide.DM_Text = "";
            this.btnHide.Location = new System.Drawing.Point(337, 11);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(70, 60);
            this.btnHide.TabIndex = 0;
            this.btnHide.Text = "dmLabel1";
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnOutLogDetail
            // 
            this.btnOutLogDetail.BackColor = System.Drawing.Color.Transparent;
            this.btnOutLogDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOutLogDetail.DM_Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnOutLogDetail.DM_Font_Size = 40F;
            this.btnOutLogDetail.DM_Key = DMSkin.Controls.DMLabelKey.空文件;
            this.btnOutLogDetail.DM_Text = "";
            this.btnOutLogDetail.Location = new System.Drawing.Point(258, 11);
            this.btnOutLogDetail.Name = "btnOutLogDetail";
            this.btnOutLogDetail.Size = new System.Drawing.Size(70, 60);
            this.btnOutLogDetail.TabIndex = 0;
            this.btnOutLogDetail.Text = "单据明细";
            this.btnOutLogDetail.Click += new System.EventHandler(this.btnOutLogDetail_Click);
            // 
            // btnCheckBox
            // 
            this.btnCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.btnCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckBox.DM_Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnCheckBox.DM_Font_Size = 40F;
            this.btnCheckBox.DM_Key = DMSkin.Controls.DMLabelKey.复制_文件;
            this.btnCheckBox.DM_Text = "";
            this.btnCheckBox.Location = new System.Drawing.Point(173, 11);
            this.btnCheckBox.Name = "btnCheckBox";
            this.btnCheckBox.Size = new System.Drawing.Size(70, 60);
            this.btnCheckBox.TabIndex = 0;
            this.btnCheckBox.Text = "装箱复核";
            this.btnCheckBox.Click += new System.EventHandler(this.btnCheckBox_Click);
            // 
            // btnUnUpload
            // 
            this.btnUnUpload.BackColor = System.Drawing.Color.Transparent;
            this.btnUnUpload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnUpload.DM_Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnUnUpload.DM_Font_Size = 40F;
            this.btnUnUpload.DM_Key = DMSkin.Controls.DMLabelKey.实心云;
            this.btnUnUpload.DM_Text = "";
            this.btnUnUpload.Location = new System.Drawing.Point(87, 11);
            this.btnUnUpload.Name = "btnUnUpload";
            this.btnUnUpload.Size = new System.Drawing.Size(70, 60);
            this.btnUnUpload.TabIndex = 0;
            this.btnUnUpload.Text = "dmLabel1";
            this.btnUnUpload.Click += new System.EventHandler(this.btnUnUpload_Click);
            // 
            // btnKeyboard
            // 
            this.btnKeyboard.BackColor = System.Drawing.Color.Transparent;
            this.btnKeyboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKeyboard.DM_Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnKeyboard.DM_Font_Size = 40F;
            this.btnKeyboard.DM_Key = DMSkin.Controls.DMLabelKey.键盘;
            this.btnKeyboard.DM_Text = "";
            this.btnKeyboard.Location = new System.Drawing.Point(1, 11);
            this.btnKeyboard.Name = "btnKeyboard";
            this.btnKeyboard.Size = new System.Drawing.Size(70, 60);
            this.btnKeyboard.TabIndex = 0;
            this.btnKeyboard.Text = "dmLabel1";
            this.btnKeyboard.Click += new System.EventHandler(this.btnKeyboard_Click);
            // 
            // InventoryOutLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1366, 727);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.DM_CanMove = false;
            this.DM_EffectCaption = DMSkin.TitleType.None;
            this.DM_howBorder = false;
            this.DM_ShowDrawIcon = false;
            this.DoubleBuffered = false;
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InventoryOutLogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InventoryOutLogForm_FormClosing);
            this.Load += new System.EventHandler(this.InventoryOutLogForm_Load);
            this.Shown += new System.EventHandler(this.InventoryOutLogForm_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.metroPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DMSkin.Metro.Controls.MetroLabel metroLabel1;
        private DMSkin.Metro.Controls.MetroToggle togHistory;
        private DMSkin.Metro.Controls.MetroTextBox txtOutlog;
        private DMSkin.Metro.Controls.MetroLabel metroLabel2;
        private DMSkin.Metro.Controls.MetroToggle togAutoprint;
        private DMSkin.Metro.Controls.MetroButton btnQuery;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Splitter splitter1;
        private DMSkin.Metro.Controls.MetroGrid grid;
        private DMSkin.Metro.Controls.MetroButton btnImportBox;
        private DMSkin.Metro.Controls.MetroComboBox cboBoxstyle;
        private DMSkin.Metro.Controls.MetroTextBox txtPrinter;
        private DMSkin.Metro.Controls.MetroTextBox txtImportBoxNo;
        private DMSkin.Metro.Controls.MetroTextBox txtFloor;
        private DMSkin.Metro.Controls.MetroTile metroTile3;
        private DMSkin.Metro.Controls.MetroTile metroTile2;
        private DMSkin.Metro.Controls.MetroTile metroTile1;
        private DMSkin.Metro.Controls.MetroLabel metroLabel3;
        private DMSkin.Metro.Controls.MetroLabel lblPlancount;
        private DMSkin.Metro.Controls.MetroLabel lblStore;
        private DMSkin.Metro.Controls.MetroLabel lblCurrentcount;
        private DMSkin.Metro.Controls.MetroLabel lblRealcount;
        private DMSkin.Metro.Controls.MetroLabel lblNooutcount;
        private DMSkin.Metro.Controls.MetroLabel metroLabel22;
        private DMSkin.Metro.Controls.MetroLabel metroLabel15;
        private DMSkin.Metro.Controls.MetroLabel metroLabel14;
        private DMSkin.Metro.Controls.MetroLabel metroLabel5;
        private DMSkin.Metro.Controls.MetroLabel metroLabel9;
        private DMSkin.Metro.Controls.MetroLabel lblDifferentcount;
        private DMSkin.Metro.Controls.MetroLabel lblOutlog;
        private DMSkin.Metro.Controls.MetroLabel lblTotalcount;
        private DMSkin.Metro.Controls.MetroLabel metroLabel23;
        private DMSkin.Metro.Controls.MetroLabel metroLabel21;
        private DMSkin.Metro.Controls.MetroLabel metroLabel19;
        private DMSkin.Metro.Controls.MetroLabel metroLabel12;
        private DMSkin.Metro.Controls.MetroLabel metroLabel18;
        private DMSkin.Metro.Controls.MetroLabel metroLabel11;
        private DMSkin.Metro.Controls.MetroLabel metroLabel7;
        private System.Windows.Forms.Label lblCurrentcountBig;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private DMSkin.Controls.DMLabel btnHide;
        private DMSkin.Controls.DMLabel btnOutLogDetail;
        private DMSkin.Controls.DMLabel btnCheckBox;
        private DMSkin.Controls.DMLabel btnUnUpload;
        private DMSkin.Controls.DMLabel btnKeyboard;
        private DMSkin.Metro.Controls.MetroButton BtnSetFullBox;
        private DMSkin.Metro.Controls.MetroButton btnInventory;
        private DMSkin.Metro.Controls.MetroButton btnGetBoxNo;
        private DMSkin.Metro.Controls.MetroButton btnOutConfirm;
        private DMSkin.Metro.Controls.MetroButton btnTempSave;
        private DMSkin.Metro.Controls.MetroButton btnCancel;
        private DMSkin.Metro.Controls.MetroButton btnBarcode;
        private DMSkin.Metro.Controls.MetroLabel metroLabel24;
        private DMSkin.Metro.Controls.MetroTextBox txtBarcode;
        private DMSkin.Metro.Controls.MetroButton btnSetnoscanbox;
        private DMSkin.Metro.Controls.MetroLabel lblIsScan;
        private DMSkin.Metro.Controls.MetroLabel lblBoxstyle;
        private DMSkin.Metro.Controls.MetroLabel metroLabel25;
        private DMSkin.Metro.Controls.MetroLabel metroLabel4;
        private DMSkin.Metro.Controls.MetroLabel lblBoxvalue;
        private DMSkin.Metro.Controls.MetroLabel lblInfo;
        private DMSkin.Metro.Controls.MetroButton btnStoreOutLogDetail;
        private DMSkin.Metro.Controls.MetroLabel lblAuPlancount;
        private DMSkin.Metro.Controls.MetroLabel lblAuRealcount;
        private DMSkin.Metro.Controls.MetroLabel metroLabel10;
        private DMSkin.Metro.Controls.MetroLabel metroLabel13;
        private DMSkin.Metro.Controls.MetroLabel lblAuDifferentcount;
        private DMSkin.Metro.Controls.MetroLabel metroLabel17;
        private DMSkin.Metro.Controls.MetroPanel metroPanel1;
        private System.Windows.Forms.Timer tmeStop;
        private DMSkin.Metro.Controls.MetroLabel lbIsRFID;
        private DMSkin.Metro.Controls.MetroLabel lbcurrentuser;
        private System.Windows.Forms.DataGridViewTextBoxColumn NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn BoxNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn boxstyle;
        private System.Windows.Forms.DataGridViewImageColumn Del;
        private System.Windows.Forms.DataGridViewImageColumn Detail;
        private System.Windows.Forms.DataGridViewImageColumn Print;
        private System.Windows.Forms.DataGridViewImageColumn FullBox;
        private DMSkin.Metro.Controls.MetroLabel lblPinXiang;
        private DMSkin.Metro.Controls.MetroLabel metroLabel6;
        private DMSkin.Metro.Controls.MetroToggle togLocalprint;
        private DMSkin.Metro.Controls.MetroPanel metroPanel2;
        private DMSkin.Metro.Controls.MetroTextBox txtTestEpc;
        private DMSkin.Metro.Controls.MetroButton btnTest;
        private DMSkin.Metro.Controls.MetroButton btnSetDefault;
        private HLACommonView.Views.CheckedComboBox LGTYPCheckedComboBox;
    }
}