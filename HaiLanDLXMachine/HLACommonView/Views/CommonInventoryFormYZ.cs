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
using ThingMagic;
using Impinj.OctaneSdk;
using UARTRfidLink.Extend;
using System.Configuration;
using UARTRfidLink.Exparam;
using Codetag.Rfid.ClassLibrary;

namespace HLACommonView.Views
{
    public partial class CommonInventoryFormYZ : MetroForm
    {
        ProcessDialog mPd = new ProcessDialog();
        private ErrorWarnForm mErrorForm = new ErrorWarnForm();

        public CReader mReader = null;
        public PLCController mPlc = null;
        public BarcodeDevice mBarcode1 = null;
        public BarcodeDevice mBarcode2 = null;
        public Queue<string> mBoxNoList = new Queue<string>();
        public bool mIsInventory = false;
        public DateTime mLastReadTime = DateTime.Now;
        public List<string> mEpcList = new List<string>();

        public List<TagDetailInfo> mTagDetailList = new List<TagDetailInfo>();
        public int mErrorEpcNumber = 0, mMainEpcNumber = 0, mAddEpcNumber = 0;
        public List<HLATagInfo> mHlaTagList = null;
        public List<MaterialInfo> mMaterialList = null;
        public int mInventoryResult = 0;

        private List<string> mIgnoreEpcs = new List<string>();

        public CommonInventoryFormYZ()
        {
            InitializeComponent();
            mIgnoreEpcs = SAPDataService.getIngnoreEpcs();
        }

        public List<CTagDetail> getTags()
        {
            List<CTagDetail> re = new List<CTagDetail>();
            try
            {
                List<string> barList = mTagDetailList.Select(i => i.BARCD).Distinct().ToList();
                foreach (var v in barList)
                {
                    TagDetailInfo ti = mTagDetailList.FirstOrDefault(i => i.BARCD == v);

                    CTagDetail t = new CTagDetail();
                    t.bar = ti.BARCD;
                    t.zsatnr = ti.ZSATNR;
                    t.zcolsn = ti.ZCOLSN;
                    t.zsiztx = ti.ZSIZTX;
                    t.charg = ti.CHARG;
                    t.quan = mTagDetailList.Count(i => i.BARCD == v && !i.IsAddEpc);

                    re.Add(t);
                }
            }
            catch (Exception)
            { }
            return re;
        }

        public virtual CheckResult CheckData()
        {
            CheckResult result = new CheckResult();
            if (mErrorEpcNumber > 0)
            {
                result.UpdateMessage(Consts.Default.EPC_WEI_ZHU_CE);
                result.InventoryResult = false;
            }
            if (mMainEpcNumber != mAddEpcNumber && mAddEpcNumber > 0)
            {
                result.UpdateMessage(Consts.Default.TWO_NUMBER_ERROR);
                result.InventoryResult = false;
            }

            if (mAddEpcNumber == 0)
            {
                if (mTagDetailList.Exists(i => !string.IsNullOrEmpty(i.BARCD_ADD)))
                {
                    result.UpdateMessage(Consts.Default.TWO_NUMBER_ERROR);
                    result.InventoryResult = false;
                }
            }

            if (mBoxNoList.Count > 0)
            {
                mBoxNoList.Clear();
                result.UpdateMessage(Consts.Default.XIANG_MA_BU_YI_ZHI);
                result.InventoryResult = false;
            }
            if (mEpcList.Count == 0)
            {
                result.UpdateMessage(Consts.Default.WEI_SAO_DAO_EPC);
                result.InventoryResult = false;
            }

            return result;
        }


        public virtual void UpdateView()
        {
        }


