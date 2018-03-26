using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using log4net.Repository.Hierarchy;
using log4net.Core;
using log4net.Appender;
using log4net.Layout;
using System.IO;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace HLACommonLib
{
    public class Log4netHelper
    {
        private Log4netHelper() { }

        //创建静态单实例类
        public static readonly Log4netHelper instance = new Log4netHelper();

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

    public class CLogManager
    {
        private string mFileName;
        private string mExtensionName = @".txt";
        private bool mShouldLog;
        public CLogManager(string fileName, bool append = false, bool shouldLog = false)
        {
            mFileName = fileName;
            mShouldLog = shouldLog;
            initLog(fileName, append);
        }
        public CLogManager(bool shouldLog = false,bool append = false)
        {
            string processFullName = Process.GetCurrentProcess().MainModule.FileName;
            string processName = Path.GetFileNameWithoutExtension(processFullName);
            DateTime today = DateTime.Now;
            mFileName = processName + @"-" + today.Year.ToString() + @"-"
            + today.Month.ToString() + @"-"
            + today.Day.ToString();
            mShouldLog = shouldLog;
            initLog(mFileName, append);
        }
        public string copyToServer(bool overrite=true)
        {
            try
            {
                string source = @"log\" + mFileName + mExtensionName;
                string dest = @"\\172.18.10.98\共享文件夹\log\" + mFileName + mExtensionName;
                if (!overrite)
                {
                    if (File.Exists(dest))
                        dest += @".new";
                }
                File.Copy(source, dest, true);
                return "";
            }
            catch (System.Exception ex)
            {
                return ex.ToString();
            }
        }
        private void initLog(string fileName, bool append)
        {
            Hierarchy hierarchy = (Hierarchy)log4net.LogManager.GetRepository();

            log4net.Layout.PatternLayout pl = new log4net.Layout.PatternLayout();
            /*%date [%thread] %-5level %logger - %message%newline*/
            pl.ConversionPattern = "%date [%thread] %-5level: %message%newline";
            pl.ActivateOptions();

            log4net.Appender.FileAppender fa = new log4net.Appender.FileAppender();
            fa.Layout = pl;
            fa.LockingModel = new FileAppender.MinimalLock();
            DateTime today = DateTime.Today;
            fa.File = @"log" + Path.DirectorySeparatorChar + fileName + mExtensionName;
            fa.AppendToFile = append;
            fa.ActivateOptions();

            log4net.Config.BasicConfigurator.Configure(fa);
        }
        public void log(string msg)
        {
            if (mShouldLog)
            {
                log4net.ILog log = log4net.LogManager.GetLogger(mFileName);
                log.Info(msg);
            }
        }
        public void openLog()
        {
            mShouldLog = true;
        }
        public void closeLog()
        {
            mShouldLog = false;
        }
    }

}
