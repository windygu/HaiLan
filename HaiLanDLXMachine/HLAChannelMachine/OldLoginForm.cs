using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMSkin;
using HLAChannelMachine.Utils;
using HLACommonLib;
using HLACommonLib.Model;

namespace HLAChannelMachine
{
    public partial class OldLoginForm : Form
    {
        public OldLoginForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtUserNo.Text = "";
            this.txtPassword.Text = "";
        }
        private void Login()
        {
            if(string.IsNullOrWhiteSpace(txtUserNo.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("工号或密码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //判断当前使用的设备编码能否正常获取楼层信息
            if (string.IsNullOrEmpty(SysConfig.DeviceNO))
            {
                MessageBox.Show("未配置设备编码，请先配置设备编码", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //判断错误箱码前缀是否存在
            if (LocalDataService.IsErrorHuConfigExists(SysConfig.DeviceNO) == false)
            {
                MessageBox.Show("错误箱码前缀不存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SysConfig.DeviceInfo = SAPDataService.GetHLANo(SysConfig.LGNUM, SysConfig.DeviceNO, "01A");
            if (SysConfig.DeviceInfo == null)
            {
                //如果获取楼层时异常，直接弹出配置界面
                MessageBox.Show("SAP下发楼层信息失败，请重新配置设备编码", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                //SysConfig.Floor = SysConfig.DeviceInfo.LOUCENG;//楼层号
                //SysConfig.sEQUIP_HLA = SysConfig.DeviceInfo.EQUIP_HLA;//设备终端号 
            }

            if (SysConfig.DeviceInfo.GxList == null || SysConfig.DeviceInfo.GxList.Count <= 0)
            {
                //工序判断
                MessageBox.Show("工序未设置，请联系SAP工作人员！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string userNo = this.txtUserNo.Text.Trim();
            string password = this.txtPassword.Text;
            if (SAPDataService.Login(userNo, password))
            {
                //缓存当前登录用户
                SysConfig.CurrentLoginUser = new HLACommonLib.Model.UserInfo() { UserId = userNo, Password = password };
                InventoryForm form = new InventoryForm();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("用户名或密码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            SettingForm form = new SettingForm();
            form.ShowDialog();
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TabTip.exe");
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                Login();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Login();
            }
        }
    }
}
