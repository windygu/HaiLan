using DMSkin;
using HLACommonLib;
using HLACommonLib.DAO;
using HLACommonLib.Model;
using HLACommonLib.Model.YK;
using HLACommonView.Model;
using HLACommonView.Views;
using HLACommonView.Configs;
using HLAYKChannelMachine.DialogForms;
using HLAYKChannelMachine.Models;
using HLAYKChannelMachine.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Xindeco.Device.Model;
using System.Xml;
using HLAYKChannelMachine.Configs;
using OSharp.Utility.Extensions;
using Newtonsoft.Json;

namespace HLAYKChannelMachine
{
    public partial class InventoryForm : CommonInventoryFormIMP
    {
        private List<AuthInfo> mControlFlag = new List<AuthInfo>();

        string currentZsatnr, currentZcolsn;
        bool isFull = false;
        Thread thread = null;
        List<YKBoxInfo> boxList = new List<YKBoxInfo>();
        /// <summary>
        /// 包装材料数据源
        /// </summary>
        DataTable BoxStyleTable = null;
        /// <summary>
        /// 当前箱的包装材料
        /// </summary>
        string packmat = string.Empty;
        /// <summary>
        /// 当前箱交接号（只针对目标存储类型为Y001的箱子有效）
        /// </summary>
        string lifnr = string.Empty;
        /// <summary>
        /// AuthCode以G开头的权限信息
        /// </summary>
        List<AuthInfo> GAuthList = null;
        public InventoryForm()
        {
            InitializeComponent();
            InitDevice(UHFReaderType.ImpinjR420, true);
        }

        private void dmButton3_Click(object sender, EventArgs e)
        {
            StartInventory();
        }

        public override void UpdateView()
        {
            Invoke(new Action(() => { lblQty.Text = mainEpcNumber.ToString(); }));
        }

        public override void StartInventory()
        {
            if (!isInventory)
            {
                Invoke(new Action(() => {
                    lblWorkStatus.Text = "开始扫描";
                    lblQty.Text = "0";
                    lblResult.Text = "";
                    lblHu.Text = "";
                }));
                SetInventoryResult(0);
                isFull = false;
                packmat = string.Empty;
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

                lastReadTime = DateTime.Now;
                reader.StartInventory(0, 0, 0);
                isInventory = true;

            }
        }

        void stopReader()
        {
            if(isInventory)
            {
                isInventory = false;
                reader.StopInventory();
            }
        }
        public override void StopInventory()
        {
            try
            {
                if (isInventory)
                {
                    Invoke(new Action(() =>
                    {
                        lblWorkStatus.Text = "停止扫描";
                    }));
                    isInventory = false;
                    reader.StopInventory();

                    CheckResult checkResult = CheckData();
                    playSound(checkResult);
                    YKBoxInfo box = GetCurrentYKBox(checkResult);

                    if (lblUsePrint.DM_Key == DMSkin.Controls.DMLabelKey.正确)
                    {

                        if (checkResult.InventoryResult)
                            PrintHelper.PrintRightTag(box, materialList);
                        else
                            PrintHelper.PrintErrorTag(box, lblCheckSku.DM_Key == DMSkin.Controls.DMLabelKey.正确);
                    }

                    if (!checkResult.IsRecheck)
                    {
                        uploadSap(box);
                    }

                    if (boxList == null) boxList = new List<YKBoxInfo>();
                    if (lblCheckPinSe.DM_Key == DMSkin.Controls.DMLabelKey.正确)
                    {
                        if (boxList.Count(i => i.Status == "S") == 0 && box.Status == "S")
                        {
                            currentZcolsn = box.Details?.First().Zcolsn;
                            currentZsatnr = box.Details?.First().Zsatnr;
                        }
                    }

                    if (!checkResult.IsRecheck)
                    {
                        boxList.RemoveAll(i => i.Hu == box.Hu);
                        boxList.Add(box);
                    }

                    AddGrid(box);
                    updateUIInfo();

                    if (checkResult.InventoryResult || checkResult.IsRecheck)
                    {
                        SetInventoryResult(1);
                    }
                    else
                    {
                        SetInventoryResult(1);
                    }
                }
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(ex);
                SetInventoryResult(1);
            }
        }

