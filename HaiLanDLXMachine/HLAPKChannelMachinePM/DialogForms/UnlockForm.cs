using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMSkin;
using HLACommonLib;

namespace HLADeliverChannelMachine.DialogForms
{
    public partial class UnlockForm : Main
    {
        public UnlockForm()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ConfirmPassword();
        }

        private void ConfirmPassword()
        {
            if (string.IsNullOrEmpty(txtPwd.Text.Trim()))
            {
                MetroMessageBox.Show(this, "密码不能为空", "警告");
                return;
            }
            string password = this.txtPwd.Text;
            if (password == "89757")
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MetroMessageBox.Show(this, "密码错误！", "提示");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TabTip.exe");
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                ConfirmPassword();
        }

    }
}
