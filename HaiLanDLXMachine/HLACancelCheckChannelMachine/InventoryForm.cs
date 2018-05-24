﻿using DMSkin;
using HLACommonLib;
using HLACommonLib.DAO;
using HLACommonLib.Model;
using HLACommonLib.Model.YK;
using HLACommonView.Model;
using HLACommonView.Views;
using HLACommonView.Configs;
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
using OSharp.Utility.Extensions;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace HLACancelCheckChannelMachine
{
    public partial class InventoryForm : CommonInventoryFormIMP
    {
        CLogManager mLog = new CLogManager(true);

        string mCurBoxNo = "";
        Thread thread = null;

        public string mDocNo = "";
        string mTotalBoxNum = "";
        Dictionary<string, CCancelCheckHu2> mCancelHuDetail2 = new Dictionary<string, CCancelCheckHu2>();
        string mDianShuBoCi = "01";
        //upload
        private object savingDataLockObject = new object();
        private Queue<CUploadData> savingData = new Queue<CUploadData>();
        private Thread savingDataThread = null;

        const string BU_ZAI_BEN_XIANG = "不在本箱";
        const string HU_IS_NULL = "箱号为空";
        const string BU_PIPEI = "数量不匹配";
        public InventoryForm()
        {
            InitializeComponent();
            InitDevice(UHFReaderType.ImpinjR420, true);

        }
        public InventoryForm(string docNo, string num)
        {
            InitializeComponent();
            InitDevice(UHFReaderType.ImpinjR420, true);

            mDocNo = docNo;
            mTotalBoxNum = num;
        }
        private void InitView()
        {
            Invoke(new Action(() => {
                lblCurrentUser.Text = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "登录信息异常";
                lblLouceng.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.LOUCENG : "设备信息异常";
                lblPlc.Text = "连接中...";
                lblReader.Text = "连接中...";
                lblWorkStatus.Text = "未开始工作";
                label11_deviceNo.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.EQUIP_HLA : "设备信息异常";
                ComboBox_Boci.SelectedIndex = 0;
            }));
        }
        private void InventoryForm_Shown(object sender, EventArgs e)
        {
            mDianShuBoCi = this.ComboBox_Boci.SelectedItem.ToString();

            restoreGrid(mDocNo);

            restoreSavingQueue(mDocNo);

            this.savingDataThread = new Thread(new ThreadStart(savingDataThreadFunc));
            this.savingDataThread.IsBackground = true;
            this.savingDataThread.Start();
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            InitView();

            btnStart.Enabled = false;
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
                    Invoke(new Action(() => { lblReader.Text = "异常"; lblReader.ForeColor = Color.OrangeRed; }));


                ShowLoading("正在下载箱明细");
                mCancelHuDetail2 = SAPDataService.GetCancelHuDetailList2(SysConfig.LGNUM, mDocNo);

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

                Invoke(new Action(() =>
                {
                    btnStart.Enabled = true;
                }));
                HideLoading();

            }));

            thread.IsBackground = true;
            thread.Start();
        }
        private void Start()
        {
#if DEBUG
            StartInventory();

            List<Xindeco.Device.Model.TagInfo> ti = new List<Xindeco.Device.Model.TagInfo>();

            Xindeco.Device.Model.TagInfo t = new Xindeco.Device.Model.TagInfo();
            t.Epc = "50000E7C2500010000001";
            ti.Add(t);

            t = new Xindeco.Device.Model.TagInfo();
            t.Epc = "500011035500010000002";
            ti.Add(t);

            t = new Xindeco.Device.Model.TagInfo();
            t.Epc = "500011036500010000001";
            ti.Add(t);

            t = new Xindeco.Device.Model.TagInfo();
            t.Epc = "500011036500010000002";
            ti.Add(t);

            t = new Xindeco.Device.Model.TagInfo();
            t.Epc = "500011036500010000003";
            ti.Add(t);

            t = new Xindeco.Device.Model.TagInfo();
            t.Epc = "500011037500010000001";
            ti.Add(t);

            t = new Xindeco.Device.Model.TagInfo();
            t.Epc = "500011038500010000001";
            ti.Add(t);

            foreach (var v in ti)
                Reader_OnTagReported(v);

            StopInventory();
#endif

            btnStart.Enabled = false;
            btnPause.Enabled = true;

            openMachine();
        }
        private void Pause()
        {
            btnStart.Enabled = true;
            btnPause.Enabled = false;

            StopInventory();
            closeMachine();
        }
        public override void StartInventory()
        {
            if (!isInventory)
            {
                Invoke(new Action(() =>
                {
                    lblWorkStatus.Text = "开始扫描";
                }));
                SetInventoryResult(0);
                errorEpcNumber = 0;
                mainEpcNumber = 0;
                addEpcNumber = 0;
                epcList.Clear();
                tagDetailList.Clear();

                mCurBoxNo = "";

                if (boxNoList.Count > 0)
                {
                    mCurBoxNo = boxNoList.Dequeue();
                }

#if DEBUG
                mCurBoxNo = "2001994676";
#endif
                setBoxNo(mCurBoxNo);


                reader.StartInventory(0, 0, 0);
                isInventory = true;
                lastReadTime = DateTime.Now;

            }
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
        private void setBoxNo(string boxNo)
        {
            Invoke(new Action(() =>
            {
                label9_boxNo.Text = boxNo;
            }));
        }
        public override CheckResult CheckData()
        {
            CheckResult result = base.CheckData();

            if (mCurBoxNo == "")
            {
                result.UpdateMessage(HU_IS_NULL);
                result.InventoryResult = false;
            }

            CCheckRe re = piPei2(mCurBoxNo);
            bool fit = true;
            foreach(var v in re.bar)
            {
                if(v.realQty!=v.shouldQty)
                {
                    fit = false;
                    break;
                }
            }
            foreach(var v in re.barAdd)
            {
                if(v.realQty!=v.shouldQty)
                {
                    fit = false;
                    break;
                }
            }

            if(!fit)
            {
                result.UpdateMessage(BU_PIPEI);
                result.InventoryResult = false;
            }
            if (result.InventoryResult)
            {
                result.UpdateMessage(Consts.Default.RIGHT);
            }

            if (mCurBoxNo == "")
            {
                grid.Rows.Insert(0, "", "", "", "", "", "", result.Message);
                if (!result.InventoryResult)
                {
                    grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                }
            }

            List<CChaYi> chayi = getChaYi(re, result.InventoryResult, result.Message);
            addGrid(chayi);

            //print
            bool isHZ = false;
            Utils.CPrintData printData = getPrintData(chayi, result, ref isHZ);
            if (result.InventoryResult && printData.beizhu == "")
            {

            }
            else
            {
                Utils.PrintHelper.PrintTag(printData);
            }

            //save to local
            saveToLocal(mDocNo, mCurBoxNo, result.InventoryResult ? "S" : "E", result.Message, chayi);

            CCancelUpload uploadData = new CCancelUpload();
            uploadData.lgnum = SysConfig.LGNUM;
            uploadData.boxno = mCurBoxNo;
            uploadData.subuser = SysConfig.CurrentLoginUser.UserId;
            uploadData.inventoryRe = result.InventoryResult;
            uploadData.equipID = SysConfig.DeviceInfo.EQUIP_HLA;
            uploadData.loucheng = SysConfig.DeviceInfo.LOUCENG;
            uploadData.docno = mDocNo;
            uploadData.dianshuBoCi = mDianShuBoCi.ToString();
            uploadData.epcList.AddRange(epcList);
            uploadData.barqty = getUploadSubData();
            uploadData.isHZ = isHZ;

            addToSavingQueue(uploadData);

            return result;
        }
        void addGrid(List<CChaYi> chayi)
        {
            foreach(var v in chayi)
            {
                grid.Rows.Insert(0, v.hu, v.bar, v.barAdd, v.shouldQty, v.barChaYiQty ,v.barAddChaYiQty, v.msg);
                if (!v.inventoryRe)
                {
                    grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                }
            }
        }

        List<CChaYi> getChaYi(CCheckRe checkRe,bool inventoryRe,string msg)
        {
            List<CChaYi> re = new List<CChaYi>();

            try
            {
                string pin = "";
                string se = "";
                string gui = "";
                foreach (var v in checkRe.bar)
                {
                    string baradd = getBarAdd(v.bar,out pin,out se,out gui);
                    CChaYi chayi = new CChaYi();
                    chayi.pin = pin;
                    chayi.se = se;
                    chayi.gui = gui;

                    chayi.inventoryRe = inventoryRe;
                    chayi.msg = msg;
                    chayi.hu = mCurBoxNo;
                    chayi.bar = v.bar;
                    chayi.barAdd = baradd;
                    chayi.shouldQty = v.shouldQty;
                    chayi.barChaYiQty = v.realQty - v.shouldQty;
                    chayi.barAddChaYiQty = checkRe.getRealQty(baradd, true) - checkRe.getShouldQty(baradd, true);
                    re.Add(chayi);
                }
                foreach (var v in checkRe.barAdd)
                {
                    string bar = getBar(v.bar, out pin, out se, out gui);

                    if (!checkRe.bar.Exists(i => i.bar == bar))
                    {
                        CChaYi chayi = new CChaYi();
                        chayi.pin = pin;
                        chayi.se = se;
                        chayi.gui = gui;

                        chayi.inventoryRe = inventoryRe;
                        chayi.msg = msg;
                        chayi.hu = mCurBoxNo;
                        chayi.bar = bar;
                        chayi.barAdd = v.bar;
                        chayi.shouldQty = v.shouldQty;
                        chayi.barChaYiQty = checkRe.getRealQty(bar, false) - checkRe.getShouldQty(bar, false);
                        chayi.barAddChaYiQty = v.realQty - v.shouldQty;
                        re.Add(chayi);

                    }
                }
            }
            catch(Exception ex)
            {
                Log4netHelper.LogError(ex);
            }

            return re;
        }
        string getBarAdd(string bar,out string pin,out string se,out string gui)
        {
            pin = "";
            se = "";
            gui = "";
            TagDetailInfo ti = tagDetailList.FirstOrDefault(i => i.BARCD == bar);
            if (ti != null)
            {
                pin = ti.ZSATNR;
                se = ti.ZCOLSN;
                gui = ti.ZSIZTX;
                return ti.BARCD_ADD;
            }
            HLATagInfo t = hlaTagList.FirstOrDefault(i => i.BARCD == bar);
            if (t != null)
            {
                MaterialInfo mi = materialList.FirstOrDefault(i => i.MATNR == t.MATNR);
                if(mi!=null)
                {
                    pin = mi.ZSATNR;
                    se = mi.ZCOLSN;
                    gui = mi.ZSIZTX;
                }
                return t.BARCD_ADD;
            }
            return "";
        }
        string getBar(string barAdd, out string pin, out string se, out string gui)
        {
            pin = "";
            se = "";
            gui = "";

            TagDetailInfo ti = tagDetailList.FirstOrDefault(i => i.BARCD_ADD == barAdd);
            if (ti != null)
            {
                pin = ti.ZSATNR;
                se = ti.ZCOLSN;
                gui = ti.ZSIZTX;
                return ti.BARCD;
            }
            HLATagInfo t = hlaTagList.FirstOrDefault(i => i.BARCD_ADD == barAdd);
            if (t != null)
            {
                MaterialInfo mi = materialList.FirstOrDefault(i => i.MATNR == t.MATNR);
                if (mi != null)
                {
                    pin = mi.ZSATNR;
                    se = mi.ZCOLSN;
                    gui = mi.ZSIZTX;
                }
                return t.BARCD;
            }
            return "";
        }
        void saveToLocal(string doc, string hu, string re,string msg,List<CChaYi> data)
        {
            try
            {
                string sql = string.Format("insert into CancelInfo (docNo,boxNo,re,msg,inInfo,timeStamp) values ('{0}','{1}','{2}','{3}','{4}',GETDATE())", doc, hu, re, msg, JsonConvert.SerializeObject(data));
                DBHelper.ExecuteNonQuery(sql);
            }
            catch(Exception ex)
            {
                Log4netHelper.LogError(ex);
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

        private HLACancelCheckChannelMachine.Utils.CPrintData getPrintData(List<CChaYi> chayi, CheckResult cr, ref bool isHZ)
        {
            HLACancelCheckChannelMachine.Utils.CPrintData re = new Utils.CPrintData();
            try
            {
                re.hu = mCurBoxNo;
                re.inventoryRe = cr.InventoryResult;
                re.totalNum = 0;
                re.beizhu = "";
                if (!mCancelHuDetail2.ContainsKey(mCurBoxNo))
                {
                    return re;
                }

                re.totalNum = mCancelHuDetail2[mCurBoxNo].mBar.Sum(i => i.qty);

                if (mCancelHuDetail2[mCurBoxNo].mIsCp)
                {
                    re.beizhu += "客诉次品/";
                }
                if (mCancelHuDetail2[mCurBoxNo].mIsHz)
                {
                    re.beizhu += "混规则/";
                    isHZ = true;
                }
                if (mCancelHuDetail2[mCurBoxNo].mIsDd)
                {
                    re.beizhu += "一箱多单/";
                }

                foreach(var v in chayi)
                {
                    Utils.CPrintContent con = new Utils.CPrintContent();

                    con.pin = v.pin;
                    con.se = v.se;
                    con.gui = v.gui;

                    con.diff = v.barChaYiQty;
                    con.diffAdd = v.barAddChaYiQty;
                    con.isRFID = mCancelHuDetail2[mCurBoxNo].mIsRFID;

                    re.content.Add(con);
                }

            }
            catch (System.Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
            }

            return re;
        }
        public List<CCancelUploadSubData> getUploadSubData()
        {
            List<CCancelUploadSubData> re = new List<CCancelUploadSubData>();
            /*
            var reData = tagDetailList.GroupBy(i => new { i.BARCD, i.BARCD_ADD, i.IsAddEpc }).Select(i => new { i.Key.BARCD, i.Key.BARCD_ADD, i.Key.IsAddEpc, Count = i.Count() });

            foreach(var item in reData)
            {
                CCancelUploadSubData usd = re.Find(r => r.barcd == item.BARCD);

                if (usd==null)
                {
                    usd = new CCancelUploadSubData();
                    usd.barcd = item.BARCD;
                    usd.barcdAdd = item.BARCD_ADD;

                    re.Add(usd);
                }

                if (!item.IsAddEpc)
                    usd.qty += item.Count;
                else
                    usd.qtyAdd += item.Count;
            }
            */
            foreach (var v in tagDetailList)
            {
                CCancelUploadSubData data = re.FirstOrDefault(i => (i.barcd + i.barcdAdd) == (v.BARCD + v.BARCD_ADD));
                if (data == null)
                {
                    CCancelUploadSubData d = new CCancelUploadSubData(v.BARCD, v.IsAddEpc ? 0 : 1, v.BARCD_ADD, v.IsAddEpc ? 1 : 0);
                    re.Add(d);
                }
                else
                {
                    if (v.IsAddEpc)
                    {
                        data.qtyAdd += 1;
                    }
                    else
                    {
                        data.qty += 1;
                    }
                }
            }
            return re;
        }
        public override void StopInventory()
        {
            if (isInventory)
            {
                Invoke(new Action(() =>
                {
                    lblWorkStatus.Text = "停止扫描";
                }));
                isInventory = false;
                reader.StopInventory();
                CheckResult cre = CheckData();

                playSound(cre.InventoryResult);

                if (cre.InventoryResult)
                {
                    SetInventoryResult(1);
                }
                else
                {
                    SetInventoryResult(3);
                }


            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void dmButton3_Click(object sender, EventArgs e)
        {
            GxForm form = new GxForm();
            form.ShowDialog();
        }
        CCheckRe piPei2(string hu)
        {
            CCheckRe re = new CCheckRe();

            if (mCancelHuDetail2 == null || tagDetailList == null || string.IsNullOrEmpty(hu))
                return re;

            if (!mCancelHuDetail2.ContainsKey(hu))
                return re;

            foreach(var v in tagDetailList)
            {
                if(v.IsAddEpc)
                {
                    if(re.barAdd.Exists(i=>i.bar == v.BARCD_ADD))
                    {
                        re.barAdd.FirstOrDefault(i => i.bar == v.BARCD_ADD).realQty += 1;
                    }
                    else
                    {
                        CCheckReData cd = new CCheckReData();
                        cd.realQty = 1;
                        cd.shouldQty = mCancelHuDetail2[hu].getBarAddQty(v.BARCD_ADD);
                        cd.bar = v.BARCD_ADD;
                        re.barAdd.Add(cd);
                    }
                }
                else
                {
                    if (re.bar.Exists(i => i.bar == v.BARCD))
                    {
                        re.bar.FirstOrDefault(i => i.bar == v.BARCD).realQty += 1;
                    }
                    else
                    {
                        CCheckReData cd = new CCheckReData();
                        cd.realQty = 1;
                        cd.shouldQty = mCancelHuDetail2[hu].getBarQty(v.BARCD);
                        cd.bar = v.BARCD;
                        re.bar.Add(cd);
                    }
                }
            }

            CCancelCheckHu2 cd2 = mCancelHuDetail2[hu];
            foreach(var v in cd2.mBar)
            {
                if (!re.bar.Exists(i => i.bar == v.bar))
                {
                    CCheckReData r = new CCheckReData();
                    r.realQty = 0;
                    r.shouldQty = v.qty;
                    r.bar = v.bar;
                    re.bar.Add(r);
                }
            }
            foreach(var v in cd2.mBarAdd)
            {
                if(!re.barAdd.Exists(i=>i.bar == v.bar))
                {
                    CCheckReData r = new CCheckReData();
                    r.realQty = 0;
                    r.shouldQty = v.qty;
                    r.bar = v.bar;
                    re.barAdd.Add(r);
                }
            }

            return re;
        }

        private void InventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            int sc = SqliteDataService.GetUnUploadCountFromSqlite();
            if(sc>0)
            {
                DialogResult re = MessageBox.Show(this, string.Format("还有{0}条未上传的数据，确定关闭吗", sc), "", MessageBoxButtons.YesNo);
                if(re == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

            }

            if (this.savingDataThread != null)
                this.savingDataThread.Abort();

            CloseWindow();
        }

        List<CCheckRe> restoreGrid(string doc)
        {
            List<CCheckRe> re = new List<CCheckRe>();

            try
            {
                string sql = string.Format("select * from CancelInfo where docNo='{0}' order by timeStamp", doc);
                DataTable dt = DBHelper.GetTable(sql, false);
                if(dt!=null && dt.Rows.Count>0)
                {
                    grid.Rows.Clear();
                    foreach (DataRow rw in dt.Rows)
                    {
                        List<CChaYi> r = JsonConvert.DeserializeObject<IEnumerable<CChaYi>>(rw["inInfo"].ToString()) as List<CChaYi>;
                        if(r!=null)
                        {
                            addGrid(r);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log4netHelper.LogError(ex);
            }

            return re;
        }
        //restore from sql
        private void restoreSavingQueue(string docno)
        {
            try
            {
                List<CUploadData> data = SqliteDataService.GetAllUploadFromSqlite<CCancelUpload>();
                if(data!=null)
                {
                    foreach(var v in data)
                    {
                        SqliteDataService.delUploadFromSqlite(v.Guid);
                        addToSavingQueue(v.Data as CCancelUpload);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        //update from sap
        public void addToSavingQueue(CCancelUpload uploadData)
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

        private void savingDataThreadFunc()
        {
            while (true)
            {
                try
                {
                    if (savingData.Count > 0)
                    {
                        CUploadData ud = null;
                        lock (savingDataLockObject)
                        {
                            ud = savingData.Dequeue();
                        }
                        if (ud != null)
                        {
                            CCancelUpload upData = ud.Data as CCancelUpload;
                            if (upData != null)
                            {
                                //upload
                                string uploadRe = "";
                                string sapMsg = "";
                                SAPDataService.UploadCancelData(upData, ref uploadRe, ref sapMsg);

                                if (uploadRe == "E")
                                {
                                    SqliteDataService.updateMsgToSqlite(ud.Guid, sapMsg);
                                    notifyException();
                                }
                                else
                                {
                                    SqliteDataService.delUploadFromSqlite(ud.Guid);
                                }

                                updateUploadCount();
                            }
                        }
                    }
                    Thread.Sleep(1000);

                }
                catch (Exception)
                {
                    //LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
                }
            }
        }

        private void dmButton1_exception_query_Click(object sender, EventArgs e)
        {
            dmButton1_exception_query.DM_NormalColor = Color.WhiteSmoke;

            UploadMsgForm ef = new UploadMsgForm(this);
            ef.ShowDialog();
        }

        private void dmButton2_upload_query_Click(object sender, EventArgs e)
        {

        }

        void updateUploadCount()
        {
            int count = SqliteDataService.GetUnUploadCountFromSqlite();
            Invoke(new Action(() =>
            {
                dmButton2_upload_query.Text = string.Format("上传列表({0})", count);
            }));

        }
        private void ComboBox_Boci_SelectionChangeCommitted(object sender, EventArgs e)
        {
            mDianShuBoCi = ComboBox_Boci.SelectedItem.ToString();
        }

        private void notifyException()
        {
            try
            {
                Invoke(new Action(() =>
                {
                    dmButton1_exception_query.DM_NormalColor = Color.Red;
                }));
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
            }
        }
    }

    public class CCheckReData
    {
        public int shouldQty;
        public int realQty;
        public string bar;
    }
    public class CCheckRe
    {
        public List<CCheckReData> bar = new List<CCheckReData>();
        public List<CCheckReData> barAdd = new List<CCheckReData>();
        public CCheckRe()
        {
            
        }
        public int getShouldQty(string barStr,bool add)
        {
            if(add)
            {
                CCheckReData d = barAdd.FirstOrDefault(i => i.bar == barStr);
                if (d != null)
                    return d.shouldQty;
            }
            else
            {
                CCheckReData d = bar.FirstOrDefault(i => i.bar == barStr);
                if (d != null)
                    return d.shouldQty;
            }

            return 0;
        }
        public int getRealQty(string barStr,bool add)
        {
            if (add)
            {
                CCheckReData d = barAdd.FirstOrDefault(i => i.bar == barStr);
                if (d != null)
                    return d.realQty;
            }
            else
            {
                CCheckReData d = bar.FirstOrDefault(i => i.bar == barStr);
                if (d != null)
                    return d.realQty;
            }

            return 0;
        }
    }
    public class CChaYi
    {
        public string hu;
        public string bar;
        public string barAdd;
        public int shouldQty;
        public int barChaYiQty;
        public int barAddChaYiQty;
        public bool inventoryRe;
        public string msg;

        public string pin;
        public string se;
        public string gui;
    }
}
