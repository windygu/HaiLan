using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonLib;
using HLACommonLib.Model;
using Stimulsoft.Report;
using DMSkin.Metro.Forms;
using HLACommonLib.Model.PK;
using OSharp.Utility.Extensions;

namespace HLAPKChannelMachine.Utils
{
    public class PrinterHelper
    {
        private static string getJiDate(DateTime PICK_DATE)
        {
            if (PICK_DATE.Year == 2000)
            {
                return "";
            }
            return PICK_DATE.ToString("yyyy-MM-dd");
        }
        public static bool PrintHeilanShippingBox(string docno,string printerName, string LOUCENG, string vsart, string fydt, DateTime shipDate, string PARTNER, string HU, string PACKMAT, int QTY, double weight, out string errormsg)
        {
            ShippingLabel label = LocalDataService.GetShippingLabelByDOCNO(docno, fydt);

            if (label == null)
            {
                errormsg = "发运标签信息不存在";
                LogHelper.Error("发运标签信息不存在", string.Format("发运日期:{0},门店：{1},装运类型：{2},发运大厅:{3},DOCNO:{4}", shipDate.ToString("yyyyMMdd"), PARTNER,vsart,fydt,docno));
                return false;
            }

            try
            {
                string weightStr = string.Format("{0:0.00}", weight);
                weightStr += "KG";
                string filepath = Application.StartupPath + "\\CASERPT.mrt";
                StiReport stiReport = new StiReport();
                stiReport.Load(filepath);
                //设置报表内的参数值
                stiReport.Compile();
                stiReport["HWK_AREA"] = label.DISPATCH_AREA;//集货区编号
                stiReport["HWP_BP"] = label.STORE_ID.TrimStart('0');//门店代码
                stiReport["HWK_WEIGHT"] = weightStr;
                stiReport["HWK_HU"] = HU;//箱码条形码
                stiReport["HWK_BPDS"] = label.STORE_NAME;//门店名称
                stiReport["HWK_STRT"] = label.ADDRESS;//门店地址
                stiReport["HWK_TRANSPORT"] = label.VSART_DES;//货运方式
                stiReport["HWK_QTY"] = QTY.ToString();//货品数量
                stiReport["HWK_LEVECROSS"] = label.LANE_ID;//发货道口
                stiReport["HWK_WAVS"] = label.WAVE_AND_YT; //波次号
                stiReport["HWK_SORT"] = "";//滑道号
                stiReport["HWK_ROUNT"] = "(" + label.ROUTE_NO + ")" + label.ROUTE_DES;//线路
                stiReport["HWK_DATE"] = label.SHIP_DATE.ToString("yyyy-MM-dd");//发货时间

                /*
                int index = PACKMAT.IndexOf('[');
                stiReport["HWK_HUTYPE"] = index > 0 ? PACKMAT.Substring(0, index) : "";//箱型
                */
                stiReport["HWK_HUTYPE"] = PACKMAT;

                stiReport["HWK_FLOOR"] = LOUCENG;//仓库楼层
                stiReport["HWK_HUADD"] = HU;//箱码
                stiReport["ZSDABW_DES"] = label.ZSDABW_DES;
                stiReport["HWK_JIDATE"] = getJiDate(label.PICK_DATE);

                PrinterSettings printerSettings = new PrinterSettings();
                stiReport.Print(false, printerSettings);

                errormsg = "";
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error("打印发运箱箱标出错", ex.Message);
                errormsg = "打印发运箱箱标出错";
            }

            return false;
        }
        public static bool PrintaAjtShippingBox(string docno, string printerName, string LOUCENG, string vsart, string fydt, DateTime shipDate, string PARTNER, string HU, string PACKMAT, int QTY, double weight, out string errormsg)
        {
            ShippingLabel label = LocalDataService.GetShippingLabelByDOCNO(docno, fydt);

            if (label == null)
            {
                errormsg = "发运标签信息不存在";
                LogHelper.Error("发运标签信息不存在", string.Format("发运日期:{0},门店：{1},装运类型：{2},发运大厅:{3},DOCNO:{4}", shipDate.ToString("yyyyMMdd"), PARTNER, vsart, fydt, docno));
                return false;
            }

            try
            {
                string filepath = Application.StartupPath + "\\CASERPTAJT.mrt";
                StiReport stiReport = new StiReport();
                stiReport.Load(filepath);
                //设置报表内的参数值
                stiReport.Compile();
                stiReport["HWP_BP"] = label.STORE_ID.TrimStart('0');//门店代码
                stiReport["HWK_BPDS"] = label.STORE_NAME;//门店名称
                stiReport["HWK_STRT"] = label.ADDRESS;//门店地址
                stiReport["HWK_TRANSPORT"] = label.VSART_DES;//货运方式
                stiReport["HWK_QTY"] = QTY.ToString();//货品数量
                stiReport["HWK_LEVECROSS"] = label.LANE_ID;//发货道口
                stiReport["HWK_WAVS"] = label.WAVE_AND_YT; //波次号
                stiReport["HWK_SORT"] = "";//滑道号
                stiReport["HWK_ROUNT"] = "(" + label.ROUTE_NO + ")" + label.ROUTE_DES;//线路
                stiReport["HWK_DATE"] = label.SHIP_DATE.ToString("yyyy-MM-dd");//发货时间

                /*
                int index = PACKMAT.IndexOf('[');
                stiReport["HWK_HUTYPE"] = index > 0 ? PACKMAT.Substring(0, index) : "";//箱型
                */
                stiReport["HWK_HUTYPE"] = PACKMAT;

                stiReport["HWK_FLOOR"] = LOUCENG;//仓库楼层
                stiReport["HWK_HUADD"] = HU;//箱码
                stiReport["ZSDABW_DES"] = label.ZSDABW_DES;
                stiReport["HWK_HU"] = HU;//箱码条形码

                PrinterSettings printerSettings = new PrinterSettings();
                stiReport.Print(false, printerSettings);

                errormsg = "";
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error("打印发运箱箱标出错", ex.Message);
                errormsg = "打印发运箱箱标出错";
            }

            return false;
        }

