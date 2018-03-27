using HLACommonLib;
using HLACommonLib.Model.YK;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;

namespace HLACancelCheckChannelMachine.Utils
{
    public class CPrintContent
    {
        public CPrintContent()
        {
            pin = "";
            se = "";
            gui = "";
            diff = 0;
            diffAdd = 0;
            isRFID = true;
        }
        public CPrintContent(string pin,string se,string gui,int diff,int diffAdd,bool isRFID)
        {
            this.pin = pin;
            this.se = se;
            this.gui = gui;
            this.diff = diff;
            this.diffAdd = diffAdd;
            this.isRFID = isRFID;
        }
        public string pin;
        public string se;
        public string gui;
        public int diff;
        public int diffAdd;
        public bool isRFID;
    }
    public class CPrintData
    {
        public List<CPrintContent> content = new List<CPrintContent>();
        public string hu;
        public bool inventoryRe;
        public int totalNum;
        public string beizhu;
    }
    class PrintHelper
    {
        public static string extractGuiGe(string guige)
        {
            try
            {
                if (guige.Contains("/"))
                {
                    guige = guige.Substring(guige.IndexOf('(') + 1).TrimEnd(')');
                    return guige;
                }
                else
                    return guige;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
            return "";

        }
        public static void PrintTag(CPrintData contentData)
        {
            try
            {
                string filepath = Application.StartupPath + "\\LabelMultiSku.mrt";
                StiReport report = new StiReport();
                report.Load(filepath);
                report.Compile();

                report["RE"] = contentData.inventoryRe ? "正常" : "异常";
                report["TotalNum"] = contentData.totalNum.ToString();
                report["Tag"] = contentData.beizhu;
                report["HU"] = contentData.hu;

                string con = "";
                foreach(var i in contentData.content)
                {
                    con += string.Format("{0}|{1}|{2}|{3}|{4}|{5}\r\n", i.pin, i.se, extractGuiGe(i.gui), i.diff.ToString(), i.diffAdd.ToString(), i.isRFID ? "是" : "否");
                }
                report["CONTENT"] = con;

                PrinterSettings printerSettings = new PrinterSettings();
                report.Print(false, printerSettings);
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

    }
}
