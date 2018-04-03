namespace HLABigData
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button2_updateVs = new System.Windows.Forms.Button();
            this.textBox1_versions = new System.Windows.Forms.TextBox();
            this.checkBox2_ajt = new System.Windows.Forms.CheckBox();
            this.checkBox1_hl = new System.Windows.Forms.CheckBox();
            this.textBox2_downUrl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(849, 458);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button2_updateVs);
            this.tabPage1.Controls.Add(this.textBox1_versions);
            this.tabPage1.Controls.Add(this.checkBox2_ajt);
            this.tabPage1.Controls.Add(this.checkBox1_hl);
            this.tabPage1.Controls.Add(this.textBox2_downUrl);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(841, 432);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "更新版本";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button2_updateVs
            // 
            this.button2_updateVs.BackColor = System.Drawing.Color.PeachPuff;
            this.button2_updateVs.Location = new System.Drawing.Point(304, 351);
            this.button2_updateVs.Name = "button2_updateVs";
            this.button2_updateVs.Size = new System.Drawing.Size(239, 34);
            this.button2_updateVs.TabIndex = 12;
            this.button2_updateVs.Text = "更新";
            this.button2_updateVs.UseVisualStyleBackColor = false;
            this.button2_updateVs.Click += new System.EventHandler(this.button2_updateVs_Click);
            // 
            // textBox1_versions
            // 
            this.textBox1_versions.BackColor = System.Drawing.Color.PeachPuff;
            this.textBox1_versions.Location = new System.Drawing.Point(304, 130);
            this.textBox1_versions.Multiline = true;
            this.textBox1_versions.Name = "textBox1_versions";
            this.textBox1_versions.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1_versions.Size = new System.Drawing.Size(239, 191);
            this.textBox1_versions.TabIndex = 11;
            // 
            // checkBox2_ajt
            // 
            this.checkBox2_ajt.AutoSize = true;
            this.checkBox2_ajt.Checked = true;
            this.checkBox2_ajt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2_ajt.Location = new System.Drawing.Point(455, 22);
            this.checkBox2_ajt.Name = "checkBox2_ajt";
            this.checkBox2_ajt.Size = new System.Drawing.Size(42, 16);
            this.checkBox2_ajt.TabIndex = 10;
            this.checkBox2_ajt.Text = "ajt";
            this.checkBox2_ajt.UseVisualStyleBackColor = true;
            // 
            // checkBox1_hl
            // 
            this.checkBox1_hl.AutoSize = true;
            this.checkBox1_hl.Checked = true;
            this.checkBox1_hl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1_hl.Location = new System.Drawing.Point(303, 22);
            this.checkBox1_hl.Name = "checkBox1_hl";
            this.checkBox1_hl.Size = new System.Drawing.Size(72, 16);
            this.checkBox1_hl.TabIndex = 9;
            this.checkBox1_hl.Text = "heilandb";
            this.checkBox1_hl.UseVisualStyleBackColor = true;
            // 
            // textBox2_downUrl
            // 
            this.textBox2_downUrl.Location = new System.Drawing.Point(304, 71);
            this.textBox2_downUrl.Name = "textBox2_downUrl";
            this.textBox2_downUrl.Size = new System.Drawing.Size(239, 21);
            this.textBox2_downUrl.TabIndex = 3;
            this.textBox2_downUrl.Text = "http://172.18.207.92/Update/";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "DownloadUrl";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(841, 432);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 458);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "通道机数据统计";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox checkBox2_ajt;
        private System.Windows.Forms.CheckBox checkBox1_hl;
        private System.Windows.Forms.TextBox textBox2_downUrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1_versions;
        private System.Windows.Forms.Button button2_updateVs;
    }
}

