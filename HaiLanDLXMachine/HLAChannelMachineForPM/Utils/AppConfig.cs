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
            SysConfig.loadConfigPM();
            SysConfig.BoxQty = int.Parse(ConfigurationManager.AppSettings["TotalCheckNum"]);

        }
    }
}
