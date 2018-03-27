using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using HLACommonLib;
using HLACommonLib.Model;
using HLACommonLib.Model.PK;
using HLACommonLib.DAO;

namespace HLAPKChannelMachine.Utils
{
    public delegate void UploadedHandler(CJianHuoUpload data, SapResult re);

    public class UploadServer
    {
        Timer uploadTimer = new Timer(1500);
        public Queue CurrentUploadQueue = new Queue();
        object _lockObject = new object();
        static object _lock = new object();
        static UploadServer instance;
        public event UploadedHandler OnUploaded;

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
        public void AddToQueue(object o)
        {
            lock (_lockObject)
            {
                CurrentUploadQueue.Enqueue(o);
            }
        }
        /// <summary>
        /// 开始上传线程，检查是否还有未上传的信息，如果有，则弹出对话框问是否显示
        /// </summary>
        /// <returns>true 表示有未上传信息，需要提示；false 表示没有未上传信息</returns>
        public void Start()
        {
            // 开始定时上传
            uploadTimer.Elapsed += UploadTimer_Elapsed;
            uploadTimer.Start();
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
            CUploadData task = null;
            if (CurrentUploadQueue.Count > 0)
            {
                lock (_lockObject)
                {
                    // 取出任务上传
                    task = CurrentUploadQueue.Dequeue() as CUploadData;
                }
            }

            if(task!=null)
            {
                //upload
                SapResult re = SAPDataService.UploadJianHuoData(task.Data as CJianHuoUpload);
                if(re.SUCCESS)
                {
                    SqliteDataService.delUploadFromSqlite(task.Guid);
                }
                else
                {
                    SqliteDataService.updateMsgToSqlite(task.Guid, re.MSG);
                    OnUploaded?.Invoke(task.Data as CJianHuoUpload, re);
                }


            }
        }

    }
}
