using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMSkin;
using HLACommonLib.Model;

namespace HLADeliverChannelMachine.DialogForms
{
    public delegate void SetStatist(bool value);
    /// <summary>
    /// 下架单异常显示
    /// </summary>
    public partial class SingleShelvesForm : MetroForm
    {
        private List<InventoryOutLogDetailInfo> outLogDetails = null;
        private List<MaterialInfo> materialList = null;

        public event SetStatist setStatist;
        public SingleShelvesForm(string sStore, List<InventoryOutLogDetailInfo> _outLogDetails, List<MaterialInfo> _materailList)
        {
            InitializeComponent();
            lblOutlog.Text = sStore;//门店代码
            this.outLogDetails = _outLogDetails;
            this.materialList = _materailList;
        }

        private void SingleShelvesForm_Load(object sender, EventArgs e)
        {
            grid.ColumnHeadersHeight = 100;
            if (outLogDetails == null || outLogDetails.Count <= 0)
            {
                return;
            }
            this.lblOutlog.Text = outLogDetails[0].PICK_TASK;
            foreach (InventoryOutLogDetailInfo item in outLogDetails)
            {
                MaterialInfo mater = materialList.Find(i => i.MATNR == item.PRODUCTNO);
                if (mater == null)
                    grid.Rows.Add(item.PICK_TASK, "", "", "", item.QTY, item.REALQTY, item.REALQTY - item.QTY, item.QTY_ADD, item.REALQTY_ADD, item.REALQTY_ADD - item.QTY_ADD);
                else
                    grid.Rows.Add(item.PICK_TASK, mater.ZSATNR, mater.ZCOLSN, mater.ZSIZTX, item.QTY, item.REALQTY, item.REALQTY - item.QTY, item.QTY_ADD, item.REALQTY_ADD, item.REALQTY_ADD - item.QTY_ADD);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// “错误统计”选项
        ///  只统计少捡记录，当少捡记录做短捡处理，则不上传少捡信息。
        ///  主管要输入账号，并确认短jian，则不上传SAP duanjian 信息
        ///  点击 错误统计 ：增加短jian错误信息至缓存。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStatistics_Click(object sender, EventArgs e)
        {
            setStatist?.Invoke(true);
        }
    }
}
