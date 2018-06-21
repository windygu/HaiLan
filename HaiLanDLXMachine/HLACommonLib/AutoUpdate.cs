using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HLACommonLib.Model;

namespace HLACommonLib
{
    public class AutoUpdate
    {
        /// <summary>
        /// 检查程序版本
        /// </summary>
        /// <param name="type"></param>
        /// <returns>
        /// true 表示版本一致不需要更新
        /// false 表示有异常，需要更新
        /// </returns>
        public static bool Update(SoftwareType type)
        {
            //if (SysConfig.IsTest)
            //    return true;
            VersionsInfo vi = LocalDataService.GetVersionsInfo((int)type);
            if (vi == null)
            {
                MessageBox.Show("检查软件版本时异常，软件即将关闭，请检查网络连接是否正常，再重启软件",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            string currentVersion = Application.ProductVersion;
            
            if (vi.Version != currentVersion)
            {
                //需要更新版本
                MessageBox.Show(
                    string.Format("当前程序有更新版本，点击确定开始更新，请耐心等待...\r\n(升级完成后程序将自动重启)"),
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                string path = Application.StartupPath + @"\" + "HLAUpdate.exe";
                if (File.Exists(path))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(vi.DownloadUrl);
                    sb.Append(" ");
                    sb.Append(currentVersion);
                    sb.Append(" ");
                    sb.Append(vi.Version);
                    sb.Append(" ");
                    string log = vi.UpdateLog.Replace(' ', '_');
                    if("" == log)
                    {
                        log = "log";
                    }
                    sb.Append(log);
                    sb.Append(" ");
                    sb.Append(Application.ProductName);
                    var process = Process.Start(path, sb.ToString());
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("自动升级模块损坏，或已不存在，请联系开发商解决！",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public enum SoftwareType
    { 
        大通道机交货软件 = 1,
        智能单检机 = 2,
        Xindeco智能供应链吊挂系统 = 3,
        大通道机平库发货软件 = 4,
        大通道机分拣复核软件 = 5,
        挂装机收货软件 = 6,
        大通道机电商收货复核软件=7,
        大通道机整理库装箱软件=8,
        挂装移库装箱系统=9,
        大通道机移库装箱系统=10,
        挂装整箱发货系统=11,
        智能单检机短拣专用版 = 12,
        通道机主数据手工下载系统 = 13,
        Xindeco智能供应链系统_吊挂 = 14,
        Xindeco智能供应链系统_大通道机 = 15,
        箱复核 = 16,
        箱复核_吊挂 = 17,
        三禾收获 = 18,
        退货复核 = 19,
        整理库装箱挂装 = 21,
        门店退货查询 = 22,
        拣货单出库 = 23,
        箱装拣货单发货 = 24,
        标签检测 = 25,
        交接单复核 = 26,
        自动下载_HLA = 27,
        自动下载_AJT = 28,
        单检机收货 = 29,
        单检机发货 = 30,
        BigChannelPM = 31,
        退货复核PM = 32,
        电商采购退复核 = 33

    }
}
