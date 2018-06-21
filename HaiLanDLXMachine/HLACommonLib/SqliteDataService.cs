using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using HLACommonLib.Model;
using HLACommonLib.Model.PK;
using Newtonsoft.Json;

namespace HLACommonLib
{
    public class SqliteDataService
    {
        public static void updateMsgToSqlite(string guid,string msg)
        {
            string sql = string.Format("update UploadData set MSG='{0}',IsUpload=1 WHERE GUID='{1}'", msg, guid);
            SqliteDBHelp.ExecuteSql(sql);
        }
        public static void saveToSqlite(CUploadData d)
        {
            string sql = string.Format("INSERT INTO UploadData(Guid,Data,IsUpload,CreateTime) VALUES('{0}','{1}',0,'{2}')", d.Guid, JsonConvert.SerializeObject(d.Data), d.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            int result = SqliteDBHelp.ExecuteSql(sql);
            if (result <= 0)
            {
                LogHelper.WriteLine(JsonConvert.SerializeObject(d.Data));
            }
        }
        public static int GetUnUploadCountFromSqlite()
        {
            try
            {
                string sql = string.Format("SELECT count(*) FROM UploadData where IsUpload=0");
                object re = SqliteDBHelp.GetValue(sql);
                if (re != null)
                {
                    int reint = 0;
                    int.TryParse(re.ToString(), out reint);
                    return reint;
                }
            }
            catch(Exception e)
            {
                Log4netHelper.LogError(e);
            }

            return 0;
        }
        public static int GetExpUploadCount()
        {
            int re = 0;
            string sql = string.Format("SELECT count(*) FROM UploadData where IsUpload = 1 order by CreateTime");
            int.TryParse(SqliteDBHelp.GetValue(sql).ToString(), out re);
            return re;
        }
        public static List<CUploadData> GetExpUploadFromSqlite<T>()
        {
            List<CUploadData> result = new List<CUploadData>();

            string sql = string.Format("SELECT Guid,Data,IsUpload,CreateTime,MSG FROM UploadData where IsUpload = 1 order by CreateTime");
            DataTable dt = SqliteDBHelp.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    CUploadData ud = new CUploadData();
                    ud.Guid = row["Guid"].ToString();
                    ud.Data = JsonConvert.DeserializeObject<T>(row["Data"].ToString());
                    ud.IsUpload = uint.Parse(row["IsUpload"].ToString());
                    ud.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                    ud.MSG = row["MSG"].ToString();
                    result.Add(ud);
                }
                return result;
            }
            return result;
        }
        public static List<CUploadData> GetAllUploadFromSqlite<T>()
        {
            List<CUploadData> result = new List<CUploadData>();

            string sql = string.Format("SELECT Guid,Data,IsUpload,CreateTime,MSG FROM UploadData order by CreateTime");
            DataTable dt = SqliteDBHelp.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    CUploadData ud = new CUploadData();
                    ud.Guid = row["Guid"].ToString();
                    ud.Data = JsonConvert.DeserializeObject<T>(row["Data"].ToString());
                    ud.IsUpload = uint.Parse(row["IsUpload"].ToString());
                    ud.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                    ud.MSG = row["MSG"].ToString();
                    result.Add(ud);
                }
                return result;
            }
            return result;
        }
        public static bool delUploadFromSqlite(string guid)
        {
            string sql = string.Format("DELETE FROM UploadData WHERE Guid='{0}'", guid);
            int result = SqliteDBHelp.ExecuteSql(sql);
            if (result > 0)
                return true;
            else
                return false;

        }



        public static void delOldData(int day = 3)
        {
            try
            {
                string sql = string.Format("delete from UploadData where CreateTime < date('now', '-{0} day')", day);
                SqliteDBHelp.ExecuteSql(sql);
            }
            catch (Exception)
            {

            }
        }








        #region 大通道机交货单
        public static void InsertUploadData(UploadData ud)
        {
            string sql = string.Format("INSERT INTO UploadData(Guid,Data,IsUpload,CreateTime) VALUES('{0}','{1}',0,'{2}')", ud.Guid, JsonConvert.SerializeObject(ud.Data), ud.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            int result = SqliteDBHelp.ExecuteSql(sql);
            if (result <= 0)
            {
                LogHelper.WriteLine(JsonConvert.SerializeObject(ud.Data));
            }
        }

        public static List<UploadData> GetUnUploadDataList()
        {
            //string sql = string.Format("DELETE FROM UploadData WHERE IsUpload=1;SELECT Guid,Data,IsUpload,CreateTime FROM UploadData WHERE IsUpload = 0");
            string sql = string.Format("SELECT Guid,Data,IsUpload,CreateTime FROM UploadData");
            DataTable dt = SqliteDBHelp.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<UploadData> result = new List<UploadData>();
                foreach (DataRow row in dt.Rows)
                {
                    UploadData ud = new UploadData();
                    ud.Guid = row["Guid"].ToString();
                    ud.Data = JsonConvert.DeserializeObject<ResultDataInfo>(row["Data"].ToString());
                    ud.IsUpload = uint.Parse(row["IsUpload"].ToString());
                    ud.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                    result.Add(ud);
                }
                return result;
            }
            return null;
        }

        public static bool SetUploaded(string guid)
        {
            //string sql = string.Format("UPDATE UploadData SET IsUpload=1 WHERE Guid='{0}'", guid);
            string sql = string.Format("DELETE FROM UploadData WHERE Guid='{0}'", guid);
            int result = SqliteDBHelp.ExecuteSql(sql);
            if (result > 0)
                return true;
            else
                return false;
        }

        public static bool CreateHuListTable()
        {
            string sql = string.Format("SELECT COUNT(*) FROM sqlite_master where type='table' and name='HuList'");
            if(Convert.ToInt32(SqliteDBHelp.GetValue(sql)) == 0)
            {
                //创建HuList表
                sql = "create table HuList(Hu VARCHAR primary key,QTY INT,Result VARCHAR,Timestamp DATETIME)";
                SqliteDBHelp.ExecuteSql(sql);
            }
            return true;
        }

        public static void SaveHu(HuInfo hu)
        {
            string sql = string.Format(@" delete from HuList where Hu='{0}';
insert into HuList(Hu,QTY,Result,Timestamp) values('{0}',{1},'{2}','{3}')",
hu.HU, hu.QTY, hu.Result, hu.Timestamp);
            SqliteDBHelp.ExecuteSql(sql);
        }

        public static List<HuInfo> GetAllHuInfo()
        {
            List<HuInfo> result = null;
            string sql = "select HU,QTY,Result,Timestamp from HuList";
            DataTable dt= SqliteDBHelp.GetTable(sql);
            if(dt!=null && dt.Rows.Count>0)
            {
                result = new List<HuInfo>();
                foreach (DataRow row in dt.Rows)
                {
                    HuInfo hu = new HuInfo() {
                        HU = row["HU"].ToString(),
                        QTY = (int)row["QTY"],
                        Result = row["Result"].ToString(),
                        Timestamp = DateTime.Parse(row["Timestamp"].ToString())
                    };
                    if (!result.Exists(i => i.HU == hu.HU))
                        result.Add(hu);
                }
            }
            return result;

        }
        #endregion

        #region 16#分拣复核接口
        public static bool DeleteUploaded(string guid)
        {
            string sql = string.Format("DELETE FROM UploadData WHERE Guid='{0}'", guid);
            return SqliteDBHelp.ExecuteSql(sql) > 0;
        }

        public static List<UploadEbBoxInfo> GetUnUploadEbBox()
        {
            string sql = string.Format("SELECT Guid,Data,IsUpload,CreateTime FROM UploadData");
            DataTable dt = SqliteDBHelp.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<UploadEbBoxInfo> result = new List<UploadEbBoxInfo>();
                foreach (DataRow row in dt.Rows)
                {
                    result.Add((JsonConvert.DeserializeObject<UploadEbBoxInfo>(row["Data"].ToString())));
                }
                return result;
            }
            return null;
        }

        public static void InsertUploadData(UploadEbBoxInfo data)
        {
            string sql = string.Format("INSERT INTO UploadData(Guid,Data,IsUpload,CreateTime) VALUES('{0}','{1}',0,'{2}')", data.Guid, JsonConvert.SerializeObject(data), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            int result = SqliteDBHelp.ExecuteSql(sql);
            if (result <= 0)
            {
                LogHelper.WriteLine(JsonConvert.SerializeObject(data));
            }
        }
        #endregion

        #region 平库大批量发货通道机相关代码
        /// <summary>
        /// 插入待上传sap发运箱信息到本地sqlite
        /// </summary>
        /// <param name="data"></param>
        public static void InsertUploadData(UploadPKBoxInfo data)
        {
            string sql = string.Format("INSERT INTO UploadData(Guid,Data,IsUpload,CreateTime) VALUES('{0}','{1}',0,'{2}')", data.Guid, JsonConvert.SerializeObject(data), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            int result = SqliteDBHelp.ExecuteSql(sql);
            if (result <= 0)
            {
                LogHelper.WriteLine(JsonConvert.SerializeObject(data));
            }
        }
        public static int GetUploadDataCount()
        {
            try
            {
                string sql = string.Format("SELECT COUNT(*) FROM UploadData");
                return Convert.ToInt32(SqliteDBHelp.GetValue(sql));
            }
            catch (Exception)
            {
                return 0;
            }

        }
        /// <summary>
        /// 获取本地sqlite未上传sap发运箱信息列表
        /// </summary>
        /// <returns></returns>
        public static List<UploadPKBoxInfo> GetUnUploadPKBox()
        {
            string sql = string.Format("SELECT Guid,Data,IsUpload,CreateTime FROM UploadData");
            DataTable dt = SqliteDBHelp.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<UploadPKBoxInfo> result = new List<UploadPKBoxInfo>();
                foreach (DataRow row in dt.Rows)
                {
                    result.Add((JsonConvert.DeserializeObject<UploadPKBoxInfo>(row["Data"].ToString())));
                }
                return result;
            }
            return null;
        }
        #endregion

    }
}
