using DMSkin;
using HLACommonLib.Model;
using HLACommonLib.Model.PACKING;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonLib.DAO;

namespace HLAPackingBoxChannelMachine.DialogForms
{
    public partial class JiaoJiaoHandle : MetroForm
    {
        public JiaoJiaoHandle()
        {
            InitializeComponent();
        }

        private void metroButton1_del_Click(object sender, EventArgs e)
        {
            try
            {
                if (metroTextBox1_hu.Text.Trim() == "")
                {
                    MessageBox.Show("请输入箱号");
                    return;
                }

                string hu = metroTextBox1_hu.Text.Trim();
                List<string> hus = new List<string>();
                hus.Add(hu);
                if (PackingBoxService.DeleteBoxByHu(hus))
                    MessageBox.Show("操作成功");
            }
            catch(Exception)
            {

            }
        }
    }
}
