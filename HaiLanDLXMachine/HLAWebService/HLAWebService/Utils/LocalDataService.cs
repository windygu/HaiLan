using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HLAWebService.Utils
{
    public class LocalDataService
    {
        /// <summary>
        /// 保存发运箱和门店关系表数据
        /// </summary>
        /// <param name="HU"></param>
        /// <param name="LOUCENG"></param>
        /// <param name="SHIP_DATE"></param>
        /// <param name="SHULIANG"></param>
        /// <param name="STORE_ID"></param>
        /// <param name="XIANGXING"></param>
        /// <returns></returns>
        public static bool SaveBoxPartnerMapInfo(string HU, string LOUCENG, DateTime? SHIP_DATE, float SHULIANG, string STORE_ID, string XIANGXING)
        {
            try
            {
                string sql = string.Format("P_BoxPartnerMap_Save ");
                SqlParameter p1 = DBHelper.CreateParameter("@HU", HU);
                SqlParameter p2 = DBHelper.CreateParameter("@PARTNER", STORE_ID);
                SqlParameter p3 = DBHelper.CreateParameter("@LOUCENG", LOUCENG);
                SqlParameter p4 = DBHelper.CreateParameter("@SHIP_DATE", SHIP_DATE);
                SqlParameter p5 = DBHelper.CreateParameter("@PACKMAT", XIANGXING);
                SqlParameter p6 = DBHelper.CreateParameter("@QUAN", SHULIANG);
                int result = int.Parse(DBHelper.GetValue(sql, true, p1, p2, p3, p4, p5, p6).ToString());

                return true;
            }
            catch (Exception ex)
            {
                //LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return false;
            }
        }
    }
}