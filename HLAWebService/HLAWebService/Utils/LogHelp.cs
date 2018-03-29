using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace HLAWebService.Utils
{
    public class LogHelp
    {
        private LogHelp() { }

        //创建静态单实例类
        public static readonly LogHelp instance = new LogHelp();

        //记录日志
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region
        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="errModule">errModule模块信息</param>
        /// <param name="ex">异常对象</param>
        public static void LogError(String errModule, Exception ex)
        {

            lock (log)
            {

                log.Error(errModule + "错误消息:" + ex.Message.ToString() + "\n" + ex.StackTrace);
            }


        }
        #endregion

        #region 记录异常信息
        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void LogError(Exception ex)
        {


            lock (log)
            {
                StringBuilder strError = new StringBuilder();
                strError.AppendFormat(ex.ToString()).Append("\n");
                if (ex.Data.Count > 0)
                {
                    strError.AppendFormat("扩展信息:\n");

                    foreach (DictionaryEntry data in ex.Data)
                    {
                        strError.AppendFormat(data.Key.ToString()).Append(":").Append(data.Value.ToString()).Append("\n");
                    }
                }
                log.Error(strError.ToString());
                strError.AppendFormat(ex.StackTrace + "\n");
                //DebugHelper.RecordError(strError.ToString());

            }


        }
        #endregion

        #region 记录错误信息

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="errModule">errModule模块信息</param>
        /// <param name="message">错误消息</param>
        public static void LogError(String errModule, string message)
        {


            lock (log)
            {
                log.Error(errModule + "错误消息:" + message);
            }

        }
        #endregion

        #region 记录警告信息
        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="Module">errModule模块信息</param>
        /// <param name="Message">警告信息</param>
        public static void LogWarn(String Module, string Message)
        {
            lock (log)
            {
                log.Warn("在:" + Module + "模块,警告信息:" + Message + "\n");


            }

        }
        #endregion

        #region 记录普通信息
        /// <summary>
        /// 记录普通信息
        /// </summary>

        /// <param name="Message">信息内容</param>
        public static void LogInfo(string Message)
        {
            lock (log)
            {
                log.Info(Message);
            }

        }
        #endregion

        #region 记录调试信息
        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="Message">信息内容</param>
        public static void LogDebug(string Message)
        {
            lock (log)
            {
                log.Debug(Message + "\n");
            }

        }
        #endregion
    }
}