using DMSkin;
using HLACommonLib;
using HLACommonView.Views;
using HLAPackingBoxChannelMachine.DialogForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HLAPackingBoxChannelMachine
{
    public partial class LoginForm : CommonLoginForm
    {
        public LoginForm()
        {
            InitializeComponent();
            Title = "RFID整理库智能装箱系统";
        }

        public override void Login()
        {
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
                InventoryFormNew form = new InventoryFormNew();
                form.ShowDialog();
            }
            else
            {
                MetroMessageBox.Show(this,"用户名或密码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            base.Login();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

            Thread thread = new Thread(new ThreadStart(() => {
                ShowLoading("正在从SAP下载设备信息...");
                SysConfig.DeviceInfo = SAPDataService.GetHLANo(SysConfig.LGNUM,SysConfig.DeviceNO,"01F");
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
        }

        private void dmButton1_Click(object sender, EventArgs e)
        {
            ShadeForm form = new ShadeForm(this);
            form.Show();
        }
    }
}
