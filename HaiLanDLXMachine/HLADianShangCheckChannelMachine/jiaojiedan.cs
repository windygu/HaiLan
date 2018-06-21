using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using HLACommonLib;
using HLACommonLib.Model;

namespace HLAJiaoJieCheckChannelMachine
{
    public partial class jiaojiedan : Form
    {
        Form1 mParent = null;
        public jiaojiedan(Form1 parentForm)
        {
            InitializeComponent();
            mParent = parentForm;
        }

        private void button2_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_ok_Click(object sender, EventArgs e)
        {
            label2_downStatus.Text = "正在下载...";
            button1_ok.Enabled = false;
            string doc = textBox1_docno.Text.Trim();
            if(string.IsNullOrEmpty(doc))
            {
                MessageBox.Show("单号不能为空");
                return;
            }

            try
            {
                string errorMsg = "";
                CDianShangDoc jjd = SAPDataService.getDianShangDocData(doc,out errorMsg);
                if (jjd.dsData.Count>0)
                {
                    if (mParent != null)
                    {
                        mParent.loadDoc(jjd);
                    }
                    MessageBox.Show("成功下载");
                    Close();
                }
                else
                {
                    throw new Exception(errorMsg);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "下载出现异常");
            }
        }
    }
}
