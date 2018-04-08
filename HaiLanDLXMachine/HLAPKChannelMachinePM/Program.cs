﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMSkin;
using HLACommonLib;
using HLACommonLib.Model;
using HLADeliverChannelMachine.Utils;

namespace HLADeliverChannelMachine
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            LogHelper.WriteLine("程序开始运行...");
            Process[] processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            Process curProcess = Process.GetCurrentProcess();
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
                                MessageBox.Show("检测到 Xindeco智能供应链系统(吊挂) 已运行！", "警告");
                                return;
                            }
                        }
                    }
                }
            }

            //载入默认配置
            AppConfig.Load();
            SysConfig.InitUomDic();

            //检测版本
            if (AutoUpdate.Update(SoftwareType.Xindeco智能供应链吊挂系统))
            {
                SAPDataService.Init();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                try
                {
                    Application.Run(new LoginForm());
                }
                catch (Exception e)
                {
                    LogHelper.Error(e.Message, e.StackTrace);
                    MessageBox.Show("程序出现问题，请联系管理员！" + e.Message, "警告");
                }
            }
        }
    }
}
