using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMSkin;
using HLACommonLib.Model.PK;

namespace HLAPKChannelMachine.DialogForms
{
    public partial class PKBoxDetailForm : MetroForm
    {
        public PKBoxDetailForm(PKDeliverBox record, List<PKDeliverErrorBox> errorList)
        {
            InitializeComponent();

            this.Text = string.Format(this.Text, record.HU, record.RESULTDESC);
            if (errorList != null && errorList.Count > 0)
            {
                foreach (PKDeliverErrorBox item in errorList)
                {
                    AddErrorRecord(item);
                }
            }
        }

        private void AddErrorRecord(PKDeliverErrorBox item)
        {
            grid.Rows.Insert(0, item.PARTNER, item.HU, item.ZSATNR, item.ZCOLSN, item.ZSIZTX, item.DIFF, item.REMARK);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
