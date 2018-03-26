using HLACommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace HLABigChannel
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process curProcess = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(curProcess.ProcessName);
            if (processes.Length > 1)
            {
                foreach (Process process in processes)
                {
                    if (curProcess.Id != process.Id)
                    {
                        if ((curProcess.MainModule.FileName == process.MainModule.FileName))
                        {
                            process.WaitForExit(2000);
                            if (!process.HasExited)
                            {
                                MessageBox.Show("检测到 Xindeco智能供应链系统(大通道机) 已运行！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }
            }

            AppConfig.Load();

            if (AutoUpdate.Update(SoftwareType.Xindeco智能供应链系统_大通道机))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}
