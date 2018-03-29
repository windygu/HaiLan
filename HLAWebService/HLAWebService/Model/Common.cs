using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using SAP.Middleware.Connector;
using HLAWebService.Utils;

namespace HLAWebService.Model
{
    public class CPDAZlkUploadData
    {
        public string loucheng;
        public string gonghao;
        public List<string> epcs;
    }

    public class CPDAZlkReDataSKU
    {
        public string gongHao;
        public string pin;
        public string se;
        public string gui;
        public string count;
        public CPDAZlkReDataSKU() { }
        public CPDAZlkReDataSKU(string gh,string p,string s,string g,string c)
        {
            gongHao = gh;
            pin = p;
            se = s;
            gui = g;
            count = c;
        }
    }
    public class CPDAZlkReData
    {
        public string status;
        public string msg;

        public List<CPDAZlkReDataSKU> skuData = new List<CPDAZlkReDataSKU>();
    }

    public class CHelp
    {
        public static void LoadConfig()
        {
            SysConfig.LGNUM = ConfigurationManager.AppSettings["LGNUM"];
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

            SysConfig.IsTest = ConfigurationManager.AppSettings["IsTest"] == "1";
        }

        public static void initSAP()
        {
            if (SysConfig.LGNUM == "HL01")
            {
                SysConfig.rfcParams.Add(RfcConfigParameters.Name, "HLA");
            }
            else if (SysConfig.LGNUM == "ET01")
            {
                SysConfig.rfcParams.Add(RfcConfigParameters.Name, "EHT");
            }

            SysConfig.rfcParams.Add(RfcConfigParameters.AppServerHost, SysConfig.AppServerHost);   //SAP主机IP
            SysConfig.rfcParams.Add(RfcConfigParameters.SystemNumber, SysConfig.SystemNumber);  //SAP实例
            SysConfig.rfcParams.Add(RfcConfigParameters.User, SysConfig.User);  //用户名
            SysConfig.rfcParams.Add(RfcConfigParameters.Password, SysConfig.Password);  //密码
            SysConfig.rfcParams.Add(RfcConfigParameters.Client, SysConfig.Client);  // Client
            SysConfig.rfcParams.Add(RfcConfigParameters.Language, SysConfig.Language);  //登陆语言
            SysConfig.rfcParams.Add(RfcConfigParameters.PoolSize, SysConfig.PoolSize);
            SysConfig.rfcParams.Add(RfcConfigParameters.PeakConnectionsLimit, SysConfig.PeakConnectionsLimit);
            SysConfig.rfcParams.Add(RfcConfigParameters.IdleTimeout, SysConfig.IdleTimeout);

        }

        public static void uploadPDAZlkData(string loucheng
                , string gonghao
                , List<string> epcs, out string status_o, out string msg_o)
        {

            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(SysConfig.rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RF_230");
                myfun.SetValue("LGNUM", SysConfig.LGNUM);//仓库编号
                myfun.SetValue("LOUCENG", loucheng);//楼层
                myfun.SetValue("PSNCODE", gonghao);//工号

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_EPC");
                if (epcs != null && epcs.Count > 0)
                {
                    foreach (string epc in epcs)
                    {
                        import = rfcrep.GetStructureMetadata("ZSRFID020STR").CreateStructure();
                        import.SetValue("EPC_SER", epc);
                        IrfTable.Insert(import);
                    }
                }

                myfun.Invoke(dest);

                status_o = myfun.GetString("STATUS"); //执行状态正确‘S’错误‘E’）
                msg_o = myfun.GetString("MSG");

                RfcSessionManager.EndContext(dest);

            }
            catch (Exception ex)
            {
                LogHelp.LogError(ex);

                status_o = "E";
                msg_o = ex.Message;
            }

        }


    }
    public class SysConfig
    {
        public static RfcConfigParameters rfcParams = new RfcConfigParameters();

        public static bool IsTest = false;
        //仓库编号
        public static string LGNUM = "HL01";
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
    }


}