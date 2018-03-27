using HLACommonLib;
using HLACommonLib.Model;
using HLACommonView.Views.Dialogs;
using Newtonsoft.Json;
using HLABoxCheckChannelMachine.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace HLABoxCheckChannelMachine
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
                                MessageBox.Show("检测到 拣货单发货 已运行！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }
            }

            AppConfig.Load();

#if DEBUG
            SysConfig.LGNUM = "HL01";
            SysConfig.DeviceNO = "08F041";
            SysConfig.DBUrl = @"Data Source=172.18.10.98;Initial Catalog=heilandb;User ID=sa;password=xindeco.123456";

#else

            if (arg.Length >= 3)
            {
                SysConfig.LGNUM = arg[0];
                SysConfig.DeviceNO = arg[1];
                SysConfig.DBUrl = arg[2];
            }
            else
            {
                MessageBox.Show("请从主界面运行程序", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
#endif

            if (AutoUpdate.Update(SoftwareType.箱装拣货单发货))
            {
                SAPDataService.Init();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LoginForm());
            }

        }
    }
}
