using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonView.Views;
using DMSkin;
using HLACommonLib;

namespace HLATools
{
    public partial class LoginForm : CommonLoginFormPM
    {
        public LoginForm()
        {
            Title = "海澜之家智能单检设备";
            InitializeComponent();
        }

        public override void Login()
        {
            if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
            {
                MetroMessageBox.Show(this, "工号或密码不能为空", "警告",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (SAPDataService.Login(User, Password))
            {
                //缓存当前登录用户
                SysConfig.CurrentLoginUser = new HLACommonLib.Model.UserInfo() { UserId = User, Password = Password };
                this.Invoke(new Action(() =>
                {
                    MainForm form = new MainForm();
                    form.ShowDialog();
                }));
            }
            else
            {
                MetroMessageBox.Show(this, "工号或密码错误！", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            EnableLoginButton();
        }
    }
}
