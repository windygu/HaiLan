using HLACommonLib;
using HLACommonLib.Model.YK;
using HLAYKChannelMachine.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HLAYKChannelMachine.Utils
{
    public class YKBoxSqliteService
    {
        public static void InsertUploadData(SqliteUploadDataInfo ud)
        {
            string sql = string.Format(@"INSERT INTO UploadData(Guid,Data,IsUpload,CreateTime) VALUES('{0}','{1}',0,'{2}')",
                ud.Guid, JsonConvert.SerializeObject(ud.Data), ud.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            int result = SqliteDBHelp.ExecuteSql(sql);
            if (result <= 0)
            {
                LogHelper.WriteLine(JsonConvert.SerializeObject(ud.Data));
            }
        }

        public static List<SqliteUploadDataInfo> GetUnUploadDataList()
        {
            string sql = string.Format("SELECT Guid,Data,IsUpload,CreateTime FROM UploadData WHERE IsUpload = 0");
            DataTable dt = SqliteDBHelp.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<SqliteUploadDataInfo> result = new List<SqliteUploadDataInfo>();
                foreach (DataRow row in dt.Rows)
                {
                    SqliteUploadDataInfo ud = new SqliteUploadDataInfo();
                    ud.Guid = row["Guid"].ToString();
                    ud.Data = JsonConvert.DeserializeObject<YKBoxInfo>(row["Data"].ToString());
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
            string sql = string.Format("DELETE FROM UploadData WHERE Guid='{0}'", guid);
            int result = SqliteDBHelp.ExecuteSql(sql);
            if (result > 0)
                return true;
            else
                return false;
        }
    }
}
