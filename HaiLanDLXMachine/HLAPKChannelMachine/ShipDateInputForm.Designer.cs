namespace HLAPKChannelMachine
{
    partial class ShipDateInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShipDateInputForm));
            this.dtShip = new DMSkin.Metro.Controls.MetroDateTime();
            this.btnDeliver = new DMSkin.Metro.Controls.MetroButton();
            this.panelLoading = new System.Windows.Forms.Panel();
            this.pbLoading = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnClose = new DMSkin.Metro.Controls.MetroButton();
            this.panelLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // dtShip
            // 
            this.dtShip.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtShip.CalendarFont = new System.Drawing.Font("微软雅黑", 15F);
            this.dtShip.Location = new System.Drawing.Point(180, 194);
            this.dtShip.MinimumSize = new System.Drawing.Size(0, 30);
            this.dtShip.Name = "dtShip";
            this.dtShip.Size = new System.Drawing.Size(182, 30);
            this.dtShip.TabIndex = 0;
            this.dtShip.Value = new System.DateTime(2015, 11, 19, 9, 35, 17, 0);
            // 
            // btnDeliver
            // 
            this.btnDeliver.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDeliver.DM_DM_DisplayFocus = true;
            this.btnDeliver.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnDeliver.DM_UseSelectable = true;
            this.btnDeliver.Location = new System.Drawing.Point(368, 194);
            this.btnDeliver.Name = "btnDeliver";
            this.btnDeliver.Size = new System.Drawing.Size(107, 30);
            this.btnDeliver.TabIndex = 3;
            this.btnDeliver.Text = "开始发货";
            this.btnDeliver.Click += new System.EventHandler(this.btnDeliver_Click);
            // 
            // panelLoading
            // 
            this.panelLoading.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panelLoading.Controls.Add(this.pbLoading);
            this.panelLoading.Controls.Add(this.lblStatus);
            this.panelLoading.Location = new System.Drawing.Point(180, 359);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.Size = new System.Drawing.Size(295, 63);
            this.panelLoading.TabIndex = 10;
            this.panelLoading.Visible = false;
            // 
            // pbLoading
            // 
            this.pbLoading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLoading.Image = ((System.Drawing.Image)(resources.GetObject("pbLoading.Image")));
            this.pbLoading.Location = new System.Drawing.Point(136, 4);
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(31, 31);
            this.pbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbLoading.TabIndex = 7;
            this.pbLoading.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblStatus.Location = new System.Drawing.Point(3, 39);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(289, 23);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Crimson;
            this.btnClose.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnClose.DM_UseCustomBackColor = true;
            this.btnClose.DM_UseCustomForeColor = true;
            this.btnClose.DM_UseSelectable = true;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(602, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "X";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ShipDateInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 426);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panelLoading);
            this.Controls.Add(this.btnDeliver);
            this.Controls.Add(this.dtShip);
            this.Name = "ShipDateInputForm";
            this.Text = "选择发运日期";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ShipDateInputForm_Load);
            this.panelLoading.ResumeLayout(false);
            this.panelLoading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DMSkin.Metro.Controls.MetroDateTime dtShip;
        private DMSkin.Metro.Controls.MetroButton btnDeliver;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.PictureBox pbLoading;
        private System.Windows.Forms.Label lblStatus;
        private DMSkin.Metro.Controls.MetroButton btnClose;
    }
}