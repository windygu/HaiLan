using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonLib;

namespace HLAChannelMachine.DialogForm
{
    public partial class Customsetting : Form
    {
        InventoryForm mParent = null;
        public Customsetting(InventoryForm parentForm)
        {
            mParent = parentForm;
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox1_timelog_CheckedChanged(object sender, EventArgs e)
        {
            mParent.mTimeLog = checkBox1_timelog.Checked;
        }
    }
}
