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
    public partial class DocSel : Form
    {
        List<KeyValuePair<string, int>> mDoxBoxNum = new List<KeyValuePair<string, int>>();
        string mDocno = "";
        string mBoxnum = "";

        public DocSel()
        {
            InitializeComponent();
        }

        private void button1_selOK_Click(object sender, EventArgs e)
        {
            if (!SysConfig.IsTest)
            {
                if (mDocno == "" || mBoxnum == "")
                {
                    MessageBox.Show("请选择点数波次");
                    return;
                }
            }

            InventoryForm form = new InventoryForm(mDocno, mBoxnum);
            form.ShowDialog();
        }

        private void button2_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_shanxuan_Click(object sender, EventArgs e)
        {
            string bocihao = textBox1_bocihao.Text.Trim();
            if (mDoxBoxNum == null)
                return;

            dataGridView1_docBox.Rows.Clear();

            if (bocihao=="")
            {
                foreach (var t in mDoxBoxNum)
                {
                    dataGridView1_docBox.Rows.Add(t.Key, t.Value);
                }
            }
            else
            {
                foreach (var t in mDoxBoxNum)
                {
                    if(t.Key.Contains(bocihao))
                        dataGridView1_docBox.Rows.Add(t.Key, t.Value);
                }
            }
        }
        List<string> getCancelAuth()
        {
            List<string> re = new List<string>();
            try
            {
                foreach (var v in SysConfig.DeviceInfo.AuthList)
                {
                    if (v.AUTH_CODE.StartsWith("M"))
                    {
                        re.Add(v.AUTH_VALUE);
                    }
                }
            }
            catch(Exception)
            {

            }

            return re;

        }

        private void DocSel_Load(object sender, EventArgs e)
        {
            
            mDoxBoxNum = SAPDataService.GetCancelHuList(SysConfig.LGNUM, getCancelAuth());

            if (mDoxBoxNum == null)
                return;

            dataGridView1_docBox.Rows.Clear();
            foreach (var t in mDoxBoxNum)
            {
                dataGridView1_docBox.Rows.Add(t.Key, t.Value);
            }
        }

        private void DocSel_Shown(object sender, EventArgs e)
        {
            if (dataGridView1_docBox.SelectedRows.Count > 1)
            {
                DataGridViewRow row = dataGridView1_docBox.SelectedRows[0];
                mDocno = row.Cells[0].Value.ToString();
                mBoxnum = row.Cells[1].Value.ToString();
            }
        }

        private void dataGridView1_docBox_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0 && e.RowIndex<=dataGridView1_docBox.RowCount-2)
            {
                DataGridViewRow row = dataGridView1_docBox.Rows[e.RowIndex];
                mDocno = row.Cells[0].Value.ToString();
                mBoxnum = row.Cells[1].Value.ToString();
            }
        }
    }
}
