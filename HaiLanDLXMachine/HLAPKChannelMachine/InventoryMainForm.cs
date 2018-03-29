﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DMSkin;
using HLACommonLib;
using HLACommonLib.Model;
using HLACommonLib.Model.PK;
using HLAPKChannelMachine.DialogForms;
using HLAPKChannelMachine.Utils;
using Xindeco.Device;
using Newtonsoft.Json;

namespace HLAPKChannelMachine
{
    public partial class InventoryMainForm : MetroForm
    {
        #region 属性
        private List<MaterialInfo> materialList = new List<MaterialInfo>();
        private List<HLATagInfo> tagList = new List<HLATagInfo>();
        private List<DeliverEpcDetail> deliverEpcDetailList = null;
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
        /// 窗体加载操作线程
        /// </summary>
        private Thread threadLoad = null;
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
        private StringBuilder errorMsg = new StringBuilder();
        private List<TagDetailInfo> tagDetailList = new List<TagDetailInfo>();
        /// <summary>
        /// 当前错误记录
        /// </summary>
        private List<PKDeliverErrorBox> currentDeliverErrorBox = new List<PKDeliverErrorBox>();
        /// <summary>
        /// 当前发运箱与门店对应关系
        /// </summary>
        private List<BoxPickTaskMapInfo> currentBoxPickTaskMapInfoList = new List<BoxPickTaskMapInfo>();
        /// <summary>
        /// 当前箱对应下架单明细
        /// </summary>
        private List<InventoryOutLogDetailInfo> currentOutLogList = new List<InventoryOutLogDetailInfo>();
        /// <summary>
        /// 历史发运错误箱信息列表
        /// </summary>
        List<PKDeliverErrorBox> historyDeliverErrorBoxList = new List<PKDeliverErrorBox>();
        /// <summary>
        /// 历史发运箱信息列表
        /// </summary>
        List<PKDeliverBox> historyDeliverBoxList = new List<PKDeliverBox>();
        /// <summary>
        /// 当前箱Guid
        /// </summary>
        private string currentBoxGuid = Guid.NewGuid().ToString();
        /// <summary>
        /// 默认错误箱行颜色
        /// </summary>
        private Color rowColor = Color.White;
        private List<ListViewTagInfo> lvtagList = new List<ListViewTagInfo>();

        private string mFYDT = "";
        private string mVsart = "";

        #region PLC
        /// <summary>
        /// PLC串口
        /// </summary>
        SerialPort port = null;
        /// <summary>
        /// PLC串口缓存
        /// </summary>
        private List<byte> comBuffer = new List<byte>(4096);
        private Queue<string> comDataQueue = new Queue<string>();
        #endregion

        #region 条码扫描模组相关属性
        /// <summary>
        /// 条码扫描模组1
        /// </summary>
        SerialPort scannerPort_1 = null;  //条码扫描枪串口
        /// <summary>
        /// 条码扫描模组2
        /// </summary>
        SerialPort scannerPort_2 = null;  //条码扫描枪串口
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
        /// 重投
        /// </summary>
        private const string CHONG_TOU = "重投";

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

        private const string XIANG_MA_AND_XIA_JIA_DAN_GUAN_LIAN_BU_CUN_ZAI = "箱号和下架单未关联";

        private const string FA_YUN_MING_XI_XIN_XI_BU_CUN_ZAI = "发运明细信息不存在";

        private const string PIN_SE_GUI_BU_FU = "捡错";

        private const string SHI_FA_SHU_LIANG_CHAO_GUO_YING_FA_SHU_LIANG = "多发";

        private const string SHI_FA_SHU_LIANG_SHAO_YU_YING_FA_SHU_LIANG = "少发";

        private const string ZHU_FU_TIAO_MA_BU_YI_ZHI = "主辅条码不一致";

        private const string HU_MULTIPARTER = "箱号对应多个门店";

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
        #endregion

        //private DateTime shipDate = DateTime.Now;

        public InventoryMainForm()
        {
            InitializeComponent();
        }

        private void InventoryMainForm_Load(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timer1.Enabled = false;

            btnGetData.Visible = SysConfig.LGNUM == "ET01" ? true : false;

            if(SysConfig.DeviceInfo.AuthList!=null)
            {
                foreach(AuthInfo ai in SysConfig.DeviceInfo.AuthList)
                {
                    if(ai.AUTH_CODE.StartsWith("H"))
                    {
                        mFYDT = ai.AUTH_VALUE;
                        break;
                    }
                }
            }

            #region TEST CODE
            /* For Test
            gridDeliverErrorBox.Rows.Add("H001", "24568974", "1000004879", "HNZAD3A187A", "R7C", "165/84A(46)", 50, 20, "正常");
            gridDeliverErrorBox.Rows.Add("H001", "24568974", "1000004879", "HNZAD3A187A", "R7C", "165/84A(46)", 50, 20, "正常");
            gridDeliverErrorBox.Rows.Add("H001", "24568974", "1000004879", "HNZAD3A187A", "R7C", "165/84A(46)", 50, 20, "正常");
            gridDeliverErrorBox.Rows.Add("H001", "24568974", "1000004879", "HNZAD3A187A", "R7C", "165/84A(46)", 50, 20, "正常");
            gridDeliverErrorBox.Rows.Add("H001", "24568974", "1000004879", "HNZAD3A187A", "R7C", "165/84A(46)", 50, 20, "正常");
            gridDeliverDetail.Rows.Add("H001", "HNZAD3A187A", "R7C", "165/84A(46)", 50, 20, 30);
            gridDeliverDetail.Rows.Add("H001", "HNZAD3A187A", "R7C", "170/88A(48)", 50, 20, 30);
            gridDeliverDetail.Rows.Add("H001", "HNZAD3A187A", "R7C", "175/92A(50)", 50, 20, 30);

            PKDeliverErrorBox pkeb = new PKDeliverErrorBox();
            pkeb.PARTNER = "H001";
            pkeb.HU = "22225548";
            pkeb.ZSATNR = "HNZAD3A187A";
            pkeb.ZCOLSN = "R7C";
            pkeb.ZSIZTX = "165/84A(46)";
            pkeb.DIFF = 10;
            pkeb.REMARK = "窜规格;箱码不一致;门店不存在";
            AddRecordToDeliverErrorBoxGrid(pkeb);
            pkeb = new PKDeliverErrorBox();
            pkeb.PARTNER = "H001";
            pkeb.HU = "22225548";
            pkeb.ZSATNR = "HNZAD3A187A";
            pkeb.ZCOLSN = "R7C";
            pkeb.ZSIZTX = "170/88A(48)";
            pkeb.DIFF = 5;
            pkeb.REMARK = "窜规格;箱码不一致;门店不存在";
            AddRecordToDeliverErrorBoxGrid(pkeb);
            pkeb = new PKDeliverErrorBox();
            pkeb.PARTNER = "H001";
            pkeb.HU = "22225548";
            pkeb.ZSATNR = "HNZAD3A187A";
            pkeb.ZCOLSN = "R7C";
            pkeb.ZSIZTX = "175/92A(50)";
            pkeb.DIFF = 10;
            pkeb.REMARK = "窜规格;箱码不一致;门店不存在";
            AddRecordToDeliverErrorBoxGrid(pkeb);
             */
            #endregion
            UpdateUIControl(InventoryControlType.SHIP_DATE_LABEL, "");
            UpdateUIControl(InventoryControlType.DEVICE_NO_LABEL, SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.EQUIP_HLA : "");
            UpdateUIControl(InventoryControlType.LOGIN_NO_LABEL, SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "");
            UpdateUIControl(InventoryControlType.STATUS_LABEL, "停止");
            UpdateUIControl(InventoryControlType.HU_LABEL, "");
            UpdateUIControl(InventoryControlType.LOUCENG_LABEL, SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.LOUCENG : "");
            UpdateUIControl(InventoryControlType.PLC_STATUS_LABEL, "正在连接");
            UpdateUIControl(InventoryControlType.READER_STATUS_LABEL, "正在连接");
            UpdateUIControl(InventoryControlType.RESULT_MESSAGE_LABEL, "");

            
            threadLoad = new Thread(new ThreadStart(() =>
            {
                bool closed = false;

                Invoke(new Action(() =>
                {
                    lblLoading.Text = "正在连接PLC...";
                }));
                initPlcPort();
                Invoke(new Action(() =>
                {
                    lblLoading.Text = "正在连接条码扫描枪...";
                }));
                initScannerPort();

                Invoke(new Action(() =>
                {
                    lblLoading.Text = "正在连接读写器...";
                }));
                #region 连接读写器
                reader.OnTagReported += Reader_OnTagReported;
                bool result = reader.Connect(SysConfig.ReaderIp, Xindeco.Device.Model.ConnectType.TCP, SynchronizationContext.Current);

                if (!result)
                {
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show("连接读写器失败!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LocalDataService.InsertErrorDataRecord(ERRORDATATYPE.读写器未连接, "设备号：" + lblDeviceNo.Text + " 楼层号：" + lblLOUCENG.Text);

                    }));
                    UpdateUIControl(InventoryControlType.READER_STATUS_LABEL, "异常");

                }
                else
                {
                    UpdateUIControl(InventoryControlType.READER_STATUS_LABEL, "正常");
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
                this.Invoke(new Action(() => {
                    panelLoading.Show();
                    lblLoading.Text = "正在下载物料和吊牌数据...";
                }));

                lblLoading.Text = "正在更新SAP最新物料数据...";
                materialList = SAPDataService.GetMaterialInfoListAll(SysConfig.LGNUM);
                if (materialList == null || materialList.Count <= 0)
                {

                    this.Invoke(new Action(() =>
                    {
                        MetroMessageBox.Show(this, "未下载到物料主数据，请检查网络情况", "提示");
                        closed = true;
                        Close();
                    }));
                }
                if (closed) return;

                tagList = LocalDataService.GetAllRfidHlaTagList();
                if (tagList == null || tagList.Count <= 0)
                {
                    this.Invoke(new Action(() =>
                    {
                        MetroMessageBox.Show(this, "未下载到吊牌主数据，请检查网络情况", "提示");
                        closed = true;
                        Close();
                    }));
                }

                if (closed) return;

                this.Invoke(new Action(() =>
                {
                    panelLoading.Show();
                    lblLoading.Text = "正在加载历史信息...";
                }));

                deliverEpcDetailList = LocalDataService.GetDeliverEpcDetailList(SysConfig.LGNUM, SysConfig.DeviceInfo.LOUCENG);
                if (deliverEpcDetailList == null)
                    deliverEpcDetailList = new List<DeliverEpcDetail>();

                //加载发运错误箱历史记录（不需要查询历史记录，去除下载该数据功能）
                if (historyDeliverErrorBoxList == null)
                    historyDeliverErrorBoxList = new List<PKDeliverErrorBox>();

                //加载发运箱历史记录
                historyDeliverBoxList = LocalDataService.GetDeliverBoxListByLOUCENGAndSHIPDATE(SysConfig.DeviceInfo.LOUCENG);
                if (historyDeliverBoxList == null)
                    historyDeliverBoxList = new List<PKDeliverBox>();
                this.Invoke(new Action(() => {
                    gridDeliverErrorBox.Rows.Clear();
                }));

                ClearDeliverBoxGrid();
                if (historyDeliverBoxList != null && historyDeliverBoxList.Count > 0)
                {
                    foreach (PKDeliverBox item in historyDeliverBoxList)
                    {
                        AddRecordToDeliverBoxGrid(item);
                    }
                }
                Invoke(new Action(() =>
                {
                    panelLoading.Hide();
                    lblLoading.Text = "";
                }));



            }));
            threadLoad.IsBackground = true;
            threadLoad.Start();

