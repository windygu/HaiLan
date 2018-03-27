using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DMSkin;
using HLACommonLib;
using HLACommonLib.Model;
using HLACommonLib.Model.ENUM;
using HLACommonLib.Model.PK;
using HLACommonLib.DAO;

namespace HLAPKChannelMachine
{
    public partial class MainForm : MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.tabControl.SelectTab(0);
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Name == "page3")
                this.txtShortHU.Focus();
        }


        private void txtShortHU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //回车
                
            }
        }
        int getIntFromStr(string s)
        {
            try
            {
                return int.Parse(s);
            }
            catch (Exception)
            {

            }
            return 0;
        }
        int getShortQty(string should,string real)
        {
            try
            {
                int s = int.Parse(should);
                int r = int.Parse(real);
                return s - r;
            }
            catch(Exception)
            {

            }
            return 0;
        }
        private void btnQueryShortPick_Click(object sender, EventArgs e)
        {
            //清空列表
            gridShort.Rows.Clear();

            if(string.IsNullOrWhiteSpace(txtShortHU.Text.Trim()))
            {
                MetroMessageBox.Show(this, "箱码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<CJianHuoHu> re = LocalDataService.GetJianHuoHu(txtShortHU.Text.Trim());
            if(re.Count>0)
            {
                int shouldT = 0;
                int realT = 0;
                foreach(var v in re)
                {
                    shouldT += getIntFromStr(v.should_qty);
                    realT += getIntFromStr(v.real_qty);
                    int shortqty = getShortQty(v.should_qty, v.real_qty);
                    gridShort.Rows.Insert(0, v.hu, v.pick_list, v.p, v.s, v.g, v.should_qty, v.real_qty, shortqty);
                    gridShort.Rows[0].Tag = v;
                    if(shortqty>0)
                    {
                        gridShort.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                    }
                }
                faTotalBoxNum.Text = shouldT.ToString();
                saoTotalNum.Text = realT.ToString();
            }
            else
            {
                MetroMessageBox.Show(this, "未找到本箱信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnShortConfirm_Click(object sender, EventArgs e)
        {
            if (gridShort.Rows.Count <= 0) return;
            //登录验证
#if DEBUG
#else
            ShortConfirmForm form = new ShortConfirmForm();
            if(form.ShowDialog() == DialogResult.OK)
#endif
            {
                foreach (DataGridViewRow row in gridShort.Rows)
                {
                    CJianHuoHu jh = row.Tag as CJianHuoHu;
                    if(jh!=null)
                    {
                        int shortqty = getShortQty(jh.should_qty, jh.real_qty);
                        if(shortqty>0)
                        {
                            LocalDataService.updateShortJianHuo(jh.hu, jh.mat, shortqty);
                        }
                    }
                }

                MetroMessageBox.Show(this, "短拣成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnQueryDeliverBox_Click(object sender, EventArgs e)
        {
            string hu = txtDeliverHu.Text.Trim();
            if (string.IsNullOrWhiteSpace(hu))
            {
                MetroMessageBox.Show(this, "箱码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            gridDeliverErrorBox.Rows.Clear();

            List<CJianHuoHu> re = LocalDataService.GetJianHuoHu(hu);

            if(re.Count>0)
            {
                foreach(var item in re)
                {
                    gridDeliverErrorBox.Rows.Add(item.hu, item.pick_list, item.p, item.s, item.g, item.should_qty, item.real_qty, item.short_qty);
                }
            }
            else
            {
                MetroMessageBox.Show(this, "未找到本箱信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

    }

}
