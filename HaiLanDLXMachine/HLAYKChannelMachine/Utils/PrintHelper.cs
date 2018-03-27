using HLACommonLib;
using HLACommonLib.Model.YK;
using HLACommonView.Configs;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonLib.Model;

namespace HLAYKChannelMachine.Utils
{
    public class PrintHelper
    {
        public static void PrintRightTag(YKBoxInfo box,List<MaterialInfo> mats)
        {
            try
            {
                int skuCount = 0;
                if(box.Details!=null && box.Details.Count>0)
                {
                    skuCount = box.Details.Select(i => i.Matnr).Distinct().Count();
                }
                string filepath = "";
                if (skuCount>1)
                {
                    filepath = Application.StartupPath + "\\LabelMultiSku.mrt";
                }
                else if(skuCount == 1)
                {
                    filepath = Application.StartupPath + "\\LabelSku.mrt";
                }
                if(filepath == "")
                {
                    throw new Exception("no sku");
                }

                StiReport report = new StiReport();
                report.Load(filepath);
                report.Compile();

                if(skuCount>1)
                {
                    //multi
                    report["HU"] = box.Hu;
                    report["STORAGETYPE"] = box.Target;

                    string content = "";
                    List<string> matnrlist = box.Details.Select(i => i.Matnr).Distinct().ToList();
                    foreach (string matnr in matnrlist)
                    {
                        string zsatnr = box?.Details?.First(i => i.Matnr == matnr).Zsatnr;
                        string zcolsn = box?.Details?.First(i => i.Matnr == matnr).Zcolsn;
                        string zsiztx = box?.Details?.First(i => i.Matnr == matnr).Zsiztx;
                        int count = box.Details.FindAll(i => i.Matnr == matnr).Count;

                        string controlFlag = "";
                        controlFlag = mats.FirstOrDefault(i => i.MATNR == matnr).PUT_STRA;

                        content += string.Format("|{0,-13}|{1,-7}|{2,-14}|{3,-3}|{4,-7}|\r\n",
                            zsatnr, zcolsn, zsiztx, count, controlFlag);
                    }
                    report["CONTENT"] = content;

                }
                else
                {
                    string zsatnr = box?.Details?.First().Zsatnr;
                    string zcolsn = box?.Details?.First().Zcolsn;
                    string zsiztx = box?.Details?.First().Zsiztx;
                    int count = box.Details.Count(i => i.IsAdd != 1);

                    string controlFlag = "";
                    string mat = box?.Details?.First().Matnr;
                    controlFlag = mats.FirstOrDefault(i => i.MATNR == mat).PUT_STRA;
                    string MAKTX = mats.FirstOrDefault(i => i.MATNR == mat).MAKTX;

                    report["HU"] = box.Hu;
                    report["STORAGETYPE"] = box.Target;
                    report["RUKUBIAOZHI"] = controlFlag;
                    report["LIFNR"] = box.LIFNR;
                    report["PINHAO"] = zsatnr;
                    report["SEHAO"] = zcolsn;
                    report["GUIGE"] = zsiztx;
                    report["SHULIANG"] = count.ToString();
                    report["MAKTX"] = MAKTX;
                }

                PrinterSettings printerSettings = new PrinterSettings();
                report.Print(false, printerSettings);

            }
            catch (Exception ex)
            {
                LogHelper.Error("打印正常箱标(按规格)出错", ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public static void PrintErrorTag(YKBoxInfo box,bool checkSku)
        {
            try
            {
                string content = "";
                string filepath = Application.StartupPath + "\\LabelError.mrt";
                if (box.Details.Count > 0)
                {
                    List<string> matnrlist = box.Details.Select(i => i.Matnr).Distinct().ToList();
                    if (matnrlist.Count > 10)
                    {
                        if (checkSku)
                        {
                            content = Consts.Default.SHANG_PIN_DA_YU_SHI;
                        }
                    }
                    else
                    {
                        bool isExist = false;
                        foreach (string matnr in matnrlist)
                        {
                            string zsatnr = box?.Details?.First(i => i.Matnr == matnr).Zsatnr;
                            string zcolsn = box?.Details?.First(i => i.Matnr == matnr).Zcolsn;
                            string zsiztx = box?.Details?.First(i => i.Matnr == matnr).Zsiztx;
                            int count = box.Details.FindAll(i => i.Matnr == matnr).Count;
                            if (zsiztx.Contains("/"))
                            {
                            }
                            else
                            {
                                isExist = true;
                            }
                            content += string.Format("{0}/{1}/{2}/{3}\r\n",
                                zsatnr, zcolsn, zsiztx, count);
                        }

                        if(isExist)
                        {
                            filepath = Application.StartupPath + "\\LabelError_Small.mrt";
                        }
                    }
                }
                else
                {
                    content = "未扫描到商品";
                }

                
                StiReport report = new StiReport();
                report.Load(filepath);
                report.Compile();
                report["HU"] = box.Hu;
                report["CONTENT"] = content;
                PrinterSettings printerSettings = new PrinterSettings();
                report.Print(false, printerSettings);

            }
            catch (Exception ex)
            {
                LogHelper.Error("打印异常箱标出错", ex.Message + "\r\n" + ex.StackTrace);
            }
        }
    }
}
