using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DMSkin;
using HLACommonLib;
using HLACommonLib.Model;
using HLATools.Utils;
using HLACommonLib.Model.ENUM;
using HLATools.Models;
using HLACommonLib.Model.PK;
using HLACommonView.Views.Dialogs;
using HLACommonLib.DAO;
using UARTRfidLink.Exparam;
using UARTRfidLink.Extend;

namespace HLATools
{
    public partial class MainForm : MetroForm
    {
        public RfidUARTLinkExtend reader = new RfidUARTLinkExtend();
        string mComPort;
        uint mPower;

        private Thread initThread = null;
        private bool isInventory = false;
        private Dictionary<string, MaterialInfo> materialList = new Dictionary<string, MaterialInfo>();
        private Dictionary<string, HLATagInfo> tagList = new Dictionary<string, HLATagInfo>();
        private List<string> epcList = new List<string>();
        private ReceiveType receiveType = ReceiveType.交货单收货;
        private ProcessDialog pb = new ProcessDialog();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.tabControl.SelectTab(0);
            cboType.SelectedIndex = 1;
            metroComboBox1_shouhuotype.SelectedIndex = 1;
            initThread = new Thread(new ThreadStart(() =>
            {
                bool retry = true;
                bool closed = false;
                while (retry)
                {
                    ShowLoading("正在下载物料主数据...");
                    materialList = LocalDataService.GetMaterialInfoDic();
                    if (materialList == null || materialList.Count <= 0)
                    {
                        this.Invoke(new Action(() =>
                        {
                            HideLoading();
                            if (MetroMessageBox.Show(this,
                            "未下载到物料主数据，请检查网络情况\r\n点击重试继续尝试下载",
                            "提示",
                            MessageBoxButtons.RetryCancel,
                            MessageBoxIcon.Information)
                            != System.Windows.Forms.DialogResult.Retry)
                            {
                                retry = false;
                                closed = true;
                                Close();
                            }
                        }));

                    }
                    else
                        retry = false;
                }
                if (closed)
                    return;
                retry = true;
                while (retry)
                {
                    ShowLoading("正在下载吊牌数据...");
                    tagList = LocalDataService.GetAllRfidHlaTagDic();

                    if (tagList == null || tagList.Count <= 0)
                    {
                        this.Invoke(new Action(() =>
                        {
                            HideLoading();
                            if (MetroMessageBox.Show(this,
                           "未下载到吊牌数据，请检查网络情况\r\n点击重试继续尝试下载",
                           "提示",
                           MessageBoxButtons.RetryCancel,
                           MessageBoxIcon.Information)
                           != System.Windows.Forms.DialogResult.Retry)
                            {
                                closed = true;
                                Close();
                            }
                        }));

                    }
                    else
                        retry = false;
                }
                if (closed)
                    return;
                //连接读写器
                ShowLoading("正在连接读写器...");
                this.Invoke(new Action(() =>
                {
                    mComPort = SysConfig.ReaderComPort;
                    mPower = 0;
                    uint.TryParse(SysConfig.ReaderPower, out mPower);

                    reader.RadioInventory += new EventHandler<RadioInventoryEventArgs>(rfid_RadioInventory);

                    if (!ConnectReader())
                    {
                        HideLoading();
                        MetroMessageBox.Show(this, "连接读写器失败,请检查设备是否连接", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        closed = true;
                    }

                    if (closed)
                        return;

                    StartInventory();
                }));
                HideLoading();
            }));
            initThread.IsBackground = true;
            initThread.Start();
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

        void reader_OnTagReported(string Epc)
        {
            if (!this.isInventory || string.IsNullOrEmpty(Epc) || epcList.Contains(Epc))
                return;

            epcList.Add(Epc);

            queryStoreDeliver(Epc);

            queryStore(Epc);

            TagHandler(Epc);
        }

        void queryStore(string epc)
        {
            try
            {
                if (tabControl.SelectedTab?.Text == "门店退货查询")
                {
                    TagDetailInfo tag = getTagDetailInfoByEpc(epc);
                    if (tag != null)
                    {
                        CStoreQuery sq = SAPDataService.getStoreFromSAP(tag.EPC);
                        metroGrid2.Rows.Insert(0, grid.Rows.Count, tag.EPC, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, tag.PUT_STRA, sq.barcd, sq.storeid, sq.hu, sq.pxqty_fh, sq.flag, sq.equip_hla, sq.loucheng, sq.date, sq.time);
                    }
                    else
                    {
                        metroGrid2.Rows.Insert(0, grid.Rows.Count, "EPC未注册", "EPC未注册", "EPC未注册", "EPC未注册", "EPC未注册", "EPC未注册", "EPC未注册", "EPC未注册", "EPC未注册", "EPC未注册");
                    }
                }
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(ex);
            }
        }

        void queryStoreDeliver(string epc)
        {
            if (tabControl.SelectedTab?.Text == "发货门店查询")
            {
                try
                {
                    CDeliverStoreQuery re = SAPDataService.getDeliverStore(epc);
                    metroGrid1.Rows.Add(re.store, re.mtr, re.bar, re.shipDate, re.hu, re.msg);
                }
                catch (Exception ex)
                {
                    Log4netHelper.LogError(ex);
                }
            }
        }

        private void TagHandler(string epc)
        {
            if (tabControl.SelectedTab.Name == "page1")
            {
                this.lblCount.Text = epcList.Count.ToString();
                TagDetailInfo tdi = getTagDetailInfoByEpc(epc);
                AddGridRow(tdi, epc);
            }
            else
            {
                //page2
                if (grid2.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in grid2.Rows)
                    {
                        if (row.Cells["EPC"].Value.ToString() == epc)
                        {
                            grid2.Rows.Remove(row);
                            grid2.Rows.Insert(0, row);
                            row.Selected = true;
                            row.Selected = false;
                            row.Cells["IsChecked"].Value = "是";
                            row.DefaultCellStyle.BackColor = Color.OrangeRed;
                            break;
                        }
                    }
                }
            }
        }

