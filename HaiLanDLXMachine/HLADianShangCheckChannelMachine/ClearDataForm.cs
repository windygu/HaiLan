using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLAJiaoJieCheckChannelMachine
{
    public partial class ClearDataForm : Form
    {
        Form1 mParent;
        public ClearDataForm(Form1 pa)
        {
            InitializeComponent();
            mParent = pa;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_clearHu_Click(object sender, EventArgs e)
        {
            mParent.clearHu(textBox1_hu.Text.Trim());
        }

        private void button1_clearDoc_Click(object sender, EventArgs e)
        {
            mParent.clearDoc(textBox2_doc.Text.Trim());
        }
    }
}
