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
            if(SysConfig.LGNUM == "HL01")
            {
                label1_hora.Text = "海澜之家";
            }
            else if(SysConfig.LGNUM == "ET01")
            {
                label1_hora.Text = "爱居兔";
            }
            else
            {
                label1_hora.Text = "未知";

            }
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
                List<HLATagInfo> tagList = SAPDataService.GetHLATagInfoListByDate(SysConfig.LGNUM, sDateTime.Value.Date.ToString("yyyyMMdd"), eDateTime.Value.Date.ToString("yyyyMMdd"));

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

        private void button1_alltag_Click(object sender, EventArgs e)
        {
            button1_alltag.Enabled = false;
            matProgressBar.Value = 0;

            if (DialogResult.No == MessageBox.Show(this, "要下载所有全部吊牌？这会耗时比较久", "", MessageBoxButtons.YesNo))
            {
                return;
            }

            new Thread(new ThreadStart(() =>
            {
                List<HLATagInfo> tagList = SAPDataService.GetTagInfoList(SysConfig.LGNUM);

                if (tagList != null)
                {
                    Invoke(new Action(() => { matProgressBar.Maximum = tagList.Count; }));

                    int tagFailCount = 0;

                    foreach (HLATagInfo tag in tagList)
                    {
                        Invoke(new Action(() => { matProgressBar.Value++; }));

                        if (!LocalDataService.SaveTagInfo(tag))
                        {
                            tagFailCount++;
                        }
                    }

                    string log = string.Format("手工下载吊牌{0}条，失败{1}条", tagList.Count, tagFailCount);
                    Invoke(new Action(() =>
                    {
                        matLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + log;
                        matLogLabel.BackColor = tagFailCount > 0 ? Color.Red : Color.White;

                    }));
                }
                else
                {
                    string log = string.Format("手工下载吊牌0条，失败0条");
                    Invoke(new Action(() =>
                    {
                        matLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + log;
                        matLogLabel.BackColor = Color.White;

                    }));
                }

                Invoke(new Action(() =>
                {
                    dateMatTagButton.Enabled = true;
                    button1_alltag.Enabled = true;
                }));

            })).Start();


    }

        private void button1_allmat_Click(object sender, EventArgs e)
        {
            matProgressBar.Value = 0;
            button1_allmat.Enabled = false;

            if (DialogResult.No == MessageBox.Show(this, "要下载所有全部物料？这会耗时比较久", "", MessageBoxButtons.YesNo))
            {
                return;
            }

            new Thread(new ThreadStart(() =>
            {
                List<MaterialInfo> mtrList = SAPDataService.GetMaterialInfoList(SysConfig.LGNUM);

                if (mtrList != null)
                {
                    Invoke(new Action(() => { matProgressBar.Maximum = mtrList.Count; }));

                    int matFailCount = 0;

                    foreach (MaterialInfo m in mtrList)
                    {
                        Invoke(new Action(() => { matProgressBar.Value++; }));

                        if (!LocalDataService.SaveMaterialInfo(m))
                        {
                            matFailCount++;
                        }
                    }

                    string log = string.Format("手工下载物料{0}条，失败{1}条", mtrList.Count, matFailCount);
                    Invoke(new Action(() =>
                    {
                        matLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + log;
                        matLogLabel.BackColor = matFailCount > 0 ? Color.Red : Color.White;

                    }));
                }
                else
                {
                    string log = string.Format("手工下载物料0条，失败0条");
                    Invoke(new Action(() =>
                    {
                        matLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + log;
                        matLogLabel.BackColor = Color.White;

                    }));
                }

                Invoke(new Action(() =>
                {
                    button1_allmat.Enabled = true;
                }));

            })).Start();
        }

        private void button1_downloadmat_Click(object sender, EventArgs e)
        {
            matProgressBar.Value = 0;

            if (eDateTime.Value.Date < sDateTime.Value.Date)
            {
                matLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + "结束日期必须大于等于开始日期";
                return;
            }

            button1_downloadmat.Enabled = false;
            dateMatTagButton.Enabled = false;

            new Thread(new ThreadStart(() =>
            {
                List<MaterialInfo> mtrList = SAPDataService.GetMaterialInfoListByDate(SysConfig.LGNUM, sDateTime.Value.Date.ToString("yyyyMMdd"), eDateTime.Value.Date.ToString("yyyyMMdd"));

                if (mtrList == null)
                {
                    matLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + "下载出错";
                    return;
                }

                Invoke(new Action(() => { matProgressBar.Maximum = mtrList.Count; }));

                int matFailCount = 0;
                foreach (MaterialInfo m in mtrList)
                {
                    Invoke(new Action(() => { matProgressBar.Value++; }));
                    if (!LocalDataService.SaveMaterialInfo(m))
                    {
                        matFailCount++;
                    }
                }


                string log = string.Format("下载物料{0}条，同步失败{1}条"
                    , mtrList.Count, matFailCount);

                Invoke(new Action(() =>
                {
                    matLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + log;
                    if (matFailCount > 0)
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
                    button1_downloadmat.Enabled = true;
                    dateMatTagButton.Enabled = true;

                }));

            })).Start();

        }

        private void matFileSelButton_Click(object sender, EventArgs e)
        {
            List<string> mtrList = new List<string>();

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                selFileNameTextBox.Text = openFileDialog1.FileName;
            }

        }

        private void matFileDownButton_Click(object sender, EventArgs e)
        {
            matProgressBar.Value = 0;

            if (selFileNameTextBox.Text == "")
            {
                matLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + "请选择sku文件";
                return;
            }

            List<string> mtrList = new List<string>();

            try
            {
                string file = openFileDialog1.FileName;
                var lines = File.ReadLines(file);
                foreach (var line in lines)
                {
                    string lineStr = line.Trim();
                    if (lineStr.StartsWith("H"))
                    {
                        mtrList.Add(line);
                    }

                }
            }
            catch (Exception ex)
            {
                mLog.log(ex.Message);
                mLog.log(ex.StackTrace);
            }

            if (mtrList.Count <= 0)
            {
                matLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + "该文件中没有sku数据";
                return;
            }

            dateMatTagButton.Enabled = false;
            matFileDownButton.Enabled = false;

            new Thread(new ThreadStart(() =>
            {
                int matTotalCount = 0;
                int matFailCount = 0;

                int tagTotalCount = 0;
                int tagFailCount = 0;
                Invoke(new Action(() => { matProgressBar.Maximum = mtrList.Count; }));

                foreach (string mtr in mtrList)
                {
                    Invoke(new Action(() => { matProgressBar.Value++; }));

                    List<MaterialInfo> matList = SAPDataService.GetMaterialInfoListByMATNR(SysConfig.LGNUM, mtr);
                    List<HLATagInfo> tagList = SAPDataService.GetHLATagInfoListByMATNR(SysConfig.LGNUM, mtr);
                    if (matList != null)
                    {
                        matTotalCount += matList.Count;
                        foreach (MaterialInfo m in matList)
                        {
                            if (!LocalDataService.SaveMaterialInfo(m))
                            {
                                matFailCount++;
                                mLog.log("SaveMaterialInfo fail:" + m.MATNR);
                            }
                        }
                    }
                    if(tagList!=null)
                    {
                        tagTotalCount += tagList.Count;
                        foreach(HLATagInfo t in tagList)
                        {
                            if(!LocalDataService.SaveTagInfo(t))
                            {
                                tagFailCount++;
                                mLog.log("SaveTagInfo fail:" + t.MATNR);
                            }
                        }
                    }

                }

                string log = string.Format("下载物料{0}条，同步失败{1}条，下载吊牌{2}条，同步失败{3}条", matTotalCount, matFailCount, tagTotalCount, tagFailCount);
                Invoke(new Action(() =>
                {
                    matLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + log;
                    if (matFailCount > 0)
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
                    matFileDownButton.Enabled = true;
                }));

            })).Start();

        }

        private void button1_singlemat_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(metroTextBox1_singlemat.Text))
            {
                matLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + "请输入产品编码";
                return;
            }
            matProgressBar.Value = 0;

            List<string> mtrList = new List<string>();
            mtrList.Add(metroTextBox1_singlemat.Text.Trim());

            new Thread(new ThreadStart(() =>
            {
                int matTotalCount = 0;
                int matFailCount = 0;

                int tagTotalCount = 0;
                int tagFailCount = 0;
                Invoke(new Action(() => { matProgressBar.Maximum = mtrList.Count; }));

                foreach (string mtr in mtrList)
                {
                    Invoke(new Action(() => { matProgressBar.Value++; }));

                    List<MaterialInfo> matList = SAPDataService.GetMaterialInfoListByMATNR(SysConfig.LGNUM, mtr);
                    List<HLATagInfo> tagList = SAPDataService.GetHLATagInfoListByMATNR(SysConfig.LGNUM, mtr);
                    if (matList != null)
                    {
                        matTotalCount += matList.Count;
                        foreach (MaterialInfo m in matList)
                        {
                            if (!LocalDataService.SaveMaterialInfo(m))
                            {
                                matFailCount++;
                                mLog.log("SaveMaterialInfo fail:" + m.MATNR);
                            }
                        }
                    }
                    if (tagList != null)
                    {
                        tagTotalCount += tagList.Count;
                        foreach (HLATagInfo t in tagList)
                        {
                            if (!LocalDataService.SaveTagInfo(t))
                            {
                                tagFailCount++;
                                mLog.log("SaveTagInfo fail:" + t.MATNR);
                            }
                        }
                    }

                }

                string log = string.Format("下载物料{0}条，同步失败{1}条，下载吊牌{2}条，同步失败{3}条", matTotalCount, matFailCount, tagTotalCount, tagFailCount);
                Invoke(new Action(() =>
                {
                    matLogLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + log;
                    if (matFailCount > 0)
                    {
                        matLogLabel.BackColor = Color.Red;
                    }
                    else
                    {
                        matLogLabel.BackColor = Color.White;
                    }
                }));

            })).Start();
        }
    }
}
