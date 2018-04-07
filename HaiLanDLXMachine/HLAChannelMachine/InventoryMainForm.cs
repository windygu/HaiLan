using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonView.Views;
using HLACommonLib.Model;
using HLACommonLib.Model.RECEIVE;
using HLACommonLib.Model.ENUM;
using HLACommonLib;
using System.Threading;
using HLAChannelMachine.DialogForm;
using HLACommonView.Model;
using Newtonsoft.Json;
using HLACommonLib.DAO;
using HLAChannelMachine.Utils;

namespace HLAChannelMachine
{
    public partial class InventoryMainForm : CommonInventoryForm
    {
        public ReceiveType mReceiveType;
        public List<HuInfo> mHuList = new List<HuInfo>();
        public DocInfo mCurDocInfo = null;
        public List<DocDetailInfo> mDocDetailInfoList = null;
        public List<EpcDetail> mDocEpcDetailList = null;
        public List<MixRatioInfo> mMixRatioList = null;

        private object savingDataLockObject = new object();
        private Queue<UploadData> savingData = new Queue<UploadData>();
        private Thread savingDataThread = null;

        private object savingDataLockObject_Local = new object();
        private Queue<ErrorRecord> savingData_Local = new Queue<ErrorRecord>();
        private Thread savingDataThread_Local = null;

        List<string> curPici = new List<string>();
        private List<ErrorRecord> currentErrorRecordList = new List<ErrorRecord>();

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


        public InventoryMainForm()
        {
            InitializeComponent();
            InitDevice(Xindeco.Device.Model.UHFReaderType.ImpinjR420, true);
        }
        private void InitView()
        {
            Invoke(new Action(() =>
            {
                lblCurrentUserNo.Text = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "登录信息异常";
                lblDeviceNo.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.EQUIP_HLA : "设备信息异常";
                lblLouceng.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.LOUCENG : "设备信息异常";
                lblPlcStatus.Text = "连接中...";
                lblReaderStatus.Text = "连接中...";
                lblWorkStatus.Text = "未开始工作";
            }));
        }



        private void btnInputDoc_Click(object sender, EventArgs e)
        {
            DocNoInputFormNew form = new DocNoInputFormNew(this);
            form.ShowDialog();

        }

        private void btnGX_Click(object sender, EventArgs e)
        {
            GxForm f = new GxForm();
            f.ShowDialog();
        }

        private void InventoryMainForm_Load(object sender, EventArgs e)
        {
            InitView();
            Thread thread = new Thread(new ThreadStart(() =>
            {
                ShowLoading("正在连接PLC...");
                if (ConnectPlc())
                    Invoke(new Action(() => { lblPlcStatus.Text = "正常"; lblPlcStatus.ForeColor = Color.Black; }));
                else
                    Invoke(new Action(() => { lblPlcStatus.Text = "异常"; lblPlcStatus.ForeColor = Color.OrangeRed; }));

                if (SysConfig.RunningModel == RunMode.高位库)
                {
                    ShowLoading("正在连接条码扫描枪...");
                    ConnectBarcode();
                }

                ShowLoading("正在连接读写器...");
                if (ConnectReader())
                    Invoke(new Action(() => { lblReaderStatus.Text = "正常"; lblReaderStatus.ForeColor = Color.Black; }));
                else
                    Invoke(new Action(() => { lblReaderStatus.Text = "异常"; lblReaderStatus.ForeColor = Color.OrangeRed; }));

                HideLoading();

                this.savingDataThread = new Thread(new ThreadStart(savingDataThreadFunc));
                this.savingDataThread.IsBackground = true;
                this.savingDataThread.Start();

                this.savingDataThread_Local = new Thread(new ThreadStart(savingDataThreadFunc_Local));
                this.savingDataThread_Local.IsBackground = true;
                this.savingDataThread_Local.Start();

            }));

            thread.IsBackground = true;
            thread.Start();

        }

