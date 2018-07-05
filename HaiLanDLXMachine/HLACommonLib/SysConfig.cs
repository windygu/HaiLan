using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using HLACommonLib;
using HLACommonLib.Model;
using System.Xml;
using System.Windows.Forms;
using System.Net;

namespace HLACommonLib
{
    public enum RunMode
    {
        平库 = 1,
        高位库 = 2,
        电商库 = 3
    }
    public enum ERRORDATATYPE
    {
        读写器未连接 = 1,
        未扫描到箱号,
        正常
    }
    public class CReadParam
    {
        public static int ghost = 3;
        public static int trigger = 280;
        public static int r6ghost = 3;

    }
    public class SysConfig
    {
        //仓库编号
        public static string LGNUM = "HL01";
        //软件版本号
        public static string Version = Application.ProductVersion;
        //RSSI限制值
        public static double RssiLimit = 0;
        //延迟时间设置
        public static int DelayTime = 0;
        //延迟时间设置2
        public static int DelayTime2 = 1000;
        //串口地址
        public static string Port = "COM1";
        //条码扫描枪串口地址
        public static string ScannerPort_1 = "COM4";
        public static string ScannerPort_2 = "COM5";
        //读写器IP
        public static string ReaderIp = "192.168.8.166:8086";

        public static string SearchMode = "1";
        public static string Session = "2";

        public static int mReaderPower = 2700;
        //读写器参数
        public static ReaderConfig ReaderConfig = new ReaderConfig();
        /// <summary>
        /// 所在楼层对应的设备终端编码
        /// </summary>
        public static string Floor { get { return DeviceInfo?.LOUCENG; } }
        /// <summary>
        /// 设备编码对应设备终端号
        /// </summary>
        public static string sEQUIP_HLA { get { return DeviceInfo?.EQUIP_HLA; } }

        /// <summary>
        /// 当前运行模式 默认平库
        /// </summary>
        public static RunMode RunningModel = RunMode.平库;

        //当前登录用户
        public static UserInfo CurrentLoginUser = null;

        //当前连接的数据库的IP
        public static string DBUrl = "localhost";

        //当前运行的设备类型
        //public static string Device = "TDJ-ZZ-1";

        /// <summary>
        /// 设备编号
        /// </summary>
        public static string DeviceNO = "10E901";

        /// <summary>
        /// 是否支持hub
        /// </summary>
        public static int IsHub = 0;

        /// <summary>
        /// 设备基础信息
        /// </summary>
        public static DeviceTable DeviceInfo = null;
        /// <summary>
        /// 下架单类型(箱装或挂装)
        /// </summary>
        public static string sKF_LX = null;
        /// <summary>
        /// 是否正式环境
        /// </summary>
        public static bool UseFormal = true;
        //SAP相关配置
        public static string AppServerHost = "172.18.200.14";
        public static string SystemNumber = "00";
        public static string User = "093482";
        public static string Password = "sunrain";
        public static string Client = "300";
        public static string Language = "ZH";
        public static string PoolSize = "5";
        public static string PeakConnectionsLimit = "10";
        public static string IdleTimeout = "60";

        public static string UseGroupLogon = "1";
        public static string LogonGroup = "EWM_PRD";
        public static string SystemID = "EWP";
        public static string MessageServerHost = "172.18.200.81";
        public static string MessageServerService = "3600";
        //打印相关配置
        public static string PrinterName = "ZDesigner GX430t";
        public static string RightPrintTemplate = "";
        public static string ErrorPrintTemplate = "";

        public static bool IsTest = false;

        public static bool IsLog = false;

        /// <summary>
        /// 单位中英对照表
        /// </summary>
        public static Dictionary<string, string> UOMDic = new Dictionary<string, string>();

        #region 挂装机收货相关代码
        public static int TrackNum = 2;
        public static int EachTrackNum = 10;
        #endregion
        public static int BoxQty = 0;

        public static string ReaderComPort = "COM1";
        public static string ReaderPower = "23";

