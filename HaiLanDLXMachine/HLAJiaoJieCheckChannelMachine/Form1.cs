using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonView.Model;
using HLACommonView.Views;
using HLACommonView.Configs;
using Xindeco.Device.Model;
using System.Threading;
using DMSkin;
using HLACommonLib;
using HLACommonLib.DAO;
using HLACommonLib.Model;
using HLACommonLib.Model.YK;

namespace HLAJiaoJieCheckChannelMachine
{
    public partial class Form1 : CommonInventoryFormIMP
    {
        public const string XIANGHAO_BUZAI_BENDAN = "箱号不在当前单";
        public const string XIANGHAO_DATA_BUFU = "明细不一致";
        public const string HAS_SAOMIAO_CHONGTOU = "重投";
        public const string HAS_SAOMIAO_YICHANG = "交接异常";
        public const string WEI_SAO_DAO_XIANGHU = "未扫到箱号";

        public CJiaoJieDan mJiaoJieDan = new CJiaoJieDan();
        public List<CJJBox> mCurDanBoxList = new List<CJJBox>();

        /*
        private object savingDataLockObject = new object();
        private Queue<CUploadData> savingData = new Queue<CUploadData>();
        private Thread savingDataThread = null;
        */
        public Form1()
        {
            InitializeComponent();
            InitDevice(UHFReaderType.ImpinjR420, true);

        }

