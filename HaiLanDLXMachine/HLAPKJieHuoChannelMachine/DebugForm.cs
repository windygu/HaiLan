using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLABoxCheckChannelMachine
{
    public partial class DebugForm : Form
    {
        InventoryForm mParentForm = null;
        public DebugForm(InventoryForm i)
        {
            mParentForm = i;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(mParentForm!=null)
            {
                mParentForm.boxNoList.Enqueue("99909398");

                mParentForm.StartInventoryDebug();

                List<string> tiL = new List<string>();
                tiL.Add("50000847950001000000");
                tiL.Add("50000848350001000000");
                tiL.Add("50000848650001000000");

                foreach(var v in tiL)
                {
                    Xindeco.Device.Model.TagInfo ti = new Xindeco.Device.Model.TagInfo();
                    ti.Epc = v;
                }

                mParentForm.StopInventoryDebug();
            }
        }
    }
}
