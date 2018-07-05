using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMSkin;

namespace HLACommon.Views
{
    public partial class ErrorWarnForm : MetroForm
    {
        private Dictionary<string, TagDetailInfo> mTags = new Dictionary<string, TagDetailInfo>();

        public ErrorWarnForm()
        {
            InitializeComponent();
        }

        private void ErrorWarnForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

        }

        public void showErrorInfo(List<TagDetailInfo> tags, string errormsg)
        {
            try
            {
                if (string.IsNullOrEmpty(errormsg))
                    return;

                if (!Visible)
                {
                    Show();
                    TopMost = true;
                }

                if (tags == null)
                {
                    dataGridView1_showGrid.Rows.Insert(0, "", "", "", "", errormsg);
                    return;
                }

                if (tags.Count > 0)
                {
                    List<string> matrList = tags.Select(i => i.MATNR).Distinct().ToList();
                    foreach (string mtr in matrList)
                    {
                        TagDetailInfo tg = tags.First(j => j.MATNR == mtr);

                        dataGridView1_showGrid.Rows.Insert(0
                                        , tg != null ? tg.ZSATNR : ""
                                        , tg != null ? tg.ZCOLSN : ""
                                        , tg != null ? tg.ZSIZTX : ""
                                        , tags.Count(i => i.MATNR == mtr && !i.IsAddEpc)
                                        , errormsg);

                    }
                }
                else
                {
                    dataGridView1_showGrid.Rows.Insert(0, "", "", "", "", errormsg);
                }

                CommonService.playSound(2);

            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(ex);
            }
        }
        public void showErrorInfo(string epc, TagDetailInfo tag, string errormsg)
        {
            try
            {
                if (string.IsNullOrEmpty(errormsg))
                    return;

                if (!Visible)
                {
                    Show();
                    TopMost = true;
                }

                if (mTags.ContainsKey(epc))
                    return;

                mTags.Add(epc, tag);

                if (tag == null)
                {
                    dataGridView1_showGrid.Rows.Insert(0, "", "", "", "", errormsg);
                }
                else
                {
                    dataGridView1_showGrid.Rows.Insert(0, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, 1, errormsg);
                }

                CommonService.playSound(2);

            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(ex);
            }

        }

        private void button1_clear_Click(object sender, EventArgs e)
        {
            mTags.Clear();
            dataGridView1_showGrid.Rows.Clear();

        }

        private void button3_close_Click(object sender, EventArgs e)
        {
            mTags.Clear();
            dataGridView1_showGrid.Rows.Clear();

            Hide();

        }
    }
}
