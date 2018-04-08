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
    public partial class DocNoInputFormNew : Form
    {
        List<DocInfo> list = null;
        Thread thread = null;
        InventoryFormNew parentForm = null;
        ReceiveType receiveType = ReceiveType.交货单收货;
        public DocNoInputFormNew(InventoryFormNew _parentForm)
        {
            InitializeComponent();

            parentForm = _parentForm;
        }

        private void DocNoInputForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (thread != null && thread.IsAlive)
                thread.Abort();
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtDocNo.Text = "";
        }

        private void ShowMessageBox(string content)
        {
            this.Invoke(new Action(() => {
                MessageBox.Show(content, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }));
        }

        private void OnThreadReturn()
        {
            this.Invoke(new Action(() =>
            {
                this.txtDocNo.Enabled = true;
                this.btnOk.Enabled = true;
                this.btnReset.Enabled = true;
                this.lblStatus.Text = "";
                this.lblStatus.Hide();
            }));
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.txtDocNo.Enabled = false;
            this.btnOk.Enabled = false;
            this.btnReset.Enabled = false;
            this.lblStatus.Show();

            receiveType = (ReceiveType)Enum.Parse(typeof(ReceiveType), cboType.Text);

            thread = new Thread(new ThreadStart(() =>
            {
                string docNo = this.txtDocNo.Text.Trim();
                if (string.IsNullOrEmpty(docNo))
                {
                    ShowMessageBox("交货单号不能为空");
                    OnThreadReturn();
                    return;
                }

                DocInfo di = null;
                StringBuilder sb = new StringBuilder();

                if (receiveType == ReceiveType.交货单收货)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.lblStatus.Text = "正在下载交货单主数据...";
                    }));
                    list = SAPDataService.GetDocInfoList(SysConfig.LGNUM, docNo);
                    if (list == null)
                    {
                        ShowMessageBox("获取交货单信息失败");
                        OnThreadReturn();
                        return;
                    }

                    di = list.FirstOrDefault(o => o.DOCNO == docNo);

                    if (di == null)
                    {
                        ShowMessageBox("此交货单不存在");
                        OnThreadReturn();
                        return;
                    }
                    if (string.IsNullOrEmpty(di.DOCNO))
                    {
                        sb.Append("交货单号为空\r\n");
                    }
                    if (string.IsNullOrEmpty(di.DOCTYPE))
                    {
                        sb.AppendFormat("凭证类型为空\r\n");
                    }
                    if (string.IsNullOrEmpty(di.GRDATE))
                    {
                        sb.AppendFormat("实际收货日期为空\r\n");
                    }
                    if (string.IsNullOrEmpty(di.ZXZWC))
                    {
                        sb.AppendFormat("卸载结果为空\r\n");
                    }
                    if (sb.Length > 0)
                    {
                        ShowMessageBox(sb.ToString());
                        OnThreadReturn();
                        return;
                    }

                    if (di.ZXZWC != "X")
                    {
                        ShowMessageBox("此交货单未卸载完成");
                        OnThreadReturn();
                        return;
                    }


                    if (di.ZZJWC != "A")
                    {
                        ShowMessageBox("此交货单未质检通过");
                        OnThreadReturn();
                        return;
                    }
                }

                List<EpcDetail> curHisEpcs = getHisEpcs(di);
                this.Invoke(new Action(() =>
                {
                    this.lblStatus.Text = "正在下载交货单明细数据...";
                }));
                //物料信息
                List<MaterialInfo> materialList = new List<MaterialInfo>();
                List<HLATagInfo> hlaTagList = new List<HLATagInfo>();

                List<DocDetailInfo> docDetailList = null;
                if (receiveType == ReceiveType.交货单收货)
                {
                    docDetailList = SAPDataService.GetDocDetailInfoList(SysConfig.LGNUM, docNo, out materialList, out hlaTagList);
                }
                else if (receiveType == ReceiveType.交接单收货)
                {
                    docDetailList = SAPDataService.GetTransferDocDetailInfoList(SysConfig.LGNUM, docNo, out materialList, out hlaTagList);
                    di = new DocInfo() { DOCNO = docNo, ZYPXFLG = "" };
                }
                else
                {
                    ShowMessageBox("未知的收获类型，无法继续收货！");
                    OnThreadReturn();
                    return;
                }

                List<DocDetailInfo> localDocDetailInfoList = LocalDataService.GetDocDetailInfoListByDocNo(di.DOCNO, receiveType);

                this.Invoke(new Action(() =>
                {
                    this.lblStatus.Text = "正在下载历史收货数据...";
                }));
                List<EpcDetail> epcDetailList = new List<EpcDetail>();

                this.Invoke(new Action(() =>
                {
                    this.lblStatus.Text = "正在下载已扫描EPC数据...";
                }));
                if (docDetailList != null && docDetailList.Count > 0)
                {
                    int page = 1;
                    int totalPage = docDetailList.Count;
                    foreach (DocDetailInfo ddi in docDetailList)
                    {
                        DocDetailInfo localDocDetail = localDocDetailInfoList != null ? localDocDetailInfoList.FirstOrDefault(o => o.ITEMNO == ddi.ITEMNO && o.ZSATNR == ddi.ZSATNR && o.ZCOLSN == ddi.ZCOLSN && o.ZSIZTX == ddi.ZSIZTX && o.ZCHARG == ddi.ZCHARG) : null;
                        //数据添加后，将原本本地数据从localDocDetailInfoList移除
                        if (localDocDetail != null)
                        {
                            ddi.REALQTY = localDocDetail.REALQTY;
                            ddi.BOXCOUNT = localDocDetail.BOXCOUNT;
                            localDocDetailInfoList.Remove(localDocDetail);
                        }
                        this.Invoke(new Action(() =>
                        {
                            this.lblStatus.Text = string.Format("正在下载已扫描EPC数据,第{0}页,共{1}页...", page, totalPage);
                        }));

                        List<EpcDetail> epcList = LocalDataService.GetEpcDetailListInfoByPinSeGui(ddi.ZSATNR, ddi.ZCOLSN, ddi.ZSIZTX, receiveType);
                        if (epcList != null)
                        {
                            epcDetailList.AddRange(epcList);
                        }
                        page++;

                    }
                }

                if (materialList == null || materialList.Count <= 0)
                {
                    ShowMessageBox("主数据-物料信息未维护");
                    OnThreadReturn();
                    return;
                }
                if (hlaTagList == null || hlaTagList.Count <= 0)
                {
                    ShowMessageBox("主数据-吊牌信息未维护");
                    OnThreadReturn();
                    return;
                }
                bool right = true;
                foreach (MaterialInfo item in materialList)
                {
                    if (item.MATNR == null || item.MATNR.Trim() == "")
                    {
                        sb.Append("产品编码未维护\r\n");
                        right = false;
                    }


                    if (item.ZCOLSN == null || item.ZCOLSN.Trim() == "")
                    {
                        sb.Append("色号未维护\r\n");
                        right = false;
                    }

                    if (item.ZSATNR == null || item.ZSATNR.Trim() == "")
                    {
                        sb.Append("品号未维护\r\n");
                        right = false;
                    }

                    if (item.ZSIZTX == null || item.ZSIZTX.Trim() == "")
                    {
                        sb.Append("规格未维护\r\n");
                        right = false;
                    }

                    if (item.ZSUPC2 == null || item.ZSUPC2.Trim() == "")
                    {
                        sb.Append("商品大类未维护\r\n");
                        right = false;
                    }

                    if (!right)
                        break;
                }

                if (!right)
                {
                    ShowMessageBox(sb.ToString());
                    OnThreadReturn();
                    return;
                }

                OnThreadReturn();

                this.Invoke(new Action(() =>
                {
                    this.parentForm.LoadDocNoInfo(di, docDetailList
                        , materialList, hlaTagList, epcDetailList, curHisEpcs,receiveType);
                    this.Close();
                }));
            }));

            thread.IsBackground = true;
            thread.Start();


        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TabTip.exe");
        }

        private List<EpcDetail> getHisEpcs(DocInfo doc, bool jiaohuodan = true)
        {
            Dictionary<string, string> reDic = new Dictionary<string, string>();
            List<EpcDetail> re = new List<EpcDetail>();

            if (doc == null)
                return re;
            try
            {
                string sql = string.Format(@"select * from {0} where DOCNO='{1}' and Result='{2}'", jiaohuodan ? "epcdetail" : "epcdetail_dema", doc.DOCNO, "S");
                DataTable dt = DBHelper.GetTable(sql, false);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        string epc = r["EPC_SER"] == null ? "" : r["EPC_SER"].ToString();
                        if (epc == "" || reDic.ContainsKey(epc))
                        {
                            continue;
                        }
                        EpcDetail epcDetail = new EpcDetail();
                        epcDetail.DOCCAT = doc.DOCTYPE;
                        epcDetail.DOCNO = doc.DOCNO;
                        epcDetail.EPC_SER = epc;
                        epcDetail.Floor = "";
                        epcDetail.Handled = 0;
                        epcDetail.HU = r["HU"] == null ? "" : r["HU"].ToString();
                        epcDetail.LGNUM = SysConfig.LGNUM;
                        epcDetail.Result = "S";
                        epcDetail.Timestamp = DateTime.Now;
                        re.Add(epcDetail);
                        reDic.Add(epc, epc);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }

            return re;
        }

    }
}
