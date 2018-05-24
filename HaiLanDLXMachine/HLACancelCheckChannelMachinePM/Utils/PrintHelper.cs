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
using System.Data;

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
                string filepath = Application.StartupPath + "\\LabelMultiSkuCancel.mrt";
                StiReport report = new StiReport();
                report.Load(filepath);
                report.Compile();

                report["RE"] = contentData.inventoryRe ? "正常" : "异常";
                report["TotalNum"] = contentData.totalNum.ToString();
                report["Tag"] = contentData.beizhu;
                report["HU"] = contentData.hu;

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("pin", Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("se", Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("gui", Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("diff", Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("diffAdd", Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("isRFID", Type.GetType("System.String")));

                foreach (var v in contentData.content)
                {
                    DataRow row = dt.NewRow();
                    row["pin"] = v.pin;
                    row["se"] = v.se;
                    row["gui"] = v.gui;
                    row["diff"] = v.diff.ToString();
                    row["diffAdd"] = v.diffAdd.ToString();
                    row["isRFID"] = v.isRFID ? "是" : "否";
                    dt.Rows.Add(row);
                }
                report.RegData("demo", dt);

                PrinterSettings printerSettings = new PrinterSettings();
                report.Print(false, printerSettings);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

    }
}
