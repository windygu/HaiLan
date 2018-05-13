using DMSkin;
using HLACommonLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLATools
{
    public partial class ShortConfirmForm : Main
    {
        public ShortConfirmForm()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text.Trim()) || string.IsNullOrEmpty(txtPwd.Text.Trim()))
            {
                MetroMessageBox.Show(this, "工号或密码不能为空", "警告");
                return;
            }
            string userNo = this.txtUser.Text.Trim();
            string password = this.txtPwd.Text;
            if (SAPDataService.OutConfirm(userNo, password))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MetroMessageBox.Show(this, "工号或密码错误！", "提示");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
