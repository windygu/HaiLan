using DMSkin;
using HLACommonLib.Model.RECEIVE;
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
    public partial class PBForm : MetroForm
    {
        private List<MixRatioInfo> _MixRatios;
        public PBForm(List<MixRatioInfo> MixRatios)
        {
            InitializeComponent();
            _MixRatios = MixRatios;
        }

        private void PBForm_Load(object sender, EventArgs e)
        {
            if(_MixRatios!=null && _MixRatios.Count>0)
            {
                foreach(MixRatioInfo item in _MixRatios)
                {
                    grid.Rows.Add(item.ZPBNO, item.ZSATNR, item.ZCOLSN, item.ZSIZTX, item.QUAN);
                }
            }
        }
    }
}
