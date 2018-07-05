using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using Newtonsoft.Json;

namespace HLACommon
{
    public class SqliteDBHelp
    {
        private static string _connStr;
        public static string ConnStr
        {
            get
            {
                if (_connStr == null)
                {
                    _connStr = @"Data Source=DB/HLA.sqlite;";
                }

                return _connStr;
            }
        }

        //创建数据库参数
        public static SQLiteParameter CreateParameter(string pName, object pValue)
        {
            SQLiteParameter ps = new SQLiteParameter();
            ps.ParameterName = pName;

            if (pValue == null)
            {
                //数据库空值
                ps.Value = DBNull.Value;
            }
            else
            {
                ps.Value = pValue;
            }

            return ps;
        }

        //通用的增，删， 改
        public static int ExecuteSql(string sql)
        {
            SQLiteConnection conn = new SQLiteConnection(ConnStr);
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            //cmd.Parameters.AddRange(ps);
            conn.Open();
            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            return result;
        }

        //通用的查询
        public static DataTable GetTable(string sql)
        {
            SQLiteConnection conn = new SQLiteConnection(ConnStr);
            SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            da.Dispose();
            conn.Close();

            return dt;
        }

        //返回第一行第一列的值
        public static object GetValue(string sql)
        {
            SQLiteConnection conn = new SQLiteConnection(ConnStr);
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            conn.Open();
            object result = cmd.ExecuteScalar();
            cmd.Dispose();
            conn.Close();

            return result;
        }


        public static object GetScalar(string sql)
        {
            SQLiteConnection conn = new SQLiteConnection(ConnStr);
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            conn.Open();
            object result = cmd.ExecuteScalar();
            cmd.Dispose();
            conn.Close();

            return result;
        }



        /// <summary>
        /// 压缩数据（释放数据库，使数据删除后数据库能减小）
        /// </summary>
        /// <returns></returns>
        public static int CompressData()
        {
            SQLiteConnection conn = new SQLiteConnection(ConnStr);
            SQLiteCommand cmd = new SQLiteCommand("VACUUM", conn);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

            return result;
        }

        private static SQLiteConnection transConn = null;
        public static int BeginTranscation()
        {
            transConn = new SQLiteConnection(ConnStr);
            SQLiteCommand cmd = new SQLiteCommand("BEGIN TRANSACTION;", transConn);

            transConn.Open();
            int result = cmd.ExecuteNonQuery();

            cmd.Dispose();
            return result;
        }

        public static int ExecuteInTranscation(string sql, params SQLiteParameter[] ps)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, transConn);
            cmd.Parameters.AddRange(ps);

            int result = cmd.ExecuteNonQuery();

            cmd.Dispose();
            return result;
        }

        public static object GetValueInTranscation(string sql, params SQLiteParameter[] ps)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, transConn);
            cmd.Parameters.AddRange(ps);

            object result = cmd.ExecuteScalar();
            cmd.Dispose();

            return result;
        }

        public static int UpdateTableInTranscation(string sql, DataTable table)
        {
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, transConn);
            SQLiteCommandBuilder scb = new SQLiteCommandBuilder(dta);
            dta.InsertCommand = scb.GetInsertCommand();
            dta.UpdateCommand = scb.GetUpdateCommand();

            DataSet DS = new DataSet();
            dta.FillSchema(DS, SchemaType.Source, "Temp");//加载表架构 注意
            dta.Fill(DS, "Temp");

            DataTable DT = DS.Tables["Temp"];
            foreach (DataRow dr in table.Rows)
            {
                DataRow _dr = DT.NewRow();
                foreach (DataColumn col in DT.Columns)
                {
                    if (table.Columns.Contains(col.ColumnName))
                        _dr[col.ColumnName] = dr[col.ColumnName];
                }

                DT.Rows.Add(_dr);
            }

            //插入数据
            int result = dta.Update(DT);

            DS.AcceptChanges();
            dta.Dispose();
            DS.Clear();

            return result;
        }

        public static int RollbackTranscation()
        {
            SQLiteCommand cmd = new SQLiteCommand("ROLLBACK TRANSACTION;", transConn);
            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            transConn.Close();
            transConn = null;
            return result;
        }

        public static int CommitTranscation()
        {
            SQLiteCommand cmd = new SQLiteCommand("COMMIT TRANSACTION;", transConn);
            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            transConn.Close();
            transConn = null;
            return result;
        }
    }

    public class SqliteDataService
    {
        public static void updateMsgToSqlite(string guid,string msg)
        {
            string sql = string.Format("update UploadData set MSG='{0}',IsUpload=1 WHERE GUID='{1}'", msg, guid);
            SqliteDBHelp.ExecuteSql(sql);
        }
        public static void saveToSqlite(CUploadData d)
        {
            string sql = string.Format("INSERT INTO UploadData(Guid,Data,IsUpload,CreateTime,HU) VALUES('{0}','{1}',0,'{2}')", d.Guid
                , JsonConvert.SerializeObject(d.Data), d.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), d.HU);
            int result = SqliteDBHelp.ExecuteSql(sql);
            if (result <= 0)
            {
                Log4netHelper.LogInfo(JsonConvert.SerializeObject(d.Data));
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

            string sql = string.Format("SELECT * FROM UploadData where IsUpload = 1 order by CreateTime");
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
                    ud.HU = row["HU"].ToString();
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

    }
}
