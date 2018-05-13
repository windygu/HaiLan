using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using DMSkin;
using DMSkin.Controls;
using DMSkin.Metro.Controls;
using HLACommonLib;
using HLACommonLib.Comparer;
using HLACommonLib.Model;
using HLADeliverChannelMachine.DialogForms;
using HLADeliverChannelMachine.Properties;
using HLADeliverChannelMachine.Utils;
using HLA.IBLL;
using HLA.BLL;
using HLA.Model;
using UARTRfidLink.Exparam;
using UARTRfidLink.Extend;

namespace HLADeliverChannelMachine
{
    public partial class InventoryOutLogForm : Main
    {

        IInventoryOutLogDetailService _services = new InventoryOutLogDetailService();
        #region 属性
        public RfidUARTLinkExtend reader = new RfidUARTLinkExtend();
        string mComPort;
        uint mPower;

        bool isInventory = false;
        /// <summary>
        /// 当前下架单
        /// </summary>
        List<InventoryOutLogDetailInfo> outLogList = null;
        /// <summary>
        /// 与当前下架单有关的物料数据
        /// </summary>
        List<MaterialInfo> materialList = null;
        /// <summary>
        /// 与当前下架单有关的吊牌数据
        /// </summary>
        List<HLATagInfo> tagList = null;
        /// <summary>
        /// 箱型数据
        /// </summary>
        DataTable BoxStyleTable = null;
        /// <summary>
        /// 当前下架单的拣错记录
        /// </summary>
        List<OutLogErrorRecord> errorList = new List<OutLogErrorRecord>();
        #region 当前扫描箱属性
        /// <summary>
        /// 当前箱码
        /// </summary>
        private string currentHu = string.Empty;
        /// <summary>
        /// 本轮扫描到的合法标签总数
        /// </summary>
        private int currentNum = 0;
        private int currentAddNum = 0;
        /// <summary>
        /// 当前下架单的扫描箱
        /// </summary>
        private DataGridViewRow currentScanBox = null;
        #endregion
        /// <summary>
        /// 当前下架单下所有已扫描的EPC列表
        /// </summary>
        private List<ShippingBoxDetail> epcList = new List<ShippingBoxDetail>();
        /// <summary>
        /// 本次读取的历史EPC列表，不允许重读
        /// </summary>
        private List<string> historyEpcList = new List<string>();
        /// <summary>
        /// 无需导入提醒的门店
        /// </summary>
        private List<string> ignoreStoreList = null;
        /// <summary>
        /// 上一次有错误提示的时间
        /// </summary>
        private DateTime lastWarningTime = DateTime.Now;

        private UploadServer uploadServer = UploadServer.GetInstance();

        private List<string> mAllLGTYPList;

        private string mFYDT = "";
        /// <summary>
        /// 上一次检测到有效标签的时间
        /// </summary>
        //private DateTime lastCheckTagTime = DateTime.Now;
        #endregion

