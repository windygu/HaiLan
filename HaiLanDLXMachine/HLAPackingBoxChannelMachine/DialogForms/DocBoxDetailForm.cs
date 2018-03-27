using DMSkin;
using HLACommonLib.Model.PACKING;
using HLACommonLib.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLAPackingBoxChannelMachine.Models;
using HLAPackingBoxChannelMachine.Utils;
using HLACommonLib;

namespace HLAPackingBoxChannelMachine.DialogForms
{
    public partial class DocBoxDetailForm : MetroForm
    {
        private List<PBBoxInfo> currentBoxList = null;
        public DocBoxDetailForm(List<PBBoxInfo> _boxList)
        {
            InitializeComponent();
            currentBoxList = _boxList;

            DialogResult = DialogResult.Cancel;
        }

        private void DocBoxDetailForm_Load(object sender, EventArgs e)
        {
            updateGrid();
        }

        private void updateGrid()
        {
            grid.Rows.Clear();
            List<PBBoxInfo> boxList = currentBoxList.FindAll(i => i.PACKRESULT == "E" && i.RESULT == "S");
            foreach (PBBoxInfo item in boxList)
            {
                AddBoxDetailGrid(item);
            }
        }

        private void AddBoxDetailGrid(PBBoxInfo box)
        {
            if (box == null) return;
            if (box.Details != null && box.Details.Count > 0)
            {
                List<string> matnrList = box.Details.Select(i => i.MATNR).Distinct().ToList();
                foreach (string item in matnrList)
                {
                    if(box.PACKRESULT == "E")
                    {
                        grid.Rows.Insert(0,false, box.Timestamps.ToString("yyyy-MM-dd"),
                                                box.LH, box.HU, box.PACKRESULT,
                                                box.Details.Find(i => i.MATNR == item).ZSATNR,
                                                box.Details.Find(i => i.MATNR == item).ZCOLSN,
                                                box.Details.Find(i => i.MATNR == item).ZSIZTX,
                                                box.PACKMSG);
                        //if (box.PACKRESULT != "S")
                        //    grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;

                        grid.Rows[0].Tag = box;

                    }
                    else
                    {
                        //grid.Rows.Add(box.Timestamps.ToString("yyyy-MM-dd"),
                        //                        box.LH, box.HU, box.PACKRESULT,
                        //                        box.Details.Find(i => i.MATNR == item).ZSATNR,
                        //                        box.Details.Find(i => i.MATNR == item).ZCOLSN,
                        //                        box.Details.Find(i => i.MATNR == item).ZSIZTX,
                        //                        box.PACKMSG);
                        //if (box.PACKRESULT != "S")
                        //    grid.Rows[grid.Rows.Count-1].DefaultCellStyle.BackColor = Color.OrangeRed;
                    }
                }
            }
            else
            {
                if (box.PACKRESULT == "E")
                {
                    grid.Rows.Insert(0, false,box.Timestamps.ToString("yyyy-MM-dd"),
                    box.LH, box.HU, box.PACKRESULT,
                        "",
                        "",
                        "",
                        box.PACKMSG);
                    //grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;

                    grid.Rows[0].Tag = box;

                }
                else
                {
                    //grid.Rows.Add(box.Timestamps.ToString("yyyy-MM-dd"),
                    //box.LH, box.HU, box.PACKRESULT,
                    //    "",
                    //    "",
                    //    "",
                    //    box.PACKMSG);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<PBBoxInfo> rowBoxList = new List<PBBoxInfo>();

            List<DataGridViewRow> rows = GetCheckedRows();
            if (rows != null && rows.Count > 0)
            {
                foreach (DataGridViewRow row in rows)
                {
                    PBBoxInfo box = row.Tag as PBBoxInfo;
                    if (box != null)
                    {
                        if (!rowBoxList.Exists(r => r.HU == box.HU))
                            rowBoxList.Add(box);
                    }
                }
            }
            if (rowBoxList.Count > 0)
            {
                if (MetroMessageBox.Show(this, "确认清除列表中箱记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    if (PackingBoxService.DeleteBoxByHu(rowBoxList.Select(i => i.HU).Distinct().ToList()))
                    {
                        MetroMessageBox.Show(this, "清除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;

                        currentBoxList.RemoveAll(i => rowBoxList.Exists(j => j.HU == i.HU));
                        updateGrid();
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "清除失败，可能是网络不稳定，请稍候再试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }


            //清除提示
            //if (boxList != null && boxList.Count > 0)
            //{
            //    if (MetroMessageBox.Show(this, "确认清除列表中所有箱记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            //    {
            //        if (PackingBoxService.DeleteBoxByHu(boxList.Select(i => i.HU).Distinct().ToList()))
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

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < grid.Rows.Count)
            {
                grid.Rows[e.RowIndex].Cells[0].Value = !(Boolean)(grid.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void uploadAgainButton_Click(object sender, EventArgs e)
        {
            List<PBBoxInfo> rowBoxList = new List<PBBoxInfo>();

            List<DataGridViewRow> rows = GetCheckedRows();
            if (rows != null && rows.Count > 0)
            {
                foreach (DataGridViewRow row in rows)
                {
                    PBBoxInfo box = row.Tag as PBBoxInfo;
                    if (box != null)
                    {
                        if (!rowBoxList.Exists(r => r.HU == box.HU))
                        {
                            rowBoxList.Add(box);
                        }

                    }
                }
            }

            foreach (PBBoxInfo data in rowBoxList)
            {
                UploadBoxInfo box = new UploadBoxInfo();
                box.EQUIP_HLA = SysConfig.DeviceInfo.EQUIP_HLA;
                box.LGNUM = SysConfig.LGNUM;
                box.LOUCENG = SysConfig.DeviceInfo.LOUCENG;
                box.SUBUSER = SysConfig.CurrentLoginUser.UserId;
                box.Box = data;

                SqliteUploadDataInfo ud = new SqliteUploadDataInfo();
                ud.Guid = Guid.NewGuid().ToString();
                ud.Data = box;
                ud.IsUpload = 0;
                ud.CreateTime = DateTime.Now;
                PackingBoxSqliteService.InsertUploadData(ud);
                UploadServer.GetInstance().CurrentUploadQueue.Push(ud);
            }

            if(rowBoxList.Count>0)
            {
                MetroMessageBox.Show(this, "成功加入上传列表", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
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

    }
}