        public static string HttpKey = "";
        public static string HttpUrl = "";
        public static string HttpSec = "";
        /// <summary>
        /// 初始化单位中英对照表
        /// </summary>
        public static void InitUomDic()
        {
            UOMDic.Add("AK", "PAR");
            UOMDic.Add("BAG", "袋");
            UOMDic.Add("BCH", "束");
            UOMDic.Add("BLK", "块");
            UOMDic.Add("BND", "捆");
            UOMDic.Add("BOK", "本");
            UOMDic.Add("BOT", "瓶");
            UOMDic.Add("BOX", "盒");
            UOMDic.Add("BRL", "桶");
            UOMDic.Add("BTC", "批");
            UOMDic.Add("BUT", "粒");
            UOMDic.Add("BYT", "BYT");
            UOMDic.Add("CHP", "片");
            UOMDic.Add("CS", "套");
            UOMDic.Add("DA2", "套");
            UOMDic.Add("DEG", "度");
            UOMDic.Add("DOR", "扇");
            UOMDic.Add("DPR", "DRR");
            UOMDic.Add("DR", "根");
            UOMDic.Add("DRR", "条");
            UOMDic.Add("DSH", "盘");
            UOMDic.Add("DZ", "打");
            UOMDic.Add("EA", "件");
            UOMDic.Add("EE", "个");
            UOMDic.Add("EEA", "支");
            UOMDic.Add("EEB", "把");
            UOMDic.Add("EEC", "枚");
            UOMDic.Add("EED", "顶");
            UOMDic.Add("EEE", "只");
            UOMDic.Add("EEJ", "节");
            UOMDic.Add("EEM", "面");
            UOMDic.Add("EML", "EML");
            UOMDic.Add("FLW", "朵");
            UOMDic.Add("FRM", "架");
            UOMDic.Add("G/L", "G/L");
            UOMDic.Add("GAU", "GAU");
            UOMDic.Add("GRO", "GRO");
            UOMDic.Add("GRP", "组");
            UOMDic.Add("GW", "GAI");
            UOMDic.Add("HSH", "刀");
            UOMDic.Add("HUN", "百件");
            UOMDic.Add("KAN", "罐");
            UOMDic.Add("KAR", "箱");
            UOMDic.Add("KE", "颗");
            UOMDic.Add("KGW", "KAI");
            UOMDic.Add("KI", "板箱");
            UOMDic.Add("KWK", "KIK");
            UOMDic.Add("LE", "AU");
            UOMDic.Add("MLW", "MLI");
            UOMDic.Add("PAA", "双");
            UOMDic.Add("PAE", "对");
            UOMDic.Add("PAK", "包");
            UOMDic.Add("PAL", "托盘");
            UOMDic.Add("PAR", "副");
            UOMDic.Add("PCE", "张");
            UOMDic.Add("PDA", "PDA");
            UOMDic.Add("POT", "盆");
            UOMDic.Add("PRS", "人");
            UOMDic.Add("REM", "令");
            UOMDic.Add("ROL", "卷");
            UOMDic.Add("SET", "台");
            UOMDic.Add("SHP", "艘");
            UOMDic.Add("SHR", "份");
            UOMDic.Add("ST", "项");
            UOMDic.Add("STR", "串");
            UOMDic.Add("TH", "千件");
            UOMDic.Add("TRE", "棵");
            UOMDic.Add("VAL", "VAL");
            UOMDic.Add("VEH", "辆");
            UOMDic.Add("WID", "幅");
        }
        public static void loadConfigPM()
        {
            SysConfig.ReaderComPort = ConfigurationManager.AppSettings["ReaderComPort"];
            SysConfig.ReaderPower = ConfigurationManager.AppSettings["ReaderPower"];

            //SAP相关配置
            SysConfig.AppServerHost = ConfigurationManager.AppSettings["AppServerHost"];
            SysConfig.SystemNumber = ConfigurationManager.AppSettings["SystemNumber"];
            SysConfig.User = ConfigurationManager.AppSettings["User"];
            SysConfig.Password = ConfigurationManager.AppSettings["Password"];
            SysConfig.Client = ConfigurationManager.AppSettings["Client"];
            SysConfig.Language = ConfigurationManager.AppSettings["Language"];
            SysConfig.PoolSize = ConfigurationManager.AppSettings["PoolSize"];
            SysConfig.PeakConnectionsLimit = ConfigurationManager.AppSettings["PeakConnectionsLimit"];
            SysConfig.IdleTimeout = ConfigurationManager.AppSettings["IdleTimeout"];

        }

