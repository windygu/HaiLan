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

namespace HLAChannelMachine
{
    public partial class DanJieForm : MetroForm
    {
        private InventoryFormNew mParentForm;
        private Dictionary<string, TagDetailInfo> mTags = new Dictionary<string, TagDetailInfo>();
        public DanJieForm(InventoryFormNew pForm)
        {
            InitializeComponent();
            mParentForm = pForm;
        }

        public void reportTag(string epc)
        {
            if (!mTags.ContainsKey(epc))
            {
                TagDetailInfo tag = mParentForm.GetTagDetailInfoByEpc(epc);
                mTags.Add(epc, tag);

                if(tag!=null)
                {
                    grid.Rows.Insert(0,tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, tag.IsAddEpc ? "副条码" : "主条码", epc, "删除");
                    grid.Rows[0].Tag = epc;
                }
                else
                {
                    grid.Rows.Insert(0, "", "", "", "", epc, "删除");
                    grid.Rows[0].Tag = epc;
                }
            }
        }



        private void btnReturn_Click(object sender, EventArgs e)
        {
            mParentForm.setDanJie(false);
            this.Hide();
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 5:
                    if (e.RowIndex >= 0 && e.RowIndex < grid.Rows.Count)
                    {
                        string epc = (string)grid.Rows[e.RowIndex].Tag;

                        if (DialogResult.OK == MessageBox.Show(this, "确认删除epc:" + epc, "", MessageBoxButtons.OKCancel))
                        {
                            string msg = "";
                            if (!mParentForm.delEpcInDanJie(epc, out msg))
                            {
                                MessageBox.Show(this, msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                foreach(DataGridViewRow row in grid.Rows)
                                {
                                    if(row.Tag as string == epc)
                                    {
                                        grid.Rows.Remove(row);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    //删除
                    break;
            }
        }

        private void metroButton2_clear_Click(object sender, EventArgs e)
        {
            mTags.Clear();
            grid.Rows.Clear();
        }

        private void metroButton1_del_Click(object sender, EventArgs e)
        {

        }
    }
}