        private void savingDataThreadFunc()
        {
            while (true)
            {
                UploadData upload = null;
                lock (savingDataLockObject)
                {
                    if (savingData.Count > 0)
                    {
                        upload = savingData.Dequeue();
                    }
                }
                if (upload != null)
                {
                    SaveData(upload);
                }
                Thread.Sleep(1000);
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

        private void SaveData(UploadData data)
        {
            try
            {
                ResultDataInfo result = data.Data as ResultDataInfo;

                if (!result.InventoryResult || result.IsRecheck)
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
                        if (ReceiveService.SaveUploadData(xddata))
                        {
                        }

                        return;
                    }
                    else
                    {
                        UploadedHandler(data.Guid);
                    }
                }

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
                        mDocEpcDetailList.Add(epcDetail);
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
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace);
                LogHelper.WriteLine(JsonConvert.SerializeObject(data));
            }

        }
        private void UpdateDocDetailInfo(string zsatnr, string zcolsn, string zsiztx, string charg, int qty, string zpbno, DocInfo doc)
        {
            if (mCurDocInfo != null && mCurDocInfo.DOCNO == doc.DOCNO)
            {
                //更新实收总数和总箱数
                int actualTotalNum = 0;
                int.TryParse(lblActualTotalNum.Text, out actualTotalNum);
                actualTotalNum = actualTotalNum + qty;
                this.lblActualTotalNum.Text = actualTotalNum.ToString();
                this.lblTotalBoxNum.Text = mDocDetailInfoList.Sum(i => i.BOXCOUNT).ToString();

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
                        mDocDetailInfoList.Find(i => i.ZSATNR == zsatnr && i.ZCOLSN == zcolsn && i.ZSIZTX == zsiztx).REALQTY = realqty;
                        mDocDetailInfoList.Find(i => i.ZSATNR == zsatnr && i.ZCOLSN == zcolsn && i.ZSIZTX == zsiztx).BOXCOUNT = boxcount;
                        LocalDataService.SaveDocDetail(mCurDocInfo.DOCNO, itemNo, zsatnr, zcolsn, zsiztx, charg, tempqty, qty, 1, mReceiveType, zpbno);
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

                    LocalDataService.SaveDocDetail(mCurDocInfo.DOCNO, "", zsatnr, zcolsn, zsiztx, charg, 0, qty, 1, mReceiveType, zpbno);
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
                LocalDataService.SaveDocDetail(doc.DOCNO, "", zsatnr, zcolsn, zsiztx, charg, 0, qty, 1, mReceiveType, zpbno);
            }
        }

        private void savingDataThreadFunc_Local()
        {
            while (true)
            {
                ErrorRecord error = null;
                lock (savingDataLockObject_Local)
                {
                    if (savingData_Local.Count > 0)
                    {
                        error = savingData_Local.Dequeue();
                    }
                }
                if (error != null)
                {
                    LocalDataService.SaveErrorRecord(error, mReceiveType);
                }
                Thread.Sleep(1000);
            }
        }

        private bool IsYupinxiang()
        {
            return mCurDocInfo?.ZYPXFLG?.ToUpper() == "X";
        }

