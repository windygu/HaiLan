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
using System.Collections;
using System.Threading;

namespace HLADeliverChannelMachine.DialogForms
{
    public delegate void ErrorScan(bool isError);
    /// <summary>
    /// 扫描过程异常窗体
    /// </summary>
    public partial class ErrorLogForm : MetroForm
    {
        public event ErrorScan errorScan;
        List<ShippingBoxDetail> epcList = new List<ShippingBoxDetail>();
        public Stack CurrentErrorQueue = new Stack(100);
        //System.Timers.Timer timer = new System.Timers.Timer(100);
        //System.Timers.Timer showtimer = new System.Timers.Timer(1000);
        private bool haderror = false;
        public bool isShow = false;
        //private static ErrorLogForm form=null;
        //public static ErrorLogForm GetErrorLogForm()
        //{
        //    if (form == null)
        //    {
        //        return new ErrorLogForm();
        //    }
        //    return form;
        //}
        public ErrorLogForm()
        {
            InitializeComponent();
            //timer.Elapsed += Timer_Elapsed;
            //showtimer.Elapsed += Showtimer_Elapsed;
            //showtimer.Start();
        }

        private void Showtimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (haderror && CurrentErrorQueue.Count == 0)
            {
                if (!isShow)
                {
                    isShow = true;
                    //showtimer.Stop();
                    this.ShowDialog();
                }
            }
        }

