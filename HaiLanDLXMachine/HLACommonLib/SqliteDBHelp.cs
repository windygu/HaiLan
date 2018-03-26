using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HLACommonLib
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
}
