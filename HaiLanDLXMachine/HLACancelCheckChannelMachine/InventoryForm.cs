using DMSkin;
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
    public partial class InventoryForm : CommonInventoryFormIMP,UploadMsgFormMethod
    {
        CLogManager mLog = new CLogManager(true);

        string mCurBoxNo = "";
        Thread thread = null;

        public string mDocNo = "";
        string mTotalBoxNum = "";
        public CCancelDoc mDocData = new CCancelDoc();
        string mDianShuBoCi = "01";
        //upload
        /*
        private object savingDataLockObject = new object();
        private Queue<CUploadData> savingData = new Queue<CUploadData>();
        private Thread savingDataThread = null;*/

        const string BU_ZAI_BEN_XIANG = "不在本单";
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
            /*
            restoreSavingQueue(mDocNo);        
            this.savingDataThread = new Thread(new ThreadStart(savingDataThreadFunc));
            this.savingDataThread.IsBackground = true;
            this.savingDataThread.Start();*/
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
                mDocData = SAPDataService.GetCancelHuDocData(SysConfig.LGNUM, mDocNo);

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

            stopReader();
            closeMachine();
        }
        void stopReader()
        {
            if (isInventory)
            {
                Invoke(new Action(() =>
                {
                    lblWorkStatus.Text = "停止扫描";
                }));
                isInventory = false;
                reader.StopInventory();
            }
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
        List<CChaYi> piPei3(string hu)
        {
            List<CChaYi> re = new List<CChaYi>();
            try
            {
                if (!mDocData.docData.Exists(i => i.hu == hu))
                {
                    List<string> barcdList = tagDetailList.Select(i => i.BARCD).Distinct().ToList();
                    foreach(var v in barcdList)
                    {
                        CChaYi cy = new CChaYi();
                        cy.hu = hu;
                        cy.pin = tagDetailList.FirstOrDefault(i => i.BARCD == v).ZSATNR;
                        cy.se = tagDetailList.FirstOrDefault(i => i.BARCD == v).ZCOLSN;
                        cy.gui = tagDetailList.FirstOrDefault(i => i.BARCD == v).ZSIZTX;

                        cy.shouldQty = "0";
                        cy.bar = v;
                        cy.barChaYiQty = tagDetailList.Count(i => i.BARCD == v && !i.IsAddEpc);
                        cy.barAdd = tagDetailList.FirstOrDefault(i => i.BARCD == v).BARCD_ADD;
                        cy.barAddChaYiQty = tagDetailList.Count(i => i.BARCD == v && i.IsAddEpc);

                        re.Add(cy);
                    }
                }
                else
                {
                    CCancelDocData docData = mDocData.docData.FirstOrDefault(i => i.hu == hu);

                    foreach(var v in docData.barQty)
                    {
                        CChaYi cy = new CChaYi();
                        cy.hu = hu;
                        getBarAdd(v.barcd, out cy.pin, out cy.se, out cy.gui);

                        cy.shouldQty = v.qty.ToString();
                        cy.bar = v.barcd;
                        cy.barChaYiQty = tagDetailList.Count(i => i.BARCD == v.barcd && !i.IsAddEpc) - v.qty;
                        cy.barAdd = getBarAdd(v.barcd);
                        if (string.IsNullOrEmpty(cy.barAdd))
                        {
                            cy.barAddChaYiQty = tagDetailList.Count(i => i.BARCD == v.barcd && i.IsAddEpc) - 0;
                        }
                        else
                        {
                            cy.barAddChaYiQty = tagDetailList.Count(i => i.BARCD == v.barcd && i.IsAddEpc) - v.qty;
                        }

                        re.Add(cy);
                    }
                    List<string> barcdList = tagDetailList.Select(i => i.BARCD).Distinct().ToList();
                    foreach(var v in barcdList)
                    {
                        if(!re.Exists(i=>i.bar == v))
                        {
                            CChaYi cy = new CChaYi();
                            cy.hu = hu;
                            cy.pin = tagDetailList.FirstOrDefault(i => i.BARCD == v).ZSATNR;
                            cy.se = tagDetailList.FirstOrDefault(i => i.BARCD == v).ZCOLSN;
                            cy.gui = tagDetailList.FirstOrDefault(i => i.BARCD == v).ZSIZTX;

                            cy.shouldQty = "0";
                            cy.bar = v;
                            cy.barChaYiQty = tagDetailList.Count(i => i.BARCD == v && !i.IsAddEpc);
                            cy.barAdd = tagDetailList.FirstOrDefault(i => i.BARCD == v).BARCD_ADD;
                            cy.barAddChaYiQty = tagDetailList.Count(i => i.BARCD == v && i.IsAddEpc);

                            re.Add(cy);
                        }
                    }

                }
            }
            catch (Exception)
            {

            }
            return re;
        }
        public override CheckResult CheckData()
        {
            CheckResult result = base.CheckData();

            if (mCurBoxNo == "")
            {
                result.UpdateMessage(HU_IS_NULL);
                result.InventoryResult = false;
            }

            if(!mDocData.docData.Exists(i=>i.hu == mCurBoxNo))
            {
                result.UpdateMessage(BU_ZAI_BEN_XIANG);
                result.InventoryResult = false;
            }

            if (result.Message.Contains(HU_IS_NULL) || result.Message.Contains(Consts.Default.XIANG_MA_BU_YI_ZHI))
            {     
                //直接返回
                addGrid(result);
                return result;
            }

            List<CChaYi> chayi = piPei3(mCurBoxNo);
            
            foreach(var v in chayi)
            {
                if(v.barChaYiQty!=0 || v.barAddChaYiQty!=0)
                {
                    result.UpdateMessage(BU_PIPEI);
                    result.InventoryResult = false;
                    break;
                }
            }

            if (result.InventoryResult)
            {
                result.UpdateMessage(Consts.Default.RIGHT);
            }

            foreach(var v in chayi)
            {
                v.inventoryRe = result.InventoryResult;
                v.msg = result.Message;
            }

            ShowLoading("正在打印...");
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
            uploadData.tagDetailList = tagDetailList.ToList();
            uploadData.isHZ = isHZ;

            ShowLoading("正在上传SAP...");
            string sapRe = "";
            string sapMsg = "";
            uploadSAP(uploadData, out sapRe, out sapMsg);

            playSound(result.InventoryResult && sapRe == "S");

            foreach (var v in chayi)
            {
                v.sapMsg = sapMsg;
                v.sapRe = sapRe;
            }

            ShowLoading("正在保存到本地...");
            //save to local
            saveToLocal(mDocNo, mCurBoxNo, result.InventoryResult ? "S" : "E", result.Message, chayi);

            addGrid(chayi);

            return result;
        }
        void addGrid(CheckResult cr)
        {
            Invoke(new Action(() =>
            {
                grid.Rows.Insert(0, "", "", "", "", "", "", cr.Message);
                if (!cr.InventoryResult)
                {
                    grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                }
            }));
        }
        void addGrid(List<CChaYi> chayi)
        {
            Invoke(new Action(() =>
            {
                foreach (var v in chayi)
                {
                    grid.Rows.Insert(0, v.hu, v.bar, v.barAdd, v.shouldQty, v.barChaYiQty, v.barAddChaYiQty, v.msg + " SAP:" + v.sapMsg);
                    if (!v.inventoryRe || v.sapRe != "S")
                    {
                        grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                    }
                }
            }));       
        }
        
        string getBarAdd(string bar)
        {
            TagDetailInfo ti = tagDetailList.FirstOrDefault(i => i.BARCD == bar);
            if (ti != null)
            {
                return ti.BARCD_ADD;
            }
            HLATagInfo t = hlaTagList.FirstOrDefault(i => i.BARCD == bar);
            if (t != null)
            {
                return t.BARCD_ADD;
            }
            return "";

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
        void saveToLocal(string doc, string hu, string re,string msg,List<CChaYi> data)
        {
            try
            {
                string sql = string.Format("insert into CancelInfo (docNo,boxNo,re,msg,inInfo,timeStamp,deviceNo) values ('{0}','{1}','{2}','{3}','{4}',GETDATE(),'{5}')", doc, hu, re, msg, JsonConvert.SerializeObject(data), SysConfig.DeviceNO);
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
                if (!mDocData.docData.Exists(i=>i.hu == mCurBoxNo))
                {
                    return re;
                }
                CCancelDocData docData = mDocData.docData.FirstOrDefault(i => i.hu == mCurBoxNo);

                re.totalNum = docData.barQty.Sum(i => i.qty);

                if (docData.mIsCp)
                {
                    re.beizhu += "客诉次品/";
                }
                if (docData.mIsHz)
                {
                    re.beizhu += "混规则/";
                    isHZ = true;
                }
                if (docData.mIsDd)
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
                    con.isRFID = docData.mIsRFID;

                    re.content.Add(con);
                }

            }
            catch (System.Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
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

                Thread t = new Thread(new ThreadStart(() =>
                {
                    CheckResult cre = CheckData();
                    HideLoading();

                    if (cre.InventoryResult)
                    {
                        SetInventoryResult(1);
                    }
                    else
                    {
                        SetInventoryResult(1);
                    }
                }));
                t.IsBackground = true;
                t.Start();

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

        private void InventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            if (this.savingDataThread != null)
                this.savingDataThread.Abort();*/

            CloseWindow();
        }

        void restoreGrid(string doc)
        {
            try
            {
                string sql = string.Format("select * from CancelInfo where docNo='{0}' and deviceNo = '{1}' order by timeStamp", doc, SysConfig.DeviceNO);
                DataTable dt = DBHelper.GetTable(sql, false);
                if (dt != null && dt.Rows.Count > 0)
                {
                    grid.Rows.Clear();
                    foreach (DataRow rw in dt.Rows)
                    {
                        List<CChaYi> r = JsonConvert.DeserializeObject<IEnumerable<CChaYi>>(rw["inInfo"].ToString()) as List<CChaYi>;
                        if (r != null)
                        {
                            addGrid(r);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(ex);
            }
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
        public void uploadSAP(CCancelUpload uploadData,out string sapRe,out string sapMsg)
        {
            sapRe = "";
            sapMsg = "";

            CUploadData ud = new CUploadData();
            ud.Guid = Guid.NewGuid().ToString();
            ud.Data = uploadData;
            ud.IsUpload = 0;
            ud.CreateTime = DateTime.Now;
            SqliteDataService.saveToSqlite(ud);

            //upload
            SAPDataService.UploadCancelData(uploadData, ref sapRe, ref sapMsg);

            if (sapRe != "S")
            {
                SqliteDataService.updateMsgToSqlite(ud.Guid, sapMsg);
                playSoundWarn();
            }
            else
            {
                SqliteDataService.delUploadFromSqlite(ud.Guid);
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

        private void ComboBox_Boci_SelectionChangeCommitted(object sender, EventArgs e)
        {
            mDianShuBoCi = ComboBox_Boci.SelectedItem.ToString();
        }

        public void Upload(CUploadData ud)
        {
            string re = "";
            string msg = "";
            uploadSAP(ud.Data as CCancelUpload, out re, out msg);
        }
    }
    public class CChaYi
    {
        public string hu;
        public string bar;
        public string barAdd;
        public string shouldQty;
        public int barChaYiQty;
        public int barAddChaYiQty;
        public bool inventoryRe;
        public string msg;

        public string pin;
        public string se;
        public string gui;

        public string sapRe;
        public string sapMsg;
    }
}
