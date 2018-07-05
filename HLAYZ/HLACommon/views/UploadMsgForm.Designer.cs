namespace HLACommon.Views
{
    partial class UploadMsgForm<T>
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
            this.btnReturn = new DMSkin.Metro.Controls.MetroButton();
            this.btnReupload = new DMSkin.Metro.Controls.MetroButton();
            this.metroButton1 = new DMSkin.Metro.Controls.MetroButton();
            this.metroButton2 = new DMSkin.Metro.Controls.MetroButton();
            this.metroButton3_cancel = new DMSkin.Metro.Controls.MetroButton();
            this.metroPanel1 = new DMSkin.Metro.Controls.MetroPanel();
            this.lblText = new System.Windows.Forms.Label();
            this.metroProgressSpinner1 = new DMSkin.Metro.Controls.MetroProgressSpinner();
            this.grid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.BackColor = System.Drawing.Color.Teal;
            this.btnReturn.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnReturn.DM_UseCustomBackColor = true;
            this.btnReturn.DM_UseCustomForeColor = true;
            this.btnReturn.DM_UseSelectable = true;
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(600, 407);
            this.btnReturn.Margin = new System.Windows.Forms.Padding(0);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(97, 50);
            this.btnReturn.TabIndex = 5;
            this.btnReturn.Text = "清除";
            this.btnReturn.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnReupload
            // 
            this.btnReupload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReupload.BackColor = System.Drawing.Color.Teal;
            this.btnReupload.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnReupload.DM_UseCustomBackColor = true;
            this.btnReupload.DM_UseCustomForeColor = true;
            this.btnReupload.DM_UseSelectable = true;
            this.btnReupload.ForeColor = System.Drawing.Color.White;
            this.btnReupload.Location = new System.Drawing.Point(23, 407);
            this.btnReupload.Margin = new System.Windows.Forms.Padding(0);
            this.btnReupload.Name = "btnReupload";
            this.btnReupload.Size = new System.Drawing.Size(235, 50);
            this.btnReupload.TabIndex = 5;
            this.btnReupload.Text = "重新上传";
            this.btnReupload.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnReupload.Click += new System.EventHandler(this.btnReupload_Click);
            // 
            // metroButton1
            // 
            this.metroButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroButton1.BackColor = System.Drawing.Color.Teal;
            this.metroButton1.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.metroButton1.DM_UseCustomBackColor = true;
            this.metroButton1.DM_UseCustomForeColor = true;
            this.metroButton1.DM_UseSelectable = true;
            this.metroButton1.ForeColor = System.Drawing.Color.White;
            this.metroButton1.Location = new System.Drawing.Point(294, 407);
            this.metroButton1.Margin = new System.Windows.Forms.Padding(0);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(127, 50);
            this.metroButton1.TabIndex = 6;
            this.metroButton1.Text = "全选/全不选";
            this.metroButton1.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // metroButton2
            // 
            this.metroButton2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroButton2.BackColor = System.Drawing.Color.Teal;
            this.metroButton2.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.metroButton2.DM_UseCustomBackColor = true;
            this.metroButton2.DM_UseCustomForeColor = true;
            this.metroButton2.DM_UseSelectable = true;
            this.metroButton2.ForeColor = System.Drawing.Color.White;
            this.metroButton2.Location = new System.Drawing.Point(471, 407);
            this.metroButton2.Margin = new System.Windows.Forms.Padding(0);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(85, 50);
            this.metroButton2.TabIndex = 7;
            this.metroButton2.Text = "刷新";
            this.metroButton2.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // metroButton3_cancel
            // 
            this.metroButton3_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroButton3_cancel.BackColor = System.Drawing.Color.Teal;
            this.metroButton3_cancel.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.metroButton3_cancel.DM_UseCustomBackColor = true;
            this.metroButton3_cancel.DM_UseCustomForeColor = true;
            this.metroButton3_cancel.DM_UseSelectable = true;
            this.metroButton3_cancel.ForeColor = System.Drawing.Color.White;
            this.metroButton3_cancel.Location = new System.Drawing.Point(745, 407);
            this.metroButton3_cancel.Margin = new System.Windows.Forms.Padding(0);
            this.metroButton3_cancel.Name = "metroButton3_cancel";
            this.metroButton3_cancel.Size = new System.Drawing.Size(152, 50);
            this.metroButton3_cancel.TabIndex = 8;
            this.metroButton3_cancel.Text = "退出";
            this.metroButton3_cancel.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.metroButton3_cancel.Click += new System.EventHandler(this.metroButton3_cancel_Click);
            // 
            // metroPanel1
            // 
            this.metroPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(163)))), ((int)(((byte)(203)))));
            this.metroPanel1.Controls.Add(this.lblText);
            this.metroPanel1.Controls.Add(this.metroProgressSpinner1);
            this.metroPanel1.DM_HorizontalScrollbarBarColor = true;
            this.metroPanel1.DM_HorizontalScrollbarDM_HighlightOnWheel = false;
            this.metroPanel1.DM_HorizontalScrollbarSize = 10;
            this.metroPanel1.DM_ThumbColor = System.Drawing.Color.Gray;
            this.metroPanel1.DM_ThumbNormalColor = System.Drawing.Color.Gray;
            this.metroPanel1.DM_UseBarColor = true;
            this.metroPanel1.DM_UseCustomBackColor = true;
            this.metroPanel1.DM_VerticalScrollbarBarColor = true;
            this.metroPanel1.DM_VerticalScrollbarDM_HighlightOnWheel = false;
            this.metroPanel1.DM_VerticalScrollbarSize = 10;
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.metroPanel1.Location = new System.Drawing.Point(20, 466);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(880, 30);
            this.metroPanel1.TabIndex = 9;
            this.metroPanel1.Visible = false;
            // 
            // lblText
            // 
            this.lblText.BackColor = System.Drawing.Color.Teal;
            this.lblText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblText.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblText.ForeColor = System.Drawing.Color.White;
            this.lblText.Location = new System.Drawing.Point(30, 0);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(850, 30);
            this.lblText.TabIndex = 3;
            this.lblText.Text = "Loading";
            this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.BackColor = System.Drawing.Color.Teal;
            this.metroProgressSpinner1.DM_Maximum = 100;
            this.metroProgressSpinner1.DM_UseCustomBackColor = true;
            this.metroProgressSpinner1.DM_UseSelectable = true;
            this.metroProgressSpinner1.DM_Value = 90;
            this.metroProgressSpinner1.Dock = System.Windows.Forms.DockStyle.Left;
            this.metroProgressSpinner1.Location = new System.Drawing.Point(0, 0);
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(30, 30);
            this.metroProgressSpinner1.Style = DMSkin.Metro.MetroColorStyle.White;
            this.metroProgressSpinner1.TabIndex = 2;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Column2});
            this.grid.Location = new System.Drawing.Point(23, 57);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grid.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.grid.RowTemplate.Height = 23;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(874, 342);
            this.grid.TabIndex = 22;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn1.HeaderText = "箱号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 74;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column2.HeaderText = "状态";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 74;
            // 
            // UploadMsgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 516);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.metroButton3_cancel);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.btnReupload);
            this.Controls.Add(this.btnReturn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UploadMsgForm";
            this.Text = "异常列表";
            this.Load += new System.EventHandler(this.UploadMgForm_Load);
            this.metroPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DMSkin.Metro.Controls.MetroButton btnReturn;
        private DMSkin.Metro.Controls.MetroButton btnReupload;
        private DMSkin.Metro.Controls.MetroButton metroButton1;
        private DMSkin.Metro.Controls.MetroButton metroButton2;
        private DMSkin.Metro.Controls.MetroButton metroButton3_cancel;
        private DMSkin.Metro.Controls.MetroPanel metroPanel1;
        private System.Windows.Forms.Label lblText;
        private DMSkin.Metro.Controls.MetroProgressSpinner metroProgressSpinner1;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}