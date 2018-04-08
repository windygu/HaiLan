using DMSkin;
using HLACommonLib;
using HLACommonLib.Model;
using HLACommonView.Configs;
using HLACommonView.Model;
using HLACommonView.Views.Dialogs;
using HLACommonView.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xindeco.Device;
using Xindeco.Device.Model;
using HLACommonView.Views;
using OSharp.Utility.Extensions;
using HLACommonLib.Model.PACKING;
using HLACommonLib.DAO;
using System.Configuration;
using HLAChannelMachine.Utils;
using HLACommonLib.Model.RECEIVE;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace HLAChannelMachine
{
    public partial class InventoryFormNew : CommonPMInventoryForm
    {
        private DocInfo mDocInfo;
        private List<DocDetailInfo> mDocDetailInfoList;
        private Dictionary<string, EpcDetail> mCurrentEpcdetailList_dic = new Dictionary<string, EpcDetail>();
        private Dictionary<string, EpcDetail> mHistoryEpcdetailList_dic = new Dictionary<string, EpcDetail>();

        private bool mInDanJie = false;
        private DanJieForm mDanJieForm = null;
        private ErrorWarningForm mErrorForm = null;
        private int mTotalCheckNum = 0;

        private object savingErrorRecordLockObject = new object();
        private Queue<ErrorRecord> savingErrorRecord = new Queue<ErrorRecord>();
        private Thread savingGridDataThread = null;

        private object savingDataLockObject = new object();
        private Queue<UploadData> savingData = new Queue<UploadData>();
        private Thread savingDataThread = null;


        public InventoryFormNew()
        {
            InitializeComponent();

            InitDevice(UHFReaderType.ThingMagic840);
            reader.OnTagReported += Reader_OnTagReported;
        }
        public bool delEpcInDanJie(string epc, out string msg)
        {
            if (!epcList.Exists(i => i == epc))
            {
                msg = "扫描列表中没有该EPC:" + epc;
                return false;
            }

            epcList.Remove(epc);
            TagDetailInfo tarTag = tagDetailList.Find(i => i.EPC == epc);
            if (tarTag == null)
            {
                errorEpcNumber--;
            }
            else
            {
                if (tarTag.IsAddEpc)
                    addEpcNumber--;
                else
                    mainEpcNumber--;
            }
            tagDetailList.RemoveAll(i => i.EPC == epc);

            updateScanNum();

            msg = "";
            return true;
        }
        public void Reader_OnTagReported(Xindeco.Device.Model.TagInfo taginfo)
        {
            if (!isInventory) return;
            if (taginfo == null || string.IsNullOrEmpty(taginfo.Epc)) return;

            if (mInDanJie)
            {
                mDanJieForm.reportTag(taginfo.Epc);
                return;
            }

            if (!epcList.Contains(taginfo.Epc))
            {
                lastReadTime = DateTime.Now;

                TagDetailInfo tag = GetTagDetailInfoByEpc(taginfo.Epc);
                string errorMsg = "";
                if (!checkTag(tag, out errorMsg))
                {
                    mErrorForm.showErrorInfo(taginfo.Epc, tag, errorMsg);
                    return;
                }


                if (tag != null)
                {
                    tagDetailList.Add(tag);
                    if (!tag.IsAddEpc)
                        mainEpcNumber++;
                    else
                        addEpcNumber++;
                }
                else
                {
                    //累加非法EPC数量
                    errorEpcNumber++;
                }

                epcList.Add(taginfo.Epc);
                updateScanNum();

            }

        }

        private void updateScanNum()
        {
            this.Invoke(new Action(() =>
            {
                this.lblScanNum.Text = mainEpcNumber.ToString();
                this.lblScanNumEx.Text = addEpcNumber.ToString();
                this.label3_errorScanNum.Text = errorEpcNumber.ToString();
            }));

        }
        private void addToCurrent(List<EpcDetail> eds)
        {
            if (eds != null && eds.Count > 0)
            {
                foreach (EpcDetail ed in eds)
                {
                    addToCurrent(ed);
                }
            }
        }
        private void addToCurrent(EpcDetail ed)
        {
            try
            {
                if (!mCurrentEpcdetailList_dic.ContainsKey(ed.EPC_SER))
                {
                    mCurrentEpcdetailList_dic.Add(ed.EPC_SER, ed);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
        }
        private void addToHistory(List<EpcDetail> epcs)
        {
            try
            {
                foreach (EpcDetail ed in epcs)
                {
                    if (!mHistoryEpcdetailList_dic.ContainsKey(ed.EPC_SER))
                    {
                        mHistoryEpcdetailList_dic.Add(ed.EPC_SER, ed);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
        }
        private bool checkTag(TagDetailInfo tag, out string msg)
        {
            if (tag == null)
            {
                msg = "不在本单";
                return false;
            }

            if (this.mDocInfo.DOCTYPE.Trim() == "DI21")
            {
                if (this.mCurrentEpcdetailList_dic.ContainsKey(tag.EPC) || this.mHistoryEpcdetailList_dic.ContainsKey(tag.EPC))
                {
                    msg = "商品已扫描";
                    return false;
                }
            }
            else
            {
                if (this.mCurrentEpcdetailList_dic.ContainsKey(tag.EPC))
                {
                    msg = "商品已扫描";
                    return false;
                }
            }

            if (tagDetailList != null && tagDetailList.Count > 0)
            {
                if (tag.MATNR != tagDetailList[0].MATNR)
                {
                    msg = "串规格";
                    return false;
                }
            }
            if (this.cbUseBoxStandard.Checked && mainEpcNumber == mTotalCheckNum)
            {
                msg = "箱规不符";
                return false;
            }
            if (mainEpcNumber > mTotalCheckNum || addEpcNumber > mTotalCheckNum)
            {
                msg = "数量大于配置";
                return false;
            }

            if (checkPiCiNotSame(tag))
            {
                msg = "批次不一致";
                return false;
            }

            msg = "";
            return true;
        }

        bool checkPiCiNotSame(TagDetailInfo tag)
        {
            foreach (var doc in mDocDetailInfoList)
            {
                foreach (var pc in tagDetailList)
                {
                    if (tag.CHARG != doc.ZCHARG)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public static void UpdateAppConfig(string newKey, string newValue)
        {
            string file = System.Windows.Forms.Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            bool exist = false;
            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                if (key == newKey)
                {
                    exist = true;
                }
            }
            if (exist)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            config.AppSettings.Settings.Add(newKey, newValue);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void btnSetBoxQty_Click(object sender, EventArgs e)
        {
            BoxQtyConfigFormNew f = new BoxQtyConfigFormNew(this);
            f.ShowDialog();

            lblTotalCheckNum.Text = mTotalCheckNum.ToString();
            UpdateAppConfig("TotalCheckNum", mTotalCheckNum.ToString());
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TabTip.exe");
        }

        private void btnSelectDocNo_Click(object sender, EventArgs e)
        {
            DocNoInputFormNew form = new DocNoInputFormNew(this);
            form.ShowDialog();
        }



        private void btnReset_Click(object sender, EventArgs e)
        {
            StopInventory();
            setControlState(false);

            ClearData();
            updateScanNum();
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            UploadForm u = new UploadForm();
            u.ShowDialog();
        }


        public void LoadDocNoInfo(DocInfo docInfo
            , List<DocDetailInfo> _docDetailInfoList
            , List<MaterialInfo> _materialList, List<HLATagInfo> _hlaTagInfo
            , List<EpcDetail> _epcdetailList, List<EpcDetail> curHis)
        {
            mDocInfo = docInfo;
            mDocDetailInfoList = _docDetailInfoList;

            mCurrentEpcdetailList_dic.Clear();
            mHistoryEpcdetailList_dic.Clear();

            addToCurrent(curHis);
            addToHistory(_epcdetailList);

            materialList = _materialList;
            hlaTagList = _hlaTagInfo;

            this.lvDocDetail.Items.Clear();
            //清空扫描列表

            this.lblDocNo.Text = this.mDocInfo.DOCNO; //交货单号
            int actualTotalNum = 0;
            int totalBoxNum = 0;
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
            this.lblActualTotalNum.Text = actualTotalNum.ToString();
            this.lblTotalBoxNum.Text = totalBoxNum.ToString();

        }

        private void InventoryFormNew_Load(object sender, EventArgs e)
        {
            this.lblCurrentUserNo.Text = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "";
            this.lblDeviceNo.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.EQUIP_HLA : "";
            this.lblDocNo.Text = "";
            this.lblBoxNo.Text = "";
            this.lblInventoryResult.Text = "";

            mTotalCheckNum = SysConfig.BoxQty;
            lblTotalCheckNum.Text = SysConfig.BoxQty.ToString();

            Thread thread = new Thread(new ThreadStart(() =>
            {
                ShowLoading("正在连接读写器...");
                if (ConnectReader())
                    Invoke(new Action(() => { lblReaderStatus.Text = "正常"; lblReaderStatus.ForeColor = Color.Black; }));
                else
                    Invoke(new Action(() => { lblReaderStatus.Text = "异常"; lblReaderStatus.ForeColor = Color.OrangeRed; }));

                HideLoading();

            }));
            thread.IsBackground = true;
            thread.Start();

            initSavingData();

            this.savingGridDataThread = new Thread(new ThreadStart(savingGridDataThreadFunc));
            this.savingGridDataThread.IsBackground = true;
            this.savingGridDataThread.Start();

            this.savingDataThread = new Thread(new ThreadStart(savingDataThreadFunc));
            this.savingDataThread.IsBackground = true;
            this.savingDataThread.Start();
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

        private void savingGridDataThreadFunc()
        {
            while (true)
            {
                ErrorRecord gridData = null;
                lock (savingErrorRecordLockObject)
                {
                    if (savingErrorRecord.Count > 0)
                    {
                        gridData = savingErrorRecord.Dequeue();
                    }
                }
                if (gridData != null)
                {
                    LocalDataService.SaveErrorRecord(gridData, HLACommonLib.Model.ENUM.ReceiveType.交货单收货);
                }
                Thread.Sleep(1000);
            }
        }
        private void savingDataThreadFunc()
        {
            while (true)
            {
                UploadData upData = null;
                lock (savingDataLockObject)
                {
                    if (savingData.Count > 0)
                    {
                        upData = savingData.Dequeue();
                    }
                }
                if (upData != null)
                {
                    SaveData(upData);
                }

                Thread.Sleep(1000);
            }
        }

        private void SaveData(UploadData data)
        {
            try
            {
                ResultDataInfo result = data.Data as ResultDataInfo;
                SapResult uploadResult;
                if (result.ReceiveType == 1)
                    uploadResult = SAPDataService.UploadTransferBoxInfo(result.LGNUM,
                        result.Doc == null ? "" : result.Doc.DOCNO, result.BoxNO,
                        result.InventoryResult, result.ErrorMsg, result.TdiExtendList,
                        result.RunningMode, result.CurrentUserId, result.Floor, result.sEQUIP_HLA);
                else
                {
                    //有添加设备终端号
                    uploadResult = SAPDataService.UploadBoxInfo(result.LGNUM, result.Doc.DOCNO, result.BoxNO, result.InventoryResult, result.ErrorMsg, result.TdiExtendList, result.RunningMode, result.CurrentUserId, result.Floor, result.sEQUIP_HLA, "");
                }

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

                LocalDataService.SaveInventoryResult(result.LGNUM, result.BoxNO, result.InventoryResult, result.CurrentNum, HLACommonLib.Model.ENUM.ReceiveType.交货单收货);
                LocalDataService.SaveEpcDetail(result.InventoryResult, result.LGNUM, result.Doc.DOCNO, result.Doc.DOCTYPE, result.BoxNO, result.EpcList, HLACommonLib.Model.ENUM.ReceiveType.交货单收货);

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
                    addToCurrent(epcDetail);
                }
                //将数据附加到交货明细表中
                foreach (ListViewTagInfo tagDetailItem in result.LvTagInfo)
                {
                    string zsatnr = tagDetailItem.ZSATNR;
                    string zcolsn = tagDetailItem.ZCOLSN;
                    string zsiztx = tagDetailItem.ZSIZTX;
                    string charg = tagDetailItem.CHARG;
                    int qty = tagDetailItem.QTY;

                    this.Invoke(new Action(() =>
                    {
                        UpdateDocDetailInfo(zsatnr, zcolsn, zsiztx, charg, qty, result.Doc);
                    }));
                }
                UploadedHandler(data.Guid);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
        }
        private void UpdateDocDetailInfo(string zsatnr, string zcolsn, string zsiztx, string charg, int qty, DocInfo doc)
        {
            //更新实收总数和总箱数
            if (mDocInfo != null && mDocInfo.DOCNO == doc.DOCNO)
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

                        LocalDataService.SaveDocDetail(this.mDocInfo.DOCNO, itemNo, zsatnr, zcolsn, zsiztx, charg, tempqty, qty, 1, HLACommonLib.Model.ENUM.ReceiveType.交货单收货, "");
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
                    LocalDataService.SaveDocDetail(this.mDocInfo.DOCNO, "", zsatnr, zcolsn, zsiztx, charg, 0, qty, 1, HLACommonLib.Model.ENUM.ReceiveType.交货单收货, "");
                }
            }
            else
            {
                LocalDataService.SaveDocDetail(doc.DOCNO, "", zsatnr, zcolsn, zsiztx, charg, 0, qty, 1, HLACommonLib.Model.ENUM.ReceiveType.交货单收货, "");
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
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (this.lblReaderStatus.Text != "正常")
            {
                AudioHelper.PlayWithSystem("Resources\\fail.wav");
                MessageBox.Show("读写器没有连接正常，请先连接读写器", "提示");
                return;
            }

            if (this.mDocInfo == null || string.IsNullOrEmpty(this.mDocInfo.DOCNO))
            {
                AudioHelper.PlayWithSystem("Resources\\fail.wav");
                MessageBox.Show("交货单号为空，请先选择交货单", "提示");
                return;
            }

            if (!getBox())
            {
                AudioHelper.PlayWithSystem("Resources\\fail.wav");
                MessageBox.Show("获取箱号失败，请重新开始", "提示");
                return;
            }

            setControlState(true);
            ClearData();
            StartInventory();

            AudioHelper.PlayWithSystem("Resources\\success.wav");
        }
        private void GetBoxNoQueue()
        {
            if (boxNoList.Count <= 0)
            {
                boxNoList = SAPDataService.GetBoxNo(SysConfig.LGNUM);
            }
        }
        private bool getBox()
        {
            try
            {
                GetBoxNoQueue();
                Invoke(new Action(() =>
                {
                    lblBoxNo.Text = boxNoList.Dequeue();
                }));
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }

            return false;
        }
        public override void StartInventory()
        {
            try
            {
                if (this.isInventory == false)
                {
                    reader.StartInventory(1000, 0, 0);
                    isInventory = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

        }

        public override void StopInventory()
        {
            try
            {
                if (isInventory)
                {
                    isInventory = false;
                    reader.StopInventory();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
            }
        }

        public void ClearData()
        {
            epcList.Clear();
            tagDetailList.Clear();
            mainEpcNumber = 0;
            addEpcNumber = 0;
            errorEpcNumber = 0;
            lastReadTime = DateTime.Now;
            checkResult.Init();

        }
        private void setControlState(bool start)
        {
            Invoke(new Action(() =>
            {
                this.btnSetBoxQty.Enabled = !start;
                this.btnStart.Enabled = !start;
                this.btnStop.Enabled = start;
                this.btnSelectDocNo.Enabled = !start;
                this.btnClose.Enabled = !start;

                if (start)
                {
                    this.lblScanNum.Text = "0";
                    this.lblScanNumEx.Text = "0";
                    this.lblWorkStatus.Text = "正在盘点";
                    this.lblInventoryResult.Text = "";
                }
                else
                {
                    this.lblWorkStatus.Text = "结束盘点";
                }
            }));

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (!lastCheck(out msg))
            {
                mErrorForm.showErrorInfo(tagDetailList, msg);
                return;
            }
            StopInventory();
            setControlState(false);

            ResultDataInfo rdi = GetResultData();
            //add to grid
            addToGridAndSave();
            //print
            printData(rdi);
            //upload
            upload(rdi);

            AudioHelper.PlayWithSystem("Resources\\success.wav");
        }

        private ResultDataInfo GetResultData()
        {
            ResultDataInfo result = new ResultDataInfo();
            result.BoxNO = this.lblBoxNo.Text.Trim();
            result.CurrentNum = mainEpcNumber;
            result.CurrentUserId = SysConfig.CurrentLoginUser.UserId;
            result.Doc = this.mDocInfo.Clone() as DocInfo;
            result.EpcList = new List<string>(this.epcList);
            result.ErrorMsg = "正常";
            result.Floor = SysConfig.Floor;
            result.InventoryResult = true;
            result.IsRecheck = false;
            result.LastResult = null;
            result.LGNUM = SysConfig.LGNUM;
            result.LvTagInfo = new List<ListViewTagInfo>();

            List<string> matrList = tagDetailList.Select(i => i.MATNR).Distinct().ToList();
            foreach (string mtr in matrList)
            {
                TagDetailInfo tg = tagDetailList.First(j => j.MATNR == mtr);

                result.LvTagInfo.Add(new ListViewTagInfo(tg.MATNR
                                , tg.ZSATNR
                                , tg.ZCOLSN
                                , tg.ZSIZTX
                                , tg.CHARG
                                , tagDetailList.Count(i => i.MATNR == mtr && !i.IsAddEpc)));
            }
            result.RunningMode = SysConfig.RunningModel;
            result.sEQUIP_HLA = SysConfig.sEQUIP_HLA;
            result.TdiExtendList = new Dictionary<string, TagDetailInfoExtend>();
            foreach (TagDetailInfo tag in tagDetailList)
            {
                TagDetailInfoExtend tExd = new TagDetailInfoExtend();
                tExd.BARCD = tag.BARCD;
                tExd.CHARG = tag.CHARG;
                tExd.HAS_RFID_ADD_EPC = tag.IsAddEpc;
                tExd.HAS_RFID_EPC = !tag.IsAddEpc;
                tExd.MATNR = tag.MATNR;
                tExd.PXQTY = tag.PXQTY;
                tExd.RFID_ADD_EPC = tag.RFID_ADD_EPC;
                tExd.RFID_EPC = tag.RFID_EPC;
                tExd.ZCOLSN = tag.ZCOLSN;
                tExd.ZSATNR = tag.ZSATNR;
                tExd.ZSIZTX = tag.ZSIZTX;

                result.TdiExtendList.Add(tag.EPC, tExd);
            }
            return result;
        }

        private void printData(ResultDataInfo rdi)
        {
            try
            {
                PrintBoxStandard(rdi);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
        }
        private void PrintBoxStandard(ResultDataInfo result)
        {
            CommonUtils.PrintRightBoxTagV2(result.InventoryResult, result.BoxNO, result.LvTagInfo);
        }

        private void addToGridAndSave()
        {
            try
            {
                if (tagDetailList.Count > 0)
                {
                    List<string> matrList = tagDetailList.Select(i => i.MATNR).Distinct().ToList();
                    foreach (string mtr in matrList)
                    {
                        TagDetailInfo tg = tagDetailList.First(j => j.MATNR == mtr);

                        ErrorRecord record = new ErrorRecord();
                        record.HU = this.lblBoxNo.Text.Trim();
                        record.QTY = tagDetailList.Count(i => i.MATNR == mtr && !i.IsAddEpc);
                        record.REMARK = "正常";
                        record.RESULT = "S";
                        record.ZCOLSN = tg.ZCOLSN;
                        record.ZSATNR = tg.ZSATNR;
                        record.ZSIZTX = tg.ZSIZTX;
                        record.DOCNO = this.lblDocNo.Text.Trim();

                        lock (savingErrorRecordLockObject)
                        {
                            savingErrorRecord.Enqueue(record);
                        }

                        foreach(var v in tagDetailList)
                        {
                            if(!v.IsAddEpc)
                            {
                                gridBox.Rows.Insert(0, "", v.EPC, v.ZSATNR, v.ZCOLSN, v.ZSIZTX, v.PXQTY, "正常");
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }

        }

        public void setBoxQty(int q)
        {
            mTotalCheckNum = q;
        }

        private void upload(ResultDataInfo rdi)
        {
            try
            {
                EnqueueUploadData(rdi);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
        }
        private void EnqueueUploadData(ResultDataInfo data)
        {
            UploadData ud = new UploadData();
            ud.Guid = Guid.NewGuid().ToString();
            ud.Data = data;
            ud.IsUpload = 0;
            ud.CreateTime = DateTime.Now;

            SqliteDataService.InsertUploadData(ud);

            lock (savingDataLockObject)
            {
                savingData.Enqueue(ud);
            }
        }

        private bool lastCheck(out string msg)
        {
            if (errorEpcNumber > 0)
            {
                msg = "不在本单";
                return false;
            }
            if (tagDetailList.Count <= 0)
            {
                msg = "未扫描到epc";
                return false;
            }
            if (addEpcNumber > 0 && mainEpcNumber != addEpcNumber)
            {
                msg = "主副条码不一致";
                return false;
            }

            if (mainEpcNumber > mTotalCheckNum)
            {
                msg = "数量大于箱规";
                return false;
            }
            if (this.cbUseBoxStandard.Checked && mainEpcNumber != mTotalCheckNum)
            {
                msg = "箱规不符";
                return false;
            }

            msg = "";
            return true;
        }

        private void button1_danjian_Click(object sender, EventArgs e)
        {
            setDanJie(true);
            mDanJieForm.TopMost = true;
            mDanJieForm.Show();
        }

        private void InventoryFormNew_Shown(object sender, EventArgs e)
        {
            mDanJieForm = new DanJieForm(this);
            mErrorForm = new ErrorWarningForm();
        }

        public void setDanJie(bool inDanJie)
        {
            mInDanJie = inDanJie;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGX_Click(object sender, EventArgs e)
        {
            GXForm g = new GXForm();
            g.ShowDialog();
        }

        private void InventoryFormNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (savingData.Count > 0)
            {
                if (DialogResult.OK != MessageBox.Show(this, string.Format("还有{0}条未上传的数据，确定关闭吗？", savingData.Count), "", MessageBoxButtons.OKCancel))
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
