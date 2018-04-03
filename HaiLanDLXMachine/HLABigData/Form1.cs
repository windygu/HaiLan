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
using HLACommonLib.Model.PK;
using System.Data.SqlClient;
using System.Configuration;

namespace HLABigData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void uploadVersion(string inputVersion,bool heilan,bool ajt)
        {            
            if(string.IsNullOrEmpty(inputVersion))
            {
                MessageBox.Show("version 为空");
                return;
            }

            //包含几个小数点 17_1.0.0.2.zip
            int dotCount = inputVersion.Count(r => r == '.');
            if(dotCount == 3)
            {
                inputVersion += ".zip";
            }

            dotCount = inputVersion.Count(r => r == '.');
            int xiahuaCount = inputVersion.Count(r => r == '_');
            if(dotCount!=4 || xiahuaCount!=1)
            {
                MessageBox.Show("version 的格式不对");
                return;
            }

            string softwareType = inputVersion.Substring(0, inputVersion.IndexOf('_'));
            string version = "";
            string nohuzui = inputVersion.Substring(0, inputVersion.IndexOf(".zip"));
            version = nohuzui.Substring(nohuzui.IndexOf('_') + 1);

            string updateLog = "update";
            string url = textBox2_downUrl.Text.Trim() + inputVersion;

            if(heilan)
            {
                string sql = "INSERT INTO Versions(Version, DownloadUrl, Timestamp,UpdateLog,SoftwareType) VALUES (@Version,@DownloadUrl,GETDATE(),@UpdateLog,@SoftwareType)";

                SqlParameter p1 = DBHelper.CreateParameter("@Version", version);
                SqlParameter p2 = DBHelper.CreateParameter("@DownloadUrl", url);
                SqlParameter p3 = DBHelper.CreateParameter("@UpdateLog", updateLog);
                SqlParameter p4 = DBHelper.CreateParameter("@SoftwareType", int.Parse(softwareType));

                string conStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
                int count = DBHelper.ExecuteSql(conStr, sql, false, p1, p2, p3, p4);
                if (count > 0)
                    MessageBox.Show("成功更新海澜");
                else
                    MessageBox.Show("更新海澜失败");
            }

            if(ajt)
            {
                string sql = "INSERT INTO Versions(Version, DownloadUrl, Timestamp,UpdateLog,SoftwareType) VALUES (@Version,@DownloadUrl,GETDATE(),@UpdateLog,@SoftwareType)";

                SqlParameter p1 = DBHelper.CreateParameter("@Version", version);
                SqlParameter p2 = DBHelper.CreateParameter("@DownloadUrl", url);
                SqlParameter p3 = DBHelper.CreateParameter("@UpdateLog", updateLog);
                SqlParameter p4 = DBHelper.CreateParameter("@SoftwareType", int.Parse(softwareType));

                string conStr = ConfigurationManager.ConnectionStrings["ConnStrAjt"].ConnectionString;
                int count = DBHelper.ExecuteSql(conStr, sql, false, p1, p2, p3, p4);
                if (count > 0)
                    MessageBox.Show("成功更新【爱居兔】");
                else
                    MessageBox.Show("更新【爱居兔】失败");
            }
        }

        private void button2_updateVs_Click(object sender, EventArgs e)
        {
            foreach(string line in textBox1_versions.Lines)
            {
                uploadVersion(line.Trim(), checkBox1_hl.Checked, checkBox2_ajt.Checked);
            }
        }
    }
}
