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
using HLACommonLib.Model.PK;
using HLAPKChannelMachine.Utils;

namespace HLABoxCheckChannelMachine
{
    
    public partial class InventoryForm : CommonInventoryFormIMP
    {
        CLogManager mLog = new CLogManager(true);
        public string mCurBoxNo = "";
        Thread thread = null;
        CJieHuoDan mCurJieHuoDan = new CJieHuoDan();
        public InventoryForm()
        {
            InitializeComponent();
            InitDevice(UHFReaderType.ImpinjR420, true);

        }
        private void InitView()
        {
            Invoke(new Action(() => {
                lblCurrentUser.Text = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "登录信息异常";
                label_curBoxno.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.LOUCENG : "设备信息异常";
                lblPlc.Text = "连接中...";
                lblReader.Text = "连接中...";
                lblWorkStatus.Text = "未开始工作";
                label_deviceNo.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.EQUIP_HLA : "";
                lblCurrentUser.Text = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "";
                label_loucheng.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.LOUCENG : "";

            }));
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            InitView();

            Invoke(new Action(() =>
            {
                btnStart.Enabled = false;
            }));

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

            UploadServer.GetInstance().OnUploaded += UploadServer_OnUploaded;
            UploadServer.GetInstance().Start();

            List<CUploadData> jhUpload = SqliteDataService.GetExpUploadFromSqlite<CJianHuoUpload>();
            if(jhUpload!=null && jhUpload.Count>0)
            {
                foreach (var v in jhUpload)
                {
                    UploadServer.GetInstance().AddToQueue(v);
                }
            }
        }
        private void UploadServer_OnUploaded(CJianHuoUpload data,SapResult re)
        {
            try
            {
                Invoke(new Action(() =>
                {
                    if (!re.SUCCESS)
                    {
                        grid.Rows.Insert(0,mCurBoxNo, mCurJieHuoDan.PICK_LIST, "", "", "", "", "", "", "", "SAP 上传出错："+re.MSG);
                        grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                    }
                }));

            }
            catch (Exception e)
            {
                Log4netHelper.LogError(e);
            }
        }
        private void Start()
        {
            btnStart.Enabled = false;
            btnPause.Enabled = true;
        }
        public override void UpdateView()
        {
        }
        private void Pause()
        {
            btnStart.Enabled = true;
            btnPause.Enabled = false;
        }

        public  void StartInventoryDebug()
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
                    Invoke(new Action(() => {
                        label_curBoxno.Text = mCurBoxNo;
                    }));
                }

