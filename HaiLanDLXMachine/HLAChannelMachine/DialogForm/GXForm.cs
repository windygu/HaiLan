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

namespace HLAChannelMachine
{
    public partial class GXForm : Form
    {
        public GXForm()
        {
            InitializeComponent();
        }

        private void GXForm_Load(object sender, EventArgs e)
        {
            initData();
        }

        private void initData()
        {
            if (SysConfig.DeviceInfo.GxList != null && SysConfig.DeviceInfo.GxList.Count > 0)
            {
                foreach (GxInfo item in SysConfig.DeviceInfo.GxList)
                {
                    ListViewItem lvi = new ListViewItem(item.GX_CODE);
                    lvi.SubItems.Add(item.GX_NAME);
                    lvi.SubItems.Add(item.VIEWGROUP);
                    lvi.SubItems.Add(item.VIEWUSR);
                    lvGX.Items.Add(lvi);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
