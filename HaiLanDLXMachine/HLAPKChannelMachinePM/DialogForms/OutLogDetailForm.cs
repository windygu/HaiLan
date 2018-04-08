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
    public partial class OutLogDetailForm : MetroForm
    {
        private List<InventoryOutLogDetailInfo> outLogDetails = null;
        private List<MaterialInfo> materialList = null;

        public OutLogDetailForm(List<InventoryOutLogDetailInfo> _outLogDetails,List<MaterialInfo> _materailList)
        {
            InitializeComponent();
            this.outLogDetails = _outLogDetails;
            this.materialList = _materailList;
        }

        private void OutLogDetailForm_Load(object sender, EventArgs e)
        {
            if (outLogDetails == null || outLogDetails.Count <= 0)
            {
                UIClear();
                return;
            }
            this.lblOutlog.Text = outLogDetails[0].PICK_TASK;
            this.lblDate.Text = outLogDetails[0].SHIP_DATE.ToString("yyyy-MM-dd");
            this.lblStore.Text = outLogDetails[0].PARTNER;
            this.lblLGNUM.Text = outLogDetails[0].LGNUM;
            this.lblPlanCount.Text = outLogDetails.Sum(i => i.QTY).ToString();
            this.lblRealCount.Text = outLogDetails.Sum(i => i.REALQTY).ToString();
            this.lblDifferent.Text = (int.Parse(this.lblPlanCount.Text) - int.Parse(this.lblRealCount.Text)).ToString();
            foreach (InventoryOutLogDetailInfo item in outLogDetails)
            {
                MaterialInfo mater = materialList.Find(i => i.MATNR == item.PRODUCTNO);
                if (mater == null)
                    grid.Rows.Add(item.PRODUCTNO,"","", item.QTY, item.REALQTY, item.REALQTY- item.QTY, item.QTY_ADD, item.REALQTY_ADD, item.REALQTY_ADD- item.QTY_ADD);
                else
                    grid.Rows.Add(mater.ZSATNR, mater.ZCOLSN, mater.ZSIZTX, item.QTY, item.REALQTY, item.REALQTY-item.QTY,item.QTY_ADD,item.REALQTY_ADD, item.REALQTY_ADD- item.QTY_ADD);
            }
        }

        private void UIClear()
        {
            this.lblOutlog.Text = "";
            this.lblDate.Text = "";
            this.lblStore.Text = "";
            this.lblLGNUM.Text = "";
            this.lblPlanCount.Text = "";
            this.lblRealCount.Text = "";
            this.lblDifferent.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
