using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonLib;
using HLACommonLib.DAO;
using HLACommonLib.Model;
using HLACommonLib.Model.YK;
using HLACommonView.Model;
using HLACommonView.Views;
using HLACommonView.Configs;

namespace HLACancelCheckChannelMachine
{
    public partial class ExceptionForm : Form
    {
        string mDocNo;
        public ExceptionForm(string docNo)
        {
            mDocNo = docNo;
            InitializeComponent();
        }

        private void getExpData(string pHu = "")
        {
            DataTable dt = null;
            if (pHu == "")
                dt = LocalDataService.GetCancelReData(mDocNo, false);
            else
                dt = LocalDataService.GetCancelReDataHu(pHu);

            if (dt != null && dt.Rows.Count > 0)
            {
                dataGridView1_Exp.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    string hu = row["hu"].ToString();
                    int main = int.Parse(row["mainNum"].ToString());
                    int add = int.Parse(row["addNum"].ToString());
                    int real = int.Parse(row["realNum"].ToString());
                    int maindif = int.Parse(row["mainDifNum"].ToString());
                    int adddif = int.Parse(row["addDifNum"].ToString());

                    dataGridView1_Exp.Rows.Insert(0, hu, main, add, real, maindif, adddif);
                }
            }
        }
        private void Exception_Load(object sender, EventArgs e)
        {
            getExpData();
        }

        private void button2_sure_Click(object sender, EventArgs e)
        {
            string hu = textBox1_Hu.Text.Trim();
            getExpData(hu);
        }

        private void button3_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
