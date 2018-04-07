using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HLACommonLib;
using HLAManualDownload.Utils;
using System.Diagnostics;
using System.Configuration;

namespace HLAManualDownload
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(String[] arg)
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
                                MessageBox.Show("检测到 通道机主数据手工下载系统 已运行！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }
            }

            AppConfig.Load();

            if (arg.Length >= 3)
            {               
                SysConfig.LGNUM = arg[0];
                SysConfig.DeviceNO = arg[1];
                SysConfig.DBUrl = arg[2];
            }
            else
            {
                SysConfig.DBUrl = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
                SysConfig.LGNUM = ConfigurationManager.AppSettings["LGNUM"];
            }

            if (AutoUpdate.Update(SoftwareType.通道机主数据手工下载系统))
            {
                //载入默认配置
                try
                {
                    SAPDataService.Init();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "警告");
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}
