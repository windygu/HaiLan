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

            SysConfig.ReaderComPort = ConfigurationManager.AppSettings["ReaderComPort"];
            SysConfig.ReaderPower = ConfigurationManager.AppSettings["ReaderPower"];

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