            logicThread = new Thread(new ThreadStart(LogicThreadFunc));
            logicThread.IsBackground = true;
            logicThread.Start();

            //启动上传队列
            UploadServer.GetInstance().Start();
        }

        private void Reader_OnTagReported(Xindeco.Device.Model.TagInfo taginfo)
        {
            if (!isInventory || taginfo == null || string.IsNullOrEmpty(taginfo.Epc) || epcList.Contains(taginfo.Epc))
                return;

            if (SysConfig.RssiLimit != 0)
            {
                if (taginfo.Rssi < SysConfig.RssiLimit)
                    return;
            }

            lastReadTime = DateTime.Now;
            currentNum++;
            UpdateUIControl(InventoryControlType.SCAN_NUM_LABEL, currentNum.ToString());//更新扫描总数

            epcList.Add(taginfo.Epc);

            TagDetailInfo tdi = getTagDetailInfoByEpc(taginfo.Epc);
            if (tdi != null)
            {
                tagDetailList.Add(tdi);
                if (!tdi.IsAddEpc)  //当扫描到主条码时合法数 + 1
                {
                    UpdateUIControl(InventoryControlType.RIGHT_NUM_LABEL, (int.Parse(lblRightNum.Text) + 1).ToString());
                }
                else
                {
                }
            }
            else
            {
                UpdateUIControl(InventoryControlType.ERROR_NUM_LABEL, (int.Parse(lblErrorNum.Text) + 1).ToString());
            }
        }

