using DMSkin;
using HLACommonLib;
using HLACommonLib.Model;
using HLACommonLib.Model.PK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLAChannelMachine
{

    public partial class UploadMsgForm : MetroForm
    {
        bool mSelAll = false;
        List<string> mHu = new List<string>();
        public UploadMsgForm(List<string> hu)
        {
            InitializeComponent();

            mHu = hu;
        }

        private void UploadMgForm_Load(object sender, EventArgs e)
        {
            int i = 1;
            foreach (var item in mHu)
            {
                grid.Rows.Insert(0, false, item, "待删除-"+i.ToString());
                grid.Rows[0].Tag = item;
                i++;
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
            if (rows != null && rows.Count > 0)
            {
                foreach (DataGridViewRow row in rows)
                {
                }
                MetroMessageBox.Show(this, "成功删除", "提示");
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

        private void metroButton1_Click(object sender, EventArgs e)
        {
            mSelAll = !mSelAll;

            if(mSelAll)
            {

            }
            else
            {

            }
        }
    }
}
