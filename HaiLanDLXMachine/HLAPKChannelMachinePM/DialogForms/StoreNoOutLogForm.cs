using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMSkin;
using HLACommonLib;
using HLACommonLib.Model;

namespace HLADeliverChannelMachine.DialogForms
{
    /// <summary>
    /// 门店未下架订单明细
    /// </summary>
    public partial class StoreNoOutLogForm : MetroForm
    {
        /// <summary>
        /// 当前门店的所有未下架的下架单明细
        /// </summary>
        private List<InventoryOutLogDetailInfo> outLogList = null;
        private List<string> pickTaskList = new List<string>();
        private string title = "[{0}]门店未完成的下架单";
        public StoreNoOutLogForm(string store, List<InventoryOutLogDetailInfo> _outLogList)
        {
            InitializeComponent();
            this.outLogList = _outLogList;
            this.Text = string.Format(title, store);
        }

        private void StoreNoOutLogForm_Load(object sender, EventArgs e)
        {
            if (SysConfig.DeviceInfo == null)
            {
                MetroMessageBox.Show(this,
                    "注意，当前设备信息为空，程序或无法正常运行，请尝试重新登录",
                    "警告",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (SysConfig.DeviceInfo.LGTYP == null || SysConfig.DeviceInfo.LGTYP.Count <= 0)
            {
                MetroMessageBox.Show(this,
                    "注意，当前设备未配置存储类型，程序或无法正常运行，请尝试重新登录",
                    "警告",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            //edit begin by wuxw on 2016.1.3  修改未下架单数和总数显示错误问题
            if (outLogList != null && outLogList.Count > 0)
            {
                int totalCount = 0;
                foreach (InventoryOutLogDetailInfo item in outLogList)
                {
                    if (!pickTaskList.Contains(item.PICK_TASK) && SysConfig.DeviceInfo.LGTYP.Contains(item.LGTYP_R))
                    {
                        int count = outLogList.FindAll(i => i.PICK_TASK == item.PICK_TASK).Sum(i => i.QTY);
                        totalCount = totalCount + count;
                        pickTaskList.Add(item.PICK_TASK);
                        grid.Rows.Add(
                            (item.LGTYP != item.LGTYP_R) ? item.PICK_TASK  : item.PICK_TASK, 
                            item.SHIP_DATE.ToString("yyyy-MM-dd"), 
                            item.LGTYP, 
                            item.LGTYP_R,
                            count);
                    }
                }
                this.lblOutLogCount.Text = this.pickTaskList.Count.ToString();
                this.lblTotalCount.Text = totalCount.ToString();
            } 
            else
            {
                this.lblOutLogCount.Text = "0";
                this.lblTotalCount.Text = "0";
            }
            //edit end by wuxw
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
