using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HLACommon
{
    public class CConfig
    {
        //硬件---------------------------------------------
        //读写器
        public static string mReaderIP;
        public static double mReaderPower;
        public static string mReaderComPort;
        //PLC
        public static string mPLCComPort;
        //扫描枪
        public static string mScannerPort_1;
        public static string mScannerPort_2;
        //读写器读取时间
        public static int mDelayTime = 700;
        //SAP相关配置
        public static string mAppServerHost = "172.18.200.14";
        public static string mSystemNumber = "00";
        public static string mUser = "093482";
        public static string mPassword = "sunrain";
        public static string mClient = "300";
        public static string mLanguage = "ZH";
        public static string mPoolSize = "5";
        public static string mPeakConnectionsLimit = "10";
        public static string mIdleTimeout = "60";
        //SAP组登录
        public static string mUseGroupLogon = "1";
        public static string mLogonGroup = "EWM_PRD";
        public static string mSystemID = "EWP";
        public static string mMessageServerHost = "172.18.200.81";
        public static string mMessageServerService = "3600";

        //仓库编号
        public static string mLGNUM = "HL01";
        public static string DeviceNO = "";
        public static string mDBUrl;

        public static DeviceTable mDeviceInfo;

        public static void loadConfig()
        {

        }
    }
}
