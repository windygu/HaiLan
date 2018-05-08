using DMSkin;
using HLACommonLib;
using HLACommonView.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HLAChannelMachine
{
    public partial class LoginForm : CommonLoginFormPM
    {
        public LoginForm()
        {
            InitializeComponent();
            Title = "RFID供应链智能收货系统";
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
                SysConfig.DeviceInfo.LOUCENG = cboLouceng.Text.Trim();

                if (string.IsNullOrEmpty(SysConfig.Floor))
                {
                    MetroMessageBox.Show(this, "楼层不能为空！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(SysConfig.sEQUIP_HLA))
                {
                    MetroMessageBox.Show(this, "设备号不能为空！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                InventoryFormNew form = new InventoryFormNew();
                form.ShowDialog();
            }
            else
            {
                MetroMessageBox.Show(this, "用户名或密码错误！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void LoginForm_Load(object sender, System.EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                ShowLoading("正在从SAP下载设备信息...");

                if (string.IsNullOrEmpty(SysConfig.DeviceNO))
                {
                    ShowRestartButton("未配置设备编码");
                    HideLoading();
                    return;
                }

                SysConfig.DeviceInfo = SAPDataService.GetHLANo(SysConfig.LGNUM, SysConfig.DeviceNO, "03A");
                if (SysConfig.DeviceInfo == null)
                {
                    //如果获取楼层时异常，直接弹出配置界面
                    ShowRestartButton("SAP未维护设备信息");
                    HideLoading();
                    return;
                }

                if (SysConfig.DeviceInfo.GxList == null || SysConfig.DeviceInfo.GxList.Count <= 0)
                {
                    //工序判断
                    ShowRestartButton("工序未设置");
                    HideLoading();
                    return;
                }

                if (LocalDataService.IsErrorHuConfigExists(SysConfig.DeviceNO) == false)
                {
                    ShowRestartButton("错误箱码前缀不存在");
                    HideLoading();
                    return;
                }

                LoadLouceng();

                EnableLoginButton();
                HideLoading();
            }));
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
