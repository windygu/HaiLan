using HLACommonLib;
using HLACommonView.Configs;
using HLACommonView.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;


namespace HLAJiaoJieCheckChannelMachine
{
    public class AppConfig
    {
        public static void Load()
        {
            SysConfig.loadConfig();
            SysConfig.HttpKey = ConfigurationManager.AppSettings["HttpKey"];
            SysConfig.HttpUrl = ConfigurationManager.AppSettings["HttpUrl"];
            SysConfig.HttpSec = ConfigurationManager.AppSettings["HttpSec"];
        }

    }
}
