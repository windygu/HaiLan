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
using System.Threading;

namespace HLADeliverChannelMachine.Utils
{
    public class PrinterHelper
    {
        public static double calWeight(ShippingBox box)
        {
            double re = 0;
            if (box != null && box.Details != null)
            {
                foreach (ShippingBoxDetail b in box.Details)
                {
                    re += b.BRGEW;
                }
                string fhbc = box.PMAT_MATNR;
                re += LocalDataService.getXiangXingWeight(fhbc);
            }
            return re;
        }
        public static bool PrintTag(string docno, string pick_task, DateTime shipDate, string fydt, string vsart, ShippingBox shippingBox, bool BoxIsPrintMergeTag, bool IsLocalprint, out string errormsg)
        {
            ShippingLabel label = null;
            //启用本地打印的时候才需要使用label
            if (IsLocalprint)
            {
                label = LocalDataService.GetShippingLabelByDOCNO(docno, fydt);
                if (label == null)
                {
                    errormsg = "发运标签信息不存在:日期:" + shipDate.ToString() + " 门店:" + shippingBox.PARTNER + " 发运大厅：" + fydt + " 装运类型：" + vsart;

                    //errormsg = "发运标签信息不存在";
                    return false;
                }
            }
            // 判断是第几个箱拼箱
            int num = shippingBox.Details != null ? shippingBox.Details.Select(o => o.PICK_TASK).Distinct().Count() : 0;


            if (SysConfig.DeviceInfo.KF_LX == "X")
            {
                //箱装
                if (shippingBox.IsFull == 1)
                {
                    //满箱，只打印发运标签
                    if (IsLocalprint)
                        PrinterHelper.PrintShippingBox(SysConfig.PrinterName, label, shippingBox);
                    else
                        SAPDataService.PrintShippingBox(SysConfig.PrinterName, SysConfig.LGNUM, pick_task, shippingBox, SysConfig.DeviceInfo.LOUCENG);

                    //满箱且存在多个下架单则询问是否打印开箱/拼箱标签

                    if (num > 1)
                    {
                        if (BoxIsPrintMergeTag)
                        {
                            if (IsLocalprint)
                                PrinterHelper.PrintMixShippingBox(pick_task, SysConfig.PrinterName, label, shippingBox);
                            else
                                SAPDataService.PrintMixShippingBoxBeforeUpload(SysConfig.PrinterName, SysConfig.LGNUM, pick_task, shippingBox);
                        }
                    }
                }
                else
                {
                    //未满箱时询问是否打印开箱/拼箱标签
                    if (BoxIsPrintMergeTag)
                    {
                        //打印开箱/拼箱标签
                        if (IsLocalprint)
                            PrinterHelper.PrintMixShippingBox(pick_task, SysConfig.PrinterName, label, shippingBox);
                        else
                            SAPDataService.PrintMixShippingBoxBeforeUpload(SysConfig.PrinterName, SysConfig.LGNUM, pick_task, shippingBox);

                    }
                }
            }
            else
            {
                //挂装
                if (shippingBox.IsFull == 1)
                {
                    //如果满箱
                    //只有一个下架单或多个下架单，都要打印发运标签
                    if (IsLocalprint)
                        PrinterHelper.PrintShippingBox(SysConfig.PrinterName, label, shippingBox);
                    else
                        SAPDataService.PrintShippingBox(SysConfig.PrinterName, SysConfig.LGNUM, pick_task, shippingBox, SysConfig.DeviceInfo.LOUCENG);

                    //存在多个下架单则必打印开箱/拼箱标签，第一个下架单为开箱，之后都为拼箱
                    if (num > 1)
                    {
                        if (IsLocalprint)
                            PrinterHelper.PrintMixShippingBox(pick_task, SysConfig.PrinterName, label, shippingBox);
                        else
                            SAPDataService.PrintMixShippingBoxBeforeUpload(SysConfig.PrinterName, SysConfig.LGNUM, pick_task, shippingBox);
                    }
                }
                else
                {
                    //非满箱，不打印发运标签，只打印拼箱标签
                    if (IsLocalprint)
                        PrinterHelper.PrintMixShippingBox(pick_task, SysConfig.PrinterName, label, shippingBox);
                    else
                        SAPDataService.PrintMixShippingBoxBeforeUpload(SysConfig.PrinterName, SysConfig.LGNUM, pick_task, shippingBox);
                }
            }
            errormsg = "";
            return true;
            //if (!result)
            //{
            //    MetroMessageBox.Show(this, "打印发运标签失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }
        private static string getJiDate(DateTime PICK_DATE)
        {
            if (PICK_DATE.Year == 2000)
            {
                return "";
            }
            return PICK_DATE.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 打印发运标签
        /// </summary>
        /// <param name="label"></param>
        /// <param name="box"></param>
        /// <param name="IsLocalprint">true 本地打印 false 远程打印</param>
        /// <returns></returns>
        /// 
        public static bool PrintShippingBox(string printerName, ShippingLabel label, ShippingBox box)
        {
            double weight = calWeight(box);
            string weightStr = string.Format("{0:0.00}", weight);
            weightStr += "KG";

            new Thread(new ThreadStart(() =>
                {
                    try
                    {
                        string filepath = Application.StartupPath + "\\CASERPT.mrt";
                        StiReport stiReport1 = new StiReport();
                        stiReport1.Load(filepath);
                        //////设置报表内的参数值
                        stiReport1.Compile();
                        stiReport1["HWK_AREA"] = label.DISPATCH_AREA;//集货区编号
                        stiReport1["HWP_BP"] = label.STORE_ID.TrimStart('0');//门店代码
                        stiReport1["HWK_WEIGHT"] = weightStr;

                        stiReport1["HWK_HU"] = box.HU;//箱码条形码
                        stiReport1["HWK_BPDS"] = label.STORE_NAME;//门店名称
                        stiReport1["HWK_STRT"] = label.ADDRESS;//门店地址
                        stiReport1["HWK_TRANSPORT"] = label.VSART_DES;//货运方式
                        stiReport1["HWK_QTY"] = box.QTY.ToString();//货品数量
                        stiReport1["HWK_LEVECROSS"] = label.LANE_ID;//发货道口
                        stiReport1["HWK_WAVS"] = label.WAVE_AND_YT; //波次号
                        stiReport1["HWK_SORT"] = "";//滑道号
                        stiReport1["HWK_ROUNT"] = "(" + label.ROUTE_NO + ")" + label.ROUTE_DES;//线路
                        stiReport1["HWK_DATE"] = label.SHIP_DATE.ToString("yyyy-MM-dd");//发货时间
                        // stiReport1["HWK_HUTYPE"] = box.MAKTX;//箱型
                        string sMAKTX = box.MAKTX;
                        stiReport1["HWK_HUTYPE"] = sMAKTX.Substring(0, sMAKTX.IndexOf('['));//箱型
                        stiReport1["HWK_FLOOR"] = box.Floor;//仓库楼层
                        stiReport1["HWK_HUADD"] = box.HU;//箱码
                        stiReport1["ZSDABW_DES"] = label.ZSDABW_DES;
                        stiReport1["HWK_JIDATE"] = getJiDate(label.PICK_DATE);
                        //stiReport1["HWK_PROP1"] = "1";
                        //stiReport1["HWK_PROP2"] = "2";
                        //stiReport1["HWK_PROP3"] = "3";
                        //stiReport1["HWK_PROP4"] = "4";
                        //stiReport1["HWK_PROP5"] = "5";
                        //stiReport1["HWK_PROP6"] = "6";
                        //stiReport1["HWK_PROP7"] = "7";
                        //stiReport1["HWK_PROP8"] = "8";
                        //stiReport1["HWK_PROP9"] = "9";

                        //Create Printer Settings
                        PrinterSettings printerSettings = new PrinterSettings();
                        //Set Printer to Use for Printing
                        //printerSettings.PrinterName = printerName;
                        //Direct Print - Don't Show Print Dialog
                        stiReport1.Print(false, printerSettings);

                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error("打印发运箱箱标出错", ex.Message);
                    }
                })).Start();
            return true;
        }

        /// <summary>
        /// 打印开箱/拼箱标签
        /// </summary>
        /// <param name="label"></param>
        /// <param name="box"></param>
        /// <returns></returns>
        public static bool PrintMixShippingBox(string picktask, string printerName, ShippingLabel label, ShippingBox box)
        {
            new Thread(new ThreadStart(() =>
            {
                try
                {
                    //添加数据到DataTable
                    if (box.Details != null && box.Details.Count > 0)
                    {
                        List<string> pickTaskList = box.Details.FindAll(i => i.IsADD == 0).Select(o => o.PICK_TASK).Distinct().ToList();
                        if (pickTaskList != null && pickTaskList.Count > 0)
                        {
                            if (picktask == null)
                            {
                                foreach (var item in pickTaskList)
                                {
                                    picktask = item;
                                    // 获取是第几箱
                                    int boxindex = pickTaskList.IndexOf(picktask) + 1;
                                    //box.Details.OrderBy(m => m.Id).Select(o => o.PICK_TASK).Distinct().ToList().IndexOf(picktask) + 1;

                                    string filepath = Application.StartupPath + "\\PinBoxCASERPT.mrt";
                                    StiReport stiReport1 = new StiReport();
                                    stiReport1.Load(filepath);
                                    //////设置报表内的参数值
                                    stiReport1.Compile();
                                    //Create Printer Settings
                                    PrinterSettings printerSettings = new PrinterSettings();
                                    //Set Printer to Use for Printing
                                    //printerSettings.PrinterName = printerName;
                                    stiReport1["HWK_TagTitle"] = boxindex == 1 ? "开箱标签" : "拼箱标签";
                                    stiReport1["HWK_AREA"] = label.DISPATCH_AREA;//集货区编号
                                    stiReport1["HWP_BP"] = label.STORE_ID.TrimStart('0');//门店代码
                                    stiReport1["HWK_BPDS"] = label.STORE_NAME;//门店名称

                                    StringBuilder qtyDesc = new StringBuilder();
                                    Dictionary<string, int> dic = new Dictionary<string, int>();
                                    foreach (ShippingBoxDetail detail in box.Details.FindAll(i => i.IsADD == 0 && i.PICK_TASK == picktask))
                                    {
                                        if (dic.ContainsKey(detail.UOM))
                                            dic[detail.UOM]++;
                                        else
                                            dic.Add(detail.UOM, 1);
                                    }
                                    foreach (KeyValuePair<string, int> keyvalue in dic)
                                    {
                                        qtyDesc.AppendFormat("{0}{1}", keyvalue.Value,
                                            SysConfig.UOMDic.ContainsKey(keyvalue.Key) ?
                                            SysConfig.UOMDic[keyvalue.Key] : keyvalue.Key);
                                    }

                                    stiReport1["HWK_QtyDesc"] = qtyDesc.ToString();//数量
                                                                                   //stiReport1["HWK_QtyDesc"] = box.Details.FindAll(i => i.IsADD == 0 && i.PICK_TASK == picktask).Count().ToString();//数量
                                    stiReport1["HWK_WAVS"] = "(" + label.COLLECT_WAVE + ")"; //波次号
                                                                                             // stiReport1["HWK_HUTYPE"] = box.MAKTX;//箱型
                                    string sMAKTX = box.MAKTX;
                                    stiReport1["HWK_HUTYPE"] = sMAKTX.Substring(0, sMAKTX.IndexOf('['));//箱型
                                    stiReport1["HWK_PickDoc"] = picktask;
                                    stiReport1["HWK_PinSerno"] = boxindex.ToString();
                                    stiReport1["HWK_HUADD"] = box.HU;//箱码


                                    //Direct Print - Don't Show Print Dialog
                                    stiReport1.Print(false, printerSettings);
                                }
                            }
                            else
                            {
                                // 获取是第几箱
                                int boxindex = pickTaskList.IndexOf(picktask) + 1;
                                //box.Details.OrderBy(m => m.Id).Select(o => o.PICK_TASK).Distinct().ToList().IndexOf(picktask) + 1;

                                string filepath = Application.StartupPath + "\\PinBoxCASERPT.mrt";
                                StiReport stiReport1 = new StiReport();
                                stiReport1.Load(filepath);
                                //////设置报表内的参数值
                                stiReport1.Compile();
                                //Create Printer Settings
                                PrinterSettings printerSettings = new PrinterSettings();
                                //Set Printer to Use for Printing
                                //printerSettings.PrinterName = printerName;
                                stiReport1["HWK_TagTitle"] = boxindex == 1 ? "开箱标签" : "拼箱标签";
                                stiReport1["HWK_AREA"] = label.DISPATCH_AREA;//集货区编号
                                stiReport1["HWP_BP"] = label.STORE_ID.TrimStart('0');//门店代码
                                stiReport1["HWK_BPDS"] = label.STORE_NAME;//门店名称
                                StringBuilder qtyDesc = new StringBuilder();
                                Dictionary<string, int> dic = new Dictionary<string, int>();
                                foreach (ShippingBoxDetail detail in box.Details.FindAll(i => i.IsADD == 0 && i.PICK_TASK == picktask))
                                {
                                    if (dic.ContainsKey(detail.UOM))
                                        dic[detail.UOM]++;
                                    else
                                        dic.Add(detail.UOM, 1);
                                }
                                foreach (KeyValuePair<string, int> keyvalue in dic)
                                {
                                    qtyDesc.AppendFormat("{0}{1}", keyvalue.Value,
                                        SysConfig.UOMDic.ContainsKey(keyvalue.Key) ?
                                        SysConfig.UOMDic[keyvalue.Key] : keyvalue.Key);
                                }

                                stiReport1["HWK_QtyDesc"] = qtyDesc.ToString();//数量
                                                                               //stiReport1["HWK_QtyDesc"] = box.Details.FindAll(i => i.IsADD == 0).Count(o => o.PICK_TASK == picktask).ToString();//数量
                                stiReport1["HWK_WAVS"] = "(" + label.COLLECT_WAVE + ")"; //波次号
                                                                                         //stiReport1["HWK_HUTYPE"] = box.MAKTX;//箱型
                                string sMAKTX = box.MAKTX;
                                stiReport1["HWK_HUTYPE"] = sMAKTX.Substring(0, sMAKTX.IndexOf('['));//箱型
                                stiReport1["HWK_PickDoc"] = picktask;
                                stiReport1["HWK_PinSerno"] = boxindex.ToString();
                                stiReport1["HWK_HUADD"] = box.HU;//箱码


                                //Direct Print - Don't Show Print Dialog
                                stiReport1.Print(false, printerSettings);
                            }

                        }
                    }

                }
                catch (Exception ex)
                {
                    LogHelper.Error("打印发运箱箱标出错", ex.Message);
                }

            })).Start();
            return true;
        }
    }
}
