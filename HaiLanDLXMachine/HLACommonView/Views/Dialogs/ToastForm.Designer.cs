namespace HLACommonView.Views.Dialogs
{
    partial class ToastForm
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
            this.metroProgressSpinner1 = new DMSkin.Metro.Controls.MetroProgressSpinner();
            this.lblText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroProgressSpinner1.DM_Maximum = 100;
            this.metroProgressSpinner1.DM_UseCustomBackColor = true;
            this.metroProgressSpinner1.DM_UseSelectable = true;
            this.metroProgressSpinner1.DM_Value = 100;
            this.metroProgressSpinner1.Location = new System.Drawing.Point(31, 31);
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(175, 175);
            this.metroProgressSpinner1.Style = DMSkin.Metro.MetroColorStyle.White;
            this.metroProgressSpinner1.TabIndex = 0;
            // 
            // lblText
            // 
            this.lblText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblText.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.lblText.ForeColor = System.Drawing.Color.White;
            this.lblText.Location = new System.Drawing.Point(206, 31);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(350, 175);
            this.lblText.TabIndex = 1;
            this.lblText.Text = "Loading";
            this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ToastForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.ClientSize = new System.Drawing.Size(587, 236);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.metroProgressSpinner1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ToastForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ToastForm_FormClosing);
            this.Load += new System.EventHandler(this.ToastForm_Load);
            this.Shown += new System.EventHandler(this.ToastForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private DMSkin.Metro.Controls.MetroProgressSpinner metroProgressSpinner1;
        private System.Windows.Forms.Label lblText;
    }
}