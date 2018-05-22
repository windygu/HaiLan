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
using Newtonsoft.Json;
using HLACommonLib.Model.ENUM;
using HLACommonLib.Model.RECEIVE;
using HLACommonLib.DAO;

namespace HLAChannelMachine
{
    
    public partial class DocNoInputFormNew : Form
    {
        ReceiveType type;
        List<DocInfo> list = null;
        Thread thread = null;
        InventoryMainForm parentForm = null;
        public DocNoInputFormNew(InventoryMainForm _parentForm)
        {
            InitializeComponent();
            parentForm = _parentForm;
        }

        private void DocNoInputForm_Load(object sender, EventArgs e)
        {
            cboType.SelectedIndex = 1;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            //退出登录
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
                txtDocNo.Enabled = true;
                btnOk.Enabled = true;
                btnReset.Enabled = true;
                lblStatus.Text = "";
                cboType.Enabled = true;
                lblStatus.Hide();
            }));
        }

        private void LoadDocInfo()
        {
            type = (ReceiveType)Enum.Parse(typeof(ReceiveType), cboType.Text);
            txtDocNo.Enabled = false;
            btnOk.Enabled = false;
            btnReset.Enabled = false;
            cboType.Enabled = false;
            lblStatus.Show();
            if (true)
            {
                thread = new Thread(new ThreadStart(() =>
                {
                    string docNo = this.txtDocNo.Text.Trim();
                    string loginfo = "";

                    if (string.IsNullOrEmpty(docNo))
                    {
                        ShowMessageBox("收货单号不能为空");
                        OnThreadReturn();
                        return;
                    }

                    DocInfo di = null;
                    StringBuilder sb = new StringBuilder();
                    DateTime lastDateTime = DateTime.Now;

                    if (type == ReceiveType.交货单收货)
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.lblStatus.Text = "正在下载收货单主数据...";
                        }));
                        lastDateTime = DateTime.Now;
                        list = SAPDataService.GetDocInfoList(SysConfig.LGNUM, docNo);
                        loginfo +=string.Format("SAPDataService.GetDocInfoList，耗时{0}ms\r\n", (DateTime.Now - lastDateTime).TotalMilliseconds);
                        if (list == null)
                        {
                            ShowMessageBox("获取收货单信息失败");
                            OnThreadReturn();
                            return;
                        }
                        di = list.FirstOrDefault(o => o.DOCNO == docNo);
                        if (di == null)
                        {
                            ShowMessageBox("此收货单不存在");
                            OnThreadReturn();
                            return;
                        }
                        if (string.IsNullOrEmpty(di.DOCNO))
                        {
                            sb.Append("收货单号为空\r\n");
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
                        if (string.IsNullOrEmpty(di.ZZJWC))
                        {
                            sb.AppendFormat("质检结果为空\r\n");
                        }
                        if (sb.Length > 0)
                        {
                            ShowMessageBox(sb.ToString());
                            OnThreadReturn();
                            return;
                        }
                        if (di.ZXZWC != "X")
                        {
                            ShowMessageBox("此收货单未卸载完成");
                            OnThreadReturn();
                            return;
                        }
                        if (di.ZZJWC != "A")
                        {
                            ShowMessageBox("此收货单未质检通过");
                            OnThreadReturn();
                            return;
                        }

                        this.Invoke(new Action(() =>
                        {
                            this.lblStatus.Text = "正在下载收货单明细数据...";
                        }));
                    }

                    //该单号历史epc
                    List<EpcDetail> hisEpcs = getHisEpcs(di);

                    //物料信息
                    List<MaterialInfo> materialList = null;
                    List<HLATagInfo> hlaTagList = new List<HLATagInfo>();
                    List<DocDetailInfo> docDetailList = null;
                    lastDateTime = DateTime.Now;
                    if (type == ReceiveType.交货单收货)
                    {
                        docDetailList = SAPDataService.GetDocDetailInfoList(SysConfig.LGNUM, docNo, out materialList, out hlaTagList);
                        loginfo+= string.Format("SAPDataService.GetDocDetailInfoList，耗时{0}ms", (DateTime.Now - lastDateTime).TotalMilliseconds);
                    }
                    else if (type == ReceiveType.交接单收货)
                    {
                        docDetailList = SAPDataService.GetTransferDocDetailInfoList(SysConfig.LGNUM, docNo, out materialList, out hlaTagList);
                        loginfo+= string.Format("SAPDataService.GetTransferDocDetailInfoList，耗时{0}ms", (DateTime.Now - lastDateTime).TotalMilliseconds);
                        di = new DocInfo() { DOCNO = docNo, ZYPXFLG = "" };
                    }
                    else
                    {
                        ShowMessageBox("未知的收获类型，无法继续收货！");
                        OnThreadReturn();
                        return;
                    }
                    List<DocDetailInfo> localDocDetailInfoList = LocalDataService.GetDocDetailInfoListByDocNo(docNo, type);
                    if (localDocDetailInfoList != null && localDocDetailInfoList.Count > 0)
                        localDocDetailInfoList.RemoveAll(i => i.REALQTY <= 0);
                    
                    this.Invoke(new Action(() =>
                    {
                        this.lblStatus.Text = "正在下载物料和吊牌数据...";
                    }));
                    List<MixRatioInfo> mixRatioList = new List<MixRatioInfo>();
                    if (docDetailList != null && docDetailList.Count > 0)
                    {
                        int page = 1;
                        int totalPage = docDetailList.Count;
                        foreach (DocDetailInfo ddi in docDetailList)
                        {
                            DocDetailInfo localDocDetail = localDocDetailInfoList != null ? localDocDetailInfoList.FirstOrDefault(o => o.ITEMNO == ddi.ITEMNO && o.ZSATNR == ddi.ZSATNR && o.ZCOLSN == ddi.ZCOLSN && o.ZSIZTX == ddi.ZSIZTX) : null;
                            //数据添加后，将原本本地数据从localDocDetailInfoList移除
                            if (localDocDetail != null)
                            {
                                if (type == ReceiveType.交货单收货)
                                    ddi.QTY = localDocDetail.QTY;
                                ddi.REALQTY = localDocDetail.REALQTY;
                                ddi.BOXCOUNT = localDocDetail.BOXCOUNT;
                                localDocDetailInfoList.Remove(localDocDetail);
                            }
                            else
                            {
                                LocalDataService.SaveDocDetail(ddi.DOCNO, ddi.ITEMNO, ddi.ZSATNR, ddi.ZCOLSN, ddi.ZSIZTX, ddi.ZCHARG, ddi.QTY, ddi.REALQTY, 0, type, ddi.ZPBNO);
                            }
                            List<MixRatioInfo> mixList = null;
                            if (di.ZYPXFLG != null && di.ZYPXFLG.ToUpper() == "X" && !string.IsNullOrEmpty(ddi.ZPBNO))
                            {
                                if (!mixRatioList.Exists(i => i.ZPBNO == ddi.ZPBNO))
                                {
                                    lastDateTime = DateTime.Now;
                                    mixList = SAPDataService.GetMixRatioListByZPBNO(SysConfig.LGNUM, ddi.ZPBNO);
                                    loginfo += string.Format("SAPDataService.GetMixRatioListByZPBNO，第{0}页，共{1}页,耗时{2}ms", page, totalPage, (DateTime.Now - lastDateTime).TotalMilliseconds);
                                    mixList.ForEach(m => {
                                        MaterialInfo material = materialList != null && materialList.Exists(i => i.MATNR == m.MATNR) ? materialList.Find(i => i.MATNR == m.MATNR) : null;
                                        if (material != null)
                                        {
                                            m.ZSATNR = material.ZSATNR;
                                            m.ZCOLSN = material.ZCOLSN;
                                            m.ZSIZTX = material.ZSIZTX;
                                        }
                                    });
                                }
                            }
                            if (mixList != null)
                                mixRatioList.AddRange(mixList);
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

                        if (item.PXQTY.ToString().Trim() == "" || item.PXQTY == 0)
                        {
                            sb.Append("箱规未维护\r\n");
                            right = false;
                        }
                        /*
                        if (item.PXQTY_FH.ToString().Trim() == "" || item.PXQTY_FH == 0)
                        {
                            sb.Append("发货箱规未维护\r\n");
                            right = false;
                        }*/

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

                    /*
                    foreach (var v in materialList)
                        LocalDataService.SaveMaterialInfo(v);
                    foreach (var v in hlaTagList)
                        LocalDataService.SaveTagInfo(v);

                    materialList = LocalDataService.GetMaterialInfoList();
                    hlaTagList = LocalDataService.GetAllRfidHlaTagList();
                    

                    if (materialList == null || materialList.Count <= 0)
                    {
                        ShowMessageBox("主数据-物料信息下载失败");
                        OnThreadReturn();
                        return;
                    }
                    if (hlaTagList == null || hlaTagList.Count <= 0)
                    {
                        ShowMessageBox("主数据-吊牌信息下载失败");
                        OnThreadReturn();
                        return;
                    }
                    */

                    if (!right)
                    {
                        ShowMessageBox(sb.ToString());
                        OnThreadReturn();
                        return;
                    }
                    OnThreadReturn();
                    this.Invoke(new Action(() => {
                        if (parentForm != null)
                            parentForm.loadBasicInfo(di, docDetailList, materialList, hlaTagList, hisEpcs, new List<HuInfo>(), type, mixRatioList);
                        this.Close();
                    }));
                }));
            }
            thread.IsBackground = true;
            thread.Start();
        }

        private List<EpcDetail> getHisEpcs(DocInfo doc,bool jiaohuodan = true)
        {
            Dictionary<string, string> reDic = new Dictionary<string, string>();
            List<EpcDetail> re = new List<EpcDetail>();

            if (doc == null)
                return re;
            try
            {
                string sql = string.Format(@"select * from {0} where DOCNO='{1}' and Result='{2}'",jiaohuodan? "epcdetail" : "epcdetail_dema", doc.DOCNO,"S");
                DataTable dt = DBHelper.GetTable(sql, false);
                if(dt!=null && dt.Rows.Count>0)
                {
                    foreach(DataRow r in dt.Rows)
                    {
                        string epc = r["EPC_SER"] == null ? "" : r["EPC_SER"].ToString();
                        if(epc == "" || reDic.ContainsKey(epc))
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
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }

            return re;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            LoadDocInfo();
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TabTip.exe");
        }

        private void txtDocNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                LoadDocInfo();
            }
        }
    }
}
