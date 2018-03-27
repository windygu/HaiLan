using HLACommonLib;
using HLACommonLib.Model;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLAPackingBoxChannelMachine.Utils
{
    public class PrintHelper
    {
        /// <summary>
        /// 打印正常标签
        /// </summary>
        /// <param name="lh">交接楼号</param>
        /// <param name="zsatnr">品号</param>
        /// <param name="zcolsn">色号</param>
        /// <param name="zsiztx">规格</param>
        /// <param name="qty">数量</param>
        /// <param name="hu">箱码</param>
        /// <param name="printerName">打印机名称</param>
        /// <param name="lifnr">交接号</param>
        public static void PrintRightTag(string lh,string zsatnr,
            string zcolsn, string zsiztx,int qty,string hu,
            string printerName,string lifnr)
        {
            try
            {
                string filepath;
                string newzsiztx = null;

                if (zsiztx.Contains("/"))
                {
                    filepath = Application.StartupPath + "\\RIGHTLABEL.mrt";
                    try
                    {
                        newzsiztx = zsiztx.Substring(zsiztx.IndexOf('(') + 1).TrimEnd(')');
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                        newzsiztx = zsiztx;
                    }
                }
                else
                {
                    filepath = Application.StartupPath + "\\RIGHTLABEL_SMALL.mrt";

                    newzsiztx = zsiztx;
                }
                if(lh.ToUpper() == "TH")
                {
                    filepath = Application.StartupPath + "\\RIGHTLABEL_SMALL_TH.mrt";

                }

                StiReport report = new StiReport();
                report.Load(filepath);
                //////设置报表内的参数值
                report.Compile();
                if (lh.ToUpper() == "TH")
                {
                    report["LH"] = "Y001";
                }
                else
                {
                    report["LH"] = lh;
                }
                report["HU"] = hu;
                report["PINHAO"] = zsatnr;
                report["SEHAO"] = zcolsn;
                report["GUIGE"] = newzsiztx;
                report["SHULIANG"] = qty.ToString();
                if(lh.ToUpper() =="TH")
                {
                    report["LIFNR"] = lifnr;
                }
                StiReport.HideMessages = true;

                //Create Printer Settings
                PrinterSettings printerSettings = new PrinterSettings();
                //Set Printer to Use for Printing
                printerSettings.PrinterName = printerName;
                //Direct Print - Don't Show Print Dialog
                report.Print(false, printerSettings);

            }
            catch (Exception ex)
            {
                LogHelper.Error("打印正常箱标出错", ex.Message);
            }
        }

        /// <summary>
        /// 打印异常标签
        /// </summary>
        /// <param name="errorType">错误类型</param>
        /// <param name="hu">箱码</param>
        /// <param name="tagDetailList">错误明细</param>
        public static void PrintErrorTag(string errorType,string hu,
            List<TagDetailInfo> tagDetailList,string printerName)
        {
            try
            {
                string filepath;
                StringBuilder content = new StringBuilder();
                var matnrList = tagDetailList.Select(x => x.MATNR).Distinct();
                bool isExist = false;
                foreach (string matnr in matnrList)
                {
                    string zsatnr = tagDetailList.Find(y => y.MATNR == matnr).ZSATNR;
                    string zcolsn = tagDetailList.Find(y => y.MATNR == matnr).ZCOLSN;
                    string zsiztx = tagDetailList.Find(y => y.MATNR == matnr).ZSIZTX;
                    string qty = tagDetailList.FindAll(y => y.MATNR == matnr).Count.ToString();
                    string newzsiztx = null;
                    if (zsiztx.Contains("/"))
                    {
                        try
                        {
                            newzsiztx = zsiztx.Substring(zsiztx.IndexOf('(') + 1).TrimEnd(')');
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                            newzsiztx = zsiztx;
                        }
                    }
                    else
                    {
                        isExist = true;
                        newzsiztx = zsiztx;
                    }
                    content.AppendFormat("{0}/{1}/{2}/{3}\r\n", zsatnr, zcolsn, newzsiztx, qty);
                }
                if (isExist)
                    filepath = Application.StartupPath + "\\ERRORLABEL_SMALL.mrt";
                else
                    filepath = Application.StartupPath + "\\ERRORLABEL.mrt";
                StiReport report = new StiReport();
                report.Load(filepath);
                //设置报表内的参数值
                report.Compile();
                report["HU"] = hu;
                report["ERROR"] = errorType;
                report["CONTENT"] = content.ToString();
                StiReport.HideMessages = true;

                //Create Printer Settings
                PrinterSettings printerSettings = new PrinterSettings();
                //Set Printer to Use for Printing
                printerSettings.PrinterName = printerName;
                //Direct Print - Don't Show Print Dialog
                report.Print(false, printerSettings);
            }
            catch (Exception ex)
            {
                LogHelper.Error("打印异常箱标出错", ex.Message);
            }

        }
    }
}
