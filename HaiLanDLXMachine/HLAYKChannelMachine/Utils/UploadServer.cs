using HLACommonLib;
using HLACommonLib.DAO;
using HLACommonLib.Model;
using HLACommonLib.Model.YK;
using HLAYKChannelMachine.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace HLAYKChannelMachine.Utils
{
    public delegate void UploadedHandler(YKBoxInfo Box);
    public class UploadServer
    {
        Timer uploadTimer = new Timer(2000);
        public Stack CurrentUploadQueue = new Stack(100);
        object _lockObject = new object();
        static object _lock = new object();
        bool isbusy = false;
        public event UploadedHandler OnUploaded;

        static UploadServer instance;

        private UploadServer()
        {

        }

        public static UploadServer GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new UploadServer();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// 开始上传线程，检查是否还有未上传的信息，如果有，则弹出对话框问是否显示
        /// </summary>
        /// <returns>true 表示有未上传信息，需要提示；false 表示没有未上传信息</returns>
        public bool Start()
        {
            bool haveUndoInfo = false;
            // 此处到数据库检查是否还有未上传的信息，如果有，返回true，主界面则弹出对话框问是否显示
            // 目前此处是不会将上次关闭系统前未上传的任务继续上传的，需要手动上传
            List<SqliteUploadDataInfo> undoList = YKBoxSqliteService.GetUnUploadDataList();
            if (undoList != null && undoList.Count > 0)
            {
                foreach (SqliteUploadDataInfo item in undoList)
                {
                    CurrentUploadQueue.Push(item);
                }
            }
            // 开始定时上传
            uploadTimer.Elapsed += UploadTimer_Elapsed;
            uploadTimer.Start();

            return haveUndoInfo;
        }

        private void UploadTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Upload();
        }
        /// <summary>
        /// 检查是否还是有未上传信息
        /// </summary>
        /// <returns></returns>
        public bool CheckUndoTask()
        {
            return CurrentUploadQueue.Count > 0;
        }
        /// <summary>
        /// 终止上传线程，如果还有未上传的信息，则弹出对话框问是否仍然退出
        /// </summary>
        /// <returns>true 表示有未完成上传信息，需要提示；false 表示没有未上传信息</returns>
        public void End()
        {
            uploadTimer.Elapsed -= UploadTimer_Elapsed;
            uploadTimer.Stop();
        }

        /// <summary>
        /// 取出队列中第一个上传任务，进行上传，上传完成后删除这条
        /// </summary>
        public void Upload()
        {
            lock (_lockObject)
            {
                if (isbusy)
                    return;
                else
                    isbusy = true;
                try
                {
                    if (CurrentUploadQueue.Count > 0)
                    {
                        //取出任务上传
                        object o = CurrentUploadQueue.Pop();
                        if (o.GetType() == typeof(SqliteUploadDataInfo))
                        {
                            YKBoxInfo box = (o as SqliteUploadDataInfo).Data;
                            //if(box.Status == "S")
                            //{
                                SapResult result = SAPDataService.UploadYKBoxInfo(SysConfig.LGNUM, box);
                                box.SapRemark = result.MSG;
                                box.SapStatus = result.STATUS;
                                bool xdSaveResult = YKBoxService.SaveBox(box);
                                YKBoxSqliteService.SetUploaded((o as SqliteUploadDataInfo).Guid);
                                if (OnUploaded != null) OnUploaded(box);
                            //}
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                }

                isbusy = false;
            }
        }
    }
}
