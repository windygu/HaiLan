using DMSkin;
using HLACommonLib;
using HLACommonLib.Model;
using HLACommonLib.Model.PK;
using HLAPKChannelMachine.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HLAPKChannelMachine.DialogForms
{
    
    public partial class GetDataForm : MetroForm
    {
        public GetDataForm()
        {
            InitializeComponent();
        }

        private void shipButton_Click(object sender, EventArgs e)
        {
            shipButton.Enabled = false;
            shipProgressBar.Value = 0;
            new Thread(new ThreadStart(() =>
            {
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

                if (inventoryList != null)
                {
                    Invoke(new Action(() => { inventoryProgressBar.Maximum = inventoryList.Count; }));

                    int failCount = 0;

                    foreach (InventoryOutLogDetailInfo item in inventoryList)
                    {
                        Invoke(new Action(() => { inventoryProgressBar.Value++; }));

                        if (!LocalDataService.SaveInventoryOutLogDetail(item))
                        {
                            failCount++;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
