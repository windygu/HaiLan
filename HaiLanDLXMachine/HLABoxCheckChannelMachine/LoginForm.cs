using DMSkin;
using HLACommonLib;
using HLACommonLib.DAO;
using HLACommonLib.Model;
using HLACommonLib.Model.YK;
using HLACommonView.Model;
using HLACommonView.Views;
using HLACommonView.Configs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Xindeco.Device.Model;
using System.Xml;
using OSharp.Utility.Extensions;
using Newtonsoft.Json;
using System.Configuration;

namespace HLABoxCheckChannelMachine
{
    public partial class LoginForm : CommonLoginForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        public override void Login()
        {
            InventoryForm form = new InventoryForm();
            form.ShowDialog();

            /*
            if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
            {
                MetroMessageBox.Show(this, "工号密码不能为空", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(cboLouceng.Text.Trim()))
            {
                MetroMessageBox.Show(this, "楼层不能为空", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (SAPDataService.Login(User, Password))
            {
                //缓存当前登录用户
                SysConfig.CurrentLoginUser = new HLACommonLib.Model.UserInfo() { UserId = User, Password = Password };
                SysConfig.DeviceInfo.LOUCENG = cboLouceng.Text;

                InventoryForm form = new InventoryForm();
                form.ShowDialog();
            }
            else
            {
                MetroMessageBox.Show(this, "用户名或密码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            base.Login();
            */
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            EnableLoginButton();
            /*
            Thread thread = new Thread(new ThreadStart(() => {
                ShowLoading("正在从SAP下载设备信息...");
                SysConfig.DeviceInfo = SAPDataService.GetHLANo(SysConfig.LGNUM, SysConfig.DeviceNO, "01F");
                if (SysConfig.DeviceInfo == null)
                    ShowRestartButton("SAP未维护设备信息");
                else if (SysConfig.DeviceInfo.AuthList == null || SysConfig.DeviceInfo.AuthList.Count == 0)
                    ShowRestartButton("SAP未维护权限信息");
                else
                {
                    LoadLouceng();
                    EnableLoginButton();
                }
                HideLoading();
            }));
            thread.IsBackground = true;
            thread.Start();
            */
        }
    }
}
