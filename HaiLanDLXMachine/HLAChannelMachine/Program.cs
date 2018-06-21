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
using HLACommonLib.Model.RECEIVE;
using HLACommonLib.DAO;

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
                                MessageBox.Show("检测到 通道机检货系统 已运行！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }
            }

            AppConfig.Load();

#if DEBUG
            SysConfig.LGNUM = "HL01";
            SysConfig.DeviceNO = "GQ-4015";
            SysConfig.DBUrl = @"Data Source=172.18.207.92;Initial Catalog=heilandb;User ID=sa;password=myk_123456";

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

            if (AutoUpdate.Update(SoftwareType.大通道机交货软件))
            {
                AppConfig.Load();
                SAPDataService.Init();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LoginForm());
            }
        }
    }
}
