using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLAChannelMachine.Utils;
using System.Speech.Synthesis;
using System.IO.Ports;
using System.Threading;
using HLACommonLib;
using HLACommonLib.Model;
using HLACommonLib.Comparer;
using Newtonsoft.Json;
using HLAChannelMachine.DialogForm;
using System.Xml;
using HLACommonLib.Model.ENUM;
using System.Runtime.InteropServices;
using HLACommonLib.Model.RECEIVE;
using HLACommonLib.DAO;

namespace HLAChannelMachine
{
    public partial class InventoryForm : Form
    {
        #region 属性变量

        #region 读写器相关属性
#if IMPINJ
        IUHFReader reader = new ImpinjR420();
#else
        IUHFReader reader = new ThingMagic840();
#endif
        Thread readerConnectThread = null;
        #endregion

        #region 其他业务
        private object savingErrorRecordLockObject = new object();
        /// <summary>
        /// 需要保存的检货记录
        /// </summary>
        private Queue<ErrorRecord> savingErrorRecord = new Queue<ErrorRecord>();
        /// <summary>
        /// 需要保存的数据的线程
        /// </summary>
        private Thread savingDataThread = null;
        /// <summary>
        /// 需要保存的数据的同步锁
        /// </summary>
        private object savingDataLockObject = new object();
        /// <summary>
        /// 需要保存的数据
        /// </summary>
        private Queue<UploadData> savingData = new Queue<UploadData>();
        /// <summary>
        /// 错误记录窗口【副屏专用】
        /// </summary>
        ErrorRecordForm errorForm = null;
        ReceiveType receiveType = ReceiveType.交货单收货;
        /// <summary>
        /// 从数据库中拉取的箱子历史信息
        /// </summary>
        private List<HuInfo> huList = new List<HuInfo>();
        /// <summary>
        /// huList锁，多线程操作该对象时需要加锁
        /// </summary>
        private object huListLock = new object();
        ///// <summary>
        ///// 本轮所有错误信息列表【弃用】
        ///// </summary>
        //private List<string> errorMsgList = new List<string>();
        /// <summary>
        /// 语音合成对象
        /// </summary>
        //public SpeechSynthesizer speech = new SpeechSynthesizer();
        /// <summary>
        /// 当前交货单物料信息
        /// </summary>
        private List<MaterialInfo> materialList = new List<MaterialInfo>();
        /// <summary>
        /// 当前交货单吊牌信息
        /// </summary>
        private List<HLATagInfo> hlaTagList = new List<HLATagInfo>();
        /// <summary>
        /// 当前交货单需要匹配的EPC明细
        /// </summary>
        private List<EpcDetail> epcdetailList = new List<EpcDetail>();
        /// <summary>
        /// 本轮扫描到的标签总数
        /// </summary>
        private int currentNum = 0;
        /// <summary>
        /// 同一个箱子的上一次检测结果 S：正常 E：异常
        /// </summary>
        string lastResult = null;
        /// <summary>
        /// 平库箱码存储区域
        /// </summary>
        Queue<string> boxNoQueue = new Queue<string>();
        /// <summary>
        /// 高位库箱码存储区域
        /// </summary>
        private Queue<string> boxNoList = new Queue<string>();
        /// <summary>
        /// 读写器是否正在盘点
        /// </summary>
        private bool isInventory = false;
        /// <summary>
        /// 读写器最后读取时间
        /// </summary>
        private DateTime lastReadTime = DateTime.Now;
        /// <summary>
        /// 本轮扫描的EPC列表
        /// </summary>
        private List<string> epcList = new List<string>();
        /// <summary>
        /// 当前交货单信息
        /// </summary>
        private DocInfo currentDocInfo = null;
        /// <summary>
        /// 本轮扫描到的主条码的数据
        /// </summary>
        private Dictionary<string, ListViewItem> dicTagDetail = new Dictionary<string, ListViewItem>();
        /// <summary>
        /// 主辅条码epc信息列表
        /// </summary>
        private Dictionary<string, TagDetailInfoExtend> tdiExtendList = new Dictionary<string, TagDetailInfoExtend>();
        /// <summary>
        /// 当前选择的行项目-高位库专用 
        /// </summary>
        private ListViewItem currentDocdetailItem = null;
        /// <summary>
        /// 是否为重投检测
        /// </summary>
        private bool IsRecheck = false;
        /// <summary>
        /// 盘点结果[TDJ-ZZ-1专用] 高位库专用
        /// 1正常 2回流 3异常 4停止皮带
        /// </summary>
        private InventoryResult inventoryResult = new InventoryResult() { Result = 0, Message = "" };
        /// <summary>
        /// 交货单明细信息（行项目信息）
        /// </summary>
        List<DocDetailInfo> docDetailInfoList = new List<DocDetailInfo>();

        private string errorMsg = "";
        #endregion

        #endregion

        #region 常量
        #region 检测结果
        /// <summary>
        /// 不符合箱规
        /// </summary>
        private const string BU_FU_HE_XIANG_GUI = "箱规不符";
        /// <summary>
        /// 数量大于箱规
        /// </summary>
        private const string SHU_LIANG_DA_YU_XIANG_GUI = "数量大于箱规";
        /// <summary>
        /// 串规格
        /// </summary>
        private const string CUAN_GUI_GE = "串规格";
        /// <summary>
        /// EPC未注册
        /// </summary>
        private const string EPC_WEI_ZHU_CE = "商品未注册";
        /// <summary>
        /// 主条码和辅条码数量不对应
        /// </summary>
        private const string TWO_NUMBER_ERROR = "主条码和辅条码数量不对应";
        /// <summary>
        /// 重投
        /// </summary>
        private const string CHONG_TOU = "重投";
        /// <summary>
        /// 未扫描到箱码
        /// </summary>
        private const string WEI_SAO_DAO_XIANG_MA = "未扫描到箱号";
        /// <summary>
        /// 未选择行项目
        /// </summary>
        private const string WEI_XUAN_ZE_HANG_XIANG_MU = "未选择行项目";
        /// <summary>
        /// 行项目不符
        /// </summary>
        private const string HANG_XIANG_MU_BU_FU = "行项目不符";
        /// <summary>
        /// 正常
        /// </summary>
        private const string RIGHT = "正常";
        /// <summary>
        /// 箱码重复使用
        /// </summary>
        private const string XIANG_MA_CHONG_FU_SHI_YONG = "箱号重复使用";
        /// <summary>
        /// EPC已扫描
        /// </summary>
        private const string EPC_YI_SAO_MIAO = "商品已扫描";
        /// <summary>
        /// 箱码不一致
        /// </summary>
        private const string XIANG_MA_BU_YI_ZHI = "箱号不一致";
        /// <summary>
        /// 未扫描到EPC
        /// </summary>
        private const string WEI_SAO_DAO_EPC = "未扫描到商品";
        /// <summary>
        /// 上传包装箱信息失败
        /// </summary>
        private const string SHANG_CHUAN_SHI_BAI = "上传包装箱信息失败";
        /// <summary>
        /// 未开始检货
        /// </summary>
        private const string WEI_KAI_SHI_JIAN_HUO = "未开始检货";
        #endregion

        #region PLC指令-杏林 FS代表工控机发送给PLC的指令 JS代表PLC发送给工控机的指令
        /// <summary>
        /// 转圈指令
        /// </summary>
        private const string XL_FS_ZHUAN_QUAN = "010600C8000409F7";
        /// <summary>
        /// 打开射频指令
        /// </summary>
        private const string XL_JS_KAI_SHE_PIN = "010600000001480A";
        /// <summary>
        /// 回复PLC指令，表示已收到PLC指令
        /// </summary>
        private const string XL_FS_HUI_FU = "010600CA00016834";
        /// <summary>
        /// 询问结果指令
        /// </summary>
        private const string XL_JS_XUN_WEN_JIE_GUO = "010600000002080B";
        /// <summary>
        /// 异常
        /// </summary>
        private const string XL_FS_YI_CHANG = "010600C80001C9F4";
        /// <summary>
        /// 正常
        /// </summary>
        private const string XL_FS_ZHENG_CHANG = "010600C8000289F5";
        #endregion

