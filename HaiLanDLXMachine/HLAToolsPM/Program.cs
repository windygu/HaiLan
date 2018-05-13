using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using DMSkin;
using HLACommonLib;
using HLATools.Utils;
using System.Configuration;

namespace HLATools
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] arg)
        {
            Process curProcess = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(curProcess.ProcessName);
            if (processes.Length > 1)
            {
                foreach (Process process in processes)
                {
                    if (curProcess.Id != process.Id)
                    {
                        if ((curProcess.MainModule.FileName == process.MainModule.FileName))//只检查进程在不同目录下的情况
                        {
                            process.WaitForExit(2000);//等待上次的进程结束后再检查，否则重启终端时会有问题
                            if (!process.HasExited)
                            {
                                MetroMessageBox.Show(null, "检测到程序已运行！", "警告");
                                return;
                            }
                        }
                    }
                }
            }

            //载入默认配置
            AppConfig.Load();

            if (arg.Length >= 3)
            {
                SysConfig.LGNUM = arg[0];
                SysConfig.DeviceNO = arg[1];
                SysConfig.DBUrl = arg[2];
            }
            else
            {
                /*
                MessageBox.Show("请从主界面运行程序", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
                */
                SysConfig.DBUrl = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
                SysConfig.LGNUM = ConfigurationManager.AppSettings["LGNUM"];
                SysConfig.DeviceNO = ConfigurationManager.AppSettings["DeviceNO"];

            }

            if (AutoUpdate.Update(SoftwareType.智能单检机))
            {
                SAPDataService.Init();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LoginForm());
            }
        }
    }
}
