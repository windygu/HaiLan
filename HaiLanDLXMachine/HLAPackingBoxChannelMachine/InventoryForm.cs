using DMSkin;
using HLACommonLib;
using HLACommonLib.DAO;
using HLACommonLib.Model;
using HLACommonLib.Model.PACKING;
using HLACommonView.Configs;
using HLACommonView.Views;
using HLACommonView.Views.Dialogs;
using HLAPackingBoxChannelMachine.DialogForms;
using HLAPackingBoxChannelMachine.Models;
using HLAPackingBoxChannelMachine.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Xindeco.Device;
using OSharp.Utility.Extensions;

namespace HLAPackingBoxChannelMachine
{
    public partial class InventoryForm : MetroForm
    {
        #region devices
        private UHFReader reader = new UHFReader(Xindeco.Device.Model.UHFReaderType.ImpinjR420);
        private PLCController plc = new PLCController(SysConfig.Port);
        private BarcodeDevice barcode1 = new BarcodeDevice(SysConfig.ScannerPort_1);
        private BarcodeDevice barcode2 = new BarcodeDevice(SysConfig.ScannerPort_2);
        #endregion
        private ProcessDialog pd = new ProcessDialog();
        private Thread initThread = null;
        private List<BoxQtyInfo> boxQtyList = null;
        private List<ReturnTypeInfo> returnTypeList = null;
        private List<HLATagInfo> hlaTagList = null;
        private List<MaterialInfo> materialList = null;
        private List<LhInfo> lhList = null;
        private Queue<string> boxNoList = new Queue<string>();
        private List<PBBoxInfo> currentBoxList = null;
        private List<AuthInfo> authList = null;
        private string lifnr = string.Empty;
        public InventoryForm()
        {
            InitializeComponent();
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            InitView();
            initThread = new Thread(new ThreadStart(() => {
                authList = SysConfig.DeviceInfo?.AuthList?.FindAll(i => i.AUTH_CODE.StartsWith("D"));
                ShowLoading("正在连接PLC...");
                ConnectPlc();
                ShowLoading("正在连接条码扫描头...");
                ConnectBarcode();
                ShowLoading("正在连接读写器...");
                ConnectReader();
                ShowLoading("正在下载楼号信息...");
                LoadLhInfo();
                ShowLoading("正在下载最新退货类型...");
                if (boxQtyList == null) boxQtyList = new List<BoxQtyInfo>();

                returnTypeList = SAPDataService.GetReturnTypeInfo(SysConfig.LGNUM);

                bool closed = false;

                ShowLoading("正在下载物料数据...");
                materialList = SAPDataService.GetMaterialInfoListAll(SysConfig.LGNUM);
                if (materialList == null || materialList.Count <= 0)
                {
                    this.Invoke(new Action(() =>
                    {
                        HideLoading();
                        MetroMessageBox.Show(this, "未下载到物料主数据，请检查网络情况", "提示");

                        closed = true;
                        Close();
                    }));

                }

                if (closed) return;

                ShowLoading("正在下载吊牌数据...");
                hlaTagList = LocalDataService.GetAllRfidHlaTagList();
                if (hlaTagList == null || hlaTagList.Count <= 0)
                {


                    this.Invoke(new Action(() =>
                    {
                        HideLoading();
                        MetroMessageBox.Show(this, "未下载到吊牌主数据，请检查网络情况", "提示");

                        closed = true;
                        Close();
                    }));

                }

                if (closed) return;

                HideLoading();

                Invoke(new Action(() => {
                    UpdateTotalInfo();
                }));

                UploadServer.GetInstance().OnUploaded += Box_OnUploaded;
                UploadServer.GetInstance().Start();
            }));
            initThread.IsBackground = true;
            initThread.Start();
        }

        private void UpdateBtnDocDetailStatus()
        {
            Invoke(new Action(() => {
                if (currentBoxList.Exists(i=>i.PACKRESULT == "E" && i.RESULT == "S"))
                {
                    btnDocBox.Text = string.Format("异常箱明细({0})", currentBoxList.Count(i=>i.PACKRESULT == "E" && i.RESULT == "S"));
                    btnDocBox.ForeColor = Color.FromArgb(255, 100, 0);
                }
                else
                {
                    btnDocBox.Text = "异常箱明细";
                    btnDocBox.ForeColor = Color.SteelBlue;
                }
            }));
        }

        private void Box_OnUploaded(PBBoxInfo Box)
        {
            if (currentBoxList == null) currentBoxList = new List<PBBoxInfo>();
            if (Box.RESULT == "E")
                return;
            else
            {
                currentBoxList.RemoveAll(i => i.HU == Box.HU);
                currentBoxList.Add(Box);
                UpdateBtnDocDetailStatus();
                UpdateTotalInfo();
            }
        }

