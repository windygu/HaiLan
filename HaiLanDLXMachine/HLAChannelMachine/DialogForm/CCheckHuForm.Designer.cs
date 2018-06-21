namespace HLAChannelMachine
{
    partial class CCheckHuForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cboType = new DMSkin.Metro.Controls.MetroComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grid2 = new DMSkin.Metro.Controls.MetroGrid();
            this.btnBarcode = new DMSkin.Metro.Controls.MetroButton();
            this.txtHU = new DMSkin.Metro.Controls.MetroTextBox();
            this.EPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsChecked = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).BeginInit();
            this.SuspendLayout();
            // 
            // cboType
            // 
            this.cboType.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cboType.DM_UseSelectable = true;
            this.cboType.FormattingEnabled = true;
            this.cboType.ItemHeight = 24;
            this.cboType.Items.AddRange(new object[] {
            "交接单收货",
            "交货单收货"});
            this.cboType.Location = new System.Drawing.Point(157, 433);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(169, 30);
            this.cboType.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.label2.Location = new System.Drawing.Point(39, 436);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 27);
            this.label2.TabIndex = 20;
            this.label2.Text = "选择类型：";
            // 
            // grid2
            // 
            this.grid2.AllowUserToAddRows = false;
            this.grid2.AllowUserToDeleteRows = false;
            this.grid2.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.grid2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.grid2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid2.BackgroundColor = System.Drawing.Color.White;
            this.grid2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.grid2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.grid2.ColumnHeadersHeight = 43;
            this.grid2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EPC,
            this.HU,
            this.dataGridViewTextBoxColumn5,
            this.IsChecked});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid2.DefaultCellStyle = dataGridViewCellStyle13;
            this.grid2.EnableHeadersVisualStyles = false;
            this.grid2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid2.GridColor = System.Drawing.Color.DarkGray;
            this.grid2.Location = new System.Drawing.Point(12, 12);
            this.grid2.MultiSelect = false;
            this.grid2.Name = "grid2";
            this.grid2.ReadOnly = true;
            this.grid2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid2.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.grid2.RowHeadersVisible = false;
            this.grid2.RowHeadersWidth = 43;
            this.grid2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid2.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.grid2.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid2.RowTemplate.Height = 43;
            this.grid2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid2.Size = new System.Drawing.Size(931, 396);
            this.grid2.TabIndex = 19;
            // 
            // btnBarcode
            // 
            this.btnBarcode.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBarcode.BackColor = System.Drawing.Color.Teal;
            this.btnBarcode.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnBarcode.DM_UseCustomBackColor = true;
            this.btnBarcode.DM_UseCustomForeColor = true;
            this.btnBarcode.DM_UseSelectable = true;
            this.btnBarcode.ForeColor = System.Drawing.Color.White;
            this.btnBarcode.Location = new System.Drawing.Point(599, 433);
            this.btnBarcode.Margin = new System.Windows.Forms.Padding(0);
            this.btnBarcode.Name = "btnBarcode";
            this.btnBarcode.Size = new System.Drawing.Size(147, 30);
            this.btnBarcode.TabIndex = 18;
            this.btnBarcode.Text = "查询";
            this.btnBarcode.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnBarcode.Click += new System.EventHandler(this.btnBarcode_Click);
            // 
            // txtHU
            // 
            this.txtHU.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtHU.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Tall;
            this.txtHU.DM_UseSelectable = true;
            this.txtHU.Lines = new string[0];
            this.txtHU.Location = new System.Drawing.Point(359, 433);
            this.txtHU.MaxLength = 32767;
            this.txtHU.Name = "txtHU";
            this.txtHU.PasswordChar = '\0';
            this.txtHU.PromptText = "请扫描箱码";
            this.txtHU.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtHU.SelectedText = "";
            this.txtHU.Size = new System.Drawing.Size(219, 30);
            this.txtHU.TabIndex = 17;
            this.txtHU.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // EPC
            // 
            this.EPC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.EPC.FillWeight = 150F;
            this.EPC.HeaderText = "EPC";
            this.EPC.Name = "EPC";
            this.EPC.ReadOnly = true;
            this.EPC.Width = 69;
            // 
            // HU
            // 
            this.HU.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.HU.HeaderText = "箱码";
            this.HU.Name = "HU";
            this.HU.ReadOnly = true;
            this.HU.Width = 76;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn5.FillWeight = 150F;
            this.dataGridViewTextBoxColumn5.HeaderText = "备注";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 76;
            // 
            // IsChecked
            // 
            this.IsChecked.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.IsChecked.FillWeight = 80F;
            this.IsChecked.HeaderText = "是否检测";
            this.IsChecked.Name = "IsChecked";
            this.IsChecked.ReadOnly = true;
            this.IsChecked.Width = 116;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.OrangeRed;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Teal;
            this.btnClose.Location = new System.Drawing.Point(800, 423);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(143, 51);
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // CCheckHuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(955, 486);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grid2);
            this.Controls.Add(this.btnBarcode);
            this.Controls.Add(this.txtHU);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CCheckHuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "上传任务";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.UploadForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DMSkin.Metro.Controls.MetroComboBox cboType;
        private System.Windows.Forms.Label label2;
        private DMSkin.Metro.Controls.MetroGrid grid2;
        private System.Windows.Forms.DataGridViewTextBoxColumn EPC;
        private System.Windows.Forms.DataGridViewTextBoxColumn HU;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsChecked;
        private DMSkin.Metro.Controls.MetroButton btnBarcode;
        private DMSkin.Metro.Controls.MetroTextBox txtHU;
        private System.Windows.Forms.Button btnClose;
    }
}