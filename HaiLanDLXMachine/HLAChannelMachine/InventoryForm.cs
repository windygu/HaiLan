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
using HLACommonLib.Model.ENUM;
using HLACommonLib.Model.RECEIVE;
using HLACommonLib.DAO;
using Xindeco.Device.Interface;
using Xindeco.Device;
using OSharp.Utility.Extensions;
using System.Data.SqlClient;

namespace HLAChannelMachine
{
    public partial class InventoryForm : Form
    {
        public bool mTimeLog = false;
        CTimeLog mInventoryTimelog = new CTimeLog(false);
        CTimeLog mTimeLogCom = new CTimeLog(false);
        CTimeLog mCheckDataTimeLog = new CTimeLog(false);
        #region 属性变量

        #region 读写器相关属性
        UHFReader reader = new UHFReader(Xindeco.Device.Model.UHFReaderType.ImpinjR420);
        #endregion

        #region PLC串口相关属性
        /// <summary>
        /// PLC串口
        /// </summary>
        PLCController plc = null;
        #endregion

        #region 条码扫描模组相关属性
        /// <summary>
        /// 条码扫描模组1
        /// </summary>
        BarcodeDevice bar1 = null;
        /// <summary>
        /// 条码扫描模组2
        /// </summary>
        BarcodeDevice bar2 = null;
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
        /// 回复PLC信息的线程
        /// </summary>
        //private Thread logicThread = null;
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

        private Dictionary<string, List<string>> dicTagPici = new Dictionary<string, List<string>>();
        List<string> curPici = new List<string>();
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

        private CChaoShou mChaoShou = new CChaoShou();
        private object mChaoShouLockObject = new object();

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
        /// 配比不符
        /// </summary>
        private const string PEI_BI_BU_FU = "配比不符";
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
        /// <summary>
        /// 数量超收
        /// </summary>
        private const string SHU_LIANG_CHAO_SHOU = "数量超收({0})";
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
        #endregion

        #region InventoryForm

        public InventoryForm()
        {
            InitializeComponent();
        }

        public InventoryForm(DocInfo docInfo, List<DocDetailInfo> _docDetailInfoList, List<MaterialInfo> _materialList, List<HLATagInfo> _hlaTagInfo, List<EpcDetail> _epcdetailList)
        {
            InitializeComponent();
            this.currentDocInfo = docInfo != null ? docInfo : new DocInfo();
            this.docDetailInfoList = _docDetailInfoList != null ? _docDetailInfoList : new List<DocDetailInfo>();
            this.materialList = _materialList != null ? _materialList : new List<MaterialInfo>();
            this.hlaTagList = _hlaTagInfo != null ? _hlaTagInfo : new List<HLATagInfo>();
            this.epcdetailList = _epcdetailList != null ? _epcdetailList : new List<EpcDetail>();
        }


        private void InventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //int saveCount = savingErrorRecord.Count + savingData.Count;
            int saveCount = 0;
            List < UploadData > list = SqliteDataService.GetUnUploadDataList();
            if(list!=null)
            {
                saveCount = list.Count;
            }
            if (saveCount>0)
            {
                if (DialogResult.Cancel == MessageBox.Show(string.Format("对不起,当前有{0}条数据正在保存,需要关闭吗？", saveCount), "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    e.Cancel = true;
                    return;
                }
            }

            StopInventory();
            //关闭读写器连接
            reader.Disconnect();

            plc.Disconnect();
            if(bar1!=null)
                bar1.Disconnect();
            if(bar2!=null)
                bar2.Disconnect();
            if (this.savingDataThread != null)
                this.savingDataThread.Abort();
            SysConfig.CurrentLoginUser = null;
        }
        #endregion

        /// <summary>
        /// 是否预拼箱
        /// </summary>
        /// <returns></returns>
        private bool IsYupinxiang()
        {
            return currentDocInfo?.ZYPXFLG?.ToUpper() == "X";
        }
        /// <summary>
        /// 是否预拼箱尾箱收货
        /// </summary>
        /// <returns></returns>
        private bool IsYpxWx()
        {
            return !cbYpxWx.Checked;
        }


        #region 私有函数
        /// <summary>
        /// 加载基础数据
        /// 交货单数据
        /// 物料、吊牌数据
        /// 历史收货数据
        /// 初始化界面
        /// </summary>
        public void loadBasicInfo(DocInfo docInfo, List<DocDetailInfo> _docDetailInfoList, 
            List<MaterialInfo> _materialList, List<HLATagInfo> _hlaTagInfo, 
            List<EpcDetail> _epcdetailList, List<HuInfo> _huList,
            ReceiveType _receiveType, List<MixRatioInfo> _mixRatioList)
        {
            receiveType = _receiveType;
            huList = _huList != null ? _huList : new List<HuInfo>();
            currentDocInfo = docInfo != null ? docInfo : new DocInfo();
            docDetailInfoList = _docDetailInfoList != null ? _docDetailInfoList : new List<DocDetailInfo>();
            materialList = _materialList != null ? _materialList : new List<MaterialInfo>();
            hlaTagList = _hlaTagInfo != null ? _hlaTagInfo : new List<HLATagInfo>();
            epcdetailList = _epcdetailList != null ? _epcdetailList : new List<EpcDetail>();
            mixRatioList = _mixRatioList != null ? _mixRatioList : new List<MixRatioInfo>();
            int actualTotalNum = 0;
            int totalBoxNum = 0;
            if (IsYupinxiang())
            {
                //预拼箱
                btnSwitchStandardBox.Hide();
                cbUseBoxStandard.Hide();
                cbUseBoxStandard.Checked = true;
                btnSetBoxQty.Hide();
                btnPeibi.Show();
                btnDocDetails.Show();
                lvErrorRecord.Hide();
                lvPBErrorRecord.Show();
                cbYpxWx.Show();
                cbYpxWx.Checked = false;
                lvDocDetail.Hide();
                lvPBDetail.Show();
                lvPBDetail.Items.Clear();

                grouper2.GroupTitle = "配比明细";
                if (docDetailInfoList != null && docDetailInfoList.Count > 0)
                {
                    List<string> zpbnoList = new List<string>();

                    foreach (DocDetailInfo ddi in docDetailInfoList)
                    {
                        if (!zpbnoList.Contains(ddi.ZPBNO))
                        {
                            zpbnoList.Add(ddi.ZPBNO);
                            ListViewItem item = new ListViewItem(ddi.ITEMNO);
                            item.SubItems.Add(ddi.ZPBNO);
                            int realqty = docDetailInfoList.FindAll(j => j.ZPBNO == ddi.ZPBNO).Sum(i => i.REALQTY);
                            item.SubItems.Add(realqty.ToString());
                            lvPBDetail.Items.Add(item);
                        }
                    }
                }
            }
            else
            {
                btnSwitchStandardBox.Show();
                cbUseBoxStandard.Show();
                btnSetBoxQty.Show();
                btnPeibi.Hide();
                btnDocDetails.Hide();
                lvErrorRecord.Show();
                lvPBErrorRecord.Hide();
                lvDocDetail.Show();
                lvPBDetail.Hide();
                grouper2.GroupTitle = "交货单明细";
                cbYpxWx.Hide();
                cbYpxWx.Checked = false;

            }

            mChaoShou = getChaoShouData(docInfo.DOCNO,receiveType);

            lvDocDetail.Items.Clear();
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

                    string psgkey = getpsgKey(ddi.ZSATNR, ddi.ZCOLSN, ddi.ZSIZTX);
                    if (mChaoShou.mQty.ContainsKey(psgkey))
                    {
                        item.SubItems.Add(mChaoShou.mQty[psgkey].ToString());
                    }
                    else
                        item.SubItems.Add("0");

                    this.lvDocDetail.Items.Add(item);

                    actualTotalNum = actualTotalNum + ddi.REALQTY;
                    totalBoxNum = totalBoxNum + ddi.BOXCOUNT;
                }
            }
            lblActualTotalNum.Text = actualTotalNum.ToString();
            lblTotalBoxNum.Text = totalBoxNum.ToString();
            lblDocNo.Text = this.currentDocInfo.DOCNO; //交货单号
            lblBoxNo.Text = ""; //箱码
            lblInventoryResult.Text = ""; //扫描结果
            cbUseBoxStandard.Checked = true;
            btnSwitchStandardBox.BackColor = Color.Tan;

