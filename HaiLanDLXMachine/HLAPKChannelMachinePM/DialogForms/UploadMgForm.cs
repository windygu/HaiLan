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
using HLADeliverChannelMachine.Utils;

namespace HLADeliverChannelMachine.DialogForms
{
    public partial class UploadMgForm : MetroForm
    {
        public delegate void OutLogOpenHandler(string pick_task);
        public event OutLogOpenHandler OnOutLogOpen;
        public UploadMgForm()
        {
            InitializeComponent();
        }

        private void UploadMgForm_Load(object sender, EventArgs e)
        { 
            // 操作：重新上传，打开下架单，删除（确认）
            List<UploadOutLogDataInfo> list = LocalDataService.GetUnUploadDataList(SysConfig.DeviceNO);
            if (list != null && list.Count > 0)
            {
                foreach (UploadOutLogDataInfo item in list)
                {
                    grid.Rows.Add(item.UploadData.PickTask, item.CreatTime, string.IsNullOrEmpty(item.ErrorMsg) ? "待上传" : item.ErrorMsg,"重新上传","打开","删除");
                    grid.Rows[grid.Rows.Count - 1].Tag = item;
                }
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            { 
                case 3:
                    //重新上传
                    if (!UploadServer.GetInstance().CurrentUploadQueue.Contains(grid.Rows[e.RowIndex].Tag as UploadOutLogDataInfo))
                    {
                        UploadServer.GetInstance().CurrentUploadQueue.Push(grid.Rows[e.RowIndex].Tag as UploadOutLogDataInfo);
                        MetroMessageBox.Show(this, "成功加入上传队列", "提示");
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "该下架单已在上传队列中，请耐心等候", "提示");                        
                    }
                    break;
                case 4:
                    //打开
                    if (OnOutLogOpen == null)
                        return;
                    else
                    {
                        OnOutLogOpen((grid.Rows[e.RowIndex].Tag as UploadOutLogDataInfo).UploadData.PickTask);
                        this.Close();
                    }
                    break;
                case 5:
                    //删除
                    if (MetroMessageBox.Show(this, "确认删除?", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        //检查该下架单在服务器上是否已经下架
                        List<InventoryOutLogDetailInfo> list = SAPDataService.GetHLAShelvesSingleTask(SysConfig.LGNUM, (grid.Rows[e.RowIndex].Tag as UploadOutLogDataInfo).UploadData.PickTask);

                        if(list!=null && list.Count>0)
                        {
                            if(list[0].IsOut != 1)
                            {
                                //已下架,允许删除
                                MetroMessageBox.Show(this, "该下架单未下架，不允许删除！", "提示"
                                    , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        if (LocalDataService.DeleteUploaded((grid.Rows[e.RowIndex].Tag as UploadOutLogDataInfo).Guid))
                        {
                            grid.Rows.RemoveAt(e.RowIndex);
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "删除失败", "提示");
                        }
                    }
                    break;
            }
        }
    }
}
