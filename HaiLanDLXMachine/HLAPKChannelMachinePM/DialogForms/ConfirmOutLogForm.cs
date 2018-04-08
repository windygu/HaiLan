using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMSkin;
using HLACommonLib.Model;

namespace HLADeliverChannelMachine.DialogForms
{
    /// <summary>
    /// 下架单异常显示
    /// </summary>
    public partial class ConfirmOutLogForm : MetroForm
    {
        private List<InventoryOutLogDetailInfo> outLogDetails = null;
        private List<MaterialInfo> materialList = null;

        public ConfirmOutLogForm(string sStore, List<InventoryOutLogDetailInfo> _outLogDetails, List<MaterialInfo> _materailList)
        {
            InitializeComponent();
            lblOutlog.Text = sStore;//门店代码
            this.outLogDetails = _outLogDetails;
            this.materialList = _materailList;
        }

        private void ConfirmOutLogForm_Load(object sender, EventArgs e)
        {
            grid.ColumnHeadersHeight = 100; 
            if (outLogDetails == null || outLogDetails.Count <= 0)
            {
                return;
            }
            this.lblOutlog.Text = outLogDetails[0].PICK_TASK;
            foreach (InventoryOutLogDetailInfo item in outLogDetails)
            {
                MaterialInfo mater = materialList.Find(i => i.MATNR == item.PRODUCTNO);
                if (mater == null)
                    grid.Rows.Add(item.PICK_TASK, "", "", "", item.QTY+" "+(item.QTY_ADD>0?"套":item.UOM), item.REALQTY + " " + (item.QTY_ADD > 0 ? "套" : item.UOM), (item.QTY - item.REALQTY) + " " + (item.QTY_ADD > 0 ? "套" : item.UOM));
                else
                    grid.Rows.Add(item.PICK_TASK, mater.ZSATNR, mater.ZCOLSN, mater.ZSIZTX, item.QTY + " " + (item.QTY_ADD > 0 ? "套" : item.UOM), item.REALQTY + " " + (item.QTY_ADD > 0 ? "套" : item.UOM), (item.QTY - item.REALQTY) + " " + (item.QTY_ADD > 0 ? "套" : item.UOM));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
