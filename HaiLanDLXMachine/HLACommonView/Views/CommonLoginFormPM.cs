using DMSkin;
using HLACommonLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HLACommonView.Views
{
    public partial class CommonLoginFormPM : Main
    {
        public string Title = "海澜之家智能供应链管理系统";
        public string User
        {
            get
            {
                return txtUser.Text.Trim();
            }
        }

        public string Password
        {
            get
            {
                return txtPwd.Text.Trim();
            }
        }

        public CommonLoginFormPM()
        {
            InitializeComponent();
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //回车
                Login();
            }
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //回车
                Login();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath+"\\Tools\\TabTip.exe");
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            lblTitle.Text = Title;
        }

        public virtual void Login()
        {
            
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            Config();
        }

        public virtual void Config()
        {
            ConfigFormPM cf = new ConfigFormPM();
            cf.ShowDialog();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        public void ShowLoading(string msg)
        {
            Invoke(new Action(() => {
                panelLoading.Show();
                lblStatus.Text = msg;
            }));
        }

        public void HideLoading()
        {
            Invoke(new Action(() => {
                panelLoading.Hide();
                lblStatus.Text = "loading...";
            }));
        }

        public void DisableLoginButton()
        {
            Invoke(new Action(() => {
                btnLogin.Enabled = false;
            }));
        }

        public void EnableLoginButton()
        {
            Invoke(new Action(() => {
                btnLogin.Enabled = true;
            }));
        }

        public void ShowRestartButton(string msg)
        {
            Invoke(new Action(() => {
                btnRestart.Text = string.Format("{0}，请维护后点击重启", msg);
                btnRestart.Show();
            }));
        }

        public void LoadLouceng()
        {
            Invoke(new Action(() => 
            {
                //if (SysConfig.DeviceInfo.AuthList != null)
                //{
                //    cboLouceng.DataSource = SysConfig.DeviceInfo.AuthList.FindAll(i => i.AUTH_CODE.StartsWith("F"));
                //    cboLouceng.DisplayMember = "AUTH_VALUE";
                //    cboLouceng.ValueMember = "AUTH_VALUE";
                //    cboLouceng.Text = SysConfig.DeviceInfo?.LOUCENG;
                //}
                if(SysConfig.DeviceInfo!=null)
                {
                    cboLouceng.Items.Add(SysConfig.DeviceInfo.LOUCENG);
                }
                if(SysConfig.DeviceInfo != null && SysConfig.DeviceInfo.AuthList!=null)
                {
                    foreach(HLACommonLib.Model.AuthInfo ainfo in SysConfig.DeviceInfo.AuthList)
                    {
                        if(ainfo.AUTH_CODE.StartsWith("F"))
                        {
                            if(!cboLouceng.Items.Contains(ainfo.AUTH_VALUE))
                                cboLouceng.Items.Add(ainfo.AUTH_VALUE);
                        }
                    }
                }

                cboLouceng.Text = SysConfig.DeviceInfo?.LOUCENG;
            }));
        }
    }
}