        void updateUIInfo()
        {
            try
            {
                UpdateErrorBoxButton();
                UpdateTotalInfo();

            }
            catch (Exception e)
            {
                Log4netHelper.LogError(e);
            }
        }
        void uploadSap(YKBoxInfo box)
        {
            try
            {
                SqliteUploadDataInfo ud = new SqliteUploadDataInfo();
                ud.Guid = Guid.NewGuid().ToString();
                ud.Data = box;
                ud.IsUpload = 0;
                ud.CreateTime = DateTime.Now;
                YKBoxSqliteService.InsertUploadData(ud);

                SapResult result = SAPDataService.UploadYKBoxInfo(SysConfig.LGNUM, box);
                box.SapRemark = result.MSG;
                box.SapStatus = result.STATUS;
                bool xdSaveResult = YKBoxService.SaveBox(box);
                YKBoxSqliteService.SetUploaded(ud.Guid);

            }
            catch (Exception e)
            {
                Log4netHelper.LogError(e);
            }
        }
        private YKBoxInfo GetCurrentYKBox(CheckResult cr)
        {
            YKBoxInfo result = new YKBoxInfo()
            {
                EquipHla = SysConfig.DeviceInfo.EQUIP_HLA,
                EquipXindeco = SysConfig.DeviceNO,
                Hu = lblHu.Text,
                IsFull = (byte)(isFull ? 1 : 0),
                LouCeng = SysConfig.DeviceInfo.LOUCENG,
                Remark = cr.Message,
                IsHandover = 0,
                Status = cr.InventoryResult ? "S" : "E",
                Source = cboSource.Text,
                Target = cboTarget.Text,
                SubUser = SysConfig.CurrentLoginUser.UserId,
                SapRemark = "",
                SapStatus = "",
                PackMat = packmat,
                LIFNR = lifnr
            };
            tagDetailList.ForEach(a => {
                result.Details.Add(new YKBoxDetailInfo() {
                    Barcd = a.BARCD, Epc = a.EPC, Hu = result.Hu,
                    Matnr = a.MATNR, Zcolsn = a.ZCOLSN, Zsatnr =a.ZSATNR,
                    Zsiztx = a.ZSIZTX, IsAdd = (byte)(a.IsAddEpc ? 1 : 0)
                });
            });
            return result;
        }

        private void EnqueueUploadData(YKBoxInfo data)
        {
            SqliteUploadDataInfo ud = new SqliteUploadDataInfo();
            ud.Guid = Guid.NewGuid().ToString();
            ud.Data = data;
            ud.IsUpload = 0;
            ud.CreateTime = DateTime.Now;
            YKBoxSqliteService.InsertUploadData(ud);
            UploadServer.GetInstance().CurrentUploadQueue.Push(ud);
        }
        private bool ExistsSameEpc()
        {
            //lock(boxListLock)
            //{
                if (boxList != null && boxList.Count > 0)
                {
                    foreach (YKBoxInfo item in boxList)
                    {
                        if (item.Status == "S" && item.SapStatus != "E")
                        {
                            if (item.Details != null && item.Hu != lblHu.Text)
                            {
                                if (item.Details.Exists(i => epcList.Contains(i.Epc)))
                                    return true;
                            }
                        }

                    }
                }
                return false;
            //}
        }