        private void AddGridRow(TagDetailInfo tdi,string epc)
        {
            int index = 0;
            int count = 0;
            if (tdi != null)
            {
                
                if (grid.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in grid.Rows)
                    {
                        if (row.Tag != null && (row.Tag as TagDetailInfo).MATNR == tdi.MATNR)
                        {
                            index++;
                            count++;
                        }
                        else
                        {
                            if (count > 0)
                                break;
                            else
                                index++;
                        }
                    }
                }

                grid.Rows.Insert(index,count+1, tdi.ZSATNR, tdi.ZCOLSN, tdi.ZSIZTX, epc, "");
                grid.Rows[index].Tag = tdi;
            }
            else
            {
                if (grid.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in grid.Rows)
                    {
                        if (row.Tag == null)
                        {
                            index++;
                            count++;
                            //row.Cells["QTY"].Value = int.Parse(row.Cells["QTY"].Value.ToString()) + 1;
                            //exist = true;
                        }
                        else
                        {
                            if (count > 0)
                                break;
                            else
                                index++;
                        }
                    }
                }
                grid.Rows.Insert(index, count + 1, "", "", "", epc, "EPC未注册");
                grid.Rows[index].Tag = tdi;
            }
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
            HLATagInfo tag = null;
            if (tagList.ContainsKey(rfidEpc))
                tag = tagList[rfidEpc];
            if (tag == null && tagList.ContainsKey(rfidAddEpc))
                tag = tagList[rfidAddEpc];
            if (tag == null)
                return null;
            else
            {
                MaterialInfo mater = materialList.ContainsKey(tag.MATNR) ? materialList[tag.MATNR] : null;
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
                    item.ZSATNR = mater.ZSATNR;
                    item.ZCOLSN = mater.ZCOLSN;
                    item.ZSIZTX = mater.ZSIZTX;
                    item.PXQTY = mater.PXQTY;
                    item.EPC = epc;
                    //判断是否为辅条码epc
                    if (rfidEpc == item.RFID_EPC)
                        item.IsAddEpc = false;
                    else
                        item.IsAddEpc = true;
                    return item;
                }
            }
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
            }
            return true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //如果仍在盘点，则关闭盘点
            if (this.isInventory)
                StopInventory();

            //关闭读写器连接
            reader.DisconnectRadio(mComPort);

        }

        private void txtHU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            { 
                //回车
                queryHuErrorInfo();
            }

        }

        private void ShowLoading(string message)
        {
            Invoke(new Action(() => {
                pb.Show();
                panelLoading.Show();
                lblLoading.Text = message;
            }));
        }

        private void HideLoading()
        {
            Invoke(new Action(() => {
                pb.Hide();
                panelLoading.Hide();
                lblLoading.Text = "";
            }));
        }

        private void queryHuErrorInfo()
        {
            if (string.IsNullOrWhiteSpace(this.txtHU.Text))
            {
                MetroMessageBox.Show(this, "请输入箱码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            receiveType = (ReceiveType)Enum.Parse(typeof(ReceiveType), cboType.Text);
            this.txtHU.Enabled = false;
            this.btnBarcode.Enabled = false;
            grid2.Rows.Clear();
            this.panelLoading.Show();
            this.lblLoading.Text = "正在加载箱异常数据...";
            Thread thread = new Thread(new ThreadStart(() => {
                //加载当前箱码下EPC未注册和商品已扫描的标签
                string hu = txtHU.Text.Trim();
                if (string.IsNullOrEmpty(hu))
                    return;
                DataTable dtError = LocalDataService.GetErrorEpcByHU(hu, receiveType);
                DataTable all = LocalDataService.GetAllDistinctEpcByHU(hu, receiveType);
                
                foreach (DataRow row in dtError.Rows)
                {
                    //加载错误为商品已扫描的epc
                    string epc = row["EPC_SER"].ToString();
                    TagDetailInfo tdi = getTagDetailInfoByEpc(epc);
                    this.Invoke(new Action(() => {
                        if (tdi == null)
                        {

                            grid2.Rows.Insert(0,
                                row["EPC_SER"].ToString(),
                                "", "", "", row["HU"].ToString(), "不在本单;商品已扫描","否");
                        }
                        else
                        {
                            grid2.Rows.Insert(0,
                                row["EPC_SER"].ToString(),
                                tdi.ZSATNR, tdi.ZCOLSN, tdi.ZSIZTX, row["HU"].ToString(), "商品已扫描","否");
                        }
                    }));
                   
                }

                foreach (DataRow row in all.Rows)
                {
                    string epc = row["EPC_SER"].ToString();
                    TagDetailInfo tdi = getTagDetailInfoByEpc(epc);
                    if (tdi == null)
                    {
                        //不在本单
                        bool exist = false;
                        foreach (DataGridViewRow dgvrow in grid2.Rows)
                        {
                            if (dgvrow.Cells["EPC"].Value.ToString().Trim() == epc)
                            {
                                exist = true;
                            }
                        }
                        if (!exist)
                        {
                            this.Invoke(new Action(() =>
                            {
                                grid2.Rows.Insert(0,
                                row["EPC_SER"].ToString(),
                                "", "", "", row["HU"].ToString(), "不在本单","否");
                            }));
                        }
                    }
                }

                this.Invoke(new Action(() =>
                {
                    this.txtHU.Enabled = true;
                    this.btnBarcode.Enabled = true;
                    this.txtHU.Focus();
                    this.panelLoading.Hide();
                    this.lblLoading.Text = "";
                    if (grid2.Rows.Count <= 0)
                    {
                        MetroMessageBox.Show(this, "当前箱没有异常信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        UnselectGrid2Rows();
                    }
                }));
            }));
            thread.IsBackground = true;
            thread.Start();
        }

        private void UnselectGrid2Rows()
        {
            foreach (DataGridViewRow row in grid2.Rows)
                row.Selected = false;
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {
            queryHuErrorInfo();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Name == "page1")
            {
                MetroMessageBox.Show(this, "page1", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MetroMessageBox.Show(this, "page2", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Name == "page2")
                this.txtHU.Focus();
            else if (tabControl.SelectedTab.Name == "page3")
                this.txtShortHU.Focus();
        }
        

        private void grid2_SelectionChanged(object sender, EventArgs e)
        {
            UnselectGrid2Rows();
        }

        private void txtShortHU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //回车
                
            }
        }

        private void btnQueryShortPick_Click(object sender, EventArgs e)
        {
            //清空列表
            this.Invoke(new Action(() => {
                gridShort.Rows.Clear();
            }));

            if(string.IsNullOrWhiteSpace(txtShortHU.Text.Trim()))
            {
                MetroMessageBox.Show(this, "箱码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Thread thread = new Thread(new ThreadStart(() => {
                try
                {
                    ShowLoading("正在下载短拣信息...");
                    DataSet ds = LocalDataService.GetShortPickHuInfo(txtShortHU.Text.Trim());
                    if (ds != null && ds.Tables.Count == 2)
                    {
                        DataTable dt1 = ds.Tables[0];
                        DataTable dt2 = ds.Tables[1];
                        List<ShortPickBoxInfo> list = new List<ShortPickBoxInfo>();
                        if (dt2.Rows.Count <= 0 || dt1.Rows.Count <= 0)
                        {
                            HideLoading();
                            Invoke(new Action(() =>
                            {
                                MetroMessageBox.Show(this, "该箱没有短拣信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }));
                            
                            return;
                        }
                        
                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt1.Rows)
                            {
                                ShortPickBoxInfo item = new ShortPickBoxInfo();
                                item.HU = row["HU"] != null ? row["HU"].ToString() : "";
                                item.PICK_TASK = row["PICK_TASK"] != null ? row["PICK_TASK"].ToString() : "";
                                item.PICK_TASK_ITEM = row["PICK_TASK_ITEM"] != null ? row["PICK_TASK_ITEM"].ToString() : "";
                                item.MATNR = row["PRODUCTNO"] != null ? row["PRODUCTNO"].ToString() : "";
                                item.QTY = row["QTY"] != null ? int.Parse(row["QTY"].ToString()) : 0;
                                var temp = list.FirstOrDefault(o => o.PICK_TASK == item.PICK_TASK);
                                if (temp == null)
                                    item.ISLAST = LocalDataService.CountUnScanDeliverBox(item.PICK_TASK) == 1 ? "是" : "否";
                                else
                                    item.ISLAST = temp.ISLAST;
                                MaterialInfo mi = materialList.ContainsKey(item.MATNR) ? materialList[item.MATNR] : null;
                                item.ZCOLSN = mi != null ? mi.ZCOLSN : "";
                                item.ZSATNR = mi != null ? mi.ZSATNR : "";
                                item.ZSIZTX = mi != null ? mi.ZSIZTX : "";

                                list.Add(item);
                            }
                        }
                        //start edit by wuxw 统计实发数量，一箱存在多下架单，且不同下架单有相同MATNR，先填满尾箱，再填非尾箱
                        list = list.OrderByDescending(o => o.ISLAST).ThenBy(o => o.PICK_TASK).ThenBy(o => o.MATNR).ThenBy(o => o.QTY).ToList();
                        if (dt2.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt2.Rows)
                            {
                                string matnr = row["MATNR"] != null ? row["MATNR"].ToString() : "";
                                int isAdd = row["IsAdd"] != null ? int.Parse(row["IsAdd"].ToString()) : 0;
                                if (isAdd != 1)
                                {
                                    //判断所属产品编码是否有属于尾箱且实发数量小于应发数量的记录
                                    var item = list.FirstOrDefault(o => o.MATNR == matnr && o.ISLAST == "是" && o.RQTY < o.QTY);
                                    if (item != null)
                                    {
                                        item.RQTY++;
                                        if (string.IsNullOrEmpty(item.ZCOLSN))
                                        {
                                            item.ZCOLSN = row["ZCOLSN"] != null ? row["ZCOLSN"].ToString() : "";
                                            item.ZSATNR = row["ZSATNR"] != null ? row["ZSATNR"].ToString() : "";
                                            item.ZSIZTX = row["ZSIZTX"] != null ? row["ZSIZTX"].ToString() : "";
                                        }
                                    }
                                    else
                                    {
                                        //判断所属产品编码是否有不是尾箱且实发数量小于应发数量的记录
                                        var tItem = list.FirstOrDefault(o => o.MATNR == matnr && o.ISLAST == "否" && o.RQTY < o.QTY);
                                        if(tItem != null)
                                        {
                                            tItem.RQTY++;
                                            if (string.IsNullOrEmpty(tItem.ZCOLSN))
                                            {
                                                tItem.ZCOLSN = row["ZCOLSN"] != null ? row["ZCOLSN"].ToString() : "";
                                                tItem.ZSATNR = row["ZSATNR"] != null ? row["ZSATNR"].ToString() : "";
                                                tItem.ZSIZTX = row["ZSIZTX"] != null ? row["ZSIZTX"].ToString() : "";
                                            }
                                        }
                                        else
                                        {
                                            //获取所属产品编码最后一条记录，看是否存在
                                            var lastItem = list.LastOrDefault(o => o.MATNR == matnr);
                                            if(lastItem != null)
                                            {
                                                lastItem.RQTY++;
                                                if (string.IsNullOrEmpty(lastItem.ZCOLSN))
                                                {
                                                    lastItem.ZCOLSN = row["ZCOLSN"] != null ? row["ZCOLSN"].ToString() : "";
                                                    lastItem.ZSATNR = row["ZSATNR"] != null ? row["ZSATNR"].ToString() : "";
                                                    lastItem.ZSIZTX = row["ZSIZTX"] != null ? row["ZSIZTX"].ToString() : "";
                                                }
                                            }
                                            else
                                            {
                                                //找不到任何数据，本身就有问题，不应该短拣，不显示
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //判断所属产品编码是否有属于尾箱且实发数量小于应发数量的记录
                                    var item = list.FirstOrDefault(o => o.MATNR == matnr && o.ISLAST == "是" && o.Add_RQTY < o.QTY);
                                    if (item != null)
                                    {
                                        item.Add_RQTY++;
                                        if (string.IsNullOrEmpty(item.ZCOLSN))
                                        {
                                            item.ZCOLSN = row["ZCOLSN"] != null ? row["ZCOLSN"].ToString() : "";
                                            item.ZSATNR = row["ZSATNR"] != null ? row["ZSATNR"].ToString() : "";
                                            item.ZSIZTX = row["ZSIZTX"] != null ? row["ZSIZTX"].ToString() : "";
                                        }
                                    }
                                    else
                                    {
                                        //判断所属产品编码是否有不是尾箱且实发数量小于应发数量的记录
                                        var tItem = list.FirstOrDefault(o => o.MATNR == matnr && o.ISLAST == "否" && o.Add_RQTY < o.QTY);
                                        if (tItem != null)
                                        {
                                            tItem.Add_RQTY++;
                                            if (string.IsNullOrEmpty(tItem.ZCOLSN))
                                            {
                                                tItem.ZCOLSN = row["ZCOLSN"] != null ? row["ZCOLSN"].ToString() : "";
                                                tItem.ZSATNR = row["ZSATNR"] != null ? row["ZSATNR"].ToString() : "";
                                                tItem.ZSIZTX = row["ZSIZTX"] != null ? row["ZSIZTX"].ToString() : "";
                                            }
                                        }
                                        else
                                        {
                                            //获取所属产品编码最后一条记录，看是否存在
                                            var lastItem = list.LastOrDefault(o => o.MATNR == matnr);
                                            if (lastItem != null)
                                            {
                                                lastItem.Add_RQTY++;
                                                if (string.IsNullOrEmpty(lastItem.ZCOLSN))
                                                {
                                                    lastItem.ZCOLSN = row["ZCOLSN"] != null ? row["ZCOLSN"].ToString() : "";
                                                    lastItem.ZSATNR = row["ZSATNR"] != null ? row["ZSATNR"].ToString() : "";
                                                    lastItem.ZSIZTX = row["ZSIZTX"] != null ? row["ZSIZTX"].ToString() : "";
                                                }
                                            }
                                            else
                                            {
                                                //找不到任何数据，本身就有问题，不应该短拣，不显示
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        //判断主副条码是否一致
                        if(list.Count(o => o.Add_RQTY > 0 && o.RQTY != o.Add_RQTY) > 0)
                        {
                            HideLoading();
                            Invoke(new Action(() =>
                            {
                                MetroMessageBox.Show(this, "主辅条码不一致，不允许短拣", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }));

                            return;
                        }
                        //end edit

                        int faCount = 0;
                        int saoCount = 0;

                        foreach (ShortPickBoxInfo item in list)
                        {
                            //实发数量和短拣数量都不为0的不显示
                            if (item.RQTY == 0 && item.SHORTQTY == 0)
                                continue;

                            faCount += item.QTY;
                            saoCount += item.RQTY;

                            AddShortPickBoxGrid(item);
                        }

                        Invoke(new Action(() =>
                        {
                            faTotalBoxNum.Text = faCount.ToString();
                            saoTotalNum.Text = saoCount.ToString();
                        }));

                    }
                    else
                    {
                        HideLoading();
                        Invoke(new Action(() => {
                            MetroMessageBox.Show(this, "该箱没有短拣信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }));
                    }
                }
                catch(Exception ex)
                {
                    LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                }
                HideLoading();
            }));
            thread.IsBackground = true;
            thread.Start();
        }

        private void AddShortPickBoxGrid(ShortPickBoxInfo item)
        {
            Invoke(new Action(() => {
                if(item.SHORTQTY>0)
                {
                    gridShort.Rows.Insert(0, item.HU, item.PICK_TASK, item.ZSATNR, item.ZCOLSN, item.ZSIZTX, item.QTY, item.RQTY, item.SHORTQTY, item.ISLAST);
                    gridShort.Rows[0].Tag = item;

                    gridShort.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;

                }
                else
                {
                    gridShort.Rows.Add(item.HU, item.PICK_TASK, item.ZSATNR, item.ZCOLSN, item.ZSIZTX, item.QTY, item.RQTY, item.SHORTQTY, item.ISLAST);
                    gridShort.Rows[gridShort.Rows.Count-1].Tag = item;

                }

            }));
        }

        private void btnShortConfirm_Click(object sender, EventArgs e)
        {
            if (gridShort.Rows.Count <= 0) return;
            //登录验证
            ShortConfirmForm form = new ShortConfirmForm();
            if(form.ShowDialog() == DialogResult.OK)
            {
                List<PKDeliverBoxShortPickDetailInfo> shortList = new List<PKDeliverBoxShortPickDetailInfo>();
                foreach (DataGridViewRow row in gridShort.Rows)
                {
                    PKDeliverBoxShortPickDetailInfo item = new PKDeliverBoxShortPickDetailInfo();
                    item.HU = (row.Tag as ShortPickBoxInfo).HU;
                    item.LGNUM = SysConfig.LGNUM;
                    item.PICK_TASK = (row.Tag as ShortPickBoxInfo).PICK_TASK;
                    item.PICK_TASK_ITEM = (row.Tag as ShortPickBoxInfo).PICK_TASK_ITEM;
                    item.MATNR = (row.Tag as ShortPickBoxInfo).MATNR;
                    item.QTY = (row.Tag as ShortPickBoxInfo).RQTY;
                    item.DJQTY = (row.Tag as ShortPickBoxInfo).SHORTQTY;
                    if (item.QTY == 0 && item.DJQTY == 0)
                        continue;

                    shortList.Add(item);
                }
                if (LocalDataService.SaveShortPickDetail(shortList))
                {
                    MetroMessageBox.Show(this, "短拣成功，请重新投放通道机检测", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //清空列表
                    this.Invoke(new Action(() => {
                        gridShort.Rows.Clear();
                    }));
                }
                else
                {
                    MetroMessageBox.Show(this, "短拣失败，可能是网络不稳定，请稍候重试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnQueryDeliverBox_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDeliverHu.Text.Trim()))
            {
                MetroMessageBox.Show(this, "箱码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            gridDeliverErrorBox.Rows.Clear();
            List<PKDeliverErrorBox> box = PKDeliverService.GetDeliverErrorBoxByHu(txtDeliverHu.Text.Trim());
            if(box?.Count>0)
            {
                foreach(PKDeliverErrorBox item in box)
                {
                    gridDeliverErrorBox.Rows.Add(item.PARTNER, item.HU, item.PICK_TASK, item.ZSATNR, item.ZCOLSN, item.ZSIZTX, item.REAL, item.DIFF, item.REMARK);
                }
            }
            else
            {
                MetroMessageBox.Show(this, "未找到本箱信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.lblCount.Text = "0";
            this.grid.Rows.Clear();
            this.epcList.Clear();
        }

        private void metroButton1_shouhuo_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(metroTextBox1_shouhuoepc.Text))
            {
                MessageBox.Show("请输入EPC");
                return;
            }
            DataTable dt = LocalDataService.getInfoFromEpc(metroTextBox1_shouhuoepc.Text.Trim(), metroComboBox1_shouhuotype.SelectedIndex == 1);
            if(dt!=null && dt.Rows.Count>0)
            {
                metroGrid1_shouhuo.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    string hu = row["HU"].ToString();
                    string t = row["Timestamp"].ToString();
                    metroGrid1_shouhuo.Rows.Insert(0, metroTextBox1_shouhuoepc.Text.Trim()
                        , hu, t);

                }
            }
            else
            {
                MessageBox.Show("没有找到该EPC记录");
            }
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            this.lblCount.Text = "0";
            metroGrid1.Rows.Clear();
            this.epcList.Clear();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.lblCount.Text = "0";
            metroGrid2.Rows.Clear();
            this.epcList.Clear();
        }
    }
}
