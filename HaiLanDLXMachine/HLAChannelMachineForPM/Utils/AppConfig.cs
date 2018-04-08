using HLACommonLib;
using HLACommonView.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace HLAChannelMachine.Utils
{
    public class AppConfig
    {
        public static void Load()
        {
            SysConfig.DBUrl = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SysConfig.LGNUM = ConfigurationManager.AppSettings["LGNUM"];
            SysConfig.DeviceNO = ConfigurationManager.AppSettings["DeviceNO"];

            SysConfig.ReaderIp = ConfigurationManager.AppSettings["ReaderIp"];

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

            SysConfig.PrinterName = ConfigurationManager.AppSettings["PrinterName"];

            SysConfig.BoxQty = int.Parse(ConfigurationManager.AppSettings["TotalCheckNum"]);
        }
    }
}