        #region InventoryOutLogForm
        public InventoryOutLogForm(List<string> LGTYPList)
        {
            mAllLGTYPList = LGTYPList;

            InitializeComponent();
            errorLoginForm = new ErrorLogForm();
            errorLoginForm.errorScan += new ErrorScan(
                (paramater) =>
                {
                    if (paramater)
                    {
                        //清除已记录的错误信息
                        //this.errorList?.Clear();
                        this.errorTag_List?.Clear();
                    }
                    else
                    {
                        if (this.errorTag_List != null && this.errorTag_List.Count > 0)
                        {
                            foreach (Error_Tag item in this.errorTag_List)
                            {
                                ErrorWarning(item.tid, item.errotType, item.data, item.isRfid);
                            }
                        }
                    }
                }
            );
            CheckForIllegalCrossThreadCalls = false;
            errorLoginForm.FormClosed += ErrorLoginForm_FormClosed;
            txtOutlog.Enter += TextBox_Enter;
            txtBarcode.Enter += TextBox_Enter;
            txtPrinter.Enter += TextBox_Enter;
            txtImportBoxNo.Enter += TextBox_Enter;
            metroTile1.Click += MetroTile_Click;
            metroTile2.Click += MetroTile_Click;
            metroTile3.Click += MetroTile_Click;

            LGTYPCheckedComboBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ccb_ItemCheck);

        }
        private void ccb_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
               {
                   LGTYPCheckedComboBox.updateText();
                   SysConfig.DeviceInfo.LGTYP = LGTYPCheckedComboBox.getSelectItems();
               });
        }

        private void MetroTile_Click(object sender, EventArgs e)
        {
            FocusLastControl();
        }

        private void FocusLastControl()
        {
            if (lastFocusControl != null)
                (lastFocusControl as Control).Focus();
            else
                txtOutlog.Focus();
        }

        private object lastFocusControl = null;
        private void TextBox_Enter(object sender, EventArgs e)
        {
            lastFocusControl = sender;
        }

        /*
private Control lastFocusControl = null;
private void RegistControlEnterEvent(Control.ControlCollection controlCollection)
{
   foreach (Control control in controlCollection)
   {
       control.GotFocus += control_GotFocus;
       if (control.HasChildren)
           RegistControlEnterEvent(control.Controls);
   }
}

void control_GotFocus(object sender, EventArgs e)
{
   if (sender.GetType() != typeof(DMSkin.Metro.Controls.MetroTextBox) &&
       sender.GetType() != typeof(MetroComboBox))
   {
       if (lastFocusControl != null && lastFocusControl != sender as Control)
           lastFocusControl.Focus();
   }
   else
   {
       lastFocusControl = sender as Control;
       (sender as Control).Focus();
   }
}
*/

        /// <summary>
        /// 存储箱型
        /// </summary>
        string mDefaultXiangXin = string.Empty;

        private void InventoryOutLogForm_Load(object sender, EventArgs e)
        {
            if (SysConfig.DeviceInfo.AuthList != null)
            {
                foreach (AuthInfo ai in SysConfig.DeviceInfo.AuthList)
                {
                    if (ai.AUTH_CODE.StartsWith("H"))
                    {
                        mFYDT = ai.AUTH_VALUE;
                        break;
                    }
                }
            }
            this.WindowState = FormWindowState.Maximized;
            this.lblDifferentcount.TextChanged += LblDifferentcount_TextChanged;
            this.lblAuDifferentcount.TextChanged += LblDifferentcount_TextChanged;

            VisiblePanel();

            #region 初始化界面信息
            initView();
            #endregion

            lbcurrentuser.Text = "当前账号：" + SysConfig.CurrentLoginUser.UserId;

            #region 加载配置信息
            //this.txtFloor.Text = "082";
            this.txtFloor.Text = SysConfig.DeviceInfo != null && SysConfig.DeviceInfo.LOUCENG != null ? SysConfig.DeviceInfo.LOUCENG : "楼层号异常";
            this.txtPrinter.Text = SysConfig.PrinterName;
            #endregion

            //默认都不选择
            SysConfig.DeviceInfo.LGTYP.Clear();

            int strIndex = 0;
            foreach (string str in mAllLGTYPList)
            {
                HLACommonView.Views.CCBoxItem item = new HLACommonView.Views.CCBoxItem(str, strIndex++);
                LGTYPCheckedComboBox.Items.Add(item);
            }
            LGTYPCheckedComboBox.MaxDropDownItems = 10;
            LGTYPCheckedComboBox.DisplayMember = "Name";
            LGTYPCheckedComboBox.ValueSeparator = " ";

        }

        private void LblDifferentcount_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(this.lblAuDifferentcount.Text) == 0 && int.Parse(this.lblDifferentcount.Text) == 0)
            {
                this.btnGetBoxNo.Enabled = false;
            }
            else
            {
                this.btnGetBoxNo.Enabled = true;
            }
        }

        void uploadServer_OnUploadSuccess(string PICK_TASK)
        {
            if (outLogList != null)
            {
                outLogList.ForEach(new Action<InventoryOutLogDetailInfo>((detail) =>
                {
                    if (detail.PICK_TASK == PICK_TASK)
                        detail.IsOut = 1;
                }));
            }
        }

        private void VisiblePanel()
        {
            if (SysConfig.DeviceInfo.KF_LX == "G")//挂装
            {
                metroPanel1.Visible = true;
            }
            else
            {//箱装
                metroPanel1.Visible = false;
            }
        }

        /// <summary>
        /// 是否存在未下架的差异为0的下架单数据
        /// </summary>
        /// <returns></returns>
        private bool IsExistNoOutLog()
        {
            if (!string.IsNullOrEmpty(this.lblOutlog.Text.Trim()))
            {
                /*
                 * 当前有下架单正在扫描
                 * 如果差异为0时，说明该下架单很可能是工人漏下架
                 * 需要给出提示
                */
                if (this.lblDifferentcount.Text.Trim() == "0")
                {
                    MetroMessageBox.Show(this,
                        string.Format("下架单[{0}]尚未下架，请先下架", this.lblOutlog.Text.Trim()),
                        "提示",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return true;
                }
            }
            return false;
        }

        private void InventoryOutLogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsExistNoOutLog())
            {
                e.Cancel = true;
                return;
            }
            //如果仍在盘点，则关闭盘点
            if (this.isInventory)
                StopInventory();
            //关闭读写器连接
            reader.DisconnectRadio(mComPort);
            uploadServer.OnUploadSuccess -= new UploadSuccessHandler(uploadServer_OnUploadSuccess);
            //保存配置信息，比如打印机编号的配置
            SetConfigValue("PrinterName", this.txtPrinter.Text.Trim());
            //移除委托监听


        }
        #endregion

        #region 控件事件
        private void btnQuery_Click(object sender, EventArgs e)
        {
            loadInventoryOutLogInfo();
        }

        private List<string> epcLists = new List<string>();

        void reader_OnTagReported(string Epc)
        {
            //if (!epcLists.Contains(taginfo.Epc))
            //{
            //    epcLists.Add(taginfo.Epc);
            //    this.lblCurrentcountBig.Text = (int.Parse(this.lblCurrentcountBig.Text) + 1).ToString();
            //}
            if (!this.isInventory || string.IsNullOrEmpty(Epc) || currentScanBox == null)
                return;
            CheckTag(Epc, true);
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = null;
            try
            {
                row = this.grid.Rows[e.RowIndex];
            }
            catch
            {

            }
            if (row == null) return;
            ShippingBox shippingBox = row.Tag as ShippingBox;
            string pickTask = this.lblOutlog.Text.Trim();
            InventoryOutLogDetailInfo xjd = this.outLogList.FirstOrDefault(o => o.PICK_TASK == pickTask);
            DateTime shipDate = xjd.SHIP_DATE;

            string vsart = xjd.VSART;
            if (xjd.VSART == "")
            {
                List<InventoryOutLogDetailInfo> re = LocalDataService.GetDeliverInventoryOutLogDetailByPicktask(pickTask);
                if (re != null && re.Count > 0)
                {
                    vsart = re[0].VSART;
                }
            }
            //DateTime shipDate = this.outLogList.FirstOrDefault(o => o.PICK_TASK == pickTask).SHIP_DATE;
            switch (e.ColumnIndex)
            {
                case 4:
                    //当不是历史单据时才允许删除
                    if (!this.togHistory.Checked)
                    {
                        if (MetroMessageBox.Show(this, "确认删除?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                            DeleteBox(row);
                    }
                    break;
                case 5:
                    //明细
                    BoxDetailForm boxDetailForm = new BoxDetailForm(shippingBox);
                    boxDetailForm.ShowDialog();
                    break;
                case 6:
                    //打印
                    PrintForm printForm = new PrintForm(getDOCNO(), shipDate, shippingBox, mFYDT, vsart, this.togLocalprint.Checked, this.lblOutlog.Text);
                    printForm.ShowDialog();
                    //AutoPrintBox(shippingBox);
                    break;
                default:
                    break;
            }
        }

        /* 
        private void AutoPrintBox(DateTime shipDate, ShippingBox shippingBox)
        {
            //ShippingLabel label = LocalDataService.GetShippingLabelByShipDateAndStoreId(shippingBox.SHIP_DATE, shippingBox.PARTNER);
            ShippingLabel label = LocalDataService.GetShippingLabelByShipDateAndStoreId(shipDate, shippingBox.PARTNER);
            if (label == null)
            {
                MetroMessageBox.Show(this, "发运标签信息不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //是箱装还是挂装，需要根据设备号获取设备配置信息，取出相关字段进行判断，待开发
            if (SysConfig.DeviceInfo.KF_LX == "X")
            {
                //箱装
                if (shippingBox.IsFull == 1)
                {
                    //满箱，只打印发运标签
                    PrinterHelper.PrintShippingBox(SysConfig.PrinterName, label, shippingBox);

                    //满箱且存在多个下架单则询问是否打印开箱/拼箱标签
                    int num = shippingBox.Details != null ? shippingBox.Details.Select(o => o.PICK_TASK).Distinct().Count() : 0;
                    if (num > 1)
                    {
                        DialogResult dResult = MetroMessageBox.Show(this, "是否打印开箱/拼箱标签？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            PrinterHelper.PrintMixShippingBox(SysConfig.PrinterName, label, shippingBox);
                        }
                    }
                }
                else
                {
                    //未满箱时询问是否打印开箱/拼箱标签
                    DialogResult dResult = MetroMessageBox.Show(this, "是否打印开箱/拼箱标签？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        //打印开箱/拼箱标签
                        PrinterHelper.PrintMixShippingBox(SysConfig.PrinterName, label, shippingBox);
                    }
                }
            }
            else
            {
                //挂装
                if (shippingBox.IsFull == 1)
                {
                    //如果满箱
                    //只有一个下架单或多个下架单，都要打印发运标签
                    PrinterHelper.PrintShippingBox(SysConfig.PrinterName, label, shippingBox);

                    //存在多个下架单则必打印开箱/拼箱标签，第一个下架单为开箱，之后都为拼箱
                    int num = shippingBox.Details != null ? shippingBox.Details.Select(o => o.PICK_TASK).Distinct().Count() : 0;
                    if (num > 1)
                    {
                        PrinterHelper.PrintMixShippingBox(SysConfig.PrinterName, label, shippingBox);
                    }
                }
                else
                {
                    //非满箱，不打印发运标签，只打印拼箱标签
                    PrinterHelper.PrintMixShippingBox(SysConfig.PrinterName, label, shippingBox);
                }
            }

            //if (!result)
            //{
            //    MetroMessageBox.Show(this, "打印发运标签失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }
         */

        private void DeleteBox(DataGridViewRow row)
        {
            ShippingBox shippingBox = row.Tag as ShippingBox;
            if (shippingBox != null)
            {
                if (shippingBox.Details != null && shippingBox.Details.Count > 0)
                {
                    string pick_task = this.lblOutlog.Text.Trim();
                    string hu = shippingBox.HU;
                    foreach (ShippingBoxDetail item in shippingBox.Details)
                    {
                        //Id大于0且属于此下架单，则数量减掉
                        if (item.PICK_TASK == pick_task)
                        {
                            var inventoryOutLogDetail = outLogList.FirstOrDefault(o => o.PICK_TASK == pick_task && o.PICK_TASK_ITEM == item.PICK_TASK_ITEM);
                            if (inventoryOutLogDetail != null)
                            {
                                if (item.IsADD == 1)
                                {
                                    inventoryOutLogDetail.REALQTY_ADD = inventoryOutLogDetail.REALQTY_ADD - 1;
                                    shippingBox.AddQTY = shippingBox.AddQTY - 1;
                                    this.lblAuRealcount.Text = (int.Parse(this.lblAuRealcount.Text) - 1).ToString();
                                    this.lblAuDifferentcount.Text = (int.Parse(this.lblAuPlancount.Text) - int.Parse(this.lblAuRealcount.Text)).ToString();
                                }
                                else
                                {
                                    inventoryOutLogDetail.REALQTY = inventoryOutLogDetail.REALQTY - 1;
                                    shippingBox.QTY = shippingBox.QTY - 1;
                                    this.lblRealcount.Text = (int.Parse(this.lblRealcount.Text) - 1).ToString();
                                    this.lblDifferentcount.Text = (int.Parse(this.lblPlancount.Text) - int.Parse(this.lblRealcount.Text)).ToString();
                                }


                            }
                        }
                    }
                    shippingBox.Details.RemoveAll(i => i.PICK_TASK == pick_task);

                    this.epcList.RemoveAll(i => i.HU == shippingBox.HU && i.PICK_TASK == pick_task);
                    saveCurrentOutLogInfo();
                    LocalDataService.DeleteShippingBoxDetail(pick_task, hu, shippingBox.QTY, shippingBox.AddQTY);
                }

                if (shippingBox.IsScanBox == 1)
                {
                    this.lblCurrentcount.Text = "0";
                    this.lblCurrentcountBig.Text = "0";
                    this.currentNum = 0;
                    this.currentAddNum = 0;
                    this.currentScanBox = null;
                }
                grid.Rows.Remove(row);
                MetroMessageBox.Show(this, "删除成功", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnStopInventory_Click(object sender, EventArgs e)
        {
            FocusLastControl();
            if (this.btnInventory.Text == "停止扫描")
            {
                StopInventory();
            }
            else
            {
                StartInventory();
            }
        }

        private void btnGetBoxNo_Click(object sender, EventArgs e)
        {
            FocusLastControl();
            if (this.currentScanBox != null && (this.currentScanBox.Tag as ShippingBox).QTY == 0)
            {
                MetroMessageBox.Show(this, "当前箱子数量为0,请先装箱再获取新箱号", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (int.Parse(this.lblDifferentcount.Text) == 0)
            {
                MetroMessageBox.Show(this, "主条码差异为0,不允许获取新箱号", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //设置满箱
            setFullBox();
            //获取新箱号
            GetNewBox();
        }

        private void BtnSetFullBox_Click(object sender, EventArgs e)
        {
            FocusLastControl();
            BtnSetFullBox.Enabled = false;
            if (this.currentScanBox == null || (this.currentScanBox.Tag as ShippingBox).QTY == 0)
            {
                MetroMessageBox.Show(this, "当前箱子数量为0,不能设置为满箱", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BtnSetFullBox.Enabled = true;
                return;
            }
            string pick_task = lblOutlog.Text;
            /*
            -- add by jrzhuang 不需要判断是否一一对应配套，只需要所有标签SKU都属于本单就可以
            if (int.Parse(lblDifferentcount.Text) == 0)
            {
                
                // 判断主辅料是否配套
                Dictionary<string, int[]> checkdic = new Dictionary<string, int[]>();
                foreach (var item in tagList.FindAll(i => i.BARCD_ADD.Length > 0 || i.RFID_ADD_EPC.Length > 0))
                {
                    if (!checkdic.ContainsKey(item.MATNR))
                    {
                        checkdic.Add(item.MATNR, new int[2] { 0, 0 });
                    }
                    checkdic[item.MATNR][0] += epcList.FindAll(i => i.PICK_TASK == pick_task).FindAll(i => (i.IsRFID == 1 &&
                        i.IsADD == 0 && i.EPC.Substring(0, 14) == item.RFID_EPC.Substring(0, 14))
                        || (i.IsRFID == 0 &&
                        i.IsADD == 0 && i.EPC == item.BARCD)).Count;

                    checkdic[item.MATNR][1] += epcList.FindAll(i => i.PICK_TASK == pick_task).FindAll(i => (i.IsRFID == 1 &&
                     i.IsADD == 1 && item.RFID_ADD_EPC.Length >= 14 && i.EPC.Substring(0, 14) == item.RFID_ADD_EPC.Substring(0, 14))
                     || (i.IsRFID == 0 &&
                     i.IsADD == 1 && i.EPC == item.BARCD_ADD)).Count;
                }
                bool issuit = true;
                foreach (var item in checkdic)
                {
                    if (item.Value[0] != item.Value[1])
                    {
                        issuit = false;
                    }
                }
                if (!issuit)
                {
                    MetroMessageBox.Show(this, "当前下架单主辅料配套不一致,不能设置为满箱", "提示",
                         MessageBoxButtons.OK, MessageBoxIcon.Question);
                    BtnSetFullBox.Enabled = true;
                    return;
                }
                
            }
           */

            if ((this.currentScanBox.Tag as ShippingBox).IsFull == 1)
            {
                MetroMessageBox.Show(this, "该箱已经是满箱", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BtnSetFullBox.Enabled = true;
                return;
            }


            setFullBox();
            BtnSetFullBox.Enabled = true;
        }

        private void btnOutConfirm_Click(object sender, EventArgs e)
        {
            FocusLastControl();
            if (grid.Rows.Count <= 0)
                return;
            // 如果不是全部满箱并且是该门店最后一箱则提示
            if (int.Parse(lblNooutcount.Text) == 1)
            {
                bool allfull = true; // 全部满箱

                foreach (DataGridViewRow row in grid.Rows)
                {
                    if ((row.Tag as ShippingBox).IsFull == 0)
                    {
                        allfull = false;
                        break;
                    }
                }

                if (allfull == false)
                {
                    if (MetroMessageBox.Show(this, "当前下架单是此门店最后一单，未设置满箱是否继续？", "警告",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }
                }
            }

            this.btnOutConfirm.Enabled = false;

            //如果直接点击下架，就设置为满箱
            // setFullBox();
            string pick_task = this.lblOutlog.Text.Trim();


            //bool isAllFull = true;
            // 将没有满箱的数据保存
            saveCurrentOutLogInfo();
            foreach (DataGridViewRow row in grid.Rows)
            {
                if ((row.Tag as ShippingBox).IsFull == 0)
                {
                    ShippingBox box = row.Tag as ShippingBox;
                    LocalDataService.SaveShippingBox(box);
                }
            }
            //if (!isAllFull)
            //{
            //    MetroMessageBox.Show(this, "当前下架单中存在未满箱的箱子,不能下架", "提示");
            //    this.btnOutConfirm.Enabled = true;
            //    return;
            //}

            /* note by jrzhuang 不需要判断一一对应的配套关系
            // 判断主辅料是否配套
            Dictionary<string, int[]> checkdic = new Dictionary<string, int[]>();
            foreach (var item in tagList.FindAll(i => i.BARCD_ADD.Length > 0 || i.RFID_ADD_EPC.Length > 0))
            {
                if (!checkdic.ContainsKey(item.MATNR))
                {
                    checkdic.Add(item.MATNR, new int[2] { 0, 0 });
                }
                checkdic[item.MATNR][0] += epcList.FindAll(i => i.PICK_TASK == pick_task).FindAll(i => (i.IsRFID == 1 &&
                    i.IsADD == 0 && i.EPC.Substring(0, 14) == item.RFID_EPC.Substring(0, 14))
                    || (i.IsRFID == 0 &&
                    i.IsADD == 0 && i.EPC == item.BARCD)).Count;

                checkdic[item.MATNR][1] += epcList.FindAll(i => i.PICK_TASK == pick_task).FindAll(i => (i.IsRFID == 1 &&
                 i.IsADD == 1 && item.RFID_ADD_EPC.Length >= 14 && i.EPC.Substring(0, 14) == item.RFID_ADD_EPC.Substring(0, 14))
                 || (i.IsRFID == 0 &&
                 i.IsADD == 1 && i.EPC == item.BARCD_ADD)).Count;
            }
            bool issuit = true;
            foreach (var item in checkdic)
            {
                if (item.Value[0] != item.Value[1])
                {
                    issuit = false;
                }
            }
            if (!issuit)
            {
                MetroMessageBox.Show(this, "当前下架单主辅料配套不一致,不能下架", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Question);
                this.btnOutConfirm.Enabled = true;
                return;
            }
            */
            // 显示下架确认提示信息（货品数量按单位汇总，如1套）
            // 如果是差异为0不弹出，2015年11月22日hfz
            if (int.Parse(lblAuDifferentcount.Text) != 0 || int.Parse(lblDifferentcount.Text) != 0)
            {
                ConfirmOutLogForm dig = new ConfirmOutLogForm(lblStore.Text, this.outLogList.FindAll(i => i.PICK_TASK == this.lblOutlog.Text.Trim()), this.materialList);
                if (dig.ShowDialog() != DialogResult.OK)
                {
                    this.btnOutConfirm.Enabled = true;
                    return;
                }
            }

            //检查是否有差异
            if (this.lblDifferentcount.Text != "0" || this.lblAuDifferentcount.Text != "0")
            {
                OutConfirmForm form = new OutConfirmForm();
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    List<InventoryOutLogDetailInfo> currentOutlogList = this.outLogList.FindAll(i => i.PICK_TASK == pick_task);
                    if (currentOutlogList != null && currentOutlogList.Count > 0)
                    {
                        foreach (InventoryOutLogDetailInfo detail in currentOutlogList)
                        {

                            if (errorList.Exists(i => i.MATNR == detail.PRODUCTNO && i.ERRTYPE == ErrorType.少拣))
                            {
                                OutLogErrorRecord error = errorList.Find(i => i.MATNR == detail.PRODUCTNO && i.ERRTYPE == ErrorType.少拣);
                                error.QTY = error.QTY - (detail.QTY - detail.REALQTY);
                            }
                        }
                    }
                    //errorList.RemoveAll(r => r.ERRTYPE == ErrorType.少拣);
                }
                else
                {
                    ////这里点击取消才加入少拣错误记录
                    //List<InventoryOutLogDetailInfo> currentOutlogList = this.outLogList.FindAll(i => i.PICK_TASK == this.lblOutlog.Text);
                    //if (currentOutlogList != null && currentOutlogList.Count > 0)
                    //{
                    //    foreach (InventoryOutLogDetailInfo detail in currentOutlogList)
                    //    {
                    //        if (detail.QTY > detail.REALQTY)
                    //        {
                    //            string barcd = string.Empty;
                    //            if (tagList.Exists(i => i.MATNR == detail.PRODUCTNO))
                    //                barcd = tagList.Find(i => i.MATNR == detail.PRODUCTNO).BARCD;
                    //            InputErrorRecord(barcd, "", ErrorType.少拣, detail.PRODUCTNO, detail.PICK_TASK);
                    //        }
                    //    }
                    //}
                    this.btnOutConfirm.Enabled = true;
                    return;
                }
            }

            //停止扫描
            StopInventory();

            //下架确认
            string floor = this.txtFloor.Text.Trim();

            List<ShippingBox> boxList = new List<ShippingBox>();
            bool BoxIsPrintMergeTag = false;
            foreach (DataGridViewRow row in grid.Rows)
            {
                boxList.Add(row.Tag as ShippingBox);
                if (!BoxIsPrintMergeTag)
                {
                    BoxIsPrintMergeTag = CheckBoxIsPrintMergeTag(row.Tag as ShippingBox);
                }
            }

            // 异步上传信息
            string errorMsg = "";

            bool success = uploadServer.AsynUploadOutLogInfo(pick_task, floor, this.outLogList, this.errorList,
                boxList, this.epcList, this.togAutoprint.Checked, BoxIsPrintMergeTag, out errorMsg);
            // bool success = true;
            if (success)
            {
                // 打印标签
                InventoryOutLogDetailInfo xjd = this.outLogList.FirstOrDefault(o => o.PICK_TASK == pick_task);
                DateTime shipDate = xjd.SHIP_DATE;
                //DateTime shipDate = this.outLogList.FirstOrDefault(o => o.PICK_TASK == pick_task).SHIP_DATE;
                //打印货运标签
                foreach (ShippingBox box in boxList)
                {
                    if (this.togAutoprint.Checked && box.IsFull == 1)
                    {
                        continue;
                    }

                    string vsart = xjd.VSART;
                    if (xjd.VSART == "")
                    {
                        List<InventoryOutLogDetailInfo> re = LocalDataService.GetDeliverInventoryOutLogDetailByPicktask(pick_task);
                        if (re != null && re.Count > 0)
                        {
                            vsart = re[0].VSART;
                        }
                    }

                    if (!PrinterHelper.PrintTag(getDOCNO(), pick_task, shipDate, mFYDT, vsart, box, BoxIsPrintMergeTag, this.togLocalprint.Checked, out errorMsg))
                    {
                        MetroMessageBox.Show(this, errorMsg, "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                foreach (DataGridViewRow row in grid.Rows)
                {
                    ShippingBox box = row.Tag as ShippingBox;
                    //添加epc过滤
                    if (box != null && box.Details != null)
                    {
                        foreach (var item in box.Details)
                        {
                            if (item.IsRFID == 1)
                                historyEpcList.Add(item.EPC);
                        }
                    }
                }

                //如果下架确认成功，则初始化界面
                initView();
            }
            else
            {
                MetroMessageBox.Show(this, errorMsg, "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            this.txtOutlog.Focus();
            this.btnOutConfirm.Enabled = true;
        }


        private void btnTempSave_Click(object sender, EventArgs e)
        {
            FocusLastControl();
            StopInventory();
            saveCurrentOutLogInfo();
            saveCurrentBoxInfo();
            // MetroMessageBox.Show(this, "保存成功", "提示");
            // 中途暂存不要提示框，只要提示声音。
            AudioHelper.PlayWithSystem("Resources/notify.wav");
            initView();
            this.txtOutlog.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (true)
            //{
            //    saveCurrentOutLogInfo();
            //    saveCurrentBoxInfo();
            //} 
            StopInventory();
            if (uploadServer.CheckUndoTask())
            {
                // 此处到数据库检查是否还有未上传的信息，如果有，返回true，主界面则弹出对话框问是否显示
                if (MetroMessageBox.Show(this, "还有未上传的下架单，是否仍然退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    uploadServer.End();
                }
                else
                {
                    return;
                }
            }
            this.Close();
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {
            onBarcodeScan();
        }

        private void btnSetnoscanbox_Click(object sender, EventArgs e)
        {
            FocusLastControl();
            if (grid.SelectedRows.Count <= 0)
            {
                MetroMessageBox.Show(this, "未选择箱", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow row = grid.SelectedRows[0];
            if ((row.Tag as ShippingBox).IsScanBox == 1)
            {
                MetroMessageBox.Show(this, "该箱已是扫描箱", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetNoScanBox(row);
        }
        #endregion

        private void SetNoScanBox(DataGridViewRow row)
        {
            UpdateGridIsFull(row.Index, false);
            (row.Tag as ShippingBox).IsFull = 0;
            (row.Tag as ShippingBox).IsScanBox = 1;
            this.currentScanBox = row;
            this.currentHu = (row.Tag as ShippingBox).HU;
            this.currentNum = (row.Tag as ShippingBox).QTY;
            this.currentAddNum = (row.Tag as ShippingBox).AddQTY;
            this.lblCurrentcount.Text = this.currentNum.ToString();
            this.lblCurrentcountBig.Text = this.currentNum.ToString();
            //将界面中其它箱子设置为非扫描箱
            foreach (DataGridViewRow otherrow in grid.Rows)
            {
                if ((otherrow.Tag as ShippingBox).HU != (row.Tag as ShippingBox).HU)
                {
                    (otherrow.Tag as ShippingBox).IsScanBox = 0;
                }
            }
            LocalDataService.SetShippingBoxIsScanBox((row.Tag as ShippingBox).HU);
        }


        private void txtOutlog_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //回车
                loadInventoryOutLogInfo();
            }
        }

        private void cboBoxstyle_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FocusLastControl();
            if (this.togHistory.Checked)
            {
                //查询历史单号的状态
                if (this.grid.SelectedRows.Count > 0)
                {
                    (this.grid.SelectedRows[0].Tag as ShippingBox)
                        .PMAT_MATNR = cboBoxstyle.SelectedValue.ToString();
                    (this.grid.SelectedRows[0].Tag as ShippingBox)
                        .MAKTX = cboBoxstyle.Text;
                    this.grid.SelectedRows[0].Cells["boxstyle"].Value = cboBoxstyle.Text;
                    LocalDataService.SaveShippingBox(this.grid.SelectedRows[0].Tag as ShippingBox);
                }
            }
            else
            {
                if (currentScanBox != null)
                {
                    (currentScanBox.Tag as ShippingBox).PMAT_MATNR = cboBoxstyle.SelectedValue.ToString();
                    (currentScanBox.Tag as ShippingBox).MAKTX = cboBoxstyle.Text;

                    currentScanBox.Cells["boxstyle"].Value = (currentScanBox.Tag as ShippingBox).MAKTX;

                    if (!currentScanBox.Selected)
                        currentScanBox.Selected = true;
                    else
                    {
                        this.lblBoxstyle.Text = cboBoxstyle.Text;
                        this.lblBoxvalue.Text = cboBoxstyle.SelectedValue.ToString();
                    }
                }
            }
        }

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count <= 0)
                return;
            DataGridViewRow row = grid.SelectedRows[0];
            if (row.Tag as ShippingBox == null)
                return;
            this.lblBoxstyle.Text = (row.Tag as ShippingBox).MAKTX.ToString();
            this.lblBoxvalue.Text = (row.Tag as ShippingBox).PMAT_MATNR.ToString();
            //this.lblCurrentcount.Text = (row.Tag as ShippingBox).QTY.ToString();
            //this.lblCurrentcountBig.Text = (row.Tag as ShippingBox).QTY.ToString();
            this.lblIsScan.Text = (row.Tag as ShippingBox).IsScanBoxString;
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            if (this.lblCurrentcountBig.Visible)
                this.lblCurrentcountBig.Hide();
            else
                this.lblCurrentcountBig.Show();
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            //调用tablet pc输入面板
            System.Diagnostics.Process.Start("TabTip.exe");
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13)
                return;
            onBarcodeScan();
        }


        #region 私有函数
        /// <summary>
        /// 获取吊牌详细信息
        /// </summary>
        /// <param name="epc"></param>
        /// <returns></returns>
        private TagDetailInfo getTagDetailInfoByEpc(string data, bool isRFID)
        {
            if (data.Length < 1)
                return null;
            HLATagInfo tag = null;
            string rfidEpc = string.Empty;
            string rfidAddEpc = string.Empty;
            if (tagList == null)
                return null;
            if (isRFID)
            {
                if (string.IsNullOrEmpty(data) || data.Length < 20)
                    return null;

                rfidEpc = data.Substring(0, 14) + "000000";
                rfidAddEpc = rfidEpc.Substring(0, 14);
                List<HLATagInfo> tags = tagList.FindAll(i => i.RFID_EPC.ToUpper() == rfidEpc.ToUpper() ||
                    i.RFID_ADD_EPC.ToUpper() == rfidAddEpc.ToUpper());
                if (tags != null && tags.Count > 0)
                {
                    tag = tags.Exists(i => !string.IsNullOrEmpty(i.RFID_ADD_EPC)) ?
                        tags.FirstOrDefault(i => !string.IsNullOrEmpty(i.RFID_ADD_EPC)) :
                        tags[0];
                }
            }
            else
            {
                List<HLATagInfo> tags = tagList.FindAll(i => i.BARCD.ToUpper() == data.ToUpper() || (i.BARCD_ADD.ToUpper() == data.ToUpper()));
                if (tags != null && tags.Count > 0)
                {
                    tag = tags.Exists(i => !string.IsNullOrEmpty(i.BARCD_ADD)) ?
                        tags.FirstOrDefault(i => !string.IsNullOrEmpty(i.BARCD_ADD)) :
                        tags[0];
                }
            }

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
                    item.BARCD_ADD = tag.BARCD_ADD;
                    item.ZSATNR = mater.ZSATNR;
                    item.ZCOLSN = mater.ZCOLSN;
                    item.ZSIZTX = mater.ZSIZTX;
                    item.PXQTY = mater.PXQTY;

                    item.BRGEW = mater.BRGEW;
                    //判断是否为辅条码epc
                    if (isRFID)
                    {
                        if (rfidEpc == item.RFID_EPC)
                            item.IsAddEpc = false;
                        else
                            item.IsAddEpc = true;
                    }
                    else
                    {
                        if (data.ToUpper() == item.BARCD.ToUpper())
                            item.IsAddEpc = false;
                        else
                            item.IsAddEpc = true;
                    }
                    return item;
                }
            }
        }

        /// <summary>
        /// 服装条形码被扫描后
        /// </summary>
        private void onBarcodeScan()
        {
            FocusLastControl();
            string barcode = this.txtBarcode.Text.ToUpper();
            if (string.IsNullOrEmpty(barcode))
                return;
            this.txtBarcode.Text = "";
            CheckTag(barcode, false);
        }

        /// <summary>
        /// 获取新箱号
        /// </summary>
        private void GetNewBox()
        {

            ShippingBox shippingbox = new ShippingBox();
            shippingbox.HU = GetHU(this.cboBoxstyle.SelectedValue.ToString());
            shippingbox.Floor = SysConfig.DeviceInfo != null && SysConfig.DeviceInfo.LOUCENG != null ? SysConfig.DeviceInfo.LOUCENG : "";
            shippingbox.IsFull = 0;
            shippingbox.LGNUM = SysConfig.LGNUM;
            shippingbox.MAKTX = this.cboBoxstyle.Text;
            shippingbox.PARTNER = this.lblStore.Text;
            shippingbox.PMAT_MATNR = this.cboBoxstyle.SelectedValue.ToString();
            shippingbox.QTY = 0;
            if (outLogList != null && outLogList.Exists(i => i.PICK_TASK == this.lblOutlog.Text.Trim()))
                shippingbox.SHIP_DATE = outLogList.Find(i => i.PICK_TASK == this.lblOutlog.Text.Trim()).SHIP_DATE;
            else
                shippingbox.SHIP_DATE = new DateTime(1900, 1, 1);
            shippingbox.SKUCOUNT = 0;
            shippingbox.IsScanBox = 1;
            this.currentNum = 0;
            this.currentAddNum = 0;
            this.lblBoxstyle.Text = this.cboBoxstyle.Text;
            this.lblBoxvalue.Text = this.cboBoxstyle.SelectedValue.ToString();
            this.lblCurrentcount.Text = "0";
            this.lblCurrentcountBig.Text = "0";
            this.lblIsScan.Text = shippingbox.IsScanBoxString;
            this.currentHu = shippingbox.HU;
            if (grid.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in grid.Rows)
                {
                    if ((row.Tag as ShippingBox).IsScanBox == 1)
                    {
                        (row.Tag as ShippingBox).IsScanBox = 0;
                    }
                }
            }
            InsertGrid(shippingbox);

        }

        /// <summary>
        /// 选择未满箱的箱子后执行
        /// </summary>
        /// <param name="box"></param>
        private void onNotFullBoxSelect(List<ShippingBox> boxList)
        {
            if (boxList == null)
            {
                //用户未选择箱子，此时需要获取新箱号
                GetNewBox();
            }
            else
            {
                foreach (ShippingBox box in boxList)
                {
                    //if (box.IsScanBox == 1)
                    //{
                    this.lblCurrentcount.Text = box.QTY.ToString();
                    this.lblCurrentcountBig.Text = box.QTY.ToString();
                    this.currentNum = box.QTY;
                    this.currentAddNum = box.AddQTY;
                    this.currentHu = box.HU;
                    if (box.Details != null && box.Details.Count > 0)
                    {
                        if (!(box.Details.Select(o => o.PICK_TASK).Distinct().Count() == 1 && box.Details.First().PICK_TASK == lblOutlog.Text))
                        {
                            box.IsMargeHU = true;
                        }
                    }
                    box.IsScanBox = 1;
                    this.lblBoxstyle.Text = box.MAKTX;
                    this.lblBoxvalue.Text = box.PMAT_MATNR;
                    this.lblIsScan.Text = box.IsScanBoxString;
                    //}
                    if (box.Details != null)
                    {
                        if (epcList == null)
                            epcList = new List<ShippingBoxDetail>();
                        foreach (ShippingBoxDetail item in box.Details)
                        {
                            if (!epcList.Exists(i => i.Id == item.Id || i.EPC == item.EPC))
                            {
                                epcList.Add(item);
                            }
                        }
                    }
                    InsertGrid(box);
                }
                //选中下拉框
                for (int im = 0; im < cboBoxstyle.Items.Count; im++)
                {
                    if ((cboBoxstyle.Items[im] as DataRowView).Row.ItemArray[1].ToString().Equals((currentScanBox.Tag as ShippingBox).PMAT_MATNR))
                    {
                        cboBoxstyle.SelectedIndex = im;
                    }
                }
            }
        }

        private void loadInventoryOutLogInfo()
        {
            if (SysConfig.DeviceInfo.LGTYP.Count <= 0)
            {
                MetroMessageBox.Show(this, "请先选择存储类型，再扫描下架单号", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string pick_task = this.txtOutlog.Text.Trim();
            this.txtOutlog.Text = "";
            bool isLLP = false;
            if (IsExistNoOutLog())
            {
                return;
            }
            if (string.IsNullOrEmpty(pick_task.Trim()))
            {
                MetroMessageBox.Show(this, "对不起，下架单号不能为空", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (outLogList == null || !outLogList.Exists(i => i.PICK_TASK == pick_task))
                outLogList = LocalDataService.GetPartnerInventoryOutLogDetailList(pick_task);
            if (outLogList == null)
            {
                /*领路牌：则查询领路牌（领路牌+当前时间_发运单号,构造为流水号，根据流水号查询。且状态为0的数据，为9则为已提交的数据）*/
                //outLogList = LocalDataService.GetLLPInventoryOutLogDetailList(pick_task);
                outLogList = _services.GetLLPInventoryOutLogDetailList(pick_task);
                if (outLogList?.Count > 0)
                {
                    isLLP = true;
                }
            }
            if (outLogList == null)
                outLogList = new List<InventoryOutLogDetailInfo>();


            //MessageBox.Show(JsonConvert.SerializeObject(outLogList));
            outLogList.RemoveAll(i => !SysConfig.DeviceInfo.LGTYP.Contains(i.LGTYP_R));
            //MessageBox.Show(JsonConvert.SerializeObject(outLogList));

            if (!isLLP)
            {
                //非领路牌：根据下架单号获取下架单对应明细
                GetOutLogList(pick_task);
            }
            if (outLogList == null || outLogList.Count <= 0)
            {
                MetroMessageBox.Show(this, "未找到下架单", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                initView();
                return;
            }

            string pick_brand = outLogList[0].BRAND;
            string res_ = "";
            BoxStyleTable = SAPDataService.GetPackagingMaterialsList(SysConfig.LGNUM, SysConfig.DeviceInfo.LOUCENG, out res_, pick_brand);
            if (BoxStyleTable != null)
            {
                cboBoxstyle.DataSource = BoxStyleTable;
                cboBoxstyle.ValueMember = "PMAT_MATNR";
                cboBoxstyle.DisplayMember = "MAKTX";
            }

            //如果启用历史单号，需要判断该单号是否已下架
            if (this.togHistory.Checked)
            {
                if (outLogList.Find(i => i.PICK_TASK == pick_task).IsOut != 1)
                {
                    MetroMessageBox.Show(this, "此下架单尚未下架，无法查询", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    initView();
                    return;
                }
            }
            else
            {
                if (outLogList.Find(i => i.PICK_TASK == pick_task).IsOut == 1)
                {
                    MetroMessageBox.Show(this, "此下架单已下架", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    initView();
                    return;
                }
            }
            string zxjd_TYPE = outLogList.Find(i => i.PICK_TASK == pick_task).ZXJD_TYPE;
            if (zxjd_TYPE == "2")
            {
                lbIsRFID.Text = "RFID";
            }
            else if (zxjd_TYPE == "4")
            {
                //lbIsRFID.Text = "领路牌";
                MetroMessageBox.Show(this, "此单为三禾拼箱，请用SAP扫描出库", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                initView();
                return;
            }
            else
            {
                lbIsRFID.Text = "非RFID";

            }

            //获取该下架单号相关的物料信息
            materialList = LocalDataService.GetMaterialListByPickTask(pick_task);
            tagList = LocalDataService.GetTagListByPickTask(pick_task);
            //获取该下架单号已扫描过的满箱箱子列表
            List<ShippingBox> boxBefore;
            if (this.togHistory.Checked)
            {
                boxBefore = LocalDataService.GetShippingBoxListByPickTask(pick_task, 2);
            }
            else
            {
                boxBefore = LocalDataService.GetShippingBoxListByPickTask(pick_task, 1);
            }

            if (epcList == null)
                epcList = new List<ShippingBoxDetail>();


            errorList?.Clear();
            this.errorTag_List?.Clear();
            grid.Rows.Clear();


            if (boxBefore != null && boxBefore.Count > 0)
            {
                foreach (ShippingBox box in boxBefore)
                {
                    if (box.Details != null && box.Details.Count > 0)
                    {
                        if (!(box.Details.Select(o => o.PICK_TASK).Distinct().Count() == 1 && box.Details.First().PICK_TASK == lblOutlog.Text))
                        {
                            box.IsMargeHU = true;
                        }
                    }
                    InsertGrid(box);
                    epcList.AddRange(box.Details);
                }
            }
            DateTime outdate = outLogList.Find(i => i.PICK_TASK == pick_task).SHIP_DATE;

            this.lblOutlog.Text = pick_task;
            this.lblStore.Text = outLogList[0].PARTNER;

            //edit begin by wuxw on 2016.1.3  修改未下架单数和总数显示错误问题;‘拼’判断增加存储类型过滤
            var templist = this.outLogList.FindAll(i => i.PARTNER == this.lblStore.Text && i.IsOut == 0 && i.SHIP_DATE.Date == outdate.Date);
            //判断是否是拼箱PinXiang
            string pingXiang = "";
            if (templist != null)
            {
                foreach (InventoryOutLogDetailInfo item in templist)
                {
                    if (SysConfig.DeviceInfo.LGTYP != null && SysConfig.DeviceInfo.LGTYP.Contains(item.LGTYP_R)
                        && item.LGTYP != item.LGTYP_R)
                    {
                        pingXiang = "[拼]";
                        break;
                    }
                }
            }
            this.lblPinXiang.Text = pingXiang;
            int totalCount = 0;
            if (templist != null && templist.Count > 0)
            {
                List<string> pickTaskList = new List<string>();
                foreach (InventoryOutLogDetailInfo item in templist)
                {
                    if (!pickTaskList.Contains(item.PICK_TASK) && SysConfig.DeviceInfo.LGTYP != null && SysConfig.DeviceInfo.LGTYP.Contains(item.LGTYP_R))
                    {
                        int count = templist.FindAll(i => i.PICK_TASK == item.PICK_TASK).Sum(i => i.QTY);
                        totalCount = totalCount + count;
                        pickTaskList.Add(item.PICK_TASK);
                    }
                }
                this.lblNooutcount.Text = pickTaskList.Count.ToString();
                this.lblTotalcount.Text = totalCount.ToString();
            }
            else
            {
                this.lblNooutcount.Text = "0";
                this.lblTotalcount.Text = "0";
            }

            //this.lblNooutcount.Text = outLogList.FindAll(i => i.IsOut == 0 && i.SHIP_DATE.Date== outdate.Date).Distinct(new InventoryOutLogDetailComparer()).Count().ToString();
            //this.lblTotalcount.Text = outLogList.FindAll(i => i.IsOut == 0 && i.SHIP_DATE.Date == outdate.Date).Sum(i => i.QTY).ToString();
            //edit end by wuxw
            if (int.Parse(this.lblNooutcount.Text) == 1)
            {
                lblInfo.Text = "注意：本单是此门店最后一单";
            }
            else
            {
                lblInfo.Text = "";
            }
            string sPlanCount = outLogList.FindAll(i => i.PICK_TASK == pick_task).Sum(i => i.QTY).ToString();
            string sRealCount = outLogList.FindAll(i => i.PICK_TASK == pick_task).Sum(i => i.REALQTY).ToString();
            string sDifferentCount = (int.Parse(sPlanCount) - int.Parse(sRealCount)).ToString();

            this.lblPlancount.Text = sPlanCount;
            this.lblRealcount.Text = sRealCount;
            this.lblDifferentcount.Text = sDifferentCount;

            int auplancount = 0;
            foreach (var item in outLogList.FindAll(i => i.PICK_TASK == pick_task))
            {
                string addepc = null;
                var tags = tagList.FindAll(i => i.MATNR == item.PRODUCTNO);
                if (outLogList.Find(i => i.PICK_TASK == pick_task).ZXJD_TYPE == "2")
                {
                    if (tags != null && tags.Count > 0)
                        addepc = tags.Exists(x => !string.IsNullOrEmpty(x.RFID_ADD_EPC)) ?
                        tags.FirstOrDefault(x => !string.IsNullOrEmpty(x.RFID_ADD_EPC)).RFID_ADD_EPC :
                        tags[0].RFID_ADD_EPC;
                }
                else
                {

                    if (tags != null && tags.Count > 0)
                        addepc = tags.Exists(x => !string.IsNullOrEmpty(x.BARCD_ADD)) ?
                        tags.FirstOrDefault(x => !string.IsNullOrEmpty(x.BARCD_ADD)).BARCD_ADD :
                        tags[0].BARCD_ADD;
                }
                if (addepc != null && addepc.Length > 0)
                {
                    item.QTY_ADD = item.QTY;
                    auplancount += item.QTY;
                }
            }

            this.lblAuPlancount.Text = auplancount.ToString();
            this.lblAuRealcount.Text = outLogList.FindAll(i => i.PICK_TASK == pick_task).Sum(i => i.REALQTY_ADD).ToString();
            this.lblAuDifferentcount.Text = (int.Parse(lblAuPlancount.Text) - int.Parse(lblAuRealcount.Text)).ToString();


            //选中下拉框
            DataTable dt = BoxStyleTable;
            int iIndex = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sPMAT_MATNR = dt.Rows[i][1].ToString();//PMAT_MATNR
                if (mDefaultXiangXin.Contains(sPMAT_MATNR))
                {
                    iIndex = i;
                    cboBoxstyle.SelectedIndex = i;
                    continue;
                }
            }



            this.lblCurrentcount.Text = "0";
            if (!this.togHistory.Checked) //如果是禁用历史单号，则执行操作；启用历史单号只做查询
            {
                //判断当前楼层下，当前下架单所属的门店下有没有未设置满箱的箱子，如果有，则显示出来让用户选择
                List<ShippingBox> boxList = LocalDataService.GetShippingBoxList(outLogList[0].PARTNER,
                    this.txtFloor.Text.Trim(), outLogList.Find(i => i.PICK_TASK == pick_task).SHIP_DATE, 0);
                if (boxList != null && boxList.Count > 0)
                {
                    if (this.ignoreStoreList == null || !this.ignoreStoreList.Contains(this.lblStore.Text))
                    {
                        NotFullBoxForm form = new NotFullBoxForm(boxList);
                        form.onSelect = new NotFullBoxForm.DoSelect(onNotFullBoxSelect);
                        if (form.ShowDialog() == System.Windows.Forms.DialogResult.Ignore)
                        {
                            //如果用户选择清除提示，则后续该门店其他单据不再弹出本窗口提示。
                            if (this.ignoreStoreList == null)
                                this.ignoreStoreList = new List<string>();
                            if (!this.ignoreStoreList.Contains(this.lblStore.Text))
                            {
                                this.ignoreStoreList.Add(this.lblStore.Text);
                            }
                        }
                    }
                    else
                    {
                        GetNewBox();
                    }
                }
                else
                {
                    //List<ShippingBox> boxListAll = LocalDataService.GetShippingBoxList(outLogList[0].PARTNER, this.txtFloor.Text.Trim(), 2);
                    if (grid.Rows.Count == 0)
                    {
                        //如果没有未满箱的箱子，则定位到最后一箱
                        GetNewBox();
                    }
                    else
                    {
                        grid.Rows[grid.Rows.Count - 1].Selected = true;
                        SetNoScanBox(grid.Rows[grid.Rows.Count - 1]);
                    }

                }
                StartInventory();
            }
            FocusLastControl();
            #region 测试代码
            //CheckTag("50000355C50001000000", true);
            //CheckTag("50000355E50001000000", true);
            #endregion
        }
        /// <summary>
        /// 非领路牌：根据下架单号获取下架单对应明细
        /// </summary>
        /// <param name="pick_task"></param>
        private void GetOutLogList(string pick_task)
        {
            List<InventoryOutLogDetailInfo> list = SAPDataService.GetHLAShelvesSingleTask(SysConfig.LGNUM, pick_task.Trim());
            //MessageBox.Show(JsonConvert.SerializeObject(list));

            if (list != null) list.RemoveAll(i => !SysConfig.DeviceInfo.LGTYP.Contains(i.LGTYP_R));
            //MessageBox.Show(JsonConvert.SerializeObject(list));

            if (list != null && list.Count > 0)
            {
                foreach (InventoryOutLogDetailInfo item in list)
                {
                    InventoryOutLogDetailInfo itemex = outLogList.Find(i => i.PICK_TASK == item.PICK_TASK && i.PICK_TASK_ITEM == item.PICK_TASK_ITEM);
                    if (itemex == null)
                    {
                        new Thread(new ThreadStart(() =>
                        {
                            LocalDataService.SaveInventoryOutLogDetail(item);
                            List<MaterialInfo> mList = SAPDataService.GetMaterialInfoListByMATNR(SysConfig.LGNUM, item.PRODUCTNO);
                            List<HLATagInfo> tList = SAPDataService.GetHLATagInfoListByMATNR(SysConfig.LGNUM, item.PRODUCTNO);
                            foreach (MaterialInfo mitem in mList)
                            {
                                LocalDataService.SaveMaterialInfo(mitem);
                            }

                            foreach (HLATagInfo titem in tList)
                            {
                                LocalDataService.SaveTagInfo(titem);
                            }
                        })).Start();
                    }
                    outLogList.RemoveAll(i => i.PICK_TASK == item.PICK_TASK && i.PICK_TASK_ITEM == item.PICK_TASK_ITEM);
                    outLogList.Add(item);
                }
            }
        }

        /// <summary>
        /// 设置满箱
        /// </summary>
        private void setFullBox()
        {
            if (this.currentScanBox == null)
                return;
            if ((this.currentScanBox.Tag as ShippingBox).IsFull == 1)
            {
                MetroMessageBox.Show(this, "当前箱子已是满箱，请勿重复设置", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            saveCurrentOutLogInfo();
            //保存箱子数据
            (this.currentScanBox.Tag as ShippingBox).IsFull = 1;
            (this.currentScanBox.Tag as ShippingBox).IsScanBox = 0;
            UpdateGridIsFull(this.currentScanBox.Index, true);
            saveCurrentBoxInfo();


            //如果满自动打印有打，则直接打印货运标签
            if (this.togAutoprint.Checked)
            {
                string pick_task = this.lblOutlog.Text.Trim();
                InventoryOutLogDetailInfo xjd = this.outLogList.FirstOrDefault(o => o.PICK_TASK == pick_task);

                //DateTime shipDate = this.outLogList.FirstOrDefault(o => o.PICK_TASK == pick_task).SHIP_DATE;
                DateTime shipDate = xjd.SHIP_DATE;
                bool BoxIsPrintMergeTag = CheckBoxIsPrintMergeTag(this.currentScanBox.Tag as ShippingBox);
                string errorMsg = "";

                string vsart = xjd.VSART;
                if (xjd.VSART == "")
                {
                    List<InventoryOutLogDetailInfo> re = LocalDataService.GetDeliverInventoryOutLogDetailByPicktask(pick_task);
                    if (re != null && re.Count > 0)
                    {
                        vsart = re[0].VSART;
                    }
                }
                //打印货运标签
                if (!PrinterHelper.PrintTag(getDOCNO(), pick_task, shipDate, mFYDT, vsart, this.currentScanBox.Tag as ShippingBox, BoxIsPrintMergeTag, this.togLocalprint.Checked, out errorMsg))
                {
                    MetroMessageBox.Show(this, errorMsg, "错误",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            this.currentScanBox = null;
        }
        string getDOCNO()
        {
            string re = "";
            try
            {
                string pick_task = this.lblOutlog.Text.Trim();
                InventoryOutLogDetailInfo xjd = this.outLogList.FirstOrDefault(o => o.PICK_TASK == pick_task);
                if (xjd != null)
                {
                    re = xjd.DOCNO;
                }
                if (string.IsNullOrEmpty(re))
                {
                    List<InventoryOutLogDetailInfo> ins = LocalDataService.GetDeliverInventoryOutLogDetailByPicktask(pick_task);
                    if (ins != null && ins.Count > 0)
                    {
                        re = ins[0].DOCNO;
                    }

                }
            }
            catch (Exception)
            {

            }
            return re;
        }
        private bool CheckBoxIsPrintMergeTag(ShippingBox box)
        {
            if (SysConfig.DeviceInfo.KF_LX == "X")
            {
                //箱装
                if ((box as ShippingBox).IsFull == 1)
                {
                    //满箱且存在多个下架单则询问是否打印开箱/拼箱标签
                    int num = (box as ShippingBox).Details != null ? (box as ShippingBox).Details.Select(o => o.PICK_TASK).Distinct().Count() : 0;
                    if (num > 1)
                    {
                        DialogResult dResult = MetroMessageBox.Show(this, "是否打印开箱/拼箱标签？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    //未满箱时询问是否打印开箱/拼箱标签
                    DialogResult dResult = MetroMessageBox.Show(this, "是否打印开箱/拼箱标签？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        //打印开箱/拼箱标签
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 保存当前下架单的数据
        /// </summary>
        private void saveCurrentOutLogInfo()
        {
            if (outLogList == null)
                return;
            foreach (InventoryOutLogDetailInfo item in outLogList)
            {
                if (item.PICK_TASK == this.lblOutlog.Text.Trim())
                    //new Thread(new ThreadStart(() => {
                    LocalDataService.SaveInventoryOutLogDetail(item);
                //})).Start();

            }
        }

        /// <summary>
        /// 中途暂存当前扫描箱
        /// </summary>
        private void saveCurrentBoxInfo()
        {
            if (this.currentScanBox == null)
                return;
            DataGridViewRow row = this.currentScanBox;
            ShippingBox box = row.Tag as ShippingBox;
            if ((row.Tag as ShippingBox).Details != null)
            {
                foreach (var item in (row.Tag as ShippingBox).Details)
                {
                    if (item.IsRFID == 1)
                        historyEpcList.Add(item.EPC);
                }
            }
            //new Thread(new ThreadStart(() => {
            LocalDataService.SaveShippingBox(box);
            //})).Start();
        }

        /// <summary>
        /// 设置配置文件的值
        /// </summary>
        /// <param name="AppKey"></param>
        /// <param name="AppValue"></param>
        public static void SetConfigValue(string AppKey, string AppValue)
        {
            XmlDocument xDoc = new XmlDocument();
            //获取可执行文件的路径和名称
            xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            XmlNode xNode;
            XmlElement xElem1;
            xNode = xDoc.SelectSingleNode("//appSettings");
            xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + AppKey + "']");
            if (xElem1 != null) xElem1.SetAttribute("value", AppValue);
            else
            {
                xElem1 = xDoc.CreateElement("add");
                xElem1.SetAttribute("key", AppKey);
                xElem1.SetAttribute("value", AppValue);
                xNode.AppendChild(xElem1);
            }
            xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");
        }

        ErrorLogForm errorLoginForm;
        private List<string> ErrorEPCList = new List<string>();
        private object _objectid = new object();

        /// <summary>
        ///  错误提示信息
        /// </summary>
        /// <param name="sEPC"></param>
        /// <param name="dti"></param>
        /// <param name="iFlag">标识错误类别</param>
        private void ErrorEpcListWarning(string sEPC, TagDetailInfo dti, int iFlag, bool isrfid)
        {
            if (!string.IsNullOrEmpty(sEPC) && (DateTime.Now - this.lastWarningTime).TotalMilliseconds > 3000)
            {
                AudioHelper.PlayWithSystem("Resources/warningwav.wav");
                this.lastWarningTime = DateTime.Now;
            }
            if (iFlag == 0)//物料不存在
            {
                errorLoginForm.AsynAddCurrentErrorQueue(sEPC, lblOutlog.Text, dti, 0, outLogList, epcList, isrfid);
            }
            else if (iFlag == 1)
            {//物料多拣
                errorLoginForm.AsynAddCurrentErrorQueue(sEPC, lblOutlog.Text, dti, 1, outLogList, epcList, isrfid);
            }
           
        }

        private void ErrorLoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ErrorEPCList.Clear();//清空异常标签
        }


        /// <summary>
        /// 声音报警
        /// </summary>
        /// <param name="errMsg"></param>
        private void ErrorWarning(TagDetailInfo tag, ErrorType type, string epcOrBarcd, bool IsRFID)
        {
            if (IsRFID)
                LogHelper.WriteLine(string.Format("------{0}------", tag.EPC));
            if (tag != null)
                InputErrorRecord(tag.BARCD, tag.IsAddEpc ? tag.RFID_ADD_EPC : tag.RFID_EPC, type, tag.MATNR, this.lblOutlog.Text, 1);
            else
                InputErrorRecord(IsRFID ? "" : epcOrBarcd, IsRFID ? epcOrBarcd : "", type, "", this.lblOutlog.Text, 1);
        }

        /// <summary>
        /// 开始盘点
        /// </summary>
        private bool StartInventory()
        {
            //判断是否未盘点，未盘点则开始盘点
            if (this.isInventory == false)
            {
                this.isInventory = true;
                reader.StartInventory(mComPort, RadioOperationMode.Continuous, 1);
                this.btnInventory.Text = "停止扫描";
                this.txtBarcode.Focus();
                //ErrorWarning("");//清空异常信息 
            }

            return true;
        }

        /// <summary>
        /// 停止盘点
        /// </summary>
        private bool StopInventory()
        {
            //判断是否正在盘点，正在盘点则停止盘点
            if (this.isInventory == true)
            {
                this.isInventory = false;
                reader.StopInventory(mComPort);
                this.btnInventory.Text = "开始扫描";
            }

            return true;
        }
        /// <summary>
        /// 初始化界面
        /// </summary>
        private void initView()
        {
            lblOutlog.Text = "";
            lblStore.Text = "";
            lblPinXiang.Text = "";
            lblNooutcount.Text = "0";
            lblTotalcount.Text = "0";
            lblPlancount.Text = "0";
            lblRealcount.Text = "0";
            lblDifferentcount.Text = "0";
            lblCurrentcount.Text = "0";
            lblCurrentcountBig.Text = "0";
            lblAuDifferentcount.Text = "0";
            lblAuPlancount.Text = "0";
            lblAuRealcount.Text = "0";
            lblBoxstyle.Text = "";
            lblIsScan.Text = "";
            lblBoxvalue.Text = "";
            lblInfo.Text = "";
            grid.Rows.Clear();
            currentScanBox = null;
            currentNum = 0;
            currentAddNum = 0;
            materialList = null;
            tagList = null;
            errorList?.Clear();
            currentHu = string.Empty;
            epcList = null;
            this.errorTag_List?.Clear();

        }
        /// <summary>
        /// 获取箱码
        /// </summary>
        /// <param name="PMAT_MATNR"></param>
        /// <returns></returns>
        private string GetHU(string PMAT_MATNR)
        {
            return SAPDataService.GetBoxNo(SysConfig.LGNUM, PMAT_MATNR, 1);
        }

        /// <summary>
        /// 为表格插入数据
        /// </summary>
        /// <param name="box"></param>
        private void InsertGrid(ShippingBox box)
        {
            int no = this.grid.Rows.Count + 1;
            int result = grid.Rows.Add(no.ToString(), box.DisplayHU, box.QTY.ToString(), box.MAKTX, Resources.删除, Resources.明细, Resources.打印, box.IsFull == 1 ? Resources.满箱 : Resources.空箱);
            if (grid.Rows.Count > 0)
            {
                grid.Rows[no - 1].Tag = box;
                if (box.IsScanBox == 1)
                    this.currentScanBox = grid.Rows[no - 1];
                grid.Rows[no - 1].Selected = true;
            }
        }

        /// <summary>
        /// 更新表格件数数据
        /// </summary>
        /// <param name="rowindex"></param>
        /// <param name="qty"></param>
        private void UpdateGridQTY(int rowindex, int qty)
        {
            grid.Rows[rowindex].Cells["QTY"].Value = qty;
        }

        /// <summary>
        /// 更新表格中满箱状态
        /// </summary>
        /// <param name="rowindex"></param>
        /// <param name="isfull"></param>
        private void UpdateGridIsFull(int rowindex, bool isfull)
        {
            grid.Rows[rowindex].Cells["FullBox"].Value = isfull ? Resources.满箱 : Resources.空箱;
        }


        /// <summary>
        /// 新增拣错记录
        /// </summary>
        /// <param name="BARCD"></param>
        /// <param name="EPC"></param>
        /// <param name="ERRTYPE"></param>
        /// <param name="MATNR"></param>
        /// <param name="PICK_TASK"></param>
        private void InputErrorRecord(string BARCD, string EPC, ErrorType ERRTYPE, string MATNR, string PICK_TASK, int QTY)
        {
            OutLogErrorRecord error = new OutLogErrorRecord();
            error.BARCD = BARCD;
            error.EPC = EPC;
            error.ERRTYPE = ERRTYPE;
            error.MATNR = MATNR;
            error.PICK_TASK = PICK_TASK;
            error.QTY = QTY;
            if (errorList == null)
                errorList = new List<OutLogErrorRecord>();
            if (ERRTYPE == ErrorType.少拣 && !errorList.Exists(i => i.MATNR == MATNR))
            {
                errorList.Add(error);
                return;
            }
            if (!errorList.Exists(i => (!string.IsNullOrEmpty(EPC) ? i.EPC == EPC : i.BARCD == BARCD)))
            {
                errorList.Add(error);
                return;
            }
        }
        List<Error_Tag> errorTag_List = new List<Error_Tag>();
        /// <summary>
        /// 检测标签
        /// </summary>
        /// <param name="IsRFID">是否是RFID标签</param>
        private void CheckTag(string data, bool IsRFID)
        {
            if (epcList == null)
                epcList = new List<ShippingBoxDetail>();
            if (!this.epcList.Exists(i => i.EPC.ToUpper() == data.ToUpper()) && !this.historyEpcList.Contains(data) && IsRFID || !IsRFID)
            {
                //如果预计件数等于实际件数，则不再接收新的EPC，同样进行声音报警
                //通过epc查找详细信息，未找到即为非法
                TagDetailInfo tdi = getTagDetailInfoByEpc(data, IsRFID);
                if (tdi != null)
                {
                    if ((lblDifferentcount.Text == "0" && !tdi.IsAddEpc) || (lblAuDifferentcount.Text == "0" && tdi.IsAddEpc))
                    {
                        //ErrorWarning("数量已达标");
                        ErrorEpcListWarning(data, tdi, 1, IsRFID);
                        Error_Tag error = new Error_Tag() { tid = tdi, data = data, errotType = ErrorType.多拣, isRfid = IsRFID };
                        if (!errorTag_List.Contains(error))
                        {
                            errorTag_List.Add(error);
                        }
                        //ErrorWarning(tdi, ErrorType.多拣, data, IsRFID);
                        return;
                    }
                    string picktaskitem = string.Empty;
                    string uom = string.Empty;
                    if (outLogList != null && outLogList.Count > 0)
                    {
                        bool isCountMore = true;   //是否数量超标
                        foreach (InventoryOutLogDetailInfo item in outLogList)
                        {
                            if (item.PRODUCTNO == tdi.MATNR && item.PICK_TASK == this.lblOutlog.Text)
                            {
                                if (tdi.IsAddEpc)
                                {
                                    var mcountadd = epcList.FindAll(i => i.IsADD == 1).Count(i => i.PICK_TASK_ITEM == item.PICK_TASK_ITEM && i.PICK_TASK == item.PICK_TASK);
                                    if ((tdi.IsAddEpc && mcountadd >= item.QTY_ADD))
                                    {
                                        continue;
                                    }

                                    else
                                    {
                                        isCountMore = false;
                                        item.REALQTY_ADD++;
                                        picktaskitem = item.PICK_TASK_ITEM;
                                        uom = item.UOM;
                                    }
                                }
                                if (!tdi.IsAddEpc)
                                {
                                    var mcount = epcList.FindAll(i => i.IsADD == 0).Count(i => i.PICK_TASK_ITEM == item.PICK_TASK_ITEM && i.PICK_TASK == item.PICK_TASK);
                                    if ((!tdi.IsAddEpc && mcount >= item.QTY))
                                    {
                                        continue;
                                    }

                                    else
                                    {
                                        isCountMore = false;
                                        item.REALQTY++;
                                        picktaskitem = item.PICK_TASK_ITEM;
                                        uom = item.UOM;
                                        break;
                                    }
                                }
                            }
                        }
                        if (isCountMore)
                        {
                            //如果数量超标，则报警
                            ErrorEpcListWarning(data, tdi, 1, IsRFID);
                            Error_Tag error = new Error_Tag() { tid = tdi, data = data, errotType = ErrorType.多拣, isRfid = IsRFID };
                            if (!errorTag_List.Contains(error))
                            {
                                errorTag_List.Add(error);
                            }
                            //ErrorWarning(tdi, ErrorType.多拣, data, IsRFID);
                            return;
                        }
                    }

                    ShippingBoxDetail sbd = new ShippingBoxDetail();
                    sbd.ZSATNR = tdi.ZSATNR;
                    sbd.ZCOLSN = tdi.ZCOLSN;
                    sbd.ZSIZTX = tdi.ZSIZTX;
                    sbd.EPC = data;
                    sbd.HU = this.currentHu;
                    sbd.Id = 0;
                    sbd.PICK_TASK = this.lblOutlog.Text;
                    sbd.PICK_TASK_ITEM = picktaskitem;
                    sbd.CHARG = tdi.CHARG;
                    sbd.UOM = uom;
                    sbd.MATNR = tdi.MATNR;

                    sbd.BRGEW = tdi.BRGEW;
                    sbd.PACKMAT_FH = tdi.PACKMAT_FH;

                    if (tdi.IsAddEpc)
                        sbd.IsADD = 1;
                    else
                        sbd.IsADD = 0;
                    if (IsRFID)
                        sbd.IsRFID = 1;
                    else
                        sbd.IsRFID = 0;
                    if (epcList == null)
                        epcList = new List<ShippingBoxDetail>();
                    epcList.Add(sbd);
                    if ((this.currentScanBox.Tag as ShippingBox).Details == null)
                        (this.currentScanBox.Tag as ShippingBox).Details = new List<ShippingBoxDetail>();
                    (this.currentScanBox.Tag as ShippingBox).Details.Add(sbd);
                    if (sbd.IsADD == 0)
                        this.currentNum++;
                    if (sbd.IsADD == 1)
                        this.currentAddNum++;
                    this.Invoke(new Action(() =>
                    {
                        this.lblCurrentcountBig.Text = this.currentNum.ToString();
                        this.lblCurrentcount.Text = this.currentNum.ToString(); //更新扫描总数
                        if (sbd.IsADD == 1)
                            this.lblAuRealcount.Text = (int.Parse(this.lblAuRealcount.Text) + 1).ToString();
                        if (sbd.IsADD == 0)
                            this.lblRealcount.Text = (int.Parse(this.lblRealcount.Text) + 1).ToString(); ;    //更新实际件数

                        this.lblDifferentcount.Text = (int.Parse(this.lblPlancount.Text) - int.Parse(this.lblRealcount.Text)).ToString();   //更新差异数据
                        this.lblAuDifferentcount.Text = (int.Parse(this.lblAuPlancount.Text) - int.Parse(this.lblAuRealcount.Text)).ToString();
                        if (tdi.IsAddEpc)
                            (this.currentScanBox.Tag as ShippingBox).AddQTY++;
                        if (!tdi.IsAddEpc)
                            (this.currentScanBox.Tag as ShippingBox).QTY++;
                        UpdateGridQTY(this.currentScanBox.Index, this.currentNum);
                        if (errorLoginForm.isShow)
                            errorLoginForm.AsynAddCurrentErrorQueue(data, lblOutlog.Text, tdi, 3, outLogList, epcList, IsRFID);
                    }));

                    //ErrorWarning("");//清空异常信息 
                }
                else
                {
                    //有非法标签，需要用声音报警 
                    //ErrorWarning("不在本单"); 

                    ErrorEpcListWarning(data, tdi, 0, IsRFID);
                    Error_Tag error = new Error_Tag() { tid = tdi, data = data, errotType = ErrorType.拣错, isRfid = IsRFID };
                    if (!errorTag_List.Contains(error))
                    {
                        errorTag_List.Add(error);
                    }
                    //ErrorWarning(tdi, ErrorType.拣错, data, IsRFID);
                }
            }
        }

        #endregion


        private void ShowOutLogDetail()
        {
            if (string.IsNullOrEmpty(this.lblOutlog.Text.Trim()))
            {
                MetroMessageBox.Show(this, "请先扫描下架单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //OutLogDetailForm form = new OutLogDetailForm(this.outLogList.FindAll(i => i.PICK_TASK == this.lblOutlog.Text.Trim()), this.materialList);
            //form.ShowDialog();
            //下架单信息显示
            SingleShelvesForm singForm = new SingleShelvesForm(lblStore.Text, this.outLogList.FindAll(i => i.PICK_TASK == this.lblOutlog.Text.Trim()), this.materialList);
            singForm.setStatist += new SetStatist(
                                                    (paramater) =>
                                                    {
                                                        if (paramater && !errorList.Exists(a => a.ERRTYPE == ErrorType.少拣))
                                                        {
                                                            AddToLessScan();
                                                        }
                                                    }
                                                );

            singForm.ShowDialog();
        }
        private void AddToLessScan()
        {
            //这里点击取消才加入少拣错误记录
            List<InventoryOutLogDetailInfo> currentOutlogList = this.outLogList.FindAll(i => i.PICK_TASK == this.lblOutlog.Text);
            if (currentOutlogList != null && currentOutlogList.Count > 0)
            {
                foreach (InventoryOutLogDetailInfo detail in currentOutlogList)
                {
                    if (detail.QTY > detail.REALQTY)
                    {
                        string barcd = string.Empty;
                        if (tagList.Exists(i => i.MATNR == detail.PRODUCTNO))
                            barcd = tagList.Find(i => i.MATNR == detail.PRODUCTNO).BARCD;
                        InputErrorRecord(barcd, "", ErrorType.少拣, detail.PRODUCTNO, detail.PICK_TASK, detail.QTY - detail.REALQTY);
                    }
                }
            }
            MetroMessageBox.Show(this, "统计完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnImportBox_Click(object sender, EventArgs e)
        {
            string boxNo = this.txtImportBoxNo.Text.Trim();
            if (string.IsNullOrEmpty(boxNo))
            {
                MetroMessageBox.Show(this, "该导入箱箱号不能为空", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (outLogList == null || outLogList.Count == 0)
            {
                MetroMessageBox.Show(this, "未找到下架单", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //判断箱号是否存在列表中，存在则提示“箱号已存在列表当中”
            foreach (DataGridViewRow row in this.grid.Rows)
            {
                if (row.Cells["BoxNO"].Value.ToString() == boxNo)
                {
                    MetroMessageBox.Show(this, "该导入箱箱号已存在列表当中", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            ShippingBox box = LocalDataService.GetShippingBoxByHu(boxNo);
            if (box == null)
            {
                MetroMessageBox.Show(this, "该导入箱不存在", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //判断箱号所属门店是否为当前下架单的门店，不同则提示错误
            if (box.PARTNER != outLogList[0].PARTNER)
            {
                MetroMessageBox.Show(this, string.Format("该导入箱所属门店[{0}]与当前下架单所属门店[{1}]不一致", box.PARTNER, outLogList[0].PARTNER), "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (box.SHIP_DATE != outLogList[0].SHIP_DATE)
            {
                MetroMessageBox.Show(this, string.Format("该导入箱发运日期[{0}]与当前下架单发运日期[{1}]不一致", box.SHIP_DATE.ToString("yyyyMMdd"), outLogList[0].SHIP_DATE.ToString("yyyyMMdd")), "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (epcList == null)
                epcList = new List<ShippingBoxDetail>();
            epcList.AddRange(box.Details);

            //将该箱设置为扫描箱、非满箱，并插入表格当中
            box.IsFull = 0;
            box.IsScanBox = 1;
            this.currentNum = box.QTY;
            this.currentAddNum = box.AddQTY;
            this.lblBoxstyle.Text = box.MAKTX;
            this.lblBoxvalue.Text = box.PMAT_MATNR;
            this.lblCurrentcount.Text = box.QTY.ToString();
            this.lblCurrentcountBig.Text = box.QTY.ToString();
            this.lblIsScan.Text = box.IsScanBoxString;
            this.currentHu = box.HU;
            if (grid.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in grid.Rows)
                {
                    if ((row.Tag as ShippingBox).IsScanBox == 1)
                    {
                        (row.Tag as ShippingBox).IsScanBox = 0;
                    }
                }
            }

            InsertGrid(box);
            SetNoScanBox(grid.Rows[grid.Rows.Count - 1]);
            //开始扫描
            StartInventory();
        }

        private void togHistory_CheckedChanged(object sender, EventArgs e)
        {

            StopInventory();
            //清空界面
            initView();

            if (this.togHistory.Checked)
            {
                //启用历史单号，界面只作为查询功能，不能有任何业务操作
                this.btnImportBox.Enabled = false;
                this.btnInventory.Enabled = false;
                this.btnGetBoxNo.Enabled = false;
                this.BtnSetFullBox.Enabled = false;
                this.btnOutConfirm.Enabled = false;
                this.btnTempSave.Enabled = false;
                this.btnSetnoscanbox.Enabled = false;
                this.btnBarcode.Enabled = false;
            }
            else
            {
                //禁用历史单号，启用所有业务操作
                this.btnImportBox.Enabled = true;
                this.btnInventory.Enabled = true;
                this.btnGetBoxNo.Enabled = true;
                this.BtnSetFullBox.Enabled = true;
                this.btnOutConfirm.Enabled = true;
                this.btnTempSave.Enabled = true;
                this.btnSetnoscanbox.Enabled = true;
                this.btnBarcode.Enabled = true;
            }
            this.txtOutlog.Focus();
        }

        private void btnStoreOutLogDetail_Click(object sender, EventArgs e)
        {
            FocusLastControl();
            if (string.IsNullOrEmpty(this.lblStore.Text.Trim()))
            {
                MetroMessageBox.Show(this, "当前没有门店", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.outLogList != null)
            {
                DateTime outdate = outLogList.Find(i => i.PICK_TASK == this.lblOutlog.Text).SHIP_DATE;
                StoreNoOutLogForm form = new StoreNoOutLogForm(this.lblStore.Text.Trim(), this.outLogList.FindAll(i => i.PARTNER == this.lblStore.Text && i.IsOut == 0 && i.SHIP_DATE.Date == outdate.Date));
                form.ShowDialog();
            }
        }

        private void btnCheckBox_Click(object sender, EventArgs e)
        {
            if (this.isInventory)
                StopInventory();
            reader.RadioInventory -= new EventHandler<RadioInventoryEventArgs>(rfid_RadioInventory);
            BoxCheckForm form = new BoxCheckForm(this.reader, mComPort);
            form.ShowDialog();
            reader.RadioInventory += new EventHandler<RadioInventoryEventArgs>(rfid_RadioInventory);
        }

        private void btnUnUpload_Click(object sender, EventArgs e)
        {
            UploadMgForm form = new UploadMgForm();
            form.OnOutLogOpen += form_OnOutLogOpen;
            form.ShowDialog();
        }

        void form_OnOutLogOpen(string pick_task)
        {
            this.txtOutlog.Text = pick_task;
            loadInventoryOutLogInfo();
        }

        private void btnOutLogDetail_Click(object sender, EventArgs e)
        {
            ShowOutLogDetail();
        }
        public void rfid_RadioInventory(object sender, RadioInventoryEventArgs e)
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

            reader_OnTagReported(epc);
        }
        public bool ConnectReader()
        {
            bool re = true;
            try
            {
                if (reader.ConnectRadio(mComPort, 115200) == operateResult.ok)
                {
                    // 这里演示初始化参数
                    // 配置天线功率
                    AntennaPortConfiguration portConfig = new AntennaPortConfiguration();
                    portConfig.powerLevel = mPower * 10; // 23dbm
                    portConfig.numberInventoryCycles = 8192;
                    portConfig.dwellTime = 2000;
                    reader.SetAntennaPortConfiguration(mComPort, 0, portConfig);

                    reader.SetCurrentLinkProfile(mComPort, 1);

                    // 配置单化算法
                    SingulationAlgorithmParms singParm = new SingulationAlgorithmParms();
                    singParm.singulationAlgorithmType = SingulationAlgorithm.Dynamicq;
                    singParm.startQValue = 4;
                    singParm.minQValue = 0;
                    singParm.maxQValue = 15;
                    singParm.thresholdMultiplier = 4;
                    singParm.toggleTarget = 1;
                    reader.SetCurrentSingulationAlgorithm(mComPort, singParm);
                    reader.SetTagGroupSession(mComPort, Session.S0);

                }
                else
                {
                    re = false;
                }
            }
            catch (Exception)
            {
                re = false;
            }

            return re;
        }


        private void InventoryOutLogForm_Shown(object sender, EventArgs e)
        {
            mComPort = SysConfig.ReaderComPort;
            mPower = 0;
            uint.TryParse(SysConfig.ReaderPower, out mPower);

            #region 连接读写器
            reader.RadioInventory += new EventHandler<RadioInventoryEventArgs>(rfid_RadioInventory);

            if (!ConnectReader())
            {
                MetroMessageBox.Show(this, "连接读写器失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #endregion

            #region 加载箱型
            //BoxStyleTable = SAPDataService.GetPackagingMaterialsList("", "");

            //BoxStyleTable = SAPDataService.GetPackagingMaterialsList(SysConfig.LGNUM, SysConfig.DeviceInfo.LOUCENG, out res);
            //if (BoxStyleTable != null)
            //{
            //    cboBoxstyle.DataSource = BoxStyleTable;
            //    cboBoxstyle.ValueMember = "PMAT_MATNR";
            //    cboBoxstyle.DisplayMember = "MAKTX";
            //}


            #endregion

            #region 启动上传服务
            uploadServer.OnUploadSuccess += uploadServer_OnUploadSuccess;
            uploadServer.OnUploadCount += uploadServer_OnUploadCount;


            /*添加错误统计委托监听*/


            if (uploadServer.Start())
            {
                // 此处到数据库检查是否还有未上传的信息，如果有，返回true，主界面则弹出对话框问是否显示
                if (MetroMessageBox.Show(this, "还有未上传的下架单，是否立即显示？", "提示",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                    new UploadMgForm().ShowDialog();
            }
            #endregion

            this.txtOutlog.Focus();
        }
        void uploadServer_OnUploadCount(bool OneMore)
        {
            if (OneMore)
                btnUnUpload.DM_Color = Color.OrangeRed;
            else
                btnUnUpload.DM_Color = Color.FromArgb(0, 174, 219);
        }

        private void lbcurrentuser_Click(object sender, EventArgs e)
        {
            GxForm form = new GxForm();
            form.ShowDialog();
        }

        private void lblCurrentcountBig_Click(object sender, EventArgs e)
        {

        }

        private void txtPrinter_TextChanged(object sender, EventArgs e)
        {
            SysConfig.PrinterName = txtPrinter.Text.Trim();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            CheckTag(txtTestEpc.Text.Trim(), true);
        }

        private void togAutoprint_CheckedChanged(object sender, EventArgs e)
        {
            FocusLastControl();
        }

        private void togLocalprint_CheckedChanged(object sender, EventArgs e)
        {
            FocusLastControl();
        }

        private void grid_Click(object sender, EventArgs e)
        {
            FocusLastControl();
        }

        private void btnSetDefault_Click(object sender, EventArgs e)
        {
            mDefaultXiangXin = cboBoxstyle.SelectedValue.ToString();
            AudioHelper.PlayWithSystem("Resources/notify.wav");
        }
    }
}
