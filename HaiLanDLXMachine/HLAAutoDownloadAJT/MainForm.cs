﻿using System;
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
using HLACommonLib.Model.PACKING;
using HLACommonLib.DAO;
using System.Configuration;
using System.IO;
using OSharp.Utility.Extensions;

namespace HLAManualDownload
{
    public partial class MainForm : MetroForm
    {
        private bool isClosed = false;

        private DateTime? tagInfoUpdateTime = null;
        private DateTime? inventoryOutLogUpdateTime = null;

        private Thread threadTagInfo = null;
        private Thread threadMaterialInfo = null;

        private Thread threadUploadEpc = null;
        private DateTime lastUpdateTime = DateTime.Now;
        private Thread threadOutLog = null;
        private Thread threadShippingLabel = null;
        private Thread threadDeliverEpc = null;
        public MainForm()
        {
            InitializeComponent();
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.isClosed)
                e.Cancel = true;

            this.WindowState = FormWindowState.Minimized;


        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            downloadTabControl.SelectedIndex = 0;

            Thread threadFull = new Thread(new ThreadStart(ThreadWhileFunc));
            threadFull.IsBackground = true;
            threadFull.Start();
        }

        private void button1_UPLOADEPC_Click(object sender, EventArgs e)
        {
            this.button1_UPLOADEPC.Enabled = false;
            threadUploadEpc = new Thread(new ThreadStart(UploadShouHuoEpc));
            threadUploadEpc.IsBackground = true;
            threadUploadEpc.Start();
        }

        private void UploadShouHuoEpc()
        {
            this.Invoke(new Action(() =>
            {
                this.button1_UPLOADEPC.Enabled = false;
                this.pgbEpcInfo.Value = 0;
            }));

            //epc上传
            Dictionary<string, List<string>> dic = LocalDataService.GetUnhandledEpcDetails(HLACommonLib.Model.ENUM.ReceiveType.交货单收货);
            Dictionary<string, List<string>> dic2 = LocalDataService.GetUnhandledEpcDetails(HLACommonLib.Model.ENUM.ReceiveType.交接单收货);

            int total = 0;
            int ti = 0;
            if (dic != null && dic.Count > 0)
            {
                total += dic.Count;
            }
            if (dic2 != null && dic2.Count > 0)
            {
                total += dic2.Count;
            }

            this.Invoke(new Action(() =>
            {
                this.pgbEpcInfo.Maximum = total;
            }));

            if (dic != null && dic.Count > 0)
            {
                foreach (string key in dic.Keys)
                {
                    ti++;
                    SAPDataService.UploadEpcDetails(key, dic[key]);
                    this.Invoke(new Action(() =>
                    {
                        this.pgbEpcInfo.Value = ti;
                    }));
                }
            }

            if (dic2 != null && dic2.Count > 0)
            {
                foreach (string key in dic2.Keys)
                {
                    ti++;
                    SAPDataService.UploadEpcDetails(key, dic2[key]);
                    this.Invoke(new Action(() =>
                    {
                        this.pgbEpcInfo.Value = ti;
                    }));
                }
            }

            this.Invoke(new Action(() =>
            {
                this.button1_UPLOADEPC.Enabled = true;
            }));
            this.lastUpdateTime = DateTime.Now;
        }

        private void ThreadWhileFunc()
        {
            while (true)
            {
                bool forceDown = false;

                DateTime start = DateTime.Now;
                if (DateTime.Now.Hour == 23)
                {
                    if (!this.tagInfoUpdateTime.HasValue || (this.tagInfoUpdateTime.Value.Date.ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd")))
                    {
                        this.tagInfoUpdateTime = DateTime.Now;
                        if (threadTagInfo == null || !threadTagInfo.IsAlive)
                        {
                            DownloadTagInfo();
                        }
                        if (threadMaterialInfo == null || !threadMaterialInfo.IsAlive)
                        {
                            DownloadMaterialInfo();
                        }
                        if (DateTime.Now.Hour > 5)
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
                    }

                }
                


                if ((DateTime.Now - this.lastUpdateTime).TotalMinutes > 3)
                {
                    UploadShouHuoEpc();
                    UploadDeliverEpc();
                }

                Thread.Sleep(100);
            }
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
        private void ShowLog(string message)
        {
            LogHelper.WriteLine(message);
            Invoke(new Action(() => {
                txtLog.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + message + "\r\n");
            }));
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
                        if (!string.IsNullOrEmpty(tag.RFID_EPC))
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
                    List<InventoryOutLogDetailInfo> listBefore = SAPDataService.GetHLAShelvesSingleList(
                        SysConfig.LGNUM, dt.ToString("yyyyMMdd"), item);
                    if(listBefore!=null && listBefore.Count>0)
                    {
                        outLogList.AddRange(listBefore);
                    }
                    //List<InventoryOutLogDetailInfo> list_List = SAPDataService.GetHLASanHeList(SysConfig.LGNUM);
                    //if (list_List != null && list_List.Count > 0)
                    //{
                    //    outLogList.AddRange(list_List);
                    //}
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

            List<ShippingLabel> listBefore = SAPDataService.GetShippingLabelList(
                SysConfig.LGNUM, dt.ToString("yyyyMMdd"));
            if(listBefore!=null && listBefore.Count>0)
            {
                list.AddRange(listBefore);
            }
            if (list != null && list.Count > 0)
            {
                int total = list.Count;
                int i = 0;
                foreach (ShippingLabel item in list)
                {
                    i++;
                    LocalDataService.SaveShippingLabelNew(item);
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
            ShowLog(string.Format("下载{0}条发运标签", list != null ? list.Count : 0));
        }


        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.TopLevel = true;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.isClosed = true;

            this.Close();
        }

        private void btnDownloadOutLog_Click(object sender, EventArgs e)
        {
            this.btnDownloadOutLog.Enabled = false;
            threadOutLog = new Thread(new ThreadStart(DownloadForInventoryOutLog));
            threadOutLog.IsBackground = true;
            threadOutLog.Start();

        }

        private void btnDownloadShippingLabel_Click(object sender, EventArgs e)
        {
            this.btnDownloadShippingLabel.Enabled = false;
            threadShippingLabel = new Thread(new ThreadStart(DownloadForShippingLabel));
            threadShippingLabel.IsBackground = true;
            threadShippingLabel.Start();

        }

        private void btnUploadDeliverEpc_Click(object sender, EventArgs e)
        {
            this.btnUploadDeliverEpc.Enabled = false;
            threadDeliverEpc = new Thread(new ThreadStart(() =>
            {
                UploadDeliverEpc();
            }));
            threadDeliverEpc.IsBackground = true;
            threadDeliverEpc.Start();
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

        private void btnDownloadMaterials_Click(object sender, EventArgs e)
        {
            DownloadMaterialInfo();
        }

        private void btnTagInfo_Click(object sender, EventArgs e)
        {
            DownloadTagInfo();
        }
    }
}
