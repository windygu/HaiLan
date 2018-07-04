namespace HLACommonView.Views
{
    partial class CommonInventoryFormYZ
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommonInventoryForm));
            this.metroPanel1 = new DMSkin.Metro.Controls.MetroPanel();
            this.lblText = new System.Windows.Forms.Label();
            this.metroProgressSpinner1 = new DMSkin.Metro.Controls.MetroProgressSpinner();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
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
            this.metroPanel1.Location = new System.Drawing.Point(0, 262);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(673, 30);
            this.metroPanel1.TabIndex = 0;
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
            this.lblText.Size = new System.Drawing.Size(643, 30);
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
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // CommonInventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 292);
            this.Controls.Add(this.metroPanel1);
            this.DisplayHeader = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CommonInventoryForm";
            this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.metroPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DMSkin.Metro.Controls.MetroPanel metroPanel1;
        private DMSkin.Metro.Controls.MetroProgressSpinner metroProgressSpinner1;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Timer timer;
    }
}