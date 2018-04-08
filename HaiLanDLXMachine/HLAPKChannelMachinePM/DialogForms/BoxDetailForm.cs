using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMSkin;
using HLACommonLib;
using HLACommonLib.Model;

namespace HLADeliverChannelMachine.DialogForms
{
    public partial class BoxDetailForm : MetroForm
    {
        private ShippingBox shippingBox = null;
        private Dictionary<string, DataGridViewRow> dgvList = new Dictionary<string, DataGridViewRow>();

        public BoxDetailForm(ShippingBox _shippingBox)
        {
            InitializeComponent();
            shippingBox = _shippingBox;
        }

        private void BoxDetailForm_Load(object sender, EventArgs e)
        {
            if (this.shippingBox != null)
            {
                //当明细为空时，重新从数据库获取明细数据，防止过去的箱子没有导入明细数据
                //if (this.shippingBox.Details == null)
                //{
                //    List<ShippingBoxDetail> list = LocalDataService.GetShippingBoxDetailListByHu(this.shippingBox.HU);
                //    this.shippingBox.Details = list;
                //}
                if (this.shippingBox.Details != null && this.shippingBox.Details.Count > 0)
                {
                    foreach (ShippingBoxDetail item in this.shippingBox.Details)
                    {
                        string key = string.Format("{0}|{1}|{2}|{3}", item.ZSATNR, item.ZCOLSN, item.ZSIZTX, item.PICK_TASK);
                        if (!this.dgvList.ContainsKey(key))
                        {
                            if (item.IsADD == 0)
                            {
                                this.grid.Rows.Add(item.ZSATNR, item.ZCOLSN, item.ZSIZTX, 1, 0, item.PICK_TASK);
                                
                            }
                            else
                            {
                                this.grid.Rows.Add(item.ZSATNR, item.ZCOLSN, item.ZSIZTX, 0, 1, item.PICK_TASK);
                            }
                            DataGridViewRow row = this.grid.Rows[this.grid.Rows.Count - 1];
                            this.dgvList.Add(key, row);
                        }
                        else
                        {
                            DataGridViewRow row = this.dgvList[key];
                            if (item.IsADD == 0)
                                row.Cells["NUM"].Value = (int.Parse(row.Cells["NUM"].Value.ToString()) + 1).ToString();
                            if (item.IsADD == 1)
                                row.Cells["NUM_ADD"].Value = (int.Parse(row.Cells["NUM_ADD"].Value.ToString()) + 1).ToString();
                        }
                    }
                }
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
