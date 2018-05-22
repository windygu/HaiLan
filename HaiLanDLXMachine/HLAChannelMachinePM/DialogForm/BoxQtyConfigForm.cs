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

namespace HLAChannelMachine.DialogForm
{
    public partial class BoxQtyConfigForm : MetroForm
    {
        private List<DocDetailInfo> docDetailList;
        private List<MaterialInfo> materialList;
        private int qty = 0;
        public BoxQtyConfigForm(List<DocDetailInfo> _docDetailList,List<MaterialInfo> _materialList)
        {
            InitializeComponent();
            docDetailList = _docDetailList;
            materialList = _materialList;
        }

        private void BoxQtyConfigForm_Load(object sender, EventArgs e)
        {
            if (SysConfig.RunningModel == RunMode.高位库)
                btnSave.Enabled = false;
            if (docDetailList != null && docDetailList.Count > 0)
            {
                foreach (DocDetailInfo item in docDetailList)
                {
                    if (materialList != null && materialList.Exists(i => i.MATNR == item.PRODUCTNO))
                    {
                        grid.Rows.Insert(0, item.ZSATNR, item.ZCOLSN, item.ZSIZTX, materialList.Find(i => i.MATNR == item.PRODUCTNO).PXQTY);
                        grid.Rows[0].Tag = item;
                    }
                    else
                    {
                        grid.Rows.Insert(0, item.ZSATNR, item.ZCOLSN, item.ZSIZTX, 0);
                        grid.Rows[0].Tag = item;
                    }
                }
            }
        }

        private void grid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //System.Diagnostics.Process.Start("TabTip.exe");
        }

        private void grid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            switch (e.ColumnIndex)
            { 
                case 0:
                    //品号
                    grid.CancelEdit();
                    break;
                case 1:
                    //色号
                    grid.CancelEdit();
                    break;
                case 2:
                    //规格
                    grid.CancelEdit();
                    break;
                case 3:
                    //箱规
                    if (SysConfig.RunningModel == RunMode.高位库)
                        grid.CancelEdit();
                    if (!int.TryParse(e.FormattedValue.ToString(), out qty))
                        grid.CancelEdit();                        
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                materialList.FindAll(i => i.MATNR == (row.Tag as DocDetailInfo).PRODUCTNO)
                    .ForEach(new Action<MaterialInfo>((m) =>
                    {
                        m.PXQTY = int.Parse(row.Cells["QTY"].Value.ToString());
                    }));
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
