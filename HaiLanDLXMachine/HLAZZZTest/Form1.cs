using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonView.Views;
using HLACommonView.Model;
using System.Threading;
using HLACommonLib;
using System.Configuration;

namespace HLAZZZTest
{
    public partial class Form1 : CommonInventoryForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConfig();

            SAPDataService.Init();

            SAPDataService.GetMaterialInfoList(SysConfig.LGNUM);
        }

        public void LoadConfig()
        {
            SysConfig.ReaderIp = ConfigurationManager.AppSettings["ReaderIp"];
            SysConfig.DelayTime = ConfigurationManager.AppSettings["DelayTime"] == null ? 700 : int.Parse(ConfigurationManager.AppSettings["DelayTime"]);

            SysConfig.ReaderConfig.SearchMode = int.Parse(ConfigurationManager.AppSettings["SearchMode"]);
            SysConfig.ReaderConfig.Session = ushort.Parse(ConfigurationManager.AppSettings["Session"]);

            SysConfig.ReaderConfig.UseAntenna1 = int.Parse(ConfigurationManager.AppSettings["UseAntenna1"]) == 0 ? false : true;
            SysConfig.ReaderConfig.UseAntenna2 = int.Parse(ConfigurationManager.AppSettings["UseAntenna2"]) == 0 ? false : true;
            SysConfig.ReaderConfig.UseAntenna3 = int.Parse(ConfigurationManager.AppSettings["UseAntenna3"]) == 0 ? false : true;
            SysConfig.ReaderConfig.UseAntenna4 = int.Parse(ConfigurationManager.AppSettings["UseAntenna4"]) == 0 ? false : true;
            SysConfig.ReaderConfig.UseAntenna5 = int.Parse(ConfigurationManager.AppSettings["UseAntenna5"]) == 0 ? false : true;
            SysConfig.ReaderConfig.UseAntenna6 = int.Parse(ConfigurationManager.AppSettings["UseAntenna6"]) == 0 ? false : true;
            SysConfig.ReaderConfig.UseAntenna7 = int.Parse(ConfigurationManager.AppSettings["UseAntenna7"]) == 0 ? false : true;
            SysConfig.ReaderConfig.UseAntenna8 = int.Parse(ConfigurationManager.AppSettings["UseAntenna8"]) == 0 ? false : true;

            SysConfig.ReaderConfig.AntennaPower1 = double.Parse(ConfigurationManager.AppSettings["AntennaPower1"]);
            SysConfig.ReaderConfig.AntennaPower2 = double.Parse(ConfigurationManager.AppSettings["AntennaPower2"]);
            SysConfig.ReaderConfig.AntennaPower3 = double.Parse(ConfigurationManager.AppSettings["AntennaPower3"]);
            SysConfig.ReaderConfig.AntennaPower4 = double.Parse(ConfigurationManager.AppSettings["AntennaPower4"]);
            SysConfig.ReaderConfig.AntennaPower5 = double.Parse(ConfigurationManager.AppSettings["AntennaPower5"]);
            SysConfig.ReaderConfig.AntennaPower6 = double.Parse(ConfigurationManager.AppSettings["AntennaPower6"]);
            SysConfig.ReaderConfig.AntennaPower7 = double.Parse(ConfigurationManager.AppSettings["AntennaPower7"]);
            SysConfig.ReaderConfig.AntennaPower8 = double.Parse(ConfigurationManager.AppSettings["AntennaPower8"]);

            //SAP相关配置
            SysConfig.AppServerHost = ConfigurationManager.AppSettings["AppServerHost"];
            SysConfig.SystemNumber = ConfigurationManager.AppSettings["SystemNumber"];
            SysConfig.User = ConfigurationManager.AppSettings["User"];
            SysConfig.Password = ConfigurationManager.AppSettings["Password"];
            SysConfig.Client = ConfigurationManager.AppSettings["Client"];
            SysConfig.Language = ConfigurationManager.AppSettings["Language"];
            SysConfig.PoolSize = ConfigurationManager.AppSettings["PoolSize"];
            SysConfig.PeakConnectionsLimit = ConfigurationManager.AppSettings["PeakConnectionsLimit"];
            SysConfig.IdleTimeout = ConfigurationManager.AppSettings["IdleTimeout"];

            //通道机硬件设备相关配置
            SysConfig.Port = ConfigurationManager.AppSettings["Port"];
            SysConfig.ScannerPort_1 = ConfigurationManager.AppSettings["ScannerPort_1"];
            SysConfig.ScannerPort_2 = ConfigurationManager.AppSettings["ScannerPort_2"];

        }

    }
}
