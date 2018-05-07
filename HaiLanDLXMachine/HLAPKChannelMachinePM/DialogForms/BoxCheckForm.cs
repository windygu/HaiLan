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
using HLADeliverChannelMachine.Utils;
using UARTRfidLink.Exparam;
using UARTRfidLink.Extend;

namespace HLADeliverChannelMachine.DialogForms
{
    public partial class BoxCheckForm : MetroForm
    {
        private Dictionary<string, DataGridViewRow> dgvList = new Dictionary<string, DataGridViewRow>();
        private RfidUARTLinkExtend reader;
        private bool isInventory = false;
        private ShippingBox box = null;
        private ShippingBoxDetail currentShippingBoxDetail = null;
        private List<string> epcList = new List<string>();
        string mComPort;
        public BoxCheckForm()
        {
            InitializeComponent(); 
        }

        public BoxCheckForm(RfidUARTLinkExtend _reader,string port)
        {
            InitializeComponent();
            reader = _reader;
            mComPort = port;
            reader.RadioInventory += rfid_RadioInventory;

        }
        public void rfid_RadioInventory(object sender, RadioInventoryEventArgs e)
        {
            string epc = "";
            try
            {
                for (int i = 0; i < e.tagInfo.epc.Length; i++)
                {
                    epc += string.Format("{0:X4}", e.tagInfo.epc[i]);
                }
            }
            catch (Exception) { }

            reader_OnTagReported(epc);
        }

        private void CheckTag(string data)
        {
            if (box == null || box.Details == null || epcList.Contains(data))
                return;

            if (box.Details.Exists(i => i.EPC == data))
            {
                this.lblEpc.Text = "";
                epcList.Add(data);
                currentShippingBoxDetail = box.Details.Find(i => i.EPC == data);

                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (currentShippingBoxDetail.ZSATNR == row.Cells["ZSATNR"].Value.ToString() &&
                        currentShippingBoxDetail.ZCOLSN == row.Cells["ZCOLSN"].Value.ToString() &&
                        currentShippingBoxDetail.ZSIZTX == row.Cells["ZSIZTX"].Value.ToString())
                    {
                        if (currentShippingBoxDetail.IsADD == 1)
                        {
                            row.Cells["SCAN_NUM_ADD"].Value = int.Parse(row.Cells["SCAN_NUM_ADD"].Value.ToString()) + 1;
                            row.Cells["DIFF_NUM_ADD"].Value = int.Parse(row.Cells["DIFF_NUM_ADD"].Value.ToString()) - 1;
                        }
                        else
                        {
                            row.Cells["SCAN_NUM"].Value = int.Parse(row.Cells["SCAN_NUM"].Value.ToString()) + 1;
                            row.Cells["DIFF_NUM"].Value = int.Parse(row.Cells["DIFF_NUM"].Value.ToString()) - 1;
                        }

                    }
                }
            }
            else
            {
                this.lblEpc.Text = "不在本箱";
                this.lblEpc.ForeColor = this.lblEpc.ForeColor == Color.Red ? Color.Yellow : Color.Red;
            }
        }

        void reader_OnTagReported(string Epc)
        {
            CheckTag(Epc);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            StopInventory();
            string boxNo = this.txtBoxNo.Text.Trim();
            box = LocalDataService.GetShippingBoxByHu(boxNo);
            if (box == null)
            {
                MetroMessageBox.Show(this, "该箱码不存在", "提示");
                return;
            }
            this.grid.Rows.Clear();
            epcList.Clear();
            if (box.Details != null && box.Details.Count > 0)
            {
                foreach (ShippingBoxDetail item in box.Details)
                {
                    string key = string.Format("{0}|{1}|{2}", item.ZSATNR, item.ZCOLSN, item.ZSIZTX);
                    if (!this.dgvList.ContainsKey(key))
                    {
                        if (item.IsADD == 0)
                        {
                            this.grid.Rows.Add(item.ZSATNR, item.ZCOLSN, item.ZSIZTX, "1", "0", "1", "0", "0", "0");
                            DataGridViewRow row = this.grid.Rows[this.grid.Rows.Count - 1];
                            this.dgvList.Add(key, row);
                        }
                        else
                        {
                            this.grid.Rows.Add(item.ZSATNR, item.ZCOLSN, item.ZSIZTX, "0", "0", "0", "1", "0", "1");
                            DataGridViewRow row = this.grid.Rows[this.grid.Rows.Count - 1];
                            this.dgvList.Add(key, row);
                        }
                    }
                    else
                    {
                        DataGridViewRow row = this.dgvList[key];
                        if (item.IsADD == 0)
                        {
                            row.Cells["NUM"].Value = (int.Parse(row.Cells["NUM"].Value.ToString()) + 1).ToString();
                            row.Cells["DIFF_NUM"].Value = (int.Parse(row.Cells["DIFF_NUM"].Value.ToString()) + 1).ToString();
                        }
                        if (item.IsADD == 1)
                        {
                            row.Cells["NUM_ADD"].Value = (int.Parse(row.Cells["NUM_ADD"].Value.ToString()) + 1).ToString();
                            row.Cells["DIFF_NUM_ADD"].Value = (int.Parse(row.Cells["DIFF_NUM_ADD"].Value.ToString()) + 1).ToString();
                        }
                    }
                }
            }
            this.txtBarcode.Focus();
            StartInventory();
        }

       

        private bool StartInventory()
        {
            //判断是否未盘点，未盘点则开始盘点
            if (this.isInventory == false)
            {
                this.isInventory = true;
                reader.StartInventory(mComPort, RadioOperationMode.Continuous, 1);
            }

            return true;
        }

        private bool StopInventory()
        {
            //判断是否正在盘点，正在盘点则停止盘点
            if (this.isInventory == true)
            {
                this.isInventory = false;
                reader.StopInventory(mComPort);
            }
            return true;
        }

        private void BoxCheckForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.isInventory)
                StopInventory();
            reader.RadioInventory -= new EventHandler<RadioInventoryEventArgs>(rfid_RadioInventory);
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //回车
                string barcode = txtBarcode.Text.Trim();
                txtBarcode.Clear();
                CheckTag(barcode);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
