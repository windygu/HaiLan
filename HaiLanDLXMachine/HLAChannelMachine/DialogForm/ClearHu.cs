using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonLib;

namespace HLAChannelMachine.DialogForm
{
    public partial class ClearHu : Form
    {
        public ClearHu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string hu = textBox1_hu.Text.Trim();
                if(string.IsNullOrEmpty(hu))
                {
                    MessageBox.Show("请输入箱号");
                    return;
                }

                if (MessageBox.Show(this, "是否继续","提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string sql = string.Format("DELETE FROM {0} WHERE HU = '{1}'", comboBox1_type.SelectedIndex == 0 ? "hulist" : "hulist_dema", hu);
                    DBHelper.ExecuteNonQuery(sql);
                    sql = string.Format("DELETE FROM {0} WHERE HU = '{1}'", comboBox1_type.SelectedIndex == 0 ? "epcdetail" : "epcdetail_dema", hu);
                    DBHelper.ExecuteNonQuery(sql);

                    MessageBox.Show("删除成功");
                }
                
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClearHu_Load(object sender, EventArgs e)
        {
            comboBox1_type.SelectedIndex = 0;
        }

        private void button3_clearDoc_Click(object sender, EventArgs e)
        {
            try
            {
                string docno = textBox1_docno.Text.Trim();
                if (string.IsNullOrEmpty(docno))
                {
                    MessageBox.Show("请输入单号");
                    return;
                }

                if (MessageBox.Show(this, "将会清除该单号所有数据，是否继续", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string sql = string.Format("DELETE FROM {0} WHERE DOCNO = '{1}'", comboBox1_type.SelectedIndex == 0 ? "epcdetail" : "epcdetail_dema", docno);
                    DBHelper.ExecuteNonQuery(sql);

                    sql = string.Format("DELETE FROM {0} WHERE DOCNO = '{1}'", comboBox1_type.SelectedIndex == 0 ? "docdetail" : "docdetail_dema", docno);
                    DBHelper.ExecuteNonQuery(sql);

                    sql = string.Format("DELETE FROM {0} WHERE DOCNO = '{1}'", comboBox1_type.SelectedIndex == 0 ? "ErrorRecord" : "ErrorRecord_dema", docno);
                    DBHelper.ExecuteNonQuery(sql);

                    MessageBox.Show("删除成功，请重启软件并更换箱号");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
        }
    }
}
