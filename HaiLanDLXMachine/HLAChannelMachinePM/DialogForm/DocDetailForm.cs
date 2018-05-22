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

namespace HLAChannelMachine.DialogForm
{
    public partial class DocDetailForm : MetroForm
    {
        private List<DocDetailInfo> _DocDetails;

        public DocDetailForm(List<DocDetailInfo> DocDetails)
        {
            InitializeComponent();
            _DocDetails = DocDetails;
        }

        private void DocDetailForm_Load(object sender, EventArgs e)
        {
            if(_DocDetails!=null && _DocDetails.Count>0)
            {
                foreach(DocDetailInfo item in _DocDetails)
                {
                    grid.Rows.Add(item.ITEMNO, item.ZSATNR, item.ZCOLSN, item.ZSIZTX, item.QTY, item.REALQTY);
                }
            }
        }
    }
}
