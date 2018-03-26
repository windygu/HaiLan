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
using HLACommonLib.Model.PACKING;
using HLACommonLib.DAO;
using System.Configuration;
using System.IO;
using OSharp.Utility.Extensions;

namespace HLAManualDownload
{
    public partial class MainForm : MetroForm
    {
        private Thread threadOutLog = null;
        private Thread threadShippingLabel = null;
        private CLogManager mLog = null;
        public MainForm()
        {
            InitializeComponent();
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MetroMessageBox.Show(this, "是否确认退出？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }

            if (threadOutLog != null)
                threadOutLog.Abort();
            if (threadShippingLabel != null)
                threadShippingLabel.Abort();
        }

        private void btnDownloadOutLog_Click(object sender, EventArgs e)
        {
            this.btnDownloadOutLog.Enabled = false;
            threadOutLog = new Thread(new ThreadStart(DownloadForInventoryOutLog));
            threadOutLog.IsBackground = true;
            threadOutLog.Start();
        }

        private void DownloadForInventoryOutLog()
        {
            this.Invoke(new Action(() =>
            {
                this.btnDownloadOutLog.Enabled = false;
                this.pbgOutlog.Value = 0;
            }));
            List<string> s = SAPDataService.GetProType(SysConfig.LGNUM);
            if (s != null && s.Count > 0)
            {
                List<InventoryOutLogDetailInfo> outLogList = new List<InventoryOutLogDetailInfo>();
                foreach (string item in s)
                {
                    List<InventoryOutLogDetailInfo> list = SAPDataService.GetHLAShelvesSingleList(
                        SysConfig.LGNUM, this.dtpDate.Value.Date.ToString("yyyyMMdd"), item);
                    if (list != null)
                        outLogList.AddRange(list);
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
            }

            this.Invoke(new Action(() =>
            {
                this.btnDownloadOutLog.Enabled = true;
            }));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void shipButton_Click(object sender, EventArgs e)
        {
            shipButton.Enabled = false;
            shipProgressBar.Value = 0;
            new Thread(new ThreadStart(() =>
            {
                mLog.log("GetShippingLabelList:" + SysConfig.LGNUM + ":" + this.shipDateTime.Value.Date.ToString("yyyyMMdd"));
                List<ShippingLabel> shipList = SAPDataService.GetShippingLabelList(SysConfig.LGNUM
                    , this.shipDateTime.Value.Date.ToString("yyyyMMdd"));
                if (shipList != null)
                {
                    Invoke(new Action(() => { shipProgressBar.Maximum = shipList.Count; }));

                    int failCount = 0;

                    foreach (ShippingLabel sl in shipList)
                    {
                        Invoke(new Action(() => { shipProgressBar.Value++; }));
                        if (!LocalDataService.SaveShippingLabelNew(sl))
                        {
                            failCount++;
                        }
                    }

                    string log = string.Format("手工下载货运{0}条，失败{1}条", shipList.Count, failCount);
                    Invoke(new Action(() =>
                    {
                        shipLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + log;
                        shipLogLabel.BackColor = failCount > 0 ? Color.Red : Color.White;
                    }));
                }
                else
                {
                    string log = string.Format("手工下载货运0条，失败0条");
                    Invoke(new Action(() =>
                    {
                        shipLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + log;
                        shipLogLabel.BackColor = Color.White;
                    }));
                }

                Invoke(new Action(() => { shipButton.Enabled = true; }));

            })).Start();
        }

        private void inventoryButton_Click(object sender, EventArgs e)
        {
            /*
             * 下面这个函数可以得到所有存储类型
             * List<string> s = SAPDataService.GetProType(SysConfig.LGNUM);
             */
            string storeTypeStr = inventoryStoreTextBox.Text.Trim();
            bool reset = dmCheckBox1_resetIfExist.Checked;
            bool allStoreType = false;

            if (string.IsNullOrWhiteSpace(storeTypeStr))
            {
                DialogResult result = MetroMessageBox.Show(this, "将会下载所有存储类型的下架单数据，是否继续？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }

                //inventoryLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + "存储类型不能为空";
                //return;
                allStoreType = true;
            }

            inventoryButton.Enabled = false;
            inventoryProgressBar.Value = 0;

            new Thread(new ThreadStart(() =>
            {
                mLog.log("GetHLAShelvesSingleList:" + SysConfig.LGNUM + ":" + this.inventoryDateTime.Value.Date.ToString("yyyyMMdd") + ":" + storeTypeStr);

                List<InventoryOutLogDetailInfo> inventoryList = new List<InventoryOutLogDetailInfo>();

                if (allStoreType)
                {
                    List<string> s = SAPDataService.GetProType(SysConfig.LGNUM);
                    if (s != null)
                    {
                        foreach (string storeType in s)
                        {
                            List<InventoryOutLogDetailInfo> inventoryList_t = SAPDataService.GetHLAShelvesSingleList(SysConfig.LGNUM
                                , this.inventoryDateTime.Value.Date.ToString("yyyyMMdd"), storeType);
                            if (inventoryList_t != null && inventoryList_t.Count > 0)
                            {
                                inventoryList.AddRange(inventoryList_t);
                            }
                        }
                    }

                }
                else
                {
                    inventoryList = SAPDataService.GetHLAShelvesSingleList(SysConfig.LGNUM
    , this.inventoryDateTime.Value.Date.ToString("yyyyMMdd"), storeTypeStr);

                }

                List<InventoryOutLogDetailInfo> list_List = SAPDataService.GetHLASanHeList(SysConfig.LGNUM);
                if (list_List != null && list_List.Count > 0)
                {
                    inventoryList.AddRange(list_List);
                }

                if (inventoryList != null)
                {
                    Invoke(new Action(() => { inventoryProgressBar.Maximum = inventoryList.Count; }));

                    int failCount = 0;

                    foreach (InventoryOutLogDetailInfo item in inventoryList)
                    {
                        Invoke(new Action(() => { inventoryProgressBar.Value++; }));

                        if (reset)
                        {
                            if (!LocalDataService.SaveInventoryOutLogDetailWithReset(item))
                            {
                                failCount++;
                            }
                        }
                        else
                        {
                            if (!LocalDataService.SaveInventoryOutLogDetail(item))
                            {
                                failCount++;
                            }
                        }
                    }

                    string log = string.Format("手工下载下架单{0}条，失败{1}条", inventoryList.Count, failCount);
                    Invoke(new Action(() =>
                    {
                        inventoryLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + log;
                        inventoryLogLabel.BackColor = failCount > 0 ? Color.Red : Color.White;

                    }));
                }
                else
                {
                    string log = string.Format("手工下载下架单0条，失败0条");
                    Invoke(new Action(() =>
                    {
                        inventoryLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + log;
                        inventoryLogLabel.BackColor = Color.White;

                    }));
                }

                Invoke(new Action(() => { inventoryButton.Enabled = true; }));

            })).Start();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            mLog = new CLogManager(true);
        }

        private void returnTypeutton_Click(object sender, EventArgs e)
        {
            bool allChecked = returnTypeCheckBox.Checked;

            returnTypeutton.Enabled = false;
            returnTypeProgressBar.Value = 0;

            new Thread(new ThreadStart(() =>
            {
                List<ReturnTypeInfo> rtInfo = SAPDataService.GetReturnTypeInfo(SysConfig.LGNUM, allChecked ? "X" : "");

                if (rtInfo != null)
                {
                    Invoke(new Action(() => { returnTypeProgressBar.Maximum = rtInfo.Count; }));

                    int failCount = 0;

                    foreach (ReturnTypeInfo ri in rtInfo)
                    {
                        Invoke(new Action(() => { returnTypeProgressBar.Value++; }));
                        if (!PackingBoxService.SaveReturnType(ri))
                        {
                            failCount++;
                        }
                    }

                    string log = string.Format("手工下载退货类型{0}条，失败{1}条", rtInfo.Count, failCount);
                    Invoke(new Action(() =>
                    {
                        returnTypeLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + log;
                        returnTypeLabel.BackColor = failCount > 0 ? Color.Red : Color.White;

                    }));
                }
                else
                {
                    string log = string.Format("手工下载退货类型0条，失败0条");
                    Invoke(new Action(() =>
                    {
                        returnTypeLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + log;
                        returnTypeLabel.BackColor = Color.White;
                    }));
                }

                Invoke(new Action(() => { returnTypeutton.Enabled = true; }));
            })).Start();
        }


        private void dateMatTagButton_Click(object sender, EventArgs e)
        {
            matProgressBar.Value = 0;

            if (eDateTime.Value.Date < sDateTime.Value.Date)
            {
                matLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + "结束日期必须大于等于开始日期";
                return;
            }

            dateMatTagButton.Enabled = false;

            new Thread(new ThreadStart(() =>
            {
                List<HLATagInfo> tagList = SAPDataService.GetHLATagInfoListByDate(SysConfig.LGNUM, sDateTime.Value.Date, eDateTime.Value.Date);

                if (tagList == null)
                {
                    matLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + "下载出错";
                    return;
                }

                Invoke(new Action(() => { matProgressBar.Maximum = tagList.Count; }));

                int tagFailCount = 0;
                foreach (HLATagInfo tag in tagList)
                {
                    Invoke(new Action(() => { matProgressBar.Value++; }));
                    if (!string.IsNullOrEmpty(tag.RFID_EPC))
                    {
                        if (!LocalDataService.SaveTagInfo(tag))
                        {
                            tagFailCount++;
                        }
                    }
                }

                string log = string.Format("下载吊牌{0}条，同步失败{1}条"
                    , tagList.Count, tagFailCount);

                Invoke(new Action(() =>
                {
                    matLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + log;
                    if (tagFailCount > 0)
                    {
                        matLogLabel.BackColor = Color.Red;
                    }
                    else
                    {
                        matLogLabel.BackColor = Color.White;
                    }
                }));


                Invoke(new Action(() =>
                {
                    dateMatTagButton.Enabled = true;

                }));

            })).Start();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "delete from ReturnType";
                int c = DBHelper.ExecuteNonQuery(sql);
                MessageBox.Show("成功清除:" + c.ToString() + "行");
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(ex);
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
