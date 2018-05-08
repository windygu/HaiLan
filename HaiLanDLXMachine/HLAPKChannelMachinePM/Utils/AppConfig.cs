using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using HLACommonLib;
using HLACommonLib.Model;
using HLACommonView.Model;

namespace HLADeliverChannelMachine.Utils
{
    public class AppConfig
    {
        public static void Load()
        {
            SysConfig.loadConfigPM();
        }
    }
}