        public TagDetailInfo GetTagDetailInfoByEpc(string epc)
        {
            if (string.IsNullOrEmpty(epc) || epc.Length < 20)
                return null;
            string rfidEpc = epc.Substring(0, 14) + "000000";
            string rfidAddEpc = rfidEpc.Substring(0, 14);
            if (mHlaTagList == null || mMaterialList == null)
                return null;
            List<HLATagInfo> tags = mHlaTagList.FindAll(i => i.RFID_EPC == rfidEpc || i.RFID_ADD_EPC == rfidAddEpc);
            if (tags == null || tags.Count == 0)
                return null;
            else
            {
                HLATagInfo tag = tags.First();
                MaterialInfo mater = mMaterialList.FirstOrDefault(i => i.MATNR == tag.MATNR);
                if (mater == null)
                    return null;
                else
                {
                    TagDetailInfo item = new TagDetailInfo();
                    item.EPC = epc;
                    item.RFID_EPC = tag.RFID_EPC;
                    item.RFID_ADD_EPC = string.IsNullOrEmpty(tag.RFID_ADD_EPC) ? "" : tag.RFID_ADD_EPC;
                    item.CHARG = tag.CHARG;
                    item.MATNR = tag.MATNR;
                    item.BARCD = tag.BARCD;
                    item.BARCD_ADD = tag.BARCD_ADD;
                    item.ZSATNR = mater.ZSATNR;
                    item.ZCOLSN = mater.ZCOLSN;
                    item.ZSIZTX = mater.ZSIZTX;
                    item.ZCOLSN_WFG = mater.ZCOLSN_WFG;
                    item.PXQTY = mater.PXQTY;
                    item.PXQTY_FH = mater.PXQTY_FH;
                    item.PACKMAT = mater.PXMAT;
                    item.PACKMAT_FH = mater.PXMAT_FH;
                    item.PUT_STRA = mater.PUT_STRA;
                    item.BRGEW = mater.BRGEW;
                    item.MAKTX = mater.MAKTX;

                    if (rfidEpc == item.RFID_EPC)
                        item.IsAddEpc = false;
                    else
                        item.IsAddEpc = true;
                    item.LIFNRS = new List<string>();
                    foreach (HLATagInfo t in tags)
                    {
                        if (!string.IsNullOrEmpty(t.LIFNR))
                        {
                            if (!item.LIFNRS.Contains(t.LIFNR))
                            {
                                item.LIFNRS.Add(t.LIFNR);
                            }
                        }
                    }
                    return item;
                }
            }
        }

        public virtual void ShowLoading(string message)
        {
            Invoke(new System.Action(() => {
#if DEBUG

#else
                mPd.Show();
#endif
                metroPanel1.Show();
                lblText.Text = message;
            }));

        }

        public virtual void HideLoading()
        {
            Invoke(new System.Action(() => {
                mPd.Hide();
                metroPanel1.Hide();
                lblText.Text = "";
            }));
        }

        public virtual void SetInventoryResult(int result)
        {
            mInventoryResult = result;
        }

        private void CommonInventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseWindow();
        }
        #region 设备相关
        public virtual void InitDevice(READER_TYPE readerType, bool connectBarcode)
        {
            mReader = new CReader(readerType);

            if (readerType == READER_TYPE.READER_IMP || readerType == READER_TYPE.READER_TM)
            {
                mReader.OnTagReported += Reader_OnTagReported;
            }
            if (readerType == READER_TYPE.READER_DLX_PM || readerType == READER_TYPE.READER_XD_PM)
            {
                mReader.OnTagReported += Reader_OnTagReportedPM;
            }

            mPlc = new PLCController(SysConfig.Port);
            if (connectBarcode)
            {
                mBarcode1 = new BarcodeDevice(SysConfig.ScannerPort_1);
                mBarcode2 = new BarcodeDevice(SysConfig.ScannerPort_2);
            }
        }

        public virtual void CloseWindow()
        {
            DisconnectReader();
            DisconnectPlc();
            DisconnectBarcode();
        }
        #region reader
        private void DisconnectReader()
        {
            mReader.disConnect();
        }

        public virtual bool ConnectReader()
        {
            return mReader.connect();
        }
    
