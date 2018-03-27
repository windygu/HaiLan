using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLAChannelMachine.Utils;
using System.Xml;
using HLACommonLib;
using System.Diagnostics;
using System.Runtime.InteropServices;
using HLACommonLib.Model;

namespace HLAChannelMachine
{
    public partial class SettingForm : Form
    {

        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            //this.cboMaxCircleTimes.Text = SysConfig.MaxCircleTimes.ToString();
            this.txtRssiLimit.Text = SysConfig.RssiLimit.ToString();
            this.txtPower.Text = SysConfig.ReaderConfig.AntennaPower1.ToString();
            this.txtDelayTime.Text = SysConfig.DelayTime.ToString();
            this.txtDeviceNO.Text = SysConfig.DeviceNO.ToString();
            this.cboModel.SelectedIndex = (int)SysConfig.RunningModel - 1;
            this.txtIp.Text = SysConfig.DBUrl;
            this.txtVersion.Text = SysConfig.Version;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //int maxCircleTimes = int.Parse(this.cboMaxCircleTimes.Text.Trim());
            double rssiLimit = double.Parse(this.txtRssiLimit.Text.Trim());
            double antennaPower = double.Parse(this.txtPower.Text.Trim());
            int delayTime = int.Parse(this.txtDelayTime.Text.Trim());
            string deviceNO = this.txtDeviceNO.Text.Trim();

            //注释修改设备编码功能，防止在设置界面被修改，海澜之家的要求 
            //SysConfig.DeviceInfo = SAPDataService.GetHLANo(SysConfig.LGNUM, deviceNO);
            //if (SysConfig.DeviceInfo == null)
            //{
            //    //如果获取楼层时异常，直接弹出配置界面
            //    MessageBox.Show("设备编码有误，请重新配置", "警告");
            //    this.txtDeviceNO.Focus();
            //    this.txtDeviceNO.SelectAll();
            //    return;
            //}
            //SysConfig.DeviceNO = deviceNO;
            //SysConfig.Floor = SysConfig.DeviceInfo.LOUCENG;//楼层号
            //SysConfig.sEQUIP_HLA = SysConfig.DeviceInfo.EQUIP_HLA;//设备终端号

            RunMode runningModel = (RunMode)Enum.Parse(typeof(RunMode), (cboModel.SelectedIndex + 1).ToString());
             
            SysConfig.RunningModel = runningModel;
            //SysConfig.MaxCircleTimes = maxCircleTimes;  //平库 杏林通道机used 最大转圈次数
            SysConfig.RssiLimit = rssiLimit;
            SysConfig.ReaderConfig.AntennaPower1 = antennaPower;
            SysConfig.ReaderConfig.AntennaPower2 = antennaPower;
            SysConfig.ReaderConfig.AntennaPower3 = antennaPower;
            SysConfig.ReaderConfig.AntennaPower4 = antennaPower;
            SysConfig.ReaderConfig.AntennaPower5 = antennaPower;
            SysConfig.ReaderConfig.AntennaPower6 = antennaPower;
            SysConfig.DelayTime = delayTime;

            //SetConfigValue("CircleTimes", maxCircleTimes.ToString());
            //SetConfigValue("RecheckTimes", maxCircleTimes.ToString());
            SetConfigValue("RssiLimit", rssiLimit.ToString());
            SetConfigValue("AntennaPower1", antennaPower.ToString());
            SetConfigValue("AntennaPower2", antennaPower.ToString());
            SetConfigValue("AntennaPower3", antennaPower.ToString());
            SetConfigValue("AntennaPower4", antennaPower.ToString());
            SetConfigValue("AntennaPower5", antennaPower.ToString());
            SetConfigValue("AntennaPower6", antennaPower.ToString());
            SetConfigValue("DelayTime", delayTime.ToString());
            SetConfigValue("RunMode", ((int)runningModel).ToString());
            SetConfigValue("DeviceNO", deviceNO);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 设置配置文件的值
        /// </summary>
        /// <param name="AppKey"></param>
        /// <param name="AppValue"></param>
        public static void SetConfigValue(string AppKey, string AppValue)
        {
            XmlDocument xDoc = new XmlDocument();
            //获取可执行文件的路径和名称
            xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");

            XmlNode xNode;
            XmlElement xElem1;
            xNode = xDoc.SelectSingleNode("//appSettings");
            xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + AppKey + "']");
            if (xElem1 != null) xElem1.SetAttribute("value", AppValue);
            else
            {
                xElem1 = xDoc.CreateElement("add");
                xElem1.SetAttribute("key", AppKey);
                xElem1.SetAttribute("value", AppValue);
                xNode.AppendChild(xElem1);
            }

            xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TabTip.exe");
        }

        //private void txtFloor_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyValue >= 48 && e.KeyValue <= 57)
        //    { }
        //    else
        //    {
        //    }
        //}

        //private void txtFloor_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar >= 48 && e.KeyChar <= 57)
        //    {
        //        if (txtFloor.Text.Length >= 3)
        //            e.Handled = true;
        //        else
        //            e.Handled = false;                
        //    }
        //    else
        //    {
        //        if (e.KeyChar == 8)
        //            e.Handled = false;
        //        else
        //            e.Handled = true;
        //    }
        //}



    }
}
