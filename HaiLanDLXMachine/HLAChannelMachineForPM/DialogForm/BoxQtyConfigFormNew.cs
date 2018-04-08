using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HLAChannelMachine.Utils;
using HLACommonLib;
using HLACommonLib.Model;
using HLACommonLib.Model.ENUM;

namespace HLAChannelMachine
{
    public partial class BoxQtyConfigFormNew : Form
    {
        InventoryFormNew parentForm = null;

        public BoxQtyConfigFormNew(InventoryFormNew _parentForm)
        {
            InitializeComponent();

            parentForm = _parentForm;
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TabTip.exe");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int boxQty = 0;
            int.TryParse(boxQtyTextBox.Text, out boxQty);

            parentForm.setBoxQty(boxQty);

            Close();
        }

        private void button1_close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
