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
    public partial class InventoryFormNew : CommonInventoryFormIMP
    {
        List<AuthInfo> mGAuthList = new List<AuthInfo>();
        List<YKBoxInfo> mBoxList = new List<YKBoxInfo>();
        private Queue<CUploadData> savingData = new Queue<CUploadData>();
        private object savingDataLockObject = new object();
        private Thread savingDataThread = null;
        string mPackmat = "";
        bool mIsFull = false;
        bool mCancel = false;
        public InventoryFormNew()
        {
            InitializeComponent();
            InitDevice(UHFReaderType.ImpinjR420, true);
        }

        public override void UpdateView()
        {
            Invoke(new Action(() => { lblQty.Text = mainEpcNumber.ToString(); }));
        }

        public override void StartInventory()
        {
            if (!isInventory)
            {
                Invoke(new Action(() =>
                {
                    lblWorkStatus.Text = "开始扫描";
                    lblQty.Text = "0";
                    lblResult.Text = "";
                    lblHu.Text = "";
                }));
                SetInventoryResult(0);
                mIsFull = false;
                mPackmat = "";
                errorEpcNumber = 0;
                mainEpcNumber = 0;
                addEpcNumber = 0;
                epcList.Clear();
                tagDetailList.Clear();
                if (boxNoList.Count > 0)
                {
                    Invoke(new Action(() =>
                    {
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
            if (isInventory)
            {
                isInventory = false;
                reader.StopInventory();
            }
        }
        public override void StopInventory()
        {
            try
            {
                if (!isInventory)
                {
                    SetInventoryResult(1);
                    return;
                }
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
                    addToSavingQueue(box);
                }

                AddGrid(box);

                if (checkResult.InventoryResult || checkResult.IsRecheck)
                {
                    SetInventoryResult(1);
                }
                else
                {
                    SetInventoryResult(1);
                }
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(ex);
                SetInventoryResult(1);
            }
        }

        string getLifnr()
        {
            if (tagDetailList.Count > 0)
            {
                if (tagDetailList.First().LIFNRS != null && tagDetailList.First().LIFNRS.Count == 1)
                {
                    return tagDetailList.First().LIFNRS.First();
                }
            }
            return "";
        }
        private YKBoxInfo GetCurrentYKBox(CheckResult cr)
        {
            YKBoxInfo result = new YKBoxInfo()
            {
                EquipHla = SysConfig.DeviceInfo.EQUIP_HLA,
                EquipXindeco = SysConfig.DeviceNO,
                Hu = lblHu.Text,
                IsFull = (byte)(mIsFull ? 1 : 0),
                LouCeng = SysConfig.DeviceInfo.LOUCENG,
                Remark = cr.Message,
                IsHandover = 0,
                Status = cr.InventoryResult ? "S" : "E",
                Source = cboSource.Text,
                Target = cboTarget.Text,
                SubUser = SysConfig.CurrentLoginUser.UserId,
                SapRemark = "",
                SapStatus = "",
                PackMat = mPackmat,
                LIFNR = getLifnr()
            };
            tagDetailList.ForEach(a =>
            {
                result.Details.Add(new YKBoxDetailInfo()
                {
                    Barcd = a.BARCD,
                    Epc = a.EPC,
                    Hu = result.Hu,
                    Matnr = a.MATNR,
                    Zcolsn = a.ZCOLSN,
                    Zsatnr = a.ZSATNR,
                    Zsiztx = a.ZSIZTX,
                    IsAdd = (byte)(a.IsAddEpc ? 1 : 0)
                });
            });
            return result;
        }

        public void addToSavingQueue(YKBoxInfo uploadData)
        {
            CUploadData ud = new CUploadData();
            ud.Guid = Guid.NewGuid().ToString();
            ud.Data = uploadData;
            ud.IsUpload = 0;
            ud.CreateTime = DateTime.Now;
            SqliteDataService.saveToSqlite(ud);

            lock (savingDataLockObject)
            {
                savingData.Enqueue(ud);
            }

            updateUploadCount();

        }

        void updateUploadCount()
        {
            int count = SqliteDataService.GetUnUploadCountFromSqlite();
            Invoke(new Action(() =>
            {
                dmButton1_upload.Text = string.Format("上传列表({0})", count);
            }));

        }

        private bool ExistsSameEpc()
        {
            if (mBoxList != null && mBoxList.Count > 0)
            {
                foreach (YKBoxInfo item in mBoxList)
                {
                    if (item.Status == "S" && item.SapStatus == "S")
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
        }

        void playSoundWarn()
        {
            try
            {
                AudioHelper.Play(".\\Res\\warningwav.wav");
            }
            catch (Exception)
            { }
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

        bool checkSame(ref CheckResult result)
        {
            bool re = false;
            YKBoxInfo box = mBoxList == null ? null : mBoxList.Find(i => i.Hu == lblHu.Text);
            if (box != null && box.Status != null && box.Status == "S" && box.SapStatus == "S")
            {
                re = true;
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
            return re;
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
                    if (tagDetailList.First().LIFNRS != null && tagDetailList.First().LIFNRS.Count > 1)
                    {
                        result.UpdateMessage(Consts.Default.JIAO_JIE_HAO_BU_YI_ZHI);
                        result.InventoryResult = false;
                    }

                    if (!checkSame(ref result))
                    {
                        if (lblCheckSku.DM_Key == DMSkin.Controls.DMLabelKey.正确)
                        {
                            if (tagDetailList.Select(i => i.MATNR).Distinct().Count() > 10)
                            {
                                result.UpdateMessage(Consts.Default.SHANG_PIN_DA_YU_SHI);
                                result.InventoryResult = false;
                            }
                        }
                        if (lblCheckPinSe.DM_Key == DMSkin.Controls.DMLabelKey.正确)
                        {
                            if (tagDetailList != null && tagDetailList.Count > 0)
                            {
                                TagDetailInfo t = tagDetailList[0];
                                foreach (var v in tagDetailList)
                                {
                                    if (v.ZSATNR == t.ZSATNR && v.ZCOLSN == t.ZCOLSN)
                                    {

                                    }
                                    else
                                    {
                                        result.UpdateMessage(Consts.Default.PIN_SE_BU_FU);
                                        result.InventoryResult = false;

                                        break;
                                    }
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
                                mPackmat = material.PXMAT;
                            }
                            else
                            {
                                pxqty = material.PXQTY_FH;
                                mPackmat = material.PXMAT_FH;
                            }
                            /*
                             * 如果设备信息中配置了带G开头的权限信息
                             * 且对应的AUTH_VALUE的值与当前选择的源存储类型一致 
                             * 则取收货箱规
                             */
                            if (mGAuthList?.Count > 0 && mGAuthList.Exists(i => i.AUTH_VALUE == cboSource.Text))
                            {
                                pxqty = material.PXQTY;
                            }
                            if (lblUseBoxStandard.DM_Key == DMSkin.Controls.DMLabelKey.正确)
                            {
                                if (mainEpcNumber != pxqty)
                                {
                                    mIsFull = false;
                                    result.UpdateMessage(Consts.Default.BU_FU_HE_XIANG_GUI + string.Format("({0})", pxqty));
                                    result.InventoryResult = false;
                                }
                                else
                                    mIsFull = true;
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
                            mPackmat = cboPxmat.SelectedValue.CastTo("未选择包材");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(ex);
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

        private void InventoryForm_Load(object sender, EventArgs e)
        {
#if DEBUG
#endif
            InitView();

            Thread thread = new Thread(new ThreadStart(() =>
            {
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
                    Invoke(new Action(() =>
                    {
                        lblReader.Text = "异常"; lblReader.ForeColor = Color.OrangeRed;
                    }));
                }

                bool closed = false;

                ShowLoading("正在下载包材信息...");
                string def = "";
                DataTable boxStyleTable = SAPDataService.GetPackagingMaterialsList(SysConfig.LGNUM, SysConfig.DeviceInfo.LOUCENG, out def);

                if (boxStyleTable != null && boxStyleTable.Rows.Count > 0)
                {
                    cboPxmat.DataSource = boxStyleTable;
                    cboPxmat.ValueMember = "PMAT_MATNR";
                    cboPxmat.DisplayMember = "MAKTX";
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
                mBoxList = YKBoxService.GetUnHandoverBoxList(SysConfig.DeviceNO);

                HideLoading();
            }));
            thread.IsBackground = true;
            thread.Start();
        }

        private void UpdateTotalInfo()
        {
            Invoke(new Action(() =>
            {
                int totalBoxNum = mBoxList.Count(i => i.SapStatus == "S" && i.Status == "S");
                int totalNum = mBoxList.FindAll(i => i.SapStatus == "S" && i.Status == "S").Sum(j => j.Details.Count);
                lblTotalBoxNum.Text = totalBoxNum.ToString();
                lblTotalNum.Text = totalNum.ToString();
            }));
        }

        void updateExpButton()
        {
            int expCount = SqliteDataService.GetExpUploadCount();
            Invoke(new Action(() =>
            {
                string str = string.Format("异常箱明细({0})", expCount);
                btnErrorBox.Text = str;

                if (expCount > 0)
                {
                    btnErrorBox.DM_NormalColor = Color.Red;
                }
                else
                {
                    btnErrorBox.DM_NormalColor = Color.WhiteSmoke;
                }
            }));

        }

        private void AddGrid(YKBoxInfo box)
        {
            if (box.Details != null && box.Details.Count > 0)
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
                            box.Details.Count(i => i.Matnr == matnr), box.Remark);
                        grid.Rows[0].Tag = matnr;

                        if (box.Status == "E")
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
            Invoke(new Action(() =>
            {
                lblCurrentUser.Text = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "登录信息异常";
                lblDeviceNo.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.EQUIP_HLA : "设备信息异常";
                lblLouceng.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.LOUCENG : "设备信息异常";
                lblPlc.Text = "连接中...";
                lblReader.Text = "连接中...";
                lblWorkStatus.Text = "未开始工作";
                lblHu.Text = "";
                lblQty.Text = "0";
                lblResult.Text = "";
                lblUsePrint.DM_Key = DMSkin.Controls.DMLabelKey.正确;
                foreach (AuthInfo source in SysConfig.DeviceInfo?.AuthList?.FindAll(i => i.AUTH_CODE.StartsWith("A")))
                {
                    cboSource.Items.Add(source.AUTH_VALUE);
                }
                foreach (AuthInfo target in SysConfig.DeviceInfo?.AuthList?.FindAll(i => i.AUTH_CODE.StartsWith("B")))
                {
                    cboTarget.Items.Add(target.AUTH_VALUE);
                }
                mGAuthList = SysConfig.DeviceInfo?.AuthList?.FindAll(i => i.AUTH_CODE.StartsWith("G"));

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

            if (mBoxList != null && mBoxList.Count > 0)
            {
                if (mBoxList.First().Source != cboSource.Text || mBoxList.First().Target != cboTarget.Text)
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
            if (mBoxList != null && mBoxList.Count > 0)
            {
                HideLoading();
                if (MetroMessageBox.Show(this, "确认交接列表中所有箱记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    new Thread(new ThreadStart(() =>
                    {
                        ShowLoading("正在交接...");
                        if (YKBoxService.HandoverBoxByDevice(SysConfig.DeviceNO))
                        {
                            Invoke(new Action(() =>
                            {
                                mBoxList.Clear();
                                grid.Rows.Clear();
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
            btnErrorBox.DM_NormalColor = Color.WhiteSmoke;

            UploadMsgForm form = new UploadMsgForm(this);
            form.ShowDialog();
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
            mCancel = true;
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

        private void btnCheckPinSe_Click(object sender, EventArgs e)
        {
            SwitchCheckPinSe();
        }

        private void SwitchCheckPinSe()
        {
            if (mBoxList?.Count > 0)
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

        private void lblUsePs_Click(object sender, EventArgs e)
        {
            SwitchUsePs();
        }
        void restoreGrid()
        {
            foreach (YKBoxInfo item in mBoxList)
            {
                AddGrid(item);
            }
        }
        void restoreUpload()
        {
            List<CUploadData> up = SqliteDataService.GetAllUploadFromSqlite<YKBoxInfo>();
            foreach (var v in up)
            {
                SqliteDataService.delUploadFromSqlite(v.Guid);
                addToSavingQueue(v.Data as YKBoxInfo);
            }
        }
        private void InventoryFormNew_Shown(object sender, EventArgs e)
        {
            restoreGrid();
            restoreUpload();

            UpdateTotalInfo();
            updateExpButton();

            this.savingDataThread = new Thread(new ThreadStart(savingDataThreadFunc));
            this.savingDataThread.IsBackground = true;
            this.savingDataThread.Start();
        }
        void updateBoxList(YKBoxInfo box)
        {
            Invoke(new Action(() =>
            {
                mBoxList.RemoveAll(i => i.Hu == box.Hu);
                mBoxList.Add(box);
            }));
        }
        CUploadData getQueueData()
        {
            CUploadData re = null;
            try
            {
                lock (savingDataLockObject)
                {
                    if (savingData.Count > 0)
                        return savingData.Dequeue();
                }
            }
            catch (Exception) { }
            return re;
        }
        private void savingDataThreadFunc()
        {
            while (true)
            {
                try
                {
                    if (mCancel)
                        return;

                    CUploadData ud = getQueueData();

                    if (ud != null)
                    {
                        YKBoxInfo upData = ud.Data as YKBoxInfo;
                        if (upData != null)
                        {
                            //upload
                            string uploadRe = "";
                            string sapMsg = "";

                            SapResult result = SAPDataService.UploadYKBoxInfo(SysConfig.LGNUM, upData);
                            uploadRe = result.STATUS;
                            sapMsg = result.MSG;

                            if (uploadRe == "E")
                            {
                                SqliteDataService.updateMsgToSqlite(ud.Guid, sapMsg);
                                playSoundWarn();
                            }
                            else
                            {
                                SqliteDataService.delUploadFromSqlite(ud.Guid);
                            }

                            upData.SapRemark = result.MSG;
                            upData.SapStatus = result.STATUS;

                            //save
                            YKBoxService.SaveBox(upData);

                            if (upData.Status == "S" && uploadRe == "S")
                            {
                                updateBoxList(upData);
                                UpdateTotalInfo();
                            }

                            updateUploadCount();
                            updateExpButton();
                        }
                    }
                    Thread.Sleep(500);
                }
                catch (Exception)
                {
                    //LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
                }
            }
        }

        private void dmButton1_upload_Click(object sender, EventArgs e)
        {

        }
    }
}
