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
    public partial class DocNoInputForm : Form
    {
        List<DocInfo> list = null;
        Thread thread = null;
        InventoryForm parentForm = null;
        ReceiveType receiveType = ReceiveType.交货单收货;
        public DocNoInputForm(InventoryForm _parentForm)
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
            if (true)//(thread == null)
            {
                thread = new Thread(new ThreadStart(() =>
                {
                    
                    string docNo = this.txtDocNo.Text.Trim();
                    if (string.IsNullOrEmpty(docNo))
                    {
                        ShowMessageBox("交货单号不能为空");
                        OnThreadReturn();
                        return;
                    }

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

                    DocInfo di = list.FirstOrDefault(o => o.DOCNO == docNo);

                    if (di == null)
                    {
                        ShowMessageBox("此交货单不存在");
                        OnThreadReturn();
                        return;
                    }
                    StringBuilder sb = new StringBuilder();
                    if (string.IsNullOrEmpty(di.DOCNO))
                    {
                        sb.Append("交货单号为空\r\n");
                        //MessageBox.Show("交货单号为空！", "提示");
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
                    /*
                    if (string.IsNullOrEmpty(di.ZZJWC))
                    {
                        sb.AppendFormat("质检结果为空\r\n");
                    }*/
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
                    /*
                    if (di.ZZJWC != "A")
                    {
                        ShowMessageBox("此交货单未质检通过");
                        OnThreadReturn();
                        return;
                    }*/

                    this.Invoke(new Action(() =>
                    {
                        this.lblStatus.Text = "正在下载交货单明细数据...";
                    }));
                    //物料信息
                    List<MaterialInfo> materialList = new List<MaterialInfo>();
                    List<HLATagInfo> hlaTagList = new List<HLATagInfo>();
                    List<DocDetailInfo> docDetailList = SAPDataService.GetDocDetailInfoList(SysConfig.LGNUM, di.DOCNO, out materialList,out hlaTagList);
                    List<DocDetailInfo> localDocDetailInfoList = LocalDataService.GetDocDetailInfoListByDocNo(di.DOCNO, receiveType);

                    //this.Invoke(new Action(() =>
                    //{
                    //    this.lblStatus.Text = "正在下载物料主数据...";
                    //}));

                    //this.Invoke(new Action(() =>
                    //{
                    //    this.lblStatus.Text = "正在下载吊牌数据...";
                    //}));
                    //吊牌信息
                    //List<HLATagInfo> hlaTagList = LocalDataService.GetAllRfidHlaTagList();
                    //与本交货单行项目相同品色规的所有EPC明细
                    this.Invoke(new Action(() =>
                    {
                        this.lblStatus.Text = "正在下载历史收货数据...";
                    }));
                    //List<EpcDetail> epcDetailList = LocalDataService.GetAllRightEpcDetail();
                    List<EpcDetail> epcDetailList = new List<EpcDetail>();
                    List<HuInfo> huList = new List<HuInfo>();

                    //List<HuInfo> huList = LocalDataService.GetHuInfoListByLGNUM(SysConfig.LGNUM,new DateTime(2000,1,1));

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

                            //List<MaterialInfo> mList = SAPDataService.GetMaterialInfoListByMATNR(SysConfig.LGNUM, ddi.PRODUCTNO);
                            List<EpcDetail> epcList = LocalDataService.GetEpcDetailListInfoByPinSeGui(ddi.ZSATNR, ddi.ZCOLSN, ddi.ZSIZTX, receiveType);
                            //List<HLATagInfo> tagList = SAPDataService.GetHLATagInfoListByMATNR(SysConfig.LGNUM, ddi.PRODUCTNO);
                            //if (tagList != null)
                            //    hlaTagList.AddRange(tagList);

                            //加载物料信息
                            //if (mList != null)
                            //materialList.AddRange(mList);
                            //加载EPC明细
                            if (epcList != null)
                            {
                                epcDetailList.AddRange(epcList);
                                foreach (EpcDetail detail in epcList)
                                {
                                    if (!huList.Exists(i => i.HU == detail.HU))
                                    {
                                        huList.Add(new HuInfo()
                                        {
                                            HU = detail.HU,
                                            QTY = epcList.Count(x => x.HU == detail.HU),
                                            Result = detail.Result
                                        });
                                    }
                                }
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

                        //if (item.PXQTY.ToString().Trim() == "" || item.PXQTY == 0)
                        //{
                        //    sb.Append("箱规未维护\r\n");
                        //    right = false;
                        //}

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

                    this.Invoke(new Action(() => {
                        //InventoryForm form = new InventoryForm(di, docDetailList, materialList, hlaTagList, epcDetailList, huList);
                        //form.ShowDialog();
                        //加载数据到盘点窗口
                        this.parentForm.LoadDocNoInfo(di, docDetailList, materialList, hlaTagList, epcDetailList, huList);
                        this.Close();
                    }));
                }));
            }
            thread.IsBackground = true;
            thread.Start();

            
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TabTip.exe");
        }
    }
}
