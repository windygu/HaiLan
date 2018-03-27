namespace HLAChannelMachine.DialogForm
{
    partial class Customsetting
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
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1_timelog = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(654, 456);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(199, 41);
            this.button2.TabIndex = 4;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1_timelog
            // 
            this.checkBox1_timelog.AutoSize = true;
            this.checkBox1_timelog.Location = new System.Drawing.Point(165, 87);
            this.checkBox1_timelog.Name = "checkBox1_timelog";
            this.checkBox1_timelog.Size = new System.Drawing.Size(108, 24);
            this.checkBox1_timelog.TabIndex = 5;
            this.checkBox1_timelog.Text = "记录耗时";
            this.checkBox1_timelog.UseVisualStyleBackColor = true;
            this.checkBox1_timelog.CheckedChanged += new System.EventHandler(this.checkBox1_timelog_CheckedChanged);
            // 
            // Customsetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 509);
            this.ControlBox = false;
            this.Controls.Add(this.checkBox1_timelog);
            this.Controls.Add(this.button2);
            this.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Customsetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1_timelog;
    }
}