        private void initErrorRecord()
        {
            List<ErrorRecord> list = LocalDataService.GetErrorRecordsByDocNo(this.mCurDocInfo.DOCNO, mReceiveType);

            if (IsYupinxiang())
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

        public void loadBasicInfo(DocInfo docInfo, List<DocDetailInfo> _docDetailInfoList,
                                List<MaterialInfo> _materialList, List<HLATagInfo> _hlaTagInfo,
                                List<EpcDetail> _epcdetailList, List<HuInfo> _huList,
                                ReceiveType _receiveType, List<MixRatioInfo> _mixRatioList)
        {
            mReceiveType = _receiveType;
            mHuList = _huList != null ? _huList : new List<HuInfo>();
            mCurDocInfo = docInfo != null ? docInfo : new DocInfo();
            mDocDetailInfoList = _docDetailInfoList != null ? _docDetailInfoList : new List<DocDetailInfo>();
            materialList = _materialList != null ? _materialList : new List<MaterialInfo>();
            hlaTagList = _hlaTagInfo != null ? _hlaTagInfo : new List<HLATagInfo>();
            mDocEpcDetailList = _epcdetailList != null ? _epcdetailList : new List<EpcDetail>();
            mMixRatioList = _mixRatioList != null ? _mixRatioList : new List<MixRatioInfo>();
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
                if (mDocDetailInfoList != null && mDocDetailInfoList.Count > 0)
                {
                    List<string> zpbnoList = new List<string>();

                    foreach (DocDetailInfo ddi in mDocDetailInfoList)
                    {
                        if (!zpbnoList.Contains(ddi.ZPBNO))
                        {
                            zpbnoList.Add(ddi.ZPBNO);
                            ListViewItem item = new ListViewItem(ddi.ITEMNO);
                            item.SubItems.Add(ddi.ZPBNO);
                            int realqty = mDocDetailInfoList.FindAll(j => j.ZPBNO == ddi.ZPBNO).Sum(i => i.REALQTY);
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

            lvDocDetail.Items.Clear();
            if (mDocDetailInfoList != null && mDocDetailInfoList.Count > 0)
            {
                foreach (DocDetailInfo ddi in mDocDetailInfoList)
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
            lblActualTotalNum.Text = actualTotalNum.ToString();
            lblTotalBoxNum.Text = totalBoxNum.ToString();
            lblDocNo.Text = this.mCurDocInfo.DOCNO; //交货单号
            lblBoxNo.Text = ""; //箱码
            lblInventoryResult.Text = ""; //扫描结果
            cbUseBoxStandard.Checked = true;
            btnSwitchStandardBox.BackColor = Color.Tan;

            lblCurrentZSATNR.Text = "";

            lblType.Text = string.Format("{0}单号：", mReceiveType == ReceiveType.交货单收货 ? "交货" : "交接");
            initErrorRecord();
        }

        private void btnSetBoxQty_Click(object sender, EventArgs e)
        {
            BoxQtyConfigForm form = new BoxQtyConfigForm(this.mDocDetailInfoList, this.materialList);
            form.ShowDialog();
        }

        private void cbYpxWx_CheckedChanged(object sender, EventArgs e)
        {
            if (cbYpxWx.Checked)
            {
                cbYpxWx.BackColor = Color.Tan;
            }
            else
            {
                cbYpxWx.BackColor = Color.WhiteSmoke;
            }
        }

        private void btnSwitchStandardBox_Click(object sender, EventArgs e)
        {
            cbUseBoxStandard.Checked = !cbUseBoxStandard.Checked;
        }

        private void btnDocDetails_Click(object sender, EventArgs e)
        {
            DocDetailForm form = new DocDetailForm(mDocDetailInfoList);
            form.ShowDialog();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            UploadFormNew form = new UploadFormNew(mReceiveType, this);
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InventoryMainForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            CloseWindow();
        }

        public override void UpdateView()
        {
            base.UpdateView();
        }

        public override void StartInventory()
        {
            if (isInventory)
                return;

            try
            {
                //清除当前屏幕统计数量
                SetInventoryResult(0);
                errorEpcNumber = 0;
                mainEpcNumber = 0;
                addEpcNumber = 0;
                epcList.Clear();
                tagDetailList.Clear();

                curPici.Clear();
                currentErrorRecordList.Clear();

                Invoke(new Action(() =>
                {
                    lblBoxNo.Text = "";
                    lblWorkStatus.Text = "正在盘点";
                    lblInventoryResult.Text = "";
                }));

                int i, j, k;
                LocalDataService.GetGhostAndTrigger(out i, out j, out k);
                reader.StartInventory(i, j, k);
                lastReadTime = DateTime.Now;
                isInventory = true;

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
            }
        }

        public override void StopInventory()
        {
            if (!isInventory)
                return;

            try
            {
                this.isInventory = false;
                reader.StopInventory();

                this.Invoke(new Action(() =>
                {
                    this.lblWorkStatus.Text = "停止";
                }));

                CheckResult result = CheckData();
                EnqueueErrorRecord();
                ResultDataInfo rdi = GetResultData(result);

                if (SysConfig.RunningModel == RunMode.平库 || SysConfig.LGNUM == "ET01")
                {
                    PrintBoxStandard(rdi);
                }

                EnqueueUploadData(rdi);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        private bool IsYpxWx()
        {
            return !cbYpxWx.Checked;
        }

        private ResultDataInfo GetResultData(CheckResult inventoryResult)
        {
            ResultDataInfo result = new ResultDataInfo();
            result.BoxNO = this.lblBoxNo.Text.Trim();
            result.CurrentNum = epcList.Count;
            result.CurrentUserId = SysConfig.CurrentLoginUser.UserId;
            result.Doc = mCurDocInfo == null ? null : this.mCurDocInfo.Clone() as DocInfo;
            result.EpcList = new List<string>(this.epcList);
            if (IsYupinxiang())
            {
                if (IsYpxWx())
                    result.ZPBNO = "";
                else
                    result.ZPBNO = lvPBDetail.SelectedItems[0].SubItems[1].Text;
            }
            else
            {
                result.ZPBNO = "";
            }
            if (inventoryResult.Message.EndsWith(";"))
                inventoryResult.Message = inventoryResult.Message.Remove(inventoryResult.Message.Length - 1);

            result.ErrorMsg = inventoryResult.Message;
            result.Floor = SysConfig.Floor;
            result.InventoryResult = inventoryResult.IsRecheck ? true : inventoryResult.InventoryResult;
            result.IsRecheck = inventoryResult.IsRecheck;
            result.LGNUM = SysConfig.LGNUM;
            result.ReceiveType = (int)mReceiveType;
            result.LvTagInfo = new List<ListViewTagInfo>();

            foreach (var tdi in tagDetailList)
            {
                if (!tdi.IsAddEpc)
                {
                    if (result.LvTagInfo.Exists(i => i.ZSATNR == tdi.ZSATNR && i.ZCOLSN == tdi.ZCOLSN && i.ZSIZTX == tdi.ZSIZTX))
                    {
                        result.LvTagInfo.First(i => i.ZSATNR == tdi.ZSATNR && i.ZCOLSN == tdi.ZCOLSN && i.ZSIZTX == tdi.ZSIZTX).QTY += 1;
                    }
                    else
                    {
                        result.LvTagInfo.Add(new ListViewTagInfo(tdi.MATNR, tdi.ZSATNR, tdi.ZCOLSN, tdi.ZSIZTX, tdi.CHARG, 1));
                    }
                }
            }

            result.RunningMode = SysConfig.RunningModel;
            result.sEQUIP_HLA = SysConfig.sEQUIP_HLA;
            result.TdiExtendList = new Dictionary<string, TagDetailInfoExtend>();

            foreach (var tdi in tagDetailList)
            {
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

                result.TdiExtendList.Add(tdi.EPC, tdiExtend);
            }

            return result;
        }

        private void PrintBoxStandard(ResultDataInfo result)
        {
            //如果该箱为重投，盘点结果为false，且errorMsg==“重投”
            if (result.IsRecheck == true)
            {
                if (result.LvTagInfo.Count > 0)
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
                    if (IsYupinxiang())
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


                    lock (savingDataLockObject_Local)
                    {
                        savingData_Local.Enqueue(record);
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

        public override CheckResult CheckData()
        {
            return base.CheckData();
        }

        private void lvDocDetail_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (SysConfig.RunningModel == RunMode.高位库)
            {
                this.lblCurrentZSATNR.Text = "品号：" + e.Item.SubItems[1].Text + " 色号：" + e.Item.SubItems[2].Text + " 规格：" + e.Item.SubItems[3].Text;
            }
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

        private void lvPBDetail_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (SysConfig.RunningModel == RunMode.高位库 || IsYupinxiang())
            {
                lblCurrentZSATNR.Text = "配比号：" + e.Item.SubItems[1].Text;
            }
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

        public bool isItemSel()
        {
            if(lvDocDetail.Visible)
            {
                return lvDocDetail.SelectedItems.Count > 0;
            }
            if(lvPBDetail.Visible)
            {
                return lvPBDetail.SelectedItems.Count > 0;
            }

            return false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (mCurDocInfo == null || lvDocDetail.Items.Count <= 0)
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
                    if (mReceiveType != ReceiveType.交接单收货)
                    {
                        if (!isItemSel())
                        {
                            MessageBox.Show("请先选择行项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }
            else if (IsYupinxiang())
            {
                if (!IsYpxWx())
                {
                    if (!isItemSel())
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

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.btnStop.Enabled = false;
            if (SysConfig.RunningModel == RunMode.高位库)
            {
                this.lblCurrentZSATNR.Text = "";
            }
            else
                this.btnSetBoxQty.Enabled = true;
            StopInventory();
            this.btnStart.Enabled = true;
        }

        private void btnPeibi_Click(object sender, EventArgs e)
        {
            PBForm pb = new PBForm(mMixRatioList);
            pb.ShowDialog();
        }
    }
}
