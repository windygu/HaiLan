using DMSkin;
using HLACommonLib.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLAEBReceiveChannelMachine.DialogForms
{
    public partial class EbBoxDetailForm : MetroForm
    {
        public EbBoxDetailForm(EbBoxCheckRecordInfo record, List<EbBoxErrorRecordInfo> errorList)
        {
            InitializeComponent();
            this.Text = string.Format(this.Text, record.HU, record.STATUS == 1 ? "正常" : "异常");
            if (errorList != null && errorList.Count > 0)
            {
                foreach (EbBoxErrorRecordInfo item in errorList)
                {
                    AddErrorRecord(item);
                }
            }
        }

        private void AddErrorRecord(EbBoxErrorRecordInfo item)
        {
            grid.Rows.Insert(0, item.HU, item.ZSATNR, item.ZCOLSN, item.ZSIZTX, item.DIFF, item.REMARK);
        }
    }
}
