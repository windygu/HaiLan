using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLAChannelMachine.Utils;

namespace HLAChannelMachine
{
    public partial class DownloadForm : Form
    {
        public DownloadForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            //下载物料主数据
            DataTable table = SAPDataService.GetMaterialInfoList(SysConfig.LGNUM);
            if (table != null && table.Rows.Count > 0)
            {
                int total = table.Rows.Count;
                int i = 0;
                foreach (DataRow row in table.Rows)
                {
                    i++;
                    LocalDataService.SaveMaterialInfo(row);
                    this.pgbMaterialInfo.Value = i * 100 / total;
                }
            }
            else
            {
                this.pgbMaterialInfo.Value = 100;
            }

            //下载吊牌信息
            table = SAPDataService.GetTagInfoList(SysConfig.LGNUM);
            if (table != null && table.Rows.Count > 0)
            {
                int total = table.Rows.Count;
                int i = 0;
                foreach (DataRow row in table.Rows)
                {
                    i++;
                    LocalDataService.SaveTagInfo(row);
                    this.pgbTagInfo.Value = i * 100 / total;
                }
            }
            else
            {
                this.pgbTagInfo.Value = 100;
            }

            //epc上传
            Dictionary<string, List<string>> dic = LocalDataService.GetUnhandledEpcDetails();
            if (dic != null && dic.Count > 0)
            {
                int total = dic.Count;
                int i = 0;
                foreach (string key in dic.Keys)
                {
                    i++;
                    SAPDataService.UploadEpcDetails(key, dic[key]);
                    this.pgbEpcInfo.Value = i * 100 / total;
                }
            }
            else
            {
                this.pgbEpcInfo.Value = 100;
            }
        }
    }
}
