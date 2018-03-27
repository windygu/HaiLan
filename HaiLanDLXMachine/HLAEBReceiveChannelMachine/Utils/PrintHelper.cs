using HLACommonLib;
using HLACommonLib.Model;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLAEBReceiveChannelMachine.Utils
{
    public class PrintHelper
    {
        /// <summary>
        /// 打印正确箱标签
        /// </summary>
        /// <param name="lgpla">上架仓位</param>
        /// <param name="boxnum">箱码</param>
        /// <param name="lvTagInfo">明细信息</param>
        /// <returns></returns>
        public static bool PrintRightBoxTag(string lgpla,string boxnum,
            List<ListViewTagInfo> lvTagInfo,bool inventoryresult)
        {
            try
            {
                ListViewTagInfo tagDetailItem = null;
                //lgpla = "DS01-12345678";
                if (lvTagInfo.Count>0)
                    tagDetailItem = lvTagInfo[0];
                string zsatnr = tagDetailItem != null ? tagDetailItem.ZSATNR : "未扫描到商品";
                string zcolsn = tagDetailItem != null ? tagDetailItem.ZCOLSN : "";
                string zsiztx = tagDetailItem != null ? tagDetailItem.ZSIZTX : "";
                //string charg = tagDetailItem.CHARG;
                int qty = tagDetailItem != null ? tagDetailItem.QTY : 0;
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
                StiReport stiReport = new StiReport();
                stiReport.Load(filepath);
                //////设置报表内的参数值
                stiReport.Compile();
                stiReport["HU"] = boxnum + (inventoryresult ? "" : "(异常)");
                stiReport["PINHAO"] = zsatnr;
                stiReport["SEHAO"] = zcolsn;
                stiReport["GUIGE"] = newzsiztx;
                stiReport["SHULIANG"] = qty.ToString();
                if (lgpla.Contains("-"))
                {
                    stiReport["KUWEI"] = string.Format("{0}", lgpla.Split('-')[0]);
                    stiReport["KUWEI02"] = string.Format("{0}", lgpla.Split('-')[1]);
                }
                else
                {
                    stiReport["KUWEI02"] = string.Format("{0}", lgpla);
                }

                stiReport["DATE"] = DateTime.Now.ToString("yyyy-MM-dd");

                PrinterSettings printerSettings = new PrinterSettings();
                stiReport.Print(false, printerSettings);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error("打印正常箱标出错", ex.Message);
            }

            return false;
        }

        public static bool PrintErrorBoxTag(List<ListViewTagInfo> lvTagInfo)
        {
            try
            {
                string filepath;
                StringBuilder content = new StringBuilder();
                int i = 0;
                if (lvTagInfo != null && lvTagInfo.Count > 0)
                {
                    //是否存在不包含'/'的规格
                    bool isExist = false;
                    foreach (ListViewTagInfo item in lvTagInfo)
                    {
                        if (i < 15)
                        {
                            string zsatnr = item.ZSATNR;
                            string zcolsn = item.ZCOLSN;
                            string zsiztx = item.ZSIZTX;
                            string charg = item.CHARG;
                            int qty = item.QTY;
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
                            i++;
                        }
                    }
                    if(isExist)
                        filepath = Application.StartupPath + "\\ERRORLABEL_SMALL.mrt";
                    else
                        filepath = Application.StartupPath + "\\ERRORLABEL.mrt";
                }
                else
                    filepath = Application.StartupPath + "\\ERRORLABEL.mrt";
                StiReport stiReport = new StiReport();
                stiReport.Load(filepath);
                //设置报表内的参数值
                stiReport.Compile();
                stiReport["CONTENT1"] = content.Length <= 0 ? "未扫描到商品" : content.ToString();
               

                PrinterSettings printerSettings = new PrinterSettings();
                stiReport.Print(false, printerSettings);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error("打印异常箱标出错", ex.Message);
            }

            return false;
        }
    }
}
