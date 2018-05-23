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
    public partial class InventoryForm : CommonInventoryFormIMP
    {
        CLogManager mLog = new CLogManager(true);

        string mCurBoxNo = "";
        Thread thread = null;

        public string mDocNo = "";
        string mTotalBoxNum = "";
        Dictionary<string, CCancelCheckHu> mCancelHuDetail = null;
        Dictionary<string, List<CCancelBarcdData2>> mCancelHuDetail2 = new Dictionary<string, List<CCancelBarcdData2>>();
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

            grid.Rows.Clear();

            if (SysConfig.IsTest)
            {
            }

            DataTable dt = LocalDataService.GetCancelReData(mDocNo);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string hu = row["hu"].ToString();
                    int main = int.Parse(row["mainNum"].ToString());
                    int add = int.Parse(row["addNum"].ToString());
                    int real = int.Parse(row["realNum"].ToString());
                    int maindif = int.Parse(row["mainDifNum"].ToString());
                    int adddif = int.Parse(row["addDifNum"].ToString());
                    int notreg = int.Parse(row["notReg"].ToString());
                    int notinbox = int.Parse(row["notInBox"].ToString());
                    string msg = row["msg"].ToString();
                    int re = int.Parse(row["re"].ToString());

                    grid.Rows.Insert(0, hu, main, add, real, maindif, adddif, msg);

                    if (re != 0)
                    {
                        grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                    }
                }
            }

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
                mCancelHuDetail = SAPDataService.GetCancelHuDetailList(SysConfig.LGNUM, mDocNo);
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
            btnStart.Enabled = false;
            btnPause.Enabled = true;
        }
        private void Pause()
        {
            btnStart.Enabled = true;
            btnPause.Enabled = false;
        }
        public override void StartInventory()
        {
            if (!isInventory)
            {
                Invoke(new Action(() =>
                {
                    lblWorkStatus.Text = "开始扫描";
                    if (btnStart.Enabled)
                    {
                        Start();
                    }
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

                setBoxNo(mCurBoxNo);


                reader.StartInventory(0, 0, 0);
                isInventory = true;
                lastReadTime = DateTime.Now;

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

            //CCancelCheckHu re = piPei(mCurBoxNo, ref notInBoxNum);
            List<CCheckRe> re = piPei2(mCurBoxNo);
            bool fit = true;
            foreach(var v in re)
            {
                if(!string.IsNullOrEmpty(v.barcdadd))
                {
                    if (v.shouldSao != v.barcdaddnum || v.shouldSao != v.barcdnum)
                        fit = false;
                }
                else
                {
                    if (v.shouldSao != v.barcdnum)
                        fit = false;
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
            foreach (var v in re)
            {
                grid.Rows.Insert(0, mCurBoxNo, v.barcd, v.barcdadd, v.shouldSao, v.barcdnum - v.shouldSao, string.IsNullOrEmpty(v.barcdadd) ? v.barcdaddnum - 0 : v.barcdaddnum - v.shouldSao, result.Message);
                if (!result.InventoryResult)
                {
                    grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                }
            }

            //print
            bool isHZ = false;
            Utils.CPrintData printData = getPrintData(re, result, ref isHZ);
            if (result.InventoryResult && printData.beizhu == "")
            {

            }
            else
            {
                Utils.PrintHelper.PrintTag(printData);
            }

            //save to local
            saveToLocal(mDocNo, mCurBoxNo, result.InventoryResult ? "S" : "E", result.Message, re);

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
        void saveToLocal(string doc, string hu, string re,string msg,List<CCheckRe> data)
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
        private HLACancelCheckChannelMachine.Utils.CPrintData getPrintData(CCancelCheckHu checkRe, CheckResult cr, ref bool isHZ)
        {
            HLACancelCheckChannelMachine.Utils.CPrintData re = new Utils.CPrintData();
            try
            {
                if (checkRe == null)
                    return re;

                re.hu = mCurBoxNo;
                re.inventoryRe = cr.InventoryResult;
                re.totalNum = 0;
                re.beizhu = "";
                if (mCancelHuDetail.ContainsKey(mCurBoxNo))
                {
                    re.totalNum = mCancelHuDetail[mCurBoxNo].getMainDif() + mCancelHuDetail[mCurBoxNo].getAddDif();
                }
                foreach (var v in checkRe.mBarcd)
                {
                    string pin = "";
                    string se = "";
                    string gui = "";
                    TagDetailInfo tInfo = tagDetailList.Find(r => r.BARCD == v.Key);
                    if (tInfo != null)
                    {
                        pin = tInfo.ZSATNR;
                        se = tInfo.ZCOLSN;
                        gui = tInfo.ZSIZTX;
                    }

                    Utils.CPrintContent pCon = re.content.Find(r => (r.pin == pin && r.se == se && r.gui == gui));
                    if (pCon == null)
                    {
                        pCon = new Utils.CPrintContent();
                        re.content.Add(pCon);
                    }

                    pCon.pin = pin;
                    pCon.se = se;
                    pCon.gui = gui;
                    pCon.diff += (-v.Value.mQty);
                    pCon.isRFID = v.Value.mIsRFID;

                    if (v.Value.mIsCp)
                    {
                        re.beizhu += "客诉次品/";
                    }
                    if (v.Value.mIsHz)
                    {
                        re.beizhu += "混规则/";
                        isHZ = true;
                    }
                    if (v.Value.mIsDd)
                    {
                        re.beizhu += "一箱多单/";
                    }
                }
                foreach (var v in checkRe.mBarcdAdd)
                {
                    string pin = "";
                    string se = "";
                    string gui = "";
                    TagDetailInfo tInfo = tagDetailList.Find(r => r.BARCD_ADD == v.Key);
                    if (tInfo != null)
                    {
                        pin = tInfo.ZSATNR;
                        se = tInfo.ZCOLSN;
                        gui = tInfo.ZSIZTX;
                    }

                    Utils.CPrintContent pCon = re.content.Find(r => (r.pin == pin && r.se == se && r.gui == gui));
                    if (pCon == null)
                    {
                        pCon = new Utils.CPrintContent();
                        re.content.Add(pCon);
                    }

                    pCon.pin = pin;
                    pCon.se = se;
                    pCon.gui = gui;
                    pCon.diffAdd += (-v.Value.mQty);
                    pCon.isRFID = v.Value.mIsRFID;
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
        List<CCheckRe> piPei2(string hu)
        {
            List<CCheckRe> re = new List<CCheckRe>();

            if (mCancelHuDetail2 == null || tagDetailList == null || string.IsNullOrEmpty(hu))
                return re;

            if (!mCancelHuDetail2.ContainsKey(hu))
                return re;

            foreach(var v in tagDetailList)
            {
                CCheckRe cr = re.FirstOrDefault(i => i.matr == v.MATNR);
                if(cr == null)
                {
                    CCheckRe r = new CCheckRe();
                    if (v.IsAddEpc)
                    {
                        r.barcdaddnum = 1;
                        if (mCancelHuDetail2[hu].Exists(i=>i.barcdAdd == v.BARCD_ADD))
                        {
                            r.shouldSao = mCancelHuDetail2[hu].FirstOrDefault(i=>i.barcdAdd == v.BARCD_ADD).mQty;
                        }
                        else
                        {
                            r.shouldSao = 0;
                        }
                    }
                    else
                    {
                        r.barcdnum = 1;
                        if (mCancelHuDetail2[hu].Exists(i => i.barcd == v.BARCD))
                        {
                            r.shouldSao = mCancelHuDetail2[hu].FirstOrDefault(i => i.barcd == v.BARCD).mQty;
                        }
                        else
                        {
                            r.shouldSao = 0;
                        }
                    }
                    r.matr = v.MATNR;
                    r.barcd = v.BARCD;
                    r.barcdadd = v.BARCD_ADD;
                    r.pin = v.ZSATNR;
                    r.se = v.ZCOLSN;
                    r.gui = v.ZSIZTX;

                    re.Add(r);
                }
                else
                {
                    if (v.IsAddEpc)
                        cr.barcdaddnum++;
                    else
                        cr.barcdnum++;
                }
            }

            List<CCancelBarcdData2> cd2 = mCancelHuDetail2[hu];
            foreach(var v in cd2)
            {
                if(!re.Exists(i=>i.barcd == v.barcd && i.barcdadd == v.barcdAdd))
                {
                    CCheckRe r = new CCheckRe();

                    r.barcd = v.barcd;
                    r.barcdadd = v.barcdAdd;
                    r.barcdnum = 0;
                    r.barcdaddnum = 0;
                    r.shouldSao = v.mQty;
                    TagDetailInfo t = tagDetailList.FirstOrDefault(i => i.BARCD == v.barcd && i.BARCD_ADD == v.barcdAdd);
                    if(t!=null)
                    {
                        r.matr = t.MATNR;
                        r.pin = t.ZSATNR;
                        r.se = t.ZCOLSN;
                        r.gui = t.ZSIZTX;
                    }

                    re.Add(r);
                }
            }

            return re;
        }
        private CCancelCheckHu piPei(string hu, ref int notInHuNum)
        {
            if (mCancelHuDetail == null || tagDetailList == null)
                return null;

            if (!mCancelHuDetail.ContainsKey(hu))
                return null;

            CCancelCheckHu cck = (CCancelCheckHu)mCancelHuDetail[hu].Clone();
            List<TagDetailInfo> tags = new List<TagDetailInfo>();
            foreach(var t in tagDetailList)
            {
                tags.Add((TagDetailInfo)t.Clone());
            }

            while(tags.Count > 0)
            {
                TagDetailInfo td = tags[0];
                tags.RemoveAt(0);

                if(td.IsAddEpc)
                {
                    if(cck.mBarcdAdd.ContainsKey(td.BARCD_ADD))
                    {
                        cck.mBarcdAdd[td.BARCD_ADD].mQty -= 1;
                    }
                    else
                    {
                        notInHuNum++;
                    }
                }
                else
                {
                    if (cck.mBarcd.ContainsKey(td.BARCD))
                    {
                        cck.mBarcd[td.BARCD].mQty -= 1;
                    }
                    else
                    {
                        notInHuNum++;
                    }
                }
            }

            return cck;
        }
        private int getShouldRev(string hu)
        {
            if (mCancelHuDetail == null)
                return 0;

            if (!mCancelHuDetail.ContainsKey(hu))
                return 0;

            int re = 0;
            CCancelCheckHu cck = mCancelHuDetail[hu];
            foreach(var i in cck.mBarcd)
            {
                re += i.Value.mQty;
            }

            return re;
        }

        private void InventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(savingData.Count>0)
            {
                DialogResult re = MessageBox.Show(this, string.Format("还有{0}条未上传的数据，确定关闭吗", savingData.Count), "", MessageBoxButtons.YesNo);
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
        public void saveUploadToLocal(CCancelUpload data)
        {
            try
            {
                string sql = string.Format("select count(*) from CancelUpload where docNo='{0}' and hu='{1}'", data.docno, data.boxno);
                if (DBHelper.GetValue(sql, false).CastTo<int>(0) > 0)
                {
                    sql = "update CancelUpload set data=@data,isUpload=0,uploadRe='',uploadMsg='',doTime=GETDATE() where docNo=@docNo and hu=@hu";
                    SqlParameter p1 = DBHelper.CreateParameter("@data", JsonConvert.SerializeObject(data));
                    SqlParameter p2 = DBHelper.CreateParameter("@docNo", data.docno);
                    SqlParameter p3 = DBHelper.CreateParameter("@hu", data.boxno);

                    DBHelper.ExecuteSql(sql, false, p1,p2,p3);
                }
                else
                {
                    sql = string.Format("insert into CancelUpload values('{0}','{1}','{2}',0,'','',GETDATE())"
                        , data.docno, data.boxno, JsonConvert.SerializeObject(data));
                    DBHelper.ExecuteSql(sql, false);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        //restore from sql
        private void restoreSavingQueue(string docno)
        {
            try
            {
                string sql = string.Format("select * from CancelUpload where docNo='{0}' and isUpload=0", docno);
                DataTable dt = DBHelper.GetTable(sql, false);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        CCancelUpload cu = JsonConvert.DeserializeObject<CCancelUpload>(row["data"].ToString());

                        addToSavingQueue(cu);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        //update from sap
        private void updateUploadFromSAP(string docno,string hu,string uploadRe,string sapMsg)
        {
            try
            {
                string sql = string.Format("update CancelUpload set isUpload=1,uploadRe='{0}',uploadMsg='{1}',doTime=GETDATE() where docNo='{2}' and hu='{3}'", uploadRe, sapMsg, docno, hu);
                DBHelper.ExecuteSql(sql, false);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
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
                            }
                        }
                    }
                    Thread.Sleep(1000);

                }
                catch (Exception ex)
                {
                    LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
                }
            }
        }

        private void dmButton1_exception_query_Click(object sender, EventArgs e)
        {
            ExceptionForm ef = new ExceptionForm(mDocNo);
            ef.ShowDialog();
        }

        private void dmButton2_upload_query_Click(object sender, EventArgs e)
        {
            dmButton2_upload_query.DM_NormalColor = Color.WhiteSmoke;

            UploadForm uf = new UploadForm(this);
            uf.ShowDialog();
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
                    dmButton2_upload_query.DM_NormalColor = Color.Red;
                }));
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
            }
        }
    }

    public class CCheckRe
    {
        public string matr;
        public string barcd;
        public string barcdadd;
        public int shouldSao;
        public int barcdnum;
        public int barcdaddnum;
        public string pin;
        public string se;
        public string gui;
        public CCheckRe()
        {
            matr = "";
            barcd = "";
            barcdadd = "";
            shouldSao = 0;
            barcdnum = 0;
            barcdaddnum = 0;
            pin = "";
            se = "";
            gui = "";
        }
    }
}
