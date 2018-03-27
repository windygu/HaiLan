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

namespace HLABoxCheckChannelMachine
{
    
    public partial class UploadMsgForm : MetroForm
    {
        bool mSelAll = false;
        public UploadMsgForm()
        {
            InitializeComponent();
        }

        private void initData()
        {
            grid.Rows.Clear();

            List<CUploadData> list = SqliteDataService.GetUnUploadFromSqlite<CJianHuoUpload>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    CJianHuoUpload ju = item.Data as CJianHuoUpload;
                    if (ju != null)
                    {
                        grid.Rows.Insert(0, false, ju.HU, item.MSG);
                        grid.Rows[0].Tag = item;
                    }
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
                if (MetroMessageBox.Show(this, "确认要清除记录吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        CUploadData box = row.Tag as CUploadData;
                        SqliteDataService.delUploadFromSqlite(box.Guid);
                    }
                }
            }
            if (rows != null && rows.Count > 0)
            {
                MetroMessageBox.Show(this, "成功清除", "提示");
                initData();
            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0 && e.RowIndex<grid.Rows.Count)
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
                    UploadServer.GetInstance().AddToQueue(row.Tag);
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

        private void metroButton1_Click(object sender, EventArgs e)
        {
            mSelAll = !mSelAll;
            grid.Focus();

            if(mSelAll)
            {
                grid.SelectAll();
            }
            else
            {
                grid.ClearSelection();
            }

            foreach (DataGridViewRow row in grid.Rows)
            {
                row.Cells[0].Value = mSelAll;
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            initData();
        }

        private void metroButton3_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
