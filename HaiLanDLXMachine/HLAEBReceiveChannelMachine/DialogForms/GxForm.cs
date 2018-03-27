using DMSkin;
using HLACommonLib;
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
    public partial class GxForm : MetroForm
    {
        public GxForm()
        {
            InitializeComponent();
        }
        private void GxForm_Load(object sender, EventArgs e)
        {
            initData();
        }

        private void initData()
        {
            if (SysConfig.DeviceInfo.GxList != null && SysConfig.DeviceInfo.GxList.Count > 0)
            {
                foreach (GxInfo item in SysConfig.DeviceInfo.GxList)
                {
                    grid.Rows.Add(item.GX_CODE, item.GX_NAME, item.VIEWGROUP, item.VIEWUSR);
                }
            }
        }
    }
}
