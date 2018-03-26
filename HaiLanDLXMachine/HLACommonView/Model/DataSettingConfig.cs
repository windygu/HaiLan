using HLACommonView.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonView.Model
{
    public static class DataSettingConfig
    {
        static DataSettingConfig()
        {
            //DataSetting.Default.Reset();
        }

        public static bool UseFormal
        {
            get
            {
                return DataSetting.Default.UseFormalEnvironmemt;
                //return false;
            }
        }

        public static string DBAddress
        {
            get
            {
                return UseFormal ? DataSetting.Default.DBAddress : DataSetting.Default.TestDBAddress;
            }
        }

        public static string DBName
        {
            get
            {
                return UseFormal ? DataSetting.Default.DBName : DataSetting.Default.TestDBName;
            }
        }

        public static string DBUser
        {
            get
            {
                return UseFormal ? DataSetting.Default.DBUser : DataSetting.Default.TestDBUser;
            }
        }

        public static string DBPassword
        {
            get
            {
                return UseFormal ? DataSetting.Default.DBPassword : DataSetting.Default.TestDBPassword;
            }
        }

        public static string SAPServerAddress
        {
            get
            {
                return UseFormal ? DataSetting.Default.FormalSAPServerAddress : DataSetting.Default.TestSAPServerAddress;
            }
        }

        public static string SAPSystemNum
        {
            get
            {
                return UseFormal ? DataSetting.Default.FormalSystemNum : DataSetting.Default.TestSystemNum;
            }
        }

        public static string SAPIdleTimeout
        {
            get
            {
                return UseFormal ? DataSetting.Default.FormalIdleTimeout : DataSetting.Default.TestIdleTimeout;
            }
        }

        public static string SAPUser
        {
            get
            {
                return UseFormal ? DataSetting.Default.FormalSAPUser : DataSetting.Default.TestSAPUser;
            }
        }

        public static string SAPPassword
        {
            get
            {
                return UseFormal ? DataSetting.Default.FormalSAPPassword : DataSetting.Default.TestSAPPassword;
            }
        }

        public static string SAPClient
        {
            get
            {
                return UseFormal ? DataSetting.Default.FormalClient : DataSetting.Default.TestClient;
            }
        }

        public static string SAPLanguage
        {
            get
            {
                return UseFormal ? DataSetting.Default.FormalLanguage : DataSetting.Default.TestLanguage;
            }
        }

        public static string SAPPeakConnectionsLimit
        {
            get
            {
                return UseFormal ? DataSetting.Default.FormalPeakConnectionsLimit : DataSetting.Default.TestPeakConnectionsLimit;
            }
        }

        public static string SAPPollSize
        {
            get
            {
                return UseFormal ? DataSetting.Default.FormalPollSize : DataSetting.Default.TestPollSize;
            }
        }

        public static string DBUrl
        {
            get
            {
                return string.Format("Data Source={0};Initial Catalog={1};User ID={2};password={3}"
                , DBAddress, DBName, DBUser, DBPassword);
            }
        }


    }
}
