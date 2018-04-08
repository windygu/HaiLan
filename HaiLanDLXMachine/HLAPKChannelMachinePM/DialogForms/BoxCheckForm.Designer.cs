namespace HLADeliverChannelMachine.DialogForms
{
    partial class BoxCheckForm
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
            this.NUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SCAN_NUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DIFF_NUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NUM_ADD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SCAN_NUM_ADD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DIFF_NUM_ADD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBoxNo = new DMSkin.Metro.Controls.MetroTextBox();
            this.metroLabel1 = new DMSkin.Metro.Controls.MetroLabel();
            this.btnQuery = new DMSkin.Metro.Controls.MetroButton();
            this.lblEpc = new DMSkin.Metro.Controls.MetroLabel();
            this.txtBarcode = new DMSkin.Metro.Controls.MetroTextBox();
            this.metroLabel2 = new DMSkin.Metro.Controls.MetroLabel();
            this.btnReturn = new DMSkin.Metro.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
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
            this.NUM,
            this.SCAN_NUM,
            this.DIFF_NUM,
            this.NUM_ADD,
            this.SCAN_NUM_ADD,
            this.DIFF_NUM_ADD});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle3;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.GridColor = System.Drawing.Color.DarkGray;
            this.grid.Location = new System.Drawing.Point(9, 164);
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
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.grid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.RowTemplate.Height = 43;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(986, 372);
            this.grid.TabIndex = 3;
            // 
            // ZSATNR
            // 
            this.ZSATNR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ZSATNR.FillWeight = 15F;
            this.ZSATNR.HeaderText = "品号";
            this.ZSATNR.Name = "ZSATNR";
            this.ZSATNR.ReadOnly = true;
            // 
            // ZCOLSN
            // 
            this.ZCOLSN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ZCOLSN.FillWeight = 10F;
            this.ZCOLSN.HeaderText = "色号";
            this.ZCOLSN.Name = "ZCOLSN";
            this.ZCOLSN.ReadOnly = true;
            // 
            // ZSIZTX
            // 
            this.ZSIZTX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ZSIZTX.FillWeight = 10F;
            this.ZSIZTX.HeaderText = "规格";
            this.ZSIZTX.Name = "ZSIZTX";
            this.ZSIZTX.ReadOnly = true;
            this.ZSIZTX.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ZSIZTX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NUM
            // 
            this.NUM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NUM.FillWeight = 9F;
            this.NUM.HeaderText = "原有数量";
            this.NUM.Name = "NUM";
            this.NUM.ReadOnly = true;
            // 
            // SCAN_NUM
            // 
            this.SCAN_NUM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SCAN_NUM.FillWeight = 9F;
            this.SCAN_NUM.HeaderText = "扫描数量";
            this.SCAN_NUM.Name = "SCAN_NUM";
            this.SCAN_NUM.ReadOnly = true;
            // 
            // DIFF_NUM
            // 
            this.DIFF_NUM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DIFF_NUM.FillWeight = 9F;
            this.DIFF_NUM.HeaderText = "差异";
            this.DIFF_NUM.Name = "DIFF_NUM";
            this.DIFF_NUM.ReadOnly = true;
            // 
            // NUM_ADD
            // 
            this.NUM_ADD.FillWeight = 15F;
            this.NUM_ADD.HeaderText = "原有数量(辅助料)";
            this.NUM_ADD.Name = "NUM_ADD";
            this.NUM_ADD.ReadOnly = true;
            // 
            // SCAN_NUM_ADD
            // 
            this.SCAN_NUM_ADD.FillWeight = 15F;
            this.SCAN_NUM_ADD.HeaderText = "扫描数量(辅助料)";
            this.SCAN_NUM_ADD.Name = "SCAN_NUM_ADD";
            this.SCAN_NUM_ADD.ReadOnly = true;
            // 
            // DIFF_NUM_ADD
            // 
            this.DIFF_NUM_ADD.FillWeight = 15F;
            this.DIFF_NUM_ADD.HeaderText = "差异(辅助料)";
            this.DIFF_NUM_ADD.Name = "DIFF_NUM_ADD";
            this.DIFF_NUM_ADD.ReadOnly = true;
            // 
            // txtBoxNo
            // 
            this.txtBoxNo.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Tall;
            this.txtBoxNo.DM_UseSelectable = true;
            this.txtBoxNo.Lines = new string[0];
            this.txtBoxNo.Location = new System.Drawing.Point(90, 72);
            this.txtBoxNo.MaxLength = 32767;
            this.txtBoxNo.Name = "txtBoxNo";
            this.txtBoxNo.PasswordChar = '\0';
            this.txtBoxNo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBoxNo.SelectedText = "";
            this.txtBoxNo.Size = new System.Drawing.Size(275, 33);
            this.txtBoxNo.TabIndex = 5;
            this.txtBoxNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel1.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel1.Location = new System.Drawing.Point(23, 75);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(60, 24);
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "箱  码:";
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnQuery.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnQuery.DM_UseCustomBackColor = true;
            this.btnQuery.DM_UseCustomForeColor = true;
            this.btnQuery.DM_UseSelectable = true;
            this.btnQuery.ForeColor = System.Drawing.Color.White;
            this.btnQuery.Location = new System.Drawing.Point(380, 72);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(0);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(90, 33);
            this.btnQuery.TabIndex = 6;
            this.btnQuery.Text = "查询";
            this.btnQuery.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // lblEpc
            // 
            this.lblEpc.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.lblEpc.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.lblEpc.DM_UseCustomForeColor = true;
            this.lblEpc.ForeColor = System.Drawing.Color.Red;
            this.lblEpc.Location = new System.Drawing.Point(485, 116);
            this.lblEpc.Name = "lblEpc";
            this.lblEpc.Size = new System.Drawing.Size(510, 33);
            this.lblEpc.TabIndex = 4;
            this.lblEpc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBarcode
            // 
            this.txtBarcode.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Tall;
            this.txtBarcode.DM_UseSelectable = true;
            this.txtBarcode.Lines = new string[0];
            this.txtBarcode.Location = new System.Drawing.Point(90, 116);
            this.txtBarcode.MaxLength = 32767;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.PasswordChar = '\0';
            this.txtBarcode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBarcode.SelectedText = "";
            this.txtBarcode.Size = new System.Drawing.Size(275, 33);
            this.txtBarcode.TabIndex = 9;
            this.txtBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_KeyPress);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.DM_FontSize = DMSkin.Metro.MetroLabelSize.Tall;
            this.metroLabel2.DM_FontWeight = DMSkin.Metro.MetroLabelWeight.Bold;
            this.metroLabel2.Location = new System.Drawing.Point(23, 119);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(60, 24);
            this.metroLabel2.TabIndex = 8;
            this.metroLabel2.Text = "条  码:";
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnReturn.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnReturn.DM_UseCustomBackColor = true;
            this.btnReturn.DM_UseCustomForeColor = true;
            this.btnReturn.DM_UseSelectable = true;
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(900, 550);
            this.btnReturn.Margin = new System.Windows.Forms.Padding(0);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(90, 33);
            this.btnReturn.TabIndex = 10;
            this.btnReturn.Text = "返回";
            this.btnReturn.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // BoxCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 605);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtBoxNo);
            this.Controls.Add(this.lblEpc);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.grid);
            this.Name = "BoxCheckForm";
            this.Padding = new System.Windows.Forms.Padding(20, 65, 20, 22);
            this.Text = "箱复核";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BoxCheckForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DMSkin.Metro.Controls.MetroGrid grid;
        private DMSkin.Metro.Controls.MetroTextBox txtBoxNo;
        private DMSkin.Metro.Controls.MetroLabel metroLabel1;
        private DMSkin.Metro.Controls.MetroButton btnQuery;
        private DMSkin.Metro.Controls.MetroLabel lblEpc;
        private DMSkin.Metro.Controls.MetroTextBox txtBarcode;
        private DMSkin.Metro.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZSATNR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZCOLSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZSIZTX;
        private System.Windows.Forms.DataGridViewTextBoxColumn NUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn SCAN_NUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn DIFF_NUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn NUM_ADD;
        private System.Windows.Forms.DataGridViewTextBoxColumn SCAN_NUM_ADD;
        private System.Windows.Forms.DataGridViewTextBoxColumn DIFF_NUM_ADD;
        private DMSkin.Metro.Controls.MetroButton btnReturn;
    }
}