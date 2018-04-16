using HLACommonLib;
using HLACommonView.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OSharp.Utility.Extensions;

namespace HLAChannelMachine.Utils
{
    public class AppConfig
    {
        public static void Load()
        {
            SysConfig.RunningModel = (RunMode)Enum.Parse(typeof(RunMode), ConfigurationManager.AppSettings["RunMode"].CastTo("2"));

            SysConfig.loadConfig();

        }
    }
}
