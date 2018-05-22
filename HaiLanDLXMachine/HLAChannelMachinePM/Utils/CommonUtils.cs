using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLACommonLib;
using System.Xml;
using HLACommonLib.Model;
using System.Windows.Forms;
using Stimulsoft.Report;
using System.Drawing.Printing;

namespace HLAChannelMachine.Utils
{
    public class CommonUtils
    {
        /// <summary>
        /// 打印正常箱标
        /// </summary>
        /// <param name="inventoryResult">盘点结果</param>
        /// <param name="boxNo">箱码</param>
        /// <param name="zsatnr">品号</param>
        /// <param name="zcolsn">色号</param>
        /// <param name="zsiztx">规格</param>
        /// <param name="charg">批次</param>
        /// <param name="qty">数量</param>
        /// <returns></returns>
        public static bool PrintRightBoxTag(bool inventoryResult, string boxNo, List<ListViewTagInfo> lvTagInfo)
        {
            string data = SysConfig.RightPrintTemplate;

            ListViewTagInfo tagDetailItem = lvTagInfo[0];
            string zsatnr = tagDetailItem.ZSATNR;
            string zcolsn = tagDetailItem.ZCOLSN;
            string zsiztx = tagDetailItem.ZSIZTX;
            string charg = tagDetailItem.CHARG;
            int qty = tagDetailItem.QTY;

            string newzsiztx = null;
            try
            {
                newzsiztx = zsiztx.Substring(zsiztx.IndexOf('(') + 1).TrimEnd(')');
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                newzsiztx = zsiztx;
            }
            data = data.Replace("12345678", boxNo);
            data = data.Replace("#PINHAO#", zsatnr);
            data = data.Replace("#SEHAO#", zcolsn);
            data = data.Replace("#GUIGE#", newzsiztx);
            data = data.Replace("#PICI#", charg);
            data = data.Replace("#SHULIANG#", qty.ToString());
            data = data.Replace("#JIEGUO#", inventoryResult ? "S" : "E");
            bool result = ZebraPrintHelper.PrintWithDRV(data, SysConfig.PrinterName, false);

            return result;
        }

        /// <summary>
        /// 打印正常箱标
        /// </summary>
        /// <param name="inventoryResult">盘点结果</param>
        /// <param name="boxNo">箱码</param>
        /// <param name="zsatnr">品号</param>
        /// <param name="zcolsn">色号</param>
        /// <param name="zsiztx">规格</param>
        /// <param name="charg">批次</param>
        /// <param name="qty">数量</param>
        /// <returns></returns>
        public static bool PrintRightBoxTagV2(bool inventoryResult, string boxNo, List<ListViewTagInfo> lvTagInfo)
        {
            try
            {
                ListViewTagInfo tagDetailItem = lvTagInfo[0];
                string zsatnr = tagDetailItem.ZSATNR;
                string zcolsn = tagDetailItem.ZCOLSN;
                string zsiztx = tagDetailItem.ZSIZTX;
                string charg = tagDetailItem.CHARG;
                int qty = tagDetailItem.QTY;
                string filepath;
                string newzsiztx = null;
                if(zsiztx.Contains("/"))
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

                StiReport stiReport1 = new StiReport();
                stiReport1.Load(filepath);
                //////设置报表内的参数值
                stiReport1.Compile();
                stiReport1["HU"] = boxNo;
                stiReport1["PINHAO"] = zsatnr;
                stiReport1["SEHAO"] = zcolsn;
                stiReport1["GUIGE"] = newzsiztx;
                stiReport1["SHULIANG"] = qty.ToString();

                StiReport.HideMessages = true;

                //Create Printer Settings
                PrinterSettings printerSettings = new PrinterSettings();
                //Set Printer to Use for Printing
                printerSettings.PrinterName = SysConfig.PrinterName;
                //Direct Print - Don't Show Print Dialog
                stiReport1.Print(false, printerSettings);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine("打印正常箱标出错\r\n"+ex.Message);
            }

            return false;
        }


        public static bool PrintRightBoxTagWithPBNO(string boxNo, string zpbno,int qty)
        {
            try
            {
                string filepath = Application.StartupPath + "\\RIGHTPBLABEL.mrt";
                StiReport stiReport1 = new StiReport();
                stiReport1.Load(filepath);
                //////设置报表内的参数值
                stiReport1.Compile();
                stiReport1["HU"] = boxNo;
                stiReport1["ZPBNO"] = zpbno;
                stiReport1["SHULIANG"] = qty.ToString();

                StiReport.HideMessages = true;

                //Create Printer Settings
                PrinterSettings printerSettings = new PrinterSettings();
                //Set Printer to Use for Printing
                printerSettings.PrinterName = SysConfig.PrinterName;
                //Direct Print - Don't Show Print Dialog
                stiReport1.Print(false, printerSettings);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine("打印正常箱标出错\r\n"+ ex.Message);
            }

            return false;
        }

