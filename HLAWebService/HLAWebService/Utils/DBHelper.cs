using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace HLAWebService.Utils
{
    public static class DBHelper
    {
        private static string _connStr;
        public static string ConnStr
        {
            get
            {
                if (_connStr == null)
                {

                    _connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
                }

                return _connStr;
            }
        }

        //private static SqlConnection conn = new SqlConnection(ConnStr);

        //public static string Ip = conn.DataSource;

        //public static bool Connect()
        //{
        //    if (conn.State == ConnectionState.Closed)
        //        conn.Open();

        //    return true;
        //}

        public static bool Disconnect()
        {
            //if (conn.State != ConnectionState.Closed)
            //    conn.Close();

            return true;
        }

        //创建数据库参数
        public static SqlParameter CreateParameter(string pName, object pValue)
        {
            SqlParameter ps = new SqlParameter();
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

        /// <summary>   
        /// 执行不带参数sql语句，返回所影响的行数   
        /// </summary>   
        /// <param name="cmdstr">增，删，改sql语句</param>   
        /// <returns>返回所影响的行数</returns>   
        public static int ExecuteNonQuery(string cmdText)
        {
            int count;
            try
            {
                SqlConnection conn = new SqlConnection(ConnStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                count = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return count;
        }


        //通用的增，删， 改
        public static int ExecuteSql(string sql, bool isProcedure, params SqlParameter[] ps)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (isProcedure)
                    cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(ps);

                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return 0;
        }

        //通用的查询
        public static DataTable GetTable(string sql, bool isProcedure, params SqlParameter[] ps)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnStr);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                if (isProcedure)
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddRange(ps);

                DataSet ds = new DataSet();
                da.Fill(ds);

                da.Dispose();
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// 通用查询 支持返回多张表
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="isProcedure"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql, bool isProcedure, params SqlParameter[] ps)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnStr);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                if (isProcedure)
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddRange(ps);

                DataSet ds = new DataSet();
                da.Fill(ds);

                da.Dispose();
                conn.Close();
                return ds;
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return null;
        }

        //返回第一行第一列的值
        public static object GetValue(string sql, bool isProcedure, params SqlParameter[] ps)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (isProcedure)
                    cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(ps);

                object result = cmd.ExecuteScalar();
                cmd.Dispose();
                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return null;
        }

        public static bool BeginTransaction()
        {
            string sql = "BEGIN TRAN;";
            ExecuteSql(sql, false);

            return true;
        }

        public static bool RollbackTransaction()
        {
            string sql = "ROLLBACK TRAN;";
            ExecuteSql(sql, false);

            return true;
        }

        public static bool CommitTransaction()
        {
            string sql = "COMMIT TRAN;";
            ExecuteSql(sql, false);

            return true;
        }
    }
}