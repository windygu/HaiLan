using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using HLACommonLib;

namespace HLAManualDownload.Utils
{
    public class AppConfig
    {
        public static void Load()
        {
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
        }
    }
}
