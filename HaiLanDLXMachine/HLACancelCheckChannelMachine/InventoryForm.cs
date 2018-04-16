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
    public partial class InventoryForm : CommonInventoryForm
    {
        CLogManager mLog = new CLogManager(true);

        string mCurBoxNo = "";
        Thread thread = null;

        public string mDocNo = "";
        string mTotalBoxNum = "";
        Dictionary<string, CCancelCheckHu> mCancelHuDetail = null;

        string mDianShuBoCi = "01";
        //upload
        private object savingDataLockObject = new object();
        private Queue<CCancelUpload> savingData = new Queue<CCancelUpload>();
        private Thread savingDataThread = null;

        const string BU_ZAI_BEN_XIANG = "不在本箱";
        const string HU_IS_NULL = "箱号为空";
        const string BU_PIPEI = "数量不匹配";
        public InventoryForm()
        {
            InitializeComponent();
            InitDevice(UHFReaderType.ImpinjR420, true);

        }
        public InventoryForm(string docNo,string num)
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

            if(SysConfig.IsTest)
            {
            }

            DataTable dt = LocalDataService.GetCancelReData(mDocNo);

            if(dt!=null && dt.Rows.Count>0)
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

                    grid.Rows.Insert(0, mCurBoxNo, main, add, real, maindif, adddif, msg);

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


                reader.StartReading();
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
            CheckResult result = new CheckResult();
            if (errorEpcNumber > 0)
            {
                result.UpdateMessage(Consts.Default.EPC_WEI_ZHU_CE);
                result.InventoryResult = false;
            }
            if (boxNoList.Count > 0)
            {
                boxNoList.Clear();
                result.UpdateMessage(Consts.Default.XIANG_MA_BU_YI_ZHI);
                result.InventoryResult = false;
            }
            if (epcList.Count == 0)
            {
                result.UpdateMessage(Consts.Default.WEI_SAO_DAO_EPC);
                result.InventoryResult = false;
            }
            if(mCurBoxNo == "")
            {
                result.UpdateMessage(HU_IS_NULL);
                result.InventoryResult = false;
            }

            int mainDif = -1;
            int addDif = -1;
            int notInBoxNum = 0;
            CCancelCheckHu re = piPei(mCurBoxNo, ref notInBoxNum);
            if(re!=null)
            {
                mainDif = re.getMainDif();
                addDif = re.getAddDif();
            }

            if (notInBoxNum > 0)
            {
                result.UpdateMessage(BU_ZAI_BEN_XIANG);
                result.InventoryResult = false;
            }
            if(mainDif!=0 || addDif!=0)
            {
                result.UpdateMessage(BU_PIPEI);
                result.InventoryResult = false;
            }

            int shouldSao = 0;
            if(mCancelHuDetail.ContainsKey(mCurBoxNo))
            {
                shouldSao = mCancelHuDetail[mCurBoxNo].getMainDif();
            }

            grid.Rows.Insert(0, mCurBoxNo, tagDetailList.Count(i => i.IsAddEpc == false)
                , tagDetailList.Count(i => i.IsAddEpc == true)
                , shouldSao, -mainDif, -addDif);

            if(!result.InventoryResult)
            {
                grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
            }

           
            if (result.InventoryResult)
            {
                result.UpdateMessage(Consts.Default.RIGHT);
            }

            //print
            bool isHZ = false;
            Utils.CPrintData printData = getPrintData(re, result,ref isHZ);
            if (result.InventoryResult && printData.beizhu == "")
            {

            }
            else
            {
                Utils.PrintHelper.PrintTag(printData);
            }

            //save inventory re to sql
            LocalDataService.InsertCancelReData(mDocNo, mCurBoxNo, tagDetailList.Count(i => i.IsAddEpc == false)
                , tagDetailList.Count(i => i.IsAddEpc == true)
                , shouldSao, -mainDif, -addDif, errorEpcNumber, notInBoxNum
                , result.Message
                , result.InventoryResult ? 0 : 1);

            //upload
            //if (result.InventoryResult)
            {
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
                //save to sql
                saveUploadToLocal(uploadData);

                addToSavingQueue(uploadData);
            }

            return result;
        }
        private HLACancelCheckChannelMachine.Utils.CPrintData getPrintData(CCancelCheckHu checkRe,CheckResult cr,ref bool isHZ)
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
            catch(System.Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
            }

            return re;
        }
        public List<CCancelUploadSubData> getUploadSubData()
        {
            List<CCancelUploadSubData> re = new List<CCancelUploadSubData>();

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
                reader.StopReading();
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
            lock (savingDataLockObject)
            {
                savingData.Enqueue(uploadData);
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
                        CCancelUpload upData = null;
                        lock (savingDataLockObject)
                        {
                            upData = savingData.Dequeue();
                        }
                        if (upData != null)
                        {
                            //upload
                            string uploadRe = "";
                            string sapMsg = "";
                            SAPDataService.UploadCancelData(upData, ref uploadRe, ref sapMsg);
                            //check and save upload result
                            updateUploadFromSAP(upData.docno, upData.boxno, uploadRe, sapMsg);

                            if (uploadRe == "E")
                            {
                                notifyException();
                            }
                        }
                    }
                    Thread.Sleep(2000);

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
                if (InvokeRequired)
                {
                    if (!IsHandleCreated)
                        CreateHandle();

                    Invoke(new Action(() =>
                    {
                        dmButton2_upload_query.DM_NormalColor = Color.Red;
                    }));
                }
                else
                {
                    dmButton2_upload_query.DM_NormalColor = Color.Red;
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
            }
        }
    }


}
