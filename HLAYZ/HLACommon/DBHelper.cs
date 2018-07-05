using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace HLACommon
{
    public static class DBHelper
    {
        private static string _connStr;
        public static string ConnStr
        {
            get
            {
                if (string.IsNullOrEmpty(_connStr))
                {
                    _connStr = CConfig.mDBUrl;
                }

                return _connStr;
            }

            set
            {
                _connStr = value;
            }
        }

        public static bool Disconnect()
        {
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
            int count = 0;

            SqlConnection conn = new SqlConnection(ConnStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdText, conn); 
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(cmdText, ex);
                count = 0;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }

        public static int ExecuteSql(string cusStr,string sql, bool isProcedure, params SqlParameter[] ps)
        {
            SqlConnection conn = new SqlConnection(cusStr);
            int result = 0;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (isProcedure)
                    cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(ps);

                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(sql, ex);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        //通用的增，删， 改
        public static int ExecuteSql(string sql, bool isProcedure, params SqlParameter[] ps)
        {
            SqlConnection conn = new SqlConnection(ConnStr);

            int result = 0;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (isProcedure)
                    cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(ps);

                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(sql, ex);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        //通用的查询
        public static DataTable GetTable(string sql,bool isProcedure, params SqlParameter[] ps)
        {
            DataTable re = null;
            SqlConnection conn = new SqlConnection(ConnStr);

            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                if (isProcedure)
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddRange(ps);

                DataSet ds = new DataSet();
                da.Fill(ds);

                da.Dispose();
                re = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(sql, ex);
            }
            finally
            {
                conn.Close();

            }

            return re;
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
            DataSet re = null;
            SqlConnection conn = new SqlConnection(ConnStr);

            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                if (isProcedure)
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddRange(ps);

                re = new DataSet();
                da.Fill(re);

                da.Dispose();
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(sql, ex);
            }
            finally
            {
                conn.Close();
            }

            return re;
        }

        //返回第一行第一列的值
        public static object GetValue(string sql,bool isProcedure, params SqlParameter[] ps)
        {
            object re = null;
            SqlConnection conn = new SqlConnection(ConnStr);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (isProcedure)
                    cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(ps);

                re = cmd.ExecuteScalar();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(sql, ex);
            }
            finally
            {
                conn.Close();

            }

            return re;
        }

        public static bool BeginTransaction()
        {
            string sql = "BEGIN TRAN;";
            ExecuteSql(sql,false);

            return true;
        }

        public static bool RollbackTransaction()
        {
            string sql = "ROLLBACK TRAN;";
            ExecuteSql(sql,false);

            return true;
        }

        public static bool CommitTransaction()
        {
            string sql = "COMMIT TRAN;";
            ExecuteSql(sql,false);

            return true;
        }
    }
}
