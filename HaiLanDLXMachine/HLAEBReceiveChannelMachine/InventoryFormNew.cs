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
using HLACommonView.Views;
using HLACommonView.Model;

namespace HLAEBReceiveChannelMachine
{
    public partial class InventoryFormNew : CommonInventoryFormIMP
    {
        #region properties

        object _lockObject = new object();

        private List<EbBoxInfo> ebBoxList = null;
        private List<HLATagInfo> tagList = null;
        private List<ListViewTagInfo> lvtagList = new List<ListViewTagInfo>();
        private Thread threadRightView = null;
        private Thread threadLoad = null;
        private const string deviceStatus = "检测到[{0}]设备异常，请检查对应设备连接，然后重启软件";
        /// <summary>
        /// 处理PLC信息的线程
        /// </summary>
        private Thread logicThread = null;
        private List<EbBoxInfo> currentBox = null;
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

        public InventoryFormNew()
        {
            InitializeComponent();
            txtImportBoxNo.KeyPress += TxtImportBoxNo_KeyPress;
            FormClosing += InventoryForm_FormClosing;
            InitDevice(Xindeco.Device.Model.UHFReaderType.ImpinjR420, true);
        }
        private void InventoryForm_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            lblUser.Text = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "用户异常";
            lblDeviceNo.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.EQUIP_HLA : "设备异常";
            dtShip.Value = DateTime.Now;
            threadLoad = new Thread(new ThreadStart(() => {

                ShowLoading("正在连接PLC...");
                string deviceString = "";
                if (!ConnectPlc())
                    deviceString += "PLC|";
                ShowLoading("正在连接条码扫描枪...");
                ConnectBarcode();
                ShowLoading("正在连接读写器...");

                if (!ConnectReader())
                {
                    deviceString += "读写器|";
                    MetroMessageBox.Show(this, "连接读写器失败!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                if(deviceString.EndsWith("|"))
                {
                    deviceString = deviceString.Remove(deviceString.Length - 1, 1);
                }
                this.Invoke(new Action(() =>
                {
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
        public override void UpdateView()
        {
            this.Invoke(new Action(() =>
            {
                this.lblScanNum.Text = epcList.Count.ToString(); //更新扫描总数
                this.lblRightNum.Text = mainEpcNumber.ToString();
                this.lblErrorNum.Text = errorEpcNumber.ToString();
            }));
        }


        /// <summary>
        /// 停止盘点
        /// </summary>
        public override void StopInventory()
        {
            //判断是否正在盘点，正在盘点则停止盘点
            if (this.isInventory == true)
            {
                try
                {
                    this.Invoke(new Action(() =>
                    {
                        this.lblStatus.Text = "停止扫描";
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
                        CheckResult result = CheckData();
                        
                        EnqueueUploadData(GetCurrentCheckRecord(result.InventoryResult));
                        EnqueueUploadData(GetCurrentUploadEbBox(result));

                        if(!isMultiSku)
                        {
                            //单一SKU的，打印正常标签
                            PrintHelper.PrintRightBoxTag(
                                SAPDataService.GetShelvesPosition(SysConfig.LGNUM, (tagDetailList!=null&& tagDetailList.Count>0) ? tagDetailList.FirstOrDefault().MATNR : ""),
                                lblHU.Text, lvtagList, result.InventoryResult);
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
                }
            }
        }
        private string currentHu = "";
        public override void StartInventory()
        {
            if (this.isInventory == false)
            {
                try
                {
                    SetInventoryResult(0);
                    errorEpcNumber = 0;
                    mainEpcNumber = 0;
                    addEpcNumber = 0;
                    epcList.Clear();
                    tagDetailList.Clear();

                    currentBox = null;
                    isMultiSku = false;
                    rowColor = this.rowColor == Color.LimeGreen ? Color.White : Color.LimeGreen;
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
                    }));
                    reader.StartInventory(0, 0, 0);
                    isInventory = true;
                    lastReadTime = DateTime.Now;

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
                }
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

        private UploadEbBoxInfo GetCurrentUploadEbBox(CheckResult inventoryResult)
        {
            return new UploadEbBoxInfo()
            {
                ChangeTime = DateTime.Now,
                ErrorMsg = inventoryResult.Message,
                Guid = Guid.NewGuid().ToString(),
                HU = this.lblHU.Text,
                InventoryResult = inventoryResult.InventoryResult,
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
        public override CheckResult CheckData()
        {
            CheckResult cr = new CheckResult();

            if (string.IsNullOrEmpty(this.lblHU.Text.Trim()))
            {
                cr.UpdateMessage(WEI_SAO_DAO_XIANG_MA);
                cr.InventoryResult = false;
            }
            else
            {
                if (this.currentBox != null && this.currentBox.Count > 0)
                {
                    //检查当前箱子是否是当前选择的发运日期
                    if (this.currentBox[0].SHIPDATE != shipDate)
                    {
                        cr.UpdateMessage(BU_SHU_YU_DA_QIAN_FA_YUN_RI_QI);
                        cr.InventoryResult = false;
                    }
                }
            }

            if (this.lblErrorNum.Text.Trim() != "0")
            {
                cr.UpdateMessage(EPC_WEI_ZHU_CE);
                cr.InventoryResult = false;
            }

            if (this.epcList.Count == 0)
            {
                cr.UpdateMessage(WEI_SAO_DAO_EPC);
                cr.InventoryResult = false;
            }

            if (this.boxNoList.Count > 0)
            {
                boxNoList.Clear();
                cr.UpdateMessage(XIANG_MA_BU_YI_ZHI);
                cr.InventoryResult = false;
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
                        cr.InventoryResult = false;
                        EbBoxErrorRecordInfo error = new EbBoxErrorRecordInfo();
                        error.DIFF = diff;
                        error.HU = currentBoxList[0].HU;
                        error.PRODUCTNO = currentBoxList[0].PRODUCTNO;
                        error.REMARK = cr.Message;
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
                        cr.InventoryResult = false;
                        EbBoxErrorRecordInfo error = new EbBoxErrorRecordInfo();
                        error.DIFF = diff;
                        error.HU = this.lblHU.Text;
                        error.REMARK = cr.Message;
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
                    cr.UpdateMessage(WEI_ZHAO_DAO_DANG_QIAN_XIANG_SHU_JU);
                    cr.InventoryResult = false;
                }

            }
            if (matnrList.Count == 0)
            {
                EbBoxErrorRecordInfo error = new EbBoxErrorRecordInfo();
                error.DIFF = 0;
                error.HU = this.lblHU.Text;
                error.REMARK = cr.Message;
                error.PRODUCTNO = "";
                error.ZCOLSN = "";
                error.ZSATNR = "";
                error.ZSIZTX = "";
                EnqueueUploadData(error);
            }

            if (cr.InventoryResult)
            {
                if (isMultiSku)
                {
                    //多SKU，走异常口，显示正常
                    cr.UpdateMessage("正常");
                    SetInventoryResult(3);
                }
                else
                {
                    //单SKU
                    SetInventoryResult(1);
                }
            }
            else
            {
                SetInventoryResult(3);
            }
            return cr;
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
            timer.Stop();
            if (this.logicThread != null)
                this.logicThread.Abort();
            if (this.threadRightView != null)
                this.threadRightView.Abort();
            if (this.threadLoad != null)
                this.threadLoad.Abort();

            CloseWindow();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
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
            HLACommonView.Views.GxForm form = new HLACommonView.Views.GxForm();
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
                ShowLoading("正在下载发运箱数据...");

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
                ShowLoading("正在加载历史信息...");

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

                ShowLoading("正在下载物料主数据...");

                List<MaterialInfo> materialList;
                if (Cache.Instance[CacheKey.MATERIAL] == null)
                {
                    //materialList = SAPDataService.GetMaterialInfoListAll(SysConfig.LGNUM);
                    materialList = LocalDataService.GetMaterialInfoList();

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

                ShowLoading("正在下载吊牌数据...");

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
                }));
                HideLoading();

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
            }));
            HideLoading();

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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowLoading("正在重新下载发运箱数据...");

            string errormsg;
            List<EbBoxInfo> boxList = SAPDataService.GetEbBoxList(SysConfig.LGNUM, "", shipDate.ToString("yyyy-MM-dd"), "", out errormsg, "D");

            if(boxList!=null && boxList.Count>0)
            {
                lock(_lockObject)
                {
                    ebBoxList = boxList;
                }
            }

            HideLoading();
        }
    }
}