        #region PLC指令-漳州 FS代表工控机发送给PLC的指令 JS代表PLC发送给工控机的指令
        /// <summary>
        /// 打开射频指令
        /// </summary>
        private const string ZZ_JS_KAI_SHE_PIN = "010100000001FDCA";
        /// <summary>
        /// 关闭射频指令
        /// </summary>
        private const string ZZ_JS_GUAN_SHE_PIN = "010200000001B9CA";
        /// <summary>
        /// 询问结果指令
        /// </summary>
        private const string ZZ_JS_XUN_WEN_JIE_GUO = "010300000001840A";
        /// <summary>
        /// 正常指令
        /// </summary>
        private const string ZZ_FS_ZHENG_CHANG = "010300000001840A";
        /// <summary>
        /// 重检指令（已过时）
        /// </summary>
        private const string ZZ_FS_CHONG_JIAN = "010300000002C40B";
        /// <summary>
        /// 异常指令
        /// </summary>
        private const string ZZ_FS_YI_CHANG = "01030000000305CB";
        /// <summary>
        /// 延时检测指令（已过时）
        /// </summary>
        private const string ZZ_FS_YAN_SHI_JIAN_CE = "0103000000044409";
        #endregion

        private SynchronizationContext CurrentSynchronizationContext = null;
        #endregion

        #region InventoryForm

        public InventoryForm()
        {
            InitializeComponent();
            this.CurrentSynchronizationContext = SynchronizationContext.Current;
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
#if DEBUG
            grouper3.Show();
#endif
            //if (Screen.AllScreens.Length > 1)
            //{
            //    //如果有多个屏幕，则显示错误信息窗体
            //    errorForm = new ErrorRecordForm();
            //    errorForm.Show();
            //    //启动定时刷新错误信息窗体
            //    timer2.Start();
            //}
            if (SysConfig.RunningModel == RunMode.平库)
                this.btnSetBoxQty.Show();
            
            this.lblCurrentUserNo.Text = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "";
            this.lblDeviceNo.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.EQUIP_HLA : "";
            this.lblDocNo.Text = ""; //交货单号
            this.lblBoxNo.Text = ""; //箱码
            this.lblInventoryResult.Text = ""; //扫描结果
            this.txtTrackNum.Text = SysConfig.TrackNum.ToString();
            this.txtEachTrackNum.Text = SysConfig.EachTrackNum.ToString();
            this.lblTotalCheckNum.Text = (SysConfig.TrackNum * SysConfig.EachTrackNum).ToString();

#region 设置语音播放参数
            //使用 synth 设置朗读音量 [范围 0 ~ 100]  
            //speech.Volume = 70;
            //使用 synth 设置朗读频率 [范围 -10 ~ 10]
            //speech.Rate = 0;
            //speech.SelectVoice("Microsoft Lili");
#endregion

            initSavingData();

            this.savingDataThread = new Thread(new ThreadStart(savingDataThreadFunc));
            this.savingDataThread.IsBackground = true;
            this.savingDataThread.Start();

#region 连接读写器
            reader.OnTagReported += new TagReportedHandler(reader_OnTagReported);
            this.lblReaderStatus.Text = "正在连接";
            readerConnectThread = new Thread(new ThreadStart(() =>
            {
                //bool result = reader.Connect(SysConfig.ReaderIp, SynchronizationContext.Current);
                bool result = reader.Connect(SysConfig.ReaderIp, this.GetSynchronizationContext());

                if (!result)
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show("连接读写器失败！", "错误");
                        this.lblReaderStatus.Text = "异常";
                    }));
                    return;
                }
                else
                {
                    reader.SetParameter(SysConfig.ReaderConfig);

                    this.Invoke(new Action(() =>
                    {
                        this.lblReaderStatus.Text = "正常";
                    }));
                }
            }));
            readerConnectThread.IsBackground = true;
            readerConnectThread.Start();
