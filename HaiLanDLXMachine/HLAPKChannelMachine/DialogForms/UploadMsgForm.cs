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
using System.Windows.Forms;

namespace HLAPKChannelMachine.DialogForms
{
    
    public partial class UploadMsgForm : MetroForm
    {
        public UploadMsgForm()
        {
            InitializeComponent();
        }

        private void initData()
        {
            grid.Rows.Clear();
            List<UploadPKBoxInfo> list = SqliteDataService.GetUnUploadPKBox();
            if (list != null && list.Count > 0)
            {
                foreach (UploadPKBoxInfo item in list)
                {
                    string shipData = "";
                    if(item.SHIP_DATE!=null)
                    {
                        shipData = item.SHIP_DATE.ToString("yyyy-MM-dd");
                    }
                    grid.Rows.Insert(0, false, item.HU, shipData + "：" + (string.IsNullOrEmpty(item.UploadMsg) ? "待上传" : item.UploadMsg));
                    grid.Rows[0].Tag = item;
                }
            }
        }
        private void UploadMgForm_Load(object sender, EventArgs e)
        {
            initData();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rows = GetCheckedRows();
            if (rows != null && rows.Count > 0)
            {
                if (MetroMessageBox.Show(this, "确认要清除箱记录提示?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (DataGridViewRow row in rows)
                    {

                        UploadPKBoxInfo box = row.Tag as UploadPKBoxInfo;
                        List<string> picktaskList = box.DeliverErrorBoxList.Select(i => i.PICK_TASK).Distinct().ToList();
                        //检查该下架单在服务器上是否已经下架
                        bool existNoOut = false;
                        if (picktaskList != null && picktaskList.Count > 0)
                        {
                            foreach (string picktask in picktaskList)
                            {
                                List<InventoryOutLogDetailInfo> list = SAPDataService.GetHLAShelvesSingleTask(SysConfig.LGNUM, picktask);

                                if (list != null && list.Count > 0)
                                {
                                    if (list[0].IsOut != 1)
                                    {
                                        existNoOut = true;
                                        break;
                                    }
                                }
                            }
                        }

                        if (existNoOut)
                        {
                            /*
                            MetroMessageBox.Show(this, string.Format("箱号：{0}，存在未下架的下架单，不允许清除该箱记录！", box.HU), "提示"
                                        , MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        */

                            DialogResult result = MetroMessageBox.Show(this, string.Format("箱号：{0}，存在未下架的下架单，是否还要清除？", box.HU), "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {
                            }
                            else
                            {
                                return;
                            }

                        }
                        if (SqliteDataService.DeleteUploaded(box.Guid))
                        {
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "清除失败", "提示",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    initData();
                }
            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < grid.Rows.Count)
            {
                grid.Rows[e.RowIndex].Cells[0].Value = !(Boolean)(grid.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void btnReupload_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rows = GetCheckedRows();
            if (rows!=null && rows.Count>0)
            {
                foreach (DataGridViewRow row in rows)
                {
                    UploadServer.GetInstance().addToQueue(row.Tag as UploadPKBoxInfo);
                }
                MetroMessageBox.Show(this, "成功加入上传队列", "提示");
            }
        }

        private List<DataGridViewRow> GetCheckedRows()
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>(); 
            if (grid.Rows.Count>0)
            {
                foreach(DataGridViewRow row in grid.Rows)
                {
                    if((Boolean)row.Cells[0].Value)
                    {
                        result.Add(row);
                    }
                }
            }
            return result;
        }

        private void metroButton1_refresh_Click(object sender, EventArgs e)
        {
            initData();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
