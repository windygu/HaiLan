using DMSkin;
using HLACommonLib.DAO;
using HLACommonLib.Model.YK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLAYKChannelMachine.Utils;
using HLAYKChannelMachine.Models;

namespace HLAYKChannelMachine.DialogForms
{
    public partial class ErrorBoxForm : MetroForm
    {
        private List<YKBoxInfo> currentBoxList = null;
        public ErrorBoxForm( List<YKBoxInfo> _boxList)
        {
            InitializeComponent();
            currentBoxList = _boxList;
            DialogResult = DialogResult.Cancel;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            List<YKBoxInfo> rowBoxList = new List<YKBoxInfo>();

            List<DataGridViewRow> rows = GetCheckedRows();
            if (rows != null && rows.Count > 0)
            {
                foreach (DataGridViewRow row in rows)
                {
                    YKBoxInfo box = row.Tag as YKBoxInfo;
                    if (box != null)
                    {
                        if (!rowBoxList.Exists(r => r.Hu == box.Hu))
                            rowBoxList.Add(box);
                    }
                }
            }
            if(rowBoxList.Count>0)
            {
                if (MetroMessageBox.Show(this, "确认清除列表中箱记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    if (YKBoxService.DeleteBoxByHu(rowBoxList.Select(i => i.Hu).Distinct().ToList()))
                    {
                        MetroMessageBox.Show(this, "清除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;

                        currentBoxList.RemoveAll(i => rowBoxList.Exists(j => j.Hu == i.Hu));
                        updateGrid();
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "清除失败，可能是网络不稳定，请稍候再试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }



            //if (boxList!=null && boxList.Count>0)
            //{
            //    if (MetroMessageBox.Show(this, "确认清除列表中所有箱记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            //    {
            //        if(YKBoxService.DeleteBoxByHu(boxList.Select(i=>i.Hu).Distinct().ToList()))
            //        {
            //            MetroMessageBox.Show(this, "清除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            DialogResult = DialogResult.OK;
            //            Close();
            //        }
            //        else
            //        {
            //            MetroMessageBox.Show(this, "清除失败，可能是网络不稳定，请稍候再试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }
            //}
        }

        private void AddGrid(YKBoxInfo box)
        {
            if (box.Details != null && box.Details.Count > 0)
            {
                List<string> matnrlist = box.Details.Select(i => i.Matnr).Distinct().ToList();
                foreach (string matnr in matnrlist)
                {
                    grid.Rows.Insert(0,false, box.Source, box.Target, box.Hu,
                        box.Details.First(i => i.Matnr == matnr).Zsatnr,
                        box.Details.First(i => i.Matnr == matnr).Zcolsn,
                        box.Details.First(i => i.Matnr == matnr).Zsiztx,
                        box.Details.Count(i => i.Matnr == matnr), box.SapRemark);
                    grid.Rows[0].Tag = box;
                }
            }
            else
            {
                grid.Rows.Insert(0,false, box.Source, box.Target, box.Hu, "", "", "", 0,box.SapRemark);
                grid.Rows[0].Tag = box;
            }
        }

        private void ErrorBoxForm_Load(object sender, EventArgs e)
        {
            updateGrid();
        }
        private void updateGrid()
        {
            List<YKBoxInfo> boxs = currentBoxList.FindAll(i => i.SapStatus == "E" && i.Status == "S");
            grid.Rows.Clear();
            if (boxs != null && boxs.Count > 0)
            {
                foreach (YKBoxInfo item in boxs)
                {
                    AddGrid(item);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private List<DataGridViewRow> GetCheckedRows()
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();
            if (grid.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in grid.Rows)
                {
                    if ((Boolean)row.Cells[0].Value)
                    {
                        result.Add(row);
                    }
                }
            }
            return result;
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0 && e.RowIndex<grid.Rows.Count)
            {
                grid.Rows[e.RowIndex].Cells[0].Value = !(Boolean)(grid.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void uploadAgainButton_Click(object sender, EventArgs e)
        {
            List<YKBoxInfo> rowBoxList = new List<YKBoxInfo>();

            List<DataGridViewRow> rows = GetCheckedRows();
            if (rows != null && rows.Count > 0)
            {
                foreach (DataGridViewRow row in rows)
                {
                    YKBoxInfo box = row.Tag as YKBoxInfo;
                    if(box!=null)
                    {
                        if (!rowBoxList.Exists(r => r.Hu == box.Hu))
                        {
                            rowBoxList.Add(box);
                        }

                    }
                }
            }

            foreach(YKBoxInfo data in rowBoxList)
            {
                SqliteUploadDataInfo ud = new SqliteUploadDataInfo();
                ud.Guid = Guid.NewGuid().ToString();
                ud.Data = data;
                ud.IsUpload = 0;
                ud.CreateTime = DateTime.Now;
                YKBoxSqliteService.InsertUploadData(ud);
                UploadServer.GetInstance().CurrentUploadQueue.Push(ud);
            }

            if (rowBoxList.Count > 0)
            {
                MetroMessageBox.Show(this, "成功加入上传列表", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }

        }

        private void allSelButton_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grid.Rows)
                {
                    row.Cells[0].Value = true;
                }
            }
            catch(Exception ex)
            {
                HLACommonLib.LogHelper.WriteLine(ex.Message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace);
            }

        }
    }
}
