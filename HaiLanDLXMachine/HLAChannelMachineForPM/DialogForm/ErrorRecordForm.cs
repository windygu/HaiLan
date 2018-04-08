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
        }

        /// <summary>
        /// 更新表格信息
        /// </summary>
        /// <param name="lvi"></param>
        /// <param name="insert"></param>
        public void UpdateRecordInfo(ListViewItem lvi, bool insert)
        {
            this.Invoke(new Action(() =>
            {
                ListViewItem lvitem = lvi.Clone() as ListViewItem;
                Font font = new Font("微软雅黑", 14);
                lvitem.Font = font;
                lvitem.BackColor = Color.White;
                if (insert)
                    this.lvErrorRecord.Items.Insert(0, lvitem);
                else
                    this.lvErrorRecord.Items.Add(lvitem);
            }));
        }

        public void UpdateMonitor(string boxNo, string boxStardand, string scanNum, string errorNum, 
            string workStatus, string epcNum, string inventoryResult, string currentZSATNR, string actualTotalNum,
            string totalBoxNum, ListView lv)
        {
            this.Invoke(new Action(() =>
            {
                this.lblBoxNo.Text = boxNo;
                this.lblBoxStandard.Text = boxStardand;
                this.lblScanNum.Text = scanNum;
                this.lblErrorNum.Text = errorNum;
                this.lblWorkStatus.Text = workStatus;
                this.lblEpcNum.Text = epcNum;
                this.lblInventoryResult.Text = inventoryResult;
                this.lblCurrentZSATNR.Text = currentZSATNR;
                this.lblActualTotalNum.Text = actualTotalNum;
                this.lblTotalBoxNum.Text = totalBoxNum;

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
            }));
        }
    }
}