        private void ConnectBarcode()
        {
            barcode1.OnBarcodeReported += OnBarcodeReported;
            barcode1.Connect();
            barcode2.OnBarcodeReported += OnBarcodeReported;
            barcode2.Connect();
        }

        private void LoadLhInfo()
        {
            if (SysConfig.DeviceInfo != null && SysConfig.DeviceInfo.AuthList != null && SysConfig.DeviceInfo.AuthList.Count > 0)
            {
                lhList = new List<LhInfo>();

                List<AuthInfo> lhAuthList = SysConfig.DeviceInfo.AuthList.FindAll(x => x.AUTH_CODE.StartsWith("D"));
                List<AuthInfo> rtAuthList = SysConfig.DeviceInfo.AuthList.FindAll(x => x.AUTH_CODE.StartsWith("E"));
                if(lhAuthList!=null)
                {
                    foreach (AuthInfo item in lhAuthList)
                    {
                        LhInfo lh = new LhInfo();
                        lh.Lh = item.AUTH_VALUE;
                        lh.InTag = item.AUTH_VALUE_DES;
                        lh.ReturnType = "TH01";
                        lhList.Add(lh);
                    }
                }
                
                if(rtAuthList!=null)
                {
                    lhList.RemoveAll(i => rtAuthList.Exists(j => j.AUTH_VALUE_DES == i.Lh));
                    foreach(AuthInfo item in rtAuthList)
                    {
                        LhInfo lh = new LhInfo();
                        lh.Lh = item.AUTH_VALUE_DES;
                        lh.InTag = lhAuthList.Exists(i => i.AUTH_VALUE == item.AUTH_VALUE_DES) ? lhAuthList.Find(i => i.AUTH_VALUE == item.AUTH_VALUE_DES).AUTH_VALUE_DES : "";
                        lh.ReturnType = item.AUTH_VALUE;
                        lhList.Add(lh);
                    }
                }

                if (lhList != null && lhList.Count > 0)
                {
                    foreach (LhInfo item in lhList)
                    {
                        if (!cboLh.Items.Contains(item.Lh))
                        {
                            Invoke(new Action(() =>
                            {
                                cboLh.Items.Add(item.Lh);
                            }));
                        }
                    }
                }
            }
            else
            {
                MetroMessageBox.Show(this, "未下载到楼号信息，请联系开发商解决", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DisconnectBarcode()
        {
            barcode1.OnBarcodeReported -= OnBarcodeReported;
            barcode1.Disconnect();
            barcode2.OnBarcodeReported -= OnBarcodeReported;
            barcode2.Disconnect();
        }

        private void OnBarcodeReported(string barcode)
        {
            if (!boxNoList.Contains(barcode))
                boxNoList.Enqueue(barcode);
        }

        private void ConnectPlc()
        {
            plc.OnPLCDataReported += Plc_OnPLCDataReported;
            if(plc.Connect())
            {
                Invoke(new Action(() => {
                    lblPlc.Text = "正常";
                    lblPlc.ForeColor = Color.Black;
                }));
            }
            else
            {
                Invoke(new Action(() => {
                    lblPlc.Text = "异常";
                    lblPlc.ForeColor = Color.Red;
                }));
            }
        }

        private void DisconnectPlc()
        {
            plc.OnPLCDataReported -= Plc_OnPLCDataReported;
            plc.Disconnect();
        }

        private void Plc_OnPLCDataReported(Xindeco.Device.Model.PLCData plcData)
        {
            if(plcData.Command == Xindeco.Device.Model.PLCRequest.OPEN_RFID)
            {
                StartInventory();
            }
            else if (plcData.Command == Xindeco.Device.Model.PLCRequest.ASK_RESULT)
            {
                switch(inventoryResult)
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

        private void UpdateTotalInfo()
        {
            Invoke(new Action(() => {
                int totalBoxNum = (int)currentBoxList?.Count(i => i.PACKRESULT == "S" && i.RESULT == "S").CastTo<int>(0);
                int totalNum = (int)currentBoxList?.FindAll(i => i.PACKRESULT == "S" && i.RESULT == "S").Sum(j => j.Details?.Count);
                lblTotalBoxNum.Text = totalBoxNum.ToString();
                lblTotalNum.Text = totalNum.ToString();
            }));
        }

        private void ConnectReader()
        {
            reader.OnTagReported += Reader_OnTagReported;
            bool result = reader.Connect(SysConfig.ReaderIp, Xindeco.Device.Model.ConnectType.TCP, WindowsFormsSynchronizationContext.Current);
            if(!result)
            {
                Invoke(new Action(() => {
                    lblReader.Text = "异常";
                    lblReader.ForeColor = Color.OrangeRed;

                    LocalDataService.InsertErrorDataRecord(ERRORDATATYPE.读写器未连接, "设备号：" + lblDeviceNo.Text + " 楼层号：" + lblLouceng.Text);
                }));
            }
            else
            {
                Invoke(new Action(() => {
                    lblReader.Text = "正常";
                    btnReceiveByBoxStandard.Enabled = true;
                    btnStart.Enabled = true;
                    btnGenerateDoc.Enabled = true;
                    btnDownload.Enabled = true;
                    btnDocBox.Enabled = true;
                }));
                Xindeco.Device.Model.ReaderConfig config = new Xindeco.Device.Model.ReaderConfig();
                config.SearchMode = SysConfig.ReaderConfig.SearchMode;
                config.Session = SysConfig.ReaderConfig.Session;
                if (config.AntennaList == null) config.AntennaList = new List<Xindeco.Device.Model.ReaderAntenna>();
                if(SysConfig.ReaderConfig.UseAntenna1)
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
        }

        private void DisconnectReader()
        {
            reader.Disconnect();
        }

        private bool isInventory = false;
        private List<string> epcList = new List<string>();
        private List<TagDetailInfo> tagDetailList = new List<TagDetailInfo>();
        private DateTime lastReadTime = DateTime.Now;
        private string errorMsg = "";
        private int errorEpcNumber = 0, mainEpcNumber = 0, addEpcNumber = 0;
        private void StartInventory()
        {
            if (!isInventory)
            {
                Invoke(new Action(() => {
                    lblWorkStatus.Text = "开始扫描";
                    lblQty.Text = "0";
                    lblResult.Text = "";
                    lblHu.Text = "";
                    if(btnStart.Enabled)
                    {
                        Start();
                    }
                }));
                SetInventoryResult(0);
                IsFull = false;
                errorMsg = "";
                lifnr = string.Empty;
                errorEpcNumber = 0;
                mainEpcNumber = 0;
                addEpcNumber = 0;
                epcList.Clear();
                tagDetailList.Clear();
                if (boxNoList.Count > 0)
                {
                    Invoke(new Action(() => {
                        lblHu.Text = boxNoList.Dequeue();
                    }));
                }
                int mv, trigger, r6mv;
                LocalDataService.GetGhostAndTrigger(out mv, out trigger,out r6mv);
                reader.StartInventory(mv, trigger, r6mv);
                
                isInventory = true;
                lastReadTime = DateTime.Now;
            }
        }

        private void StopInventory()
        {
            if (isInventory)
            {
                Invoke(new Action(() => {
                    lblWorkStatus.Text = "停止扫描";
                }));
                isInventory = false;
                reader.StopInventory();
                CheckResult result = CheckData();
                UploadBoxInfo box = GetUploadBox(result);
                
                if (result.InventoryResult)
                {
                    if(box.Box.Details!=null && box.Box.Details.Count>0)
                    {
                        PrintHelper.PrintRightTag(box.Box.LH,
                            box.Box.Details.First().ZSATNR,
                            box.Box.Details.First().ZCOLSN,
                            box.Box.Details.First().ZSIZTX,
                            box.Box.QTY, box.Box.HU,
                            SysConfig.PrinterName,box.Box.LIFNR);
                    }
                    if(!result.IsRecheck)
                    {
                        if (currentBoxList == null) currentBoxList = new List<PBBoxInfo>();
                        currentBoxList.RemoveAll(i => i.HU == box.Box.HU);
                        currentBoxList.Add(box.Box);
                    }
                }
                else
                {
                    PrintHelper.PrintErrorTag(errorMsg, lblHu.Text, tagDetailList, SysConfig.PrinterName);
                }
                AddBoxDetailGrid(box.Box);
                if (!result.IsRecheck)
                {
                    EnqueueUploadData(box);
                }
                

            }
        }
        private void EnqueueUploadData(UploadBoxInfo data)
        {
            SqliteUploadDataInfo ud = new SqliteUploadDataInfo();
            ud.Guid = Guid.NewGuid().ToString();
            ud.Data = data;
            ud.IsUpload = 0;
            ud.CreateTime = DateTime.Now;
            PackingBoxSqliteService.InsertUploadData(ud);
            UploadServer.GetInstance().CurrentUploadQueue.Push(ud);
        }

        private UploadBoxInfo GetUploadBox(CheckResult result)
        {
            UploadBoxInfo box = new UploadBoxInfo();
            box.EQUIP_HLA = SysConfig.DeviceInfo.EQUIP_HLA;
            box.LGNUM = SysConfig.LGNUM;
            box.LOUCENG = SysConfig.DeviceInfo.LOUCENG;
            box.SUBUSER = SysConfig.CurrentLoginUser.UserId;
            box.Box = new PBBoxInfo();
            box.Box.EQUIP = SysConfig.DeviceInfo.EQUIP_HLA;
            box.Box.HU = lblHu.Text;
            box.Box.LH = cboLh.Text;
            box.Box.QTY = mainEpcNumber;
            box.Box.RESULT = result.InventoryResult ? "S" : "E";
            box.Box.MSG = errorMsg;
            box.Box.MX = IsFull ? "X" : "";
            box.Box.LIFNR = lifnr;
            box.Box.Details = new List<PBBoxDetailInfo>();
            if(tagDetailList!=null)
            {
                foreach (TagDetailInfo tag in tagDetailList)
                {
                    PBBoxDetailInfo item = new PBBoxDetailInfo();
                    item.BARCD = tag.BARCD;
                    item.EPC = tag.EPC;
                    item.HU = lblHu.Text;
                    item.MATNR = tag.MATNR;
                    item.ZCOLSN = tag.ZCOLSN;
                    item.ZSATNR = tag.ZSATNR;
                    item.ZSIZTX = tag.ZSIZTX;
                    item.IsAdd = tag.IsAddEpc ? 1 : 0;
                    box.Box.Details.Add(item);
                }
            }
            
            return box;
        }
        private void Reader_OnTagReported(Xindeco.Device.Model.TagInfo taginfo)
        {
            if (!isInventory) return;
            if (taginfo == null || string.IsNullOrEmpty(taginfo.Epc)) return;
            if(!epcList.Contains(taginfo.Epc))
            {
                lastReadTime = DateTime.Now;
                epcList.Add(taginfo.Epc);
                
                TagDetailInfo tag = GetTagDetailInfoByEpc(taginfo.Epc);
                if(tag!=null)   //合法EPC
                {
                    tagDetailList.Add(tag);
                    if(!tag.IsAddEpc)   //主条码
                        mainEpcNumber++;
                    else
                        addEpcNumber++;
                }
                else
                {
                    //累加非法EPC数量
                    errorEpcNumber++;
                }
                Invoke(new Action(() => {
                    lblQty.Text = mainEpcNumber.ToString();
                }));
            }
        }

        private void ErrorHandle(string msg, int inventoryResult)
        {
            if (!string.IsNullOrEmpty(msg.Trim()) && !errorMsg.Contains(msg))
                errorMsg = errorMsg + msg + ";";
            string boxNo = lblHu.Text.Trim();
            //当箱码为空时，获取一个新的错误箱码
            
            Invoke(new Action(() =>
            {
                lblResult.Text = errorMsg;
            }));
            SetInventoryResult(inventoryResult);
        }
        private int inventoryResult = 0;
        private void SetInventoryResult(int result)
        {
            inventoryResult = result;
        }
        private TagDetailInfo GetTagDetailInfoByEpc(string epc)
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
                    item.EPC = epc;
                    item.RFID_EPC = tag.RFID_EPC;
                    item.RFID_ADD_EPC = tag.RFID_ADD_EPC;
                    item.CHARG = tag.CHARG;
                    item.MATNR = tag.MATNR;
                    item.BARCD = tag.BARCD;
                    item.ZSATNR = mater.ZSATNR;
                    item.ZCOLSN = mater.ZCOLSN;
                    item.ZSIZTX = mater.ZSIZTX;
                    item.ZCOLSN_WFG = mater.ZCOLSN_WFG;
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

        #region const strings 
        private const string EPC_WEI_ZHU_CE = "商品未注册";
        private const string TWO_NUMBER_ERROR = "主条码和辅条码数量不对应";
        private const string WEI_SAO_DAO_EPC = "未扫描到商品";
        private const string XIANG_MA_BU_YI_ZHI = "箱号不一致";
        private const string WEI_SAO_DAO_XIANG_MA = "未扫描到箱号";
        private const string XIANG_MA_CHONG_FU_SHI_YONG = "箱号重复使用";
        private const string EPC_YI_SAO_MIAO = "商品已扫描";
        private const string DUO_GE_SHANG_PIN = "多个商品";
        private const string LOU_HAO_BU_FU = "交接楼号不符";
        private const string BU_FU_HE_XIANG_GUI = "箱规不符";
        private const string CHONG_TOU = "重投";
        private const string RIGHT = "正常";
        private const string SHU_LIANG_DA_YU_XIANG_GUI = "数量大于箱规";
        #endregion

        private bool IsUseBoxStandard()
        {
            return lblUseBoxStandard.DM_Key == DMSkin.Controls.DMLabelKey.正确;
        }

        private object GetLastCheckResult(string hu)
        {
            if (currentBoxList!= null && currentBoxList.Exists(i => i.HU == hu))
            {
                if (currentBoxList.Find(i => i.HU == hu).RESULT == "S" && currentBoxList.Find(i => i.HU == hu).PACKRESULT == "S")
                    return "S";
            }
            return "E";
        }

        private List<PBBoxDetailInfo> GetBoxDetailByHU(string hu)
        {
            if(currentBoxList.Exists(i=>i.HU == hu))
            {
                return currentBoxList.Find(i => i.HU == hu).Details;
            }
            else
            {
                return PackingBoxService.GetBoxDetailsByHU(hu);
            }
        }

        public bool IsFull = false;
        private CheckResult CheckData()
        {
            CheckResult result = new CheckResult();
            if(btnStart.Enabled)
            {
                ErrorHandle("未开始检货",0);
                result.InventoryResult = false;
            }
            else
            {
                if (errorEpcNumber > 0)
                {
                    ErrorHandle(EPC_WEI_ZHU_CE, 0);
                    result.InventoryResult = false;
                }
                if (mainEpcNumber != addEpcNumber && addEpcNumber>0)
                {
                    ErrorHandle(TWO_NUMBER_ERROR, 0);
                    result.InventoryResult = false;
                }
                if (boxNoList.Count > 0)
                {
                    boxNoList.Clear();
                    ErrorHandle(XIANG_MA_BU_YI_ZHI, 0);
                    result.InventoryResult = false;
                }
                if (string.IsNullOrEmpty(lblHu.Text.Trim()))
                {
                    ErrorHandle(WEI_SAO_DAO_XIANG_MA, 0);

                    LocalDataService.InsertErrorDataRecord(ERRORDATATYPE.未扫描到箱号, "设备号：" + lblDeviceNo.Text + " 楼层号：" + lblLouceng.Text);

                    result.InventoryResult = false;

                    string hu = LocalDataService.GetNewErrorHu(SysConfig.DeviceNO);
                    Invoke(new Action(() =>
                    {
                        lblHu.Text = hu;
                    }));
                }
                else
                {
                    LocalDataService.InsertErrorDataRecord(ERRORDATATYPE.正常, "设备号：" + lblDeviceNo.Text + " 楼层号：" + lblLouceng.Text);

                }

                if (epcList.Count == 0)
                {
                    ErrorHandle(WEI_SAO_DAO_EPC, 0);
                    result.InventoryResult = false;
                }
                else
                {
                    //判断是否重投
                    object lastResult = GetLastCheckResult(lblHu.Text);
                    if (lastResult != null && lastResult.ToString().ToUpper() == "S")
                    {
                        //上次检测结果为正常，

                        bool isAllSame = true;
                        bool isAllNotSame = true;
                        List<PBBoxDetailInfo> lastCheckDetail = GetBoxDetailByHU(lblHu.Text);
                        if (lastCheckDetail != null && lastCheckDetail.Count > 0)
                        {
                            if (lastCheckDetail.Count != epcList.Count) isAllSame = false;
                            foreach (PBBoxDetailInfo item in lastCheckDetail)
                            {
                                if (!epcList.Contains(item.EPC))
                                {
                                    isAllSame = false;
                                    break;
                                }
                                else
                                {
                                    isAllNotSame = false;
                                }
                            }
                        }
                        else
                        {
                            isAllSame = false;
                            isAllNotSame = true;
                        }

                        if (isAllSame)
                        {
                            result.IsRecheck = true;
                            //result.InventoryResult = false;
                        }
                        else if (isAllNotSame)
                        {
                            //两批EPC对比，完全不一样，示为箱码重复使用
                            ErrorHandle(XIANG_MA_CHONG_FU_SHI_YONG, 0);
                            result.InventoryResult = false;
                        }

                        if (lastCheckDetail.Count > 0 && !isAllSame && !isAllNotSame)
                        {
                            ErrorHandle(EPC_YI_SAO_MIAO, 0);
                            result.InventoryResult = false;
                        }
                    }
                    else
                    {
                        
                        if(ExistsSameEpc())
                        {
                            ErrorHandle(EPC_YI_SAO_MIAO, 0);
                            result.InventoryResult = false;
                        }
                        //判断是否只有一个SKU
                        if (IsOneSku())
                        {
                            if (tagDetailList != null && tagDetailList.Count > 0)
                            {
                                

                                TagDetailInfo tag = tagDetailList.First();
                                ReturnTypeInfo rt = null;

                                if (cboLh.Text.ToUpper() == "TH")
                                {
                                    if (tag.LIFNRS?.Count > 1)
                                    {
                                        ErrorHandle(Consts.Default.JIAO_JIE_HAO_BU_YI_ZHI, 0);
                                        result.InventoryResult = false;
                                    }
                                    else
                                    {
                                        lifnr = tag.LIFNRS?.FirstOrDefault();
                                    }
                                }

                                if (returnTypeList != null)
                                {
                                    rt = returnTypeList.Find(i => i.ZPRDNR == tag.ZSATNR && i.ZCOLSN == tag.ZCOLSN_WFG);
                                }

                                MaterialInfo material = materialList.Find(i => i.MATNR == tag.MATNR);
                                int pxqty = 0;
                                
                                if (material != null)
                                {
                                    if (material.PUT_STRA == "ADM1")
                                        pxqty = material.PXQTY;
                                    else
                                        pxqty = material.PXQTY_FH;
                                }

                                if (IsUseBoxStandard())
                                {
                                    //按箱规收货
                                    if (pxqty != mainEpcNumber)
                                    {
                                        ErrorHandle(BU_FU_HE_XIANG_GUI + string.Format("({0})", pxqty), 0);
                                        result.InventoryResult = false;
                                    }
                                    else
                                    {
                                        IsFull = true;
                                    }
                                }
                                else
                                {
                                    if (pxqty < mainEpcNumber)
                                    {
                                        ErrorHandle(SHU_LIANG_DA_YU_XIANG_GUI, 0);
                                        result.InventoryResult = false;
                                    }
                                }

                                if (rt == null || string.IsNullOrEmpty(rt.THTYPE) || (rt.THTYPE.ToUpper() != "TH02" && rt.THTYPE.ToUpper() != "TH03"))
                                {
                                    //退货类型为空值
                                    List<AuthInfo> auth = authList != null ? authList.FindAll(i => i.AUTH_VALUE_DES == material.PUT_STRA) : null;
                                    if (material != null)
                                    {
                                        if(auth!=null)
                                        {
                                            if (!auth.Exists(i=>i.AUTH_VALUE == cboLh.Text))
                                            {
                                                string authPrint = "";
                                                auth.ForEach(i => {
                                                    authPrint += i.AUTH_VALUE + ",";
                                                });
                                                if (authPrint.EndsWith(","))
                                                    authPrint = authPrint.Remove(authPrint.Length - 1, 1);
                                                ErrorHandle(LOU_HAO_BU_FU+ string.Format("({0})", authPrint), 0);
                                                result.InventoryResult = false;
                                            }
                                        }
                                        else
                                        {
                                            ErrorHandle(LOU_HAO_BU_FU, 0);
                                            result.InventoryResult = false;
                                        }
                                        
                                    }
                                }
                                else
                                {
                                    if (rt.THTYPE.ToUpper() == "TH02" || rt.THTYPE.ToUpper() == "TH03")
                                    {
                                        if (!lhList.Exists(i => i.Lh == cboLh.Text && i.ReturnType == rt.THTYPE))
                                        {
                                            List<LhInfo> l = lhList.FindAll(i => i.ReturnType == rt.THTYPE);
                                            string lh = "";
                                            if(l!=null && l.Count>0)
                                            {
                                                l.ForEach(i => {
                                                    lh += i.Lh + ",";
                                                });

                                                if (lh.EndsWith(",")) lh = lh.Remove(lh.Length - 1, 1);
                                            }
                                            ErrorHandle(LOU_HAO_BU_FU + string.Format("({0})", lh), 0);
                                            result.InventoryResult = false;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            ErrorHandle(DUO_GE_SHANG_PIN, 0);
                            result.InventoryResult = false;
                        }
                    }
                }
            }
            

            if (result.IsRecheck || result.InventoryResult)
                RightHandle(result.IsRecheck ? CHONG_TOU : RIGHT);
            else
                ErrorHandle("", 3);
            return result;
        }

        private bool ExistsSameEpc()
        {
            if(currentBoxList!=null && currentBoxList.Count>0)
            {
                foreach(PBBoxInfo item in currentBoxList)
                {
                    if(item.RESULT == "S")
                    {
                        if (item.Details != null && item.HU != lblHu.Text)
                        {
                            if (item.Details.Exists(i => epcList.Contains(i.EPC)))
                                return true;
                        }
                    }
                    
                }
            }
            return false;
        }

        private void RightHandle(string msg)
        {
            errorMsg = msg;
            SetInventoryResult(1);
        }

        private bool IsOneSku()
        {
            if (epcList.Count != tagDetailList.Count)
                return false;
            if (tagDetailList.Select(i => i.MATNR).Distinct().Count() > 1)
                return false;
            return true;
        }

        /// <summary>
        /// 初始化界面，只在窗口加载的时候调用
        /// </summary>
        private void InitView()
        {
            lblCurrentUser.Text = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "未登录";
            lblDeviceNo.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.EQUIP_HLA : "未下载设备信息";
            lblLouceng.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.LOUCENG : "未下载设备信息";
            lblHu.Text = "";
            lblResult.Text = "";
            lblQty.Text = "0";
            cboLh.SelectedItem = null;
            if (currentBoxList == null) currentBoxList = new List<PBBoxInfo>();
            currentBoxList.Clear();
            grid.Rows.Clear();
#if DEBUG
            panelDebug.Show();
#else
            panelDebug.Hide();
#endif
        }

        private void ShowLoading(string msg)
        {
            Invoke(new Action(() => {
                panelLoading.Show();
                pd.Show();
                lblStatus.Text = msg;
            }));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        string currentLh = string.Empty;
        private void cboLh_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void AddBoxDetailGrid(PBBoxInfo box)
        {
            if (box == null) return;
            if(box.Details!=null && box.Details.Count>0)
            {
                List<string> matnrList = box.Details.Select(i => i.MATNR).Distinct().ToList();
                foreach(string item in matnrList)
                {
                    grid.Rows.Insert(0, box.LH, box.HU, 
                        box.Details.Find(i=>i.MATNR == item).ZSATNR,
                        box.Details.Find(i => i.MATNR == item).ZCOLSN,
                        box.Details.Find(i => i.MATNR == item).ZSIZTX,
                        box.Details.FindAll(i => i.MATNR == item).Count,
                        box.MSG);
                    if(box.RESULT!="S")
                    {
                        grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                    }
                    grid.Rows[0].Tag = box;
                }
            }
            else
            {
                grid.Rows.Insert(0, box.LH, box.HU,
                        "",
                        "",
                        "",
                        0,
                        box.MSG);
                if (box.RESULT != "S")
                {
                    grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                }
                grid.Rows[0].Tag = box;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void Start()
        {
            if(string.IsNullOrEmpty(cboLh.Text))
            {
                MetroMessageBox.Show(this, "请先选择交接楼号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            btnStart.Enabled = false;
            btnPause.Enabled = true;
            cboLh.Enabled = false;
        }

        private void Pause()
        {
            btnStart.Enabled = true;
            btnPause.Enabled = false;
            cboLh.Enabled = true;
        }

        private void btnReceiveByBoxStandard_Click(object sender, EventArgs e)
        {
            if(lblUseBoxStandard.DM_Key == DMSkin.Controls.DMLabelKey.正确)
            {
                lblUseBoxStandard.DM_Key = DMSkin.Controls.DMLabelKey.错误;
            }
            else
            {
                lblUseBoxStandard.DM_Key = DMSkin.Controls.DMLabelKey.正确;
            }
        }

        private void dmButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cboLh.Text);
        }

        private void btnGx_Click(object sender, EventArgs e)
        {
            GxForm form = new GxForm();
            form.ShowDialog();
        }

        private void dmButton4_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(metroTextBox1.Text))
            {
                if (!boxNoList.Contains(metroTextBox1.Text.Trim()))
                    boxNoList.Enqueue(metroTextBox1.Text.Trim());
            }
        }

        private void dmButton5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(metroTextBox2.Text))
            {
                Reader_OnTagReported(new Xindeco.Device.Model.TagInfo() { Epc = metroTextBox2.Text.Trim() });
            }
        }

        private void dmButton3_Click(object sender, EventArgs e)
        {
            StartInventory();
        }

        private void dmButton2_Click(object sender, EventArgs e)
        {
            StopInventory();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(() => {
                Invoke(new Action(() => { btnDownload.Enabled = false; }));
                ShowLoading("正在下载最新的自定义箱规、退货类型...");
                returnTypeList = SAPDataService.GetReturnTypeInfo(SysConfig.LGNUM);
                Invoke(new Action(() => { btnDownload.Enabled = true; }));
                HideLoading();
            })).Start();
        }

        private void btnGenerateDoc_Click(object sender, EventArgs e)
        {

            GenerateDocForm form = new GenerateDocForm(currentBoxList);
            form.OnGenerateSuccess += OnGenerateSuccess;
            form.ShowDialog();
        }

        private void OnGenerateSuccess(string lh)
        {
            UpdateBtnDocDetailStatus();
            UpdateTotalInfo();

            InitView();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (isInventory)
            {
                //当前正在盘点，则判断上次读取时间和现在读取时间
                if ((DateTime.Now - lastReadTime).TotalMilliseconds >= SysConfig.DelayTime)
                {
                    StopInventory();
                }
            }
        }

        private void btnDocBox_Click(object sender, EventArgs e)
        {
            if (currentBoxList != null && currentBoxList.Exists(i => i.PACKRESULT == "E" && i.RESULT == "S"))
            {
                DocBoxDetailForm form = new DocBoxDetailForm(currentBoxList);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    grid.Rows.Clear();
                    if (currentBoxList.Count > 0)
                    {
                        foreach (PBBoxInfo item in currentBoxList)
                        {
                            AddBoxDetailGrid(item);
                        }
                    }
                }
            }
        }

        private void cboLh_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (PackingBoxService.IsExistsNoGenerateBox())
            {
                if (MetroMessageBox.Show(this, "存在未生成交接单的箱记录，是否继续未生产交接单的箱记录?",
                    "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    currentBoxList = PackingBoxService.GetUnGenerateBoxListWithDetail();
                    grid.Rows.Clear();
                    //errorCount = 0;
                    if (currentBoxList != null && currentBoxList.Count > 0)
                    {
                        foreach (PBBoxInfo item in currentBoxList)
                        {
                            AddBoxDetailGrid(item);
                        }
                        string lh = currentBoxList.First().LH;
                        currentLh = lh;
                        cboLh.Text = lh;
                    }

                    UpdateBtnDocDetailStatus();
                    UpdateTotalInfo();

                }
                else
                {
                    if (string.IsNullOrEmpty(currentLh))
                    {
                        cboLh.SelectedItem = null;
                    }
                    else
                    {
                        cboLh.Text = currentLh;
                    }
                }
            }
            else
            {
                if(grid.Rows.Count>0)
                {
                    if (MetroMessageBox.Show(this, "是否删除列表箱记录?",
                    "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        List<string> huList = new List<string>();
                        foreach(DataGridViewRow row in grid.Rows)
                        {
                            if (!huList.Contains((row.Tag as PBBoxInfo).HU))
                                huList.Add((row.Tag as PBBoxInfo).HU);
                        }
                        if (PackingBoxService.DeleteBoxByHu(huList))
                        {
                            grid.Rows.Clear();
                            currentBoxList.Clear();
                        }
                    }
                }
            }
        }
        private void UpdateErrorBoxButton()
        {
            Invoke(new Action(() =>
            {
                int errorcount = (currentBoxList?.Count(i => i.PACKRESULT == "E" && i.RESULT == "S")).CastTo(0);
                dmButton6.Text = string.Format("异常箱明细({0})", errorcount);
                dmButton6.DM_NormalColor = errorcount > 0 ? Color.FromArgb(255, 100, 0) : Color.FromArgb(27, 163, 203);
                dmButton6.DM_MoveColor = errorcount > 0 ? Color.FromArgb(255, 60, 0) : Color.FromArgb(27, 123, 203);
                dmButton6.DM_DownColor = errorcount > 0 ? Color.FromArgb(255, 20, 0) : Color.FromArgb(27, 93, 203);
            }));
        }
        private void dmButton6_Click(object sender, EventArgs e)
        {
            if (currentBoxList != null && currentBoxList.Exists(i => i.PACKRESULT == "E" && i.RESULT == "S"))
            {
                List<PBBoxInfo> boxs = currentBoxList.FindAll(i => i.PACKRESULT == "E" && i.RESULT == "S");

                DocBoxDetailForm form = new DocBoxDetailForm(currentBoxList);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    currentBoxList.RemoveAll(i => boxs.Exists(j => j.HU == i.HU));
                    grid.Rows.Clear();
                    if (currentBoxList.Count > 0)
                    {
                        foreach (PBBoxInfo item in currentBoxList)
                        {
                            AddBoxDetailGrid(item);
                        }
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invoke(new Action(() => {
                int count = UploadServer.GetInstance().CurrentUploadQueue.Count;
                uploadButton.Text = string.Format("上传列表({0})", count);

            }));
        }

        private void dmButton7_handleJiaoJie_Click(object sender, EventArgs e)
        {
            JiaoJiaoHandle form = new JiaoJiaoHandle();
            form.ShowDialog();
        }

        private void HideLoading()
        {
            Invoke(new Action(() => {
                pd.Hide();
                panelLoading.Hide();
                lblStatus.Text = "loading...";
            }));
        }

        private void InventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;

            DisconnectReader();
            DisconnectPlc();
            DisconnectBarcode();
            UploadServer.GetInstance().End();
        }
    }

    public class CheckResult
    {

        public CheckResult()
        {
            IsRecheck = false;
            InventoryResult = true;
        }
        public bool IsRecheck { get; set; }

        public bool InventoryResult { get; set; }
    }
}
