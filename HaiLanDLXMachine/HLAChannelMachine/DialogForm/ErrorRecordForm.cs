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
    public partial class ErrorRecordForm : Form
    {
        public ErrorRecordForm()
        {
            InitializeComponent();
        }

        private void ErrorRecordForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            foreach (Screen item in Screen.AllScreens)
            {
                if (!item.Primary)
                {
                    this.DesktopBounds = item.Bounds;
                    break;
                }
            }
            /*
            ListViewItem lvi = new ListViewItem("33451276");
            lvi.SubItems.Add("HK1SAFEIBJ1");
            lvi.SubItems.Add("XFL0");
            lvi.SubItems.Add("HK1SAFEIBJ1");
            lvi.SubItems.Add("10");
            lvi.SubItems.Add("不符合箱规；的空间的框架房啥练腹肌；");
            UpdateRecordInfo(lvi, true);
            */
        }

        public void ClearRecordInfo()
        {
            lvErrorRecord.Items.Clear();
        }

        public void ClearPBRecordInfo()
        {
            lvPBErrorRecord.Items.Clear();
        }

        public void CleartDocDetailInfo()
        {
            lvDocDetail.Items.Clear();
        }

        public void ClearPBDetailInfo()
        {
            lvPBDetail.Items.Clear();
        }

        public void SetCurrentDocFlag(bool _IsYupinxiang)
        {
            if (_IsYupinxiang)
            {
                lvDocDetail.Hide();
                lvErrorRecord.Hide();
                lvPBDetail.Show();
                lvPBErrorRecord.Show();
            }
            else
            {
                lvDocDetail.Show();
                lvErrorRecord.Show();
                lvPBDetail.Hide();
                lvPBErrorRecord.Hide();
            }
        }

        /// <summary>
        /// 更新表格信息
        /// </summary>
        /// <param name="lvi"></param>
        /// <param name="insert"></param>
        public void UpdateRecordInfo(ListViewItem lvi, bool insert)
        {
            ListViewItem lvitem = lvi.Clone() as ListViewItem;
            Font font = new Font("微软雅黑", 12, FontStyle.Bold);
            lvitem.Font = font;
            lvitem.BackColor = Color.White;
            this.Invoke(new Action(() =>
            {
                if (insert)
                    lvErrorRecord.Items.Insert(0, lvitem);
                else
                    lvErrorRecord.Items.Add(lvitem);
            }));
            
        }

        public void UpdatePBRecordInfo(ListViewItem lvi,bool insert)
        {
            ListViewItem lvitem = lvi.Clone() as ListViewItem;
            Font font = new Font("微软雅黑", 12, FontStyle.Bold);
            lvitem.Font = font;
            lvitem.BackColor = Color.White;
            this.Invoke(new Action(() =>
            {
                if (insert)
                    lvPBErrorRecord.Items.Insert(0, lvitem);
                else
                    lvPBErrorRecord.Items.Add(lvitem);
            }));
            
        }

        public void UpdateMonitor(string boxNo, string boxStardand, string scanNum, string errorNum, 
            string workStatus, string epcNum, string inventoryResult, string currentZSATNR, string actualTotalNum,
            string totalBoxNum, ListView lv,bool isChecked, bool IsYuPinXiang)
        {
            Invoke(new Action(() =>
            {
                lblBoxNo.Text = boxNo;
                lblBoxStandard.Text = boxStardand;
                lblScanNum.Text = scanNum;
                lblErrorNum.Text = errorNum;
                lblWorkStatus.Text = workStatus;
                lblEpcNum.Text = epcNum;
                lblInventoryResult.Text = inventoryResult;
                lblCurrentZSATNR.Text = currentZSATNR;
                lblActualTotalNum.Text = actualTotalNum;
                lblTotalBoxNum.Text = totalBoxNum;
                cbUseBoxStandard.Checked = isChecked;
                btnSwitchStandardBox.Text = isChecked ? "按箱规收货" : "不按箱规收货";
                if(IsYuPinXiang)
                {
                    bool isExists = false;
                    foreach (ListViewItem docDetailItem in lv.Items)
                    {
                        string itemno = docDetailItem.SubItems[0].Text;
                        string zpbno = docDetailItem.SubItems[1].Text;

                        foreach (ListViewItem item in lvPBDetail.Items)
                        {
                            if (item.SubItems[0].Text == itemno && item.SubItems[1].Text == zpbno)
                            {
                                item.SubItems[0].Text = itemno;
                                item.SubItems[1].Text = docDetailItem.SubItems[1].Text;
                                item.SubItems[2].Text = docDetailItem.SubItems[2].Text;
                                isExists = true;
                                item.Selected = docDetailItem.Selected;

                                break;
                            }
                        }
                        if (!isExists)
                        {
                            ListViewItem item = new ListViewItem(itemno);
                            item.SubItems.Add(zpbno);
                            item.SubItems.Add(docDetailItem.SubItems[2].Text);
                            item.Selected = docDetailItem.Selected;
                            lvPBDetail.Items.Add(item);
                        }
                    }
                }
                else
                {
                    bool isExists = false;
                    foreach (ListViewItem docDetailItem in lv.Items)
                    {
                        string zsatnr = docDetailItem.SubItems[1].Text;
                        string zcolsn = docDetailItem.SubItems[2].Text;
                        string zsiztx = docDetailItem.SubItems[3].Text;
                        string charg = docDetailItem.SubItems[4].Text;

                        foreach (ListViewItem item in this.lvDocDetail.Items)
                        {
                            if (item.SubItems[1].Text == zsatnr && item.SubItems[2].Text == zcolsn
                                && item.SubItems[3].Text == zsiztx && item.SubItems[4].Text == charg)
                            {
                                item.SubItems[0].Text = docDetailItem.SubItems[0].Text;
                                item.SubItems[5].Text = docDetailItem.SubItems[5].Text;
                                item.SubItems[6].Text = docDetailItem.SubItems[6].Text;
                                item.SubItems[7].Text = docDetailItem.SubItems[7].Text;
                                isExists = true;
                                item.Selected = docDetailItem.Selected;

                                break;
                            }
                        }
                        if (!isExists)
                        {
                            ListViewItem item = new ListViewItem(docDetailItem.SubItems[0].Text);
                            item.SubItems.Add(zsatnr);
                            item.SubItems.Add(zcolsn);
                            item.SubItems.Add(zsiztx);
                            item.SubItems.Add(charg);
                            item.SubItems.Add(docDetailItem.SubItems[5].Text);
                            item.SubItems.Add(docDetailItem.SubItems[6].Text);
                            item.SubItems.Add(docDetailItem.SubItems[7].Text);
                            item.Selected = docDetailItem.Selected;
                            this.lvDocDetail.Items.Add(item);
                        }
                    }
                }
                
            }));
        }
    }
}
