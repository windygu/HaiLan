using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonLib;
using HLACommonLib.Model;

namespace HLAChannelMachine
{
    public partial class UploadForm : Form
    {
        public UploadForm()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            initData();
        }

        private void UploadForm_Load(object sender, EventArgs e)
        {
            initData();
        }

        private void initData()
        {
            lvData.Items.Clear();
            List<UploadData> list = SqliteDataService.GetUnUploadDataList();
            if (list != null && list.Count > 0)
            {
                btnUpload.Enabled = true;
                foreach (UploadData item in list)
                {
                    ResultDataInfo data = item.Data as ResultDataInfo;
                    ListViewItem lvi = new ListViewItem(data.Doc != null && data.Doc.DOCNO!=null ? data.Doc.DOCNO : "异常单号");
                    lvi.SubItems.Add(data.BoxNO != null ? data.BoxNO : "异常箱码");
                    lvi.SubItems.Add(data.ErrorMsg != null ? data.ErrorMsg : "异常");
                    lvi.SubItems.Add(data.CurrentNum.ToString());
                    lvi.SubItems.Add(item.IsUpload == 0 ? "未上传" : "已上传");
                    lvData.Items.Add(lvi);
                }
            }
            else
            {
                btnUpload.Enabled = false;
                MessageBox.Show("没有未上传的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if(lvData.Items.Count>0)
                MessageBox.Show("已启动重新上传,请稍候刷新界面", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
