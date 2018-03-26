using HLACommonLib.Model;
using HLACommonLib.Model.ENUM;
using HLACommonLib.Model.RECEIVE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OSharp.Utility.Extensions;

namespace HLACommonLib.DAO
{
    public class ReceiveService
    {
        /// <summary>
        /// 获取历史EPC数据，只获取箱码和EPC
        /// 此方法仅用于检测大通道机交货系统中，箱码重复使用，商品已扫描，重投三种情况
        /// </summary>
        /// <param name="epcList"></param>
        /// <returns></returns>
        public static List<EpcDetail> GetBeforeEpcDetailByEpcList(string docno, List<string> epcList, ReceiveType type)
        {
            List<EpcDetail> result = new List<EpcDetail>();
            string epcparams = "";
            foreach(string item in epcList)
            {
                epcparams += string.Format("'{0}',", item);
            }
            if (epcparams.EndsWith(","))
                epcparams = epcparams.Substring(0, epcparams.Length - 1);
            string sql;
            if (type== ReceiveType.交接单收货)
            {
                sql = string.Format(@"SELECT EPC_SER,HU FROM dbo.epcdetail_dema WHERE EPC_SER IN ({0}) AND Result='S' AND DOCNO = '{1}'",
                epcparams, docno);
            }
            else
            {
                sql = string.Format(@"SELECT EPC_SER,HU FROM dbo.epcdetail WHERE EPC_SER IN ({0}) AND Result='S'",
                epcparams);
            }
            

            DataTable dt = DBHelper.GetTable(sql, false);
            //MessageBox.Show(JsonConvert.SerializeObject(dt));
            if(dt?.Rows?.Count>0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    EpcDetail epc = new EpcDetail();
                    epc.HU = row["HU"].CastTo("");
                    epc.EPC_SER = row["EPC_SER"].CastTo("");
                    result.Add(epc);
                }
            }
            return result;
        }
        public static string GetSaveDataSapResult(string guid)
        {
            try
            {
                string sql = string.Format(@"select top 1 SapResult from dbo.ReceiveUploadData where Guid='{0}' order by CreateTime desc",guid);
                object obj = DBHelper.GetValue(sql, false);
                if(obj == null)
                {
                    return "";
                }
                else
                {
                    return obj.ToString();
                }
            }
            catch(Exception)
            {
            }

            return "";
        }
        public static bool SaveUploadData(ReceiveUploadData param)
        {
            bool result = false;
            SqlParameter p1 = DBHelper.CreateParameter("@Device", param.Device);
            SqlParameter p2 = DBHelper.CreateParameter("@CreateTime", param.CreateTime);
            SqlParameter p3 = DBHelper.CreateParameter("@Data", param.Data);
            SqlParameter p4 = DBHelper.CreateParameter("@Guid", param.Guid);
            SqlParameter p5 = DBHelper.CreateParameter("@IsUpload", param.IsUpload);
            SqlParameter p6 = DBHelper.CreateParameter("@SapResult", param.SapResult);
            SqlParameter p7 = DBHelper.CreateParameter("@SapStatus", param.SapStatus);
            SqlParameter p8 = DBHelper.CreateParameter("@Hu", param.Hu);
            string sql = @"INSERT INTO dbo.ReceiveUploadData
        ( Guid ,
          Data ,
          IsUpload ,
          CreateTime ,
          Device ,
          SapStatus ,
          SapResult ,
          Hu
        )
VALUES  ( @Guid ,
          @Data ,
          @IsUpload ,
          @CreateTime ,
          @Device , 
          @SapStatus , 
          @SapResult , 
          @Hu 
        )";
            result = DBHelper.ExecuteSql(sql, false, p1, p2, p3, p4, p5, p6, p7, p8) > 0;
            return result;
        }
    }
}