        public static bool PrintErrorBoxTagByTable(List<InventoryOutLogDetailInfo> outlogList, List<PKDeliverErrorBox> pkDeliverErrorBoxList, string msg)
        {
            try
            {
                LogHelper.WriteLine("调用异常打印接口："+msg);
                string filepath = Application.StartupPath + "\\ERRORLABEL.mrt";
                StiReport stiReport = new StiReport();
                stiReport.Load(filepath);
                //设置报表内的参数值
                stiReport.Compile();
                int totalqty = (int)(pkDeliverErrorBoxList?.Sum(i => i.REAL).CastTo<int>(0));
                string hu = pkDeliverErrorBoxList?.FirstOrDefault()?.HU.CastTo<string>("");
                stiReport["HU"] = hu;
                stiReport["TOTALQTY"] = totalqty.ToString();
                DataTable content = new DataTable();
                content.Columns.Add(new DataColumn("ZSATNR", System.Type.GetType("System.String")));
                content.Columns.Add(new DataColumn("ZCOLSN", System.Type.GetType("System.String")));
                content.Columns.Add(new DataColumn("ZSIZTX", System.Type.GetType("System.String")));
                content.Columns.Add(new DataColumn("QTY", System.Type.GetType("System.Int32")));
                content.Columns.Add(new DataColumn("ADDQTY", System.Type.GetType("System.Int32")));
                content.Columns.Add(new DataColumn("YQTY", System.Type.GetType("System.Int32")));
                content.Columns.Add(new DataColumn("ADDYQTY", System.Type.GetType("System.Int32")));
                if(!msg.Contains(XIANG_MA_AND_XIA_JIA_DAN_GUAN_LIAN_BU_CUN_ZAI))
                {
                    if (pkDeliverErrorBoxList != null && pkDeliverErrorBoxList.Count > 0)
                    {
                        foreach (PKDeliverErrorBox pkDeliverErrorBox in pkDeliverErrorBoxList)
                        {
                            if (!string.IsNullOrEmpty(pkDeliverErrorBox.MATNR))
                            {
                                string zsatnr = pkDeliverErrorBox.ZSATNR;
                                string zcolsn = pkDeliverErrorBox.ZCOLSN;
                                string zsiztx = pkDeliverErrorBox.ZSIZTX;
                                int qty = (int)pkDeliverErrorBox.REAL;
                                int addqty = (int)pkDeliverErrorBox.ADD_REAL;
                                string newzsiztx = zsiztx;

                                int yingfa = 0;
                                int boxyingfa = 0;
                                int addboxyingfa = 0;
                                InventoryOutLogDetailInfo temp = outlogList.FirstOrDefault(x => x.PICK_TASK == pkDeliverErrorBox.PICK_TASK && x.PICK_TASK_ITEM == pkDeliverErrorBox.PICK_TASK_ITEM && x.PRODUCTNO == pkDeliverErrorBox.MATNR);
                                if (temp != null)
                                {
                                    yingfa = temp.QTY;
                                    boxyingfa = temp.QTY - temp.REALQTY;
                                    addboxyingfa = temp.QTY_ADD - temp.REALQTY_ADD;
                                }
                                if (boxyingfa != qty || addqty != addboxyingfa) //将列表的数据都显示出来
                                {
                                    DataRow row = content.NewRow();
                                    row["ZSATNR"] = zsatnr;
                                    row["ZCOLSN"] = zcolsn;
                                    row["ZSIZTX"] = newzsiztx;
                                    row["QTY"] = qty;
                                    row["ADDQTY"] = addqty;
                                    row["YQTY"] = qty - boxyingfa;
                                    row["ADDYQTY"] = addqty - addboxyingfa;
                                    content.Rows.Add(row);
                                }
                            }
                        }
                    }
                }
                else
                {
                    DataRow row = content.NewRow();
                    row["ZSATNR"] = "";
                    row["ZCOLSN"] = "";
                    row["ZSIZTX"] = "";
                    row["QTY"] = 0;
                    row["ADDQTY"] = 0;
                    row["YQTY"] = 0;
                    row["ADDYQTY"] = 0;
                    content.Rows.Add(row);
                }

                stiReport["REMARK"] = msg;

                PrinterSettings printerSettings = new PrinterSettings();
                stiReport.RegData("Inventory", content);
                stiReport.Print(false, printerSettings);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error("打印异常箱标出错", ex.Message);
            }

            return false;
        }

        private const string XIANG_MA_AND_XIA_JIA_DAN_GUAN_LIAN_BU_CUN_ZAI = "箱号和下架单未关联";

    }
}
