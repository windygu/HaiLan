using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using HLACommonLib;

namespace HLABigChannel
{ 
    public class AppConfig
    {
        public static void Load()
        {
            SysConfig.DBUrl = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SysConfig.LGNUM = ConfigurationManager.AppSettings["LGNUM"];
            SysConfig.DeviceNO = ConfigurationManager.AppSettings["DeviceNO"];

        }
    }
}
