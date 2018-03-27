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
    public partial class BoxDetailForm : Form
    {
        private List<ListViewItem> boxDetailList = null;
        public BoxDetailForm(List<ListViewItem> boxDetailList)
        {
            InitializeComponent();

            this.boxDetailList = boxDetailList;
        }

        private void BoxDetailForm_Load(object sender, EventArgs e)
        {
            this.lvBoxDetail.Items.Clear();
            if (this.boxDetailList != null && this.boxDetailList.Count > 0)
            {
                //List<string> boxNoList = new List<string>();
                //boxNoList.Add("全部");
                foreach (ListViewItem item in this.boxDetailList)
                {
                    //if(!boxNoList.Contains(item.SubItems[0].Text))
                    //    boxNoList.Add(item.SubItems[0].Text);
                    this.lvBoxDetail.Items.Add(item);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.lvBoxDetail.Items.Clear();
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.lvBoxDetail.Items.Clear();

            string boxNo = this.txtBoxNo.Text.Trim();
            List<ListViewItem> list = null;
            if (string.IsNullOrEmpty(boxNo))
                list = this.boxDetailList;
            else
                list = this.boxDetailList.Where(o => o.SubItems[0].Text == boxNo).ToList();

            if (list != null && list.Count > 0)
            {
                foreach (ListViewItem item in list)
                {
                    this.lvBoxDetail.Items.Add(item);
                }
            }
        }

        
    }
}