        object _object = new object();
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (_object)
            {
                if (!haderror || CurrentErrorQueue.Count == 0)
                {
                    return;
                }

                error a = CurrentErrorQueue.Pop() as error;

                UpdateRecordInfo(a.sEPC, a.sPICK_TASK, a.dti, a.iFlag, a.outLogList);
            }
        }

        private void ErrorLogForm_Load(object sender, EventArgs e)
        {
            grid.ColumnHeadersHeight = 100;
            //errlist.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            errorScan?.Invoke(false);
            HideThis();
        }

        private void HideThis()
        {
            //timer.Stop();
            haderror = false;
            this.grid.Rows.Clear();
            errlist = new List<string>();
            this.LessEPCList = new List<string>();

            Thread.Sleep(200);
            this.Hide();
            isShow = false;
        }

        public int i = 0;//标识错误
        public int iGridRowsCount = 0;//异常标识行
        public int iNum1 = 0;//错误差异数量
        public int iNum2 = 0;//错误实扫数量
        public int iNum3 = 0;//多拣差异数量
        public int iNum4 = 0;//多拣实扫数量


        private List<string> LessEPCList = new List<string>();
        private object _objectid = new object();

        private List<string> errlist = new List<string>();
        public class error
        {
            public string sEPC
            {
                get; set;
            }
            public string sPICK_TASK
            {
                get; set;
            }
            public TagDetailInfo dti
            {
                get; set;
            }
            public int iFlag
            {
                get; set;
            }
            public List<InventoryOutLogDetailInfo> outLogList
            {
                get; set;
            }
        }
        int mm = 0;
        public void AsynAddCurrentErrorQueue(string _sEPC, string _sPICK_TASK, TagDetailInfo _dti,
            int _iFlag, List<InventoryOutLogDetailInfo> _outLogList, List<ShippingBoxDetail> _epcList, bool isrfid)
        {
            this.BringToFront();
            epcList = _epcList;
            if (isrfid&&errlist.Contains(_sEPC))
                return;
            if (isrfid)
                errlist.Add(_sEPC);
            if (!this.isShow)
            {
                try
                {
                    this.isShow = true;
                    this.Show();
                    //new Thread(() => { this.ShowDialog(); }).Start();
                }
                catch (Exception e)
                {
                    LogHelper.WriteLine(e.Message + "\r\n" + e.StackTrace);
                }
            }
            //CurrentErrorQueue.Push(new error() { sEPC = _sEPC, sPICK_TASK = _sPICK_TASK, dti = _dti, iFlag = _iFlag, outLogList = _outLogList });
            //if (!haderror)
            //{
            //    haderror = true;
            //    timer.Start();
            //    showtimer.Start();
            //}
            UpdateRecordInfo(_sEPC, _sPICK_TASK, _dti, _iFlag, _outLogList);
        }

        //更新列表数据
        private void UpdateRecordInfo(string sEPC, string sPICK_TASK, TagDetailInfo dti, int iFlag, List<InventoryOutLogDetailInfo> outLogList)
        {
            if (string.IsNullOrEmpty(sEPC))
                return;
            string sMessage = "(不在本单)";
            DataTable dt = LocalDataService.GetEPCByMT(sEPC);
            DataRow row = null;
            //获取EPC对应明细
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    string matnr = r["MATNR"]==null?"" : r["MATNR"].ToString();
                    if (string.IsNullOrEmpty(matnr))
                    {
                        if (r[0].ToString().Equals(matnr))
                        {
                            row = r;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        row = r;
                        break;
                    }
                }
            }
            if (row == null) // 标签未注册
            {
                dt = new DataTable();
                dt.Columns.Add();
                dt.Columns.Add();
                dt.Columns.Add();
                dt.Columns.Add();
                row = dt.NewRow();
                row[0] = "标签未注册";
                row[1] = row[2] = row[3] = "空";
            }
            if (dti == null)
            {
                dti = new TagDetailInfo();
                dti.IsAddEpc = row.Table.Columns.Count == 4 ? false : (sEPC.Length > 13) && row["RFID_ADD_EPC"].ToString().Equals(sEPC.Substring(0, 14));

            }
            string sMATNR = row[0].ToString();//产品编码
            if (!string.IsNullOrEmpty(sMATNR) && !this.LessEPCList.Contains(sMATNR))
            {
                if (iFlag != 3)
                {
                    this.LessEPCList.Add(sMATNR);//不存在新增
                    ShowGetMessage(sPICK_TASK, dti, iFlag, outLogList, sMessage, row, sMATNR);
                    //this.Invoke(new Action(() =>
                    //{
                    //    NewMethod(sPICK_TASK, dti, iFlag, outLogList, sMessage, row, sMATNR);
                    //}));
                }
            }
            else
            { //存在修改
                if (grid != null && grid.Rows.Count > 0)
                {
                    foreach (DataGridViewRow gridRow in this.grid.Rows)
                    {
                        if (gridRow.Cells[0].Value.ToString() == sMATNR)
                        {

                            if (!dti.IsAddEpc)
                            {
                                string sQty = gridRow.Cells[6].Value.ToString().Replace(sMessage, "");
                                string sQtyAdd = gridRow.Cells[7].Value.ToString().Replace(sMessage, "");
                                gridRow.Cells[6].Value = (Convert.ToInt32(sQty) + 1) + (iFlag == 0 ? sMessage : "");
                                gridRow.Cells[7].Value = Convert.ToInt32(sQtyAdd) + 1;
                            }
                            else
                            {
                                string sQty = gridRow.Cells[9].Value.ToString().Replace(sMessage, "");
                                string sQtyAdd = gridRow.Cells[10].Value.ToString().Replace(sMessage, "");
                                gridRow.Cells[9].Value = (Convert.ToInt32(sQty) + 1) + (iFlag == 0 ? sMessage : "");
                                gridRow.Cells[10].Value = Convert.ToInt32(sQtyAdd) + 1;
                            }
                        }
                    }
                }
            }


        }

        private delegate void ShowMessages(string sPICK_TASK, TagDetailInfo dti, int iFlag, List<InventoryOutLogDetailInfo> outLogList, string sMessage, DataRow row, string sMATNR);
        private void ShowGetMessage(string sPICK_TASK, TagDetailInfo dti, int iFlag, List<InventoryOutLogDetailInfo> outLogList, string sMessage, DataRow row, string sMATNR)
        {
            if (this.InvokeRequired)
            {
                ShowMessages show = new ShowMessages(ShowGetMessage);
                this.BeginInvoke(show, sPICK_TASK, dti, iFlag, outLogList, sMessage, row, sMATNR);
            }
            else
            {
                grid.Rows.Add(sMATNR, sPICK_TASK, row[1].ToString(), row[3].ToString(), row[2].ToString(), 
                    
                    
                    iFlag == 0 ? 0 : outLogList.FindAll(i => i.PICK_TASK == sPICK_TASK && i.PRODUCTNO == sMATNR).Sum(i => i.QTY),
                    dti.IsAddEpc ? 0 : (1 + epcList.FindAll(i => i.IsADD == 0 && i.ZSATNR == row[1].ToString() && i.ZCOLSN == row[3].ToString() && i.ZSIZTX == row[2].ToString()).Count),
                    (dti.IsAddEpc ? "0" : "1" + (iFlag == 0 ? sMessage : "")),
                   
                    iFlag == 0 ? 0 : outLogList.FindAll(i => i.PICK_TASK == sPICK_TASK && i.PRODUCTNO == sMATNR).Sum(i => i.QTY_ADD), 
                    !dti.IsAddEpc ? 0 : (1 + epcList.FindAll(i => i.IsADD == 1 && i.ZSATNR == row[1].ToString() && i.ZCOLSN == row[3].ToString() && i.ZSIZTX == row[2].ToString()).Count),
                    (!dti.IsAddEpc ? "0" : "1" + (iFlag == 0 ? sMessage : "")));
            }
        }
        /// <summary>
        /// 增加“误读”选项，  by linzw 160514
        /// 当扫描员选择误读时，清除错误统计，当扫描员选择确认时，统计错误；查看下架单扫描明细界面，
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFalse_Click(object sender, EventArgs e)
        {
            errorScan?.Invoke(true);
            /*清楚列表数据*/ 
            HideThis();
        }

        private void ErrorLogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            errorScan?.Invoke(false);
        }
    }
}