                isInventory = true;
                lastReadTime = DateTime.Now;

            }
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
                    Invoke(new Action(() => {
                        label_curBoxno.Text = mCurBoxNo;
                    }));
                }

                reader.StartInventory(0, 0, 0);

                isInventory = true;
                lastReadTime = DateTime.Now;

            }
        }
        private Dictionary<string,CJianHuoContrastRe> contrastEpc()
        {
            Dictionary<string, CJianHuoContrastRe> re = new Dictionary<string, CJianHuoContrastRe>();
            try
            {
                Dictionary<string, int> standard = new Dictionary<string, int>();
                foreach (var v in mCurJieHuoDan.mJieHuo)
                {
                    if (v.QTY != 0)
                    {
                        if (standard.ContainsKey(v.PRODUCTNO))
                        {
                            standard[v.PRODUCTNO] += v.QTY;
                        }
                        else
                        {
                            standard.Add(v.PRODUCTNO, v.QTY);
                        }
                    }
                }
                Dictionary<string, int> tar = new Dictionary<string, int>();
                foreach (var v in tagDetailList)
                {
                    if (!v.IsAddEpc)
                    {
                        if (tar.ContainsKey(v.MATNR))
                        {
                            tar[v.MATNR] += 1;
                        }
                        else
                        {
                            tar.Add(v.MATNR, 1);
                        }
                    }
                }

                foreach (var v in tar)
                {
                    int shortNum = LocalDataService.GetJianHuoShortNum(mCurBoxNo, v.Key);
                    TagDetailInfo ti = tagDetailList.FirstOrDefault(i => i.MATNR == v.Key);
                    if (standard.ContainsKey(v.Key))
                    {
                        CJianHuoContrastRe cr = new CJianHuoContrastRe();
                        cr.mat = v.Key;
                        cr.barcd = ti!=null ? ti.BARCD : "";
                        cr.shouldQty = standard[v.Key];
                        cr.realQty = v.Value;
                        cr.p = ti!=null?ti.ZSATNR:"";
                        cr.s = ti!=null?ti.ZCOLSN:"";
                        cr.g = ti!= null?ti.ZSIZTX:"";
                        cr.shortQty = shortNum;
                        re.Add(v.Key, cr);
                        standard.Remove(v.Key);
                    }
                    else
                    {
                        //多拣
                        CJianHuoContrastRe cr = new CJianHuoContrastRe();
                        cr.mat = v.Key;
                        cr.barcd = ti != null ? ti.BARCD : "";
                        cr.shouldQty = 0;
                        cr.realQty = v.Value;
                        cr.p = ti != null ? ti.ZSATNR : "";
                        cr.s = ti != null ? ti.ZCOLSN : "";
                        cr.g = ti != null ? ti.ZSIZTX : "";
                        re.Add(v.Key, cr);
                    }
                }

                foreach (var v in standard)
                {
                    MaterialInfo mater = materialList.FirstOrDefault(i => i.MATNR == v.Key);

                    CJianHuoContrastRe cr = new CJianHuoContrastRe();
                    cr.mat = v.Key;
                    cr.barcd = v.Key;
                    cr.shouldQty = v.Value;
                    cr.realQty = 0;
                    cr.p = mater != null ? mater.ZSATNR : "";
                    cr.s = mater != null ? mater.ZCOLSN : "";
                    cr.g = mater != null ? mater.ZSIZTX : "";
                    cr.shortQty = LocalDataService.GetJianHuoShortNum(mCurBoxNo, v.Key);

                    re.Add(v.Key, cr);
                }

            }
            catch(Exception e)
            {
                Log4netHelper.LogError(e);
            }
            return re;
        }
        private void addGrid(Dictionary<string, CJianHuoContrastRe> re)
        {
            try
            {
                Invoke(new Action(() =>
                {
                    Dictionary<string, CMatEpc> main = new Dictionary<string, CMatEpc>();

                    foreach (var v in tagDetailList)
                    {
                        if (main.ContainsKey(v.MATNR))
                        {
                            if (v.IsAddEpc)
                                main[v.MATNR].addNum += 1;
                            else
                                main[v.MATNR].mainNum += 1;
                        }
                        else
                        {
                            CMatEpc me = new CMatEpc();
                            if (v.IsAddEpc)
                                me.addNum += 1;
                            else
                                me.mainNum += 1;
                            me.mat = v.MATNR;
                            me.p = v.ZSATNR;
                            me.s = v.ZCOLSN;
                            me.g = v.ZSIZTX;
                            main.Add(v.MATNR, me);
                        }
                    }


                    foreach (var v in re)
                    {
                        string reStr = "正常";

                        bool hasAdd = false;
                        if(main.ContainsKey(v.Key))
                        {
                            if(main[v.Key].addNum!=0)
                            {
                                hasAdd = true;
                            }
                        }
                        if (v.Value.realQty + v.Value.shortQty == v.Value.shouldQty)
                        {
                            reStr = "正常";
                        }
                        if(v.Value.realQty + v.Value.shortQty < v.Value.shouldQty)
                        {
                            reStr = "少拣";
                        }
                        if (v.Value.realQty + v.Value.shortQty > v.Value.shouldQty)
                        {
                            reStr = "多拣";
                        }
                        if (reStr == "正常")
                        {
                            grid.Rows.Add(mCurBoxNo, mCurJieHuoDan.PICK_LIST
                                    , v.Value.p, v.Value.s, v.Value.g
                                    , v.Value.realQty, v.Value.realQty - v.Value.shouldQty
                                    , hasAdd ? v.Value.realQty : 0, hasAdd ? (v.Value.realQty - v.Value.shouldQty) : 0
                                    , reStr);
                        }
                        else
                        {
                            grid.Rows.Insert(0, mCurBoxNo, mCurJieHuoDan.PICK_LIST
                                    , v.Value.p, v.Value.s, v.Value.g
                                    , v.Value.realQty, v.Value.realQty - v.Value.shouldQty
                                    , hasAdd ? v.Value.realQty : 0, hasAdd ? (v.Value.realQty - v.Value.shouldQty) : 0
                                    , reStr);
                            grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;

                        }

                    }

                }));
            }
            catch (Exception e)
            {
                Log4netHelper.LogError(e);
            }
        }
        private bool isContrastReOK(Dictionary<string,CJianHuoContrastRe> re)
        {
            foreach (var v in re)
            {
                if (v.Value.shouldQty != v.Value.realQty + v.Value.shortQty)
                {
                    return false;
                }
            }
            return true;
        }
        CPrintRightData getPrintRightData()
        {
            CPrintRightData re = new CPrintRightData();

            try
            {
                re.hu = mCurBoxNo;
                re.storeType = mCurJieHuoDan.mJieHuo[0].STOTYPE;
                re.jihuodate = mCurJieHuoDan.mJieHuo[0].PICK_DATE;
                re.demawave = mCurJieHuoDan.mJieHuo[0].WAVEID;
                re.daolukou = mCurJieHuoDan.mJieHuo[0].EXPORT_NO;
                re.num = mCurJieHuoDan.mJieHuo.Sum(i => i.QTY).ToString();
            }
            catch(Exception e)
            {
                Log4netHelper.LogError(e);
            }

            return re;
        }
        CPrintErrorData getPrintErrorData()
        {
            CPrintErrorData re = new CPrintErrorData();
            try
            {
                re.hu = mCurBoxNo;
                Dictionary<string, int> tar = new Dictionary<string, int>();
                foreach (var v in tagDetailList)
                {
                    if (!v.IsAddEpc)
                    {
                        if (tar.ContainsKey(v.MATNR))
                        {
                            tar[v.MATNR] += 1;
                        }
                        else
                        {
                            tar.Add(v.MATNR, 1);
                        }
                    }
                }

                foreach(var v in tar)
                {
                    CPSGNum psg = new CPSGNum();
                    TagDetailInfo ti = tagDetailList.FirstOrDefault(i => i.MATNR == v.Key);
                    psg.p = ti != null ? ti.ZSATNR : "";
                    psg.s = ti != null ? ti.ZCOLSN : "";
                    psg.g = ti != null ? ti.ZSIZTX : "";
                    psg.num = v.Value.ToString();
                    re.psg.Add(psg);
                }

            }
            catch (Exception e)
            {
                Log4netHelper.LogError(e);
            }

            return re;
        }
        private void logicCheck(ref CheckResult result)
        {
            try
            {
                Dictionary<string, CJianHuoContrastRe> re = contrastEpc();
                bool conOK = isContrastReOK(re);
                if (!conOK)
                {
                    result.InventoryResult = false;
                    result.UpdateMessage("异常");
                }           
                else
                {
                    result.UpdateMessage(Consts.Default.RIGHT);
                }
                // add grid
                addGrid(re);
                //print
                if(conOK)
                {
                    PrintHelper.PrintRightTag(getPrintRightData());
                }
                else
                {
                    PrintHelper.PrintErrorTag(getPrintErrorData());
                }
                //save upload
                LocalDataService.SaveJianHuoInfo(mCurBoxNo, mCurJieHuoDan.PICK_LIST, re.Values.ToList());

                CJianHuoUpload uploadData = getUploadData(re);

                CUploadData cu = new CUploadData();
                cu.Guid = Guid.NewGuid().ToString();
                cu.IsUpload = 0;
                cu.Data = uploadData;
                cu.CreateTime = DateTime.Now;
                SqliteDataService.saveToSqlite(cu);

                UploadServer.GetInstance().AddToQueue(cu);
            }
            catch(Exception e)
            {
                Log4netHelper.LogError(e);
            }
        }

        public CJianHuoUpload getUploadData(Dictionary<string,CJianHuoContrastRe> conRe)
        {
            CJianHuoUpload re = new CJianHuoUpload();
            try
            {
                bool checkRe = isContrastReOK(conRe);
                re.LGNUM = SysConfig.LGNUM;
                re.SHIP_DATE = mCurJieHuoDan.mJieHuo[0].SHIP_DATE;
                re.HU = mCurBoxNo;
                re.STATUS_IN = checkRe? "S" : "E";
                re.MSG_IN = checkRe ? "正常" : "异常";
                re.SUBUSER = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "登录信息异常";
                re.LOUCENG = SysConfig.DeviceInfo.LOUCENG;
                re.EQUIP_HLA = SysConfig.DeviceInfo.EQUIP_HLA;

                foreach(var v in conRe)
                {
                    CJianHuoUploadBar b = new CJianHuoUploadBar();
                    b.PICK_LIST = mCurJieHuoDan.PICK_LIST;
                    b.BARCD = v.Value.barcd;
                    b.QTY = v.Value.realQty.ToString();
                    b.DJ_QTY = v.Value.shortQty.ToString();
                    b.ERR_QTY = (v.Value.realQty - v.Value.shouldQty + v.Value.shortQty).ToString();
                    re.bars.Add(b);
                }
            }
            catch (Exception e)
            {
                Log4netHelper.LogError(e);
            }

            return re;
        }
         
        public override CheckResult CheckData()
        {
            //基础检查，出错不上传
            CheckResult result = base.CheckData();
            if(string.IsNullOrEmpty(mCurBoxNo))
            {
                result.UpdateMessage("箱号为空");
                result.InventoryResult = false;
            }
            List<BoxPickTaskMapInfo> jiehuodan = LocalDataService.GetBoxPickTaskMapInfoListByHU(mCurBoxNo);
            if(jiehuodan == null)
            {
                result.UpdateMessage("找不到拣货单：" + mCurBoxNo);
                result.InventoryResult = false;
            }
            if(jiehuodan != null && jiehuodan.Count>1)
            {
                result.UpdateMessage("箱号对应多个拣货单：" + mCurBoxNo);
                result.InventoryResult = false;
            }
            if (jiehuodan != null && jiehuodan.Count == 1)
            {
                mCurJieHuoDan = SAPDataService.GetJieHuoDan(SysConfig.LGNUM, jiehuodan[0].PICK_TASK);
                if(mCurJieHuoDan.mJieHuo.Count<=0)
                {
                    result.UpdateMessage("无法获取拣货单信息：" + jiehuodan[0].PICK_TASK);
                    result.InventoryResult = false;
                }
            }

            //基础检查ok
            if (result.InventoryResult)
            {
                //逻辑检查，打印，出错也要上传
                logicCheck(ref result);
            }
            else
            {
                Invoke(new Action(() =>
                {
                    grid.Rows.Add(mCurBoxNo, mCurJieHuoDan.PICK_LIST, "", "", "", "", "", "", "", result.Message);
                }));
            }

            if (result.InventoryResult)
            {
                SetInventoryResult(1);
            }
            else
            {
                //异常口排出
                SetInventoryResult(3);
            }

            return result;
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
                Invoke(new Action(() =>
                {
                    label_inre.Text = cre.Message;
                }));

            }
        }

        public void StopInventoryDebug()
        {
            if (isInventory)
            {
                Invoke(new Action(() =>
                {
                    lblWorkStatus.Text = "停止扫描";
                }));
                isInventory = false;

                CheckResult cre = CheckData();
                Invoke(new Action(() =>
                {
                    label_inre.Text = cre.Message;
                }));

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

        private void dmButton_group_Click(object sender, EventArgs e)
        {
            GxForm form = new GxForm();
            form.ShowDialog();
        }

        private void timer1_uploadStatus_Tick(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                dmButton_uploadlist.Text = string.Format("上传列表（{0}）", SqliteDataService.GetUnUploadCountFromSqlite());
            }));
        }

        private void InventoryForm_Shown(object sender, EventArgs e)
        {
            timer1_uploadStatus.Enabled = true;
        }

        private void dmButton_uploadlist_Click(object sender, EventArgs e)
        {
            UploadMsgForm f = new UploadMsgForm();
            f.ShowDialog();
        }

        private void dmButton_duanjie_Click(object sender, EventArgs e)
        {
            HLAPKChannelMachine.MainForm mf = new HLAPKChannelMachine.MainForm();
            mf.ShowDialog();
        }


        private void InventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1_uploadStatus.Enabled = false;
        }

        private void dmButton1_Click(object sender, EventArgs e)
        {
            DebugForm df = new DebugForm(this);
            df.Show();
        }

    }

    public class CMatEpc
    {
        public string mat = "";
        public string p = "";
        public string s = "";
        public string g = "";
        public int mainNum = 0;
        public int addNum = 0;
    }
    
}
