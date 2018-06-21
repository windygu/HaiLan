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
using HLACommonLib.Model.ENUM;
using HLACommonLib.DAO;
using System.Threading;
using DMSkin;

namespace HLAChannelMachine
{
    public partial class CCheckHuForm : Form
    {
        public CCheckHuForm()
        {
            InitializeComponent();
            initView();
        }

        void initView()
        {
            cboType.SelectedIndex = 1;
        }
        private void UploadForm_Load(object sender, EventArgs e)
        {
        }


        private void btnBarcode_Click(object sender, EventArgs e)
        {
            queryHuErrorInfo();
        }
        private void UnselectGrid2Rows()
        {
            foreach (DataGridViewRow row in grid2.Rows)
                row.Selected = false;
        }

        private void queryHuErrorInfo()
        {
            if (string.IsNullOrWhiteSpace(this.txtHU.Text))
            {
                MetroMessageBox.Show(this, "请输入箱码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ReceiveType receiveType = (ReceiveType)Enum.Parse(typeof(ReceiveType), cboType.Text);
            this.txtHU.Enabled = false;
            this.btnBarcode.Enabled = false;
            grid2.Rows.Clear();
            Thread thread = new Thread(new ThreadStart(() =>
            {
                //加载当前箱码下EPC未注册和商品已扫描的标签
                string hu = txtHU.Text.Trim();
                if (string.IsNullOrEmpty(hu))
                    return;
                DataTable dtError = LocalDataService.GetErrorEpcByHU(hu, receiveType);
                DataTable all = LocalDataService.GetAllDistinctEpcByHU(hu, receiveType);

                foreach (DataRow row in dtError.Rows)
                {
                    //加载错误为商品已扫描的epc
                    string epc = row["EPC_SER"].ToString();
                    this.Invoke(new Action(() =>
                    {
                        grid2.Rows.Insert(0,
                            row["EPC_SER"].ToString(), row["HU"].ToString(), "商品已扫描", "否");
                    }));

                }

                foreach (DataRow row in all.Rows)
                {
                    string epc = row["EPC_SER"].ToString();
                    //不在本单
                    bool exist = false;
                    foreach (DataGridViewRow dgvrow in grid2.Rows)
                    {
                        if (dgvrow.Cells["EPC"].Value.ToString().Trim() == epc)
                        {
                            exist = true;
                        }
                    }
                    if (!exist)
                    {
                        this.Invoke(new Action(() =>
                        {
                            grid2.Rows.Insert(0,
                            row["EPC_SER"].ToString(), row["HU"].ToString(), "不在本单", "否");
                        }));
                    }
                }

                this.Invoke(new Action(() =>
                {
                    this.txtHU.Enabled = true;
                    this.btnBarcode.Enabled = true;
                    this.txtHU.Focus();
                    if (grid2.Rows.Count <= 0)
                    {
                        MetroMessageBox.Show(this, "当前箱没有异常信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        UnselectGrid2Rows();
                    }
                }));
            }));
            thread.IsBackground = true;
            thread.Start();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