        private void InventoryMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UploadServer.GetInstance().CheckUndoTask())
            {
                if (MetroMessageBox.Show(this, "当前有未上传的队列数据，是否确认现在退出?\r\n【注意】退出可能导致数据丢失，请谨慎操作", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    e.Cancel = true;
            }

            timer.Enabled = false;
            timer1.Enabled = false;

            StopInventory();
            timer.Stop();
            reader.Disconnect();
            if (port != null)
            {
                port.Close();
            }
            if (scannerPort_1 != null)
                scannerPort_1.Close();
            if (scannerPort_2 != null)
                scannerPort_2.Close();
            if (threadLoad != null)
                threadLoad.Abort();
            if (logicThread != null)
                logicThread.Abort();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
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
            if (tagList == null || materialList == null)
                return null;
            List<HLATagInfo> tags = tagList.FindAll(i => i.RFID_EPC == rfidEpc || i.RFID_ADD_EPC == rfidAddEpc);
            if (tags == null || tags.Count == 0)
                return null;
            else
            {
                HLATagInfo tag = tags.First();
                MaterialInfo mater = materialList.FirstOrDefault(i => i.MATNR == tag.MATNR);
                if (mater == null)
                    return null;
                else
                {
                    TagDetailInfo item = new TagDetailInfo();
                    item.EPC = epc;
                    item.RFID_EPC = tag.RFID_EPC;
                    item.RFID_ADD_EPC = !string.IsNullOrEmpty(tag.RFID_ADD_EPC) ? tag.RFID_ADD_EPC.Trim() : "";
                    item.CHARG = tag.CHARG;
                    item.MATNR = tag.MATNR;
                    item.BARCD = tag.BARCD;
                    item.ZSATNR = mater.ZSATNR;
                    item.ZCOLSN = mater.ZCOLSN;
                    item.ZSIZTX = mater.ZSIZTX;
                    item.PXQTY = mater.PXQTY;

                    item.BRGEW = mater.BRGEW;

                    //判断是否为辅条码epc
                    if (rfidEpc == item.RFID_EPC)
                        item.IsAddEpc = false;
                    else
                        item.IsAddEpc = true;
                    return item;
                }
            }
        }
        void logBoxDetial_no_short(List<PKPickTaskInfo> pkPickTaskInfoList)
        {
            try
            {
                foreach (PKPickTaskInfo pkPickTaskInfo in pkPickTaskInfoList)
                {
                    /*
                    PKDeliverErrorBox pb = new PKDeliverErrorBox()
                    {
                        BOXGUID = currentBoxGuid,
                        PICK_TASK = pkPickTaskInfo.PICK_TASK,
                        PICK_TASK_ITEM = pkPickTaskInfo.PICK_TASK_ITEM,
                        LOUCENG = SysConfig.DeviceInfo.LOUCENG,
                        PARTNER = "007",
                        SHIP_DATE = pkPickTaskInfo.SHIP_DATE,
                        HU = lblHU.Text,
                        MATNR = pkPickTaskInfo.MATNR,
                        ZSATNR = pkPickTaskInfo.ZSATNR,
                        ZCOLSN = pkPickTaskInfo.ZCOLSN,
                        ZSIZTX = pkPickTaskInfo.ZSIZTX,
                        QUAN = pkPickTaskInfo.QTY,
                        SHORTQTY = 0,
                        REAL = pkPickTaskInfo.Current_QTY,
                        ADD_REAL = pkPickTaskInfo.ADD_Current_QTY,
                        DIFF = pkPickTaskInfo.REAL_QTY + pkPickTaskInfo.Current_QTY - pkPickTaskInfo.QTY,
                        IsError = true,
                        REMARK = (pkPickTaskInfo.REAL_QTY + pkPickTaskInfo.Current_QTY - pkPickTaskInfo.QTY > 0 ? SHI_FA_SHU_LIANG_CHAO_GUO_YING_FA_SHU_LIANG : SHI_FA_SHU_LIANG_SHAO_YU_YING_FA_SHU_LIANG) + ";",
                        RecordType = pkPickTaskInfo.REAL_QTY + pkPickTaskInfo.Current_QTY - pkPickTaskInfo.QTY > 0 ? DeliverRecordType.多拣 : DeliverRecordType.少拣
                    };
                    */
                    string msg = JsonConvert.SerializeObject(pkPickTaskInfo);

                    LogHelper.Error("", msg);
                }
            }
            catch (Exception e)
            { }
        }
        private CheckResultInfo CheckData()
        {
            CheckResultInfo checkResult = new CheckResultInfo() { Result = true, IsReCheck = false };

            if (string.IsNullOrEmpty(lblHU.Text.Trim()))
            {
                SetInventoryResult(0, WEI_SAO_DAO_XIANG_MA);
                checkResult.Result = false;

                LocalDataService.InsertErrorDataRecord(ERRORDATATYPE.未扫描到箱号, "设备号：" + lblDeviceNo.Text + " 楼层号：" + lblLOUCENG.Text);

            }
            else
            {
                LocalDataService.InsertErrorDataRecord(ERRORDATATYPE.正常, "设备号：" + lblDeviceNo.Text + " 楼层号：" + lblLOUCENG.Text);

            }

            if (currentOutLogList != null && currentOutLogList.Count > 0)
            {
                foreach (InventoryOutLogDetailInfo info in currentOutLogList)
                {
                    if(info.ZXJD_TYPE == "5")
                    {
                        SetInventoryResult(0, "无法出库类型为5的下架单");
                        checkResult.Result = false;
                        break;
                    }
                }
            }

            if (boxNoList.Count > 0)
            {
                boxNoList.Clear();
                SetInventoryResult(0, XIANG_MA_BU_YI_ZHI);
                checkResult.Result = false;
            }

            if(currentBoxPickTaskMapInfoList == null || currentBoxPickTaskMapInfoList.Count == 0)
            {
                SetInventoryResult(0, XIANG_MA_AND_XIA_JIA_DAN_GUAN_LIAN_BU_CUN_ZAI);
                checkResult.Result = false;
            }

            if (epcList.Count == 0)
            {
                SetInventoryResult(0, WEI_SAO_DAO_EPC);
                checkResult.Result = false;
            }

            List<string> huParters = LocalDataService.GetParterByHu(lblHU.Text.Trim());
            if (huParters.Distinct().Count() > 1)
            {
                SetInventoryResult(0, HU_MULTIPARTER);
                checkResult.Result = false;
            }

            if (lblErrorNum.Text.Trim() != "0")
            {
                //SetInventoryResult(0, EPC_WEI_ZHU_CE);
                currentDeliverErrorBox.Add(new PKDeliverErrorBox()
                {
                    BOXGUID = currentBoxGuid,
                    PICK_TASK = "",
                    PICK_TASK_ITEM = "",
                    LOUCENG = SysConfig.DeviceInfo.LOUCENG,
                    PARTNER = "",
                    SHIP_DATE = DateTime.Now.Date,
                    HU = lblHU.Text,
                    MATNR = "",
                    ZSATNR = "",
                    ZCOLSN = "",
                    ZSIZTX = "",
                    QUAN = 0,
                    REAL = int.Parse(lblErrorNum.Text),
                    ADD_REAL = 0,
                    DIFF = int.Parse(lblErrorNum.Text),
                    SHORTQTY = 0,
                    IsError = true,
                    REMARK = EPC_WEI_ZHU_CE,
                    RecordType = DeliverRecordType.其他
                });
                lvtagList.Add(new ListViewTagInfo("", "", "", "", "", int.Parse(lblErrorNum.Text)));
                checkResult.Result = false;
            }
            if (tagDetailList != null)
            {
                tagDetailList.ForEach(new Action<TagDetailInfo>((tag) => {
                    if (!lvtagList.Exists(i => i.MATNR == tag.MATNR))
                        lvtagList.Add(new ListViewTagInfo(
                            tag.MATNR, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, tag.CHARG,
                            tagDetailList.FindAll(x => x.MATNR == tag.MATNR && !x.IsAddEpc).Count));
                }));
            }
            //if(mainCount!=addCount)
            //{
            //    SetInventoryResult(0, ZHU_FU_TIAO_MA_BU_YI_ZHI);
            //    checkResult.Result = false;
            //}
            //检查是否是短拣的箱子，如果是，则走短拣流程
            bool isShort = (currentBoxPickTaskMapInfoList != null && currentBoxPickTaskMapInfoList.Count > 0 && currentBoxPickTaskMapInfoList[0].IS_SHORT_PICK )? true : false;

            if(isShort)
            {
                //短拣流程
                List<PKDeliverBoxShortPickDetailInfo> shortDetail = LocalDataService.GetShortPickDetailList(SysConfig.LGNUM, lblHU.Text);
                string partner = currentBoxPickTaskMapInfoList != null && currentBoxPickTaskMapInfoList.Count > 0 ? currentBoxPickTaskMapInfoList[0].PARTNER : "";
                
                //start edit by wuxw 判断数据是否与短拣数据完全一致
                if(shortDetail == null)
                {
                    shortDetail = new List<PKDeliverBoxShortPickDetailInfo>();
                }
                if(tagDetailList != null && tagDetailList.Count > 0)
                { 
                    foreach(TagDetailInfo detail in tagDetailList)
                    {
                        if (!detail.IsAddEpc)
                        {
                            //获取所属产品编码实发数量小于应发数量的短拣记录
                            var item = shortDetail.FirstOrDefault(o => o.MATNR == detail.MATNR && o.REAL_QTY < o.QTY);
                            if (item != null)
                            {
                                item.REAL_QTY++;
                                if (string.IsNullOrEmpty(item.ZCOLSN))
                                {
                                    item.ZCOLSN = detail.ZCOLSN;
                                    item.ZSATNR = detail.ZSATNR;
                                    item.ZSIZTX = detail.ZSIZTX;
                                }
                                if (!item.HAS_ADD_TAG.HasValue)
                                    item.HAS_ADD_TAG = string.IsNullOrEmpty(detail.RFID_ADD_EPC) ? false : true;
                            }
                            else
                            {
                                //获取所属产品编码的短拣记录
                                var tItem = shortDetail.LastOrDefault(o => o.MATNR == detail.MATNR);
                                if (tItem != null)
                                {
                                    tItem.REAL_QTY++;
                                    if (string.IsNullOrEmpty(tItem.ZCOLSN))
                                    {
                                        tItem.ZCOLSN = detail.ZCOLSN;
                                        tItem.ZSATNR = detail.ZSATNR;
                                        tItem.ZSIZTX = detail.ZSIZTX;
                                    }
                                    if (!tItem.HAS_ADD_TAG.HasValue)
                                        tItem.HAS_ADD_TAG = string.IsNullOrEmpty(detail.RFID_ADD_EPC) ? false : true;
                                }
                                else
                                {
                                    PKDeliverBoxShortPickDetailInfo shortInfo = new PKDeliverBoxShortPickDetailInfo();
                                    shortInfo.HU = lblHU.Text;
                                    shortInfo.LGNUM = SysConfig.LGNUM;
                                    shortInfo.PICK_TASK = "";
                                    shortInfo.PICK_TASK_ITEM = "";
                                    shortInfo.MATNR = detail.MATNR;
                                    shortInfo.ZCOLSN = detail.ZCOLSN;
                                    shortInfo.ZSATNR = detail.ZSATNR;
                                    shortInfo.ZSIZTX = detail.ZSIZTX;
                                    shortInfo.QTY = 0;
                                    shortInfo.REAL_QTY++;
                                    if (!shortInfo.HAS_ADD_TAG.HasValue)
                                        shortInfo.HAS_ADD_TAG = string.IsNullOrEmpty(detail.RFID_ADD_EPC) ? false : true;
                                    shortDetail.Add(shortInfo);
                                }
                            }
                        }
                        else
                        {
                            //获取所属产品编码实发数量小于应发数量的短拣记录
                            var item = shortDetail.FirstOrDefault(o => o.MATNR == detail.MATNR && o.ADD_REAL_QTY < o.QTY);
                            if (item != null)
                            {
                                item.ADD_REAL_QTY++;
                                if (string.IsNullOrEmpty(item.ZCOLSN))
                                {
                                    item.ZCOLSN = detail.ZCOLSN;
                                    item.ZSATNR = detail.ZSATNR;
                                    item.ZSIZTX = detail.ZSIZTX;
                                }
                                if (!item.HAS_ADD_TAG.HasValue)
                                    item.HAS_ADD_TAG = string.IsNullOrEmpty(detail.RFID_ADD_EPC) ? false : true;
                            }
                            else
                            {
                                //获取所属产品编码的短拣记录
                                var tItem = shortDetail.LastOrDefault(o => o.MATNR == detail.MATNR);
                                if (tItem != null)
                                {
                                    tItem.ADD_REAL_QTY++;
                                    if (string.IsNullOrEmpty(tItem.ZCOLSN))
                                    {
                                        tItem.ZCOLSN = detail.ZCOLSN;
                                        tItem.ZSATNR = detail.ZSATNR;
                                        tItem.ZSIZTX = detail.ZSIZTX;
                                    }
                                    if (!tItem.HAS_ADD_TAG.HasValue)
                                        tItem.HAS_ADD_TAG = string.IsNullOrEmpty(detail.RFID_ADD_EPC) ? false : true;
                                }
                                else
                                {
                                    PKDeliverBoxShortPickDetailInfo shortInfo = new PKDeliverBoxShortPickDetailInfo();
                                    shortInfo.HU = lblHU.Text;
                                    shortInfo.LGNUM = SysConfig.LGNUM;
                                    shortInfo.PICK_TASK = "";
                                    shortInfo.PICK_TASK_ITEM = "";
                                    shortInfo.MATNR = detail.MATNR;
                                    shortInfo.ZCOLSN = detail.ZCOLSN;
                                    shortInfo.ZSATNR = detail.ZSATNR;
                                    shortInfo.ZSIZTX = detail.ZSIZTX;
                                    shortInfo.QTY = 0;
                                    shortInfo.ADD_REAL_QTY++;
                                    if (!shortInfo.HAS_ADD_TAG.HasValue)
                                        shortInfo.HAS_ADD_TAG = string.IsNullOrEmpty(detail.RFID_ADD_EPC) ? false : true;
                                    shortDetail.Add(shortInfo);
                                }
                            }
                        }

                    }
                }

                if(shortDetail != null && shortDetail.Count >0)
                {
                    foreach(PKDeliverBoxShortPickDetailInfo shortDetailItem in shortDetail)
                    {
                        if (string.IsNullOrEmpty(shortDetailItem.ZCOLSN))
                        {
                            //var minfo = materialList != null ? materialList.First(o => o.MATNR == shortDetailItem.MATNR) : null;
                            MaterialInfo minfo = materialList.FirstOrDefault(i => i.MAKTX == shortDetailItem.MATNR);
                            shortDetailItem.ZCOLSN = minfo != null ? minfo.ZCOLSN : "";
                            shortDetailItem.ZSATNR = minfo != null ? minfo.ZSATNR : "";
                            shortDetailItem.ZSIZTX = minfo != null ? minfo.ZSIZTX : "";
                        }

                        string remark = shortDetailItem.HAS_ADD_TAG.HasValue && shortDetailItem.HAS_ADD_TAG.Value && shortDetailItem.REAL_QTY != shortDetailItem.ADD_REAL_QTY ? ZHU_FU_TIAO_MA_BU_YI_ZHI : "";
                        if (!string.IsNullOrEmpty(remark))
                            checkResult.Result = false;
                        var outLogDetail = currentOutLogList.FirstOrDefault(o => o.PICK_TASK == shortDetailItem.PICK_TASK && o.PICK_TASK_ITEM == shortDetailItem.PICK_TASK_ITEM && o.PRODUCTNO == shortDetailItem.MATNR);
                        if (outLogDetail != null)
                        {
                            if (shortDetailItem.QTY != shortDetailItem.REAL_QTY)
                            {
                                //多拣、少拣
                                //SetInventoryResult(0, countCurrent - countShort > 0 ? SHI_FA_SHU_LIANG_CHAO_GUO_YING_FA_SHU_LIANG : SHI_FA_SHU_LIANG_SHAO_YU_YING_FA_SHU_LIANG);
                                checkResult.Result = false;
                                currentDeliverErrorBox.Add(new PKDeliverErrorBox()
                                {
                                    BOXGUID = currentBoxGuid,
                                    PICK_TASK = shortDetailItem.PICK_TASK,
                                    PICK_TASK_ITEM = shortDetailItem.PICK_TASK_ITEM,
                                    LOUCENG = SysConfig.DeviceInfo.LOUCENG,
                                    PARTNER = partner,
                                    SHIP_DATE = outLogDetail.SHIP_DATE,
                                    HU = lblHU.Text,
                                    MATNR = shortDetailItem.MATNR,
                                    ZSATNR = shortDetailItem.ZSATNR,
                                    ZCOLSN = shortDetailItem.ZCOLSN,
                                    ZSIZTX = shortDetailItem.ZSIZTX,
                                    QUAN = outLogDetail.QTY,
                                    REAL = shortDetailItem.REAL_QTY,
                                    ADD_REAL = shortDetailItem.ADD_REAL_QTY,
                                    DIFF = shortDetailItem.REAL_QTY - shortDetailItem.QTY,
                                    SHORTQTY = shortDetailItem.DJQTY,
                                    IsError = true,
                                    REMARK = (shortDetailItem.REAL_QTY - shortDetailItem.QTY > 0 ? SHI_FA_SHU_LIANG_CHAO_GUO_YING_FA_SHU_LIANG : SHI_FA_SHU_LIANG_SHAO_YU_YING_FA_SHU_LIANG) + ";" + remark,
                                    RecordType = shortDetailItem.REAL_QTY - shortDetailItem.QTY > 0 ? DeliverRecordType.多拣 : DeliverRecordType.少拣
                                });
                            }
                            else
                            {
                                //正常
                                currentDeliverErrorBox.Add(new PKDeliverErrorBox()
                                {
                                    BOXGUID = currentBoxGuid,
                                    PICK_TASK = shortDetailItem.PICK_TASK,
                                    PICK_TASK_ITEM = shortDetailItem.PICK_TASK_ITEM,
                                    LOUCENG = SysConfig.DeviceInfo.LOUCENG,
                                    PARTNER = partner,
                                    SHIP_DATE = outLogDetail.SHIP_DATE,
                                    HU = lblHU.Text,
                                    MATNR = shortDetailItem.MATNR,
                                    ZSATNR = shortDetailItem.ZSATNR,
                                    ZCOLSN = shortDetailItem.ZCOLSN,
                                    ZSIZTX = shortDetailItem.ZSIZTX,
                                    QUAN = outLogDetail.QTY,
                                    REAL = shortDetailItem.REAL_QTY,
                                    ADD_REAL = shortDetailItem.ADD_REAL_QTY,
                                    SHORTQTY = shortDetailItem.DJQTY,
                                    DIFF = 0,
                                    IsError = string.IsNullOrEmpty(remark) ? false : true,
                                    REMARK = string.IsNullOrEmpty(remark) ? "正常" : remark,
                                    RecordType = DeliverRecordType.正常
                                });
                            }
                        }
                        else
                        {
                            //下架单中不存在指定sku数据
                            //SetInventoryResult(0, PIN_SE_GUI_BU_FU);
                            checkResult.Result = false;

                            currentDeliverErrorBox.Add(new PKDeliverErrorBox()
                            {
                                BOXGUID = currentBoxGuid,
                                PICK_TASK = "",
                                PICK_TASK_ITEM = "",
                                LOUCENG = SysConfig.DeviceInfo.LOUCENG,
                                PARTNER = partner,
                                SHIP_DATE = DateTime.Now.Date,
                                HU = lblHU.Text,
                                MATNR = shortDetailItem.MATNR,
                                ZSATNR = shortDetailItem.ZSATNR,
                                ZCOLSN = shortDetailItem.ZCOLSN,
                                ZSIZTX = shortDetailItem.ZSIZTX,
                                QUAN = 0,
                                REAL = shortDetailItem.REAL_QTY,
                                ADD_REAL = shortDetailItem.ADD_REAL_QTY,
                                DIFF = shortDetailItem.REAL_QTY,
                                SHORTQTY = 0,
                                REMARK = PIN_SE_GUI_BU_FU + ";" + remark,
                                IsError = true,
                                RecordType = DeliverRecordType.拣错
                            });
                        }
                    }
                }
                //end edit by wuxw
            }
            else
            {
                //判断是否重投
                bool isHuExists = historyDeliverBoxList == null ? false : historyDeliverBoxList.Exists(o => o.HU == lblHU.Text.Trim() && o.RESULT.ToUpper() == "S");
                if (isHuExists)
                {
                    List<DeliverEpcDetail> epcListBefore = deliverEpcDetailList.Where(o => o.HU == lblHU.Text.Trim() && o.Result == "S").ToList();
                    //是否完全匹配
                    bool isSame = true;
                    //是否完全不匹配
                    bool isAllNotSame = true;
                    if (epcListBefore != null && epcListBefore.Count > 0)
                    {
                        if (epcList.Count == epcListBefore.Count)
                        {
                            foreach (DeliverEpcDetail epc in epcListBefore)
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
                            foreach (DeliverEpcDetail epc in epcListBefore)
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
                        checkResult.Result = true;
                        checkResult.IsReCheck = true;
                        SetInventoryResult(0, CHONG_TOU); //重投
                    }
                    else if (isAllNotSame)
                    {
                        //数据完全不一致
                        checkResult.Result = false;
                        checkResult.IsReCheck = false;
                        SetInventoryResult(0, XIANG_MA_CHONG_FU_SHI_YONG);  //箱码重复使用
                    }
                    else
                    {
                        //数据部分一致
                        checkResult.Result = false;
                        checkResult.IsReCheck = false;
                        SetInventoryResult(0, EPC_YI_SAO_MIAO);  //商品已扫描
                    }
                }

                string partner = currentBoxPickTaskMapInfoList != null && currentBoxPickTaskMapInfoList.Count > 0 ? currentBoxPickTaskMapInfoList[0].PARTNER : "";

                //如果重投不再重新计算品色规数量;如果不是重投才计算品色规
                if (!checkResult.IsReCheck)
                {
                    //begin edit by wuxw
                    List<PKPickTaskInfo> pkPickTaskInfoList = new List<PKPickTaskInfo>();
                    if (currentOutLogList != null && currentOutLogList.Count > 0)
                    {
                        foreach(InventoryOutLogDetailInfo outLogDetail in currentOutLogList)
                        {
                            PKPickTaskInfo pkPickTaskInfo = new PKPickTaskInfo();
                            pkPickTaskInfo.HU = lblHU.Text;
                            pkPickTaskInfo.PICK_TASK = outLogDetail.PICK_TASK;
                            pkPickTaskInfo.PICK_TASK_ITEM = outLogDetail.PICK_TASK_ITEM;
                            pkPickTaskInfo.SHIP_DATE = outLogDetail.SHIP_DATE;
                            pkPickTaskInfo.MATNR = outLogDetail.PRODUCTNO;
                            pkPickTaskInfo.QTY = outLogDetail.QTY;
                            pkPickTaskInfo.REAL_QTY = outLogDetail.REALQTY;
                            pkPickTaskInfo.ADD_REAL_QTY = outLogDetail.REALQTY_ADD;
                            //如果列表中存在此下架单号，则直接使用列表中的是否尾箱判断
                            //var temp = pkPickTaskInfoList.FirstOrDefault(o => o.PICK_TASK == outLogDetail.PICK_TASK);
                            //if (temp == null)
                            //    pkPickTaskInfo.ISLASTBOX = LocalDataService.CountUnScanDeliverBox(outLogDetail.PICK_TASK) == 1 ? true : false;
                            //else
                            //    pkPickTaskInfo.ISLASTBOX = temp.ISLASTBOX;


                            //pkPickTaskInfo.ISLASTBOX = outLogDetail.UnScanBoxCount == 1 ? true : false;

                            pkPickTaskInfo.ISLASTBOX = LocalDataService.CountUnScanDeliverBox(outLogDetail.PICK_TASK) == 1 ? true : false;

                            pkPickTaskInfoList.Add(pkPickTaskInfo);
                        }
                    }
                    pkPickTaskInfoList = pkPickTaskInfoList.OrderByDescending(o => o.ISLASTBOX).ThenBy(o => o.PICK_TASK).ThenBy(o => o.MATNR).ThenBy(o => o.QTY - o.REAL_QTY).ToList();
                    //更新下架单对应实际数量
                    if (tagDetailList != null && tagDetailList.Count > 0)
                    {
                        foreach(TagDetailInfo tagDetail in tagDetailList)
                        {
                            if(!tagDetail.IsAddEpc)
                            {
                                //先将属于尾箱的下架单填满
                                var item = pkPickTaskInfoList.FirstOrDefault(o => o.MATNR == tagDetail.MATNR && o.ISLASTBOX && o.Current_QTY < (o.QTY - o.REAL_QTY));
                                if(item != null)
                                {
                                    item.Current_QTY++;
                                    if (string.IsNullOrEmpty(item.ZCOLSN))
                                    {
                                        item.ZCOLSN = tagDetail.ZCOLSN;
                                        item.ZSATNR = tagDetail.ZSATNR;
                                        item.ZSIZTX = tagDetail.ZSIZTX;
                                    }
                                    if(!item.HAS_ADD_TAG.HasValue)
                                        item.HAS_ADD_TAG = string.IsNullOrEmpty(tagDetail.RFID_ADD_EPC) ? false : true;
                                }
                                else
                                {
                                    //将不属于尾箱的下架单填满
                                    var tItem = pkPickTaskInfoList.FirstOrDefault(o => o.MATNR == tagDetail.MATNR && o.ISLASTBOX == false && o.Current_QTY < (o.QTY - o.REAL_QTY));
                                    if (tItem != null)
                                    {
                                        tItem.Current_QTY++;
                                        if (string.IsNullOrEmpty(tItem.ZCOLSN))
                                        {
                                            tItem.ZCOLSN = tagDetail.ZCOLSN;
                                            tItem.ZSATNR = tagDetail.ZSATNR;
                                            tItem.ZSIZTX = tagDetail.ZSIZTX;
                                        }
                                        if (!tItem.HAS_ADD_TAG.HasValue)
                                            tItem.HAS_ADD_TAG = string.IsNullOrEmpty(tagDetail.RFID_ADD_EPC) ? false : true;
                                    }
                                    else
                                    {
                                        //将剩下的填充到最后一箱
                                        var lastItem = pkPickTaskInfoList.LastOrDefault(o => o.MATNR == tagDetail.MATNR);
                                        if (lastItem != null)
                                        {
                                            lastItem.Current_QTY++;
                                            if (string.IsNullOrEmpty(lastItem.ZCOLSN))
                                            {
                                                lastItem.ZCOLSN = tagDetail.ZCOLSN;
                                                lastItem.ZSATNR = tagDetail.ZSATNR;
                                                lastItem.ZSIZTX = tagDetail.ZSIZTX;
                                            }
                                            if (!lastItem.HAS_ADD_TAG.HasValue)
                                                lastItem.HAS_ADD_TAG = string.IsNullOrEmpty(tagDetail.RFID_ADD_EPC) ? false : true;
                                        }
                                        else
                                        {
                                            //找不到任何数据，新增一条记录
                                            PKPickTaskInfo pkPickTaskInfo = new PKPickTaskInfo();
                                            pkPickTaskInfo.HU = lblHU.Text;
                                            pkPickTaskInfo.PICK_TASK = "";
                                            pkPickTaskInfo.SHIP_DATE = DateTime.Now.Date;
                                            pkPickTaskInfo.MATNR = tagDetail.MATNR;
                                            pkPickTaskInfo.ZCOLSN = tagDetail.ZCOLSN;
                                            pkPickTaskInfo.ZSATNR = tagDetail.ZSATNR;
                                            pkPickTaskInfo.ZSIZTX = tagDetail.ZSIZTX;
                                            pkPickTaskInfo.QTY = 0;
                                            pkPickTaskInfo.Current_QTY++;
                                            pkPickTaskInfo.ISLASTBOX =  false;
                                            if (!pkPickTaskInfo.HAS_ADD_TAG.HasValue)
                                                pkPickTaskInfo.HAS_ADD_TAG = string.IsNullOrEmpty(tagDetail.RFID_ADD_EPC) ? false : true;
                                            pkPickTaskInfoList.Add(pkPickTaskInfo);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //先将属于尾箱的下架单填满
                                var item = pkPickTaskInfoList.FirstOrDefault(o => o.MATNR == tagDetail.MATNR && o.ISLASTBOX && o.ADD_Current_QTY < (o.QTY - o.ADD_REAL_QTY));
                                if (item != null)
                                {
                                    item.ADD_Current_QTY++;
                                    if (string.IsNullOrEmpty(item.ZCOLSN))
                                    {
                                        item.ZCOLSN = tagDetail.ZCOLSN;
                                        item.ZSATNR = tagDetail.ZSATNR;
                                        item.ZSIZTX = tagDetail.ZSIZTX;
                                    }
                                    if (!item.HAS_ADD_TAG.HasValue)
                                        item.HAS_ADD_TAG = string.IsNullOrEmpty(tagDetail.RFID_ADD_EPC) ? false : true;
                                }
                                else
                                {
                                    //将不属于尾箱的下架单填满
                                    var tItem = pkPickTaskInfoList.FirstOrDefault(o => o.MATNR == tagDetail.MATNR && o.ISLASTBOX == false && o.ADD_Current_QTY < (o.QTY - o.ADD_REAL_QTY));
                                    if (tItem != null)
                                    {
                                        tItem.ADD_Current_QTY++;
                                        if (string.IsNullOrEmpty(tItem.ZCOLSN))
                                        {
                                            tItem.ZCOLSN = tagDetail.ZCOLSN;
                                            tItem.ZSATNR = tagDetail.ZSATNR;
                                            tItem.ZSIZTX = tagDetail.ZSIZTX;
                                        }
                                        if (!tItem.HAS_ADD_TAG.HasValue)
                                            tItem.HAS_ADD_TAG = string.IsNullOrEmpty(tagDetail.RFID_ADD_EPC) ? false : true;
                                    }
                                    else
                                    {
                                        //将剩下的填充到最后一箱
                                        var lastItem = pkPickTaskInfoList.LastOrDefault(o => o.MATNR == tagDetail.MATNR);
                                        if (lastItem != null)
                                        {
                                            lastItem.ADD_Current_QTY++;
                                            if (string.IsNullOrEmpty(lastItem.ZCOLSN))
                                            {
                                                lastItem.ZCOLSN = tagDetail.ZCOLSN;
                                                lastItem.ZSATNR = tagDetail.ZSATNR;
                                                lastItem.ZSIZTX = tagDetail.ZSIZTX;
                                            }
                                            if (!lastItem.HAS_ADD_TAG.HasValue)
                                                lastItem.HAS_ADD_TAG = string.IsNullOrEmpty(tagDetail.RFID_ADD_EPC) ? false : true;
                                        }
                                        else
                                        {
                                            //找不到任何数据，新增一条记录
                                            PKPickTaskInfo pkPickTaskInfo = new PKPickTaskInfo();
                                            pkPickTaskInfo.HU = lblHU.Text;
                                            pkPickTaskInfo.PICK_TASK = "";
                                            pkPickTaskInfo.SHIP_DATE = DateTime.Now.Date;
                                            pkPickTaskInfo.MATNR = tagDetail.MATNR;
                                            pkPickTaskInfo.ZCOLSN = tagDetail.ZCOLSN;
                                            pkPickTaskInfo.ZSATNR = tagDetail.ZSATNR;
                                            pkPickTaskInfo.ZSIZTX = tagDetail.ZSIZTX;
                                            pkPickTaskInfo.QTY = 0;
                                            pkPickTaskInfo.ADD_Current_QTY++;
                                            pkPickTaskInfo.ISLASTBOX = false;
                                            if (!pkPickTaskInfo.HAS_ADD_TAG.HasValue)
                                                pkPickTaskInfo.HAS_ADD_TAG = string.IsNullOrEmpty(tagDetail.RFID_ADD_EPC) ? false : true;
                                            pkPickTaskInfoList.Add(pkPickTaskInfo);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //判断数据是否有问题
                    if(pkPickTaskInfoList != null && pkPickTaskInfoList.Count > 0)
                    {
                        //logBoxDetial_no_short(pkPickTaskInfoList);

                        foreach(PKPickTaskInfo pkPickTaskInfo in pkPickTaskInfoList)
                        {
                            if (string.IsNullOrEmpty(pkPickTaskInfo.ZCOLSN))
                            {
                                //var minfo = materialList != null ? materialList.First(o => o.MATNR == pkPickTaskInfo.MATNR) : null;
                                MaterialInfo minfo = materialList.FirstOrDefault(i => i.MATNR == pkPickTaskInfo.MATNR);
                                pkPickTaskInfo.ZCOLSN = minfo != null ? minfo.ZCOLSN : "";
                                pkPickTaskInfo.ZSATNR = minfo != null ? minfo.ZSATNR : "";
                                pkPickTaskInfo.ZSIZTX = minfo != null ? minfo.ZSIZTX : "";
                            }

                            string remark = pkPickTaskInfo.HAS_ADD_TAG.HasValue && pkPickTaskInfo.HAS_ADD_TAG.Value && pkPickTaskInfo.ADD_Current_QTY != pkPickTaskInfo.Current_QTY ? ZHU_FU_TIAO_MA_BU_YI_ZHI : "";
                            if (!string.IsNullOrEmpty(remark))
                                checkResult.Result = false;

                            if (!string.IsNullOrEmpty(pkPickTaskInfo.PICK_TASK))
                            {
                                if(pkPickTaskInfo.ISLASTBOX)
                                {
                                    //如果属于尾箱，则实发总量和应发总量必须相等
                                    if (pkPickTaskInfo.REAL_QTY + pkPickTaskInfo.Current_QTY != pkPickTaskInfo.QTY)
                                    {
                                        //SetInventoryResult(0, outLogDetail.REALQTY + count - outLogDetail.QTY > 0 ? SHI_FA_SHU_LIANG_CHAO_GUO_YING_FA_SHU_LIANG : SHI_FA_SHU_LIANG_SHAO_YU_YING_FA_SHU_LIANG);
                                        checkResult.Result = false;
                                        currentDeliverErrorBox.Add(new PKDeliverErrorBox()
                                        {
                                            BOXGUID = currentBoxGuid,
                                            PICK_TASK = pkPickTaskInfo.PICK_TASK,
                                            PICK_TASK_ITEM = pkPickTaskInfo.PICK_TASK_ITEM,
                                            LOUCENG = SysConfig.DeviceInfo.LOUCENG,
                                            PARTNER = partner,
                                            SHIP_DATE = pkPickTaskInfo.SHIP_DATE,
                                            HU = lblHU.Text,
                                            MATNR = pkPickTaskInfo.MATNR,
                                            ZSATNR = pkPickTaskInfo.ZSATNR,
                                            ZCOLSN = pkPickTaskInfo.ZCOLSN,
                                            ZSIZTX = pkPickTaskInfo.ZSIZTX,
                                            QUAN = pkPickTaskInfo.QTY,
                                            SHORTQTY = 0,
                                            REAL = pkPickTaskInfo.Current_QTY,
                                            ADD_REAL = pkPickTaskInfo.ADD_Current_QTY,
                                            DIFF = pkPickTaskInfo.REAL_QTY + pkPickTaskInfo.Current_QTY - pkPickTaskInfo.QTY,
                                            IsError = true,
                                            REMARK = (pkPickTaskInfo.REAL_QTY + pkPickTaskInfo.Current_QTY - pkPickTaskInfo.QTY > 0 ? SHI_FA_SHU_LIANG_CHAO_GUO_YING_FA_SHU_LIANG : SHI_FA_SHU_LIANG_SHAO_YU_YING_FA_SHU_LIANG) + ";" + remark,
                                            RecordType = pkPickTaskInfo.REAL_QTY + pkPickTaskInfo.Current_QTY - pkPickTaskInfo.QTY > 0 ? DeliverRecordType.多拣 : DeliverRecordType.少拣
                                        });
                                    }
                                    else //正常
                                    {
                                        //尾箱，且当前数量均不为0的才显示
                                        if (pkPickTaskInfo.Current_QTY != 0 || pkPickTaskInfo.ADD_Current_QTY != 0)
                                        {
                                            currentDeliverErrorBox.Add(new PKDeliverErrorBox()
                                            {
                                                BOXGUID = currentBoxGuid,
                                                PICK_TASK = pkPickTaskInfo.PICK_TASK,
                                                PICK_TASK_ITEM = pkPickTaskInfo.PICK_TASK_ITEM,
                                                LOUCENG = SysConfig.DeviceInfo.LOUCENG,
                                                PARTNER = partner,
                                                SHIP_DATE = pkPickTaskInfo.SHIP_DATE,
                                                HU = lblHU.Text,
                                                MATNR = pkPickTaskInfo.MATNR,
                                                ZSATNR = pkPickTaskInfo.ZSATNR,
                                                ZCOLSN = pkPickTaskInfo.ZCOLSN,
                                                ZSIZTX = pkPickTaskInfo.ZSIZTX,
                                                QUAN = pkPickTaskInfo.QTY,
                                                REAL = pkPickTaskInfo.Current_QTY,
                                                ADD_REAL = pkPickTaskInfo.ADD_Current_QTY,
                                                SHORTQTY = 0,
                                                DIFF = 0,
                                                IsError = string.IsNullOrEmpty(remark) ? false : true,
                                                REMARK = string.IsNullOrEmpty(remark) ? "正常" : remark,
                                                RecordType = DeliverRecordType.正常
                                            });
                                        }
                                    }
                                }
                                else
                                {
                                    //如果不属于尾箱，跳过所有当前数量均为0的记录，不显示
                                    if (pkPickTaskInfo.Current_QTY == 0 && pkPickTaskInfo.ADD_Current_QTY == 0)
                                        continue;

                                    //如果不属于尾箱：实发数量大于应发数量，异常
                                    if (pkPickTaskInfo.REAL_QTY + pkPickTaskInfo.Current_QTY > pkPickTaskInfo.QTY)
                                    {
                                        //SetInventoryResult(0, SHI_FA_SHU_LIANG_CHAO_GUO_YING_FA_SHU_LIANG);
                                        checkResult.Result = false;
                                        currentDeliverErrorBox.Add(new PKDeliverErrorBox()
                                        {
                                            BOXGUID = currentBoxGuid,
                                            PICK_TASK = pkPickTaskInfo.PICK_TASK,
                                            PICK_TASK_ITEM = pkPickTaskInfo.PICK_TASK_ITEM,
                                            LOUCENG = SysConfig.DeviceInfo.LOUCENG,
                                            PARTNER = partner,
                                            SHIP_DATE = pkPickTaskInfo.SHIP_DATE,
                                            HU = lblHU.Text,
                                            MATNR = pkPickTaskInfo.MATNR,
                                            ZSATNR = pkPickTaskInfo.ZSATNR,
                                            ZCOLSN = pkPickTaskInfo.ZCOLSN,
                                            ZSIZTX = pkPickTaskInfo.ZSIZTX,
                                            QUAN = pkPickTaskInfo.QTY,
                                            SHORTQTY = 0,
                                            REAL = pkPickTaskInfo.Current_QTY,
                                            ADD_REAL = pkPickTaskInfo.ADD_Current_QTY,
                                            DIFF = pkPickTaskInfo.REAL_QTY + pkPickTaskInfo.Current_QTY - pkPickTaskInfo.QTY,
                                            IsError = true,
                                            REMARK = "多拣" + ";" + remark,
                                            RecordType = DeliverRecordType.多拣
                                        });
                                    }
                                    else //正常
                                    {
                                        if (pkPickTaskInfo.Current_QTY != 0 || pkPickTaskInfo.ADD_Current_QTY != 0)
                                        {
                                            currentDeliverErrorBox.Add(new PKDeliverErrorBox()
                                            {
                                                BOXGUID = currentBoxGuid,
                                                PICK_TASK = pkPickTaskInfo.PICK_TASK,
                                                PICK_TASK_ITEM = pkPickTaskInfo.PICK_TASK_ITEM,
                                                LOUCENG = SysConfig.DeviceInfo.LOUCENG,
                                                PARTNER = partner,
                                                SHIP_DATE = pkPickTaskInfo.SHIP_DATE,
                                                HU = lblHU.Text,
                                                MATNR = pkPickTaskInfo.MATNR,
                                                ZSATNR = pkPickTaskInfo.ZSATNR,
                                                ZCOLSN = pkPickTaskInfo.ZCOLSN,
                                                ZSIZTX = pkPickTaskInfo.ZSIZTX,
                                                QUAN = pkPickTaskInfo.QTY,
                                                REAL = pkPickTaskInfo.Current_QTY,
                                                ADD_REAL = pkPickTaskInfo.ADD_Current_QTY,
                                                DIFF = 0,
                                                SHORTQTY = 0,
                                                IsError = string.IsNullOrEmpty(remark) ? false : true,
                                                REMARK = string.IsNullOrEmpty(remark) ? "正常" : remark,
                                                RecordType = DeliverRecordType.正常
                                            });
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //下架单中不存在指定sku数据
                                //SetInventoryResult(0, PIN_SE_GUI_BU_FU);
                                checkResult.Result = false;

                                currentDeliverErrorBox.Add(new PKDeliverErrorBox()
                                {
                                    BOXGUID = currentBoxGuid,
                                    //PICK_TASK = (currentOutLogList!=null && currentOutLogList.Count>0)?currentOutLogList.FirstOrDefault().PICK_TASK : "",
                                    PICK_TASK = "",
                                    PICK_TASK_ITEM = "",
                                    LOUCENG = SysConfig.DeviceInfo.LOUCENG,
                                    PARTNER = partner,
                                    SHIP_DATE = pkPickTaskInfo.SHIP_DATE,
                                    HU = lblHU.Text,
                                    MATNR = pkPickTaskInfo.MATNR,
                                    ZSATNR = pkPickTaskInfo.ZSATNR,
                                    ZCOLSN = pkPickTaskInfo.ZCOLSN,
                                    ZSIZTX = pkPickTaskInfo.ZSIZTX,
                                    QUAN = 0,
                                    REAL = pkPickTaskInfo.Current_QTY,
                                    ADD_REAL = pkPickTaskInfo.ADD_Current_QTY,
                                    DIFF = pkPickTaskInfo.Current_QTY,
                                    SHORTQTY = 0,
                                    IsError = true,
                                    REMARK = PIN_SE_GUI_BU_FU + ";" + remark,
                                    RecordType = DeliverRecordType.拣错
                                });
                            }
                        }
                    }

                    //end edit by wuxw
                }

                if (checkResult.Result == false && currentDeliverErrorBox.Count <= 0)
                {
                    currentDeliverErrorBox.Add(new PKDeliverErrorBox()
                    {
                        BOXGUID = currentBoxGuid,
                        LOUCENG = SysConfig.DeviceInfo.LOUCENG,
                        PARTNER = partner,
                        SHIP_DATE = DateTime.Now.Date,
                        HU = lblHU.Text,
                        QUAN = 0,
                        REAL = long.Parse(lblScanNum.Text),
                        ADD_REAL = 0,
                        DIFF = long.Parse(lblScanNum.Text),
                        REMARK = "其他错误",
                        SHORTQTY = 0,
                        IsError = true,
                        RecordType = DeliverRecordType.其他
                    });
                }
            }
            return checkResult;
        }

        private bool IsCurrentBoxShort()
        {
            if (currentBoxPickTaskMapInfoList == null || currentBoxPickTaskMapInfoList.Count == 0)
                return false;

            return currentBoxPickTaskMapInfoList.Find(i => i.HU == lblHU.Text.Trim()).IS_SHORT_PICK;
        }

        /// <summary>
        /// 设置盘点结果 
        /// </summary>
        /// <param name="result">1正常 3异常</param>
        /// <param name="message">错误信息</param>
        private void SetInventoryResult(int result, string message)
        {
            inventoryResult = result;
            if (!string.IsNullOrEmpty(message))
            {
                if(!errorMsg.ToString().Contains(message))
                    errorMsg.AppendFormat("{0};", message);
            }
        }

        /// <summary>
        /// 开始盘点
        /// </summary>
        private bool StartInventory()
        {
            if (isInventory == false)
            {
                try
                {
                    epcList.Clear();
                    currentBoxPickTaskMapInfoList = null;
                    currentOutLogList.Clear();
                    currentDeliverErrorBox.Clear();
                    errorMsg.Clear();
                    tagDetailList.Clear();
                    inventoryResult = 0;
                    lvtagList.Clear();
                    mVsart = "";
                    this.Invoke(new Action(() => {
                        gridDeliverErrorBox.Rows.Clear();
                        if (this.btnStart.Text == "开始")
                            Start();
                    }));
                    
                    //每次开始盘点时自动生成一个新的BoxGuid
                    currentBoxGuid = Guid.NewGuid().ToString();
                    //清除当前屏幕统计数量
                    UpdateUIControl(InventoryControlType.HU_LABEL, "");
                    UpdateUIControl(InventoryControlType.SCAN_NUM_LABEL, "0");
                    UpdateUIControl(InventoryControlType.RIGHT_NUM_LABEL, "0");
                    UpdateUIControl(InventoryControlType.ERROR_NUM_LABEL, "0");
                    UpdateUIControl(InventoryControlType.STATUS_LABEL, "正在扫描");
                    UpdateUIControl(InventoryControlType.RESULT_MESSAGE_LABEL, "");
                    //Invoke(new Action(() =>
                    //{
                    //    gridDeliverDetail.Rows.Clear();
                    //}));
                    //UpdateSubFormDeliverDetailGrid(gridDeliverDetail.Rows);

                    if (boxNoList.Count > 0)
                    {
                        string boxno = boxNoList.Dequeue();

                        BoxPickTaskUnionInfo unionInfo = LocalDataService.GetBoxPickTaskUnionListByHU(boxno);

                        if(unionInfo.InventoryOutLogDetailList!=null && unionInfo.InventoryOutLogDetailList.Count>0)
                        {
                            mVsart = unionInfo.InventoryOutLogDetailList[0].VSART;
                        }

                        if(unionInfo!=null && unionInfo.BoxPickTaskMapInfoList!=null)
                        {
                            foreach(BoxPickTaskMapInfo bmi in unionInfo.BoxPickTaskMapInfoList)
                            {
                                string pick_task = bmi.PICK_TASK;
                                if(unionInfo.InventoryOutLogDetailList==null)
                                {
                                    unionInfo.InventoryOutLogDetailList = new List<InventoryOutLogDetailInfo>();
                                }
                                if(!unionInfo.InventoryOutLogDetailList.Exists(r=>r.PICK_TASK == pick_task))
                                {
                                    List<InventoryOutLogDetailInfo> re = SAPDataService.GetHLAShelvesSingleTask(SysConfig.LGNUM, pick_task);
                                    unionInfo.InventoryOutLogDetailList.AddRange(re);

                                    foreach(var v in re)
                                    {
                                        LocalDataService.SaveInventoryOutLogDetail(v);
                                    }
                                }
                            }
                        }

                        currentBoxPickTaskMapInfoList = unionInfo != null ? unionInfo.BoxPickTaskMapInfoList : null;
                        if(unionInfo != null && unionInfo.InventoryOutLogDetailList != null)
                        {
                            currentOutLogList.AddRange(unionInfo.InventoryOutLogDetailList);
                        }
                        //currentBoxPickTaskMapInfoList = LocalDataService.GetBoxPickTaskMapInfoListByHU(boxno);
                        ////通过下架单号获取下架单明细列表
                        //if (currentBoxPickTaskMapInfoList != null && currentBoxPickTaskMapInfoList.Count > 0)
                        //{
                        //    foreach (var item in currentBoxPickTaskMapInfoList)
                        //    {
                        //        var plist = LocalDataService.GetInventoryOutLogDetailByPicktask(item.PICK_TASK);
                        //        currentOutLogList.AddRange(plist);
                        //    }
                        //}

                        SetCurrentOutLogListAddQty();

                        UpdateUIControl(InventoryControlType.HU_LABEL, boxno);
                    }

                    lastReadTime = DateTime.Now;
                    currentNum = 0;
                    int i, j,k;
                    LocalDataService.GetGhostAndTrigger(out i, out j,out k);
                    reader.StartInventory(i, j,k);
                    isInventory = true;
                    
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                    SetInventoryResult(3, "");
                    return false;
                }
            }

            return true;
        }

        private void SetCurrentOutLogListAddQty()
        {
            if(currentOutLogList?.Count>0)
            {
                currentOutLogList.ForEach((i) => {
                    if (!string.IsNullOrEmpty(tagList.FirstOrDefault(j => j.MATNR == i.PRODUCTNO)?.RFID_ADD_EPC))
                        i.QTY_ADD = i.QTY;
                });
            }
        }

        public bool isWholeDeliver()
        {
            if (currentOutLogList != null && currentOutLogList.Count > 0)
            {
                foreach (InventoryOutLogDetailInfo info in currentOutLogList)
                {
                    if (info.ZXJD_TYPE == "5")
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        string getDOCNO()
        {
            string re = "";

            try
            {
                if(currentOutLogList.Count > 0)
                {
                    re = currentOutLogList.First().DOCNO;
                }
                if (re.Trim() == "")
                {
                    string xjdh = currentBoxPickTaskMapInfoList.Find(i => i.HU == lblHU.Text.Trim()).PICK_TASK;
                    List<InventoryOutLogDetailInfo> ins = LocalDataService.GetInventoryOutLogDetailByPicktask(xjdh);
                    if (ins != null && ins.Count > 0)
                    {
                        re = ins[0].DOCNO;
                    }
                }
            }
            catch(Exception)
            {

            }
            return re;
        }
        void playSound(CheckResultInfo cr)
        {
            try
            {
                if (cr.Result)
                {
                    AudioHelper.Play("success.wav");
                }
                else
                {
                    AudioHelper.Play("fail.wav");
                }
            }
            catch(Exception)
            { }
        }
        /// <summary>
        /// 停止盘点
        /// </summary>
        private bool StopInventory()
        {
            //判断是否正在盘点，正在盘点则停止盘点
            if (isInventory == true)
            {
                try
                {
                    UpdateUIControl(InventoryControlType.STATUS_LABEL, "停止扫描");
                    isInventory = false;
                    reader.StopInventory();
                    CheckResultInfo checkResult = CheckData();
                    playSound(checkResult);
                    UploadPKBoxInfo upbi = GetCurrentUploadPKBox(checkResult.Result);

                    if (checkResult.IsReCheck == false)
                    {
                        //将需要上传SAP数据插入队列当中(需要先保存到队列，再去更新界面和数据库中的发运明细的实发数量，防止数据被更新后造成上传SAP再次判断数据不一致)
                        EnqueueUploadData(upbi);

                        if (checkResult.Result)
                        {
                            //将epc加到缓存
                            foreach (string epc in epcList)
                            {
                                DeliverEpcDetail epcDetail = new DeliverEpcDetail();
                                epcDetail.LGNUM = upbi.LGNUM;
                                epcDetail.SHIP_DATE = upbi.SHIP_DATE;
                                epcDetail.LOUCENG = upbi.LOUCENG;
                                epcDetail.PARTNER = upbi.PARTNER;
                                epcDetail.HU = upbi.HU;
                                epcDetail.EPC_SER = epc;
                                epcDetail.Result = upbi.InventoryResult ? "S" : "E";
                                epcDetail.BOXGUID = upbi.Guid;
                                deliverEpcDetailList.Add(epcDetail);
                            }
                        }

                        //将epc明细上传至我司数据库
                        LocalDataService.SaveDeliverEpcDetail(upbi);
                    }

                    if (checkResult.Result)
                    {
                        if (!checkResult.IsReCheck)
                        {
                            //start edit by wuxw 更新下架单数据
                            if (upbi.DeliverErrorBoxList != null && upbi.DeliverErrorBoxList.Count > 0)
                            {
                                List<InventoryOutLogDetailInfo> toBeUploadList = new List<InventoryOutLogDetailInfo>();
                                foreach (PKDeliverErrorBox eb in upbi.DeliverErrorBoxList)
                                {
                                    var outLogDetail = currentOutLogList.FirstOrDefault(o => o.PICK_TASK == eb.PICK_TASK && o.PICK_TASK_ITEM == eb.PICK_TASK_ITEM && o.PRODUCTNO == eb.MATNR);
                                    if (outLogDetail != null)
                                    {
                                        outLogDetail.REALQTY = outLogDetail.REALQTY + (int)eb.REAL;
                                        outLogDetail.REALQTY_ADD = outLogDetail.REALQTY_ADD + (int)eb.ADD_REAL;

                                        //更新下架单记录到数据库(该功能注释，改为批量更新)
                                        //LocalDataService.UpdateInventoryOutLogDetailRealQty(outLogDetail);
                                        toBeUploadList.Add(outLogDetail);
                                    }
                                }
                                //批量更新下架单记录到数据库
                                LocalDataService.UpdateInventoryOutLogDetailRealQty(toBeUploadList);
                            }
                            //end edit by wuxw

                            //将箱码和下架单对应关系表中该箱码的记录更新为已扫描
                            LocalDataService.SetDeliverBoxIsScan(lblHU.Text, true);
                        }

                        //打印货运标签
                        string PARTNER = currentBoxPickTaskMapInfoList != null && currentBoxPickTaskMapInfoList.Count > 0 ? currentBoxPickTaskMapInfoList[0].PARTNER : "";
                        string PACKMAT = currentBoxPickTaskMapInfoList != null && currentBoxPickTaskMapInfoList.Count > 0 ? currentBoxPickTaskMapInfoList[0].PACKMAT : "";
                        DateTime? shipdate = currentBoxPickTaskMapInfoList != null && currentBoxPickTaskMapInfoList.Count > 0 ? currentBoxPickTaskMapInfoList[0].SHIP_DATE : DateTime.Now.Date;
                        int QTY = tagDetailList.Count(o => o.IsAddEpc == false);

                        string bzwl = currentBoxPickTaskMapInfoList.Find(i => i.HU == lblHU.Text.Trim()).PACKMAT;

                        double weight = calWeight(tagDetailList,bzwl);
                        string error = "";

                        string vsart = mVsart;
                        if(vsart == "" && currentOutLogList.Count>0)
                        {
                            vsart = currentOutLogList.First().VSART;
                        }
                        if(vsart == "")
                        { 
                            string xjdh = currentBoxPickTaskMapInfoList.Find(i => i.HU == lblHU.Text.Trim()).PICK_TASK;
                            List<InventoryOutLogDetailInfo> re = LocalDataService.GetInventoryOutLogDetailByPicktask(xjdh);
                            if (re != null && re.Count > 0)
                            {
                                vsart = re[0].VSART;
                            }

                        }
                        string docno = getDOCNO();
                        if(SysConfig.LGNUM == "ET01")
                        {
                            bool printResult = PrinterHelper.PrintaAjtShippingBox(docno, SysConfig.PrinterName, SysConfig.DeviceInfo.LOUCENG, vsart, mFYDT, shipdate.Value, PARTNER, lblHU.Text, LocalDataService.getXiangXingStr(PACKMAT), QTY, weight, out error);
                        }
                        else
                        {
                            bool printResult = PrinterHelper.PrintHeilanShippingBox(docno, SysConfig.PrinterName, SysConfig.DeviceInfo.LOUCENG, vsart, mFYDT, shipdate.Value, PARTNER, lblHU.Text,LocalDataService.getXiangXingStr(PACKMAT), QTY, weight, out error);
                        }
                    }
                    else
                    {
                        bool printResult = PrinterHelper.PrintErrorBoxTagByTable(currentOutLogList, upbi.DeliverErrorBoxList,errorMsg.ToString());
                        //往DeliverErrorBox添加错误记录
                    }
                    if (currentDeliverErrorBox.Count > 0)
                    {
                        //foreach (PKDeliverErrorBox item in currentDeliverErrorBox)
                        //{
                        //    item.REMARK = item.REMARK + ";" + errorMsg.ToString();
                        //    item.REMARK = item.REMARK.Replace(";;", ";");
                        //    item.REMARK = item.REMARK.TrimEnd(';');
                        //    historyDeliverErrorBoxList.Add(item);
                        //    AddRecordToDeliverErrorBoxGrid(item);
                        //    EnqueueUploadData(item);
                        //}

                        //int jiecuoIndex = 0;
                        foreach (PKDeliverErrorBox item in currentDeliverErrorBox)
                        {
                            //if (item.RecordType == DeliverRecordType.拣错 && currentOutLogList.Count>0)
                            //{
                            //    InventoryOutLogDetailInfo outLog = currentOutLogList[jiecuoIndex % currentOutLogList.Count];
                            //    if (++jiecuoIndex >= currentOutLogList.Count)
                            //        jiecuoIndex = 0;
                            //    item.PICK_TASK = outLog.PICK_TASK;
                            //    item.PICK_TASK_ITEM = outLog.PICK_TASK_ITEM;

                            //    item.REMARK = item.REMARK + ";" + errorMsg.ToString();
                            //    item.REMARK = item.REMARK.Replace(";;", ";");
                            //    item.REMARK = item.REMARK.TrimEnd(';');
                            //    historyDeliverErrorBoxList.Add(item);
                            //    AddRecordToDeliverErrorBoxGrid(item);
                            //    EnqueueUploadData(item);
                            //}
                            //else
                            if (item.RecordType == DeliverRecordType.拣错 && currentOutLogList.Count == 1)
                            {
                                InventoryOutLogDetailInfo outLog = currentOutLogList[0];
                                
                                item.PICK_TASK = outLog.PICK_TASK;
                                item.PICK_TASK_ITEM = outLog.PICK_TASK_ITEM;

                                item.REMARK = item.REMARK + ";" + errorMsg.ToString();
                                item.REMARK = item.REMARK.Replace(";;", ";");
                                item.REMARK = item.REMARK.TrimEnd(';');
                                historyDeliverErrorBoxList.Add(item);
                                AddRecordToDeliverErrorBoxGrid(item);
                                EnqueueUploadData(item);
                            }
                            else
                            {
                                item.REMARK = item.REMARK + ";" + errorMsg.ToString();
                                item.REMARK = item.REMARK.Replace(";;", ";");
                                item.REMARK = item.REMARK.TrimEnd(';');
                                historyDeliverErrorBoxList.Add(item);
                                AddRecordToDeliverErrorBoxGrid(item);
                                EnqueueUploadData(item);
                            }
                        }

                    }
                    //往DeliverBox添加发运箱记录
                    PKDeliverBox deliverBox = new PKDeliverBox() 
                    { 
                        GUID = currentBoxGuid,
                        LOUCENG = SysConfig.DeviceInfo.LOUCENG,
                        PARTNER = currentBoxPickTaskMapInfoList != null && currentBoxPickTaskMapInfoList.Count > 0 ? currentBoxPickTaskMapInfoList[0].PARTNER : "",
                        SHIP_DATE = currentBoxPickTaskMapInfoList != null && currentBoxPickTaskMapInfoList.Count > 0 ? currentBoxPickTaskMapInfoList[0].SHIP_DATE.Value : DateTime.Now.Date,
                        HU = lblHU.Text,
                        RESULT = checkResult.Result ? "S" : "E",
                        REMARK = checkResult.IsReCheck ? "重投" : errorMsg.ToString()
                    };
                    historyDeliverBoxList.Add(deliverBox);
                    AddRecordToDeliverBoxGrid(deliverBox);
                    EnqueueUploadData(deliverBox);

                    UpdateUIControl(InventoryControlType.RESULT_MESSAGE_LABEL, checkResult.Result ? (checkResult.IsReCheck == false ? "正常" : "重投") : "异常");

                    //设置扫描结果，可以往plc发送指令了
                    if (checkResult.Result)
                        SetInventoryResult(1, "");
                    else
                        SetInventoryResult(3, "");

                }
                catch (Exception ex)
                {
                    LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                    SetInventoryResult(3, ex.Message);
                    return false;
                }
            }
            return true;
        }
        double calWeight(List<TagDetailInfo> tags,string bzwl)
        {
            //int guahao = bzwl.IndexOf('[');
            //if(guahao!=-1)
            //{
            //    bzwl = bzwl.Substring(0, guahao);
            //}
            //else
            //{
            //    LogHelper.WriteLine("箱型找不到[");
            //}
            //double re = 0;
            //if(tags!=null)
            //{
            //    foreach(TagDetailInfo t in tags)
            //    {
            //        re += t.BRGEW;
            //    }

            //    string fhbc = bzwl;
            //    if (fhbc == "箱装标准箱")
            //    {
            //        re += 1.211;
            //    }
            //    else if (fhbc == "挂装标准箱")
            //    {
            //        re += 1.764;
            //    }
            //    else if (fhbc == "鞋子箱")
            //    {
            //        re += 1.041;
            //    }
            //    else if (fhbc == "小件箱")
            //    {
            //        re += 0.784;
            //    }
            //    else if (fhbc == "大衣箱")
            //    {
            //        re += 1.900;
            //    }

            //    return re;
            //}

            double re = 0;
            if (tags != null)
            {
                foreach (TagDetailInfo t in tags)
                {
                    re += t.BRGEW;
                }
            }
            string fhbc = bzwl;

            re += LocalDataService.getXiangXingWeight(fhbc);
            

            return re;
        }
        /// <summary>
        /// 将所有需要异步上传的数据都加入此队列
        /// </summary>
        /// <param name="obj"></param>
        private bool EnqueueUploadData(object obj)
        {
            if (obj.GetType() == typeof(PKDeliverErrorBox))
            {
                LocalDataService.SaveDeliverErrorBox(obj as PKDeliverErrorBox);
                return true;
            }
            else if (obj.GetType() == typeof(PKDeliverBox))
            {
                LocalDataService.SaveDeliverBox(obj as PKDeliverBox);
                return true;
            }
            else if (obj.GetType() == typeof(UploadPKBoxInfo))
            {
                SqliteDataService.InsertUploadData(obj as UploadPKBoxInfo);
                UploadServer.GetInstance().addToQueue(obj as UploadPKBoxInfo);
            }
            return true;
        }

        /// <summary>
        /// 更新副窗口发运箱明细Grid
        /// 已过时-note by zjr 20160322
        /// </summary>
        /// <param name="rows"></param>
        private void UpdateSubFormDeliverDetailGrid(DataGridViewRowCollection rows)
        {
        }

        /// <summary>
        /// 添加记录到发运错误箱Grid
        /// </summary>
        /// <param name="item"></param>
        private void AddRecordToDeliverErrorBoxGrid(PKDeliverErrorBox item)
        {
            if (item == null)
                return;

            //行颜色切换
            //if (item.BOXGUID != lastRowBoxGuid)
            //{
            //    rowColor = rowColor == Color.LimeGreen ? Color.White : Color.LimeGreen;
            //    lastRowBoxGuid = item.BOXGUID;
            //}
            Invoke(new Action(() =>
            {
                gridDeliverErrorBox.Rows.Insert(0, item.PARTNER, item.HU, item.PICK_TASK, item.ZSATNR, item.ZCOLSN, item.ZSIZTX, item.REAL, item.DIFF, item.REMARK);
                //gridDeliverErrorBox.Rows[0].DefaultCellStyle.BackColor = rowColor;
                
            }));
        }

        /// <summary>
        /// 添加记录到发运箱Grid
        /// </summary>
        /// <param name="item"></param>
        private void AddRecordToDeliverBoxGrid(PKDeliverBox item)
        {
        }

        /// <summary>
        /// 清空发运箱Grid
        /// </summary>
        private void ClearDeliverBoxGrid()
        {
        }

        private UploadPKBoxInfo GetCurrentUploadPKBox(bool inventoryResult)
        {
            return new UploadPKBoxInfo()
            {
                Guid = currentBoxGuid,
                LGNUM = SysConfig.LGNUM,
                SHIP_DATE = currentBoxPickTaskMapInfoList != null && currentBoxPickTaskMapInfoList.Count > 0 ? currentBoxPickTaskMapInfoList[0].SHIP_DATE.Value : DateTime.Now.Date,
                PARTNER = currentBoxPickTaskMapInfoList != null && currentBoxPickTaskMapInfoList.Count > 0 ? currentBoxPickTaskMapInfoList[0].PARTNER : "",
                HU = lblHU.Text,
                InventoryResult = inventoryResult,
                ErrorMsg = errorMsg.ToString(),
                SubUser = SysConfig.CurrentLoginUser.UserId,
                LOUCENG = SysConfig.DeviceInfo.LOUCENG,
                EQUIP_HLA = SysConfig.DeviceInfo.EQUIP_HLA,
                ChangeTime = DateTime.Now,
                TagDetailList = tagDetailList != null ? new List<TagDetailInfo>(tagDetailList) : null,
                DeliverErrorBoxList = currentDeliverErrorBox != null ? new List<PKDeliverErrorBox>(currentDeliverErrorBox) : null,
                IsWholeDeviver = isWholeDeliver()
                
            };
        }

        /// <summary>
        /// PLC信息处理方法
        /// </summary>
        private void LogicThreadFunc()
        {
            while (true)
            {
                if (comDataQueue.Count > 0)
                {
                    string hexStr = comDataQueue.Dequeue();
                    if (!string.IsNullOrEmpty(hexStr))
                    {
                        if (hexStr == ZZ_JS_KAI_SHE_PIN) //开射频
                        {
                            StartInventory();
                        }
                        else if (hexStr == ZZ_JS_GUAN_SHE_PIN) //关射频
                        {
                            StopInventory();
                        }
                        else if (hexStr == ZZ_JS_XUN_WEN_JIE_GUO) //询问结果
                        {
                            int result = inventoryResult;
                            switch (result)
                            {
                                case 1: //正常
                                    StopInventory();
                                    WriteMsgToPlc(ZZ_FS_ZHENG_CHANG);
                                    break;
                                case 3: //异常
                                    StopInventory();
                                    WriteMsgToPlc(ZZ_FS_YI_CHANG);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                Thread.Sleep(20);
            }
        }

        private void WriteMsgToPlc(string command)
        {
            byte[] responseBts = null;
            responseBts = TypeConvert.HexStringToByteArray(command);
            port.Write(responseBts, 0, 8);
        }
        /// <summary>
        /// 初始化PLC串口通信
        /// </summary>
        private void initPlcPort()
        {
            #region 连接串口
            port = new SerialPort(SysConfig.Port);
            port.BaudRate = 9600;//波特率
            port.Parity = Parity.None;//无奇偶校验位
            port.StopBits = StopBits.One;//一个停止位
            port.DataBits = 8;
            port.ReadTimeout = 200;
            port.ReadBufferSize = 8;

            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);//DataReceived事件委托

            try
            {
                port.Open();
            }
            catch { }

            if (!port.IsOpen)
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show("连接串口设备失败！", "错误");
                }));
                //return;
                UpdateUIControl(InventoryControlType.PLC_STATUS_LABEL, "异常");
            }
            else
            {
                UpdateUIControl(InventoryControlType.PLC_STATUS_LABEL, "正常");
            }

            #endregion
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int n = port.BytesToRead;
                byte[] buf = new byte[n];
                //默认是杏林的通道机

                port.Read(buf, 0, n);

                //缓存数据
                comBuffer.AddRange(buf);

                //判断数据是否达到8个字节
                while (comBuffer.Count >= 8)
                {
                    //判断数据头是否为01
                    if (comBuffer[0] == 0x01)
                    {
                        byte[] package = new byte[8];
                        comBuffer.CopyTo(0, package, 0, 8);
                        string hexStr = TypeConvert.ByteArrayToHexString(package);
                        //移除该8个字节
                        comBuffer.RemoveRange(0, 8);

                        //检测校验位是否正确，不正确则跳过
                        if (!TypeConvert.CheckCRC(hexStr))
                        {
                            continue;
                        }
                        comDataQueue.Enqueue(hexStr);
                    }
                    else
                    {
                        //数据头不正确，清除
                        comBuffer.RemoveAt(0);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 初始化扫描模组串口
        /// </summary>
        private void initScannerPort()
        {
            #region 连接条码扫描器串口-add by jrzhuang
            scannerPort_1 = new SerialPort(SysConfig.ScannerPort_1);
            scannerPort_1.BaudRate = 9600;//波特率
            scannerPort_1.Parity = Parity.None;//无奇偶校验位
            scannerPort_1.StopBits = StopBits.One;//一个停止位
            scannerPort_1.DataBits = 8;
            scannerPort_1.ReadTimeout = 200;
            scannerPort_1.ReadBufferSize = 8;
            scannerPort_1.DataReceived += new SerialDataReceivedEventHandler(scannerPort_1_DataReceived);
            try
            {
                scannerPort_1.Open();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            if (!scannerPort_1.IsOpen)
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show("连接条码扫描枪串口设备1失败！", "错误");
                }));
            }


            scannerPort_2 = new SerialPort(SysConfig.ScannerPort_2);
            scannerPort_2.BaudRate = 9600;//波特率
            scannerPort_2.Parity = Parity.None;//无奇偶校验位
            scannerPort_2.StopBits = StopBits.One;//一个停止位
            scannerPort_2.DataBits = 8;
            scannerPort_2.ReadTimeout = 200;
            scannerPort_2.ReadBufferSize = 8;
            scannerPort_2.DataReceived += new SerialDataReceivedEventHandler(scannerPort_2_DataReceived);
            try
            {
                scannerPort_2.Open();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            if (!scannerPort_2.IsOpen)
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show("连接条码扫描枪串口设备2失败！", "错误");
                }));
            }
            #endregion
        }

        void scannerPort_2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int n = scannerPort_2.BytesToRead;
                byte[] buf = new byte[n];
                scannerPort_2.Read(buf, 0, n);
                scannerPortReadHandle(buf, 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        void scannerPort_1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int n = scannerPort_1.BytesToRead;
                byte[] buf = new byte[n];
                scannerPort_1.Read(buf, 0, n);
                scannerPortReadHandle(buf, 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private string barcode = "";
        /// <summary>
        /// 有两个扫描模组，重构出一个公共的数据读取接口
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="scannerPortNo"></param>
        private void scannerPortReadHandle(byte[] buf, int scannerPortNo)
        {
            //string hexStr = TypeConvert.ByteArrayToHexString(buf);
            if (barcode.EndsWith("\r\n"))
                barcode = "";
            barcode += System.Text.Encoding.Default.GetString(buf);
            //LogHelper.WriteLine(barcode + DateTime.Now);

            if (barcode.EndsWith("\r\n"))
            {
                //以回车和换行结尾，表示合法数据
                string barcodestring = barcode.Replace("\r\n", "");

                if (!boxNoList.Contains(barcodestring))
                {
                    boxNoList.Enqueue(barcodestring);
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (isInventory)
            {
                if ((DateTime.Now - lastReadTime).TotalMilliseconds >= SysConfig.DelayTime)
                {
                    //1S钟内没有读到新的标签，则未为本箱检测完毕，关闭射频，检查结果
                    StopInventory();
                }
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            GxForm form = new GxForm();
            form.ShowDialog();
        }

        private void UpdateUIControl(InventoryControlType type, string str)
        {
            Invoke(new Action(() =>
            {
                switch (type)
                {
                    case InventoryControlType.SHIP_DATE_LABEL:
                        lblShipDate.Text = str;
                        break;
                    case InventoryControlType.DEVICE_NO_LABEL:
                        lblDeviceNo.Text = str;
                        break;
                    case InventoryControlType.STATUS_LABEL:
                        lblStatus.Text = str;
                        break;
                    case InventoryControlType.SCAN_NUM_LABEL:
                        lblScanNum.Text = str;
                        break;
                    case InventoryControlType.RIGHT_NUM_LABEL:
                        lblRightNum.Text = str;
                        break;
                    case InventoryControlType.ERROR_NUM_LABEL:
                        lblErrorNum.Text = str;
                        break;
                    case InventoryControlType.HU_LABEL:
                        lblHU.Text = str;
                        break;
                    case InventoryControlType.LOGIN_NO_LABEL:
                        lblLoginNo.Text = str;
                        break;
                    case InventoryControlType.LOUCENG_LABEL:
                        lblLOUCENG.Text = str;
                        break;
                    case InventoryControlType.PLC_STATUS_LABEL:
                        lblPLCStatus.Text = str;
                        break;
                    case InventoryControlType.READER_STATUS_LABEL:
                        lblReaderStatus.Text = str;
                        break;
                    case InventoryControlType.RESULT_MESSAGE_LABEL:
                        lblResultMessage.Text = str;
                        break;
                }
            }));

        }

        #region 测试代码
        private void btnTestStart_Click(object sender, EventArgs e)
        {
            StartInventory();
        }

        private void btnTestStop_Click(object sender, EventArgs e)
        {
            StopInventory();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string boxNo = txtTestBoxNo.Text.Trim();
            if (!string.IsNullOrEmpty(boxNo))
            {
                boxNoList.Enqueue(boxNo);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string epcs = txtTestEpc.Text.Trim();
            if (!string.IsNullOrEmpty(epcs))
            {
                string[] epcList = epcs.Split('\n');
                if (epcList != null && epcList.Length > 0)
                {
                    foreach (string epc in epcList)
                    {
                        if (!string.IsNullOrEmpty(epc))
                        {
                            Reader_OnTagReported(new Xindeco.Device.Model.TagInfo()
                            {
                                Epc = epc.Trim(),
                                Rssi = -30
                            });
                        }
                    }
                }
            }
        }

        private Point _mousePoint;
        private void label10_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                gbTestPanel.Top = MousePosition.Y - _mousePoint.Y;
                gbTestPanel.Left = MousePosition.X - _mousePoint.X;
            }
        }

        private void label10_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mousePoint.X = e.X;
                _mousePoint.Y = e.Y;
            }
        }
        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            QueryBox();
        }

        private void txtImportBoxNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                QueryBox();
            }
        }

        private void QueryBox()
        {
            string hu = txtImportBoxNo.Text.Trim();
            txtImportBoxNo.Clear();
            PKDeliverBox boxInfo = historyDeliverBoxList != null ? historyDeliverBoxList.FirstOrDefault(o => o.HU == hu) : null;
            if (boxInfo == null)
            {
                MetroMessageBox.Show(this, string.Format("未查找到 {0} 箱的记录", hu), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<PKDeliverErrorBox> errorList = historyDeliverErrorBoxList.Where(o => o.HU == hu).ToList();
            PKBoxDetailForm form = new PKBoxDetailForm(boxInfo, errorList);
            form.ShowDialog();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "开始")
            {
                Start();
            }
            else
            {
                btnGetData.Enabled = true;
                btnStop.Enabled = false;

                btnStart.Text = "开始";
            }
        }

        private void Start()
        {
            btnGetData.Enabled = false;
            btnStop.Enabled = true;

            btnStart.Text = "停止";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnGetData.Enabled = true;
            btnStop.Enabled = false;
        }

        private void ShorUploadMsgForm()
        {
            UploadMsgForm form = new UploadMsgForm();
            form.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {           
            int uploadCount = SqliteDataService.GetUploadDataCount();
            updateUploadButton(uploadCount);
        }
        private void updateUploadButton(int cou)
        {
            this.Invoke(new Action(() =>
            {
                uploadButton.Text = string.Format("上传列表({0})", cou);
            }));
        }
        private void newUploadButton_Click(object sender, EventArgs e)
        {
            ShorUploadMsgForm();
        }

        private void button1_dj_Click(object sender, EventArgs e)
        {
            MainForm djform = new MainForm(materialList);
            djform.ShowDialog();
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            ShorUploadMsgForm();
        }

        private void InventoryMainForm_Shown(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer1.Enabled = true;
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            GetDataForm gd = new GetDataForm();
            gd.ShowDialog();
        }
    }

    public class CheckResultInfo
    {
        /// <summary>
        /// 盘点结果
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 是否重投
        /// </summary>
        public bool IsReCheck { get; set; }
    }

    /// <summary>
    /// 主辅条码数量映射表
    /// </summary>
    public class MAMapInfo
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 主条码数量
        /// </summary>
        public int MainNumber { get; set; }
        /// <summary>
        /// 辅条码数量
        /// </summary>
        public int AddNumber { get; set; }
    }
}