        private void InitView()
        {
            Invoke(new Action(() => {
                lblCurrentUser.Text = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "登录信息异常";
                lblLouceng.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.LOUCENG : "设备信息异常";
                lblPlc.Text = "连接中...";
                lblReader.Text = "连接中...";
                lblInventoryRe.Text = "未开始工作";
                label11_status.Text = "未开始工作";
                label15_deviceNo.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.EQUIP_HLA : "设备信息异常";
                button2_stop.Enabled = false;
            }));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitView();
            Thread thread = new Thread(new ThreadStart(() => {
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
                    Invoke(new Action(() => { lblReader.Text = "异常"; lblReader.ForeColor = Color.OrangeRed; }));

                bool closed = false;

                ShowLoading("正在下载物料数据...");
                //materialList = SAPDataService.GetMaterialInfoListAll(SysConfig.LGNUM);
                materialList = LocalDataService.GetMaterialInfoList();

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

                /*
                this.savingDataThread = new Thread(new ThreadStart(savingDataThreadFunc));
                this.savingDataThread.IsBackground = true;
                this.savingDataThread.Start();
                */
            }));

            thread.IsBackground = true;
            thread.Start();
        }
        public void clearHu(string hu)
        {
            if(string.IsNullOrEmpty(hu))
            {
                MessageBox.Show("请输入箱号");
                return;
            }

            if (MessageBox.Show("确认删除该箱数据吗？", "询问", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string msg = "";
                if (LocalDataService.clearJiaoJieDanHu(mJiaoJieDan.doc, hu, ref msg))
                {
                    mCurDanBoxList.RemoveAll(i => i.hu == hu);
                    updateHuCount();
                    MessageBox.Show("删除成功");
                }
                else
                {
                    MessageBox.Show(msg, "删除失败");
                }
            }
        }

        public void clearDoc(string doc)
        {
            if (string.IsNullOrEmpty(doc))
            {
                MessageBox.Show("请输入单号");
                return;
            }

            if (doc != mJiaoJieDan.doc)
            {
                MessageBox.Show("输入的单号和当前交接单号不一致");
                return;
            }

            if(MessageBox.Show("确认删除该单号所有数据吗？","询问",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string msg = "";
                if(LocalDataService.clearJiaoJieDan(doc,ref msg))
                {
                    mCurDanBoxList.Clear();
                    updateHuCount();
                    grid.Rows.Clear();
                    MessageBox.Show("删除成功");
                }
                else
                {
                    MessageBox.Show(msg, "删除失败");
                }
            }
        }
        /*
        string getBarAddcd(string barcd)
        {
            string re = "";
            try
            {
                if(!string.IsNullOrEmpty(barcd))
                {
                    HLATagInfo tg = hlaTagList.FirstOrDefault(i => i.BARCD == barcd);
                    if(tg!=null && !string.IsNullOrEmpty(tg.BARCD_ADD))
                    {
                        return tg.BARCD_ADD;
                    }
                }
            }
            catch(Exception)
            {

            }
            return re;
        }
        void checkAddBarcd(CJiaoJieDan dan)
        {
            try
            {
                if (dan == null)
                    return;

                foreach (var v in dan.huData)
                {
                    foreach (var item in v.Value)
                    {
                        string barcd = item.barcd;
                        item.barcd_add = getBarAddcd(barcd);
                    }
                }
            }
            catch(Exception)
            {

            }
        }*/
        public void loadDoc(CJiaoJieDan jjd)
        {
            //checkAddBarcd(jjd);

            label13_jiaojiedocNO.Text = jjd.doc;
            label9_totalHu.Text = jjd.huData.Count.ToString();

            mJiaoJieDan = jjd;
            mCurDanBoxList = LocalDataService.getJiaoJieDan(jjd.doc);

            grid.Rows.Clear();
            foreach(var v in mCurDanBoxList)
            {
                addgrid(v);
            }

            updateHuCount();
        }

        public void updateHuCount()
        {
            Invoke(new Action(() =>
            {
                label20_okHu.Text = mCurDanBoxList.Count(i => i.inventoryRe == "S" && i.sapRe == "S").ToString();
                label21_errorHu.Text = mCurDanBoxList.Count(i => i.inventoryRe != "S" || i.sapRe != "S").ToString();
            }));
        }

        private void btnInputDoc_Click(object sender, EventArgs e)
        {
            jiaojiedan f = new jiaojiedan(this);
            f.ShowDialog();
        }

        public override void StartInventory()
        {
            if (!isInventory)
            {
                Invoke(new Action(() => {
                    label11_status.Text = "开始扫描";
                    lblInventoryRe.Text = "";
                    label17_currentHu.Text = "";
                }));

                SetInventoryResult(0);
                errorEpcNumber = 0;
                mainEpcNumber = 0;
                addEpcNumber = 0;
                epcList.Clear();
                tagDetailList.Clear();
                if (boxNoList.Count > 0)
                {
                    Invoke(new Action(() => {
                        label17_currentHu.Text = boxNoList.Dequeue();
                    }));
                }
                reader.StartInventory(0, 0, 0);
                isInventory = true;
                lastReadTime = DateTime.Now;
            }
        }

        public void addgrid(CJJBox box)
        {
            try
            {
                List<string> mats = box.tags.Select(i => i.MATNR).Distinct().ToList();
                Invoke(new Action(() =>
                {
                    foreach (var item in mats)
                    {
                        int c = box.tags.Count(i => i.MATNR == item && !i.IsAddEpc);
                        TagDetailInfo ti = box.tags.First(i => i.MATNR == item && !i.IsAddEpc);
                        grid.Rows.Insert(0, box.hu, ti.ZSATNR, ti.ZCOLSN, ti.ZSIZTX, c, box.inventoryMsg + " " + "SAP:" + box.sapMsg);
                        if (box.inventoryRe == "E" || box.sapRe == "E")
                        {
                            grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                        }
                    }
                }));
            }
            catch(Exception)
            {

            }
        }
        void stopReader()
        {
            if (isInventory)
            {
                Invoke(new Action(() =>
                {
                    label11_status.Text = "停止扫描";
                }));
                isInventory = false;
                reader.StopInventory();
            }
        }
        public override void StopInventory()
        {
            if (isInventory)
            {
                Invoke(new Action(() => {
                    label11_status.Text = "停止扫描";
                }));
                isInventory = false;
                reader.StopInventory();

                CheckResult checkResult = check(label17_currentHu.Text);

                if (checkResult.InventoryResult)
                {
                    checkResult.UpdateMessage(Consts.Default.RIGHT);
                }

                Invoke(new Action(() => {
                    lblInventoryRe.Text = checkResult.InventoryResult ? "正常" : "异常";
                }));
                
                CJJBox curBox = getCurBox(checkResult);

                if(checkResult.Message.Contains(HAS_SAOMIAO_CHONGTOU)
                    || checkResult.Message.Contains(WEI_SAO_DAO_XIANGHU)
                    || checkResult.Message.Contains(Consts.Default.XIANG_MA_BU_YI_ZHI)
                    || checkResult.Message.Contains(HAS_SAOMIAO_YICHANG))
                {
                }
                else
                {
                    //上传
                    saveAndUpdate(curBox);
                }

                //add grid
                addgrid(curBox);

                playSound(checkResult.InventoryResult);

                if (checkResult.InventoryResult)
                {
                    SetInventoryResult(1);
                }
                else
                {
                    SetInventoryResult(1);
                }
            }
        }

        void playSound(bool re)
        {
            try
            {
                if (re)
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

        bool duibi(string hu)
        {
            bool re = true;
            try
            {
                /*
                if (mJiaoJieDan.huData.ContainsKey(hu))
                {
                    List<TagDetailInfo> tags = tagDetailList.ToList();
                    List<CJiaoJieDanData> huData = mJiaoJieDan.huData[hu].ToList();
                    foreach(var v in huData)
                    {
                        if(v.quan == tags.Count(i=>i.BARCD == v.barcd && !i.IsAddEpc))
                        {
                            tags.RemoveAll(i => i.BARCD == v.barcd && !i.IsAddEpc);
                            if(!string.IsNullOrEmpty(v.barcd_add))
                            {
                                if(v.quan == tags.Count(i => i.BARCD_ADD == v.barcd_add && i.IsAddEpc))
                                {
                                    tags.RemoveAll(i => i.BARCD_ADD == v.barcd_add && i.IsAddEpc);
                                }
                                else
                                {
                                    re = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            re = false;
                            break;
                        }
                    }
                    if(tags.Count>0)
                    {
                        re = false;
                    }

                }
                else
                {
                    re = false;
                }*/

                if (!mJiaoJieDan.huData.ContainsKey(hu))
                    re = false;
                else
                {
                    List<TagDetailInfo> tags = tagDetailList.ToList();
                    List<CJiaoJieDanData> huData = mJiaoJieDan.huData[hu].ToList();
                    foreach (var v in huData)
                    {
                        if (v.quan != tags.Count(i => i.BARCD == v.barcd && !i.IsAddEpc))
                        {
                            re = false;
                            break;
                        }
                        tags.RemoveAll(i => i.BARCD == v.barcd);
                    }
                    if (tags.Count > 0)
                    {
                        re = false;
                    }
                }
            }
            catch(Exception ex)
            {
                re = false;
                Log4netHelper.LogError(ex);
            }
            return re;
        }
        CheckResult check(string hu)
        {
            CheckResult re = CheckData();

            if (button3_start.Enabled)
            {
                re.UpdateMessage("请点击开始按钮");
                re.InventoryResult = false;
            }

            if (string.IsNullOrEmpty(hu))
            {
                re.UpdateMessage(WEI_SAO_DAO_XIANGHU);
                re.InventoryResult = false;
            }

            //是否已经扫描过了
            if (mCurDanBoxList.Exists(i=>i.hu == hu && i.inventoryRe == "S" && i.sapRe == "S"))
            {
                if (boxSame(hu))
                {
                    re.UpdateMessage(HAS_SAOMIAO_CHONGTOU);
                    re.InventoryResult = false;
                }
                else
                {
                    re.UpdateMessage(HAS_SAOMIAO_YICHANG);
                    re.InventoryResult = false;
                }
            }

             //不在本单，直接返回
            if(!mJiaoJieDan.huData.ContainsKey(hu))
            {
                re.UpdateMessage(XIANGHAO_BUZAI_BENDAN);
                re.InventoryResult = false;
            }

            if(!duibi(hu))
            {
                re.UpdateMessage(XIANGHAO_DATA_BUFU);
                re.InventoryResult = false;
            }

            return re;
        }
        
        bool boxSame(string hu)
        {
            CJJBox box = mCurDanBoxList.FirstOrDefault(i => i.hu == hu);
            return LocalDataService.compareListStr(box.epc, epcList);
        }
        CJJBox getCurBox(CheckResult cr)
        {
            CJJBox re = new CJJBox();

            try
            {
                re.doc = mJiaoJieDan.doc;
                re.user = SysConfig.CurrentLoginUser.UserId;
                re.devno = SysConfig.DeviceInfo.EQUIP_HLA;
                re.loucheng = SysConfig.DeviceInfo.LOUCENG;
                re.hu = label17_currentHu.Text;
                re.inventoryRe = cr.InventoryResult ? "S" : "E";
                re.inventoryMsg = cr.Message;
                re.epc = epcList.ToList();
                re.tags = tagDetailList.ToList();
            }
            catch(Exception e)
            {
                Log4netHelper.LogError(e);
            }

            return re;
        }
        /*
        private void savingDataThreadFunc()
        {
            while (true)
            {
                CUploadData upData = null;
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
        */

        public void updateBoxList(CJJBox curBox)
        {
            if (curBox != null && !string.IsNullOrEmpty(curBox.hu))
            {
                mCurDanBoxList.RemoveAll(i => i.doc == mJiaoJieDan.doc && i.hu == curBox.hu);
                mCurDanBoxList.Add(curBox);
            }
        }
        public void saveAndUpdate(CJJBox box)
        {
            if (box == null)
                return;

            saveData(box);
            updateBoxList(box);
            updateHuCount();
        }
        public void saveData(CJJBox box)
        {
            if (box == null)
                return;
            try
            {
                CUploadData data = saveToSqlite(box);
                //uplad to sap
                string sapRe = "";
                string sapMsg = "";
                SAPDataService.uploadJiaoJieDan(box, ref sapRe, ref sapMsg);
                box.sapRe = sapRe;
                box.sapMsg = sapMsg;
                //save to local
                LocalDataService.saveJiaoJieDan(box);

                if (sapRe == "S")
                {
                    SqliteDataService.delUploadFromSqlite(data.Guid);
                }
                else
                {
                    SqliteDataService.updateMsgToSqlite(data.Guid, sapMsg);
                }
            }
            catch(Exception ex)
            {
                Log4netHelper.LogError(ex);
            }
        }
        public CUploadData saveToSqlite(CJJBox data)
        {
            CUploadData cu = new CUploadData();
            cu.Guid = Guid.NewGuid().ToString();
            cu.IsUpload = 0;
            cu.Data = data;
            cu.CreateTime = DateTime.Now;
            SqliteDataService.saveToSqlite(cu);
            return cu;
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
        private void button1_group_Click(object sender, EventArgs e)
        {
            HLACommonView.Views.GxForm form = new GxForm();
            form.ShowDialog();
        }

        private void button3_start_Click(object sender, EventArgs e)
        {
            button3_start.Enabled = false;
            button2_stop.Enabled = true;

            openMachine();
        }

        private void button2_stop_Click(object sender, EventArgs e)
        {
            button3_start.Enabled = true;
            button2_stop.Enabled = false;

            stopReader();
            closeMachine();
        }

        private void button4_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_upload_Click(object sender, EventArgs e)
        {
            UploadMsgForm uf = new UploadMsgForm(this);
            uf.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            boxNoList.Enqueue(textBox1.Text.Trim());
            StartInventory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            errorEpcNumber = 0;
            mainEpcNumber = 59;
            addEpcNumber = 0;
            {
                for(int i=1;i<=23;i++)
                {
                    string epc = "500007B0850001000000" + i.ToString();
                    epcList.Add(epc);
                    tagDetailList.Add(GetTagDetailInfoByEpc(epc));
                }

                for (int i = 1; i <= 23; i++)
                {
                    string epc = "50000673950001000000" + i.ToString();
                    epcList.Add(epc);
                    tagDetailList.Add(GetTagDetailInfoByEpc(epc));
                }
                for (int i = 1; i <= 12; i++)
                {
                    string epc = "500006DEE50001000000" + i.ToString();
                    epcList.Add(epc);
                    tagDetailList.Add(GetTagDetailInfoByEpc(epc));
                }
            }

            StopInventory();
        }

        private void button3_clearData_Click(object sender, EventArgs e)
        {
            ClearDataForm cdf = new ClearDataForm(this);
            cdf.ShowDialog();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            SqliteDataService.delOldData();
        }
    }

}