        public static void loadConfig()
        {
            SysConfig.ReaderIp = ConfigurationManager.AppSettings["ReaderIp"];
            SysConfig.ReaderConfig.SearchMode = int.Parse(ConfigurationManager.AppSettings["SearchMode"]);
            SysConfig.ReaderConfig.Session = ushort.Parse(ConfigurationManager.AppSettings["Session"]);
            SysConfig.ReaderConfig.UseAntenna1 = int.Parse(ConfigurationManager.AppSettings["UseAntenna1"]) == 0 ? false : true;
            SysConfig.ReaderConfig.UseAntenna2 = int.Parse(ConfigurationManager.AppSettings["UseAntenna2"]) == 0 ? false : true;
            SysConfig.ReaderConfig.UseAntenna3 = int.Parse(ConfigurationManager.AppSettings["UseAntenna3"]) == 0 ? false : true;
            SysConfig.ReaderConfig.UseAntenna4 = int.Parse(ConfigurationManager.AppSettings["UseAntenna4"]) == 0 ? false : true;
            SysConfig.ReaderConfig.AntennaPower1 = double.Parse(ConfigurationManager.AppSettings["AntennaPower1"]);
            SysConfig.ReaderConfig.AntennaPower2 = double.Parse(ConfigurationManager.AppSettings["AntennaPower2"]);
            SysConfig.ReaderConfig.AntennaPower3 = double.Parse(ConfigurationManager.AppSettings["AntennaPower3"]);
            SysConfig.ReaderConfig.AntennaPower4 = double.Parse(ConfigurationManager.AppSettings["AntennaPower4"]);

            SysConfig.DelayTime = ConfigurationManager.AppSettings["DelayTime"] == null ? 700 : int.Parse(ConfigurationManager.AppSettings["DelayTime"]);

            //SAP相关配置
            SysConfig.AppServerHost = ConfigurationManager.AppSettings["AppServerHost"];
            SysConfig.SystemNumber = ConfigurationManager.AppSettings["SystemNumber"];
            SysConfig.User = ConfigurationManager.AppSettings["User"];
            SysConfig.Password = ConfigurationManager.AppSettings["Password"];
            SysConfig.Client = ConfigurationManager.AppSettings["Client"];
            SysConfig.Language = ConfigurationManager.AppSettings["Language"];
            SysConfig.PoolSize = ConfigurationManager.AppSettings["PoolSize"];
            SysConfig.PeakConnectionsLimit = ConfigurationManager.AppSettings["PeakConnectionsLimit"];
            SysConfig.IdleTimeout = ConfigurationManager.AppSettings["IdleTimeout"];

            //通道机硬件设备相关配置
            SysConfig.Port = ConfigurationManager.AppSettings["Port"];
            SysConfig.ScannerPort_1 = ConfigurationManager.AppSettings["ScannerPort_1"];
            SysConfig.ScannerPort_2 = ConfigurationManager.AppSettings["ScannerPort_2"];

        }
        public static void Load()
        {
            LGNUM = ConfigurationManager.AppSettings["LGNUM"];
            RssiLimit = double.Parse(ConfigurationManager.AppSettings["RssiLimit"]);
            DelayTime = int.Parse(ConfigurationManager.AppSettings["DelayTime"]);
            DelayTime2 = int.Parse(ConfigurationManager.AppSettings["DelayTime2"]);
            Port = ConfigurationManager.AppSettings["Port"];
            ScannerPort_1 = ConfigurationManager.AppSettings["ScannerPort_1"];
            ScannerPort_2 = ConfigurationManager.AppSettings["ScannerPort_2"];
            ReaderIp = ConfigurationManager.AppSettings["ReaderIp"];
            DeviceNO = ConfigurationManager.AppSettings["DeviceNO"] == null ? string.Empty : ConfigurationManager.AppSettings["DeviceNO"];
            RunningModel = (RunMode)Enum.Parse(typeof(RunMode), ConfigurationManager.AppSettings["RunMode"]);
            IsHub = int.Parse(ConfigurationManager.AppSettings["IsHub"]);
            try
            {
                IsTest = int.Parse(ConfigurationManager.AppSettings["IsTest"]) == 1 ? true : false;
            }
            catch
            {
            }

            DBUrl = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            ReaderConfig.SearchMode = int.Parse(ConfigurationManager.AppSettings["SearchMode"]);
            ReaderConfig.Session = ushort.Parse(ConfigurationManager.AppSettings["Session"]);
            ReaderConfig.UseAntenna1 = int.Parse(ConfigurationManager.AppSettings["UseAntenna1"]) == 0 ? false : true;
            ReaderConfig.UseAntenna2 = int.Parse(ConfigurationManager.AppSettings["UseAntenna2"]) == 0 ? false : true;
            ReaderConfig.UseAntenna3 = int.Parse(ConfigurationManager.AppSettings["UseAntenna3"]) == 0 ? false : true;
            ReaderConfig.UseAntenna4 = int.Parse(ConfigurationManager.AppSettings["UseAntenna4"]) == 0 ? false : true;
            ReaderConfig.UseAntenna5 = int.Parse(ConfigurationManager.AppSettings["UseAntenna5"]) == 0 ? false : true;
            ReaderConfig.UseAntenna6 = int.Parse(ConfigurationManager.AppSettings["UseAntenna6"]) == 0 ? false : true;
            ReaderConfig.UseAntenna7 = int.Parse(ConfigurationManager.AppSettings["UseAntenna7"]) == 0 ? false : true;
            ReaderConfig.UseAntenna8 = int.Parse(ConfigurationManager.AppSettings["UseAntenna8"]) == 0 ? false : true;
            ReaderConfig.AntennaPower1 = double.Parse(ConfigurationManager.AppSettings["AntennaPower1"]);
            ReaderConfig.AntennaPower2 = double.Parse(ConfigurationManager.AppSettings["AntennaPower2"]);
            ReaderConfig.AntennaPower3 = double.Parse(ConfigurationManager.AppSettings["AntennaPower3"]);
            ReaderConfig.AntennaPower4 = double.Parse(ConfigurationManager.AppSettings["AntennaPower4"]);
            ReaderConfig.AntennaPower5 = double.Parse(ConfigurationManager.AppSettings["AntennaPower5"]);
            ReaderConfig.AntennaPower6 = double.Parse(ConfigurationManager.AppSettings["AntennaPower6"]);
            ReaderConfig.AntennaPower7 = double.Parse(ConfigurationManager.AppSettings["AntennaPower7"]);
            ReaderConfig.AntennaPower8 = double.Parse(ConfigurationManager.AppSettings["AntennaPower8"]);

            #region 挂装机收货相关代码
            int.TryParse(ConfigurationManager.AppSettings["TrackNum"], out TrackNum);
            int.TryParse(ConfigurationManager.AppSettings["EachTrackNum"], out EachTrackNum);
            #endregion

            //SAP相关配置
            AppServerHost = ConfigurationManager.AppSettings["AppServerHost"];
            SystemNumber = ConfigurationManager.AppSettings["SystemNumber"];
            User = ConfigurationManager.AppSettings["User"];
            Password = ConfigurationManager.AppSettings["Password"];
            Client = ConfigurationManager.AppSettings["Client"];
            Language = ConfigurationManager.AppSettings["Language"];
            PoolSize = ConfigurationManager.AppSettings["PoolSize"];
            PeakConnectionsLimit = ConfigurationManager.AppSettings["PeakConnectionsLimit"];
            IdleTimeout = ConfigurationManager.AppSettings["IdleTimeout"];
            string islog = LocalDataService.GetSysInfoFieldValue("LOG");
            if(!string.IsNullOrEmpty(islog))
            {
                IsLog = bool.Parse(islog);
            }
            //打印相关配置
            PrinterName = ConfigurationManager.AppSettings["PrinterName"];
            FileStream fs = new FileStream("Output.prn", FileMode.Open);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
            RightPrintTemplate = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            fs = new FileStream("ErrorOutput.prn", FileMode.Open);
            sr = new StreamReader(fs, System.Text.Encoding.UTF8);
            ErrorPrintTemplate = sr.ReadToEnd();
            sr.Close();
            fs.Close();
        }

