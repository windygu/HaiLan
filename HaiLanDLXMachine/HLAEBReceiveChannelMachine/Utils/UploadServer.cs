using HLACommonLib;
using HLACommonLib.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace HLAEBReceiveChannelMachine.Utils
{
    public class UploadServer
    {
        Timer uploadTimer = new Timer(1500);
        public Stack CurrentUploadQueue = new Stack(100);
        object _lockObject = new object();
        static object _lock = new object();
        bool isbusy = false;
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
        public void Start()
        {
            
            List<UploadEbBoxInfo> list = SqliteDataService.GetUnUploadEbBox();
            if (list != null && list.Count > 0)
            {
                foreach (UploadEbBoxInfo item in list)
                {
                    CurrentUploadQueue.Push(item);
                }
            }
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
            lock (_lockObject)
            {
                if (isbusy)
                    return;
                else
                    isbusy = true;

                if (CurrentUploadQueue.Count > 0)
                {
                    // 取出任务上传
                    object o = CurrentUploadQueue.Pop();
                    
                    if (o.GetType() == typeof(UploadEbBoxInfo))
                    {
                        UploadEbBoxInfo box = o as UploadEbBoxInfo;
                        bool isUpload = SAPDataService.UploadEbBoxInfo(box.LGNUM, box.EQUIP_HLA, box.HU, box.ChangeTime, box.InventoryResult, box.ErrorMsg, box.SubUser, box.TagDetailList);
                        if (isUpload)
                        {
                            SqliteDataService.DeleteUploaded(box.Guid);  //删除已上传成功的数据
                        }
                        else
                        {
                            box.RetryTimes++;
                            if (box.RetryTimes < 3) //最多重试3次
                            {
                                CurrentUploadQueue.Push(o);
                            }
                            else
                            {
                                SqliteDataService.DeleteUploaded(box.Guid);  //删除超过3次上传失败的数据
                                LogHelper.WriteLine(JsonConvert.SerializeObject(box));  //并记录日志，防止将来还需要使用
                            }
                        }
                    }
                    else if (o.GetType() == typeof(EbBoxCheckRecordInfo))
                    {
                        EbBoxCheckRecordInfo record = o as EbBoxCheckRecordInfo;
                        LocalDataService.InsertEbCheckRecord(record, HLACommonLib.Model.ENUM.CheckType.电商收货复核);
                    }
                    else if (o.GetType() == typeof(EbBoxErrorRecordInfo))
                    {
                        EbBoxErrorRecordInfo record = o as EbBoxErrorRecordInfo;
                        LocalDataService.InsertEbBoxErrorRecord(record, HLACommonLib.Model.ENUM.CheckType.电商收货复核);
                    }
                    
                }
                isbusy = false;
            }
        }

    }
}