        public string getTarControlFlag()
        {
            if (mControlFlag != null)
            {
                string tar = cboTarget.Text.Trim();
                foreach (AuthInfo ai in mControlFlag)
                {
                    if (ai.AUTH_VALUE.Trim() == tar)
                        return ai.AUTH_VALUE_DES.Trim();
                }
            }
            return "";
        }
        void playSound(CheckResult cr)
        {
            try
            {
                if (cr.InventoryResult)
                {
                    AudioHelper.Play(".\\Res\\success.wav");
                }
                else
                {
                    AudioHelper.Play(".\\Res\\fail.wav");
                }
            }
            catch (Exception)
            { }
        }
        public override CheckResult CheckData()
        {
            CheckResult result = new CheckResult();
            try
            {
                result = base.CheckData();
                //检查商品已扫描，重投，箱码重复使用
                if (string.IsNullOrEmpty(lblHu.Text.Trim()))
                {
                    result.UpdateMessage(Consts.Default.WEI_SAO_DAO_XIANG_MA);
                    result.InventoryResult = false;

                    string hu = LocalDataService.GetNewErrorHu(SysConfig.DeviceNO);
                    Invoke(new Action(() =>
                    {
                        lblHu.Text = hu;
                    }));
                }

                if (tagDetailList.Count > 0)
                {
                    if (tagDetailList.First().LIFNRS?.Count > 1)
                    {
                        result.UpdateMessage(Consts.Default.JIAO_JIE_HAO_BU_YI_ZHI);
                        result.InventoryResult = false;
                    }
                    else
                    {
                        lifnr = tagDetailList.First().LIFNRS?.FirstOrDefault();
                    }

                    YKBoxInfo box = boxList == null ? null : boxList.Find(i => i.Hu == lblHu.Text);
                    if (box != null && box.Status != null && box.Status == "S" && box.SapStatus == "S")
                    {
                        //上次检测结果为正常，
                        bool isAllSame = true;
                        bool isAllNotSame = true;
                        List<YKBoxDetailInfo> lastCheckDetail = box.Details;
                        if (lastCheckDetail != null && lastCheckDetail.Count > 0)
                        {
                            if (lastCheckDetail.Count != epcList.Count) isAllSame = false;
                            foreach (YKBoxDetailInfo item in lastCheckDetail)
                            {
                                if (!epcList.Contains(item.Epc))
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
                        }
                        else if (isAllNotSame)
                        {
                            //两批EPC对比，完全不一样，示为箱码重复使用
                            result.UpdateMessage(Consts.Default.XIANG_MA_CHONG_FU_SHI_YONG);
                            result.InventoryResult = false;
                        }

                        if (lastCheckDetail.Count > 0 && !isAllSame && !isAllNotSame)
                        {
                            result.UpdateMessage(Consts.Default.EPC_YI_SAO_MIAO);
                            result.InventoryResult = false;
                        }
                    }
                    else
                    {
                        if (tagDetailList.Select(i => i.MATNR).Distinct().Count() > 10)
                        {
                            if (lblCheckSku.DM_Key == DMSkin.Controls.DMLabelKey.正确)
                            {
                                result.UpdateMessage(Consts.Default.SHANG_PIN_DA_YU_SHI);
                                result.InventoryResult = false;
                            }
                        }
                        if (lblCheckPinSe.DM_Key == DMSkin.Controls.DMLabelKey.正确)
                        {
                            if (!string.IsNullOrEmpty(currentZsatnr) && !string.IsNullOrEmpty(currentZcolsn))
                            {
                                if (tagDetailList?.First().ZSATNR != currentZsatnr || tagDetailList?.First().ZCOLSN != currentZcolsn)
                                {
                                    result.UpdateMessage(Consts.Default.PIN_SE_BU_FU);
                                    result.InventoryResult = false;
                                }
                            }
                        }

                        if (ExistsSameEpc())
                        {
                            result.UpdateMessage(Consts.Default.EPC_YI_SAO_MIAO);
                            result.InventoryResult = false;
                        }

                        if (GetCurrentReceiveMode() == ReceiveMode.按品色装箱)
                        {
                            if (!IsOnePinSe())
                            {
                                result.UpdateMessage(Consts.Default.DUO_GE_PIN_SE);
                                result.InventoryResult = false;
                            }
                        }
                        if (GetCurrentReceiveMode() == ReceiveMode.按规格装箱)
                        {
                            if (!IsOneSku())
                            {
                                result.UpdateMessage(Consts.Default.DUO_GE_SHANG_PIN);
                                result.InventoryResult = false;
                            }

                            int pxqty = 0;
                            MaterialInfo material = materialList.Find(i => i.MATNR == tagDetailList.First().MATNR);

                            if (material.PUT_STRA == "ADM1" || cboTarget.Text == "BDMX")
                            {
                                pxqty = material.PXQTY;
                                packmat = material.PXMAT;
                            }
                            else
                            {
                                pxqty = material.PXQTY_FH;
                                packmat = material.PXMAT_FH;
                            }
                            /*
                             * 如果设备信息中配置了带G开头的权限信息
                             * 且对应的AUTH_VALUE的值与当前选择的源存储类型一致 
                             * 则取收货箱规
                             */
                            if (GAuthList?.Count > 0 && GAuthList.Exists(i => i.AUTH_VALUE == cboSource.Text))
                            {
                                pxqty = material.PXQTY;
                            }
                            if (lblUseBoxStandard.DM_Key == DMSkin.Controls.DMLabelKey.正确)
                            {
                                if (mainEpcNumber != pxqty)
                                {
                                    isFull = false;
                                    result.UpdateMessage(Consts.Default.BU_FU_HE_XIANG_GUI + string.Format("({0})", pxqty));
                                    result.InventoryResult = false;
                                }
                                else
                                    isFull = true;
                            }
                            else
                            {
                                if (mainEpcNumber > pxqty)
                                {
                                    result.UpdateMessage(Consts.Default.SHU_LIANG_DA_YU_XIANG_GUI + string.Format("({0})", pxqty));
                                    result.InventoryResult = false;
                                }
                            }
                        }
                        else
                        {
                            if (shouldCheckNum())
                            {
                                //075接口获取数量对比
                                string sapRe = "";
                                string sapMsg = "";
                                int re = SAPDataService.RFID_075F(lblHu.Text, ref sapRe, ref sapMsg);
                                if (sapRe == "E")
                                {
                                    result.UpdateMessage(string.Format("未获取到装箱数据 {0}", lblHu.Text));
                                    result.InventoryResult = false;
                                }
                                else
                                {
                                    if (mainEpcNumber != re)
                                    {
                                        result.UpdateMessage(string.Format("装箱数量错误 {0}-{1}", re, mainEpcNumber));
                                        result.InventoryResult = false;
                                    }
                                }
                            }
                        }

                        if (lblUseBoxStandard.DM_Key != DMSkin.Controls.DMLabelKey.正确)
                        {
                            packmat = cboPxmat.SelectedValue.CastTo("未选择包材");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(ex);
                result.InventoryResult = false;
                result.IsRecheck = false;
                result.Message = ex.ToString();
            }

            if (result.InventoryResult || result.IsRecheck)
            {
                result.UpdateMessage(result.IsRecheck ? Consts.Default.CHONG_TOU : Consts.Default.RIGHT);
            }

            lblResult.Text = result.Message;
            return result;
        }
        string getSourceDes()
        {
            return cboSource.Text.Trim();
        }

        bool shouldCheckNum()
        {
            try
            {
                AuthInfo ai = SysConfig.DeviceInfo?.AuthList?.FirstOrDefault(i => i.AUTH_VALUE == getSourceDes());
                if (ai != null && ai.AUTH_VALUE_DES == "X")
                {
                    return true;
                }
            }
            catch (Exception)
            { }

            return false;
        }

        private bool IsOneSku()
        {
            if (epcList.Count != tagDetailList.Count)
                return false;
            if (tagDetailList.Select(i => i.MATNR).Distinct().Count() > 1)
                return false;
            return true;
        }

        private bool IsOnePinSe()
        {
            if (epcList.Count != tagDetailList.Count)
                return false;
            TagDetailInfo tag = tagDetailList.First();
            if (tagDetailList.Exists(i => i.ZSATNR != tag.ZSATNR || i.ZCOLSN != tag.ZCOLSN))
                return false;
            return true;
        }
        private void dmButton2_Click(object sender, EventArgs e)
        {
            StopInventory();
        }

        private void dmButton5_Click(object sender, EventArgs e)
        {
        }

        private void dmButton4_Click(object sender, EventArgs e)
        {
            if (boxNoList.Contains(metroTextBox1.Text.Trim())) return;
            boxNoList.Enqueue(metroTextBox1.Text.Trim());
        }

        private void LoadPxmatInfo()
        {

        }
        string res = string.Empty;
        private void InventoryForm_Load(object sender, EventArgs e)
        {
#if DEBUG
            panelDebug.Show();
#endif
            InitView();
            dmButtonStart.Enabled = false;
            dmButtonStop.Enabled = false;
            thread = new Thread(new ThreadStart(() => {
                ShowLoading("正在连接PLC...");
                if (ConnectPlc())
                    Invoke(new Action(() => { lblPlc.Text = "正常"; lblPlc.ForeColor = Color.Black; }));
                else
                    Invoke(new Action(() => { lblPlc.Text = "异常"; lblPlc.ForeColor = Color.OrangeRed; }));
                ShowLoading("正在连接条码扫描枪...");
                ConnectBarcode();
                ShowLoading("正在连接读写器...");
                if (ConnectReader())
                    Invoke(new Action(() => { lblReader.Text = "正常"; lblReader.ForeColor = Color.Black; }));
                else
                {
                    Invoke(new Action(() => {
                        lblReader.Text = "异常"; lblReader.ForeColor = Color.OrangeRed;
                    }));
                }

                mControlFlag = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo?.AuthList?.FindAll(i => i.AUTH_CODE.StartsWith("B")) : null;


                bool closed = false;

                ShowLoading("正在下载包材信息...");
                BoxStyleTable = SAPDataService.GetPackagingMaterialsList(SysConfig.LGNUM, SysConfig.DeviceInfo.LOUCENG, out res);

                if (BoxStyleTable != null)
                {
                    cboPxmat.DataSource = BoxStyleTable;
                    cboPxmat.ValueMember = "PMAT_MATNR";
                    cboPxmat.DisplayMember = "MAKTX";

                    if (BoxStyleTable.Rows.Count <= 0)
                    {
                        this.Invoke(new Action(() =>
                        {
                            HideLoading();
                            MetroMessageBox.Show(this, "未下载到包材数据，请检查网络情况", "提示");
                            closed = true;
                            Close();
                        }));
                    }
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        HideLoading();
                        MetroMessageBox.Show(this, "未下载到包材数据，请检查网络情况", "提示");
                        closed = true;
                        Close();
                    }));
                }

                if (closed) return;

                ShowLoading("正在更新SAP最新物料数据...");
                materialList = SAPDataService.GetMaterialInfoList(SysConfig.LGNUM);
                //materialList = LocalDataService.GetMaterialInfoList();

                if (materialList == null || materialList.Count<=0)
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


                ShowLoading("正在检查是否存在未交接的历史箱记录...");
                boxList = YKBoxService.GetUnHandoverBoxList(SysConfig.DeviceNO);
                if (boxList != null && boxList.Count > 0)
                {
                    bool first = true;
                    foreach(YKBoxInfo item in boxList)
                    {
                        Invoke(new Action(() => {
                            if (first)
                            {
                            
                                cboSource.Text = item.Source;
                                cboTarget.Text = item.Target;
                                Start();
                            
                            }
                            AddGrid(item);
                        }));
                    }
                }
                UploadServer.GetInstance().OnUploaded += UploadServer_OnUploaded;
                UploadServer.GetInstance().Start();
                Invoke(new Action(() => {
                    dmButtonStart.Enabled = true;
                    UpdateTotalInfo();
                    UpdateErrorBoxButton();
                }));
                HideLoading();
            }));
            thread.IsBackground = true;
            thread.Start();
        }

        //private object boxListLock = new object();
        private void UploadServer_OnUploaded(YKBoxInfo Box)
        {
            if (boxList == null) boxList = new List<YKBoxInfo>();
            foreach (YKBoxInfo item in boxList)
            {
                if (item.Hu == Box.Hu)
                {
                    item.SapRemark = Box.SapRemark;
                    item.SapStatus = Box.SapStatus;
                    break;
                }
            }

            UpdateErrorBoxButton();
            UpdateTotalInfo();
        }

        private void UpdateTotalInfo()
        {
            Invoke(new Action(() =>
            {
                int totalBoxNum = (int)boxList?.Count(i => i.SapStatus == "S" && i.Status == "S").CastTo<int>(0);
                int totalNum = (int)boxList?.FindAll(i => i.SapStatus == "S" && i.Status == "S").Sum(j => j.Details?.Count);
                lblTotalBoxNum.Text = totalBoxNum.ToString();
                lblTotalNum.Text = totalNum.ToString();
            }));
        }

        private void UpdateErrorBoxButton()
        {
            Invoke(new Action(() =>
            {
                int errorcount = (boxList?.Count(i => i.SapStatus == "E" && i.Status == "S")).CastTo<int>(0);
                btnErrorBox.Text = string.Format("异常箱明细({0})", errorcount);
                btnErrorBox.DM_NormalColor = errorcount > 0 ? Color.FromArgb(255, 100, 0) : Color.FromArgb(27, 163, 203);
                btnErrorBox.DM_MoveColor = errorcount > 0 ? Color.FromArgb(255, 60, 0) : Color.FromArgb(27, 123, 203);
                btnErrorBox.DM_DownColor = errorcount > 0 ? Color.FromArgb(255, 20, 0) : Color.FromArgb(27, 93, 203);
            }));
        }

        private void AddGrid(YKBoxInfo box)
        {
            if(box.Details!=null && box.Details.Count>0)
            {
                List<string> matnrlist = box.Details.Select(i => i.Matnr).Distinct().ToList();
                Invoke(new Action(() =>
                {
                    foreach (string matnr in matnrlist)
                    {
                        grid.Rows.Insert(0, box.Source, box.Target, box.Hu,
                            box.Details.First(i => i.Matnr == matnr).Zsatnr,
                            box.Details.First(i => i.Matnr == matnr).Zcolsn,
                            box.Details.First(i => i.Matnr == matnr).Zsiztx,
                            box.Details.Count(i => i.Matnr == matnr), box.Remark + " SAP:" + box.SapRemark);
                        grid.Rows[0].Tag = matnr;

                        if (box.Status == "E" || box.SapStatus == "E")
                        {
                            grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                        }
                    }
                }));

            }
            else
            {
                Invoke(new Action(() =>
                {
                    grid.Rows.Insert(0, box.Source, box.Target, box.Hu, "", "", "", 0, box.Remark);
                    grid.Rows[0].Tag = "";
                    grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                }));

            }
        }

        private void InitView()
        {
            Invoke(new Action(() => {
                lblCurrentUser.Text = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "登录信息异常";
                lblDeviceNo.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.EQUIP_HLA : "设备信息异常";
                lblLouceng.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.LOUCENG : "设备信息异常";
                lblPlc.Text = "连接中...";
                lblReader.Text = "连接中...";
                lblWorkStatus.Text = "未开始工作";
                lblHu.Text = "";
                lblQty.Text = "0";
                lblResult.Text = "";
                txtPrinter.Text = SysConfig.PrinterName;
                lblUsePrint.DM_Key = DMSkin.Controls.DMLabelKey.正确;
                foreach(AuthInfo source in SysConfig.DeviceInfo?.AuthList?.FindAll(i => i.AUTH_CODE.StartsWith("A")))
                {
                    cboSource.Items.Add(source.AUTH_VALUE);
                }
                foreach (AuthInfo target in SysConfig.DeviceInfo?.AuthList?.FindAll(i => i.AUTH_CODE.StartsWith("B")))
                {
                    cboTarget.Items.Add(target.AUTH_VALUE);
                }
                GAuthList = SysConfig.DeviceInfo?.AuthList?.FindAll(i => i.AUTH_CODE.StartsWith("G"));

            }));
        }

        private void Start()
        {
            if (string.IsNullOrEmpty(cboSource.Text) || string.IsNullOrEmpty(cboTarget.Text))
            {
                HideLoading();
                MetroMessageBox.Show(this, "请先选择源/目标存储类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(boxList!= null && boxList.Count>0)
            {
                if (boxList.First().Source != cboSource.Text || boxList.First().Target != cboTarget.Text)
                {
                    HideLoading();
                    MetroMessageBox.Show(this, "存在未交接的数据，请交接后再更换源/目标存储类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            dmButtonStart.Enabled = false;
            dmButtonStop.Enabled = true;

            cboSource.Enabled = false;
            cboTarget.Enabled = false;
        }

        private void Stop()
        {
            dmButtonStart.Enabled = true;
            dmButtonStop.Enabled = false;

            cboSource.Enabled = true;
            cboTarget.Enabled = true;
        }

        private void btnUsePs_Click(object sender, EventArgs e)
        {
            SwitchUsePs();
        }
        private void SwitchUsePs()
        {
            lblUsePs.DM_Key = lblUsePs.DM_Key == DMSkin.Controls.DMLabelKey.正确 ? DMSkin.Controls.DMLabelKey.错误 : DMSkin.Controls.DMLabelKey.正确;
            if (lblUseSize.DM_Key == DMSkin.Controls.DMLabelKey.正确)
            {
                lblUseSize.DM_Key = DMSkin.Controls.DMLabelKey.错误;
            }

            if (lblUseSize.DM_Key == DMSkin.Controls.DMLabelKey.正确)
            {
                btnUseBoxStandard.Enabled = true;
            }
            else
            {
                btnUseBoxStandard.Enabled = false;
                lblUseBoxStandard.DM_Key = DMSkin.Controls.DMLabelKey.错误;
            }
        }
        private void btnUseSize_Click(object sender, EventArgs e)
        {
            SwitchUseSize();
        }

        private void SwitchUseSize()
        {
            lblUseSize.DM_Key = lblUseSize.DM_Key == DMSkin.Controls.DMLabelKey.正确 ? DMSkin.Controls.DMLabelKey.错误 : DMSkin.Controls.DMLabelKey.正确;
            if (lblUsePs.DM_Key == DMSkin.Controls.DMLabelKey.正确)
            {
                lblUsePs.DM_Key = DMSkin.Controls.DMLabelKey.错误;
            }
            if (lblUseSize.DM_Key == DMSkin.Controls.DMLabelKey.正确)
            {
                btnUseBoxStandard.Enabled = true;
            }
            else
            {
                btnUseBoxStandard.Enabled = false;
                lblUseBoxStandard.DM_Key = DMSkin.Controls.DMLabelKey.错误;
            }
        }
        private void btnUseBoxStandard_Click(object sender, EventArgs e)
        {
            SwitchUseBoxStandard();
        }
        private void SwitchUseBoxStandard()
        {
            lblUseBoxStandard.DM_Key = lblUseBoxStandard.DM_Key == DMSkin.Controls.DMLabelKey.正确 ? DMSkin.Controls.DMLabelKey.错误 : DMSkin.Controls.DMLabelKey.正确;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGx_Click(object sender, EventArgs e)
        {
            GxForm form = new GxForm();
            form.ShowDialog();
        }

        private void btnGenerateDoc_Click(object sender, EventArgs e)
        {
            if(boxList!= null && boxList.Count>0)
            {
                HideLoading();
                if (MetroMessageBox.Show(this, "确认交接列表中所有箱记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    new Thread(new ThreadStart(() => {
                        ShowLoading("正在交接...");
                        if (YKBoxService.HandoverBoxByDevice(SysConfig.DeviceNO))
                        {
                            Invoke(new Action(() => {
                                boxList.Clear();
                                grid.Rows.Clear();
                                UpdateErrorBoxButton();
                                UpdateTotalInfo();
                                HideLoading();
                                MetroMessageBox.Show(this, "交接成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }));
                        }
                        else
                        {
                            HideLoading();
                            MetroMessageBox.Show(this, "交接失败，可能是网络不稳定，请稍候再试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    })).Start();
                }
            }
           
        }

        private void btnErrorBox_Click(object sender, EventArgs e)
        {
            if (boxList != null && boxList.Exists(i => i.SapStatus == "E" && i.Status == "S"))
            {
                ErrorBoxForm form = new ErrorBoxForm(boxList);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    grid.Rows.Clear();
                    if (boxList.Count > 0)
                    {
                        foreach (YKBoxInfo item in boxList)
                        {
                            AddGrid(item);
                        }
                    }
                }
            }
        }

        private ReceiveMode GetCurrentReceiveMode()
        {
            if (lblUsePs.DM_Key == DMSkin.Controls.DMLabelKey.正确)
                return ReceiveMode.按品色装箱;
            if (lblUseSize.DM_Key == DMSkin.Controls.DMLabelKey.正确)
                return ReceiveMode.按规格装箱;
            return ReceiveMode.空;
        }

        private void InventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseWindow();
        }
        private void btnCheckSku_Click(object sender, EventArgs e)
        {
            lblCheckSku.DM_Key = lblCheckSku.DM_Key == DMSkin.Controls.DMLabelKey.正确 ? DMSkin.Controls.DMLabelKey.错误 : DMSkin.Controls.DMLabelKey.正确;
        }

        private void btnUsePrint_Click(object sender, EventArgs e)
        {
            SwitchUsePrint();
        }

        private void SwitchUsePrint()
        {
            lblUsePrint.DM_Key = lblUsePrint.DM_Key == DMSkin.Controls.DMLabelKey.正确 ? DMSkin.Controls.DMLabelKey.错误 : DMSkin.Controls.DMLabelKey.正确;
        }

        private void txtPrinter_TextChanged(object sender, EventArgs e)
        {
            SysConfig.PrinterName = txtPrinter.Text.Trim();
        }

        private void btnCheckPinSe_Click(object sender, EventArgs e)
        {
            SwitchCheckPinSe();
        }

        private void SwitchCheckPinSe()
        {
            if (boxList?.Count > 0)
            {
                HideLoading();
                MetroMessageBox.Show(this, "请先交接完本批箱记录，再更换品色判断标准", 
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            lblCheckPinSe.DM_Key = lblCheckPinSe.DM_Key == DMSkin.Controls.DMLabelKey.正确 ? DMSkin.Controls.DMLabelKey.错误 : DMSkin.Controls.DMLabelKey.正确;
        }

        private void lblCheckPinSe_Click(object sender, EventArgs e)
        {
            SwitchCheckPinSe();
        }

        private void lblUsePrint_Click(object sender, EventArgs e)
        {
            SwitchUsePrint();
        }

        private void lblUseBoxStandard_Click(object sender, EventArgs e)
        {
            SwitchUseBoxStandard();
        }

        private void lblUseSize_Click(object sender, EventArgs e)
        {
            SwitchUseSize();
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            string box = metroTextBox2.Text.Trim();
            cboPxmat.SelectedValue = box;
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lblPlc_Click(object sender, EventArgs e)
        {

        }
        void openMachine()
        {
            try
            {
                if (plc != null)
                {
                    plc.SendCommand((PLCResponse)5);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void closeMachine()
        {
            try
            {
                if (plc != null)
                {
                    plc.SendCommand((PLCResponse)6);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dmButtonStart_Click(object sender, EventArgs e)
        {
            Start();
            openMachine();
        }

        private void dmButtonStop_Click(object sender, EventArgs e)
        {
            Stop();
            stopReader();
            closeMachine();
        }



        private void uploadButton_Click(object sender, EventArgs e)
        {

        }

        private void lblUsePs_Click(object sender, EventArgs e)
        {
            SwitchUsePs();
        }
    }

    public enum ReceiveMode
    {
        空 =0,
        按品色装箱=1,
        按规格装箱=2
    }
}
