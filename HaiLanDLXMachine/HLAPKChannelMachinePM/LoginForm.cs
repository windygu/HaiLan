using DMSkin;
using HLACommonLib;
using HLACommonView.Views;
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

namespace HLADeliverChannelMachine
{
    public partial class LoginForm : CommonLoginFormPM
    {
        private List<string> mLGTYP;
        public LoginForm()
        {
            InitializeComponent();
            Title = "RFID供应链智能发货系统";
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
                this.Invoke(new Action(() =>
                {
                    InventoryOutLogForm form = new InventoryOutLogForm(mLGTYP);
                    form.ShowDialog();
                }));
            }
            else
            {
                MetroMessageBox.Show(this, "用户名或密码错误！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(() => {
                ShowLoading("正在从SAP下载设备信息...");

                if (string.IsNullOrEmpty(SysConfig.DeviceNO))
                {
                    ShowRestartButton("未配置设备编码");
                    HideLoading();
                    return;
                }

                SysConfig.DeviceInfo = SAPDataService.GetHLANo(SysConfig.LGNUM, SysConfig.DeviceNO, "03B");
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
                //MessageBox.Show(JsonConvert.SerializeObject(SysConfig.DeviceInfo.AuthList));
                SysConfig.DeviceInfo.LGTYP = SysConfig.DeviceInfo.AuthList
                .Where(w => w.AUTH_CODE.StartsWith("C"))
                .Select(s => s.AUTH_VALUE)
                .Distinct()
                .ToList();

                if (SysConfig.DeviceInfo.LGTYP == null || SysConfig.DeviceInfo.LGTYP.Count <= 0)
                {
                    ShowRestartButton("存储类型未维护");
                    HideLoading();
                    return;
                }

                mLGTYP = SysConfig.DeviceInfo.LGTYP.ToList();
                //默认都不选择
                SysConfig.DeviceInfo.LGTYP.Clear();

                LoadLouceng();

                EnableLoginButton();
                HideLoading();
            }));
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
