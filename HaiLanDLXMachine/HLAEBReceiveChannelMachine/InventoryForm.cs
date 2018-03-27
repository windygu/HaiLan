using DMSkin;
using HLACommonLib;
using HLACommonLib.Model;
using HLAEBReceiveChannelMachine.DialogForms;
using HLAEBReceiveChannelMachine.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Xindeco.Device;

namespace HLAEBReceiveChannelMachine
{
    public partial class InventoryForm : MetroForm
    {
        #region properties

        object _lockObject = new object();

        private List<EbBoxInfo> ebBoxList = null;
        private List<MaterialInfo> materialList = null;
        private List<HLATagInfo> tagList = null;
        private List<ListViewTagInfo> lvtagList = new List<ListViewTagInfo>();
        private Thread threadRightView = null;
        private Thread threadLoad = null;
        private const string deviceStatus = "检测到[{0}]设备异常，请检查对应设备连接，然后重启软件";
        /// <summary>
        /// 读写器是否正在盘点
        /// </summary>
        private bool isInventory = false;
        /// <summary>
        /// 读写器
        /// </summary>
        UHFReader reader = new UHFReader(Xindeco.Device.Model.UHFReaderType.ImpinjR420);
        private int inventoryResult = 0;
        /// <summary>
        /// 箱码列表
        /// </summary>
        private Queue<string> boxNoList = new Queue<string>();
        /// <summary>
        /// 处理PLC信息的线程
        /// </summary>
        private Thread logicThread = null;
        /// <summary>
        /// 本轮扫描的EPC列表
        /// </summary>
        private List<string> epcList = new List<string>();
        /// <summary>
        /// 读写器最后读取时间
        /// </summary>
        private DateTime lastReadTime = DateTime.Now;
        /// <summary>
        /// 本轮扫描到的标签总数
        /// </summary>
        private int currentNum = 0;
        private List<EbBoxInfo> currentBox = null;
        private StringBuilder errorMsg = new StringBuilder();
        private List<TagDetailInfo> tagDetailList = new List<TagDetailInfo>();
        private DateTime shipDate = new DateTime(1900, 1, 1);
        #endregion

        #region 扫描结果
        /// <summary>
        /// 未扫描到箱码
        /// </summary>
        private const string WEI_SAO_DAO_XIANG_MA = "未扫描到箱号";

        /// <summary>
        /// EPC未注册
        /// </summary>
        private const string EPC_WEI_ZHU_CE = "商品未注册";

        /// <summary>
        /// 未扫描到EPC
        /// </summary>
        private const string WEI_SAO_DAO_EPC = "未扫描到商品";

        /// <summary>
        /// 箱码不一致
        /// </summary>
        private const string XIANG_MA_BU_YI_ZHI = "箱号不一致";

        /// <summary>
        /// 箱码重复使用
        /// </summary>
        private const string XIANG_MA_CHONG_FU_SHI_YONG = "箱号重复使用";

        /// <summary>
        /// 箱子不属于当前发运日期
        /// </summary>
        private const string BU_SHU_YU_DA_QIAN_FA_YUN_RI_QI = "不属于当前发运日期";

        /// <summary>
        /// 当前箱不存在
        /// </summary>
        private const string WEI_ZHAO_DAO_DANG_QIAN_XIANG_SHU_JU = "未找到当前箱数据";
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
        /// 异常指令
        /// </summary>
        private const string ZZ_FS_YI_CHANG = "01030000000305CB";
        #endregion
        public InventoryForm()
        {
            InitializeComponent();
            txtImportBoxNo.KeyPress += TxtImportBoxNo_KeyPress;
            FormClosing += InventoryForm_FormClosing;
        }
        private void InventoryForm_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            lblUser.Text = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "用户异常";
            lblDeviceNo.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.EQUIP_HLA : "设备异常";
            dtShip.Value = DateTime.Now;
            threadLoad = new Thread(new ThreadStart(() => {
                
                this.Invoke(new Action(() => {
                    this.lblLoading.Text = "正在连接PLC...";
                }));
                string deviceString = "";
                if (!initPlcPort())
                    deviceString += "PLC|";
                this.Invoke(new Action(() =>
                {
                    this.lblLoading.Text = "正在连接条码扫描枪...";
                }));
                if(!initScannerPort())
                    deviceString += "条码扫描枪|";
                this.Invoke(new Action(() =>
                {
                    this.lblLoading.Text = "正在连接读写器...";
                }));
                #region 连接读写器
                reader.OnTagReported += Reader_OnTagReported;
                bool result = reader.Connect(SysConfig.ReaderIp, Xindeco.Device.Model.ConnectType.TCP, SynchronizationContext.Current);

                if (!result)
                {
                    deviceString += "读写器|";
                    MetroMessageBox.Show(this, "连接读写器失败!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LocalDataService.InsertErrorDataRecord(ERRORDATATYPE.读写器未连接, "设备号：" + lblDeviceNo.Text);
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        btnStart.Enabled = true;
                    }));
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

                if(deviceString.EndsWith("|"))
                {
                    deviceString = deviceString.Remove(deviceString.Length - 1, 1);
                }
                this.Invoke(new Action(() =>
                {
                    this.panelLoading.Hide();
                    this.lblLoading.Text = "";
                    if(deviceString.Length>0)
                    {
                        lblDeviceStatus.Text = string.Format(deviceStatus, deviceString);
                        lblDeviceStatus.Show();
                    }
                }));
            }));
            threadLoad.IsBackground = true;
            threadLoad.Start();

