using HLACommonLib;
using HLACommonLib.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace HLADeliverChannelMachine.Utils
{
    public delegate void UploadSuccessHandler(string PICK_TASK);
    public delegate void UploadCountHandler(bool OneMore);
    public class UploadServer
    {
        Timer uploadTimer = new Timer(2000);
        public Stack CurrentUploadQueue = new Stack(100);
        object _lockObject = new object();
        static object _lock = new object();
        bool isbusy = false;
        public event UploadSuccessHandler OnUploadSuccess;
        public event UploadCountHandler OnUploadCount;

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

                if (CurrentUploadQueue.Count > 0)
                {
                    if (OnUploadCount != null)
                        OnUploadCount(true);
                    // 取出任务上传
                    object o = CurrentUploadQueue.Pop();
                    if (o.GetType() == typeof(UploadOutLogDataInfo))
                    {
                        OutLogDataInfo log = (o as UploadOutLogDataInfo).UploadData;
                        string errorMsg = "";
                        /*判断数据类型*/
                        string zxjd_TYPE = string.Empty;
                        string seqNo = string.Empty;
                        if (log.OutLogList != null && log.OutLogList.Count > 0)
                        {
                            zxjd_TYPE = log.OutLogList.Find(i=>i.PICK_TASK == log.PickTask).ZXJD_TYPE;
                            seqNo = log.OutLogList.Find(i => i.PICK_TASK == log.PickTask).PICK_TASK_ITEM;
                        }
                        bool isUpload = false;
                        if (zxjd_TYPE != "4")
                        {
                            isUpload = SAPDataService.UploadOutLogInfo(log.LGNUM, log.PickTask, log.LOUCENG, log.OutLogList, log.ErrorList, log.BoxList, log.BoxDetailList, out errorMsg);
                        }
                        else
                        {
                            isUpload = SAPDataService.UploadOutLogInfo_051(log.LGNUM, seqNo, log.PickTask, log.LOUCENG, log.OutLogList, log.ErrorList, log.BoxList, log.BoxDetailList, out errorMsg);
                        }
                        if (isUpload)
                        {
                            UploadSuccessOption(log.PickTask);
                            LocalDataService.DeleteUploaded((o as UploadOutLogDataInfo).Guid);  //删除已上传成功的数据
                            AudioHelper.PlayWithSystem("Resources/success.wav");
                        }
                        else
                        {
                            //(o as UploadOutLogDataInfo).RetryTimes++;
                            //上传下架单失败，记录错误信息到数据库，同时将信息再次放入队列等待下次重试，自动重试次数不超过3次
                            LocalDataService.UpdateErrorOfUploadData((o as UploadOutLogDataInfo).Guid, errorMsg);
                            //if ((o as UploadOutLogDataInfo).RetryTimes < 3)
                            //{
                            //CurrentUploadQueue.Push(o);
                            //}
                            AudioHelper.PlayWithSystem("Resources/fail.wav");

                        }
                    }
                }
                else
                {
                    if (OnUploadCount != null)
                        OnUploadCount(false);
                }
                isbusy = false;
            }
        }

        public bool AsynUploadOutLogInfo(string picktask, string louceng, List<InventoryOutLogDetailInfo> outLogList,
            List<OutLogErrorRecord> errorList, List<ShippingBox> boxList, List<ShippingBoxDetail> boxdetailList,
            bool togAutoprint, bool BoxIsPrintMergeTag, out string errorMsg)
        {
            //满箱自动打印开关：开启时为异步操作，不需要下架成功再打印；
            //关闭时为同步操作，需下架成功再打印所有标签，下架失败需弹窗提示。
            errorMsg = "";
            OutLogDataInfo outLog = new OutLogDataInfo();

            outLog.BoxDetailList = boxdetailList != null ? new List<ShippingBoxDetail>(boxdetailList) : null;
            outLog.BoxList = boxList != null ? new List<ShippingBox>(boxList) : null;
            outLog.ErrorList = errorList != null ? new List<OutLogErrorRecord>(errorList) : null;
            outLog.LGNUM = SysConfig.LGNUM;
            outLog.LOUCENG = louceng;
            outLog.OutLogList = outLogList != null ? new List<InventoryOutLogDetailInfo>(outLogList) : null;
            outLog.PickTask = picktask;
            if (togAutoprint)
            {
                if (CurrentUploadQueue.Count > 99)
                {
                    errorMsg = "上传下架单序列已满失败！请检查网络环境并稍后提交。";
                    return false;
                }
                UploadOutLogDataInfo upload = new UploadOutLogDataInfo(SysConfig.DeviceNO, outLog);
                // 加入队列，并存入数据库
                if (!LocalDataService.InsertUploadData(upload))
                {
                    errorMsg = "下架单上传云服务器失败！";
                    return false;
                }
                else
                {
                    CurrentUploadQueue.Push(upload);
                }
            }
            else
            {
                bool isUpload = SAPDataService.UploadOutLogInfo(outLog.LGNUM, outLog.PickTask, outLog.LOUCENG,
                    outLog.OutLogList, outLog.ErrorList, outLog.BoxList, outLog.BoxDetailList, out errorMsg);
                if (isUpload)
                {
                    UploadSuccessOption(outLog.PickTask);
                    return true;
                }
                else
                {
                    //上传下架单失败
                    return false;
                }
            }

            return true;
        }

        public void UploadSuccessOption(string picktask)
        {
            LocalDataService.SetInventoryOutLogDetailOutStatus(picktask, 1);//设置下架单出库状态为1
            if (OnUploadSuccess != null)
                OnUploadSuccess(picktask);
        }
    }
}
