using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SAP.Middleware.Connector;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Configuration;
using System.Security.Cryptography;
using System.Net;
using System.Xml;

namespace HLACommon
{
    public class SAPDataService
    {
        public static RfcConfigParameters rfcParams = new RfcConfigParameters();
        public static void ReadSAPGroupConfig()
        {
            string UseGroupLogonStr = ConfigurationManager.AppSettings["UseGroupLogon"];
            if (!string.IsNullOrEmpty(UseGroupLogonStr))
            {
                CConfig.mUseGroupLogon = UseGroupLogonStr;
            }

            string LogonGroupStr = ConfigurationManager.AppSettings["LogonGroup"];
            if (!string.IsNullOrEmpty(LogonGroupStr))
            {
                CConfig.mLogonGroup = LogonGroupStr;
            }

            string SystemIDStr = ConfigurationManager.AppSettings["SystemID"];
            if (!string.IsNullOrEmpty(SystemIDStr))
            {
                CConfig.mSystemID = SystemIDStr;
            }

            string MessageServerHostStr = ConfigurationManager.AppSettings["MessageServerHost"];
            if (!string.IsNullOrEmpty(MessageServerHostStr))
            {
                CConfig.mMessageServerHost = MessageServerHostStr;
            }

            string MessageServerServiceStr = ConfigurationManager.AppSettings["MessageServerService"];
            if (!string.IsNullOrEmpty(MessageServerServiceStr))
            {
                CConfig.mMessageServerService = MessageServerServiceStr;
            }
        }
        public static void Init()
        {
            ReadSAPGroupConfig();

            if (CConfig.mLGNUM == "HL01")
            {
                rfcParams.Add(RfcConfigParameters.Name, "HLA");
            }
            else if (CConfig.mLGNUM == "ET01")
            {
                rfcParams.Add(RfcConfigParameters.Name, "EHT");
                //爱居兔不用组登陆
                //CConfig.UseGroupLogon = "0";
            }
            if (CConfig.mUseGroupLogon == "1")
            {
                if (CConfig.mLGNUM == "ET01")
                {
                    rfcParams.Add(RfcConfigParameters.LogonGroup, "SCP_PRD");
                    rfcParams.Add(RfcConfigParameters.SystemID, "SCP");
                    rfcParams.Add(RfcConfigParameters.MessageServerHost, "172.18.200.61");
                    rfcParams.Add(RfcConfigParameters.MessageServerService, "3600");

                    rfcParams.Add(RfcConfigParameters.User, CConfig.mUser);  //用户名
                    rfcParams.Add(RfcConfigParameters.Password, CConfig.mPassword);  //密码
                    rfcParams.Add(RfcConfigParameters.Client, CConfig.mClient);  // Client
                    rfcParams.Add(RfcConfigParameters.Language, CConfig.mLanguage);  //登陆语言
                }
                else
                {
                    rfcParams.Add(RfcConfigParameters.LogonGroup, CConfig.mLogonGroup);
                    rfcParams.Add(RfcConfigParameters.SystemID, CConfig.mSystemID);
                    rfcParams.Add(RfcConfigParameters.MessageServerHost, CConfig.mMessageServerHost);
                    rfcParams.Add(RfcConfigParameters.MessageServerService, CConfig.mMessageServerService);

                    rfcParams.Add(RfcConfigParameters.User, CConfig.mUser);  //用户名
                    rfcParams.Add(RfcConfigParameters.Password, CConfig.mPassword);  //密码
                    rfcParams.Add(RfcConfigParameters.Client, CConfig.mClient);  // Client
                    rfcParams.Add(RfcConfigParameters.Language, CConfig.mLanguage);  //登陆语言
                }
            }
            else
            {
                rfcParams.Add(RfcConfigParameters.AppServerHost, CConfig.mAppServerHost);   //SAP主机IP
                rfcParams.Add(RfcConfigParameters.SystemNumber, CConfig.mSystemNumber);  //SAP实例
                rfcParams.Add(RfcConfigParameters.User, CConfig.mUser);  //用户名
                rfcParams.Add(RfcConfigParameters.Password, CConfig.mPassword);  //密码
                rfcParams.Add(RfcConfigParameters.Client, CConfig.mClient);  // Client
                rfcParams.Add(RfcConfigParameters.Language, CConfig.mLanguage);  //登陆语言
                rfcParams.Add(RfcConfigParameters.PoolSize, CConfig.mPoolSize);
                rfcParams.Add(RfcConfigParameters.PeakConnectionsLimit, CConfig.mPeakConnectionsLimit);
                rfcParams.Add(RfcConfigParameters.IdleTimeout, CConfig.mIdleTimeout);
            }
        }
        public static string getZiDuan(IRfcTable t, string ziduan)
        {
            try
            {
                return t.GetString(ziduan).Trim();
            }
            catch (Exception)
            {

            }

            return "";
        }
        public static List<string> getIngnoreEpcs(string date = "")
        {
            List<string> re = new List<string>();

            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RF_5000");

                myfun.SetValue("IV_LGNUM", CConfig.mLGNUM);
                if (!string.IsNullOrEmpty(date))
                    myfun.SetValue("IV_DATE", date);

                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_DATA");
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;
                    string epc = getZiDuan(IrfTable, "EPC");
                    if (!string.IsNullOrEmpty(epc))
                        re.Add(epc);
                }

                string result = myfun.GetString("EV_STATUS");
                string sapMsg = myfun.GetString("EV_MSG");
                RfcSessionManager.EndContext(dest);

            }
            catch (Exception)
            {

            }
            return re;
        }

    }
    public class HttpWebResponseUtility
    {
        public static int connectTimeout = 30000;

        public static string StrToUrlEncode(string sdata)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(sdata);
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }
            return sb.ToString();
        }


        public static string USerMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            for (int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString("x2");
            }
            return pwd;

        }
        public static string Submit(string postData, string serviceType, CPPInfo ppinfo)
        {
            string requestTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            string str = "bizData=" + postData + "&msgId=424&msgType=sync&notifyUrl=&partnerId=" + ppinfo.Inerfae_key + "&partnerKey=" + ppinfo.Secret + "&serviceType=" + serviceType + "&serviceVersion=1.0";
            string qm = USerMd5(str);
            string binzdata = StrToUrlEncode(postData);
            string str2 = "bizData=" + binzdata + "&serviceType=" + serviceType + "&msgId=424&msgType=sync&partnerId=" + ppinfo.Inerfae_key + "&partnerKey=" + ppinfo.Secret + "&serviceVersion=1.0&notifyUrl=&sign=" + qm + "&";


            byte[] bytePostData = Encoding.UTF8.GetBytes(str2);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(ppinfo.Interface_url);
            req.Method = "POST";

            req.Timeout = connectTimeout;
            req.ContentType = "application/x-www-form-urlencoded"; ;
            req.ContentLength = bytePostData.Length;
            try
            {
                using (System.IO.Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bytePostData, 0, bytePostData.Length);
                }
                using (WebResponse wr = req.GetResponse())
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(wr.GetResponseStream(), System.Text.Encoding.UTF8);

                    string responseFromServer = reader.ReadToEnd();
                    return responseFromServer;  
                }
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(ex);
            }

            return "";
        }

    }
}
