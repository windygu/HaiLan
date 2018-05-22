using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonLib.Model;
using HLACommonLib;
using HLACommonLib.Model.ENUM;

namespace HLAChannelMachine
{
    public partial class RecordForm : Form
    {
        ReceiveType type = ReceiveType.交货单收货;
        public RecordForm(ReceiveType _type)
        {
            InitializeComponent();
            cboResult.SelectedIndex = 0;
            type = _type;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string docno = txtDocno.Text.Trim();
            string hu = txtBoxNo.Text.Trim();
            string result = "";
            
            if (this.cboResult.Text == "正常")
            {
                result = "S";
            }
            else if (this.cboResult.Text == "异常")
            {
                result = "E";
            }
            else
            {
                result = "";
            }
            string start = this.dtpStart.Value.ToString("yyyy-MM-dd");
            string end = this.dtpEnd.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            lvRecord.Items.Clear();
            List<ErrorRecord> records = LocalDataService.GetErrorRecordsByHU(hu,start,end,result,docno,SysConfig.Floor, type);
            if (records != null)
            {
                int i = 0;
                foreach (ErrorRecord item in records)
                {
                    i++;
                    ListViewItem lvi = new ListViewItem(i.ToString());
                    lvi.SubItems.Add(item.HU);
                    lvi.SubItems.Add(item.ZSATNR);
                    lvi.SubItems.Add(item.ZCOLSN);
                    lvi.SubItems.Add(item.ZSIZTX);
                    lvi.SubItems.Add(item.QTY.ToString());
                    lvi.SubItems.Add(item.REMARK);
                    lvi.SubItems.Add(item.Timestamp);
                    if (item.RESULT == "E")
                        lvi.BackColor = Color.Red;
                    lvRecord.Items.Add(lvi);
                }
            }
            else
            {
                MessageBox.Show("没有查找到交货记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