        /// <summary>
        /// 设置配置文件的值
        /// </summary>
        /// <param name="AppKey"></param>
        /// <param name="AppValue"></param>
        public static void SetConfigValue(string AppKey, string AppValue)
        {
            XmlDocument xDoc = new XmlDocument();
            //获取可执行文件的路径和名称
            xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");

            XmlNode xNode;
            XmlElement xElem1;
            xNode = xDoc.SelectSingleNode("//appSettings");
            xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + AppKey + "']");
            if (xElem1 != null) xElem1.SetAttribute("value", AppValue);
            else
            {
                xElem1 = xDoc.CreateElement("add");
                xElem1.SetAttribute("key", AppKey);
                xElem1.SetAttribute("value", AppValue);
                xNode.AppendChild(xElem1);
            }

            xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");
        }

        /// <summary>
        /// 获取当前局域网IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string sClientIp = ConfigurationManager.AppSettings["ReaderIp"].ToString();

            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    string sAddressIP = _IPAddress.ToString();//当前IP

                    if (!string.IsNullOrEmpty(sClientIp))
                    {
                        string[] IPstring = sClientIp.Split('.');

                        if (sAddressIP.Contains(IPstring[0] + "." + IPstring[1] + "." + IPstring[2]))
                        {
                            continue;
                        }
                    }
                    AddressIP = sAddressIP;
                    break;
                }
            }
            return AddressIP;
        }
    }

    public class YZConfig
    {
        public static string mIp;
        public static double mPower;
        public static string mCom;

        public static void loadConfig()
        {

        }
    }
}
