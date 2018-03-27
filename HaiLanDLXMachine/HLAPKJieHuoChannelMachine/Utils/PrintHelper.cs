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

namespace HLABoxCheckChannelMachine
{
    public class CPrintRightData
    {
        public string hu = "";
        public string storeType = "";
        public string jihuodate = "";
        public string demawave = "";
        public string daolukou = "";
        public string num = "";
    }
    public class CPSGNum
    {
        public string p = "";
        public string s = "";
        public string g = "";
        public string num = "";
    }
    public class CPrintErrorData
    {
        public string hu = "";
        public List<CPSGNum> psg = new List<CPSGNum>();
    }
    class PrintHelper
    {
        public static void PrintRightTag(CPrintRightData rd)
        {
            try
            {
                string filepath = Application.StartupPath + "\\Print\\RightLabel.mrt";
                StiReport stiReport = new StiReport();
                stiReport.Load(filepath);
                stiReport.Compile();

                stiReport["HU"] = rd.hu;
                stiReport["STORETYPE"] = rd.storeType;
                stiReport["JIHUODATE"] = rd.jihuodate;
                stiReport["DEMAWAVE"] = rd.demawave;
                stiReport["DAOLUKOU"] = rd.daolukou;
                stiReport["SHULIANG"] = rd.num;

                PrinterSettings printerSettings = new PrinterSettings();
                stiReport.Print(false, printerSettings);

            }
            catch (Exception e)
            {
                Log4netHelper.LogError(e);
            }
        }
        public static void PrintErrorTag(CPrintErrorData ed)
        {
            try
            {
                string filepath = Application.StartupPath + "\\Print\\ErrorLabel.mrt";
                StiReport stiReport = new StiReport();
                stiReport.Load(filepath);
                stiReport.Compile();

                stiReport["HU"] = ed.hu;
                StringBuilder content = new StringBuilder();

                foreach (var v in ed.psg)
                {
                    string zsatnr = v.p;
                    string zcolsn = v.s;
                    string zsiztx = v.g;
                    {
                        content.AppendFormat("{0}/{1}/{2}/{3}\r\n", zsatnr, zcolsn, zsiztx, v.num);
                    }
                }
                stiReport["CONTENT"] = content.ToString();

                PrinterSettings printerSettings = new PrinterSettings();
                stiReport.Print(false, printerSettings);

            }
            catch (Exception e)
            {
                Log4netHelper.LogError(e);
            }

        }
    }
}
