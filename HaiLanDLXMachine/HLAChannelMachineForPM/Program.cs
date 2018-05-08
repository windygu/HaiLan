using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HLAChannelMachine.Utils;
using HLACommonLib;
using HLACommonLib.Model;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Data;
using HLAChannelMachine.DialogForm;

namespace HLAChannelMachine
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
                                MessageBox.Show("检测到 单检机收货系统 已运行！");
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
                MessageBox.Show("请从主界面运行程序", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (AutoUpdate.Update(SoftwareType.单检机收货))
            {
                SAPDataService.Init();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LoginForm());
            }
        }

    }
}
