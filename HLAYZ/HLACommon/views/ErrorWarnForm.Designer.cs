namespace HLACommon.Views
{
    partial class ErrorWarnForm
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
            this.btnKeyboard = new DMSkin.Controls.DMLabel();
            this.button1_clear = new System.Windows.Forms.Button();
            this.button3_close = new System.Windows.Forms.Button();
            this.dataGridView1_showGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1_showGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnKeyboard
            // 
            this.btnKeyboard.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnKeyboard.BackColor = System.Drawing.Color.Transparent;
            this.btnKeyboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKeyboard.DM_Color = System.Drawing.Color.OrangeRed;
            this.btnKeyboard.DM_Font_Size = 30F;
            this.btnKeyboard.DM_Key = DMSkin.Controls.DMLabelKey.圆_叹;
            this.btnKeyboard.DM_Text = "";
            this.btnKeyboard.Location = new System.Drawing.Point(346, 18);
            this.btnKeyboard.Name = "btnKeyboard";
            this.btnKeyboard.Size = new System.Drawing.Size(46, 39);
            this.btnKeyboard.TabIndex = 9;
            this.btnKeyboard.Text = "dmLabel1";
            // 
            // button1_clear
            // 
            this.button1_clear.BackColor = System.Drawing.Color.Teal;
            this.button1_clear.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1_clear.ForeColor = System.Drawing.Color.White;
            this.button1_clear.Location = new System.Drawing.Point(8, 463);
            this.button1_clear.Name = "button1_clear";
            this.button1_clear.Size = new System.Drawing.Size(237, 62);
            this.button1_clear.TabIndex = 18;
            this.button1_clear.Text = "清除";
            this.button1_clear.UseVisualStyleBackColor = false;
            this.button1_clear.Click += new System.EventHandler(this.button1_clear_Click);
            // 
            // button3_close
            // 
            this.button3_close.BackColor = System.Drawing.Color.Teal;
            this.button3_close.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3_close.ForeColor = System.Drawing.Color.White;
            this.button3_close.Location = new System.Drawing.Point(745, 463);
            this.button3_close.Name = "button3_close";
            this.button3_close.Size = new System.Drawing.Size(235, 62);
            this.button3_close.TabIndex = 20;
            this.button3_close.Text = "返回";
            this.button3_close.UseVisualStyleBackColor = false;
            this.button3_close.Click += new System.EventHandler(this.button3_close_Click);
            // 
            // dataGridView1_showGrid
            // 
            this.dataGridView1_showGrid.AllowUserToAddRows = false;
            this.dataGridView1_showGrid.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1_showGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1_showGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1_showGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dataGridView1_showGrid.Location = new System.Drawing.Point(8, 63);
            this.dataGridView1_showGrid.MultiSelect = false;
            this.dataGridView1_showGrid.Name = "dataGridView1_showGrid";
            this.dataGridView1_showGrid.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1_showGrid.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1_showGrid.RowTemplate.Height = 23;
            this.dataGridView1_showGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1_showGrid.Size = new System.Drawing.Size(972, 380);
            this.dataGridView1_showGrid.TabIndex = 21;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column1.HeaderText = "品号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 74;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column2.HeaderText = "色号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 74;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column3.HeaderText = "规格";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 74;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column4.HeaderText = "数量";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 74;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column5.HeaderText = "错误描述";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 114;
            // 
            // ErrorWarnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 548);
            this.Controls.Add(this.dataGridView1_showGrid);
            this.Controls.Add(this.button3_close);
            this.Controls.Add(this.button1_clear);
            this.Controls.Add(this.btnKeyboard);
            this.Name = "ErrorWarnForm";
            this.Text = "扫描过程发现错误";
            this.TextAlign = DMSkin.MetroFormTextAlign.Center;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ErrorWarnForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1_showGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DMSkin.Controls.DMLabel btnKeyboard;
        private System.Windows.Forms.Button button1_clear;
        private System.Windows.Forms.Button button3_close;
        private System.Windows.Forms.DataGridView dataGridView1_showGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}