        public bool ignore(string epc)
        {
            try
            {
                if (mIgnoreEpcs != null && mIgnoreEpcs.Count > 0)
                {
                    if (!string.IsNullOrEmpty(epc) && epc.Length >= 14)
                    {
                        string rfidEpc = epc.Substring(0, 14);
                        if (mIgnoreEpcs.Exists(i => i.Contains(rfidEpc)))
                        {
                            return true;
                        }
                    }

                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public virtual void Reader_OnTagReportedPM(string epc)
        {
            if (!mIsInventory || string.IsNullOrEmpty(epc) || ignore(epc)) return;
            if (!mEpcList.Contains(epc))
            {
                mLastReadTime = DateTime.Now;

                TagDetailInfo tag = GetTagDetailInfoByEpc(epc);

                string errorMsg = "";
                if (!checkTagOK(tag, out errorMsg))
                {
                    mErrorForm.showErrorInfo(tag.EPC, tag, errorMsg);
                    return;
                }

                if (tag != null)   //合法EPC
                {
                    mTagDetailList.Add(tag);
                    if (!tag.IsAddEpc)   //主条码
                        mMainEpcNumber++;
                    else
                        mAddEpcNumber++;
                }
                else
                {
                    //累加非法EPC数量
                    mErrorEpcNumber++;
                }

                mEpcList.Add(epc);
                UpdateView();
            }

        }
        public virtual bool checkTagOK(TagDetailInfo tg, out string msg)
        {
            msg = "";
            return true;
        }

        public void Reader_OnTagReported(string epc)
        {
            if (!mIsInventory) return;
            if (string.IsNullOrEmpty(epc) || ignore(epc))
                return;
            if (!mEpcList.Contains(epc))
            {
                mLastReadTime = DateTime.Now;
                mEpcList.Add(epc);

                TagDetailInfo tag = GetTagDetailInfoByEpc(epc);
                if (tag != null)   //合法EPC
                {
                    mTagDetailList.Add(tag);
                    if (!tag.IsAddEpc)   //主条码
                        mMainEpcNumber++;
                    else
                        mAddEpcNumber++;
                }
                else
                {
                    //累加非法EPC数量
                    mErrorEpcNumber++;
                }
                UpdateView();
            }
        }

        public virtual void StartInventory()
        {
            throw new NotImplementedException();
        }

        public virtual void StopInventory()
        {
            throw new NotImplementedException();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (mIsInventory)
            {
                //当前正在盘点，则判断上次读取时间和现在读取时间
                if ((DateTime.Now - mLastReadTime).TotalMilliseconds >= SysConfig.DelayTime)
                {
                    StopInventory();
                }
            }
        }
        #endregion

        #region plc

        public virtual bool ConnectPlc()
        {
            mPlc.OnPLCDataReported += Plc_OnPLCDataReported;
            return mPlc.Connect();
        }

        private void DisconnectPlc()
        {
            mPlc.OnPLCDataReported -= Plc_OnPLCDataReported;
            mPlc.Disconnect();
        }
        private void Plc_OnPLCDataReported(Xindeco.Device.Model.PLCData plcData)
        {
            if (plcData.Command == Xindeco.Device.Model.PLCRequest.OPEN_RFID)
            {
                StartInventory();
            }
            else if (plcData.Command == Xindeco.Device.Model.PLCRequest.ASK_RESULT)
            {
                switch (mInventoryResult)
                {
                    case 1://正常
                        StopInventory();
                        mPlc.SendCommand(Xindeco.Device.Model.PLCResponse.RIGHT);
                        break;
                    case 3://异常
                        StopInventory();
                        mPlc.SendCommand(Xindeco.Device.Model.PLCResponse.ERROR);
                        break;
                    default:

                        break;
                }
            }
        }
        #endregion

        #region barcode

        public virtual void ConnectBarcode()
        {
            mBarcode1.OnBarcodeReported += OnBarcodeReported;
            mBarcode1.Connect();
            mBarcode2.OnBarcodeReported += OnBarcodeReported;
            mBarcode2.Connect();
        }

        private void DisconnectBarcode()
        {
            mBarcode1.OnBarcodeReported -= OnBarcodeReported;
            mBarcode1.Disconnect();
            mBarcode2.OnBarcodeReported -= OnBarcodeReported;
            mBarcode2.Disconnect();
        }

        private void OnBarcodeReported(string barcode)
        {
            if (!mBoxNoList.Contains(barcode))
                mBoxNoList.Enqueue(barcode);
        }
        #endregion

        #endregion

    }
    public class CReader
    {
        //1~32.5
        public ImpinjReader mReaderIMP = null;
        //500~3150
        public Reader mReaderTM = null;
        //10~230
        public RfidUARTLinkExtend mReaderDLXPM = null;
        public string mComPort;
        //10~325
        public RfidReader mReaderXDPM = null;

        public READER_TYPE mReaderType;
        public string mIp;
        public double mPower;

        public delegate void TagReportedHandler(string epc);
        public event TagReportedHandler OnTagReported;
        public CReader(READER_TYPE rt)
        {
            mReaderType = rt;

            if(mReaderType == READER_TYPE.READER_IMP)
            {
                mIp = ConfigurationManager.AppSettings["ReaderIP_IMP"];
                mPower = double.Parse(ConfigurationManager.AppSettings["ReaderPower_IMP"]);
            }
            if (mReaderType == READER_TYPE.READER_TM)
            {
                mIp = ConfigurationManager.AppSettings["ReaderIP_TM"];
                mPower = double.Parse(ConfigurationManager.AppSettings["ReaderPower_TM"]);
            }
            if (mReaderType == READER_TYPE.READER_DLX_PM)
            {
                mIp = ConfigurationManager.AppSettings["ReaderIP_DLX_PM"];
                mPower = double.Parse(ConfigurationManager.AppSettings["ReaderPower_DLX_PM"]);
                mComPort = ConfigurationManager.AppSettings["ReaderCom_DLX_PM"];
            }
            if (mReaderType == READER_TYPE.READER_XD_PM)
            {
                mIp = ConfigurationManager.AppSettings["ReaderIP_XD_PM"];
                mPower = double.Parse(ConfigurationManager.AppSettings["ReaderPower_XD_PM"]);
            }
        }

        private void tagsReportedXDPM(object sender, Codetag.Rfid.ClassLibrary.TagsReportedEventArgs e)
        {
            if (e != null && !string.IsNullOrEmpty(e.Epc))
            {
                string epc = e.Epc.Replace(" ", "");
                if (!string.IsNullOrEmpty(epc))
                {
                    epc = epc.ToUpper();
                }
                this.OnTagReported?.Invoke(epc);
            }
        }
        private void tagsReportedIMP(ImpinjReader reader, TagReport report)
        {
            if (report != null && report.Tags != null)
            {
                foreach (Tag tag in report.Tags)
                {
                    string epc = tag.Epc.ToString().Replace(" ", "");
                    if (!string.IsNullOrEmpty(epc))
                    {
                        epc = epc.ToUpper();
                    }
                    this.OnTagReported?.Invoke(epc);
                }
            }
        }
        public void tagsReportedTM(Object sender, TagReadDataEventArgs taginfo)
        {
            if (taginfo == null || taginfo.TagReadData == null || string.IsNullOrEmpty(taginfo.TagReadData.EpcString))
                return;

            string epc = taginfo.TagReadData.EpcString;

            if (!string.IsNullOrEmpty(epc))
            {
                epc = epc.ToUpper();
            }
            this.OnTagReported?.Invoke(epc);
        }
        public void tagsReportedDLXPM(object sender, RadioInventoryEventArgs e)
        {
            string epc = "";
            try
            {
                for (int i = 0; i < e.tagInfo.epc.Length; i++)
                {
                    epc += string.Format("{0:X4}", e.tagInfo.epc[i]);
                }
            }
            catch (Exception) { }

            this.OnTagReported?.Invoke(epc);
        }
        public bool connect()
        {
#if DEBUG
            return false;
#endif
            bool re = false;

            if (mReaderType == READER_TYPE.READER_IMP)
            {
                try
                {
                    mReaderIMP = new ImpinjReader();
                    mReaderIMP.TagsReported += this.tagsReportedIMP;

                    mReaderIMP.Connect(mIp);
                    mReaderIMP.ApplyDefaultSettings();

                    configIMP(mPower);

                    re = true;
                }
                catch (Exception)
                {
                    re = false;
                }
            }

            if(mReaderType == READER_TYPE.READER_TM)
            {
                try
                {
                    Reader.SetSerialTransport("tcp", SerialTransportTCP.CreateSerialReader);
                    mReaderTM = Reader.Create(string.Format("tcp://{0}", mIp));
                    mReaderTM.TagRead += tagsReportedTM;

                    mReaderTM.Connect();

                    configTM(mPower);

                    re = true;
                }
                catch (Exception)
                {
                    re = false;
                }
            }

            if(mReaderType == READER_TYPE.READER_DLX_PM)
            {
                try
                {
                    mReaderDLXPM = new RfidUARTLinkExtend();
                    mReaderDLXPM.RadioInventory += tagsReportedDLXPM;

                    if (mReaderDLXPM.ConnectRadio(mComPort, 115200) == operateResult.ok)
                    {
                        AntennaPortConfiguration portConfig = new AntennaPortConfiguration();
                        portConfig.powerLevel = (uint)mPower;
                        portConfig.numberInventoryCycles = 8192;
                        portConfig.dwellTime = 2000;
                        mReaderDLXPM.SetAntennaPortConfiguration(mComPort, 0, portConfig);
                        mReaderDLXPM.SetCurrentLinkProfile(mComPort, 1);
                        SingulationAlgorithmParms singParm = new SingulationAlgorithmParms();
                        singParm.singulationAlgorithmType = SingulationAlgorithm.Dynamicq;
                        singParm.startQValue = 4;
                        singParm.minQValue = 0;
                        singParm.maxQValue = 15;
                        singParm.thresholdMultiplier = 4;
                        singParm.toggleTarget = 1;
                        mReaderDLXPM.SetCurrentSingulationAlgorithm(mComPort, singParm);
                        mReaderDLXPM.SetTagGroupSession(mComPort, Session.S0);

                        re = true;
                    }
                }
                catch (Exception)
                {
                    re = false;
                }
            }

            if (mReaderType == READER_TYPE.READER_DLX_PM)
            {
                try
                {
                    mReaderXDPM = new RfidReader();
                    mReaderXDPM.OnTagsReported += tagsReportedXDPM;
                    re = mReaderXDPM.OpenReader(mIp, 2048, SynchronizationContext.Current, "M6E").Success;
                    configXDPM(mPower);
                }
                catch(Exception)
                {
                    re = false;
                }
            }

            return re;
        }
        public void disConnect()
        {
            if (mReaderType == READER_TYPE.READER_IMP)
            {
                mReaderIMP.Disconnect();
            }
            if (mReaderType == READER_TYPE.READER_TM)
            {
                mReaderTM.Destroy();
            }
            if(mReaderType == READER_TYPE.READER_DLX_PM)
            {
                mReaderDLXPM.DisconnectRadio(mComPort);
            }
            if (mReaderType == READER_TYPE.READER_XD_PM)
            {
                mReaderXDPM.CloseReader();
            }

        }
        public void configXDPM(double power = 28, int searchMode = 1, ushort session = 2)
        {
            try
            {
                uint num = 0u;
                if (searchMode != 1)
                {
                    if (searchMode == 2)
                    {
                        num = 0u;
                    }
                }
                else
                {
                    num = 1u;
                }
                uint num2 = 0u;
                mReaderXDPM.SetSession(session, num2, num);

                for (uint i = 0; i <= 3; i++)
                {
                    mReaderXDPM.SetAntennaPort(i, true);
                    mReaderXDPM.SetAntennaPower((uint)mPower, i);
                }
            }
            catch (Exception)
            {
            }
        }
        public void configIMP(double power = 28, int searchMode = 1, ushort session = 2)
        {
            try
            {
                Settings settings = mReaderIMP.QueryDefaultSettings();
                settings.Report.Mode = ReportMode.Individual;
                settings.Report.IncludeAntennaPortNumber = true;
                settings.Report.IncludePeakRssi = true;
                switch (searchMode)
                {
                    case 1:
                        settings.SearchMode = SearchMode.DualTarget;
                        break;
                    case 2:
                        settings.SearchMode = SearchMode.SingleTarget;
                        break;
                }
                settings.Session = session;
                settings.Antennas.EnableAll();
                settings.Antennas.TxPowerInDbm = power;
                mReaderIMP.ApplySettings(settings);
            }
            catch (Exception)
            {
            }
        }
        public void configTM(double power = 28)
        {
            int[] antennaList = { 1, 2, 3, 4 }; //选择天线1,2,3,4

            mReaderTM.ParamSet("/reader/region/id", Reader.Region.NA);

            SimpleReadPlan plan = new SimpleReadPlan(antennaList, TagProtocol.GEN2, null, null, 1000); //设置天线和协议
            mReaderTM.ParamSet("/reader/read/plan", plan);

            //场景配置,用于隧道机
            Gen2.LinkFrequency blf = Gen2.LinkFrequency.LINK320KHZ;
            mReaderTM.ParamSet("/reader/gen2/BLF", blf);

            Gen2.Tari tari = Gen2.Tari.TARI_6_25US;
            mReaderTM.ParamSet("/reader/gen2/tari", tari);

            Gen2.TagEncoding tagncoding = Gen2.TagEncoding.FM0;
            mReaderTM.ParamSet("/reader/gen2/tagEncoding", tagncoding);

            Gen2.Session session = Gen2.Session.S1;
            mReaderTM.ParamSet("/reader/gen2/session", session);

            Gen2.Target target = Gen2.Target.A;
            mReaderTM.ParamSet("/reader/gen2/target", target);

            //500~3150
            mReaderTM.ParamSet("/reader/radio/readPower", (int)(power));
        }
        public void startRead()
        {
            try
            {
                if (mReaderType == READER_TYPE.READER_IMP)
                {
                    mReaderIMP.Start();
                }
                if (mReaderType == READER_TYPE.READER_TM)
                {
                    mReaderTM.StartReading();
                }
                if (mReaderType == READER_TYPE.READER_DLX_PM)
                {
                    mReaderDLXPM.StartInventory(mComPort, RadioOperationMode.Continuous, 1);
                }
                if (mReaderType == READER_TYPE.READER_XD_PM)
                {
                    mReaderXDPM.StartInventory(1000, 8192u, false);
                }
            }
            catch(Exception)
            {

            }
        }
        public void stopRead()
        {
            try
            {
                if (mReaderType == READER_TYPE.READER_IMP)
                {
                    mReaderIMP.Stop();
                }
                if (mReaderType == READER_TYPE.READER_TM)
                {
                    mReaderTM.StopReading();
                }
                if (mReaderType == READER_TYPE.READER_DLX_PM)
                {
                    mReaderDLXPM.StopInventory(mComPort);
                }
                if (mReaderType == READER_TYPE.READER_XD_PM)
                {
                    mReaderXDPM.StopInventory();
                }
            }
            catch(Exception)
            {

            }
        }
    }
    public enum READER_TYPE
    {
        READER_IMP,
        READER_TM,
        READER_DLX_PM,
        READER_XD_PM
    }
}
