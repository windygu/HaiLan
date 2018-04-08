namespace HLADeliverChannelMachine.DialogForms
{
    partial class PrintForm
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
            this.btnCancel = new DMSkin.Metro.Controls.MetroButton();
            this.btnOk = new DMSkin.Metro.Controls.MetroButton();
            this.cbShippingBox = new DMSkin.Metro.Controls.MetroCheckBox();
            this.cbMixShippingBox = new DMSkin.Metro.Controls.MetroCheckBox();
            this.SuspendLayout();
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
            this.btnCancel.Location = new System.Drawing.Point(375, 223);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(115, 58);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "取消";
            this.btnCancel.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnOk.DM_FontSize = DMSkin.Metro.MetroButtonSize.Tall;
            this.btnOk.DM_UseCustomBackColor = true;
            this.btnOk.DM_UseCustomForeColor = true;
            this.btnOk.DM_UseSelectable = true;
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(136, 223);
            this.btnOk.Margin = new System.Windows.Forms.Padding(0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(115, 58);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "打印";
            this.btnOk.Theme = DMSkin.Metro.MetroThemeStyle.Light;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cbShippingBox
            // 
            this.cbShippingBox.AutoSize = true;
            this.cbShippingBox.Checked = true;
            this.cbShippingBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShippingBox.DM_FontSize = DMSkin.Metro.MetroCheckBoxSize.Tall;
            this.cbShippingBox.DM_FontWeight = DMSkin.Metro.MetroCheckBoxWeight.Bold;
            this.cbShippingBox.DM_UseSelectable = true;
            this.cbShippingBox.Location = new System.Drawing.Point(138, 116);
            this.cbShippingBox.Name = "cbShippingBox";
            this.cbShippingBox.Size = new System.Drawing.Size(98, 24);
            this.cbShippingBox.TabIndex = 7;
            this.cbShippingBox.Text = "发运标签";
            // 
            // cbMixShippingBox
            // 
            this.cbMixShippingBox.AutoSize = true;
            this.cbMixShippingBox.Checked = true;
            this.cbMixShippingBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMixShippingBox.DM_FontSize = DMSkin.Metro.MetroCheckBoxSize.Tall;
            this.cbMixShippingBox.DM_FontWeight = DMSkin.Metro.MetroCheckBoxWeight.Bold;
            this.cbMixShippingBox.DM_UseSelectable = true;
            this.cbMixShippingBox.Location = new System.Drawing.Point(316, 116);
            this.cbMixShippingBox.Name = "cbMixShippingBox";
            this.cbMixShippingBox.Size = new System.Drawing.Size(142, 24);
            this.cbMixShippingBox.TabIndex = 8;
            this.cbMixShippingBox.Text = "开箱/拼箱标签";
            // 
            // PrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 345);
            this.Controls.Add(this.cbMixShippingBox);
            this.Controls.Add(this.cbShippingBox);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "PrintForm";
            this.Text = "标签打印";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DMSkin.Metro.Controls.MetroButton btnCancel;
        private DMSkin.Metro.Controls.MetroButton btnOk;
        private DMSkin.Metro.Controls.MetroCheckBox cbShippingBox;
        private DMSkin.Metro.Controls.MetroCheckBox cbMixShippingBox;
    }
}