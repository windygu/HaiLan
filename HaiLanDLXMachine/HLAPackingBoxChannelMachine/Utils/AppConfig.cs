using HLACommonLib;
using HLACommonView.Configs;
using HLACommonView.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace HLAPackingBoxChannelMachine.Utils
{
    public class AppConfig
    {
        public static void Load()
        {
            SysConfig.loadConfig();

        }
    }
}
