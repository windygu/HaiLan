using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HLAAutoDownload.Utils;
using HLACommonLib;
using System.Diagnostics;

namespace HLAAutoDownload
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
            //载入默认配置
            AppConfig.Load();

            if (AutoUpdate.Update(SoftwareType.自动下载_HLA))
            {
                SAPDataService.Init();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new DownloadForm());
            }
        }
    }
}
