using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLAChannelMachine
{
    public partial class PowerForm : Form
    {
        public PowerForm(int pxqty)
        {
            InitializeComponent();

            this.lblMsg.Text = "当前最大箱规：" + pxqty.ToString();
        }
    }
}
