using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using HLACommonLib;
using HLACommonLib.Model;
using System.Threading.Tasks;
using DMSkin;
using HLACommonLib.Model.PACKING;
using HLACommonLib.DAO;
using OSharp.Utility.Extensions;

namespace HLAAutoDownload
{
    public partial class DownloadForm : MetroForm
    {
        private bool isClosed = false;
        private DateTime? tagInfoUpdateTime = null;
        private DateTime? inventoryOutLogUpdateTime = null;
        private Thread thread = null;
        private Thread threadOutLog = null;
        private Thread threadShippingLabel = null;
        private Thread threadTagInfo = null;
        private Thread threadUploadEpc = null;
        private Thread threadMaterialInfo = null;
        private Thread threadEbBox = null;
        private Thread threadDeliverEpc = null;
        private Thread threadReturnType = null;
        private DateTime lastUpdateTime = DateTime.Now;
        private DateTime lastDownloadEbBoxTime = DateTime.Now;

        public DownloadForm()
        {
            InitializeComponent();
            this.Text = string.Format("定时上传下载-{0}-{1}", SysConfig.AppServerHost, SysConfig.User);
        }

        private void DownloadForm_Load(object sender, EventArgs e)
        {
            metroTabControl1.SelectTab(0);
            lblSAP.Text = string.Format("SAP地址：{0}  用户名：{1}  客户端：{2}", SysConfig.AppServerHost, SysConfig.User, SysConfig.Client);
            Thread threadFull = new Thread(new ThreadStart(ThreadWhileFunc));
            threadFull.IsBackground = true;
            threadFull.Start();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            this.btnDownload.Enabled = false;
            threadUploadEpc = new Thread(new ThreadStart(DownloadFunc));
            threadUploadEpc.IsBackground = true;
            threadUploadEpc.Start();
        }

        bool isBusy = false;
        private void DownloadEbBox()
        {
            if (isBusy)
                return;
            isBusy = true;
            DateTime shipDate = DateTime.Now;
            int x = 0;
            while (x <= 1)
            {
                this.Invoke(new Action(() =>
                {
                    this.btnDownloadEbBox.Enabled = false;
                    this.pbEbBox.Value = 0;
                }));
                //DateTime shipDate = new DateTime(2015, 11, 25);
                string errormsg;
                List<EbBoxInfo> ebBoxList = null;
                if (EbXCheckBox.Checked)
                {
                    ebBoxList = SAPDataService.GetEbBoxList(SysConfig.LGNUM, "", shipDate.AddDays(x).ToString("yyyy-MM-dd"), "", out errormsg, "S", "X");
                }
                else
                {
                    ebBoxList = SAPDataService.GetEbBoxList(SysConfig.LGNUM, "", shipDate.AddDays(x).ToString("yyyy-MM-dd"), "", out errormsg);
                    //Log4netHelper.LogInfo(shipDate.AddDays(x).ToString("yyyy-MM-dd") + ":" + errormsg);
                }

                if (ebBoxList != null && ebBoxList.Count > 0)
                {
                    List<string> hulist = new List<string>();
                    int total = ebBoxList.Count;
                    int i = 0;
                    if (total > 0)
                    {
                        foreach (EbBoxInfo item in ebBoxList)
                        {
                            i++;
                            LocalDataService.SaveEbBox(item, HLACommonLib.Model.ENUM.CheckType.分拣复核);
                            if (!hulist.Contains(item.HU))
                                hulist.Add(item.HU);
                            this.Invoke(new Action(() =>
                            {
                                this.pbEbBox.Value = i * 100 / total;
                            }));
                        }
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.pbEbBox.Value = 100;
                        }));
                    }

                    SAPDataService.DeleteDownloadedBox(SysConfig.LGNUM, shipDate.AddDays(x), hulist);
                }

