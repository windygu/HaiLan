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
using HLADeliverChannelMachine.Utils;

namespace HLADeliverChannelMachine.DialogForms
{
    public partial class PrintForm : MetroForm
    {
        private DateTime shipDate = DateTime.Now;
        private ShippingBox shippingBox = null;
        private bool IsLocalprint = false;
        private string pickTask = string.Empty;
        private string mFYDT = "";
        private string mVSART = "";
        string mDocno = "";
        public PrintForm(string docno,DateTime shipDate, ShippingBox shippingBox, string fydt, string vsart, bool isLocalprint, string pickTask)
        {
            InitializeComponent();
            this.IsLocalprint = isLocalprint;
            this.shippingBox = shippingBox;
            this.shipDate = shipDate;
            this.pickTask = pickTask;
            mFYDT = fydt;
            mVSART = vsart;
            mDocno = docno;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //ShippingLabel label = LocalDataService.GetShippingLabelByShipDateAndStoreId(this.shipDate, shippingBox.PARTNER,mFYDT,mVSART);
            ShippingLabel label = LocalDataService.GetShippingLabelByDOCNO(mDocno, mFYDT);

            if (label == null)
            {
                string msg = "发运标签信息不存在:日期:" + shipDate.ToString() + " 门店:" + shippingBox.PARTNER + " 发运大厅：" + mFYDT + " 装运类型：" + mVSART;
                MetroMessageBox.Show(this, msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            if (this.cbShippingBox.Checked)
            {
                if (IsLocalprint)
                    PrinterHelper.PrintShippingBox(SysConfig.PrinterName, label, shippingBox);
                else
                    SAPDataService.PrintShippingBox(SysConfig.PrinterName, SysConfig.LGNUM, pickTask, shippingBox, SysConfig.DeviceInfo.LOUCENG);
            }

            if (this.cbMixShippingBox.Checked)
            {
                if (IsLocalprint)
                    PrinterHelper.PrintMixShippingBox(null, SysConfig.PrinterName, label, shippingBox);
                else
                    SAPDataService.PrintMixShippingBoxBeforeUpload(SysConfig.PrinterName, SysConfig.LGNUM, null, shippingBox);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
