using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using OSharp.Utility.Extensions;
namespace HLACommonLib
{
    public class LogHelper
    {
        private static bool IsDebug = ConfigurationManager.AppSettings["Debug"].CastTo<bool>(true);// bool.Parse(ConfigurationManager.AppSettings["Debug"]);
        private static string filename = "Log/log.txt";
        private static FileStream fs = null;
        private static StreamWriter sw = null;

        public static void Init()
        {
            filename = string.Format("Log/{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
            if (!Directory.Exists("Log"))
            {
                Directory.CreateDirectory("Log");
            }
            fs = new FileStream(filename, FileMode.Append);
            sw = new StreamWriter(fs, Encoding.UTF8);
            sw.AutoFlush = true;
        }

        public static void WriteLine(string msg)
        {
            if (!IsDebug)
                return;

            if (fs == null || sw == null)
            {
                Init();
            }

            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":" + msg);
        }

        public static void Error(string title, string msg)
        {
            if (fs == null || sw == null)
            {
                Init();
            }

            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":[" + title + "]:" + msg);
        }

        public static void Close()
        {
            if (sw != null)
                sw.Close();
            if (fs != null)
                fs.Close();
        }
    }
}