                x++;
            }
            lastDownloadEbBoxTime = DateTime.Now;
            isBusy = false;
            this.Invoke(new Action(() =>
            {
                this.btnDownloadEbBox.Enabled = true;
            }));
        }

        private void DownloadFunc()
        {
            this.Invoke(new Action(() =>
            {
                this.btnDownload.Enabled = false;
                this.pgbEpcInfo.Value = 0;
            }));

            //epc上传
            Dictionary<string, List<string>> dic = LocalDataService.GetUnhandledEpcDetails(HLACommonLib.Model.ENUM.ReceiveType.交货单收货);
            if (dic != null && dic.Count > 0)
            {
                int total = dic.Count;
                int i = 0;
                foreach (string key in dic.Keys)
                {
                    i++;
                    SAPDataService.UploadEpcDetails(key, dic[key]);
                    this.Invoke(new Action(() =>
                    {
                        this.pgbEpcInfo.Value = i * 100 / total;
                    }));
                }
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    this.pgbEpcInfo.Value = 100;
                }));
            }
            this.Invoke(new Action(() =>
            {
                this.btnDownload.Enabled = true;
            }));
            this.lastUpdateTime = DateTime.Now;
        }

        private void DownloadReturnType()
        {
            this.Invoke(new Action(() =>
            {
                btnReturnType.Enabled = false;
                pbReturnType.Value = 0;
            }));
            List<ReturnTypeInfo> list = SAPDataService.GetReturnTypeInfo(SysConfig.LGNUM,"");
            if (list != null)
            {

                int i = 0, total = list.Count;
                if (total > 0)
                {

                    foreach (ReturnTypeInfo item in list)
                    {
                        i++;
                        PackingBoxService.SaveReturnType(item);
                        this.Invoke(new Action(() =>
                        {
                            this.pbReturnType.Value = i * 100 / total;
                        }));
                    }
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        this.pbReturnType.Value = 100;
                    }));
                }
            }

            this.Invoke(new Action(() =>
            {
                this.btnReturnType.Enabled = true;
            }));
            ShowLog(string.Format("下载{0}条退货类型", list != null ? list.Count : 0));

        }
        private void UploadDeMaEpcDetail()
        {
            this.Invoke(new Action(() =>
            {
                this.btnDownload.Enabled = false;
                this.pgbEpcInfo.Value = 0;
            }));

            //epc上传
            Dictionary<string, List<string>> dic = LocalDataService.GetUnhandledEpcDetails(HLACommonLib.Model.ENUM.ReceiveType.交接单收货);
            if (dic != null && dic.Count > 0)
            {
                int total = dic.Count;
                int i = 0;
                foreach (string key in dic.Keys)
                {
                    i++;
                    SAPDataService.UploadTransferEpcDetails(key, dic[key]);
                    this.Invoke(new Action(() =>
                    {
                        this.pgbEpcInfo.Value = i * 100 / total;
                    }));
                }
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    this.pgbEpcInfo.Value = 100;
                }));
            }
            this.Invoke(new Action(() =>
            {
                this.btnDownload.Enabled = true;
            }));
            this.lastUpdateTime = DateTime.Now;
        }

        private void DownloadMaterialInfo()
        {
            DataTable table = null;

            //下载物料主数据
            try
            {
                this.Invoke(new Action(() =>
                {
                    btnDownloadMaterials.Enabled = false;
                    pgbMaterialInfo.Value = 100;
                }));
                table = SAPDataService.GetMaterialInfoList(SysConfig.LGNUM);
                int failCount = 0;
                if (table != null && table.Rows.Count > 0)
                {
                    int total = table.Rows.Count;
                    int i = 0;
                    foreach (DataRow row in table.Rows)
                    {
                        i++;
                        MaterialInfo material = new MaterialInfo();
                        material.MATNR = row["MATNR"] != null ? row["MATNR"].ToString() : "";
                        int pxqty;
                        int.TryParse(row["PXQTY"] != null ? row["PXQTY"].ToString() : "0", out pxqty);
                        material.PXQTY = pxqty;
                        int pxqty_fh;
                        int.TryParse(row["PXQTY_FH"] != null ? row["PXQTY_FH"].ToString() : "0", out pxqty_fh);
                        material.PXQTY_FH = pxqty_fh;
                        material.ZCOLSN = row["ZCOLSN"] != null ? row["ZCOLSN"].ToString() : "";
                        material.ZSATNR = row["ZSATNR"] != null ? row["ZSATNR"].ToString() : "";
                        material.ZSIZTX = row["ZSIZTX"] != null ? row["ZSIZTX"].ToString() : "";
                        material.ZSUPC2 = row["ZSUPC2"] != null ? row["ZSUPC2"].ToString() : "";
                        material.PUT_STRA = row["PUT_STRA"] != null ? row["PUT_STRA"].ToString() : "";
                        material.ZCOLSN_WFG = row["ZCOLSN_WFG"] != null ? row["ZCOLSN_WFG"].ToString() : "";
                        material.PXMAT = row["PXMAT"] != null ? row["PXMAT"].ToString() : "";
                        material.PXMAT_FH = row["PXMAT_FH"] != null ? row["PXMAT_FH"].ToString() : "";
                        double brgew;
                        double.TryParse(row["BRGEW"] != null ? row["BRGEW"].ToString() : "0", out brgew);
                        material.BRGEW = brgew;
                        if (!LocalDataService.SaveMaterialInfo(material))
                            failCount++;
                        this.Invoke(new Action(() =>
                        {
                            this.pgbMaterialInfo.Value = i * 100 / total;
                        }));
                    }
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        this.pgbMaterialInfo.Value = 100;
                    }));
                }
                ShowLog(string.Format("下载{0}条物料数据,同步失败{1}条", table?.Rows?.Count, failCount));
                this.Invoke(new Action(() =>
                {
                    this.btnDownloadMaterials.Enabled = true;
                }));

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }
        }

        private void DownloadTagInfo()
        {
            this.Invoke(new Action(() =>
            {
                this.pgbTagInfo.Value = 0;
                this.btnTagInfo.Enabled = false;
            }));

            DataTable table = null;
            //下载吊牌信息
            try
            {
                table = SAPDataService.GetTagInfoList(SysConfig.LGNUM);
                
                if (table != null && table.Rows.Count > 0)
                {
                    int total = table.Rows.Count;
                    int i = 0;
                    foreach (DataRow row in table.Rows)
                    {
                        i++;
                        HLATagInfo tag = new HLATagInfo();
                        tag.MATNR = row["MATNR"].CastTo("");
                        tag.CHARG = row["CHARG"].CastTo("");
                        tag.BARCD = row["BARCD"].CastTo("");
                        tag.BARCD_ADD = row["BARCD_ADD"].CastTo("");
                        tag.RFID_EPC = row["RFID_EPC"].CastTo("").Trim() != "" ? ((string)row["RFID_EPC"]).PadRight(20, '0').Trim() : row["RFID_EPC"].CastTo("").Trim();
                        tag.RFID_ADD_EPC = row["RFID_ADD_EPC"].CastTo("");
                        tag.BARDL = row["BARDL"].CastTo("");
                        tag.LIFNR = row["LIFNR"].CastTo("");

                        //当主epc和辅epc都为空时，跳过，不保存
                        //if (string.IsNullOrEmpty(tag.RFID_EPC) && string.IsNullOrEmpty(tag.RFID_ADD_EPC))
                        //{
                        //    //不作处理
                        //}
                        //else
                        //{
                        if(!string.IsNullOrEmpty(tag.RFID_EPC))
                            LocalDataService.SaveTagInfo(tag);
                        //}
                        this.Invoke(new Action(() =>
                        {
                            this.pgbTagInfo.Value = i * 100 / total;
                        }));
                    }
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        this.pgbTagInfo.Value = 100;
                    }));
                }
                ShowLog(string.Format("下载{0}条吊牌数据", table?.Rows?.Count));

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            this.Invoke(new Action(() =>
            {
                this.btnTagInfo.Enabled = true;
            }));
        }

        private void DownloadForInventoryOutLog()
        {
            this.Invoke(new Action(() =>
            {
                this.btnDownloadOutLog.Enabled = false;
                this.pbgOutlog.Value = 0;
            }));
            List<string> s = SAPDataService.GetProType(SysConfig.LGNUM);
            DateTime dt = DateTime.Now;
            //DateTime dt = new DateTime(2016, 4, 26);
            if (s != null && s.Count > 0)
            {
                List<InventoryOutLogDetailInfo> outLogList = new List<InventoryOutLogDetailInfo>();
                foreach (string item in s)
                {
                    List<InventoryOutLogDetailInfo> list = SAPDataService.GetHLAShelvesSingleList(
                        SysConfig.LGNUM, dt.AddDays(1).ToString("yyyyMMdd"), item);
                    if (list != null && list.Count > 0)
                    {
                        outLogList.AddRange(list);
                    }
                    List<InventoryOutLogDetailInfo> list_List = SAPDataService.GetHLASanHeList(SysConfig.LGNUM);
                    if (list_List != null && list_List.Count > 0)
                    {
                        outLogList.AddRange(list_List);
                    }
                }
                int total = outLogList.Count;
                int i = 0;
                if (total > 0)
                {
                    foreach (InventoryOutLogDetailInfo item in outLogList)
                    {
                        i++;
                        LocalDataService.SaveInventoryOutLogDetail(item);
                        this.Invoke(new Action(() =>
                        {
                            this.pbgOutlog.Value = i * 100 / total;
                        }));
                        //List<MaterialInfo> mList = SAPDataService.GetMaterialInfoListByMATNR(SysConfig.LGNUM, item.PRODUCTNO);
                        //List<HLATagInfo> tList = SAPDataService.GetHLATagInfoListByMATNR(SysConfig.LGNUM, item.PRODUCTNO);
                        //foreach (MaterialInfo mitem in mList)
                        //{
                        //    LocalDataService.SaveMaterialInfo(mitem);
                        //}

                        //foreach (HLATagInfo titem in tList)
                        //{
                        //    LocalDataService.SaveTagInfo(titem);
                        //}
                    }
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        this.pbgOutlog.Value = 100;
                    }));
                }
                ShowLog(string.Format("下载{0}条下架单数据", total));

            }

            this.Invoke(new Action(() =>
            {
                this.btnDownloadOutLog.Enabled = true;
            }));
        }

        private void DownloadForShippingLabel()
        {
            this.Invoke(new Action(() =>
            {
                this.btnDownloadShippingLabel.Enabled = false;
                this.pbgShippingLabel.Value = 0;
            }));
            DateTime dt = DateTime.Now;
            //DateTime dt = new DateTime(2016, 4, 26);
            List<ShippingLabel> list = SAPDataService.GetShippingLabelList(
                SysConfig.LGNUM, dt.AddDays(1).ToString("yyyyMMdd"));
            int failCount = 0;
            if (list != null && list.Count > 0)
            {
                int total = list.Count;
                int i = 0;
                foreach (ShippingLabel item in list)
                {
                    i++;
                    if (!LocalDataService.SaveShippingLabelNew(item))
                        failCount++;
                    this.Invoke(new Action(() =>
                    {
                        this.pbgShippingLabel.Value = i * 100 / total;
                    }));
                }
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    this.pbgShippingLabel.Value = 100;
                }));
            }

            this.Invoke(new Action(() =>
            {
                this.btnDownloadShippingLabel.Enabled = true;
            }));
            ShowLog(string.Format("下载{0}条发运标签，失败{1}条", list != null ? list.Count : 0, failCount));
        }

        private void ThreadWhileFunc()
        {
            while (true)
            {
#if TEST
                return;
#endif
                bool forceDown = false;
                DateTime start = DateTime.Now;
                if (DateTime.Now.Hour == 23)
                {
                    if (!this.tagInfoUpdateTime.HasValue || (this.tagInfoUpdateTime.Value.Date.ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd")))
                    {
                        this.tagInfoUpdateTime = DateTime.Now;
                        if(threadTagInfo == null || !threadTagInfo.IsAlive)
                        {
                            DownloadTagInfo();
                        }
                        if (threadMaterialInfo==null || !threadMaterialInfo.IsAlive)
                        {
                            DownloadMaterialInfo();
                        }
                        if(DateTime.Now.Hour>5)
                        {
                            forceDown = true;
                        }
                    }
                }

                if (DateTime.Now.Hour == 5 || forceDown)//modify by zjr 每天早上5点，下载第二天的下架单
                {
                    if (!this.inventoryOutLogUpdateTime.HasValue || (this.inventoryOutLogUpdateTime.Value.Date.ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd")))
                    {
                        inventoryOutLogUpdateTime = DateTime.Now;
                        //下载下架单
                        DownloadForInventoryOutLog();
                        //下载发运标签信息
                        DownloadForShippingLabel();
                        //下载退货类型
                        DownloadReturnType();
                    }

                }

                if ((DateTime.Now - this.lastUpdateTime).TotalMinutes > 3)
                {
                    DownloadFunc();
                    UploadDeMaEpcDetail();
                    UploadDeliverEpc();
                    UploadInventoryOutEpc();
                }

                if ((DateTime.Now - this.lastDownloadEbBoxTime).TotalMilliseconds > 5000)
                {
                    DownloadEbBox();
                }
                
                Thread.Sleep(100);
            }
        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    //在每天23点进行更新
        //    //if (DateTime.Now.Hour == 23)
        //    //{
        //    //    if (!this.tagInfoUpdateTime.HasValue || (this.tagInfoUpdateTime.Value.Date.ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd")))
        //    //    {
        //    //        this.tagInfoUpdateTime = DateTime.Now;
        //    //        DownloadTagInfo();
        //    //        DownloadMaterialInfo();
        //    //    }
        //    //}

        //    //if ((DateTime.Now - this.lastUpdateTime).TotalMinutes > 3)
        //    //{
        //    //    DownloadFunc();
        //    //}

        //    //if ((DateTime.Now - this.lastDownloadEbBoxTime).TotalMilliseconds > 30000)
        //    //{
        //    //    DownloadEbBox();
        //    //}

        //    ////if (DateTime.Now.ToString("HH:mm:ss") == "04:00:00")
        //    ////{
        //    ////    //凌晨四点，下载下架单
        //    ////    DownloadForInventoryOutLog();
        //    ////    //下载发运标签信息
        //    ////    DownloadForShippingLabel();
        //    ////}

        //    //if (DateTime.Now.ToString("HHmm") == "0400")//零晨二点
        //    //{
        //    //    //下载下架单
        //    //    DownloadForInventoryOutLog();
        //    //    //下载发运标签信息
        //    //    DownloadForShippingLabel();
        //    //}
        //}

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Normal;
            this.TopLevel = true;//.BringToFront();
            //this.Show();
        }

        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.isClosed)
                e.Cancel = true;

            this.WindowState = FormWindowState.Minimized;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.isClosed = true;
            if (this.thread != null)
            {
                this.thread.Abort();
            }

            this.Close();
        }

        private void btnDownloadOutLog_Click(object sender, EventArgs e)
        {
            this.btnDownloadOutLog.Enabled = false;
            threadOutLog = new Thread(new ThreadStart(DownloadForInventoryOutLog));
            threadOutLog.IsBackground = true;
            threadOutLog.Start();
        }

        private void btnDownloadShippingInfo_Click(object sender, EventArgs e)
        {
            this.btnDownloadShippingLabel.Enabled = false;
            threadShippingLabel = new Thread(new ThreadStart(DownloadForShippingLabel));
            threadShippingLabel.IsBackground = true;
            threadShippingLabel.Start();
        }

        private void btnTagInfo_Click(object sender, EventArgs e)
        {
            this.btnTagInfo.Enabled = false;
            threadTagInfo = new Thread(new ThreadStart(DownloadTagInfo));
            threadTagInfo.IsBackground = true;
            threadTagInfo.Start();
        }

        private void btnDownloadMaterials_Click(object sender, EventArgs e)
        {
            this.btnDownloadMaterials.Enabled = false;
            threadMaterialInfo = new Thread(new ThreadStart(DownloadMaterialInfo));
            threadMaterialInfo.IsBackground = true;
            threadMaterialInfo.Start();
        }

        private void btnDownloadEbBox_Click(object sender, EventArgs e)
        {
            this.btnDownloadEbBox.Enabled = false;
            threadEbBox = new Thread(new ThreadStart(DownloadEbBox));
            threadEbBox.IsBackground = true;
            threadEbBox.Start();
        }

        private void btnUploadDeliverEpc_Click(object sender, EventArgs e)
        {
            this.btnUploadDeliverEpc.Enabled = false;
            threadDeliverEpc = new Thread(new ThreadStart(() =>
            {
                UploadDeliverEpc();
                UploadInventoryOutEpc();
            }));
            threadDeliverEpc.IsBackground = true;
            threadDeliverEpc.Start();
        }

        private void UploadInventoryOutEpc()
        {
            this.Invoke(new Action(() =>
            {
                this.btnUploadDeliverEpc.Enabled = false;
                this.pgbDeliverEpc.Value = 0;
            }));

            //发运epc上传
            Dictionary<string, Dictionary<string, string>> dic = LocalDataService.GetUnhandledInventoryOutEpcDetails();
            if (dic != null && dic.Count > 0)
            {
                int total = dic.Count;
                int i = 0;
                foreach (string key in dic.Keys)
                {
                    if (SAPDataService.UploadInventoryOutEpcDetails(key, dic[key].Values.ToList()))
                    {
                        i++;
                        LocalDataService.SetInventoryOutEpcDetailsHandled(dic[key].Keys.ToList());
                        this.Invoke(new Action(() =>
                        {
                            this.pgbDeliverEpc.Value = i * 100 / total;
                        }));
                    }
                }
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    this.pgbDeliverEpc.Value = 100;
                }));
            }
            this.Invoke(new Action(() =>
            {
                this.btnUploadDeliverEpc.Enabled = true;
            }));
        }

        private void UploadDeliverEpc()
        {
            this.Invoke(new Action(() =>
            {
                this.btnUploadDeliverEpc.Enabled = false;
                this.pgbDeliverEpc.Value = 0;
            }));

            //发运epc上传
            Dictionary<string, List<string>> dic = LocalDataService.GetUnhandledDeliverEpcDetails();
            //Dictionary < string, List < string >> dicInventory = LocalDataService
            if (dic != null && dic.Count > 0)
            {
                int total = dic.Count;
                int i = 0;
                foreach (string key in dic.Keys)
                {
                    i++;
                    SAPDataService.UploadDeliverEpcDetails(key, dic[key]);
                    this.Invoke(new Action(() =>
                    {
                        this.pgbDeliverEpc.Value = i * 100 / total;
                    }));
                }
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    this.pgbDeliverEpc.Value = 100;
                }));
            }
            this.Invoke(new Action(() =>
            {
                this.btnUploadDeliverEpc.Enabled = true;
            }));

        }

        private void btnReturnType_Click(object sender, EventArgs e)
        {
            threadReturnType = new Thread(new ThreadStart(DownloadReturnType));
            threadReturnType.IsBackground = true;
            threadReturnType.Start();
        }

        private void ShowLog(string message)
        {
            LogHelper.WriteLine(message);
            Invoke(new Action(() => {
                txtLog.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+" "+message + "\r\n");
            }));
        }

        private void btnMaterialAndTag_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatnr.Text))
                return;
            btnMaterialAndTag.Enabled = false;
            new Thread(new ThreadStart(() => {
                List<string> matnrList = txtMatnr.Text.Split(';').ToList();
                List<MaterialInfo> mList = new List<MaterialInfo>();
                List<HLATagInfo> tList = new List<HLATagInfo>();
                int mFailCount = 0, tFailCount = 0;
                foreach (string matnr in matnrList)
                {
                    List<MaterialInfo> list = SAPDataService.GetMaterialInfoListByMATNR(SysConfig.LGNUM, matnr);
                    List<HLATagInfo> list2 = SAPDataService.GetHLATagInfoListByMATNR(SysConfig.LGNUM, matnr);
                    if (list != null) mList.AddRange(list);
                    if (list2 != null) tList.AddRange(list2);
                }
                Invoke(new Action(() => { pbMaterialAndTag.Maximum = mList.Count + tList.Count; }));
                

                if (pbMaterialAndTag.Maximum > 0)
                {
                    foreach (MaterialInfo m in mList)
                    {
                        Invoke(new Action(() => { pbMaterialAndTag.Value++; }));
                        if (!LocalDataService.SaveMaterialInfo(m))
                            mFailCount++;
                    }

                    foreach (HLATagInfo t in tList)
                    {
                        Invoke(new Action(() => { pbMaterialAndTag.Value++; }));
                        if (!LocalDataService.SaveTagInfo(t))
                            tFailCount++;
                    }
                }
                ShowLog(string.Format("手工下载物料{0}条，失败{1}条", mList.Count, mFailCount));
                ShowLog(string.Format("手工下载吊牌{0}条，失败{1}条", tList.Count, tFailCount));
                Invoke(new Action(() => { btnMaterialAndTag.Enabled = true; }));
            })).Start();
            
        }

        private void btnOutAndShipTag_Click(object sender, EventArgs e)
        {
            btnOutAndShipTag.Enabled = false;
            new Thread(new ThreadStart(() =>
            {
                List<string> s = SAPDataService.GetProType(SysConfig.LGNUM);
                DateTime dt = dtOutAndShipTag.Value;
                List<InventoryOutLogDetailInfo> outLogList = new List<InventoryOutLogDetailInfo>();
                if (s != null && s.Count > 0)
                {
                    foreach (string item in s)
                    {
                        List<InventoryOutLogDetailInfo> list = SAPDataService.GetHLAShelvesSingleList(
                            SysConfig.LGNUM, dt.ToString("yyyyMMdd"), item);
                        if (list != null && list.Count > 0)
                        {
                            outLogList.AddRange(list);
                        }
                        List<InventoryOutLogDetailInfo> list_List = SAPDataService.GetHLASanHeList(SysConfig.LGNUM);
                        if (list_List != null && list_List.Count > 0)
                        {
                            outLogList.AddRange(list_List);
                        }
                    }
                }
                List<ShippingLabel> labelList = SAPDataService.GetShippingLabelList(
                    SysConfig.LGNUM, dt.ToString("yyyyMMdd"));
                if (labelList == null) labelList = new List<ShippingLabel>();
                Invoke(new Action(() => { pbOutAndShipTag.Maximum = (int)(labelList?.Count + outLogList?.Count); }));

                int oFailCount = 0, sFailCount = 0;
                if (pbOutAndShipTag.Maximum >0 )
                {
                    foreach(InventoryOutLogDetailInfo iout in outLogList)
                    {
                        Invoke(new Action(() => { pbOutAndShipTag.Value++; }));
                        if (!LocalDataService.SaveInventoryOutLogDetail(iout))
                            oFailCount++;
                        List<MaterialInfo> mList = SAPDataService.GetMaterialInfoListByMATNR(SysConfig.LGNUM, iout.PRODUCTNO);
                        List<HLATagInfo> tList = SAPDataService.GetHLATagInfoListByMATNR(SysConfig.LGNUM, iout.PRODUCTNO);
                        foreach (MaterialInfo mitem in mList)
                        {
                            LocalDataService.SaveMaterialInfo(mitem);
                        }

                        foreach (HLATagInfo titem in tList)
                        {
                            LocalDataService.SaveTagInfo(titem);
                        }
                    }

                    foreach (ShippingLabel label in labelList)
                    {
                        Invoke(new Action(() => { pbOutAndShipTag.Value++; }));
                        if (!LocalDataService.SaveShippingLabelNew(label))
                            sFailCount++;
                    }
                }
                ShowLog(string.Format("手工下载下架单{0}条，失败{1}条", outLogList.Count, oFailCount));
                ShowLog(string.Format("手工下载发运标签{0}条，失败{1}条", labelList.Count, sFailCount));
                Invoke(new Action(() => { btnOutAndShipTag.Enabled = true; }));
            })).Start();
        }

        private void button1_fenjiefuhe_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime shipDate = fendiefuhe_time.Value;

                List<EbBoxInfo> ebBoxList = null;
                string errormsg = "";
                ebBoxList = SAPDataService.GetEbBoxList(SysConfig.LGNUM, "", shipDate.ToString("yyyy-MM-dd"), "", out errormsg);
                Log4netHelper.LogInfo(shipDate.ToString("yyyy-MM-dd") + ":" + errormsg);
                label14_msg_manual.Text = shipDate.ToString("yyyy-MM-dd") + ":" + errormsg;

                if (ebBoxList != null && ebBoxList.Count > 0)
                {
                    List<string> hulist = new List<string>();
                    foreach (EbBoxInfo item in ebBoxList)
                    {
                        LocalDataService.SaveEbBox(item, HLACommonLib.Model.ENUM.CheckType.分拣复核);
                        if (!hulist.Contains(item.HU))
                            hulist.Add(item.HU);
                    }

                    SAPDataService.DeleteDownloadedBox(SysConfig.LGNUM, shipDate, hulist);
                }
            }
            catch(Exception ep)
            {
                Log4netHelper.LogError(ep);
                label14_msg_manual.Text = ep.ToString();
            }

        }
    }
}
