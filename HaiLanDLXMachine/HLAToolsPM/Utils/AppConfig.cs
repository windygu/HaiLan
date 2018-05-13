using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using HLACommonLib;
using HLACommonView.Configs;

namespace HLATools.Utils
{
    public class AppConfig
    {
        public static void Load()
        {
            SysConfig.loadConfigPM();
        }
    }
}
