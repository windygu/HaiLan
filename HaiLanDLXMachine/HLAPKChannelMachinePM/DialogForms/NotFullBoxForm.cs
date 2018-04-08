using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMSkin;
using HLACommonLib;
using HLACommonLib.Model;
using HLADeliverChannelMachine.Properties;

namespace HLADeliverChannelMachine.DialogForms
{
    /// <summary>
    /// 加载当前门店、当前楼层下未满箱的箱子信息
    /// </summary>
    public partial class NotFullBoxForm : MetroForm
    {

        public delegate void DoSelect(List<ShippingBox> list);
        public DoSelect onSelect;
        List<ShippingBox> list = null;
        List<ShippingBox> selectlist = new List<ShippingBox>();

        public NotFullBoxForm(List<ShippingBox> _list)
        {
            InitializeComponent();
            list = _list;
        }

        private void NotFullBoxForm_Load(object sender, EventArgs e)
        {
            
            //list = LocalDataService.GetShippingBoxList(partner, floor, 0);
            if (list != null && list.Count > 0)
            {
                foreach (ShippingBox item in list)
                {
                    InsertGrid(item.HU, item.QTY, item.IsFull);
                }    
            }
        }

        private void InsertGrid(string hu, int qty, byte isfull)
        {
            grid.Rows.Add(hu, qty, isfull == 1 ? Resources.满箱 : Resources.空箱);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //if (grid.SelectedRows.Count <= 0)
            //{
            //    MetroMessageBox.Show(this, "未选择箱", "提示");
            //    return;
            //}
            //ShippingBox selectedRow = list.FirstOrDefault(i => i.HU.Trim() == grid.SelectedRows[0].Cells["BoxNO"].Value.ToString().Trim());
            foreach (DataGridViewRow item in grid.Rows)
            {
               if(item.Selected)
                {
                    selectlist.Add(list.Find(m => m.HU == item.Cells[0].Value.ToString()));
                }
            }
            if (selectlist.Count == 0)
            {
                MetroMessageBox.Show(this, "未选择箱", "提示");
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            if (onSelect != null)
                onSelect(selectlist);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            if (onSelect != null)
                onSelect(null);
        }

        private void btnClearprompt_Click(object sender, EventArgs e)
        {
            //if (grid.SelectedRows.Count <= 0)
            //{
            //    MetroMessageBox.Show(this, "未选择箱", "提示");
            //    return;
            //}
            //ShippingBox selectedRow = list.FirstOrDefault(i => i.HU.Trim() == grid.SelectedRows[0].Cells["BoxNO"].Value.ToString().Trim());
            this.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            if (onSelect != null)
                onSelect(null); //将值设为null，清除提示不在提示，且获取新箱号
        }

    }
}