            //启动上传队列
            UploadServer.GetInstance().Start();

            this.lblHU.Text = "";
            this.lblQTY.Text = "0";
            this.lblScanNum.Text = "0";
            this.lblRightNum.Text = "0";
            this.lblErrorNum.Text = "0";
            this.lblStatus.Text = "停止";
            this.lblResult.Text = "";
            this.txtImportBoxNo.Focus();
        }

        private void Reader_OnTagReported(Xindeco.Device.Model.TagInfo taginfo)
        {
            if (!this.isInventory || taginfo == null || string.IsNullOrEmpty(taginfo.Epc) || this.epcList.Contains(taginfo.Epc))
                return;
            this.lastReadTime = DateTime.Now;
            this.currentNum++;
            this.Invoke(new Action(() =>
            {
                this.lblScanNum.Text = this.currentNum.ToString(); //更新扫描总数
            }));
            this.epcList.Add(taginfo.Epc);
            TagDetailInfo tdi = getTagDetailInfoByEpc(taginfo.Epc);
            if (tdi != null)
            {
                tagDetailList.Add(tdi);
                if (!tdi.IsAddEpc)  //当扫描到主条码时合法数 + 1
                {
                    this.Invoke(new Action(() =>
                    {
                        this.lblRightNum.Text = (int.Parse(this.lblRightNum.Text) + 1).ToString();
                    }));
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
                        this.lblStatus.Text = "停止扫描";
                        this.panelLoading.Hide();
                    }));
                    this.isInventory = false;
                    reader.StopInventory();
                    if (!btnStart.Enabled)
                    {
                        //等待基础数据加载完成后再判断数据
                        while (startThread != null && startThread.IsAlive)
                        {
                            Thread.Sleep(200);
                        }
                        bool result = CheckData();
                        
                        EnqueueUploadData(GetCurrentCheckRecord(result));
                        EnqueueUploadData(GetCurrentUploadEbBox(result));

                        if(!isMultiSku)
                        {
                            //单一SKU的，打印正常标签
                            PrintHelper.PrintRightBoxTag(
                                SAPDataService.GetShelvesPosition(SysConfig.LGNUM, (tagDetailList!=null&& tagDetailList.Count>0) ? tagDetailList.FirstOrDefault().MATNR : ""),
                                lblHU.Text, lvtagList, result);
                        }
                        else
                        {
                            //多SKU的，打印异常标签
                            PrintHelper.PrintErrorBoxTag(lvtagList);
                        }
                    }
                    else
                        MetroMessageBox.Show(this,
                            "未开始复核，请点击【开始】按钮进行复核\r\n通道机内若有箱子请先使用手动控制功能移出",
                            "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                    return false;
                }
            }
            return true;
        }
        private string currentHu = "";
        private bool StartInventory()
        {
            if (this.isInventory == false)
            {
                try
                {
                    epcList.Clear();
                    currentBox = null;
                    isMultiSku = false;
                    errorMsg.Clear();
                    tagDetailList.Clear();
                    inventoryResult = 0;
                    currentNum = 0;
                    rowColor = this.rowColor == Color.LimeGreen ? Color.White : Color.LimeGreen;
                    lastReadTime = DateTime.Now;
                    lvtagList.Clear();
                    //清除当前屏幕统计数量
                    Invoke(new Action(() =>
                    {
                        lblHU.Text = "";
                        lblQTY.Text = "0";
                        lblScanNum.Text = "0";
                        lblRightNum.Text = "0";
                        lblErrorNum.Text = "0";
                        lblStatus.Text = "正在扫描";
                        lblResult.Text = "";
                        panelLoading.Show();
                        lblLoading.Text = "";
                    }));
                    int i, j, k;
                    LocalDataService.GetGhostAndTrigger(out i, out j, out k);
                    reader.StartInventory(i, j, k);
                    isInventory = true;
                    if (boxNoList.Count > 0)
                    {
                        string boxno = boxNoList.Dequeue();
                        currentBox = GetCurrentEbBox(boxno);
                        Invoke(new Action(() =>
                        {
                            lblHU.Text = boxno;
                            if (currentBox != null)
                                lblQTY.Text = currentBox.Sum(x => x.QTY).ToString();
                        }));
                    }
                    currentHu = lblHU.Text;
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
        /// 设置盘点结果 
        /// </summary>
        /// <param name="result">1正常 3异常</param>
        /// <param name="message">错误信息</param>
        private void SetInventoryResult(int result, string message)
        {
            this.inventoryResult = result;
            if (!string.IsNullOrEmpty(message))
            {
                if (!errorMsg.ToString().Contains(message))
                    errorMsg.AppendFormat("{0};", message);
            }
        }

        private List<EbBoxInfo> GetCurrentEbBox(string hu)
        {
            string errormsg;
            List<EbBoxInfo> currentBoxList = null;
            List<EbBoxInfo> result = new List<EbBoxInfo>();

            lock (_lockObject)
            {
                if (ebBoxList != null)
                    currentBoxList = ebBoxList.FindAll(i => i.HU == hu);
            }

            if (currentBoxList == null || currentBoxList.Count <= 0)
                currentBoxList = LocalDataService.GetEbBoxList(shipDate, hu, HLACommonLib.Model.ENUM.CheckType.电商收货复核);
            if (currentBoxList == null || currentBoxList.Count <= 0)
                currentBoxList = SAPDataService.GetEbBoxList(SysConfig.LGNUM, hu, "", "", out errormsg, "D");
            foreach(EbBoxInfo box in currentBoxList)
            {
                if(result.Exists(i=>i.PRODUCTNO == box.PRODUCTNO))
                {
                    result.Find(i => i.PRODUCTNO == box.PRODUCTNO).QTY += box.QTY;
                }
                else
                {
                    result.Add(box);
                }
            }
            return result;
        }

        private EbBoxCheckRecordInfo GetCurrentCheckRecord(bool inventoryResult)
        {
            EbBoxCheckRecordInfo result = new EbBoxCheckRecordInfo();
            result.HU = this.lblHU.Text;
            result.PQTY = int.Parse(this.lblQTY.Text);
            result.RQTY = int.Parse(this.lblScanNum.Text);
            result.STATUS = inventoryResult ? 1 : 0;
            return result;
        }

        private UploadEbBoxInfo GetCurrentUploadEbBox(bool inventoryResult)
        {
            return new UploadEbBoxInfo()
            {
                ChangeTime = DateTime.Now,
                ErrorMsg = errorMsg.ToString(),
                Guid = Guid.NewGuid().ToString(),
                HU = this.lblHU.Text,
                InventoryResult = inventoryResult,
                LGNUM = SysConfig.LGNUM,
                EQUIP_HLA = SysConfig.DeviceInfo.EQUIP_HLA,
                SubUser = SysConfig.CurrentLoginUser.UserId,
                TagDetailList = new List<TagDetailInfo>(this.tagDetailList),
            };
        }

        private void QueryBox()
        {
            string hu = txtImportBoxNo.Text.Trim();
            txtImportBoxNo.Clear();
            EbBoxCheckRecordInfo checkRecord = null;
            if (Cache.Instance[CacheKey.CHECK_RECORD] != null)
                checkRecord = (Cache.Instance[CacheKey.CHECK_RECORD] as List<EbBoxCheckRecordInfo>).FindLast(i => i.HU == hu);
            if (checkRecord == null)
            {
                checkRecord = LocalDataService.GetLastEbCheckRecord(hu, HLACommonLib.Model.ENUM.CheckType.电商收货复核);
            }
            if (checkRecord == null)
            {
                MetroMessageBox.Show(this, string.Format("未查找到 {0} 箱的记录", hu), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<EbBoxErrorRecordInfo> errorList = null;
            if (Cache.Instance[CacheKey.ERROR_RECORD] != null)
                errorList = (Cache.Instance[CacheKey.ERROR_RECORD] as List<EbBoxErrorRecordInfo>).FindAll(i => i.HU == hu);
            EbBoxDetailForm form = new EbBoxDetailForm(checkRecord, errorList);
            form.ShowDialog();
        }
        /// <summary>
        /// 检查数据 
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            bool result = true;

            if (string.IsNullOrEmpty(this.lblHU.Text.Trim()))
            {
                SetInventoryResult(0, WEI_SAO_DAO_XIANG_MA);
                result = false;

                LocalDataService.InsertErrorDataRecord(ERRORDATATYPE.未扫描到箱号, "设备号：" + lblDeviceNo.Text);
            }
            else
            {
                LocalDataService.InsertErrorDataRecord(ERRORDATATYPE.正常, "设备号：" + lblDeviceNo.Text);

                if (this.currentBox != null && this.currentBox.Count > 0)
                {
                    //检查当前箱子是否是当前选择的发运日期
                    if (this.currentBox[0].SHIPDATE != shipDate)
                    {
                        SetInventoryResult(0, BU_SHU_YU_DA_QIAN_FA_YUN_RI_QI);
                        result = false;
                    }
                }
            }

            if (this.lblErrorNum.Text.Trim() != "0")
            {
                SetInventoryResult(0, EPC_WEI_ZHU_CE);
                result = false;
            }

            if (this.epcList.Count == 0)
            {
                SetInventoryResult(0, WEI_SAO_DAO_EPC);
                result = false;
            }

            if (this.boxNoList.Count > 0)
            {
                boxNoList.Clear();
                SetInventoryResult(0, XIANG_MA_BU_YI_ZHI);
                result = false;
            }
            List<string> matnrList = new List<string>();

            if (tagDetailList != null)
                tagDetailList.ForEach(new Action<TagDetailInfo>((tag) => {
                    if (!matnrList.Contains(tag.MATNR))
                        matnrList.Add(tag.MATNR);
                    if (!lvtagList.Exists(i => i.MATNR == tag.MATNR))
                        lvtagList.Add(new ListViewTagInfo(
                            tag.MATNR, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, tag.CHARG,
                            tagDetailList.FindAll(x => x.MATNR == tag.MATNR && !x.IsAddEpc).Count));
                }));

            //判断当前读取的标签信息中，是否是多SKU
            if (matnrList.Count > 1)
            {
                isMultiSku = true;
            }
            else
            {
                isMultiSku = false;
            }

            if (this.currentBox != null)
            {
                currentBox.ForEach(new Action<EbBoxInfo>((box) => {
                    if (!matnrList.Contains(box.PRODUCTNO))
                        matnrList.Add(box.PRODUCTNO);
                }));
            }

            foreach (string matnr in matnrList)
            {
                List<TagDetailInfo> scanList = tagDetailList == null ? null : tagDetailList.FindAll(i => i.MATNR == matnr);
                int scanCount = scanList == null ? 0 : scanList.Count;
                List<EbBoxInfo> currentBoxList = currentBox == null ? null : currentBox.FindAll(i => i.PRODUCTNO == matnr);
                int boxCount = currentBoxList == null ? 0 : currentBoxList.Sum(i => i.QTY);
                int diff = scanCount - boxCount;

                if (diff != 0)
                {
                    if (currentBoxList != null && currentBoxList.Count > 0)
                    {
                        //存在差异,记录错误信息
                        result = false;
                        EbBoxErrorRecordInfo error = new EbBoxErrorRecordInfo();
                        error.DIFF = diff;
                        error.HU = currentBoxList[0].HU;
                        error.PRODUCTNO = currentBoxList[0].PRODUCTNO;
                        error.REMARK = errorMsg.ToString();
                        if (scanList != null && scanList.Count > 0)
                        {
                            error.ZCOLSN = scanList[0].ZCOLSN;
                            error.ZSATNR = scanList[0].ZSATNR;
                            error.ZSIZTX = scanList[0].ZSIZTX;
                        }
                        else if (currentBoxList != null && currentBoxList.Count > 0)
                        {
                            MaterialInfo material = materialList.Find(i => i.MATNR == currentBoxList[0].PRODUCTNO);
                            if (material != null)
                            {
                                error.ZCOLSN = material.ZCOLSN;
                                error.ZSATNR = material.ZSATNR;
                                error.ZSIZTX = material.ZSIZTX;
                            }
                            else
                            {
                                error.ZCOLSN = "";
                                error.ZSATNR = "";
                                error.ZSIZTX = "";
                            }
                        }
                        else
                        {
                            error.ZCOLSN = "";
                            error.ZSATNR = "";
                            error.ZSIZTX = "";
                        }
                        EnqueueUploadData(error);
                    }
                    else
                    {
                        result = false;
                        //SetInventoryResult(0, WEI_ZHAO_DAO_DANG_QIAN_XIANG_SHU_JU);
                        EbBoxErrorRecordInfo error = new EbBoxErrorRecordInfo();
                        error.DIFF = diff;
                        error.HU = this.lblHU.Text;
                        error.REMARK = errorMsg.ToString();
                        if (scanList != null && scanList.Count > 0)
                        {
                            error.PRODUCTNO = scanList[0].MATNR;
                            error.ZCOLSN = scanList[0].ZCOLSN;
                            error.ZSATNR = scanList[0].ZSATNR;
                            error.ZSIZTX = scanList[0].ZSIZTX;
                        }
                        else
                        {
                            error.PRODUCTNO = "";
                            error.ZCOLSN = "";
                            error.ZSATNR = "";
                            error.ZSIZTX = "";
                        }
                        EnqueueUploadData(error);
                    }
                }

                if (this.currentBox == null || this.currentBox.Count <= 0)
                {
                    SetInventoryResult(0, WEI_ZHAO_DAO_DANG_QIAN_XIANG_SHU_JU);
                }

            }
            if (matnrList.Count == 0)
            {
                EbBoxErrorRecordInfo error = new EbBoxErrorRecordInfo();
                error.DIFF = 0;
                error.HU = this.lblHU.Text;
                error.REMARK = errorMsg.ToString();
                error.PRODUCTNO = "";
                error.ZCOLSN = "";
                error.ZSATNR = "";
                error.ZSIZTX = "";
                EnqueueUploadData(error);
            }

            if (result)
            {
                if (isMultiSku)
                {
                    //多SKU，走异常口，显示正常
                    SetInventoryResult(3, "正常");
                }
                else
                {
                    //单SKU
                    SetInventoryResult(1, "");
                }
            }
            else
            {
                SetInventoryResult(3, "");
            }
            return result;
        }
        private bool isMultiSku = false;
        /// <summary>
        /// 将所有需要异步上传的数据都加入此队列
        /// </summary>
        /// <param name="obj"></param>
        private bool EnqueueUploadData(object obj)
        {
            if (UploadServer.GetInstance().CurrentUploadQueue.Count > 199)
            {
                MetroMessageBox.Show(this, "装箱数据上传队列已满,请检查网络环境,以确保数据能正常上传", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (obj.GetType() == typeof(EbBoxErrorRecordInfo))
            {
                if (Cache.Instance[CacheKey.ERROR_RECORD] != null)
                {
                    (Cache.Instance[CacheKey.ERROR_RECORD] as List<EbBoxErrorRecordInfo>).Add(obj as EbBoxErrorRecordInfo);
                }
            }
            else if (obj.GetType() == typeof(EbBoxCheckRecordInfo))
            {
                if (Cache.Instance[CacheKey.CHECK_RECORD] != null)
                {
                    (Cache.Instance[CacheKey.CHECK_RECORD] as List<EbBoxCheckRecordInfo>).Add(obj as EbBoxCheckRecordInfo);
                }
                AddCheckRecord(obj as EbBoxCheckRecordInfo);
            }
            else if (obj.GetType() == typeof(UploadEbBoxInfo))
            {
                SqliteDataService.InsertUploadData(obj as UploadEbBoxInfo);
            }
            UploadServer.GetInstance().CurrentUploadQueue.Push(obj);
            return true;
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
            HLATagInfo tag = tagList.FirstOrDefault(i => i.RFID_EPC == rfidEpc || i.RFID_ADD_EPC == rfidAddEpc);
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

        #region barcode scanner property
        /// <summary>
        /// 条码扫描模组1
        /// </summary>
        BarcodeDevice scannerPort_1 = null;  //条码扫描枪串口
        /// <summary>
        /// 条码扫描模组2
        /// </summary>
        BarcodeDevice scannerPort_2 = null;  //条码扫描枪串口
        #endregion
        /// <summary>
        /// 初始化扫描模组串口
        /// </summary>
        private bool initScannerPort()
        {
            bool result = true;
            #region 连接条码扫描器串口-add by jrzhuang
            scannerPort_1 = new  BarcodeDevice(SysConfig.ScannerPort_1);
            scannerPort_1.BaudRate = 9600;//波特率
            scannerPort_1.Parity = Parity.None;//无奇偶校验位
            scannerPort_1.StopBits = StopBits.One;//一个停止位
            scannerPort_1.DataBits = 8;
            scannerPort_1.ReadTimeout = 200;
            scannerPort_1.ReadBufferSize = 8;
            //scannerPort_1.DataReceived += new SerialDataReceivedEventHandler(scannerPort_1_DataReceived);
            scannerPort_1.OnBarcodeReported += ScannerPort_1_OnBarcodeReported;
            bool isOpen = false;
            try
            {
                isOpen = scannerPort_1.Connect();
            }
            catch (Exception ex)
            {
                result = false;
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            if (!isOpen)
            {
                result = false;
                MetroMessageBox.Show(this, "连接条码扫描枪串口设备1失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            scannerPort_2 = new  BarcodeDevice(SysConfig.ScannerPort_2);
            scannerPort_2.BaudRate = 9600;//波特率
            scannerPort_2.Parity = Parity.None;//无奇偶校验位
            scannerPort_2.StopBits = StopBits.One;//一个停止位
            scannerPort_2.DataBits = 8;
            scannerPort_2.ReadTimeout = 200;
            scannerPort_2.ReadBufferSize = 8;
            //scannerPort_2.DataReceived += new SerialDataReceivedEventHandler(scannerPort_2_DataReceived);
            scannerPort_2.OnBarcodeReported += ScannerPort_2_OnBarcodeReported;
            try
            {
                isOpen = scannerPort_2.Connect();
            }
            catch (Exception ex)
            {
                result = false;
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            if (!isOpen)
            {
                result = false;
                MetroMessageBox.Show(this, "连接条码扫描枪串口设备2失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #endregion
            return result;
        }

        private void ScannerPort_2_OnBarcodeReported(string barcode)
        {
            if (!boxNoList.Contains(barcode))
            {
                boxNoList.Enqueue(barcode);
            }
        }

        private void ScannerPort_1_OnBarcodeReported(string barcode)
        {
            if (!boxNoList.Contains(barcode))
            {
                boxNoList.Enqueue(barcode);
            }
        }

        //void scannerPort_2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    try
        //    {
        //        int n = scannerPort_2.BytesToRead;
        //        byte[] buf = new byte[n];
        //        scannerPort_2.Read(buf, 0, n);
        //        scannerPortReadHandle(buf, 1);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
        //    }
        //}

        //void scannerPort_1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    try
        //    {
        //        int n = scannerPort_1.BytesToRead;
        //        byte[] buf = new byte[n];
        //        scannerPort_1.Read(buf, 0, n);
        //        scannerPortReadHandle(buf, 1);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
        //    }
        //}

        private string barcode = "";
        /// <summary>
        /// 有两个扫描模组，重构出一个公共的数据读取接口
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="scannerPortNo"></param>
        //private void scannerPortReadHandle(byte[] buf, int scannerPortNo)
        //{
        //    //string hexStr = TypeConvert.ByteArrayToHexString(buf);
        //    if (barcode.EndsWith("\r\n"))
        //        barcode = "";
        //    barcode += System.Text.Encoding.Default.GetString(buf);
        //    if (barcode.EndsWith("\r\n"))
        //    {
        //        //以回车和换行结尾，表示合法数据
        //        string barcodestring = barcode.Replace("\r\n", "");

        //        if (!boxNoList.Contains(barcodestring))
        //        {
        //            boxNoList.Enqueue(barcodestring);
        //        }
        //    }
        //}

        PLCController port = null;
        //private List<byte> comBuffer = new List<byte>(4096);
        //private Queue<string> comDataQueue = new Queue<string>();
        /// <summary>
        /// 初始化PLC串口通信
        /// </summary>
        private bool initPlcPort()
        {
            #region 连接串口
            port = new PLCController(SysConfig.Port);
            port.BaudRate = 9600;//波特率
            port.Parity = Parity.None;//无奇偶校验位
            port.StopBits = StopBits.One;//一个停止位
            port.DataBits = 8;
            port.ReadTimeout = 200;
            port.ReadBufferSize = 8;

            //port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);//DataReceived事件委托
            port.OnPLCDataReported += Port_OnPLCDataReported;
            bool isOpen = false;
            try
            {
                isOpen = port.Connect();
            }
            catch { }

            if (!isOpen)
            {
                MetroMessageBox.Show(this, "连接PLC设备失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
            #endregion
        }

        private void Port_OnPLCDataReported(Xindeco.Device.Model.PLCData plcData)
        {
            if (plcData.Command == Xindeco.Device.Model.PLCRequest.OPEN_RFID)
            {
                StartInventory();
            }
            else if (plcData.Command == Xindeco.Device.Model.PLCRequest.ASK_RESULT)
            {
                int inventoryResult = this.inventoryResult;
                switch (inventoryResult)
                {
                    case 1://正常
                        StopInventory();
                        port.SendCommand(Xindeco.Device.Model.PLCResponse.RIGHT);
                        break;
                    case 3://异常
                        StopInventory();
                        port.SendCommand(Xindeco.Device.Model.PLCResponse.ERROR);
                        break;
                    default:

                        break;
                }
            }
        }

        private Color rowColor = Color.LimeGreen;
        private void initHistoryRecord(List<string> huList)
        {
            List<EbBoxErrorRecordInfo> errorRecordList = LocalDataService.GetEbBoxErrorRecordList(huList, HLACommonLib.Model.ENUM.CheckType.电商收货复核);
            List<EbBoxCheckRecordInfo> checkRecordList = LocalDataService.GetEbCheckRecordList(huList, HLACommonLib.Model.ENUM.CheckType.电商收货复核);


            if (checkRecordList != null && checkRecordList.Count > 0)
            {
                Cache.Instance[CacheKey.CHECK_RECORD] = checkRecordList;
                this.Invoke(new Action(() =>
                {
                    grid2.Rows.Clear();
                    foreach (EbBoxCheckRecordInfo item in checkRecordList)
                    {
                        AddCheckRecord(item);
                    }
                }));
            }

        }
        public void AddCheckRecord(EbBoxCheckRecordInfo item)
        {
            grid2.Rows.Insert(0, item.HU, item.PQTY, item.RQTY, item.STATUS == 1 ? "正常" : "异常");
            grid2.Rows[0].DefaultCellStyle.BackColor = item.STATUS == 1 ? Color.White : Color.OrangeRed;
        }
        private void ErrorForm_OnClose()
        {
            if (threadRightView != null)
            {
                threadRightView.Abort();
                threadRightView = null;
            }
        }

        private void InventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UploadServer.GetInstance().CheckUndoTask())
            {
                if (MetroMessageBox.Show(this, "当前有未上传的队列数据，是否确认现在退出?\r\n【注意】退出可能导致数据丢失，请谨慎操作", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    e.Cancel = true;
            }
            StopInventory();
            timer.Stop();
            reader.Disconnect();
            if (port != null)
            {
                port.Disconnect();
            }
            if (scannerPort_1 != null)
                scannerPort_1.Disconnect();
            if (scannerPort_2 != null)
                scannerPort_2.Disconnect();
            if (this.logicThread != null)
                this.logicThread.Abort();
            if (this.threadRightView != null)
                this.threadRightView.Abort();
            if (this.threadLoad != null)
                this.threadLoad.Abort();
                
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.isInventory)
            {
                //当前正在盘点，则判断上次读取时间和现在读取时间
                if (int.Parse(this.lblRightNum.Text) < int.Parse(this.lblQTY.Text))
                {
                    if ((DateTime.Now - this.lastReadTime).TotalMilliseconds >= Math.Max(2000, SysConfig.DelayTime))
                    {
                        StopInventory();
                    }
                }
                else
                {
                    if ((DateTime.Now - this.lastReadTime).TotalMilliseconds >= SysConfig.DelayTime)
                    {
                        //1S钟内没有读到新的标签，则未为本箱检测完毕，关闭射频，检查结果
                        StopInventory();
                    }
                }
            }

            this.Invoke(new Action(() =>
            {
                this.lblUploadQueue.Text = UploadServer.GetInstance().CurrentUploadQueue.Count.ToString();
            }));
            
        }

        private void TxtImportBoxNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                QueryBox();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tileOptGroupDetail_Click(object sender, EventArgs e)
        {
            GxForm form = new GxForm();
            form.ShowDialog();
        }
        Thread startThread = null;
        private void btnStart_Click(object sender, EventArgs e)
        {
            
            if (threadLoad != null && threadLoad.IsAlive)
            {
                MetroMessageBox.Show(this, "请等待系统连接读写器成功之后再操作", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.btnStart.Enabled = false;
            DateTime shipDate = this.dtShip.Value.Date;
            startThread = new Thread(new ThreadStart(() =>
            {
                this.Invoke(new Action(() =>
                {
                    panelLoading.Show();
                    lblLoading.Text = "正在下载发运箱数据...";
                }));

                //List<EbBoxInfo> boxList = LocalDataService.GetEbBoxList(shipDate, "", HLACommonLib.Model.ENUM.CheckType.电商收货复核);
                string errormsg;
                List<EbBoxInfo> boxList = SAPDataService.GetEbBoxList(SysConfig.LGNUM, "", shipDate.ToString("yyyy-MM-dd"), "",out errormsg, "D");
                if (boxList.Count == 0)
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show(string.Format("未下载到装箱信息，请联系SAP确认{0}是否有待复核信息\r\n错误信息：{1}", 
                            shipDate.ToString("yyyy-MM-dd"),errormsg));
                    }));
                }
                this.Invoke(new Action(() => {
                    this.panelLoading.Show();
                    this.lblLoading.Text = "正在加载历史信息...";
                }));
                if (boxList != null && boxList.Count > 0)
                {
                    List<string> huList = new List<string>();
                    foreach (EbBoxInfo box in boxList)
                    {
                        if (huList.Contains(box.HU))
                            continue;
                        huList.Add(box.HU);
                    }
                    if (huList.Count > 0)
                    {
                        //初始化历史记录
                        initHistoryRecord(huList);
                    }
                }

                if (boxList == null) boxList = new List<EbBoxInfo>();

                this.Invoke(new Action(() =>
                {
                    lblLoading.Text = "正在下载物料主数据...";
                }));
                List<MaterialInfo> materialList;
                if (Cache.Instance[CacheKey.MATERIAL] == null)
                {

                    //materialList = LocalDataService.GetMaterialInfoList();
                    materialList = SAPDataService.GetMaterialInfoListAll(SysConfig.LGNUM);
                    if (materialList == null || materialList.Count <= 0)
                    {
                        MetroMessageBox.Show(this, "未下载到物料主数据，请检查网络情况", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetEbBoxOver();
                        return;
                    }
                    Cache.Instance[CacheKey.MATERIAL] = materialList;
                }
                else
                {
                    materialList = Cache.Instance[CacheKey.MATERIAL] as List<MaterialInfo>;
                }
                this.Invoke(new Action(() =>
                {
                    lblLoading.Text = "正在下载吊牌数据...";
                }));
                List<HLATagInfo> tagList;
                if (Cache.Instance[CacheKey.TAG] == null)
                {

                    tagList = LocalDataService.GetAllRfidHlaTagList();
                    if (tagList == null || tagList.Count <= 0)
                    {
                        MetroMessageBox.Show(this, "未下载到吊牌数据，请检查网络情况", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetEbBoxOver();
                        return;
                    }
                    Cache.Instance[CacheKey.TAG] = tagList;
                }
                else
                {
                    tagList = Cache.Instance[CacheKey.TAG] as List<HLATagInfo>;
                }
                this.Invoke(new Action(() =>
                {
                    btnStop.Enabled = true;
                    panelLoading.Hide();
                    lblLoading.Text = "";
                }));
                LoadBasicInfo(shipDate, boxList, materialList, tagList);
            }));
            startThread.IsBackground = true;
            startThread.Start();
            
        }
        private void LoadBasicInfo(DateTime _shipDate, List<EbBoxInfo> _ebBoxList, List<MaterialInfo> _materialList, List<HLATagInfo> _tagList)
        {
            shipDate = _shipDate;
            lock (_lockObject)
            {
                ebBoxList = _ebBoxList;
            }
            materialList = _materialList;
            tagList = _tagList;
        }
        private void GetEbBoxOver()
        {

            this.Invoke(new Action(() =>
            {
                this.btnStart.Enabled = true;
                panelLoading.Hide();
                lblLoading.Text = "";
            }));
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryBox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartInventory();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StopInventory();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reader_OnTagReported(new Xindeco.Device.Model.TagInfo() { Epc = textBox1.Text });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                panelLoading.Show();
                lblLoading.Text = "正在重新下载发运箱数据...";
            }));

            string errormsg;
            List<EbBoxInfo> boxList = SAPDataService.GetEbBoxList(SysConfig.LGNUM, "", shipDate.ToString("yyyy-MM-dd"), "", out errormsg, "D");

            if(boxList!=null && boxList.Count>0)
            {
                lock(_lockObject)
                {
                    ebBoxList = boxList;
                }
            }

            this.Invoke(new Action(() =>
            {
                panelLoading.Hide();
                lblLoading.Text = "";
            }));
        }
    }
}