        /// <summary>
        /// 打印异常箱标
        /// </summary>
        /// <param name="inventoryResult">盘点结果</param>
        /// <param name="boxNo">箱码</param>
        /// <param name="listviewItems"></param>
        /// <returns></returns>
        public static bool PrintErrorBoxTag(bool inventoryResult, string boxNo, List<ListViewTagInfo> lvTagInfo)
        {
            string data = SysConfig.ErrorPrintTemplate;
            data = data.Replace("12345678", boxNo);
            int i = 1;
            if (lvTagInfo.Count > 0)
            {
                foreach (ListViewTagInfo item in lvTagInfo)
                {
                    if (i > 5)
                        break;

                    string zsatnr = item.ZSATNR;
                    string zcolsn = item.ZCOLSN;
                    string zsiztx = item.ZSIZTX;
                    string charg = item.CHARG;
                    int qty = item.QTY;
                    string newzsiztx = null;
                    try
                    {
                        newzsiztx = zsiztx.Substring(zsiztx.IndexOf('(') + 1).TrimEnd(')');
                    }
                    catch(Exception ex)
                    {
                        LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                        newzsiztx = zsiztx;
                    }
                    data = data.Replace(string.Format("#PINHAO{0}#", i), zsatnr);
                    data = data.Replace(string.Format("#SEHAO{0}#", i), zcolsn);
                    data = data.Replace(string.Format("#GUIGE{0}#", i), newzsiztx);
                    data = data.Replace(string.Format("#SHULIANG{0}#", i), qty.ToString());

                    i++;
                }
            }

            while(i <= 5)
            {
                data = data.Replace(string.Format("#PINHAO{0}#", i), " ");
                data = data.Replace(string.Format("#SEHAO{0}#", i), " ");
                data = data.Replace(string.Format("#GUIGE{0}#", i), " ");
                data = data.Replace(string.Format("#SHULIANG{0}#", i), " ");

                i++;
            }

            bool result = ZebraPrintHelper.PrintWithDRV(data, SysConfig.PrinterName, false);

            return result;
        }

        /// <summary>
        /// 打印异常箱标
        /// </summary>
        /// <param name="inventoryResult">盘点结果</param>
        /// <param name="boxNo">箱码</param>
        /// <param name="listviewItems"></param>
        /// <returns></returns>
        public static bool PrintErrorBoxTagV2(bool inventoryResult, string boxNo, List<ListViewTagInfo> lvTagInfo)
        {
            try
            {
                string filepath = Application.StartupPath + "\\ERRORLABEL.mrt";
                StiReport stiReport1 = new StiReport();
                stiReport1.Load(filepath);
                //////设置报表内的参数值
                stiReport1.Compile();
                stiReport1["HU"] = boxNo;

                int i = 1;
                if (lvTagInfo.Count > 0)
                {
                    foreach (ListViewTagInfo item in lvTagInfo)
                    {
                        if (i > 5)
                            break;

                        string zsatnr = item.ZSATNR;
                        string zcolsn = item.ZCOLSN;
                        string zsiztx = item.ZSIZTX;
                        string charg = item.CHARG;
                        int qty = item.QTY;
                        string newzsiztx = null;
                        try
                        {
                            newzsiztx = zsiztx.Substring(zsiztx.IndexOf('(') + 1).TrimEnd(')');
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                            newzsiztx = zsiztx;
                        }
                        stiReport1[string.Format("PINHAO{0}", i)] = zsatnr;
                        stiReport1[string.Format("SEHAO{0}", i)] = zcolsn;
                        stiReport1[string.Format("GUIGE{0}", i)] = newzsiztx;
                        stiReport1[string.Format("SHULIANG{0}", i)] = qty.ToString();

                        i++;
                    }
                }

                while (i <= 5)
                {
                    stiReport1[string.Format("PINHAO{0}", i)] = " ";
                    stiReport1[string.Format("SEHAO{0}", i)] = " ";
                    stiReport1[string.Format("GUIGE{0}", i)] = " ";
                    stiReport1[string.Format("SHULIANG{0}", i)] = " ";

                    i++;
                }

                StiReport.HideMessages = true;

                //Create Printer Settings
                PrinterSettings printerSettings = new PrinterSettings();
                //Set Printer to Use for Printing
                printerSettings.PrinterName = SysConfig.PrinterName;
                //Direct Print - Don't Show Print Dialog
                stiReport1.Print(false, printerSettings);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine("打印异常箱标出错\r\n"+ex.Message);
            }

            return false;
        }
    }
}
