using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonLib;
using HLACommonLib.DAO;
using HLACommonLib.Model;
using HLACommonLib.Model.YK;
using HLACommonView.Model;
using HLACommonView.Views;
using HLACommonView.Configs;
using Newtonsoft.Json;

namespace HLACancelCheckChannelMachine
{
    public partial class UploadForm : Form
    {
        InventoryForm mParentForm = null;
        string mDocNo;
        public UploadForm(InventoryForm form)
        {
            mDocNo = "";
            mParentForm = form;
            if (mParentForm != null)
            {
                mDocNo = mParentForm.mDocNo;
            }
            InitializeComponent();
        }

        private void getUploadData(string pHu="")
        {
            DataTable dt = null;
            if (pHu == "")
            {
                dt = LocalDataService.GetCancelUpload(mDocNo);
            }
            else
            {
                dt = LocalDataService.GetCancelUpload(mDocNo, pHu);
            }
            if(dt!=null && dt.Rows.Count>0)
            {
                grid.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    string hu = row["hu"].ToString();
                    string isupload = row["isUpload"].ToString().Trim();
                    if (isupload == "1")
                        isupload = "已经上传";
                    if (isupload == "0")
                        isupload = "未上传";
                    string sapRe = row["uploadRe"].ToString().Trim();
                    string sapMsg = row["uploadMsg"].ToString().Trim();
                    if (sapRe == "E")
                    {
                        grid.Rows.Insert(0, false, hu, isupload, sapRe, sapMsg);
                        grid.Rows[0].Tag = JsonConvert.DeserializeObject<CCancelUpload>(row["data"].ToString());
                    }
                    
                }
            }
        }
        private void UploadForm_Load(object sender, EventArgs e)
        {
            getUploadData();
        }

        private void button2_sure_Click(object sender, EventArgs e)
        {
            getUploadData(textBox1_Hu.Text.Trim());
        }

        private void button3_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < grid.Rows.Count)
            {
                grid.Rows[e.RowIndex].Cells[0].Value = !(Boolean)(grid.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void button1_reUpload_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> result = GetCheckedRows();
            foreach(var item in result)
            {
                CCancelUpload upData = (CCancelUpload)item.Tag;

                mParentForm.saveUploadToLocal(upData);
                mParentForm.addToSavingQueue(upData);
            }
            string msg = string.Format("成功加入{0}条数据到上传队列", result.Count);
            MessageBox.Show(msg);

            getUploadData();
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

        private void button1_refresh_Click(object sender, EventArgs e)
        {
            getUploadData();
        }
    }
}