#endregion
        }

        public void LoadDocNoInfo(DocInfo docInfo, List<DocDetailInfo> _docDetailInfoList, List<MaterialInfo> _materialList, List<HLATagInfo> _hlaTagInfo, List<EpcDetail> _epcdetailList, List<HuInfo> _huList)
        {
            this.huList = _huList != null ? _huList : new List<HuInfo>();
            this.currentDocInfo = docInfo != null ? docInfo : new DocInfo();
            this.docDetailInfoList = _docDetailInfoList != null ? _docDetailInfoList : new List<DocDetailInfo>();
            this.materialList = _materialList != null ? _materialList : new List<MaterialInfo>();
            this.hlaTagList = _hlaTagInfo != null ? _hlaTagInfo : new List<HLATagInfo>();
            this.epcdetailList = _epcdetailList != null ? _epcdetailList : new List<EpcDetail>();

            //清空交货单明细列表
            this.lvDocDetail.Items.Clear();
            //清空扫描列表

            this.lblDocNo.Text = this.currentDocInfo.DOCNO; //交货单号
            int actualTotalNum = 0;
            int totalBoxNum = 0;
            if (docDetailInfoList != null && docDetailInfoList.Count > 0)
            {
                foreach (DocDetailInfo ddi in docDetailInfoList)
                {
                    ListViewItem item = new ListViewItem(ddi.ITEMNO);
                    item.SubItems.Add(ddi.ZSATNR);
                    item.SubItems.Add(ddi.ZCOLSN);
                    item.SubItems.Add(ddi.ZSIZTX);
                    item.SubItems.Add(ddi.ZCHARG);
                    item.SubItems.Add(ddi.QTY.ToString());
                    item.SubItems.Add(ddi.REALQTY.ToString());
                    item.SubItems.Add(ddi.BOXCOUNT.ToString());
                    this.lvDocDetail.Items.Add(item);

                    actualTotalNum = actualTotalNum + ddi.REALQTY;
                    totalBoxNum = totalBoxNum + ddi.BOXCOUNT;
                }
            }
            this.lblActualTotalNum.Text = actualTotalNum.ToString();
            this.lblTotalBoxNum.Text = totalBoxNum.ToString();

            initErrorRecord();
        }

        private void InventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            int saveCount = savingErrorRecord.Count + savingData.Count;
            if (saveCount>0)
            {
                MessageBox.Show(string.Format("对不起,当前有{0}条数据正在保存,请稍候再关闭程序!",saveCount), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            
            if (errorForm != null)
            {
                errorForm.Close();
            }

            //if (SysConfig.RunningModel == RunMode.平库)
            //    SendCustomCommand("010600CB0000F834");//停止
            //StopInventory();
            reader.StopInventory();
            //关闭读写器连接
            reader.Disconnect();
            //关闭语音
            //speech.Dispose();

            if (readerConnectThread != null && readerConnectThread.IsAlive)
                readerConnectThread.Abort();

            if (this.savingDataThread != null)
                this.savingDataThread.Abort();

            //退出登录
            SysConfig.CurrentLoginUser = null;
        }
#endregion

#region 私有函数
        private SynchronizationContext GetSynchronizationContext()
        {
            return this.CurrentSynchronizationContext;
        }

        private void initSavingData()
        {
            List<UploadData> datalist = SqliteDataService.GetUnUploadDataList();
            if (datalist != null && datalist.Count > 0)
            {
                foreach (UploadData item in datalist)
                {
                    savingData.Enqueue(item);
                }
            }
        }

        /// <summary>
        /// 保存包装箱信息的线程
        /// </summary>
        private void savingDataThreadFunc()
        {
            while (true)
            {
                lock (savingDataLockObject)
                {
                    if (savingData.Count > 0)
                    {
                        UploadData upload = savingData.Dequeue();
                        Thread thread = new Thread(new ThreadStart(() =>
                        {
                            SaveData(upload);
                        }));
                        thread.IsBackground = true;
                        thread.Start();
                    }
                }
                lock (savingErrorRecordLockObject)
                {
                    if (savingErrorRecord.Count > 0)
                    {
                        ErrorRecord error = savingErrorRecord.Dequeue();
                        Thread thread = new Thread(new ThreadStart(() =>
                        {
                            LocalDataService.SaveErrorRecord(error, receiveType);
                        }));
                        thread.IsBackground = true;
                        thread.Start();
                    }
                }

                Thread.Sleep(2000);
            }
        }

        /// <summary>
        /// 获取箱号-平库使用
        /// </summary>
        private void getBoxNoQueue()
        {
            //string hu = LocalDataService.GetNewPMHu(SysConfig.DeviceNO);
            //if(!string.IsNullOrEmpty(hu))
            //    boxNoQueue.Enqueue(hu);
            if (SysConfig.IsTest)
            {
                boxNoQueue.Enqueue(new Random().Next(11111111, 99999999).ToString());
                boxNoQueue.Enqueue(new Random().Next(11111111, 99999999).ToString());
                boxNoQueue.Enqueue(new Random().Next(11111111, 99999999).ToString());
                return;
            }
            boxNoQueue = SAPDataService.GetBoxNo(SysConfig.LGNUM);
        }
        /// <summary>
        /// 开始盘点
        /// </summary>
        private bool StartInventory()
        {

            //判断是否未盘点，未盘点则开始盘点
            if (this.isInventory == false)
            {
                try
                {
                    //清除当前屏幕统计数量
                    this.errorMsg = "";
                    this.epcList.Clear();
                    this.currentNum = 0;
                    this.dicTagDetail.Clear();
                    this.tdiExtendList.Clear();
                    this.IsRecheck = false;
                    this.currentErrorRecordList.Clear();
                    SetInventoryResult(0);
                    lastResult = null;
                    this.Invoke(new Action(() =>
                    {
                        this.lvTagDetail.Items.Clear();
                        this.lblBoxNo.Text = "";
                        this.lblBoxStandard.Text = "0";
                        this.lblScanNum.Text = "0";
                        this.lblScanNumEx.Text = "0";
                        this.lblEpcNum.Text = "0";
                        this.lblErrorNum.Text = "0";
                        this.lblWorkStatus.Text = "正在盘点";
                        this.lblInventoryResult.Text = "";
                    }));
                    reader.StartInventory(1000, 0);
                    this.isInventory = true;
                    this.lastReadTime = DateTime.Now;
                    if (SysConfig.RunningModel == RunMode.平库)
                    {
                        //如果是平库，自动获取箱号
                        SetBoxNo("");
                    }
                    else if (SysConfig.RunningModel == RunMode.高位库)
                    {
                        //如果是高位库，从扫描头获取箱号
                        if (this.boxNoList.Count > 0)
                        {
                            string boxno = boxNoList.Dequeue();
                            this.Invoke(new Action(() =>
                            {
                                this.lblBoxNo.Text = boxno;
                            }));
                            //LogHelper.WriteLine(boxno);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// 获取本箱检测总数
        /// </summary>
        /// <returns></returns>
        private int GetInventoryTotalNum()
        {
            if (lvTagDetail.Items.Count > 0)
            {
                int result = 0;
                foreach (ListViewItem item in lvTagDetail.Items)
                {
                    result += int.Parse(item.SubItems[4].Text.Trim());
                }
                return result;
            }
            return this.currentNum;
        }

        private List<ErrorRecord> currentErrorRecordList = new List<ErrorRecord>();

        private void UpdateRecordInfo(bool isSuccess)
        {
            //只保存正确信息
            //错误信息要确保当场解决，才能提交
            //add by zjr 
            if (!isSuccess) return;
            if (lvTagDetail.Items.Count > 0)
            {
                foreach (ListViewItem item in lvTagDetail.Items)
                {
                    if (currentErrorRecordList.Count >= lvTagDetail.Items.Count)
                    {
                        currentErrorRecordList.ForEach(new Action<ErrorRecord>((record) =>
                        {
                            record.REMARK = errorMsg;
                        }));
                    }
                    else
                    {
                        ErrorRecord record = new ErrorRecord();
                        record.HU = this.lblBoxNo.Text.Trim();
                        record.QTY = int.Parse(item.SubItems[4].Text.Trim());
                        record.REMARK = errorMsg;
                        record.RESULT = isSuccess ? "S" : "E";
                        record.ZCOLSN = item.SubItems[1].Text.Trim();
                        record.ZSATNR = item.SubItems[0].Text.Trim();
                        record.ZSIZTX = item.SubItems[2].Text.Trim();
                        record.DOCNO = this.lblDocNo.Text.Trim();
                        currentErrorRecordList.Add(record);
                    }
                }
            }
            else
            {
                if (currentErrorRecordList.Count > 0)
                {
                    currentErrorRecordList.ForEach(new Action<ErrorRecord>((record) => {
                        record.REMARK = errorMsg;
                    }));
                }
                else
                {
                    ErrorRecord record = new ErrorRecord();
                    record.HU = this.lblBoxNo.Text.Trim();
                    record.QTY = int.Parse(this.lblEpcNum.Text);
                    record.REMARK = errorMsg;
                    record.RESULT = isSuccess ? "S" : "E";
                    record.ZCOLSN = "";
                    record.ZSATNR = "";
                    record.ZSIZTX = "";
                    record.DOCNO = this.lblDocNo.Text.Trim();
                    currentErrorRecordList.Add(record);
                }
            }
            
        }

        /// <summary>
        /// 显示检测结果信息-公用
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="msg"></param>
        private void ShowInventoryResult(bool isSuccess)
        {
            this.Invoke(new Action(() =>
            {
                if (isSuccess)
                    this.lblInventoryResult.ForeColor = Color.DarkGreen;
                else
                    this.lblInventoryResult.ForeColor = Color.Red;

                this.lblInventoryResult.Text = isSuccess ? "正常" : "异常";//errorMsg;

                UpdateRecordInfo(isSuccess);
            }));

        }
        /// <summary>
        /// 更新交货明细信息
        /// </summary>
        /// <param name="zsatnr">品号</param>
        /// <param name="zcolsn">色号</param>
        /// <param name="zsiztx">规格</param>
        /// <param name="charg">批次</param>
        /// <param name="qty">数量</param>
        /// <param name="isExists">是否存在交货明细</param>
        private void UpdateDocDetailInfo(string zsatnr, string zcolsn, string zsiztx, string charg, int qty,DocInfo doc)
        {
            //更新实收总数和总箱数
            if(currentDocInfo != null && currentDocInfo.DOCNO == doc.DOCNO)
            {
                int actualTotalNum = 0;
                int.TryParse(this.lblActualTotalNum.Text, out actualTotalNum);
                actualTotalNum = actualTotalNum + qty;
                this.lblActualTotalNum.Text = actualTotalNum.ToString();
                int totalBoxNum = 0;
                int.TryParse(this.lblTotalBoxNum.Text, out totalBoxNum);
                totalBoxNum = totalBoxNum + 1;
                this.lblTotalBoxNum.Text = totalBoxNum.ToString();

                bool isExists = false;
                foreach (ListViewItem docDetailItem in this.lvDocDetail.Items)
                {
                    if (docDetailItem.SubItems[1].Text == zsatnr && docDetailItem.SubItems[2].Text == zcolsn
                        && docDetailItem.SubItems[3].Text == zsiztx)
                    {
                        string itemNo = docDetailItem.SubItems[0].Text;
                        int tempqty = 0;
                        int.TryParse(docDetailItem.SubItems[5].Text, out tempqty);
                        int realqty = 0;
                        int.TryParse(docDetailItem.SubItems[6].Text, out realqty);
                        realqty = realqty + qty;
                        int boxcount = 0;
                        int.TryParse(docDetailItem.SubItems[7].Text, out boxcount);
                        boxcount = boxcount + 1;
                        docDetailItem.SubItems[6].Text = realqty.ToString();
                        docDetailItem.SubItems[7].Text = boxcount.ToString();
                        isExists = true;

                        LocalDataService.SaveDocDetail(this.currentDocInfo.DOCNO, itemNo, zsatnr, zcolsn, zsiztx, charg, tempqty, qty, 1, receiveType, "");
                        break;
                    }
                }

                if (!isExists)
                {
                    ListViewItem item = new ListViewItem("");
                    item.SubItems.Add(zsatnr);
                    item.SubItems.Add(zcolsn);
                    item.SubItems.Add(zsiztx);
                    item.SubItems.Add(charg);
                    item.SubItems.Add("0");
                    item.SubItems.Add(qty.ToString());
                    item.SubItems.Add("1");
                    this.Invoke(new Action(() =>
                    {
                        this.lvDocDetail.Items.Add(item);
                    }));
                    LocalDataService.SaveDocDetail(this.currentDocInfo.DOCNO, "", zsatnr, zcolsn, zsiztx, charg, 0, qty, 1, receiveType, "");
                }
            }
            else
            {
                LocalDataService.SaveDocDetail(doc.DOCNO, "", zsatnr, zcolsn, zsiztx, charg, 0, qty, 1, receiveType, "");
            }
        }

        //private string GetErrorMsgInfo()
        //{
        //    string errorMsg = "";
        //    if (this.errorMsgList.Count > 0)
        //    {
        //        foreach (string item in errorMsgList)
        //        {
        //            errorMsg += item + ",";
        //        }
        //    }
        //    if (errorMsg.EndsWith(","))
        //        errorMsg = errorMsg.Remove(errorMsg.Length - 1);
        //    return errorMsg;
        //}

        private void UpdateHuList(string lgnum, string hu, bool result, int qty)
        {
            HuInfo hi = new HuInfo();
            hi.LGNUM = lgnum;
            hi.HU = hu;
            hi.Floor = SysConfig.Floor;
            hi.QTY = qty;
            hi.Result = result ? "S" : "E";
            hi.Timestamp = DateTime.Now;
            lock (huListLock)
            {
                if (!huList.Exists(i => i.HU == hu))
                    huList.Add(hi);
                else
                {
                    huList.Find(i => i.HU == hu).Result = hi.Result;
                    huList.Find(i => i.HU == hu).QTY = hi.QTY;
                }
            }
        }

        private ResultDataInfo  GetResultData(bool inventoryResult)
        {
            ResultDataInfo result = new ResultDataInfo();
            result.BoxNO = this.lblBoxNo.Text.Trim();
            result.CurrentNum = this.currentNum;
            result.CurrentUserId = SysConfig.CurrentLoginUser.UserId;
            result.Doc = this.currentDocInfo.Clone() as DocInfo;
            result.EpcList = new List<string>(this.epcList);
            if (errorMsg.EndsWith(";"))
                errorMsg = errorMsg.Remove(errorMsg.Length - 1);
            result.ErrorMsg = this.errorMsg;
            result.Floor = SysConfig.Floor;
            result.InventoryResult = IsRecheck ? true : inventoryResult;
            result.IsRecheck = this.IsRecheck;
            result.LastResult = this.lastResult;
            result.LGNUM = SysConfig.LGNUM;
            result.LvTagInfo = new List<ListViewTagInfo>();
            foreach (ListViewItem item in lvTagDetail.Items)
            {
                string zsatnr = item.SubItems[0].Text;
                string zcolsn = item.SubItems[1].Text;
                string zsiztx = item.SubItems[2].Text;
                string charg = item.SubItems[3].Text;
                int qty = int.Parse(item.SubItems[4].Text);
                result.LvTagInfo.Add(new ListViewTagInfo(item.Tag.ToString(),zsatnr, zcolsn, zsiztx, charg, qty));
            }
            result.RunningMode = SysConfig.RunningModel;
            result.sEQUIP_HLA = SysConfig.sEQUIP_HLA;
            result.TdiExtendList = new Dictionary<string, TagDetailInfoExtend>();
            foreach (KeyValuePair<string, TagDetailInfoExtend> keyvalue in this.tdiExtendList)
            {
                result.TdiExtendList.Add(keyvalue.Key, keyvalue.Value.Clone() as TagDetailInfoExtend);
            }
            return result;
        }

        /// <summary>
        /// 打印标签 平库需要打印箱标，高位库不需要
        /// </summary>
        /// <param name="result"></param>
        private void PrintBoxStandard(ResultDataInfo result)
        {
            //如果该箱为重投，盘点结果为false，且errorMsg==“重投”
            //if (result.IsRecheck == true && result.InventoryResult == false && result.ErrorMsg == CHONG_TOU)
            if (result.IsRecheck == true)
            {
                if (result.LvTagInfo.Count>0)
                {
                    CommonUtils.PrintRightBoxTagV2(result.InventoryResult, result.BoxNO, result.LvTagInfo);
                }
                return;
            }
            if (result.InventoryResult && result.LvTagInfo.Count > 0)
            {
                CommonUtils.PrintRightBoxTagV2(result.InventoryResult, result.BoxNO, result.LvTagInfo);
            }
            else if (result.InventoryResult == false)
            {
                //CommonUtils.PrintErrorBoxTagV2(result.InventoryResult, result.BoxNO, result.LvTagInfo);
            }
        }

        /// <summary>
        /// 保存盘点结果数据
        /// </summary>
        private void SaveData(UploadData data)
        {
            ResultDataInfo result = data.Data as ResultDataInfo;
            if ((result.LastResult == "S" && !result.InventoryResult) || result.IsRecheck)
            {
                //所有检测结果为异常的，若有历史同一箱码检测结果为S的，则不做任何处理，且不修改原箱码的检测结果。
                UploadedHandler(data.Guid);
                return;
            }
            else
            {
                //有添加设备终端号
                SapResult uploadResult = SAPDataService.UploadBoxInfo(result.LGNUM, result.Doc.DOCNO, result.BoxNO, result.InventoryResult, result.ErrorMsg, result.TdiExtendList, result.RunningMode, result.CurrentUserId, result.Floor, result.sEQUIP_HLA,"");

                if (!uploadResult.SUCCESS)
                {
                    ReceiveUploadData xddata = new ReceiveUploadData()
                    {
                        Guid = data.Guid,
                        CreateTime = data.CreateTime,
                        Data = JsonConvert.SerializeObject(data.Data),
                        Device = SysConfig.DeviceNO,
                        Hu = data.Data.BoxNO,
                        IsUpload = 0,
                        SapResult = uploadResult.MSG,
                        SapStatus = uploadResult.STATUS
                    };
                    if (ReceiveService.SaveUploadData(xddata))
                    {
                        UploadedHandler(data.Guid);
                    }
                    return;
                }
                LocalDataService.SaveInventoryResult(result.LGNUM, result.BoxNO, result.InventoryResult, result.CurrentNum, receiveType);
                UpdateHuList(result.LGNUM, result.BoxNO, result.InventoryResult, result.CurrentNum);
            }

            /*如果为重投，则删除原有epcdetail数据
            if (result.IsRecheck)
            {
                LocalDataService.DeleteEpcDetail(result.LGNUM, result.Doc.DOCNO, result.Doc.DOCTYPE, result.BoxNO);
                epcdetailList.RemoveAll(i => i.LGNUM == result.LGNUM && i.DOCNO == result.Doc.DOCNO && i.HU == result.BoxNO);
            }*/

            //保存epc流水号明细
            LocalDataService.SaveEpcDetail(result.InventoryResult, result.LGNUM, result.Doc.DOCNO, result.Doc.DOCTYPE, result.BoxNO, result.EpcList, receiveType);
            //将EPC明细加入缓存中
            if (result.InventoryResult)
            {
                foreach (string epc in result.EpcList)
                {
                    EpcDetail epcDetail = new EpcDetail();
                    epcDetail.DOCCAT = result.Doc.DOCTYPE;
                    epcDetail.DOCNO = result.Doc.DOCNO;
                    epcDetail.EPC_SER = epc;
                    epcDetail.Floor = result.Floor;
                    epcDetail.Handled = 0;
                    epcDetail.HU = result.BoxNO;
                    epcDetail.LGNUM = result.LGNUM;
                    epcDetail.Result = result.InventoryResult ? "S" : "E";
                    epcDetail.Timestamp = DateTime.Now;
                    this.epcdetailList.Add(epcDetail);
                }
                //将数据附加到交货明细表中
                foreach (ListViewTagInfo tagDetailItem in result.LvTagInfo)
                {
                    string zsatnr = tagDetailItem.ZSATNR;
                    string zcolsn = tagDetailItem.ZCOLSN;
                    string zsiztx = tagDetailItem.ZSIZTX;
                    string charg = tagDetailItem.CHARG;
                    int qty = tagDetailItem.QTY;

                    //当盘点结果正常时累加数量

                    if (this.lvDocDetail.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            UpdateDocDetailInfo(zsatnr, zcolsn, zsiztx, charg, qty,result.Doc);
                        }));
                    }
                    else
                    {
                        UpdateDocDetailInfo(zsatnr, zcolsn, zsiztx, charg, qty, result.Doc);
                    }
                }
            }
            UploadedHandler(data.Guid);
        }

        private void UploadedHandler(string guid)
        {
            //已上传完成,更新uploaddata
            if (!SqliteDataService.SetUploaded(guid))
            {
                LogHelper.WriteLine(string.Format("更新uploaddata出错:GUID[{0}]", guid));
            }
        }

        private List<EpcDetail> GetEpcdetailListByHU(string HU)
        {
            return epcdetailList.FindAll(i => i.HU != null && i.HU.Trim() != "" && i.HU == HU && i.Result == "S").Distinct(new EpcDetailComparer()).ToList<EpcDetail>();
        }
#region 过时代码 勿删除，将来可能再启用
        /// <summary>
        /// 检查标签数量是否达到箱规
        /// </summary>
        //private void CheckNumber()
        //{
        //    if (tdiExtendList != null && tdiExtendList.Count > 0)
        //    {
        //        TagDetailInfoExtend tdiExtend = this.tdiExtendList.Values.First();
        //        int pxqty = tdiExtend.PXQTY;
        //        int normalNum = this.tdiExtendList.Count;//统计合法数量
        //        int rfidEpcNum = this.tdiExtendList.Values.Count(o => o.HAS_RFID_EPC == true);//统计主条码epc数
        //        int rfidAddEpcNum = this.tdiExtendList.Values.Count(o => o.HAS_RFID_ADD_EPC == true);//统计辅条码epc数
        //        if (rfidAddEpcNum > 0 && rfidEpcNum != rfidAddEpcNum)
        //        {
        //            return;
        //        }
        //        if (rfidEpcNum < pxqty) return;
        //        if (rfidEpcNum > pxqty)
        //        {
        //            if (this.cbUseBoxStandard.Checked)
        //            {
        //                //ErrorHandle(BU_FU_HE_XIANG_GUI, 3);
        //                errorMsg = BU_FU_HE_XIANG_GUI;
        //                this.inventoryResult = new InventoryResult() { Message = BU_FU_HE_XIANG_GUI, Result = 3 };
        //            }
        //        }
        //    }
        //}
#endregion
        /// <summary>
        /// 平库，分配箱码
        /// </summary>
        /// <param name="boxNo"></param>
        private void SetBoxNo(string boxNo)
        {
            if (boxNoQueue.Count > 0)
                boxNo = boxNoQueue.Dequeue();
            else
            {
                getBoxNoQueue();
                boxNo = boxNoQueue.Dequeue();
            }

            this.Invoke(new Action(() =>
            {
                this.lblBoxNo.Text = boxNo;
            }));
        }
        /// <summary>
        /// 匹配是否有已扫描过的EPC
        /// </summary>
        /// <param name="epcList"></param>
        /// <returns></returns>
        private EpcDetail GetEpcDetailByEpcList(List<string> epcList)
        {
            if (epcList == null || epcList.Count == 0)
                return null;
            if (epcdetailList == null || epcdetailList.Count == 0)
                return null;
            return epcdetailList.Find(i => epcList.Contains(i.EPC_SER));
        }

        private string GetInventoryResultByBoxNo(string hu)
        {
            lock (huListLock)
            {
                HuInfo item = huList.FirstOrDefault(i => i.HU == hu);
                if (item != null)
                    return item.Result;
                else
                    return "";
            }
        }

        /// <summary>
        /// 平库-数据检测
        /// </summary>
        /// <returns></returns>
        private bool CheckDataForPingKu()
        {
            bool result = true;
            if (!CheckDataForCommon()) return false;

            //此时仍然未获得箱码，则拉取箱码使用
            if (string.IsNullOrEmpty(this.lblBoxNo.Text))
            {
                //epc上传列表中不存在该epc，获取新箱号
                SetBoxNo("");
            }

            if (result || IsRecheck)
                RightHandle();
            else
                ErrorHandle("", 3);
            return result;
        }
        /// <summary>
        /// 高位库-数据检测
        /// </summary>
        private bool CheckDataForGaoWeiKu()
        {
            /*
            bool result = true;
            if (!CheckDataForCommon()) return false;
            if (boxNoList.Count > 0)
            {
                boxNoList.Clear();
                ErrorHandle(XIANG_MA_BU_YI_ZHI, 0);
                result = false;
            }
            if (string.IsNullOrEmpty(this.lblBoxNo.Text.Trim()))
            {
                ErrorHandle(WEI_SAO_DAO_XIANG_MA, 0);
                result = false;
            }
            //高位库 需要检测的箱子与所选行项目对应
            if (this.currentDocdetailItem != null)
            {
                if (this.lvTagDetail.Items.Count > 0)
                {
                    bool isFit = false;
                    foreach (ListViewItem item in lvTagDetail.Items)
                    {
                        string zsatnr = item.SubItems[0].Text;
                        string zcolsn = item.SubItems[1].Text;
                        string zsiztx = item.SubItems[2].Text;
                        if (currentDocdetailItem.SubItems[1].Text == zsatnr &&
                            currentDocdetailItem.SubItems[2].Text == zcolsn &&
                            currentDocdetailItem.SubItems[3].Text == zsiztx)
                        {
                            isFit = true;
                        }
                    }
                    if (!isFit)
                    {
                        ErrorHandle(HANG_XIANG_MU_BU_FU, 0);
                        result = false;
                    }
                }
            }
            else
            {
                ErrorHandle(WEI_XUAN_ZE_HANG_XIANG_MU, 0);
                result = false;
            }
            if (!string.IsNullOrEmpty(this.lblBoxNo.Text.Trim()))
            {
                lastResult = GetInventoryResultByBoxNo(this.lblBoxNo.Text.Trim());
                if (lastResult == "S")
                {
                    List<EpcDetail> epcListBefore = GetEpcdetailListByHU(this.lblBoxNo.Text.Trim());
                    //是否完全匹配
                    bool isSame = true;
                    //是否完全不匹配
                    bool isAllNotSame = true;
                    if (epcListBefore != null && epcListBefore.Count > 0)
                    {
                        if (this.epcList.Count == epcListBefore.Count)
                        {
                            foreach (EpcDetail epc in epcListBefore)
                            {
                                if (!epcList.Contains(epc.EPC_SER))
                                {
                                    isSame = false;
                                    break;
                                }
                                else
                                {
                                    isAllNotSame = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            isSame = false;
                            foreach (EpcDetail epc in epcListBefore)
                            {
                                if (epcList.Contains(epc.EPC_SER))
                                {
                                    isAllNotSame = false;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        isSame = false;
                        isAllNotSame = true;
                    }

                    if (isSame)
                    {
                        //两批EPC对比，完全一样，示为重投
                        //errorMsg = CHONG_TOU;   //高位库
                        //ShowInventoryResult(true);//高位库
                        //SetInventoryResult(0);//高位库
                        IsRecheck = true;
                        result = false;
                    }
                    else if (isAllNotSame)
                    {
                        //两批EPC对比，完全不一样，示为箱码重复使用
                        ErrorHandle(XIANG_MA_CHONG_FU_SHI_YONG, 0);
                        result = false;
                    }

                    if (epcListBefore.Count > 0 && !isSame && !isAllNotSame)
                    {
                        ErrorHandle(EPC_YI_SAO_MIAO, 0);
                        result = false;
                    }
                }
                else
                {
                    EpcDetail item = epcdetailList.Find(i => epcList.Contains(i.EPC_SER) && i.HU != this.lblBoxNo.Text.Trim() && i.Result == "S");
                    if (item != null)
                    {
                        ErrorHandle(EPC_YI_SAO_MIAO, 0);
                        result = false;
                    }
                }
            }

            if (result || IsRecheck)
                RightHandle();
            else
                ErrorHandle("", 3);
            return result;
            */
            return false;
        }
        /// <summary>
        /// 公共的数据检测方法【高位库 平库都要通过以下检测才能通过】
        /// </summary>
        /// <returns></returns>
        private bool CheckDataForCommon()
        {
            /*
            if (this.btnStart.Enabled)
            {
                ErrorHandle(WEI_KAI_SHI_JIAN_HUO, 0);
                result = false;
            }
            if (this.lblErrorNum.Text.Trim() != "0")
            {
                ErrorHandle(EPC_WEI_ZHU_CE, 0);
                result = false;
            }
            
            
            */
            //检查该箱内主条码是否全部相同
            
            TagDetailInfoExtend tdiExtend = null;
            if (this.tdiExtendList != null && this.tdiExtendList.Count > 0)
            {
                tdiExtend = this.tdiExtendList.Values.First();
            }
            if (tdiExtend != null)
            {
                string matnr = tdiExtend.MATNR;
                int pxqty = int.Parse(this.lblTotalCheckNum.Text); //自定义箱规   //tdiExtend.PXQTY;
                int normalNum = this.tdiExtendList.Count;//统计合法数量
                int tempNum = this.tdiExtendList.Values.Count(o => o.MATNR == matnr);
                int rfidEpcNum = this.tdiExtendList.Values.Count(o => o.HAS_RFID_EPC == true);//统计主条码epc数
                int rfidAddEpcNum = this.tdiExtendList.Values.Count(o => o.HAS_RFID_ADD_EPC == true);//统计辅条码epc数
                
                //如果勾选了[按箱规收货]，则判断总数量是否等于箱规
                if (this.cbUseBoxStandard.Checked)
                {
                    if (rfidEpcNum <= 0 || rfidEpcNum != pxqty)
                    {
                        ErrorHandle(BU_FU_HE_XIANG_GUI, 0);
                        ShowErrorLogForm();
                        elForm.UpdateErrorLog(lblDocNo.Text, tdiExtend, pxqty, rfidEpcNum);
                        return false;
                    }
                }

                if(rfidEpcNum>pxqty)
                {
                    ErrorHandle(SHU_LIANG_DA_YU_XIANG_GUI, 0);
                    ShowErrorLogForm();
                    elForm.UpdateErrorLogBigThanPeiZhi(lblDocNo.Text, tdiExtend, pxqty, rfidEpcNum);
                    return false;
                }
                
                //如果存在辅条码，检查主条码和辅条码数量是否一致
                if (rfidAddEpcNum > 0 && rfidEpcNum != rfidAddEpcNum)
                {
                    ErrorHandle(TWO_NUMBER_ERROR, 0);
                    ShowErrorLogForm();
                    elForm.UpdateErrorLog(lblDocNo.Text, tdiExtend, pxqty, rfidEpcNum, pxqty, rfidAddEpcNum);
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// 检查当前扫描到的数据，并设置最终结果
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (SysConfig.RunningModel == RunMode.高位库)
            {
                return CheckDataForGaoWeiKu();
            }
            else
            {
                return CheckDataForPingKu();
            }
        }
        //private void UpdateErrorMsgList(string errorMsg)
        //{
        //    if (!errorMsgList.Contains(errorMsg))
        //    {
        //        errorMsgList.Add(errorMsg);
        //    }
        //}

        /// <summary>
        /// 异常处理，检测结果为异常的情况下，调用此方法-漳州 杏林通用
        /// </summary>
        /// <param name="MsgContent"></param>
        /// <param name="inventoryResult"></param>
        private void ErrorHandle(string MsgContent, int inventoryResult)
        {
            /*
            if(!string.IsNullOrEmpty(MsgContent.Trim()) && !errorMsg.Contains(MsgContent))
                errorMsg = errorMsg + MsgContent + ";";
            if (SysConfig.RunningModel == RunMode.平库)
            {
                //平库，要为每个错误，分配一个箱码
                string boxNo = this.lblBoxNo.Text.Trim();
                if (string.IsNullOrEmpty(boxNo))
                {
                    SetBoxNo(boxNo);
                }
            }
            else if (SysConfig.RunningModel == RunMode.高位库)
            {
                string boxNo = this.lblBoxNo.Text.Trim();
                //当箱码为空且错误信息为WEI_SAO_DAO_XIANG_MA时，获取一个新的箱码
                if (string.IsNullOrEmpty(boxNo) && MsgContent == WEI_SAO_DAO_XIANG_MA)
                {
                    SetBoxNo(boxNo);
                }
            }
            SetInventoryResult(inventoryResult);
            
            if(inventoryResult!=0)*/
            ShowInventoryResult(false);
        }
        /// <summary>
        /// 正常处理，检测结果为正常的情况下，调用此方法-漳州 杏林通用
        /// </summary>
        private void RightHandle()
        {
            if (IsRecheck)
                errorMsg = CHONG_TOU;
            else
                errorMsg = RIGHT;
            ShowInventoryResult(true);
            SetInventoryResult(1);
        }
        /// <summary>
        /// 漳州通道机 设置盘点结果 - 高位库专用 
        /// </summary>
        /// <param name="result">1正常 2重检 3异常 4延时检测</param>
        /// <param name="message">错误信息</param>
        private void SetInventoryResult(int result)
        {
            this.inventoryResult.Result = result;
            this.inventoryResult.Message = errorMsg;
        }

        private string currentBoxNo = string.Empty;

        /// <summary>
        /// 停止盘点
        /// </summary>
        private bool StopInventory()
        {
            //判断是否正在盘点，正在盘点则停止盘点
            if (this.isInventory == true)
            {
                try
                {
                    this.Invoke(new Action(() =>
                    {
                        this.lblWorkStatus.Text = "停止";
                    }));
                    this.isInventory = false;//将isInventory = false提前到reader.StopInventory()之前
                    reader.StopInventory();
                    currentBoxNo = this.lblBoxNo.Text;
                    bool result = true;
                    //将要保存的错误信息加入队列
                    if(epcList.Count>0)
                    {
                        EnqueueErrorRecord();
                        ResultDataInfo rdi = GetResultData(result);
                        if (SysConfig.RunningModel == RunMode.平库)
                            PrintBoxStandard(rdi);
                        EnqueueUploadData(rdi);
                    }
                    this.Invoke(new Action(() =>
                    {
                        this.lblBoxNo.Text = "";
                    }));
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                    return false;
                }
            }
            
            return true;
        }

        private void EnqueueErrorRecord()
        {
            if (currentErrorRecordList.Count > 0)
            {
                foreach (ErrorRecord record in currentErrorRecordList)
                {
                    if (record.REMARK.EndsWith(";"))
                    {
                        record.REMARK = record.REMARK.Remove(record.REMARK.Length - 1);
                    }
                    ListViewItem error = new ListViewItem(record.HU);
                    error.SubItems.Add(record.ZSATNR);
                    error.SubItems.Add(record.ZCOLSN);
                    error.SubItems.Add(record.ZSIZTX);
                    error.SubItems.Add(record.QTY.ToString());
                    error.SubItems.Add(record.REMARK);
                    if (record.RESULT == "E")
                    {
                        error.BackColor = Color.Red;
                    }
                    //只有正常的结果才显示在界面上 
                    //add by zjr 20160130
                    if(record.RESULT == "S")
                        lvErrorRecord.Items.Insert(0, error);
                    if (record.RESULT == "E" && errorForm != null)//只显示错误记录
                        errorForm.UpdateRecordInfo(error, true);

                    lock (savingErrorRecordLockObject)
                    {
                        savingErrorRecord.Enqueue(record);
                    }
                }
            }
        }

        private void EnqueueUploadData(ResultDataInfo data)
        {
            UploadData ud = new UploadData();
            ud.Guid = Guid.NewGuid().ToString();
            ud.Data = data;
            ud.IsUpload = 0;
            ud.CreateTime = DateTime.Now;
            lock (savingDataLockObject)
            {
                savingData.Enqueue(ud);
            }
            SqliteDataService.InsertUploadData(ud);
        }
        /// <summary>
        /// 获取吊牌详细信息
        /// </summary>
        /// <param name="epc"></param>
        /// <returns></returns>
        private TagDetailInfo getTagDetailInfoByEpc(string epc)
        {
            if (string.IsNullOrEmpty(epc) || epc.Length < 20)
                return null;

            string rfidEpc = epc.Substring(0, 14) + "000000";
            string rfidAddEpc = rfidEpc.Substring(0, 14);
            HLATagInfo tag = hlaTagList.FirstOrDefault(i => i.RFID_EPC == rfidEpc || i.RFID_ADD_EPC == rfidAddEpc);
            if (tag == null)
                return null;
            else
            {
                MaterialInfo mater = materialList.FirstOrDefault(i => i.MATNR == tag.MATNR);
                if (mater == null)
                    return null;
                else
                {
                    TagDetailInfo item = new TagDetailInfo();
                    item.RFID_EPC = tag.RFID_EPC;
                    item.RFID_ADD_EPC = tag.RFID_ADD_EPC;
                    item.CHARG = tag.CHARG;
                    item.MATNR = tag.MATNR;
                    item.BARCD = tag.BARCD;
                    item.ZSATNR = mater.ZSATNR;
                    item.ZCOLSN = mater.ZCOLSN;
                    item.ZSIZTX = mater.ZSIZTX;
                    item.PXQTY = mater.PXQTY;

                    //判断是否为辅条码epc
                    if (rfidEpc == item.RFID_EPC)
                        item.IsAddEpc = false;
                    else
                        item.IsAddEpc = true;
                    return item;
                }
            }
        }
        /// <summary>
        /// 初始化检货错误记录
        /// </summary>
        private void initErrorRecord()
        {
            this.lvErrorRecord.Items.Clear();
            List<ErrorRecord> list = LocalDataService.GetErrorRecordsByDocNo(this.currentDocInfo.DOCNO, receiveType);
            if (list != null)
            {
                foreach (ErrorRecord item in list)
                {
                    ListViewItem lvi = new ListViewItem(item.HU);
                    lvi.SubItems.Add(item.ZSATNR);
                    lvi.SubItems.Add(item.ZCOLSN);
                    lvi.SubItems.Add(item.ZSIZTX);
                    lvi.SubItems.Add(item.QTY.ToString());
                    lvi.SubItems.Add(item.REMARK);
                    if (item.RESULT == "E")
                        lvi.BackColor = Color.Red;
                    //只有正常的结果才显示在界面上 
                    //add by zjr 20160130
                    if (item.RESULT == "S")
                        lvErrorRecord.Items.Add(lvi);
                    if (item.RESULT == "E" && errorForm != null)
                        errorForm.UpdateRecordInfo(lvi, false);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private ErrorLogForm elForm = new ErrorLogForm();
        private DateTime lastWarningTime = DateTime.Now;
        
        
        private void ShowErrorLogForm()
        {
            if ((DateTime.Now - lastWarningTime).TotalMilliseconds > 3000)
            {
                AudioHelper.PlayWithSystem("Resources\\warningwav.wav");
                //AudioHelper.Play(Properties.Resources.warningwav, AudioPlayMode.Background);
                lastWarningTime = DateTime.Now;
            }
            if (!elForm.Visible)
            {
                //new Thread(new ThreadStart(() => {
                    elForm.TopMost = true;
                    elForm.Show();
                //})).Start();
            }
        }

        void reader_OnTagReported(TagInfo taginfo)
        {
            try
            {
                if (!this.isInventory)
                    return;

                if (taginfo == null || string.IsNullOrEmpty(taginfo.Epc))
                    return;

                if (SysConfig.RssiLimit != 0)
                {
                    if (taginfo.Rssi < SysConfig.RssiLimit)
                        return;
                }

                if (!this.epcList.Contains(taginfo.Epc))
                {
                    this.lastReadTime = DateTime.Now;
                    //判断是否满足箱规
                    TagDetailInfo tdi = getTagDetailInfoByEpc(taginfo.Epc);

                    if (tdi == null)
                    {
                        ShowErrorLogForm();
                        elForm.UpdateErrorLog(this.lblDocNo.Text, taginfo.Epc,
                                int.Parse(lblTotalCheckNum.Text.Trim()), tdi, DialogForm.ErrorType.不在本单);
                        return;
                    }
                    else
                    {
                        //判断epc是否存在历史记录
                        if (this.epcdetailList.Exists(o => o.EPC_SER == taginfo.Epc))
                        {
                            //商品已扫描
                            ShowErrorLogForm();
                            elForm.UpdateErrorLog(this.lblDocNo.Text, taginfo.Epc,
                                int.Parse(lblTotalCheckNum.Text.Trim()), tdi, DialogForm.ErrorType.商品已扫描);
                            return;
                        }
                        //if(!tdi.IsAddEpc)
                        //{
                        //主料,判断是否串规格
                        if (tdiExtendList.Count > 0)
                        {
                            TagDetailInfoExtend tdiEx = tdiExtendList.Values.FirstOrDefault(i => i.HAS_RFID_EPC);
                            if (tdiEx != null && tdi.MATNR != tdiEx.MATNR)
                            {
                                //串规格
                                ShowErrorLogForm();
                                elForm.UpdateErrorLog(this.lblDocNo.Text, taginfo.Epc,
                            int.Parse(lblTotalCheckNum.Text.Trim()), tdi, DialogForm.ErrorType.串规格);
                                return;
                            }
                        }
                        //}

                        //判断数量是否超标
                        if (cbUseBoxStandard.Checked)
                        {
                            //如果按箱规收货，就判断是否箱规不符
                            if ((!tdi.IsAddEpc && int.Parse(lblTotalCheckNum.Text.Trim()) == int.Parse(lblScanNum.Text.Trim()))
                            || (tdi.IsAddEpc && int.Parse(lblTotalCheckNum.Text.Trim()) == int.Parse(lblScanNumEx.Text.Trim())))
                            {
                                ShowErrorLogForm();
                                elForm.UpdateErrorLog(this.lblDocNo.Text, taginfo.Epc,
                                int.Parse(lblTotalCheckNum.Text.Trim()), tdi, DialogForm.ErrorType.箱规不符);
                                return;
                            }
                        }

                        if (int.Parse(lblScanNum.Text.Trim()) > int.Parse(lblTotalCheckNum.Text.Trim()))
                        {
                            ShowErrorLogForm();
                            elForm.UpdateErrorLog(this.lblDocNo.Text, taginfo.Epc,
                            int.Parse(lblTotalCheckNum.Text.Trim()), tdi, DialogForm.ErrorType.数量大于配置);
                            return;
                        }
                    }
                    this.currentNum++;
                    this.epcList.Add(taginfo.Epc);
                    this.Invoke(new Action(() =>
                    {
                        this.lblEpcNum.Text = this.currentNum.ToString(); //更新扫描总数
                }));

                    //if (SysConfig.RunningModel == RunMode.高位库)
                    //{
                    //    if (string.IsNullOrEmpty(this.lblBoxNo.Text.Trim()))
                    //    {
                    //        ErrorHandle(WEI_SAO_DAO_XIANG_MA, 0);
                    //    }
                    //}
                    //通过epc查找详细信息，未找到即为非法

                    if (tdi != null)
                    {
                        this.Invoke(new Action(() =>
                        {
                        //如果箱规为0，则赋值箱规
                        if (this.lblBoxStandard.Text == "0")
                                this.lblBoxStandard.Text = tdi.PXQTY.ToString();

                        }));

                        if (!this.tdiExtendList.ContainsKey(taginfo.Epc))
                        {
                            this.Invoke(new Action(() =>
                            {
                            //当扫描到主条码时扫描数+1
                            if (!tdi.IsAddEpc)
                                    this.lblScanNum.Text = (int.Parse(this.lblScanNum.Text) + 1).ToString();
                                else
                                    this.lblScanNumEx.Text = (int.Parse(this.lblScanNumEx.Text) + 1).ToString();
                            }));

                            TagDetailInfoExtend tdiExtend = new TagDetailInfoExtend();
                            tdiExtend.RFID_EPC = tdi.RFID_EPC;
                            tdiExtend.RFID_ADD_EPC = tdi.RFID_ADD_EPC;
                            tdiExtend.MATNR = tdi.MATNR;
                            tdiExtend.BARCD = tdi.BARCD;
                            tdiExtend.ZSATNR = tdi.ZSATNR;
                            tdiExtend.ZCOLSN = tdi.ZCOLSN;
                            tdiExtend.ZSIZTX = tdi.ZSIZTX;
                            tdiExtend.CHARG = tdi.CHARG;
                            tdiExtend.PXQTY = tdi.PXQTY;
                            if (tdi.IsAddEpc)
                                tdiExtend.HAS_RFID_ADD_EPC = true;
                            else
                                tdiExtend.HAS_RFID_EPC = true;

                            this.tdiExtendList.Add(taginfo.Epc, tdiExtend);

                            //当扫描到主条码时，记录数量到lvTagDetail列表中
                            if (!tdi.IsAddEpc)
                            {
                                string key = string.Format("{0},{1},{2}", tdi.ZSATNR, tdi.ZCOLSN, tdi.ZSIZTX);
                                //string key = string.Format("{0},{1},{2},{3}", tdi.ZSATNR, tdi.ZCOLSN, tdi.ZSIZTX, tdi.CHARG);
                                if (!this.dicTagDetail.ContainsKey(key))
                                {
                                    ListViewItem item = new ListViewItem(tdi.ZSATNR);
                                    item.SubItems.Add(tdi.ZCOLSN);
                                    item.SubItems.Add(tdi.ZSIZTX);
                                    item.SubItems.Add(tdi.CHARG);
                                    item.SubItems.Add("1");
                                    item.Tag = tdi.MATNR;
                                    this.Invoke(new Action(() =>
                                    {
                                        this.lvTagDetail.Items.Add(item);
                                    }));

                                    this.dicTagDetail.Add(key, item);
                                }
                                else
                                {
                                    this.Invoke(new Action(() =>
                                    {
                                        ListViewItem item = this.dicTagDetail[key];
                                        item.SubItems[4].Text = (int.Parse(item.SubItems[4].Text) + 1).ToString();
                                    }));
                                }
                            }
                            //if (SysConfig.RunningModel == RunMode.高位库)
                            //    CheckNumber();  //每次识别到新的EPC，就检测一次数量，看数量是否与箱规一致{高位库}
                        }
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.lblErrorNum.Text = (int.Parse(this.lblErrorNum.Text) + 1).ToString();
                        }));
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //if (!SysConfig.IsTest)
            //{
                if (this.lblReaderStatus.Text != "正常")
            {
                AudioHelper.PlayWithSystem("Resources\\fail.wav");
                //AudioHelper.Play(Properties.Resources.fail, AudioPlayMode.Background);
                MessageBox.Show("读写器没有连接正常，请先连接读写器", "提示");
                    return;
            }
            //}


            if (this.currentDocInfo == null || string.IsNullOrEmpty(this.currentDocInfo.DOCNO))
            {
                AudioHelper.PlayWithSystem("Resources\\fail.wav");
                //AudioHelper.Play(Properties.Resources.fail, AudioPlayMode.Background);
                MessageBox.Show("交货单号为空，请先选择交货单", "提示");
                return;
            }

            if (SysConfig.RunningModel == RunMode.高位库)
            {
                if (lvDocDetail.SelectedItems.Count <= 0 || this.currentDocdetailItem == null)
                {
                    AudioHelper.PlayWithSystem("Resources\\fail.wav");
                    //AudioHelper.Play(Properties.Resources.fail, AudioPlayMode.Background);
                    MessageBox.Show("请先选择行项目", "提示");
                    return;
                }
            }
            else
                this.btnSetBoxQty.Enabled = false;
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
            this.btnSelectDocNo.Enabled = false;
            this.btnClose.Enabled = false;

            StartInventory();
            AudioHelper.PlayWithSystem("Resources\\success.wav");
            //AudioHelper.Play(Properties.Resources.success, AudioPlayMode.Background);

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            UploadForm form = new UploadForm();
            form.ShowDialog();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            EndReceive();
        }

        private void EndReceive()
        {
            if (epcList.Count > 0)
            {
                if (!CheckData()) return;
            }
            this.btnStop.Enabled = false;
            if (SysConfig.RunningModel == RunMode.高位库)
            {
                this.currentDocdetailItem = null;
                this.lblCurrentZSATNR.Text = "";
            }
            else
                this.btnSetBoxQty.Enabled = true;
            StopInventory();
            this.btnStart.Enabled = true;
            this.btnSelectDocNo.Enabled = true;
            this.btnClose.Enabled = true;
            AudioHelper.PlayWithSystem("Resources\\success.wav");
            //AudioHelper.Play(Properties.Resources.success, AudioPlayMode.Background);
        }

        /// <summary>
        /// 超过1000MS没有读到新的标签，则示为本箱已检测完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.isInventory)
            {
                //当前正在盘点，则判断上次读取时间和现在读取时间
                if (int.Parse(this.lblEpcNum.Text) < int.Parse(this.lblBoxStandard.Text))
                {
                    if ((DateTime.Now - this.lastReadTime).TotalMilliseconds >= 2000)
                    {
                        StopInventory();
                    }
                }
                else
                {
                    if ((DateTime.Now - this.lastReadTime).TotalMilliseconds >= SysConfig.DelayTime2)
                    {
                        //1S钟内没有读到新的标签，则未为本箱检测完毕，关闭射频，检查结果
                        StopInventory();
                    }
                }
            }
        }

        private void btnRecords_Click(object sender, EventArgs e)
        {
            RecordForm form = new RecordForm(receiveType);
            form.ShowDialog();
        }

        private void lvDocDetail_Validated(object sender, EventArgs e)
        {
            try
            {
                if (lvDocDetail.FocusedItem != null)
                {
                    lvDocDetail.FocusedItem.BackColor = Color.LightBlue;
                    lvDocDetail.FocusedItem.ForeColor = Color.White;
                    lvDocDetail.SelectedIndices.Add(lvDocDetail.FocusedItem.Index);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }


        private void lvDocDetail_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                ListViewItem lvi = this.lvDocDetail.GetItemAt(e.X, e.Y);
                foreach (ListViewItem item in lvDocDetail.Items)
                {
                    item.ForeColor = Color.Black;
                    item.BackColor = Color.White;
                }
                if (lvi != null && lvi.Index > -1)
                {
                    lvi.BackColor = Color.LightBlue;
                    lvi.ForeColor = Color.White;
                }
                else
                {
                    if (lvDocDetail.FocusedItem != null)
                    {
                        lvDocDetail.FocusedItem.BackColor = Color.LightBlue;
                        lvDocDetail.FocusedItem.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void lvDocDetail_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (SysConfig.RunningModel == RunMode.高位库)
            {
                if (btnStart.Enabled)
                {
                    this.lblCurrentZSATNR.Text = "品号：" + e.Item.SubItems[1].Text + " 色号：" + e.Item.SubItems[2].Text + " 规格：" + e.Item.SubItems[3].Text;
                    this.currentDocdetailItem = e.Item;
                }
            }
        }

        private void btnGX_Click(object sender, EventArgs e)
        {
            GXForm form = new GXForm();
            form.ShowDialog();
        }
#endregion

#region 测试使用相关代码
        private void button1_Click(object sender, EventArgs e)
        {
            TagInfo ti = new TagInfo();
            ti.Epc = this.textBox1.Text;
            ti.Rssi = -20;

            reader_OnTagReported(ti);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartInventory();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StopInventory();
        }
#endregion

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (errorForm != null)
            {
                errorForm.UpdateMonitor(this.lblBoxNo.Text, this.lblBoxStandard.Text, this.lblScanNum.Text,
                    this.lblErrorNum.Text, this.lblWorkStatus.Text, this.lblEpcNum.Text, this.lblInventoryResult.Text,
                    this.lblCurrentZSATNR.Text, this.lblActualTotalNum.Text, this.lblTotalBoxNum.Text, this.lvDocDetail);
            }

        }

        private void btnSetBoxQty_Click(object sender, EventArgs e)
        {
            //BoxQtyConfigForm form = new BoxQtyConfigForm(this.docDetailInfoList, this.materialList);
            //form.ShowDialog();
            int trackNum = 0;
            int eachTrackNum = 0;
            try
            {
                trackNum = int.Parse(this.txtTrackNum.Text.Trim());
            }
            catch
            {
                AudioHelper.PlayWithSystem("Resources\\fail.wav");
                //AudioHelper.Play(Properties.Resources.fail, AudioPlayMode.Background);
                MessageBox.Show("轨道数量格式错误！", "提示");
                return;
            }
            try
            {
                eachTrackNum = int.Parse(this.txtEachTrackNum.Text.Trim());
            }
            catch
            {
                AudioHelper.PlayWithSystem("Resources\\fail.wav");
                //AudioHelper.Play(Properties.Resources.fail, AudioPlayMode.Background);
                MessageBox.Show("每条轨道衣服数量格式错误！", "提示");
                return;
            }

            this.lblTotalCheckNum.Text = (trackNum * eachTrackNum).ToString();
            SetConfigValue("TrackNum", trackNum.ToString());
            SetConfigValue("EachTrackNum", eachTrackNum.ToString());
            SysConfig.TrackNum = trackNum;
            SysConfig.EachTrackNum = eachTrackNum;
            AudioHelper.PlayWithSystem("Resources\\success.wav");
            //AudioHelper.Play(Properties.Resources.success, AudioPlayMode.Background);

        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TabTip.exe");
        }

        private void btnSelectDocNo_Click(object sender, EventArgs e)
        {
            DocNoInputForm form = new DocNoInputForm(this);
            form.ShowDialog();
        }

        /// <summary>
        /// 设置配置文件的值
        /// </summary>
        /// <param name="AppKey"></param>
        /// <param name="AppValue"></param>
        public static void SetConfigValue(string AppKey, string AppValue)
        {
            XmlDocument xDoc = new XmlDocument();
            //获取可执行文件的路径和名称
            xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");

            XmlNode xNode;
            XmlElement xElem1;
            xNode = xDoc.SelectSingleNode("//appSettings");
            xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + AppKey + "']");
            if (xElem1 != null) xElem1.SetAttribute("value", AppValue);
            else
            {
                xElem1 = xDoc.CreateElement("add");
                xElem1.SetAttribute("key", AppKey);
                xElem1.SetAttribute("value", AppValue);
                xNode.AppendChild(xElem1);
            }

            xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            lblScanNum.Text = "0";
            lblScanNumEx.Text = "0";
            epcList.Clear();
            EndReceive();
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            UploadForm form = new UploadForm();
            form.ShowDialog();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (buttonUpload.InvokeRequired)
            {
                if (!buttonUpload.IsHandleCreated)
                    return;
                Invoke(new Action(() =>
                {
                    int uploadCount = savingData.Count;
                    buttonUpload.Text = string.Format("上传列表({0})", uploadCount);
                }));
            }
            else
            {
                int uploadCount = savingData.Count;
                buttonUpload.Text = string.Format("上传列表({0})", uploadCount);

            }

        }
    }
}
