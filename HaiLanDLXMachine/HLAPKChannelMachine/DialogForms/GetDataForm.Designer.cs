namespace HLAPKChannelMachine.DialogForms
{
    partial class GetDataForm
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
            this.shipProgressBar = new DMSkin.Metro.Controls.MetroProgressBar();
            this.shipButton = new System.Windows.Forms.Button();
            this.shipDateTime = new DMSkin.Metro.Controls.MetroDateTime();
            this.label9 = new System.Windows.Forms.Label();
            this.shipLogLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.inventoryLogLabel = new System.Windows.Forms.Label();
            this.inventoryStoreTextBox = new DMSkin.Metro.Controls.MetroTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.inventoryProgressBar = new DMSkin.Metro.Controls.MetroProgressBar();
            this.inventoryButton = new System.Windows.Forms.Button();
            this.inventoryDateTime = new DMSkin.Metro.Controls.MetroDateTime();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // shipProgressBar
            // 
            this.shipProgressBar.Location = new System.Drawing.Point(128, 159);
            this.shipProgressBar.Name = "shipProgressBar";
            this.shipProgressBar.Size = new System.Drawing.Size(576, 30);
            this.shipProgressBar.TabIndex = 64;
            // 
            // shipButton
            // 
            this.shipButton.BackColor = System.Drawing.Color.Teal;
            this.shipButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shipButton.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.shipButton.ForeColor = System.Drawing.Color.White;
            this.shipButton.Location = new System.Drawing.Point(596, 112);
            this.shipButton.Name = "shipButton";
            this.shipButton.Size = new System.Drawing.Size(108, 40);
            this.shipButton.TabIndex = 63;
            this.shipButton.Text = "开始下载";
            this.shipButton.UseVisualStyleBackColor = false;
            this.shipButton.Click += new System.EventHandler(this.shipButton_Click);
            // 
            // shipDateTime
            // 
            this.shipDateTime.Location = new System.Drawing.Point(232, 118);
            this.shipDateTime.MinimumSize = new System.Drawing.Size(0, 30);
            this.shipDateTime.Name = "shipDateTime";
            this.shipDateTime.Size = new System.Drawing.Size(132, 30);
            this.shipDateTime.TabIndex = 62;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label9.Location = new System.Drawing.Point(122, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 21);
            this.label9.TabIndex = 61;
            this.label9.Text = "发运日期：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // shipLogLabel
            // 
            this.shipLogLabel.BackColor = System.Drawing.Color.White;
            this.shipLogLabel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.shipLogLabel.Location = new System.Drawing.Point(128, 215);
            this.shipLogLabel.Name = "shipLogLabel";
            this.shipLogLabel.Size = new System.Drawing.Size(576, 21);
            this.shipLogLabel.TabIndex = 65;
            this.shipLogLabel.Text = "发运日志";
            this.shipLogLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.Location = new System.Drawing.Point(58, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 21);
            this.label1.TabIndex = 66;
            this.label1.Text = "货运数据：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // inventoryLogLabel
            // 
            this.inventoryLogLabel.BackColor = System.Drawing.Color.White;
            this.inventoryLogLabel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.inventoryLogLabel.Location = new System.Drawing.Point(122, 429);
            this.inventoryLogLabel.Name = "inventoryLogLabel";
            this.inventoryLogLabel.Size = new System.Drawing.Size(582, 21);
            this.inventoryLogLabel.TabIndex = 73;
            this.inventoryLogLabel.Text = "下架单日志";
            this.inventoryLogLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.inventoryLogLabel.Visible = false;
            // 
            // inventoryStoreTextBox
            // 
            this.inventoryStoreTextBox.DM_FontSize = DMSkin.Metro.MetroTextBoxSize.Medium;
            this.inventoryStoreTextBox.DM_UseSelectable = true;
            this.inventoryStoreTextBox.Lines = new string[0];
            this.inventoryStoreTextBox.Location = new System.Drawing.Point(382, 339);
            this.inventoryStoreTextBox.MaxLength = 32767;
            this.inventoryStoreTextBox.Name = "inventoryStoreTextBox";
            this.inventoryStoreTextBox.PasswordChar = '\0';
            this.inventoryStoreTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.inventoryStoreTextBox.SelectedText = "";
            this.inventoryStoreTextBox.Size = new System.Drawing.Size(92, 30);
            this.inventoryStoreTextBox.TabIndex = 72;
            this.inventoryStoreTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.inventoryStoreTextBox.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label3.Location = new System.Drawing.Point(279, 343);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 21);
            this.label3.TabIndex = 71;
            this.label3.Text = "存储类型：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Visible = false;
            // 
            // inventoryProgressBar
            // 
            this.inventoryProgressBar.Location = new System.Drawing.Point(126, 381);
            this.inventoryProgressBar.Name = "inventoryProgressBar";
            this.inventoryProgressBar.Size = new System.Drawing.Size(578, 30);
            this.inventoryProgressBar.TabIndex = 70;
            this.inventoryProgressBar.Visible = false;
            // 
            // inventoryButton
            // 
            this.inventoryButton.BackColor = System.Drawing.Color.Teal;
            this.inventoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inventoryButton.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.inventoryButton.ForeColor = System.Drawing.Color.White;
            this.inventoryButton.Location = new System.Drawing.Point(596, 335);
            this.inventoryButton.Name = "inventoryButton";
            this.inventoryButton.Size = new System.Drawing.Size(108, 40);
            this.inventoryButton.TabIndex = 69;
            this.inventoryButton.Text = "开始下载";
            this.inventoryButton.UseVisualStyleBackColor = false;
            this.inventoryButton.Visible = false;
            this.inventoryButton.Click += new System.EventHandler(this.inventoryButton_Click);
            // 
            // inventoryDateTime
            // 
            this.inventoryDateTime.Location = new System.Drawing.Point(126, 339);
            this.inventoryDateTime.MinimumSize = new System.Drawing.Size(0, 30);
            this.inventoryDateTime.Name = "inventoryDateTime";
            this.inventoryDateTime.Size = new System.Drawing.Size(132, 30);
            this.inventoryDateTime.TabIndex = 68;
            this.inventoryDateTime.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(-137, 393);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 21);
            this.label2.TabIndex = 67;
            this.label2.Text = "发运日期：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label4.Location = new System.Drawing.Point(58, 301);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 21);
            this.label4.TabIndex = 74;
            this.label4.Text = "下架单数据：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Teal;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(446, 453);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(258, 40);
            this.button1.TabIndex = 75;
            this.button1.Text = "退出";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GetDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 516);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.inventoryLogLabel);
            this.Controls.Add(this.inventoryStoreTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.inventoryProgressBar);
            this.Controls.Add(this.inventoryButton);
            this.Controls.Add(this.inventoryDateTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shipLogLabel);
            this.Controls.Add(this.shipProgressBar);
            this.Controls.Add(this.shipButton);
            this.Controls.Add(this.shipDateTime);
            this.Controls.Add(this.label9);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GetDataForm";
            this.Text = "获取数据";
            this.ResumeLayout(false);

        }

        #endregion

        private DMSkin.Metro.Controls.MetroProgressBar shipProgressBar;
        private System.Windows.Forms.Button shipButton;
        private DMSkin.Metro.Controls.MetroDateTime shipDateTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label shipLogLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label inventoryLogLabel;
        private DMSkin.Metro.Controls.MetroTextBox inventoryStoreTextBox;
        private System.Windows.Forms.Label label3;
        private DMSkin.Metro.Controls.MetroProgressBar inventoryProgressBar;
        private System.Windows.Forms.Button inventoryButton;
        private DMSkin.Metro.Controls.MetroDateTime inventoryDateTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}