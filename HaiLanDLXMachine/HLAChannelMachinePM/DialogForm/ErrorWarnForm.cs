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

namespace HLAChannelMachine.DialogForm
{
    public partial class ErrorWarnForm : MetroForm
    {
        private Dictionary<string, TagDetailInfo> mTags = new Dictionary<string, TagDetailInfo>();

        public ErrorWarnForm()
        {
            InitializeComponent();
        }

        private void dmButton1_clear_Click(object sender, EventArgs e)
        {
            mTags.Clear();
            grid.Rows.Clear();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            mTags.Clear();
            grid.Rows.Clear();

            Hide();

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
                    grid.Rows.Insert(0, "", "", "", "", "", errormsg);
                    return;
                }

                if (tags.Count > 0)
                {
                    List<string> matrList = tags.Select(i => i.MATNR).Distinct().ToList();
                    foreach (string mtr in matrList)
                    {
                        TagDetailInfo tg = tags.First(j => j.MATNR == mtr);

                        grid.Rows.Insert(0
                                        , tg != null ? tg.ZSATNR : ""
                                        , tg != null ? tg.ZCOLSN : ""
                                        , tg != null ? tg.ZSIZTX : ""
                                        , tags.Count(i => i.MATNR == mtr && !i.IsAddEpc)
                                        , tags.Count(i => i.MATNR == mtr && i.IsAddEpc)
                                        , errormsg);

                    }
                }
                else
                {
                    grid.Rows.Insert(0, "", "", "", "", "", errormsg);
                }

                AudioHelper.PlayWithSystem(Application.StartupPath + "\\Resources\\warningwav.wav");

            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
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
                    grid.Rows.Insert(0, "", "", "", "", "", errormsg);
                }
                else
                {
                    grid.Rows.Insert(0, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, tag.IsAddEpc ? 0 : 1, tag.IsAddEpc ? 1 : 0, errormsg);
                }

                AudioHelper.PlayWithSystem(Application.StartupPath + "\\Resources\\warningwav.wav");

            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }

        }

    }
}