            lblType.Text = string.Format("{0}单号：", receiveType == ReceiveType.交货单收货 ? "交货" : "交接");
            initErrorRecord();
            currentNum = 0;
            lastResult = null;
            boxNoQueue.Clear();
            boxNoList.Clear();
            lastReadTime = DateTime.Now;
            epcList.Clear();
            dicTagDetail.Clear();
            dicTagPici.Clear();
            tdiExtendList.Clear();
            currentDocdetailItem = null;
            lblCurrentZSATNR.Text = "";
            IsRecheck = false;
            inventoryResult = new InventoryResult() { Result = 0, Message = "" };
            errorMsg = "";

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

        public void addUploadData(List<UploadData> ri)
        {
            lock (savingDataLockObject)
            {
                foreach (var v in ri)
                {
                    savingData.Enqueue(v);
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
                            LocalDataService.SaveErrorRecord(error,receiveType);
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
            boxNoQueue = SAPDataService.GetBoxNo(SysConfig.LGNUM);
        }
        DateTime readTime = DateTime.Now;
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
                    mInventoryTimelog.startTimeLog("扫描");

                    //清除当前屏幕统计数量
                    errorMsg = "";
                    epcList.Clear();
                    currentNum = 0;
                    dicTagDetail.Clear();
                    curPici.Clear();
                    dicTagPici.Clear();
                    tdiExtendList.Clear();
                    IsRecheck = false;
                    currentErrorRecordList.Clear();
                    SetInventoryResult(0);
                    lastResult = null;
                    Invoke(new Action(() =>
                    {
                        lvTagDetail.Items.Clear();
                        lblBoxNo.Text = "";
                        lblBoxStandard.Text = "0";
                        lblScanNum.Text = "0";
                        lblEpcNum.Text = "0";
                        lblErrorNum.Text = "0";
                        lblWorkStatus.Text = "正在盘点";
                        lblInventoryResult.Text = "";
                    }));
                    int i, j, k;
                    LocalDataService.GetGhostAndTrigger(out i, out j, out k);
                    reader.StartInventory(i, j, k);
                    readTime = DateTime.Now;
                    isInventory = true;
                    lastReadTime = DateTime.Now;
                    if (boxNoList.Count > 0)
                    {
                        string boxno = boxNoList.Dequeue();
                        Invoke(new Action(() =>
                        {
                            lblBoxNo.Text = boxno;
                        }));
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
                        if(IsYupinxiang())
                        {
                            if (IsYpxWx())
                                record.ZPBNO = "";
                            else
                                record.ZPBNO = currentDocdetailItem.SubItems[1].Text;
                        }
                        else
                        {
                            record.ZPBNO = "";
                        }
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
            if(lblErrorNum.Text.ToString()!="0")
            { 
                ErrorRecord record = new ErrorRecord();
                if (IsYupinxiang())
                {
                    if (IsYpxWx())
                        record.ZPBNO = "";
                    else
                        record.ZPBNO = currentDocdetailItem.SubItems[1].Text;
                }
                else
                {
                    record.ZPBNO = "";
                }
                record.HU = this.lblBoxNo.Text.Trim();
                record.QTY = int.Parse(this.lblErrorNum.Text);
                record.REMARK = errorMsg;
                record.RESULT = isSuccess ? "S" : "E";
                record.ZCOLSN = "";
                record.ZSATNR = "";
                record.ZSIZTX = "";
                record.DOCNO = this.lblDocNo.Text.Trim();
                currentErrorRecordList.Add(record);
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

                this.lblInventoryResult.Text = errorMsg;

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
        /// <param name="zpbno">数量</param>
        /// <param name="isExists">是否存在交货明细</param>
        private void UpdateDocDetailInfo(string zsatnr, string zcolsn, string zsiztx, string charg, int qty,string zpbno,DocInfo doc)
        {
            if(currentDocInfo!=null && currentDocInfo.DOCNO == doc.DOCNO)
            {
                //更新实收总数和总箱数
                int actualTotalNum = 0;
                int.TryParse(lblActualTotalNum.Text, out actualTotalNum);
                actualTotalNum = actualTotalNum + qty;
                this.lblActualTotalNum.Text = actualTotalNum.ToString();
                //int totalBoxNum = 0;
                //int.TryParse(lblTotalBoxNum.Text, out totalBoxNum);
                //totalBoxNum = totalBoxNum + 1;
                //this.lblTotalBoxNum.Text = totalBoxNum.ToString();
                this.lblTotalBoxNum.Text = docDetailInfoList.Sum(i => i.BOXCOUNT).ToString();

                bool isExists = false;
                foreach (ListViewItem docDetailItem in lvDocDetail.Items)
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
                        docDetailInfoList.Find(i => i.ZSATNR == zsatnr && i.ZCOLSN == zcolsn && i.ZSIZTX == zsiztx).REALQTY = realqty;
                        docDetailInfoList.Find(i => i.ZSATNR == zsatnr && i.ZCOLSN == zcolsn && i.ZSIZTX == zsiztx).BOXCOUNT = boxcount;
                        LocalDataService.SaveDocDetail(currentDocInfo.DOCNO, itemNo, zsatnr, zcolsn, zsiztx, charg, tempqty, qty, 1, receiveType, zpbno);
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

                    LocalDataService.SaveDocDetail(currentDocInfo.DOCNO, "", zsatnr, zcolsn, zsiztx, charg, 0, qty, 1, receiveType, zpbno);
                }
                if (!string.IsNullOrEmpty(zpbno))
                {
                    foreach (ListViewItem item in lvPBDetail.Items)
                    {
                        if (item.SubItems[1].Text == zpbno)
                        {
                            string itemNo = item.SubItems[0].Text;
                            int realqty = 0;
                            int.TryParse(item.SubItems[2].Text, out realqty);
                            realqty = realqty + qty;
                            item.SubItems[2].Text = realqty.ToString();
                            isExists = true;
                            break;
                        }
                    }

                    if (!isExists)
                    {
                        ListViewItem item = new ListViewItem("");
                        item.SubItems.Add(zpbno);
                        item.SubItems.Add(qty.ToString());
                        this.Invoke(new Action(() =>
                        {
                            lvPBDetail.Items.Add(item);
                        }));
                    }
                }
            }
            else
            {
                LocalDataService.SaveDocDetail(doc.DOCNO, "", zsatnr, zcolsn, zsiztx, charg, 0, qty, 1, receiveType, zpbno);
            }
        }

        /// <summary>
        /// 是否超收
        /// 交接单专用
        /// </summary>
        /// <param name="zsatnr">品号</param>
        /// <param name="zcolsn">色号</param>
        /// <param name="zsiztx">规格</param>
        /// <param name="qty">当前收入数量</param>
        /// <param name="errormsg">返回的错误信息</param>
        /// <returns>true则超收 false不超收</returns>
        private bool IsOvercharge(string zsatnr, string zcolsn, string zsiztx, int qty,out string errormsg)
        {
            errormsg = "";
            try
            {
                foreach (ListViewItem docDetailItem in this.lvDocDetail.Items)
                {
                    if (docDetailItem.SubItems[1].Text == zsatnr && docDetailItem.SubItems[2].Text == zcolsn
                        && docDetailItem.SubItems[3].Text == zsiztx)
                    {
                        int tempqty = 0;
                        int.TryParse(docDetailItem.SubItems[5].Text, out tempqty);
                        int realqty = 0;
                        int.TryParse(docDetailItem.SubItems[6].Text, out realqty);
                        realqty = realqty + qty;
                        errormsg = string.Format(SHU_LIANG_CHAO_SHOU, realqty - tempqty);
                        //实收>应收
                        if (realqty > tempqty)
                            return true;
                        else
                            return false;
                    }
                }
                errormsg = string.Format(SHU_LIANG_CHAO_SHOU, qty);
            }
            catch(Exception)
            {

            }
            return true;
        }

        private void UpdateHuList(string lgnum, string hu, bool result, int qty)
        {

            //HuInfo hi = new HuInfo();
            //hi.LGNUM = lgnum;
            //hi.HU = hu;
            //hi.Floor = SysConfig.Floor;
            //hi.QTY = qty;
            //hi.Result = result ? "S" : "E";
            //hi.Timestamp = DateTime.Now;
            //lock (huListLock)
            //{
            //    if (!huList.Exists(i => i.HU == hu))
            //        huList.Add(hi);
            //    else
            //    {
            //        huList.Find(i => i.HU == hu).Result = hi.Result;
            //        huList.Find(i => i.HU == hu).QTY = hi.QTY;
            //    }
            //}

        }

        private ResultDataInfo GetResultData(bool inventoryResult)
        {
            ResultDataInfo result = new ResultDataInfo();
            result.BoxNO = this.lblBoxNo.Text.Trim();
            result.CurrentNum = this.currentNum;
            result.CurrentUserId = SysConfig.CurrentLoginUser.UserId;
            result.Doc = currentDocInfo == null ? null :this.currentDocInfo.Clone() as DocInfo;
            result.EpcList = new List<string>(this.epcList);
            if(IsYupinxiang())
            {
                if (IsYpxWx())
                    result.ZPBNO = "";
                else
                    result.ZPBNO = currentDocdetailItem != null ? currentDocdetailItem.SubItems[1].Text : "";
            }
            else
            {
                result.ZPBNO = "";
            }
            if (errorMsg.EndsWith(";"))
                errorMsg = errorMsg.Remove(errorMsg.Length - 1);
            result.ErrorMsg = this.errorMsg;
            result.Floor = SysConfig.Floor;
            result.InventoryResult = IsRecheck ? true : inventoryResult;
            result.IsRecheck = this.IsRecheck;
            result.LastResult = this.lastResult;
            result.LGNUM = SysConfig.LGNUM;
            result.ReceiveType = (int)receiveType;
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
            if (result.IsRecheck == true)
            {
                if (result.LvTagInfo.Count>0)
                {
                    if(!string.IsNullOrEmpty(result.ZPBNO))
                    {
                        CommonUtils.PrintRightBoxTagWithPBNO(result.BoxNO, result.ZPBNO, result.CurrentNum);
                    }
                    else
                    {
                        CommonUtils.PrintRightBoxTagV2(result.InventoryResult, result.BoxNO, result.LvTagInfo);
                    }
                }
                return;
            }
            if (result.InventoryResult && result.LvTagInfo.Count > 0)
            {
                if (!string.IsNullOrEmpty(result.ZPBNO))
                {
                    CommonUtils.PrintRightBoxTagWithPBNO(result.BoxNO, result.ZPBNO, result.CurrentNum);
                }
                else
                {
                    CommonUtils.PrintRightBoxTagV2(result.InventoryResult, result.BoxNO, result.LvTagInfo);
                }
            }
            else if (result.InventoryResult == false)
            {
                CommonUtils.PrintErrorBoxTagV2(result.InventoryResult, result.BoxNO, result.LvTagInfo);
            }
        }

        private void delDataWhenErrorInSap(ResultDataInfo result)
        {
            try
            {
                if (result.InventoryResult)
                {
                    subToChaoShou(result.TdiExtendList);
                    LocalDataService.DeleteEpcDetail(result.Doc.DOCNO, result.BoxNO, result.ReceiveType == 1 ? ReceiveType.交接单收货 : ReceiveType.交货单收货);
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// 保存盘点结果数据
        /// </summary>
        private void SaveData(UploadData data)
        {
            try
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
                    if (data.IsUpload == 0)
                    {
                        if (result.InventoryResult || result.ErrorMsg.Contains(EPC_YI_SAO_MIAO))
                        {
                            //保存epc流水号明细
                            LocalDataService.SaveEpcDetail(result.InventoryResult, result.LGNUM,
                                (result.Doc == null || result.Doc.DOCNO == null) ? "" : result.Doc.DOCNO,
                                (result.Doc == null || result.Doc.DOCTYPE == null) ? "" : result.Doc.DOCTYPE,
                                result.BoxNO, result.EpcList,
                                result.ReceiveType == 1 ? ReceiveType.交接单收货 : ReceiveType.交货单收货);
                        }

                        LocalDataService.SaveInventoryResult(result.LGNUM, result.BoxNO,
                                                            result.InventoryResult, result.CurrentNum,
                                                            result.ReceiveType == 1 ? ReceiveType.交接单收货 : ReceiveType.交货单收货);
                        UpdateHuList(result.LGNUM, result.BoxNO, result.InventoryResult, result.CurrentNum);
                    }
                    //有添加设备终端号
                    SapResult uploadResult;
                    if (result.ReceiveType == 1)
                        uploadResult = SAPDataService.UploadTransferBoxInfo(result.LGNUM,
                            result.Doc == null ? "" : result.Doc.DOCNO, result.BoxNO,
                            result.InventoryResult, result.ErrorMsg, result.TdiExtendList,
                            result.RunningMode, result.CurrentUserId, result.Floor, result.sEQUIP_HLA);
                    else
                    {
                        uploadResult = SAPDataService.UploadBoxInfo(result.LGNUM,
                                result.Doc == null ? "" : result.Doc.DOCNO, result.BoxNO,
                                result.InventoryResult, result.ErrorMsg, result.TdiExtendList,
                                result.RunningMode, result.CurrentUserId, result.Floor, result.sEQUIP_HLA, result.ZPBNO != null ? result.ZPBNO : "");
                    }

                    if (!uploadResult.SUCCESS)
                    {
                        //delDataWhenErrorInSap(result);
                        //上传数据失败，上传到本地服务器
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
                        if(ReceiveService.SaveUploadData(xddata))
                        {
                            //UploadedHandler(data.Guid);
                        }



                        return;
                    }
                    else
                    {
                        UploadedHandler(data.Guid);
                    }
                    //LocalDataService.SaveInventoryResult(result.LGNUM, result.BoxNO,
                    //    result.InventoryResult, result.CurrentNum,
                    //    result.ReceiveType == 1 ? ReceiveType.交接单收货 : ReceiveType.交货单收货);
                    //UpdateHuList(result.LGNUM, result.BoxNO, result.InventoryResult, result.CurrentNum);
                }

                /*如果为重投，则删除原有epcdetail数据
                if (result.IsRecheck)
                {
                    LocalDataService.DeleteEpcDetail(result.LGNUM, result.Doc.DOCNO, result.Doc.DOCTYPE, result.BoxNO);
                    epcdetailList.RemoveAll(i => i.LGNUM == result.LGNUM && i.DOCNO == result.Doc.DOCNO && i.HU == result.BoxNO);
                }*/

                ////保存epc流水号明细
                //LocalDataService.SaveEpcDetail(result.InventoryResult, result.LGNUM,
                //    (result.Doc == null || result.Doc.DOCNO == null) ? "" : result.Doc.DOCNO,
                //    (result.Doc == null || result.Doc.DOCTYPE == null) ? "" : result.Doc.DOCTYPE,
                //    result.BoxNO, result.EpcList,
                //    result.ReceiveType == 1 ? ReceiveType.交接单收货 : ReceiveType.交货单收货);
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
                        epcdetailList.Add(epcDetail);
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

                        if (lvDocDetail.InvokeRequired)
                        {
                            Invoke(new Action(() =>
                            {
                                UpdateDocDetailInfo(zsatnr, zcolsn, zsiztx, charg, qty, result.ZPBNO, result.Doc);
                            }));
                        }
                        else
                        {
                            UpdateDocDetailInfo(zsatnr, zcolsn, zsiztx, charg, qty, result.ZPBNO, result.Doc);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" +ex.Source + "\r\n" + ex.StackTrace);
                LogHelper.WriteLine(JsonConvert.SerializeObject(data));
            }
            
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

        private List<EpcDetail> GetHistoryEpcDetailListy()
        {
            if (epcList?.Count <= 0)
                return null;
            List<EpcDetail> result = epcdetailList.FindAll(i => epcList.Contains(i.EPC_SER) && i.Result == "S");
            if (result?.Count <= 0)
            {
                if (currentDocInfo != null && currentDocInfo.DOCTYPE.Trim() == "DI21")
                {
                    result = ReceiveService.GetBeforeEpcDetailByEpcList(lblDocNo.Text, epcList, receiveType);
                }
            }

            return result;
        }

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
            CheckDataForCommon(out result);
            string boxNo = "";
            //EpcDetail tempEpcDetail = GetEpcDetailByEpcList(this.epcList);
            List<EpcDetail> epcListBefore = GetHistoryEpcDetailListy();
            if (epcListBefore?.Count>0)
            {
                //epc上传列表中存在该epc，表示此箱货物已检测，获取原有箱号
                //lastResult = GetInventoryResultByBoxNo(tempEpcDetail.HU);
                //if (lastResult == "S")
                //{
                    boxNo = epcListBefore.FirstOrDefault().HU;
                    this.Invoke(new Action(() =>
                    {
                        this.lblBoxNo.Text = boxNo;
                    }));
                //}
            }
            //此时仍然未获得箱码，则拉取箱码使用
            if (string.IsNullOrEmpty(boxNo))
            {
                //epc上传列表中不存在该epc，获取新箱号
                SetBoxNo(boxNo);
            }
            else
            {
                if (epcListBefore?.Count > 0)
                {
                    if (epcListBefore.Select(i => i.HU).Distinct().Count() > 1 || epcListBefore.FirstOrDefault().HU != lblBoxNo.Text.Trim())
                    {
                        //商品已扫描
                        ErrorHandle(EPC_YI_SAO_MIAO, 0);
                        result = false;
                    }
                    else
                    {
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
                }
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
            
            bool result = true;

            mTimeLogCom.startTimeLog("CheckDataForCommon");
            CheckDataForCommon(out result);
            mTimeLogCom.stopTimeLog("CheckDataForCommon");

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

                LocalDataService.InsertErrorDataRecord(ERRORDATATYPE.未扫描到箱号, "设备号：" + lblDeviceNo.Text + " 楼层号：" + lblLouceng.Text);
            }
            else
            {
                LocalDataService.InsertErrorDataRecord(ERRORDATATYPE.正常, "设备号：" + lblDeviceNo.Text + " 楼层号：" + lblLouceng.Text);
            }
            //高位库 需要检测的箱子与所选行项目对应
            if (this.cbUseBoxStandard.Checked && receiveType == ReceiveType.交货单收货)
            {
                if (this.currentDocdetailItem != null)
                {
                    if(!IsYupinxiang())
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
                    
                }
                else
                {
                    ErrorHandle(WEI_XUAN_ZE_HANG_XIANG_MU, 0);
                    result = false;
                }
            }
            if (!string.IsNullOrEmpty(lblBoxNo.Text.Trim()))
            {
                mTimeLogCom.startTimeLog("GetHistoryEpcDetailListy");
                List<EpcDetail> epcListBefore = GetHistoryEpcDetailListy();
                mTimeLogCom.stopTimeLog("GetHistoryEpcDetailListy");

                if(epcListBefore?.Count>0)
                {
                    if(epcListBefore.Select(i=>i.HU).Distinct().Count()>1 || epcListBefore.FirstOrDefault().HU != lblBoxNo.Text.Trim())
                    {
                        //商品已扫描
                        ErrorHandle(EPC_YI_SAO_MIAO, 0);
                        result = false;
                    }
                    else
                    {
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
                }
                else
                {
                    mTimeLogCom.startTimeLog("hasExistHu");


                    if (LocalDataService.hasExistHu(SysConfig.LGNUM,lblBoxNo.Text.Trim(), receiveType))
                    {
                        //两批EPC对比，完全不一样，示为箱码重复使用
                        ErrorHandle(XIANG_MA_CHONG_FU_SHI_YONG, 0);
                        result = false;
                    }

                    mTimeLogCom.stopTimeLog("hasExistHu");
                }

                //lastResult = GetInventoryResultByBoxNo(this.lblBoxNo.Text.Trim());
                //if (lastResult == "S")
                //{
                //    //List<EpcDetail> epcListBefore = GetEpcdetailListByHU(this.lblBoxNo.Text.Trim());
                //    //是否完全匹配
                    
                //}
                //else
                //{
                //    EpcDetail item = epcdetailList.Find(i => epcList.Contains(i.EPC_SER) && i.HU != this.lblBoxNo.Text.Trim() && i.Result == "S");
                //    if (item != null)
                //    {
                //        ErrorHandle(EPC_YI_SAO_MIAO, 0);
                //        result = false;
                //    }
                //}
            }

            mTimeLogCom.startTimeLog("ResultHandle");

            if (result || IsRecheck)
                RightHandle();
            else
                ErrorHandle("", 3);

            mTimeLogCom.stopTimeLog("ResultHandle");

            return result;
        }
        /// <summary>
        /// 公共的数据检测方法【高位库 平库都要通过以下检测才能通过】
        /// </summary>
        /// <returns></returns>
        /// 
        bool isAJT()
        {
            if (SysConfig.LGNUM == "ET01")
            {
                return true;
            }

            return false;
        }
        private void CheckDataForCommon(out bool result)
        {
            result = true;
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

            if (this.epcList.Count == 0)
            {
                ErrorHandle(WEI_SAO_DAO_EPC, 0);
                result = false;
            }

            /*
            int piciRe = checkPiCi();
            if (piciRe == 1)
            {
                ErrorHandle("多个批次", 0);
                result = false;

            }
            if (piciRe == 2)
            {
                ErrorHandle("批次不一致", 0);
                result = false;
            }
            */
            if(checkPiCiNotSame())
            {
                ErrorHandle("批次不一致", 0);
                result = false;
            }

            /*
            if (checkChaoSouAjt2() || checkChaoSouAjt())
            {
                ErrorHandle("超收", 0);
                result = false;
            }
            */


            //检查该箱内主条码是否全部相同
            TagDetailInfoExtend tdiExtend = null;
            string zpbno = string.Empty;
            List<MixRatioInfo> mixs = null;
            if (IsYupinxiang() && !IsYpxWx())
            {
                zpbno = currentDocdetailItem != null ? currentDocdetailItem.SubItems[1].Text : "";
                mixs = mixRatioList.FindAll(i => i.ZPBNO == zpbno);
            }

            if (this.tdiExtendList != null && this.tdiExtendList.Count > 0)
            {
                tdiExtend = this.tdiExtendList.Values.First();
            }
            if (tdiExtend != null)
            {
                string matnr = tdiExtend.MATNR;
                int pxqty = 0;
                if (IsYupinxiang() && !IsYpxWx())
                {
                    if (mixs != null && mixs.Count > 0)
                    {
                        pxqty = mixs.Sum(i => i.QUAN);
                    }
                }
                else
                {
                    pxqty = tdiExtend.PXQTY;
                }
                int normalNum = this.tdiExtendList.Count;//统计合法数量
                int tempNum = this.tdiExtendList.Values.Count(o => o.MATNR == matnr);
                int rfidEpcNum = this.tdiExtendList.Values.Count(o => o.HAS_RFID_EPC == true);//统计主条码epc数
                int rfidAddEpcNum = this.tdiExtendList.Values.Count(o => o.HAS_RFID_ADD_EPC == true);//统计辅条码epc数

                if (normalNum != tempNum)
                {
                    //不是预拼箱，或者是预拼箱尾箱，则需要判断是否串规格
                    if (!IsYupinxiang() || IsYpxWx())
                    {
                        ErrorHandle(CUAN_GUI_GE, 0);
                        result = false;
                    }
                }

                if (IsYupinxiang() && IsYpxWx())
                {
                    if (currentNum > pxqty)
                    {
                        ErrorHandle(SHU_LIANG_DA_YU_XIANG_GUI, 0);
                        result = false;
                    }
                }

                //如果勾选了[按箱规收货]，则判断总数量是否等于箱规
                if (this.cbUseBoxStandard.Checked)
                {
                    if (!IsYupinxiang())
                    {
                        if (currentNum <= 0 || currentNum != pxqty)
                        {
                            ErrorHandle(BU_FU_HE_XIANG_GUI, 0);
                            result = false;
                        }
                    }
                }
                else
                {
                    //if (SysConfig.RunningModel == RunMode.高位库)
                    {
                        //如果是高位库，则判断数量是否大于箱规
                        if (currentNum > pxqty)
                        {
                            ErrorHandle(SHU_LIANG_DA_YU_XIANG_GUI, 0);
                            result = false;
                        }
                    }
                }
                //如果存在辅条码，检查主条码和辅条码数量是否一致
                if (rfidAddEpcNum > 0 && rfidEpcNum != rfidAddEpcNum)
                {
                    ErrorHandle(TWO_NUMBER_ERROR, 0);
                    result = false;
                }

                if (/*isAJT() ||*/ receiveType == ReceiveType.交接单收货)
                {
                    //只有交接单收货才需要判断是否超收
                    string msg;
                    if (IsOvercharge(tdiExtend.ZSATNR, tdiExtend.ZCOLSN, tdiExtend.ZSIZTX, rfidEpcNum,out msg))
                    {
                        ErrorHandle(msg, 0);
                        result = false;
                    }
                }
            }

            if(IsYupinxiang() && !IsYpxWx())
            {
                //预拼箱的情况下做是否装箱不符的判断
                bool isPinXiangFit = true;
                if(lvTagDetail.Items.Count>0)
                {


                    if (lvTagDetail.Items.Count != mixs.Count)
                    {
                        //LogHelper.WriteLine(lvTagDetail.Items.Count.ToString() + "   " + mixs.Count);
                        isPinXiangFit = false;
                    }
                    if(isPinXiangFit)
                    {
                        if (mixs != null && mixs.Count > 0)
                        {
                            foreach (ListViewItem lvi in lvTagDetail.Items)
                            {
                                if(mixs.Exists(i=>i.MATNR.ToUpper() == lvi.Tag.ToString().ToUpper()))
                                {
                                    if(lvi.SubItems[4].Text != mixs.Find(i=>i.MATNR.ToUpper() == lvi.Tag.ToString().ToUpper()).QUAN.ToString())
                                    {
                                        //LogHelper.WriteLine(lvi.SubItems[4].Text + "\r\n" + mixs.Find(i => i.MATNR.ToUpper() == lvi.Tag.ToString().ToUpper()).QUAN.ToString());
                                        isPinXiangFit = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    //LogHelper.WriteLine("产品编码：" + lvi.Tag.ToString());
                                    isPinXiangFit = false;
                                    break;
                                }
                            }
                        }
                    }
                    
                }


                if(!isPinXiangFit)
                {
                    ErrorHandle(PEI_BI_BU_FU, 0);
                    result = false;
                }

            }

        }
        private bool checkPiCiNotSame()
        {
            try
            {
                //if (SysConfig.LGNUM == "ET01")
                /*
                    if(dicTagPici.Count!=1)
                    {
                        re = 1;
                    }
                    else
                    {
                        var v = dicTagPici.First();
                        if (v.Value.Count != 1)
                        {
                            re = 1;
                        }
                        else
                        {
                            string pici = v.Value.First();
                            foreach (var doc in docDetailInfoList)
                            {
                                string key = string.Format("{0},{1},{2}", doc.ZSATNR, doc.ZCOLSN, doc.ZSIZTX);
                                if (v.Key == key)
                                {
                                    if (pici != doc.ZCHARG)
                                    {
                                        re = 2;
                                    }
                                    break;
                                }
                            }

                        }
                    
                }
                */

                foreach (var doc in docDetailInfoList)
                {
                    foreach (var pc in curPici)
                    {
                        if (pc != doc.ZCHARG)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }

            return false;
        }
        private bool checkChaoSouAjt()
        {
            bool re = false;

            try
            {
                this.Invoke(new Action(() =>
                {

                    if (SysConfig.LGNUM == "ET01")
                    {
                        foreach (ListViewItem item in lvTagDetail.Items)
                        {
                            string zsatnr = item.SubItems[0].Text;
                            string zcolsn = item.SubItems[1].Text;
                            string zsiztx = item.SubItems[2].Text;
                            string charg = item.SubItems[3].Text;
                            int qty = int.Parse(item.SubItems[4].Text);

                            foreach (ListViewItem docDetailItem in lvDocDetail.Items)
                            {
                                if (docDetailItem.SubItems[1].Text == zsatnr && docDetailItem.SubItems[2].Text == zcolsn
                                    && docDetailItem.SubItems[3].Text == zsiztx)
                                {
                                    int shouldqty = 0;
                                    int.TryParse(docDetailItem.SubItems[5].Text, out shouldqty);

                                    int realqty = 0;
                                    int.TryParse(docDetailItem.SubItems[6].Text, out realqty);

                                    if (realqty + qty > shouldqty)
                                    {
                                        re = true;
                                    }
                                }
                            }
                        }
                    }

                }));
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }

            return re;

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

        /// <summary>
        /// 异常处理，检测结果为异常的情况下，调用此方法-漳州 杏林通用
        /// </summary>
        /// <param name="MsgContent"></param>
        /// <param name="inventoryResult"></param>
        private void ErrorHandle(string MsgContent, int inventoryResult)
        {
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
                //当箱码为空且错误信息为WEI_SAO_DAO_XIANG_MA时，获取一个新的错误箱码
                if (string.IsNullOrEmpty(boxNo) && MsgContent == WEI_SAO_DAO_XIANG_MA)
                {
                    string hu = LocalDataService.GetNewErrorHu(SysConfig.DeviceNO);
                    this.Invoke(new Action(() =>
                    {
                        this.lblBoxNo.Text = hu;
                    }));
                }
            }
            SetInventoryResult(inventoryResult);
            if(inventoryResult!=0)
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
                    mInventoryTimelog.stopTimeLog("扫描");

                    this.Invoke(new Action(() =>
                    {
                        this.lblWorkStatus.Text = "停止";
                    }));
                    this.isInventory = false;//将isInventory = false提前到reader.StopInventory()之前
                    reader.StopInventory();
                    currentBoxNo = this.lblBoxNo.Text;

                    mCheckDataTimeLog.startTimeLog("checkData");
                    bool result = CheckData();
                    mCheckDataTimeLog.stopTimeLog("checkData");

                    //addChaoShao(result);

                    //将要保存的错误信息加入队列
                    EnqueueErrorRecord();
                    //将要保存的数据加入队列
                    ResultDataInfo rdi = GetResultData(result);
                    if (SysConfig.RunningModel == RunMode.平库 || SysConfig.LGNUM == "ET01")
                        PrintBoxStandard(rdi);
                    EnqueueUploadData(rdi);
                    Invoke(new Action(() =>
                    {
                        lblBoxNo.Text = "";
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
        private void addChaoShao(bool result)
        {
            if (SysConfig.LGNUM != "ET01")
                return;

            try
            {
                if(!IsRecheck && result)
                {
                    addToChaoShou(tdiExtendList);
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
        }
        private List<string> getSelectHu()
        {
            List<string> re = new List<string>();

            try
            {
                if (IsYupinxiang())
                {
                    foreach(ListViewItem item in lvPBErrorRecord.SelectedItems)
                    {
                        string hu = item.SubItems[0].Text;
                        if(!string.IsNullOrEmpty(hu))
                        {
                            re.Add(hu);
                        }
                    }
                }
                else
                {
                    foreach (ListViewItem item in lvErrorRecord.SelectedItems)
                    {
                        string hu = item.SubItems[0].Text;
                        if (!string.IsNullOrEmpty(hu))
                        {
                            re.Add(hu);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Log4netHelper.LogError(e);
            }
            return re;
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
                    if(IsYupinxiang())
                    {
                        ListViewItem error = new ListViewItem(record.HU);
                        error.SubItems.Add(record.ZPBNO);
                        error.SubItems.Add(record.ZSATNR);
                        error.SubItems.Add(record.ZCOLSN);
                        error.SubItems.Add(record.ZSIZTX);
                        error.SubItems.Add(record.QTY.ToString());
                        error.SubItems.Add(record.REMARK);
                        if (record.RESULT == "E")
                        {
                            error.BackColor = Color.Red;
                        }
                        lvPBErrorRecord.Items.Insert(0, error);
                    }
                    else
                    {
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
                        lvErrorRecord.Items.Insert(0, error);
                    }
                    

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
            List<ErrorRecord> list = LocalDataService.GetErrorRecordsByDocNo(this.currentDocInfo.DOCNO, receiveType);

            if(IsYupinxiang())
                lvPBErrorRecord.Items.Clear();
            else
                lvErrorRecord.Items.Clear();
            if (list != null)
            {
                if (IsYupinxiang())
                {
                    foreach (ErrorRecord item in list)
                    {
                        ListViewItem lvi = new ListViewItem(item.HU);
                        lvi.SubItems.Add(item.ZPBNO);
                        lvi.SubItems.Add(item.ZSATNR);
                        lvi.SubItems.Add(item.ZCOLSN);
                        lvi.SubItems.Add(item.ZSIZTX);
                        lvi.SubItems.Add(item.QTY.ToString());
                        lvi.SubItems.Add(item.REMARK);
                        if (item.RESULT == "E")
                            lvi.BackColor = Color.Red;
                        lvPBErrorRecord.Items.Add(lvi);
                    }
                }
                else
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
                        lvErrorRecord.Items.Add(lvi);
                    }
                }
            }
        }
        /// <summary>
        /// 初始化PLC串口通信
        /// </summary>
        private void initPlcPort()
        {
            #region 连接串口
            plc = new PLCController(SysConfig.Port);
            plc.OnPLCDataReported += Plc_OnPLCDataReported;
            bool isOpen = false;
            try
            {
                isOpen = plc.Connect();
            }
            catch { }

            if (!isOpen)
            {
                lblPlcStatus.Text = "异常";
                lblPlcStatus.ForeColor = Color.OrangeRed;
                MessageBox.Show("连接PLC失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                lblPlcStatus.Text = "正常";
                lblPlcStatus.ForeColor = Color.Black;
            }

            #endregion
        }

        private void Plc_OnPLCDataReported(Xindeco.Device.Model.PLCData plcData)
        {
            if (plcData.Command == Xindeco.Device.Model.PLCRequest.OPEN_RFID)
            {
                StartInventory();
            }
            else if (plcData.Command == Xindeco.Device.Model.PLCRequest.ASK_RESULT)
            {
                int inventoryResult = this.inventoryResult.Result;
                switch (inventoryResult)
                {
                    case 1://正常
                        StopInventory();
                        plc.SendCommand(Xindeco.Device.Model.PLCResponse.RIGHT);
                        break;
                    case 3://异常
                        StopInventory();
                        plc.SendCommand(Xindeco.Device.Model.PLCResponse.ERROR);
                        break;
                    default:

                        break;
                }
            }
        }

        /// <summary>
        /// 初始化扫描模组串口
        /// </summary>
        private void initScannerPort()
        {
            #region 连接条码扫描器串口-add by jrzhuang
            bar1 = new BarcodeDevice(SysConfig.ScannerPort_1);
            bar1.OnBarcodeReported += Bar1_OnBarcodeReported;
            bool isOpen = false;
            try
            {
                isOpen = bar1.Connect();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            if (!isOpen)
            {
                MessageBox.Show("连接条码扫描枪串口设备1失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            bar2 = new BarcodeDevice(SysConfig.ScannerPort_2);
            bar2.OnBarcodeReported += Bar2_OnBarcodeReported;
            try
            {
                isOpen = bar2.Connect();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            if (!isOpen)
            {
                MessageBox.Show("连接条码扫描枪串口设备2失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #endregion
        }

        private void Bar2_OnBarcodeReported(string barcode)
        {
            if (!boxNoList.Contains(barcode))
            {
                boxNoList.Enqueue(barcode);
            }
        }

        private void Bar1_OnBarcodeReported(string barcode)
        {
            if (!boxNoList.Contains(barcode))
            {
                boxNoList.Enqueue(barcode);
            }
        }

        
        #endregion

        #region 事件
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (currentDocInfo == null || lvDocDetail.Items.Count <= 0)
            {
                MessageBox.Show("请先选择交货单", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnInputDoc.Focus();
                return;
            }
            if (SysConfig.RunningModel == RunMode.高位库)
            {
                if (cbUseBoxStandard.Checked)
                {
                    if(receiveType != ReceiveType.交接单收货)
                    {
                        if (currentDocdetailItem == null)
                        {
                            MessageBox.Show("请先选择行项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }
            else if (IsYupinxiang())
            {
                if(!IsYpxWx())
                {
                    if (currentDocdetailItem == null)
                    {
                        MessageBox.Show("请先选择行项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            else
                this.btnSetBoxQty.Enabled = false;
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            UploadForm form = new UploadForm(receiveType,this);
            form.ShowDialog();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
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
            Xindeco.Device.Model.TagInfo ti = new Xindeco.Device.Model.TagInfo();
            ti.Epc = this.textBox1.Text;
            ti.Rssi = -20;
            Reader_OnTagReported(ti);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //timer1.Stop();
            StartInventory();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StopInventory();
        }
        #endregion

        private void timer2_Tick(object sender, EventArgs e)
        {
        }

        private void btnSetBoxQty_Click(object sender, EventArgs e)
        {
            BoxQtyConfigForm form = new BoxQtyConfigForm(this.docDetailInfoList, this.materialList);
            form.ShowDialog();
        }

        private void btnSwitchStandardBox_Click(object sender, EventArgs e)
        {
            cbUseBoxStandard.Checked = !cbUseBoxStandard.Checked;
        }

        private void btnInputDoc_Click(object sender, EventArgs e)
        {
            DocNoInputForm form = new DocNoInputForm(this);
            form.ShowDialog();
        }

        private void InventoryForm_Shown(object sender, EventArgs e)
        {
#if DEBUG
            grouper3.Show();
#endif

            lblCurrentUserNo.Text = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "";
            lblDeviceNo.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.EQUIP_HLA : "";
            lblLouceng.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.LOUCENG : "";

            initSavingData();
            initPlcPort();

            if (SysConfig.RunningModel == RunMode.高位库)
                initScannerPort();


            #region 连接读写器
            reader.OnTagReported += Reader_OnTagReported;
            //reader.OnTagReported += new TagReportedHandler(reader_OnTagReported);
            bool result = reader.Connect(SysConfig.ReaderIp, Xindeco.Device.Model.ConnectType.TCP, SynchronizationContext.Current);
            isReaderConnect = result;
            if (!result)
            {
                lblReaderStatus.Text = "异常";
                lblReaderStatus.ForeColor = Color.OrangeRed;
                MessageBox.Show("连接读写器失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LocalDataService.InsertErrorDataRecord(ERRORDATATYPE.读写器未连接, "设备号：" + lblDeviceNo.Text + " 楼层号：" + lblLouceng.Text);

                //return;
            }
            else
            {
                lblReaderStatus.Text = "正常";
                lblReaderStatus.ForeColor = Color.Black;
                Xindeco.Device.Model.ReaderConfig config = new Xindeco.Device.Model.ReaderConfig();
                config.SearchMode = SysConfig.ReaderConfig.SearchMode;
                config.Session = SysConfig.ReaderConfig.Session;
                if (config.AntennaList == null) config.AntennaList = new List<Xindeco.Device.Model.ReaderAntenna>();
                if (SysConfig.ReaderConfig.UseAntenna1)
                    config.AntennaList.Add(new Xindeco.Device.Model.ReaderAntenna(1, true, SysConfig.ReaderConfig.AntennaPower1));
                else
                    config.AntennaList.Add(new Xindeco.Device.Model.ReaderAntenna(1, false, SysConfig.ReaderConfig.AntennaPower1));

                if (SysConfig.ReaderConfig.UseAntenna2)
                    config.AntennaList.Add(new Xindeco.Device.Model.ReaderAntenna(2, true, SysConfig.ReaderConfig.AntennaPower2));
                else
                    config.AntennaList.Add(new Xindeco.Device.Model.ReaderAntenna(2, false, SysConfig.ReaderConfig.AntennaPower2));

                if (SysConfig.ReaderConfig.UseAntenna3)
                    config.AntennaList.Add(new Xindeco.Device.Model.ReaderAntenna(3, true, SysConfig.ReaderConfig.AntennaPower3));
                else
                    config.AntennaList.Add(new Xindeco.Device.Model.ReaderAntenna(3, false, SysConfig.ReaderConfig.AntennaPower3));

                if (SysConfig.ReaderConfig.UseAntenna4)
                    config.AntennaList.Add(new Xindeco.Device.Model.ReaderAntenna(4, true, SysConfig.ReaderConfig.AntennaPower4));
                else
                    config.AntennaList.Add(new Xindeco.Device.Model.ReaderAntenna(4, false, SysConfig.ReaderConfig.AntennaPower4));
                reader.SetParameter(config);
            }
            #endregion

            //this.logicThread = new Thread(new ThreadStart(LogicThreadFunc));
            //this.logicThread.IsBackground = true;
            //this.logicThread.Start();
            this.savingDataThread = new Thread(new ThreadStart(savingDataThreadFunc));
            this.savingDataThread.IsBackground = true;
            this.savingDataThread.Start();
        }

        private void Reader_OnTagReported(Xindeco.Device.Model.TagInfo taginfo)
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
                this.currentNum++;
                this.epcList.Add(taginfo.Epc);
                this.Invoke(new Action(() =>
                {
                    this.lblEpcNum.Text = this.currentNum.ToString(); //更新扫描总数
                }));
                //通过epc查找详细信息，未找到即为非法
                TagDetailInfo tdi = getTagDetailInfoByEpc(taginfo.Epc);
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
                            {
                                this.lblScanNum.Text = (int.Parse(this.lblScanNum.Text) + 1).ToString();
                            }
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

                        addToTagPici("", tdi.CHARG);

                        //当扫描到主条码时，记录数量到lvTagDetail列表中
                        if (!tdi.IsAddEpc)
                        {
                            string key = string.Format("{0},{1},{2}", tdi.ZSATNR, tdi.ZCOLSN, tdi.ZSIZTX);
                            
                            //string key = string.Format("{0},{1},{2},{3}", tdi.ZSATNR, tdi.ZCOLSN, tdi.ZSIZTX, tdi.CHARG);
                            if (!this.dicTagDetail.ContainsKey(key))
                            {
                                //LogHelper.WriteLine(key);

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
                    }
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        this.lblErrorNum.Text = (int.Parse(this.lblErrorNum.Text) + 1).ToString();
                    }));
                    string epcAdd = taginfo.Epc.Substring(0, 14);
                    string epc = epcAdd + "000000";
                    TagDetailInfo tdiForErrorTag = errorTagDetailList.Find(i => i.RFID_ADD_EPC == epcAdd || i.RFID_EPC == epc);
                    if(tdiForErrorTag == null)
                    {
                        tdiForErrorTag = LocalDataService.GetTagDetailInfoByEpc(taginfo.Epc);
                        if (tdiForErrorTag != null) errorTagDetailList.Add(tdiForErrorTag);
                    }
                    if(tdiForErrorTag!=null)
                    {
                        TagDetailInfoExtend tdiExtend = new TagDetailInfoExtend();
                        tdiExtend.RFID_EPC = tdiForErrorTag.RFID_EPC;
                        tdiExtend.RFID_ADD_EPC = tdiForErrorTag.RFID_ADD_EPC;
                        tdiExtend.MATNR = tdiForErrorTag.MATNR;
                        tdiExtend.BARCD = tdiForErrorTag.BARCD;
                        tdiExtend.ZSATNR = tdiForErrorTag.ZSATNR;
                        tdiExtend.ZCOLSN = tdiForErrorTag.ZCOLSN;
                        tdiExtend.ZSIZTX = tdiForErrorTag.ZSIZTX;
                        tdiExtend.CHARG = tdiForErrorTag.CHARG;
                        tdiExtend.PXQTY = tdiForErrorTag.PXQTY;
                        if (tdiForErrorTag.IsAddEpc)
                            tdiExtend.HAS_RFID_ADD_EPC = true;
                        else
                            tdiExtend.HAS_RFID_EPC = true;

                        this.tdiExtendList.Add(taginfo.Epc, tdiExtend);
                    }

                }
            }
        }
        private void addToTagPici(string key,string pici)
        {
            try
            {
                /*
                if (string.IsNullOrEmpty(key))
                    return;

                if (!dicTagPici.ContainsKey(key))
                {
                    List<string> picis = new List<string>();
                    picis.Add(pici);
                    dicTagPici.Add(key, picis);
                }
                else
                {
                    List<string> picis = dicTagPici[key];
                    if (!picis.Exists(i => i == pici))
                    {
                        picis.Add(pici);
                    }
                }
                */
                if(!curPici.Contains(pici))
                {
                    curPici.Add(pici);
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 检查到非本单的EPC，则需要前往数据库获取相关的物料信息
        /// 从数据库获取的物料信息需要存储到本列表，供后续查询使用
        /// 避免同一SKU的EPC频繁查找数据库
        /// </summary>
        private List<TagDetailInfo> errorTagDetailList = new List<TagDetailInfo>();
        private bool isReaderConnect = false;
        private void cbUseBoxStandard_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUseBoxStandard.Checked)
                btnSwitchStandardBox.BackColor = Color.Tan;
            else
                btnSwitchStandardBox.BackColor = Color.WhiteSmoke;
        }

        private List<MixRatioInfo> mixRatioList = new List<MixRatioInfo>();
        private void btnPeibi_Click(object sender, EventArgs e)
        {
            PBForm pb = new PBForm(mixRatioList);
            pb.ShowDialog();

        }

        private void btnDocDetails_Click(object sender, EventArgs e)
        {
            DocDetailForm form = new DocDetailForm(docDetailInfoList);
            form.ShowDialog();
        }

        private void lvPBDetail_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                ListViewItem lvi = lvPBDetail.GetItemAt(e.X, e.Y);
                foreach (ListViewItem item in lvPBDetail.Items)
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
                    if (lvPBDetail.FocusedItem != null)
                    {
                        lvPBDetail.FocusedItem.BackColor = Color.LightBlue;
                        lvPBDetail.FocusedItem.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void lvPBDetail_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (SysConfig.RunningModel == RunMode.高位库 || IsYupinxiang())
            {
                if (btnStart.Enabled)
                {
                    lblCurrentZSATNR.Text = "配比号："+e.Item.SubItems[1].Text;
                    currentDocdetailItem = e.Item;
                }
            }
        }

        private void grouper1_Load(object sender, EventArgs e)
        {

        }

        private void cbYpxWx_CheckedChanged(object sender, EventArgs e)
        {
            if(cbYpxWx.Checked)
            {
                cbYpxWx.BackColor = Color.Tan;
            }
            else
            {
                cbYpxWx.BackColor = Color.WhiteSmoke;
            }
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            cbYpxWx.Hide();

            if(SysConfig.RunningModel == RunMode.高位库)
            {
                grouper1.GroupTitle += "-高位库";
            }
        }

        private void saveChaoShouData(CChaoShou cs,ReceiveType rt)
        {
            if (SysConfig.LGNUM != "ET01")
                return;

                try
                {
                string docName = cs.mDoc;
                if(rt == ReceiveType.交接单收货)
                {
                    docName += "_dema";
                }
                string sql = string.Format("select count(*) from sysinfo where FieldName='{0}'", docName);
                if (DBHelper.GetValue(sql, false).CastTo<int>(0) > 0)
                {
                    sql = "update sysinfo set FieldValue=@data where FieldName=@docNo";
                    SqlParameter p1 = DBHelper.CreateParameter("@data", JsonConvert.SerializeObject(cs));
                    SqlParameter p2 = DBHelper.CreateParameter("@docNo", docName);

                    DBHelper.ExecuteSql(sql, false, p1, p2);
                }
                else
                {
                    sql = string.Format("insert into sysinfo values('{0}','{1}')"
                        , docName, JsonConvert.SerializeObject(cs));
                    DBHelper.ExecuteSql(sql, false);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private CChaoShou getChaoShouData(string docName, ReceiveType rt)
        {
            CChaoShou re = new CChaoShou();
            re.mDoc = docName;

            if (SysConfig.LGNUM != "ET01")
                return re;

            try
            {
                if (rt == ReceiveType.交接单收货)
                {
                    docName += "_dema";
                }

                string sql = string.Format("select * from sysinfo where FieldName='{0}'", docName);

                DataTable dt = DBHelper.GetTable(sql, false);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        return JsonConvert.DeserializeObject<CChaoShou>(row["FieldValue"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }

            return re;
        }

        private void addToChaoShou(Dictionary<string,TagDetailInfoExtend> epcInfos)
        {
            if (SysConfig.LGNUM != "ET01")
                return;

            foreach (TagDetailInfoExtend td in epcInfos.Values)
            {
                string psgkey = getpsgKey(td.ZSATNR, td.ZCOLSN, td.ZSIZTX);
                if(td.HAS_RFID_EPC)
                {
                    lock (mChaoShouLockObject)
                    {
                        if (mChaoShou.mQty.ContainsKey(psgkey))
                        {
                            mChaoShou.mQty[psgkey] = mChaoShou.mQty[psgkey] + 1;
                        }
                        else
                        {
                            mChaoShou.mQty.Add(psgkey, 1);
                        }
                    }
                }
            }

            saveChaoShouData(mChaoShou, receiveType);

            updateChaoShou();
        }

        private string getpsgKey(string p,string s,string g)
        {
            return p + "-" + s + "-" + g;
        }
        private void subToChaoShou(Dictionary<string, TagDetailInfoExtend> epcInfos)
        {
            if (SysConfig.LGNUM != "ET01")
                return;

            foreach (TagDetailInfoExtend td in epcInfos.Values)
            {
                string psgkey = getpsgKey(td.ZSATNR, td.ZCOLSN, td.ZSIZTX);
                if (td.HAS_RFID_EPC)
                {
                    lock (mChaoShouLockObject)
                    {
                        if (mChaoShou.mQty.ContainsKey(psgkey))
                        {
                            mChaoShou.mQty[psgkey] = mChaoShou.mQty[psgkey] - 1;
                        }
                    }
                }
            }

            saveChaoShouData(mChaoShou, receiveType);

            updateChaoShou();
        }
        private void updateChaoShou()
        {
            try
            {
                Invoke(new Action(() =>
                {
                    foreach (ListViewItem docDetailItem in lvDocDetail.Items)
                    {
                        string zsatnr = docDetailItem.SubItems[1].Text;
                        string zcolsn = docDetailItem.SubItems[2].Text;
                        string zsiztx = docDetailItem.SubItems[3].Text;

                        string psgkey = getpsgKey(zsatnr, zcolsn, zsiztx);

                        if (mChaoShou.mQty.ContainsKey(psgkey))
                        {
                            docDetailItem.SubItems[8].Text = mChaoShou.mQty[psgkey].ToString();
                        }
                    }
                }));

            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
        }
        private bool checkChaoSouAjt2()
        {
            bool re = false;
            try
            {
                Invoke(new Action(() =>
                {

                    if (SysConfig.LGNUM == "ET01")
                    {
                        foreach (ListViewItem item in lvTagDetail.Items)
                        {
                            string zsatnr = item.SubItems[0].Text;
                            string zcolsn = item.SubItems[1].Text;
                            string zsiztx = item.SubItems[2].Text;
                            string charg = item.SubItems[3].Text;
                            int qty = int.Parse(item.SubItems[4].Text);

                            foreach (ListViewItem docDetailItem in lvDocDetail.Items)
                            {
                                if (docDetailItem.SubItems[1].Text == zsatnr && docDetailItem.SubItems[2].Text == zcolsn
                                    && docDetailItem.SubItems[3].Text == zsiztx)
                                {
                                    int shouldqty = 0;
                                    int.TryParse(docDetailItem.SubItems[5].Text, out shouldqty);

                                    int realqty = 0;
                                //int.TryParse(docDetailItem.SubItems[6].Text, out realqty);
                                string psgKey = getpsgKey(zsatnr, zcolsn, zsiztx);
                                    if (mChaoShou.mQty.ContainsKey(psgKey))
                                    {
                                        realqty = mChaoShou.mQty[psgKey];
                                    }

                                    if (realqty + qty > shouldqty)
                                    {
                                        re = true;
                                    }
                                }
                            }
                        }
                    }

                }));
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
            return re;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> hu = getSelectHu();

        }
    }

    public class CChaoShou
    {
        public string mDoc;
        public Dictionary<string, int> mQty = new Dictionary<string, int>();
    }

    
}
