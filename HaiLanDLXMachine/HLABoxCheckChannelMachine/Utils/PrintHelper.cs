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

namespace HLABoxCheckChannelMachine.Utils
{
    class PrintHelper
    {
        public static void PrintRightTag(List<HLABoxCheckChannelMachine.PSGCount> box,string curBoxNo)
        {
            try
            {
                if (box.Count <= 0)
                    return;

                int skuCount = 0;
                if (box != null && box.Count > 0)
                {
                    skuCount = box.Count();
                }
                string filepath = "";
                if (skuCount > 10)
                {
                    filepath = Application.StartupPath + "\\Label10Sku.mrt";
                }
                else
                {
                    if (skuCount > 1)
                    {
                        if (box.Exists(i => !i.g.Contains("/")))
                            filepath = Application.StartupPath + "\\LabelMultiSku_Small.mrt";
                        else
                            filepath = Application.StartupPath + "\\LabelMultiSku.mrt";
                    }
                    else
                    {
                        if (box.Exists(i => !i.g.Contains("/")))
                            filepath = Application.StartupPath + "\\LabelSku_Small.mrt";
                        else
                            filepath = Application.StartupPath + "\\LabelSku.mrt";

                    }
                }
                StiReport report = new StiReport();
                report.Load(filepath);
                report.Compile();
                if (skuCount > 10)
                {
                    report["HU"] = curBoxNo;
                    report["CONTENT"] = box.Count.ToString();
                }
                else if (skuCount > 1)
                {
                    report["HU"] = curBoxNo;
                    string content = "";
                    foreach (var matnr in box)
                    {
                        string zsatnr = matnr.p;
                        string zcolsn = matnr.s;
                        string zsiztx = matnr.g;
                        int count = matnr.count;
                        string newzsiztx = null;
                        if (zsiztx.Contains("/"))
                        {
                            try
                            {
                                newzsiztx = zsiztx.Substring(zsiztx.IndexOf('(') + 1).TrimEnd(')');
                            }
                            catch (Exception)
                            {
                                newzsiztx = zsiztx;
                            }
                        }
                        else
                        {
                            newzsiztx = zsiztx;
                        }

                        content += string.Format("{0}/{1}/{2}/{3}\r\n",
                            zsatnr, zcolsn, newzsiztx, count);
                    }
                    report["CONTENT"] = content;
                }
                else
                {
                    HLABoxCheckChannelMachine.PSGCount matnr = box[0];
                    string zsatnr = matnr.p;
                    string zcolsn = matnr.s;
                    string zsiztx = matnr.g;
                    int count = matnr.count;
                    string newzsiztx = null;
                    if (zsiztx.Contains("/"))
                    {
                        try
                        {
                            newzsiztx = zsiztx.Substring(zsiztx.IndexOf('(') + 1).TrimEnd(')');
                        }
                        catch (Exception)
                        {
                            newzsiztx = zsiztx;
                        }
                    }
                    else
                    {
                        newzsiztx = zsiztx;
                    }

                    report["HU"] = curBoxNo;
                    report["PINHAO"] = zsatnr;
                    report["SEHAO"] = zcolsn;
                    report["GUIGE"] = newzsiztx;
                    report["SHULIANG"] = count.ToString();
                }

                PrinterSettings printerSettings = new PrinterSettings();
                report.Print(false, printerSettings);

            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
        }

    }
}
