using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HLACommonLib;
using HLACommonLib.Model;
using Newtonsoft.Json;
using HLACommonLib.Model.PK;
using System.Globalization;
using HLACommonLib.Model.ENUM;
using System.Windows.Forms;
using OSharp.Utility.Extensions;
using System.Diagnostics;

namespace HLACommonLib
{
    public class LocalDataService
    {
        #region 平库挂装机收货相关功能
        /// <summary>
        /// 根据设备编号获取箱码前缀
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <returns></returns>
        public static bool IsPMHuConfigExists(string deviceNo)
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM PMHuConfig WHERE DeviceNO = @DeviceNO";
                SqlParameter p1 = DBHelper.CreateParameter("@DeviceNO", deviceNo);
                int num = int.Parse(DBHelper.GetValue(sql, false, p1).ToString());
                if (num > 0)
                {
                    return true;
                }
                else
                {
                    //查找最大的前缀，并加1，插入
                    sql = "SELECT TOP 1 HuPrefix FROM PMHuConfig ORDER BY HuPrefix DESC";
                    object result = DBHelper.GetValue(sql, false);
                    string huPrefix = "";
                    if (result == null || string.IsNullOrEmpty(result.ToString()))
                    {
                        huPrefix = "XG01";
                    }
                    else
                    {
                        huPrefix = result.ToString();
                        string secondword = huPrefix.Substring(2, 2);
                        string newSecondWord = (int.Parse(secondword) + 1).ToString().PadLeft(2, '0');

                        huPrefix = huPrefix.Substring(0, 2) + newSecondWord;
                    }

                    sql = "INSERT INTO PMHuConfig(DeviceNO, HuPrefix, Hu) VALUES (@DeviceNO, @HuPrefix, 1)";
                    SqlParameter p2 = DBHelper.CreateParameter("@DeviceNO", deviceNo);
                    SqlParameter p3 = DBHelper.CreateParameter("@HuPrefix", huPrefix);
                    DBHelper.ExecuteSql(sql, false, p2, p3);

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据设备编号获取箱码
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <returns></returns>
        public static string GetNewPMHu(string deviceNo)
        {
            try
            {
                string sql = @"SELECT HuPrefix, Hu FROM PMHuConfig WHERE DeviceNO = @DeviceNO;
                                UPDATE PMHuConfig SET Hu = Hu + 1 WHERE DeviceNO = @DeviceNO;";
                SqlParameter p1 = DBHelper.CreateParameter("@DeviceNO", deviceNo);
                DataTable table = DBHelper.GetTable(sql, false, p1);
                if (table != null && table.Rows.Count > 0)
                {
                    string huPrefix = table.Rows[0]["HuPrefix"].ToString();
                    long hu = long.Parse(table.Rows[0]["Hu"].ToString());

                    if (hu >= 99999998) //当数量到达99999998时，将数量重置为1
                    {
                        sql = "UPDATE PMHuConfig SET Hu = 1 WHERE DeviceNO = @DeviceNO;";
                        SqlParameter p2 = DBHelper.CreateParameter("@DeviceNO", deviceNo);
                        DBHelper.ExecuteSql(sql, false, p2);
                    }

                    return huPrefix + hu.ToString().PadLeft(8, '0');
                }

                return "";
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region 平库大批量发货通道机相关功能代码
        /// <summary>
        /// 保存发运箱和门店关系表数据
        /// </summary>
        /// <param name="HU"></param>
        /// <param name="LOUCENG"></param>
        /// <param name="SHIP_DATE"></param>
        /// <param name="SHULIANG"></param>
        /// <param name="STORE_ID"></param>
        /// <param name="XIANGXING"></param>
        /// <returns></returns>
        public static bool SaveBoxPartnerMapInfo(string HU, string LOUCENG, DateTime? SHIP_DATE, float SHULIANG, string STORE_ID, string XIANGXING)
        {
            try
            {
                string sql = string.Format("P_BoxPartnerMap_Save ");
                SqlParameter p1 = DBHelper.CreateParameter("@HU", HU);
                SqlParameter p2 = DBHelper.CreateParameter("@PARTNER", STORE_ID);
                SqlParameter p3 = DBHelper.CreateParameter("@LOUCENG", LOUCENG);
                SqlParameter p4 = DBHelper.CreateParameter("@SHIP_DATE", SHIP_DATE);
                SqlParameter p5 = DBHelper.CreateParameter("@PACKMAT", XIANGXING);
                SqlParameter p6 = DBHelper.CreateParameter("@QUAN", SHULIANG);
                int result = int.Parse(DBHelper.GetValue(sql, true, p1, p2, p3, p4, p5, p6).ToString());

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 根据箱号和发运日期获取箱号和门店对应关系
        /// </summary>
        /// <param name="HU"></param>
        /// <returns></returns>
        public static List<BoxPickTaskMapInfo> GetBoxPickTaskMapInfoListByHU(string HU)
        {
            try
            {
                string sql = "SELECT HU, PICK_TASK, PARTNER, LOUCENG, SHIP_DATE, PACKMAT, QUAN, IS_SCAN, IS_SHORT_PICK FROM BoxPickTaskMap WHERE HU = @HU";
                SqlParameter p1 = DBHelper.CreateParameter("@HU", HU);
                DataTable table = DBHelper.GetTable(sql, false, p1);
                if (table != null && table.Rows.Count > 0)
                {
                    List<BoxPickTaskMapInfo> list = new List<BoxPickTaskMapInfo>();
                    foreach (DataRow row in table.Rows)
                    {
                        BoxPickTaskMapInfo item = new BoxPickTaskMapInfo();
                        item.HU = row["HU"] == null ? null : row["HU"].ToString();
                        item.PICK_TASK = row["PICK_TASK"] == null ? null : row["PICK_TASK"].ToString();
                        item.PARTNER = row["PARTNER"] == null ? null : row["PARTNER"].ToString();
                        item.LOUCENG = row["LOUCENG"] == null ? null : row["LOUCENG"].ToString();
                        try
                        {
                            item.SHIP_DATE = DateTime.Parse(row["SHIP_DATE"].ToString());
                        }
                        catch
                        {
                            item.SHIP_DATE = (DateTime?)null;
                        }
                        item.PACKMAT = row["PACKMAT"] == null ? null : row["PACKMAT"].ToString();
                        try
                        {
                            item.QUAN = float.Parse(row["QUAN"].ToString());
                        }
                        catch
                        {
                            item.QUAN = 0;
                        }
                        try
                        {
                            item.IS_SCAN = bool.Parse(row["IS_SCAN"].ToString());
                        }
                        catch
                        {
                            item.IS_SCAN = false;
                        }
                        try
                        {
                            item.IS_SHORT_PICK = bool.Parse(row["IS_SHORT_PICK"].ToString());
                        }
                        catch
                        {
                            item.IS_SHORT_PICK = false;
                        }

                        list.Add(item);
                    }

                    return list;
                }

                return null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        public static BoxPickTaskUnionInfo GetBoxPickTaskUnionListByHU(string HU)
        {
            try
            {
                string sql = "P_BoxPickTask_GetByHu ";
                SqlParameter p1 = DBHelper.CreateParameter("@HU", HU);

                DataSet ds = DBHelper.GetDataSet(sql, true, p1);
                BoxPickTaskUnionInfo unionInfo = new BoxPickTaskUnionInfo();

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dtBoxPickTaskMap = ds.Tables[0];
                    DataTable dtInventoryOutLogDetail = ds.Tables[1];
                    DataTable dtPickTaskUnScanNum = ds.Tables[2];

                    #region 箱码与下架单对应表
                    if (dtBoxPickTaskMap != null && dtBoxPickTaskMap.Rows.Count > 0)
                    {
                        unionInfo.BoxPickTaskMapInfoList = new List<BoxPickTaskMapInfo>();
                        foreach (DataRow row in dtBoxPickTaskMap.Rows)
                        {
                            BoxPickTaskMapInfo item = new BoxPickTaskMapInfo();
                            item.HU = row["HU"] == null ? null : row["HU"].ToString();
                            item.PICK_TASK = row["PICK_TASK"] == null ? null : row["PICK_TASK"].ToString();
                            item.PARTNER = row["PARTNER"] == null ? null : row["PARTNER"].ToString();
                            item.LOUCENG = row["LOUCENG"] == null ? null : row["LOUCENG"].ToString();
                            try
                            {
                                item.SHIP_DATE = DateTime.Parse(row["SHIP_DATE"].ToString());
                            }
                            catch
                            {
                                item.SHIP_DATE = (DateTime?)null;
                            }
                            item.PACKMAT = row["PACKMAT"] == null ? null : row["PACKMAT"].ToString();
                            try
                            {
                                item.QUAN = float.Parse(row["QUAN"].ToString());
                            }
                            catch
                            {
                                item.QUAN = 0;
                            }
                            try
                            {
                                item.IS_SCAN = bool.Parse(row["IS_SCAN"].ToString());
                            }
                            catch
                            {
                                item.IS_SCAN = false;
                            }
                            try
                            {
                                item.IS_SHORT_PICK = bool.Parse(row["IS_SHORT_PICK"].ToString());
                            }
                            catch
                            {
                                item.IS_SHORT_PICK = false;
                            }

                            unionInfo.BoxPickTaskMapInfoList.Add(item);
                        }
                    }
                    #endregion

                    #region 下架单明细
                    if (dtInventoryOutLogDetail != null && dtInventoryOutLogDetail.Rows.Count > 0)
                    {
                        unionInfo.InventoryOutLogDetailList = new List<InventoryOutLogDetailInfo>();
                        foreach (DataRow row in dtInventoryOutLogDetail.Rows)
                        {
                            InventoryOutLogDetailInfo item = new InventoryOutLogDetailInfo();

                            item.VSART = string.IsNullOrEmpty(row["VSART"].ToString().Trim()) ? "" : row["VSART"].ToString().Trim();
                            item.IS_FBC = string.IsNullOrEmpty(row["IS_FBC"].ToString().Trim()) ? "" : row["IS_FBC"].ToString().Trim();

                            item.DOCNO = string.IsNullOrEmpty(row["DOCNO"].ToString().Trim()) ? "" : row["DOCNO"].ToString().Trim();
                            item.ITEMNO = string.IsNullOrEmpty(row["ITEMNO"].ToString().Trim()) ? "" : row["ITEMNO"].ToString().Trim();
                            item.LGNUM = string.IsNullOrEmpty(row["LGNUM"].ToString().Trim()) ? "" : row["LGNUM"].ToString().Trim();
                            item.LGTYP = string.IsNullOrEmpty(row["LGTYP"].ToString().Trim()) ? "" : row["LGTYP"].ToString().Trim();
                            item.LGTYP_R = string.IsNullOrEmpty(row["LGTYP_R"].ToString().Trim()) ? "" : row["LGTYP_R"].ToString().Trim();
                            item.PARTNER = string.IsNullOrEmpty(row["PARTNER"].ToString().Trim()) ? "" : row["PARTNER"].ToString().Trim();
                            item.PICK_TASK = string.IsNullOrEmpty(row["PICK_TASK"].ToString().Trim()) ? "" : row["PICK_TASK"].ToString().Trim();
                            item.PICK_TASK_ITEM = string.IsNullOrEmpty(row["PICK_TASK_ITEM"].ToString().Trim()) ? "" : row["PICK_TASK_ITEM"].ToString().Trim();
                            item.PRODUCTNO = string.IsNullOrEmpty(row["PRODUCTNO"].ToString().Trim()) ? "" : row["PRODUCTNO"].ToString().Trim();
                            item.QTY = string.IsNullOrEmpty(row["QTY"].ToString().Trim()) ? 0 : int.Parse(row["QTY"].ToString().Trim());
                            //item.REALQTY_ADD = string.IsNullOrEmpty(row["REALQTY_ADD"].ToString().Trim()) ? 0 : int.Parse(row["REALQTY_ADD"].ToString().Trim());
                            item.SHIP_DATE = string.IsNullOrEmpty(row["SHIP_DATE"].ToString().Trim()) ? new DateTime(1900, 1, 1) : DateTime.Parse(row["SHIP_DATE"].ToString().Trim());
                            item.STOBIN = string.IsNullOrEmpty(row["STOBIN"].ToString().Trim()) ? "" : row["STOBIN"].ToString().Trim();
                            item.UOM = string.IsNullOrEmpty(row["UOM"].ToString().Trim()) ? "" : row["UOM"].ToString().Trim();
                            item.ZXJD_TYPE = string.IsNullOrEmpty(row["ZXJD_TYPE"].ToString().Trim()) ? "" : row["ZXJD_TYPE"].ToString().Trim();
                            item.IsOut = string.IsNullOrEmpty(row["IsOut"].ToString().Trim()) ? (byte)0 : byte.Parse(row["IsOut"].ToString().Trim());
                            item.REALQTY = string.IsNullOrEmpty(row["REALQTY"].ToString().Trim()) ? 0 : int.Parse(row["REALQTY"].ToString().Trim());
                            item.REALQTY_ADD = string.IsNullOrEmpty(row["REALQTY_ADD"].ToString().Trim()) ? 0 : int.Parse(row["REALQTY_ADD"].ToString().Trim());
                            unionInfo.InventoryOutLogDetailList.Add(item);
                        }
                    }
                    #endregion

                    #region 下架单未扫描箱数
                    if(dtPickTaskUnScanNum != null && dtPickTaskUnScanNum.Rows.Count > 0 && unionInfo.InventoryOutLogDetailList != null)
                    {
                        foreach(DataRow row in dtPickTaskUnScanNum.Rows)
                        {
                            string picktask = string.IsNullOrEmpty(row["PICK_TASK"].ToString().Trim()) ? "" : row["PICK_TASK"].ToString().Trim();
                            int unScanCount = string.IsNullOrEmpty(row["UNSCAN_COUNT"].ToString().Trim()) ? 0 : int.Parse(row["UNSCAN_COUNT"].ToString().Trim());
                            unionInfo.InventoryOutLogDetailList.ForEach((i) => {
                                if (i.PICK_TASK == picktask)
                                    i.UnScanBoxCount = unScanCount;
                            });
                        }
                    }
                    #endregion
                }

                return unionInfo;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 获取发运信息明细
        /// </summary>
        /// <param name="LOUCENG"></param>
        /// <param name="LGTYP_R"></param>
        /// <param name="shipDate"></param>
        /// <param name="PARTNER"></param>
        /// <returns></returns>
        public static List<DeliverDetailInfo> GetDeliverDetail(string LOUCENG, List<string> LGTYP_R, DateTime shipDate, string PARTNER)
        {
            try
            {
                //构建存储类型字符串
                StringBuilder sb = new StringBuilder();
                if (LGTYP_R != null && LGTYP_R.Count > 0)
                {
                    foreach (string lg in LGTYP_R)
                    {
                        sb.Append(string.Format("{0},", lg));
                    }
                }
                string lgtyp = sb.ToString().Trim(',');

                string sql = "P_DeliverDetail_Get  ";
                SqlParameter p1 = DBHelper.CreateParameter("@LOUCENG", LOUCENG);
                SqlParameter p2 = DBHelper.CreateParameter("@LGTYP_R", lgtyp);
                SqlParameter p3 = DBHelper.CreateParameter("@SHIP_DATE", shipDate);
                SqlParameter p4 = DBHelper.CreateParameter("@PARTNER", PARTNER);
                DataTable table = DBHelper.GetTable(sql, true, p1, p2, p3, p4);
                if (table != null && table.Rows.Count > 0)
                {
                    List<DeliverDetailInfo> list = new List<DeliverDetailInfo>();
                    foreach (DataRow row in table.Rows)
                    {
                        DeliverDetailInfo item = new DeliverDetailInfo();
                        item.LOUCENG = LOUCENG;
                        item.SHIP_DATE = shipDate;
                        item.PARTNER = PARTNER;
                        item.ZSATNR = row["ZSATNR"] == null ? null : row["ZSATNR"].ToString();
                        item.ZCOLSN = row["ZCOLSN"] == null ? null : row["ZCOLSN"].ToString();
                        item.ZSIZTX = row["ZSIZTX"] == null ? null : row["ZSIZTX"].ToString();
                        item.QUAN = long.Parse(row["QUAN"].ToString());
                        item.REAL = long.Parse(row["REAL"].ToString());
                        item.DIFF = long.Parse(row["DIFF"].ToString());
                        list.Add(item);
                    }

                    return list;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 更新下架单实发数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool UpdateInventoryOutLogDetailRealQty(InventoryOutLogDetailInfo item)
        {
            try
            {
                string sql = @"UPDATE InventoryOutLogDetail SET REALQTY = @REALQTY, REALQTY_ADD = @REALQTY_ADD 
                                WHERE LGNUM = @LGNUM AND PICK_TASK = @PICK_TASK AND PICK_TASK_ITEM = @PICK_TASK_ITEM";
                SqlParameter p1 = DBHelper.CreateParameter("@LGNUM", item.LGNUM);
                SqlParameter p2 = DBHelper.CreateParameter("@PICK_TASK", item.PICK_TASK);
                SqlParameter p3 = DBHelper.CreateParameter("@PICK_TASK_ITEM", item.PICK_TASK_ITEM);
                SqlParameter p4 = DBHelper.CreateParameter("@REALQTY", item.REALQTY);
                SqlParameter p5 = DBHelper.CreateParameter("@REALQTY_ADD", item.REALQTY_ADD);
                int result = DBHelper.ExecuteSql(sql, false, p1, p2, p3, p4, p5);

                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 批量更新下架单实发数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool UpdateInventoryOutLogDetailRealQty(List<InventoryOutLogDetailInfo> itemList)
        {
            try
            {
                StringBuilder sqlTemp = new StringBuilder();
                if(itemList != null && itemList.Count > 0)
                {
                    foreach(InventoryOutLogDetailInfo item in itemList)
                    {
                        sqlTemp.Append(string.Format(@"UPDATE InventoryOutLogDetail SET REALQTY = {0}, REALQTY_ADD = {1} 
                                WHERE LGNUM = '{2}' AND PICK_TASK = '{3}' AND PICK_TASK_ITEM = '{4}';", item.REALQTY, item.REALQTY_ADD,
                                item.LGNUM, item.PICK_TASK, item.PICK_TASK_ITEM));
                    }

                    int result = DBHelper.ExecuteSql(sqlTemp.ToString(), false);
                    return result > 0 ? true : false;
                }
                //string sql = @"UPDATE InventoryOutLogDetail SET REALQTY = @REALQTY, REALQTY_ADD = @REALQTY_ADD 
                //                WHERE LGNUM = @LGNUM AND PICK_TASK = @PICK_TASK AND PICK_TASK_ITEM = @PICK_TASK_ITEM";
                //SqlParameter p1 = DBHelper.CreateParameter("@LGNUM", item.LGNUM);
                //SqlParameter p2 = DBHelper.CreateParameter("@PICK_TASK", item.PICK_TASK);
                //SqlParameter p3 = DBHelper.CreateParameter("@PICK_TASK_ITEM", item.PICK_TASK_ITEM);
                //SqlParameter p4 = DBHelper.CreateParameter("@REALQTY", item.REALQTY);
                //SqlParameter p5 = DBHelper.CreateParameter("@REALQTY_ADD", item.REALQTY_ADD);
                //int result = DBHelper.ExecuteSql(sql, false, p1, p2, p3, p4, p5);

                //return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 设置发运箱是否已扫描
        /// </summary>
        /// <param name="hu"></param>
        /// <param name="isScan"></param>
        /// <returns></returns>
        public static bool SetDeliverBoxIsScan(string hu, bool isScan)
        {
            try
            {
                string sql = @"UPDATE BoxPickTaskMap SET IS_SCAN = @IS_SCAN 
                                WHERE HU = @HU";
                SqlParameter p1 = DBHelper.CreateParameter("@HU", hu);
                SqlParameter p2 = DBHelper.CreateParameter("@IS_SCAN", isScan);
                int result = DBHelper.ExecuteSql(sql, false, p1, p2);

                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            return false;
        }

        public static int CountUnScanDeliverBox(string pickTask)
        {
            try
            {
                string sql = @"SELECT COUNT(*)
                               FROM BoxPickTaskMap WHERE PICK_TASK = @PICK_TASK AND IS_SCAN = 0";
                SqlParameter p1 = DBHelper.CreateParameter("@PICK_TASK", pickTask);
                int num = int.Parse(DBHelper.GetValue(sql, false, p1).ToString());

                return num;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 保存发运明细数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool SaveDeliverDetailInfo(DeliverDetailInfo item)
        {
            try
            {
                string sql = "P_DeliverDetail_Save";
                SqlParameter p1 = DBHelper.CreateParameter("@LOUCENG", item.LOUCENG);
                SqlParameter p2 = DBHelper.CreateParameter("@PARTNER", item.PARTNER);
                SqlParameter p3 = DBHelper.CreateParameter("@SHIP_DATE", item.SHIP_DATE);
                SqlParameter p4 = DBHelper.CreateParameter("@ZSATNR", item.ZSATNR);
                SqlParameter p5 = DBHelper.CreateParameter("@ZCOLSN", item.ZCOLSN);
                SqlParameter p6 = DBHelper.CreateParameter("@ZSIZTX", item.ZSIZTX);
                SqlParameter p7 = DBHelper.CreateParameter("@QUAN", item.QUAN);
                SqlParameter p8 = DBHelper.CreateParameter("@REAL", item.REAL);
                SqlParameter p9 = DBHelper.CreateParameter("@DIFF", item.DIFF);
                int result = int.Parse(DBHelper.GetValue(sql, true, p1, p2, p3, p4, p5, p6, p7, p8, p9).ToString());

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 根据楼层和发货时间获取发运错误箱列表
        /// </summary>
        /// <param name="LOUCENG"></param>
        /// <param name="shipDate"></param>
        /// <returns></returns>
        public static List<PKDeliverErrorBox> GetDeliverErrorBoxListByLOUCENGAndSHIPDATE(string LOUCENG)
        {
            try
            {
                string sql = @"SELECT BOXGUID, LOUCENG, PARTNER, SHIP_DATE, HU, ZSATNR, ZCOLSN, ZSIZTX,
                                      QUAN, REAL, DIFF, REMARK, SHORTQTY, PICK_TASK, PICK_TASK_ITEM, MATNR, ADD_REAL
                               FROM DeliverErrorBox WHERE LOUCENG = @LOUCENG  
                               ORDER BY CreateTime ASC";
                SqlParameter p1 = DBHelper.CreateParameter("@LOUCENG", LOUCENG);
                DataTable table = DBHelper.GetTable(sql, false, p1);
                if (table != null && table.Rows.Count > 0)
                {
                    List<PKDeliverErrorBox> list = new List<PKDeliverErrorBox>();
                    foreach (DataRow row in table.Rows)
                    {
                        PKDeliverErrorBox item = new PKDeliverErrorBox();
                        item.BOXGUID = row["BOXGUID"] == null ? null : row["BOXGUID"].ToString();
                        item.LOUCENG = row["LOUCENG"] == null ? null : row["LOUCENG"].ToString();
                        item.PARTNER = row["PARTNER"] == null ? null : row["PARTNER"].ToString();
                        item.SHIP_DATE = DateTime.Parse(row["SHIP_DATE"].ToString());
                        item.HU = row["HU"] == null ? null : row["HU"].ToString();
                        item.MATNR = row["MATNR"] != null ? row["MATNR"].ToString() : null;
                        item.ZSATNR = row["ZSATNR"] == null ? null : row["ZSATNR"].ToString();
                        item.ZCOLSN = row["ZCOLSN"] == null ? null : row["ZCOLSN"].ToString();
                        item.ZSIZTX = row["ZSIZTX"] == null ? null : row["ZSIZTX"].ToString();
                        item.QUAN = long.Parse(row["QUAN"].ToString());
                        item.REAL = long.Parse(row["REAL"].ToString());
                        item.ADD_REAL = row["ADD_REAL"] != null ? long.Parse(row["ADD_REAL"].ToString()) : 0;
                        item.DIFF = long.Parse(row["DIFF"].ToString());
                        item.SHORTQTY = long.Parse(row["SHORTQTY"].ToString());
                        item.REMARK = row["REMARK"] == null ? null : row["REMARK"].ToString();
                        item.PICK_TASK = row["PICK_TASK"] == null ? null : row["PICK_TASK"].ToString();
                        item.PICK_TASK_ITEM = row["PICK_TASK_ITEM"] == null ? null : row["PICK_TASK_ITEM"].ToString();
                        list.Add(item);
                    }

                    return list;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 保存发运错误箱信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool SaveDeliverErrorBox(PKDeliverErrorBox item)
        {
            try
            {
                string sql = @"INSERT INTO DeliverErrorBox(BOXGUID, LOUCENG, PICK_TASK, PICK_TASK_ITEM, PARTNER, SHIP_DATE, HU, MATNR, ZSATNR, ZCOLSN, ZSIZTX, QUAN, REAL, ADD_REAL, DIFF, REMARK, CreateTime,SHORTQTY)
                                VALUES (@BOXGUID, @LOUCENG, @PICK_TASK, @PICK_TASK_ITEM, @PARTNER, @SHIP_DATE, @HU, @MATNR, @ZSATNR, @ZCOLSN, @ZSIZTX, @QUAN, @REAL, @ADD_REAL, @DIFF, @REMARK, @CreateTime,@SHORTQTY)";
                SqlParameter p1 = DBHelper.CreateParameter("@BOXGUID", item.BOXGUID);
                SqlParameter p2 = DBHelper.CreateParameter("@LOUCENG", item.LOUCENG);
                SqlParameter p3 = DBHelper.CreateParameter("@PARTNER", item.PARTNER);
                SqlParameter p4 = DBHelper.CreateParameter("@SHIP_DATE", item.SHIP_DATE);
                SqlParameter p5 = DBHelper.CreateParameter("@HU", item.HU);
                SqlParameter p6 = DBHelper.CreateParameter("@ZSATNR", item.ZSATNR);
                SqlParameter p7 = DBHelper.CreateParameter("@ZCOLSN", item.ZCOLSN);
                SqlParameter p8 = DBHelper.CreateParameter("@ZSIZTX", item.ZSIZTX);
                SqlParameter p9 = DBHelper.CreateParameter("@QUAN", item.QUAN);
                SqlParameter p10 = DBHelper.CreateParameter("@REAL", item.REAL);
                SqlParameter p11 = DBHelper.CreateParameter("@DIFF", item.DIFF);
                SqlParameter p12 = DBHelper.CreateParameter("@REMARK", item.REMARK);
                SqlParameter p13 = DBHelper.CreateParameter("@CreateTime", DateTime.Now);
                SqlParameter p14 = DBHelper.CreateParameter("@PICK_TASK", item.PICK_TASK);
                SqlParameter p15 = DBHelper.CreateParameter("@SHORTQTY", item.SHORTQTY);
                SqlParameter p16 = DBHelper.CreateParameter("@MATNR", item.MATNR);
                SqlParameter p17 = DBHelper.CreateParameter("@ADD_REAL", item.ADD_REAL);
                SqlParameter p18 = DBHelper.CreateParameter("@PICK_TASK_ITEM", item.PICK_TASK_ITEM);
                int result = DBHelper.ExecuteSql(sql, false, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18);

                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 根据楼层和发货日期获取发运箱列表
        /// </summary>
        /// <param name="LOUCENG"></param>
        /// <param name="shipDate"></param>
        /// <returns></returns>
        public static List<PKDeliverBox> GetDeliverBoxListByLOUCENGAndSHIPDATE(string LOUCENG)
        {
            try
            {
                string sql = @"SELECT GUID, LOUCENG, PARTNER, SHIP_DATE, HU, RESULT, REMARK
                               FROM DeliverBox WHERE LOUCENG = @LOUCENG 
                               ORDER BY CreateTime ASC";
                SqlParameter p1 = DBHelper.CreateParameter("@LOUCENG", LOUCENG);
                DataTable table = DBHelper.GetTable(sql, false, p1);
                if (table != null && table.Rows.Count > 0)
                {
                    List<PKDeliverBox> list = new List<PKDeliverBox>();
                    foreach (DataRow row in table.Rows)
                    {
                        PKDeliverBox item = new PKDeliverBox();
                        item.GUID = row["GUID"] == null ? null : row["GUID"].ToString();
                        item.LOUCENG = row["LOUCENG"] == null ? null : row["LOUCENG"].ToString();
                        item.PARTNER = row["PARTNER"] == null ? null : row["PARTNER"].ToString();
                        item.SHIP_DATE = DateTime.Parse(row["SHIP_DATE"].ToString());
                        item.HU = row["HU"] == null ? null : row["HU"].ToString();
                        item.RESULT = row["RESULT"] == null ? null : row["RESULT"].ToString();
                        item.REMARK = row["REMARK"] == null ? null : row["REMARK"].ToString();
                        list.Add(item);
                    }

                    return list;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 保存发运箱信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool SaveDeliverBox(PKDeliverBox item)
        {
            try
            {
                string sql = @"INSERT INTO DeliverBox(GUID, LOUCENG, PARTNER, SHIP_DATE, HU, RESULT, REMARK, CreateTime)
                                VALUES (@GUID, @LOUCENG, @PARTNER, @SHIP_DATE, @HU, @RESULT, @REMARK, @CreateTime)";
                SqlParameter p1 = DBHelper.CreateParameter("@GUID", item.GUID);
                SqlParameter p2 = DBHelper.CreateParameter("@LOUCENG", item.LOUCENG);
                SqlParameter p3 = DBHelper.CreateParameter("@PARTNER", item.PARTNER);
                SqlParameter p4 = DBHelper.CreateParameter("@SHIP_DATE", item.SHIP_DATE);
                SqlParameter p5 = DBHelper.CreateParameter("@HU", item.HU);
                SqlParameter p6 = DBHelper.CreateParameter("@RESULT", item.RESULT);
                SqlParameter p7 = DBHelper.CreateParameter("@REMARK", item.REMARK);
                SqlParameter p8 = DBHelper.CreateParameter("@CreateTime", DateTime.Now);
                int result = DBHelper.ExecuteSql(sql, false, p1, p2, p3, p4, p5, p6, p7, p8);

                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 上传发运箱epc明细
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        public static bool SaveDeliverEpcDetail(UploadPKBoxInfo box)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat(@"
DELETE FROM dbo.DeliverEpcDetail WHERE HU = '{0}'
INSERT INTO DeliverEpcDetail(LGNUM, SHIP_DATE, 
PARTNER, HU, EPC_SER, Handled, Result, [Timestamp], 
LOUCENG, BOXGUID,MATNR,ZSATNR,ZCOLSN,ZSIZTX,IsAdd) 
VALUES ", box.HU);
            if (box.TagDetailList != null && box.TagDetailList.Count > 0)
            {
                foreach (TagDetailInfo item in box.TagDetailList)
                {
                    sbSql.AppendFormat("('{0}', '{1}', '{2}', '{3}', '{4}', 0, '{5}', GETDATE(), '{6}', '{7}','{8}','{9}','{10}','{11}',{12}),",
                        box.LGNUM, box.SHIP_DATE.ToString("yyyy-MM-dd"), box.PARTNER,
                        box.HU, item.EPC, box.InventoryResult ? "S" : "E", box.LOUCENG,
                        box.Guid, item.MATNR, item.ZSATNR, item.ZCOLSN, item.ZSIZTX, item.IsAddEpc ? 1 : 0);
                }

                if (sbSql.ToString().EndsWith(","))
                    sbSql.Remove(sbSql.Length - 1, 1);

                int result = DBHelper.ExecuteSql(sbSql.ToString(), false);
            }

            return true;
        }

        /// <summary>
        /// 获取未处理的发运箱 epc明细列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, List<string>> GetUnhandledDeliverEpcDetails()
        {
            string sql = "SELECT LGNUM, SHIP_DATE, PARTNER, HU, EPC_SER FROM DeliverEpcDetail WHERE (Handled IS NULL OR Handled != 1) AND Result = 'S'";
            DataTable table = DBHelper.GetTable(sql, false);
            if (table != null && table.Rows.Count > 0)
            {
                Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
                foreach (DataRow row in table.Rows)
                {
                    string shipdate = DateTime.Parse(row["SHIP_DATE"].ToString()).ToString("yyyyMMdd");
                    string key = string.Format("{0},{1},{2},{3}", row["LGNUM"], shipdate, row["PARTNER"], row["HU"]);
                    if (dic.ContainsKey(key))
                    {
                        List<string> list = dic[key];
                        list.Add(row["EPC_SER"].ToString());
                    }
                    else
                    {
                        List<string> list = new List<string>();
                        list.Add(row["EPC_SER"].ToString());
                        dic.Add(key, list);
                    }
                }

                return dic;
            }

            return null;
        }

        /// <summary>
        /// 获取未上传的已下架的epc明细列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, string>> GetUnhandledInventoryOutEpcDetails()
        {
            string sql = @"select a.Id,a.HU,b.LGNUM,b.[PARTNER],a.EPC,b.SHIP_DATE from ShippingBoxDetail a
inner join (
select LGNUM,PICK_TASK,PICK_TASK_ITEM,[PARTNER],IsOut,SHIP_DATE from InventoryOutLogDetail union 
select LGNUM,PICK_TASK,PICK_TASK_ITEM,[PARTNER],IsOut,SHIP_DATE from InventoryOutLogDetailHistory) b 
on a.PICK_TASK =b.PICK_TASK and a.PICK_TASK_ITEM=b.PICK_TASK_ITEM and  b.IsOut=1
where a.Handled<>1 and a.IsRFID = 1 ";
            //            string sql = @"select a.Id,a.HU,b.LGNUM,b.[PARTNER],a.EPC,b.SHIP_DATE from ShippingBoxDetail a
            //inner join (
            //select LGNUM,PICK_TASK,PICK_TASK_ITEM,[PARTNER],IsOut,SHIP_DATE from InventoryOutLogDetail union 
            //select LGNUM,PICK_TASK,PICK_TASK_ITEM,[PARTNER],IsOut,SHIP_DATE from InventoryOutLogDetailHistory) b 
            //on a.PICK_TASK =b.PICK_TASK and a.PICK_TASK_ITEM=b.PICK_TASK_ITEM and  b.IsOut=1 and b.SHIP_DATE='2016-1-26'
            //where  a.IsRFID = 1 ";
            DataTable table = DBHelper.GetTable(sql, false);
            //MessageBox.Show(JsonConvert.SerializeObject(table));
            if (table != null && table.Rows.Count > 0)
            {
                Dictionary<string, Dictionary<string, string>> dic = new Dictionary<string, Dictionary<string, string>>();
                foreach (DataRow row in table.Rows)
                {
                    string shipdate = DateTime.Parse(row["SHIP_DATE"].ToString()).ToString("yyyyMMdd");
                    string key = string.Format("{0},{1},{2},{3}",
                        row["LGNUM"], shipdate, row["PARTNER"], row["HU"]);
                    if (dic.ContainsKey(key))
                    {
                        dic[key].Add(row["Id"].ToString(), row["EPC"].ToString());
                    }
                    else
                    {
                        Dictionary<string, string> list = new Dictionary<string, string>();
                        list.Add(row["Id"].ToString(), row["EPC"].ToString());
                        dic.Add(key, list);
                    }
                }

                return dic;
            }

            return null;
        }

        /// <summary>
        /// 设置指定的下架EPC明细为已处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool SetInventoryOutEpcDetailsHandled(List<string> ids)
        {
            if (ids == null || ids.Count <= 0) return true;
            StringBuilder idstring = new StringBuilder();
            foreach (string item in ids)
            {
                idstring.AppendFormat("{0},", item);
            }
            idstring = idstring.Remove(idstring.Length - 1, 1);
            string sql = string.Format("update ShippingBoxDetail set Handled=1 where Id in ({0})", idstring);
            int result = DBHelper.ExecuteSql(sql, false);
            return result > 0 ? true : false;
        }

        /// <summary>
        /// 设置指定发运箱 epc明细为已处理
        /// </summary>
        /// <param name="lgnum"></param>
        /// <param name="docno"></param>
        /// <param name="doccat"></param>
        /// <param name="hu"></param>
        /// <returns></returns>
        public static bool SetDeliverEpcDetailsHandled(string lgnum, string dateship, string partner, string hu)
        {
            DateTime dsp = DateTime.ParseExact(dateship, "yyyyMMdd", CultureInfo.InvariantCulture);
            SqlParameter p1 = DBHelper.CreateParameter("@LGNUM", lgnum);
            SqlParameter p2 = DBHelper.CreateParameter("@SHIP_DATE", dsp);
            SqlParameter p3 = DBHelper.CreateParameter("@PARTNER", partner);
            SqlParameter p4 = DBHelper.CreateParameter("@HU", hu);
            string sql = "UPDATE DeliverEpcDetail SET Handled = 1,[Timestamp] = GETDATE() WHERE LGNUM = @LGNUM AND SHIP_DATE = @SHIP_DATE AND PARTNER = @PARTNER AND HU = @HU";
            int result = DBHelper.ExecuteSql(sql, false, p1, p2, p3, p4);

            return result > 0 ? true : false;
        }

        /// <summary>
        /// 获取结果为S的发运epc明细
        /// </summary>
        /// <param name="LGNUM"></param>
        /// <param name="SHIP_DATE"></param>
        /// <param name="LOUCENG"></param>
        /// <returns></returns>
        public static List<DeliverEpcDetail> GetDeliverEpcDetailList(string LGNUM, string LOUCENG)
        {
            try
            {
                string sql = @"SELECT LGNUM, SHIP_DATE, LOUCENG, PARTNER, HU, EPC_SER, Result, BOXGUID
                               FROM DeliverEpcDetail 
                               WHERE LGNUM = @LGNUM AND LOUCENG = @LOUCENG AND Result = 'S'";
                SqlParameter p1 = DBHelper.CreateParameter("@LGNUM", LGNUM);
                SqlParameter p3 = DBHelper.CreateParameter("@LOUCENG", LOUCENG);
                DataTable table = DBHelper.GetTable(sql, false, p1, p3);
                if (table != null && table.Rows.Count > 0)
                {
                    List<DeliverEpcDetail> list = new List<DeliverEpcDetail>();
                    foreach (DataRow row in table.Rows)
                    {
                        DeliverEpcDetail item = new DeliverEpcDetail();
                        item.LGNUM = row["LGNUM"] == null ? null : row["LGNUM"].ToString();
                        item.SHIP_DATE = DateTime.Parse(row["SHIP_DATE"].ToString());
                        item.LOUCENG = row["LOUCENG"] == null ? null : row["LOUCENG"].ToString();
                        item.PARTNER = row["PARTNER"] == null ? null : row["PARTNER"].ToString();
                        item.HU = row["HU"] == null ? null : row["HU"].ToString();
                        item.EPC_SER = row["EPC_SER"] == null ? null : row["EPC_SER"].ToString();
                        item.Result = row["Result"] == null ? null : row["Result"].ToString();
                        item.BOXGUID = row["BOXGUID"] == null ? null : row["BOXGUID"].ToString();
                        list.Add(item);
                    }

                    return list;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 根据箱码获取该箱短拣的明细
        /// </summary>
        /// <param name="LGNUM">仓库编号</param>
        /// <param name="HU">箱码</param>
        /// <returns></returns>
        public static List<PKDeliverBoxShortPickDetailInfo> GetShortPickDetailList(string LGNUM, string HU)
        {
            try
            {
                string sql = @"select LGNUM,HU,PICK_TASK,PICK_TASK_ITEM,MATNR,QTY,DJQTY  from DeliverBoxShortPickDetail 
where LGNUM=@LGNUM and HU = @HU";
                SqlParameter p1 = DBHelper.CreateParameter("@LGNUM", LGNUM);
                SqlParameter p2 = DBHelper.CreateParameter("@HU", HU);
                DataTable table = DBHelper.GetTable(sql, false, p1, p2);
                if (table != null && table.Rows.Count > 0)
                {
                    List<PKDeliverBoxShortPickDetailInfo> list = new List<PKDeliverBoxShortPickDetailInfo>();
                    foreach (DataRow row in table.Rows)
                    {
                        PKDeliverBoxShortPickDetailInfo item = new PKDeliverBoxShortPickDetailInfo();
                        item.LGNUM = row["LGNUM"] == null ? null : row["LGNUM"].ToString();
                        item.HU = row["HU"] == null ? null : row["HU"].ToString();
                        item.PICK_TASK = row["PICK_TASK"] == null ? null : row["PICK_TASK"].ToString();
                        item.PICK_TASK_ITEM = row["PICK_TASK_ITEM"] == null ? null : row["PICK_TASK_ITEM"].ToString();
                        item.MATNR = row["MATNR"] == null ? null : row["MATNR"].ToString();
                        item.QTY = row["QTY"] == null ? 0 : int.Parse(row["QTY"].ToString());
                        item.DJQTY = row["DJQTY"] == null ? 0 : int.Parse(row["DJQTY"].ToString());
                        list.Add(item);
                    }
                    return list;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }


        public static bool SaveShortPickDetail(List<PKDeliverBoxShortPickDetailInfo> param)
        {
            if (param == null || param.Count <= 0) return false;
            string sql = string.Format(@"
DELETE FROM dbo.DeliverBoxShortPickDetail WHERE HU='{0}'

INSERT INTO dbo.DeliverBoxShortPickDetail
        ( LGNUM, HU, PICK_TASK, PICK_TASK_ITEM, MATNR, QTY, Timestamps,DJQTY )
VALUES ", param[0].HU);

            string sonsql = "";
            foreach (PKDeliverBoxShortPickDetailInfo item in param)
            {
                sonsql += string.Format(@"( '{0}', '{1}', '{2}', '{3}', '{4}', {5}, GETDATE(),{6} ),", item.LGNUM, item.HU, item.PICK_TASK, item.PICK_TASK_ITEM, item.MATNR, item.QTY, item.DJQTY);
            }
            if (sonsql.EndsWith(","))
                sonsql = sonsql.Remove(sonsql.Length - 1, 1);

            string sonsql2 = string.Format(@" UPDATE dbo.BoxPickTaskMap SET IS_SHORT_PICK = 1 WHERE HU='{0}'", param[0].HU);

            return DBHelper.ExecuteNonQuery(sql + sonsql + sonsql2) >= 0;
        }
        #endregion


        /// <summary>
        /// 插入异常记录-16#分拣复核
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool InsertEbBoxErrorRecord(EbBoxErrorRecordInfo param, CheckType type)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO EbBoxErrorRecord{7}(HU,PRODUCTNO,ZSATNR,ZCOLSN,ZSIZTX,DIFF,REMARK) VALUES('{0}','{1}','{2}','{3}','{4}',{5},'{6}')"
                , param.HU, param.PRODUCTNO, param.ZSATNR,
                param.ZCOLSN, param.ZSIZTX, param.DIFF,
                param.REMARK, type == CheckType.电商收货复核 ? "_Receive" : "");
            return DBHelper.ExecuteNonQuery(sb.ToString()) > 0;
        }

        /// <summary>
        /// 根据hu获取所有异常记录-16#分拣复核
        /// </summary>
        /// <param name="huList"></param>
        /// <returns></returns>
        public static List<EbBoxErrorRecordInfo> GetEbBoxErrorRecordList(List<string> huList, CheckType type)
        {
            if (huList == null && huList.Count <= 0)
                return null;
            StringBuilder huString = new StringBuilder();
            foreach (string hu in huList)
            {
                huString.AppendFormat("'{0}',", hu);
            }
            if (huString.ToString().EndsWith(","))
            {
                huString = huString.Remove(huString.Length - 1, 1);
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT Id,HU,PRODUCTNO,ZSATNR,ZCOLSN,ZSIZTX,DIFF,REMARK FROM EbBoxErrorRecord{1} WHERE HU IN({0})",
                huString, type == CheckType.电商收货复核 ? "_Receive" : "");
            DataTable dt = DBHelper.GetTable(sb.ToString(), false);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            List<EbBoxErrorRecordInfo> result = new List<EbBoxErrorRecordInfo>();
            foreach (DataRow row in dt.Rows)
            {
                EbBoxErrorRecordInfo item = new EbBoxErrorRecordInfo();
                item.Id = long.Parse(row["Id"].ToString());
                item.HU = row["HU"].ToString();
                item.PRODUCTNO = row["PRODUCTNO"].ToString();
                item.ZSATNR = row["ZSATNR"].ToString();
                item.ZCOLSN = row["ZCOLSN"].ToString();
                item.ZSIZTX = row["ZSIZTX"].ToString();
                item.REMARK = row["REMARK"].ToString();
                item.DIFF = int.Parse(row["DIFF"].ToString());
                result.Add(item);
            }
            return result;
        }
        public static void insertEBBoxDetail(string hu,List<TagDetailInfo>epclist)
        {
            try
            {
                
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("INSERT INTO EbBoxDetail(HU, EPC, TIMESTAMP,MAT) VALUES ");
                if (epclist != null && epclist.Count > 0)
                {
                    foreach (TagDetailInfo epc in epclist)
                    {
                        sbSql.AppendFormat("('{0}', '{1}', GETDATE(),'{2}'),", hu, epc.EPC, epc.MATNR);
                    }

                    if (sbSql.ToString().EndsWith(","))
                        sbSql.Remove(sbSql.Length - 1, 1);

                    int result = DBHelper.ExecuteSql(sbSql.ToString(), false);
                }
            }
            catch(Exception)
            {

            }
        }

        /// <summary>
        /// 插入检货记录-16#分拣复核
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool InsertEbCheckRecord(EbBoxCheckRecordInfo param, CheckType type)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO EbBoxCheckRecord{4}(HU,PQTY,RQTY,STATUS) VALUES('{0}',{1},{2},{3}) ",
                param.HU, param.PQTY, param.RQTY, param.STATUS,
                type == CheckType.电商收货复核 ? "_Receive" : "");
            return DBHelper.ExecuteNonQuery(sb.ToString()) > 0;
        }

        /// <summary>
        /// 根据箱码获取该箱最近一次检货记录
        /// </summary>
        /// <param name="hu"></param>
        /// <returns></returns>
        public static EbBoxCheckRecordInfo GetLastEbCheckRecord(string hu, CheckType type)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT TOP 1 Id,HU,PQTY,RQTY,STATUS FROM EbBoxCheckRecord{1} WHERE HU = '{0}' ORDER BY Id DESC",
                hu, type == CheckType.电商收货复核 ? "_Receive" : "");
            DataTable dt = DBHelper.GetTable(sb.ToString(), false);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            DataRow row = dt.Rows[0];
            EbBoxCheckRecordInfo item = new EbBoxCheckRecordInfo();
            item.Id = long.Parse(row["Id"].ToString());
            item.HU = row["HU"].ToString();
            item.PQTY = int.Parse(row["PQTY"].ToString());
            item.RQTY = int.Parse(row["RQTY"].ToString());
            item.STATUS = int.Parse(row["STATUS"].ToString());
            return item;
        }

        /// <summary>
        /// 根据hu获取所有检货记录-16#分拣复核
        /// </summary>
        /// <param name="huList"></param>
        /// <returns></returns>
        public static List<EbBoxCheckRecordInfo> GetEbCheckRecordList(List<string> huList, CheckType type)
        {
            if (huList == null && huList.Count <= 0)
                return null;
            StringBuilder huString = new StringBuilder();
            foreach (string hu in huList)
            {
                huString.AppendFormat("'{0}',", hu);
            }
            if (huString.ToString().EndsWith(","))
            {
                huString = huString.Remove(huString.Length - 1, 1);
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT Id,HU,PQTY,RQTY,STATUS FROM EbBoxCheckRecord{1} WHERE HU IN({0})",
                huString, type == CheckType.电商收货复核 ? "_Receive" : "");
            DataTable dt = DBHelper.GetTable(sb.ToString(), false);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            List<EbBoxCheckRecordInfo> result = new List<EbBoxCheckRecordInfo>();
            foreach (DataRow row in dt.Rows)
            {
                EbBoxCheckRecordInfo item = new EbBoxCheckRecordInfo();
                item.Id = long.Parse(row["Id"].ToString());
                item.HU = row["HU"].ToString();
                item.PQTY = int.Parse(row["PQTY"].ToString());
                item.RQTY = int.Parse(row["RQTY"].ToString());
                item.STATUS = int.Parse(row["STATUS"].ToString());
                result.Add(item);
            }
            return result;
        }


        /// <summary>
        /// 电商大通道机-获取复核箱数据
        /// </summary>
        /// <param name="shipDate">发运日期</param>
        /// <param name="hu">箱码 为空时按发运日期获取 否则按箱码获取</param>
        /// <returns></returns>
        public static List<EbBoxInfo> GetEbBoxList(DateTime shipDate, string hu, CheckType type)
        {
            string sql;
            if (string.IsNullOrEmpty(hu.Trim()))
                sql = string.Format("SELECT Id,HU,PRODUCTNO,QTY,SHIPDATE FROM EbBox{1} WHERE SHIPDATE='{0}'",
                    shipDate.ToString("yyyy-MM-dd"), type == CheckType.电商收货复核 ? "_Receive" : "");
            else
                sql = string.Format("SELECT Id,HU,PRODUCTNO,QTY,SHIPDATE FROM EbBox{1} WHERE HU='{0}'",
                    hu, type == CheckType.电商收货复核 ? "_Receive" : "");
            DataTable dt = DBHelper.GetTable(sql, false);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<EbBoxInfo> list = new List<EbBoxInfo>();
                foreach (DataRow row in dt.Rows)
                {
                    EbBoxInfo item = new EbBoxInfo();
                    item.Id = long.Parse(row["Id"].ToString());
                    item.HU = row["HU"].ToString();
                    item.PRODUCTNO = row["PRODUCTNO"].ToString();
                    item.QTY = int.Parse(row["QTY"].ToString());
                    item.SHIPDATE = row["SHIPDATE"].ToString().Trim() != "" ? DateTime.Parse(row["SHIPDATE"].ToString()) : new DateTime(1900, 1, 1);
                    list.Add(item);
                }
                return list;
            }
            return null;
        }

        /// <summary>
        /// 保存电商复核箱数据
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        public static bool SaveEbBox(EbBoxInfo box, CheckType type)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO EbBox{4}(HU,PRODUCTNO,QTY,SHIPDATE) VALUES('{0}','{1}',{2},'{3}');",
                box.HU, box.PRODUCTNO, box.QTY,
                box.SHIPDATE.ToString("yyyy-MM-dd"),
                type == CheckType.电商收货复核 ? "_Receive" : "");
            int result = DBHelper.ExecuteNonQuery(sb.ToString());
            return result > 0;
        }

        /// <summary>
        /// 插入要上传的数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool InsertUploadData(UploadOutLogDataInfo param)
        {
            string sql = string.Format("INSERT INTO UploadData(Guid,Data,CreateTime,DeviceNo) VALUES('{0}','{1}','{2}','{3}')", param.Guid, JsonConvert.SerializeObject(param.UploadData), param.CreatTime.ToString("yyyy-MM-dd HH:mm:ss"), param.DeviceNo);
            int result = DBHelper.ExecuteSql(sql, false);
            if (result <= 0)
            {
                LogHelper.WriteLine(JsonConvert.SerializeObject(param.UploadData));
            }
            return result > 0;
        }

        /// <summary>
        /// 根据设备编号获取未上传的数据
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <returns></returns>
        public static List<UploadOutLogDataInfo> GetUnUploadDataList(string deviceNo)
        {
            string sql = string.Format("SELECT Guid,Data,CreateTime,DeviceNo,ErrorMsg FROM UploadData WHERE DeviceNo = '{0}'", deviceNo);
            DataTable dt = DBHelper.GetTable(sql, false);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<UploadOutLogDataInfo> result = new List<UploadOutLogDataInfo>();
                foreach (DataRow row in dt.Rows)
                {
                    UploadOutLogDataInfo ud = new UploadOutLogDataInfo();
                    ud.Guid = row["Guid"].ToString();
                    ud.DeviceNo = row["DeviceNo"].ToString();
                    ud.UploadData = JsonConvert.DeserializeObject<OutLogDataInfo>(row["Data"].ToString());
                    ud.CreatTime = DateTime.Parse(row["CreateTime"].ToString());
                    ud.ErrorMsg = row["ErrorMsg"].ToString();
                    result.Add(ud);
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// 设置数据已上传
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool DeleteUploaded(string guid)
        {
            string sql = string.Format("DELETE FROM UploadData WHERE Guid='{0}'", guid);
            int result = DBHelper.ExecuteSql(sql, false);
            return result >= 0;
        }

        public static bool UpdateErrorOfUploadData(string guid, string errorMsg)
        {
            string sql = string.Format("UPDATE UploadData SET ErrorMsg='{0}',CreateTime=GETDATE() WHERE Guid='{1}'", errorMsg, guid);
            int result = DBHelper.ExecuteSql(sql, false);
            return result >= 0;
        }


        /// <summary>
        /// 根据设备编号获取异常箱码前缀
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <returns></returns>
        public static bool IsErrorHuConfigExists(string deviceNo)
        {
            try
            {
                if (SysConfig.IsTest)
                    return true;
                string sql = @"SELECT COUNT(*) FROM ErrorHuConfig WHERE DeviceNO = @DeviceNO";
                SqlParameter p1 = DBHelper.CreateParameter("@DeviceNO", deviceNo);
                int num = int.Parse(DBHelper.GetValue(sql, false, p1).ToString());
                if (num > 0)
                {
                    return true;
                }
                else
                {
                    //查找最大的前缀，并加1，插入
                    sql = "SELECT TOP 1 HuPrefix FROM ErrorHuConfig ORDER BY HuPrefix DESC";
                    object result = DBHelper.GetValue(sql, false);
                    string huPrefix = "";
                    if (result == null || string.IsNullOrEmpty(result.ToString()))
                    {
                        huPrefix = "X1";
                    }
                    else
                    {
                        huPrefix = result.ToString();
                        string secondword = huPrefix.Substring(1, 1);
                        string newSecondWord = "";
                        if (secondword == "1" || secondword == "2" || secondword == "3" || secondword == "4" || secondword == "5"
                            || secondword == "6" || secondword == "7" || secondword == "8" || secondword == "9" || secondword == "A"
                            || secondword == "B" || secondword == "C" || secondword == "D" || secondword == "E")
                        {
                            newSecondWord = string.Format("{0:X}", Convert.ToInt32(secondword, 16) + 1);
                        }
                        else if (secondword == "F" || secondword == "G" || secondword == "H" || secondword == "I" ||
                            secondword == "J" || secondword == "K" || secondword == "L" || secondword == "M" ||
                            secondword == "N" || secondword == "O" || secondword == "P" || secondword == "Q" ||
                            secondword == "R" || secondword == "S" || secondword == "T" || secondword == "U" ||
                            secondword == "V" || secondword == "W" || secondword == "X" || secondword == "Y")
                        {
                            newSecondWord = Encoding.ASCII.GetString(new byte[] { (byte)(Encoding.ASCII.GetBytes(secondword)[0] + 1) });
                        }
                        else
                        {
                            return false;
                        }

                        huPrefix = huPrefix.Substring(0, 1) + newSecondWord;
                    }

                    sql = "INSERT INTO ErrorHuConfig(DeviceNO, HuPrefix, Hu) VALUES (@DeviceNO, @HuPrefix, 1)";
                    SqlParameter p2 = DBHelper.CreateParameter("@DeviceNO", deviceNo);
                    SqlParameter p3 = DBHelper.CreateParameter("@HuPrefix", huPrefix);
                    DBHelper.ExecuteSql(sql, false, p2, p3);

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据设备编号获取异常箱码
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <returns></returns>
        public static string GetNewErrorHu(string deviceNo)
        {
            try
            {
                string sql = @"SELECT HuPrefix, Hu FROM ErrorHuConfig WHERE DeviceNO = @DeviceNO;
                                UPDATE ErrorHuConfig SET Hu = Hu + 1 WHERE DeviceNO = @DeviceNO;";
                SqlParameter p1 = DBHelper.CreateParameter("@DeviceNO", deviceNo);
                DataTable table = DBHelper.GetTable(sql, false, p1);
                if (table != null && table.Rows.Count > 0)
                {
                    string huPrefix = table.Rows[0]["HuPrefix"].ToString();
                    long hu = long.Parse(table.Rows[0]["Hu"].ToString());

                    if (hu >= 9999998) //当数量到达9999998时，将数量重置为1
                    {
                        sql = "UPDATE ErrorHuConfig SET Hu = 1 WHERE DeviceNO = @DeviceNO;";
                        SqlParameter p2 = DBHelper.CreateParameter("@DeviceNO", deviceNo);
                        DBHelper.ExecuteSql(sql, false, p2);
                    }

                    return huPrefix + hu.ToString().PadLeft(7, '0');
                }

                return "";
            }
            catch
            {
                return "";
            }
        }
        public static string nullStr(object obj)
        {
            try
            {
                if (obj == null)
                    return "";
                else
                    return obj.ToString();
            }
            catch (Exception)
            {

            }
            return "";
        }

        public static bool SaveShippingLabelNew(ShippingLabel info)
        {
            try
            {
                if(string.IsNullOrEmpty(info.DOCNO))
                {
                    return false;
                }
                else
                {
                    string sql = string.Format(@"select DOCNO from ShippingLabel where DOCNO='{0}' and FYDT='{1}'", info.DOCNO, info.FYDT);
                    string docno = nullStr(DBHelper.GetValue(sql, false));
                    if(string.IsNullOrEmpty(docno))
                    {
                        //insert
                        sql = string.Format(@"insert into ShippingLabel (SHIP_DATE,STORE_ID,STORE_NAME,DISPATCH_AREA,
					    COLLECT_WAVE,VSART_DES,ROUTE_NO,ADDRESS,TEL_NUMBER,LANE_ID,ROUTE_DES,FYDT,VSART,ZYT,FY_WAVE,
					    WAVE_AND_YT,IS_FBC,COLLECT_SEQ,ZSDABW,ZSDABW_DES,DOCNO,PICK_DATE) values (@SHIP_DATE,@STORE_ID,@STORE_NAME,
                        @DISPATCH_AREA,
					    @COLLECT_WAVE,@VSART_DES,@ROUTE_NO,@ADDRESS,@TEL_NUMBER,@LANE_ID,@ROUTE_DES,@FYDT,@VSART,@ZYT,
                        @FY_WAVE,
					    @WAVE_AND_YT,@IS_FBC,@COLLECT_SEQ,@ZSDABW,@ZSDABW_DES,@DOCNO,@PICK_DATE)");
                    }
                    else
                    {
                        //update
                        sql = string.Format(@"update ShippingLabel set STORE_NAME = @STORE_NAME,
			            DISPATCH_AREA=@DISPATCH_AREA,
			            COLLECT_WAVE =@COLLECT_WAVE,
			            VSART_DES =@VSART_DES,
			            ROUTE_NO =@ROUTE_NO,
			            [ADDRESS] =@ADDRESS,
			            TEL_NUMBER =@TEL_NUMBER,
			            LANE_ID =@LANE_ID,
			            ROUTE_DES = @ROUTE_DES,
			            ZYT = @ZYT,
			            FY_WAVE = @FY_WAVE,
			            WAVE_AND_YT = @WAVE_AND_YT,
			            IS_FBC = @IS_FBC,
			            COLLECT_SEQ = @COLLECT_SEQ,
			            ZSDABW = @ZSDABW,
			            ZSDABW_DES = @ZSDABW_DES,
                        DOCNO = @DOCNO,
                        PICK_DATE = @PICK_DATE WHERE DOCNO='{0}' and FYDT='{1}'", info.DOCNO, info.FYDT);

                    }

                    SqlParameter p1 = DBHelper.CreateParameter("@SHIP_DATE", info.SHIP_DATE);
                    SqlParameter p2 = DBHelper.CreateParameter("@STORE_ID", info.STORE_ID);
                    SqlParameter p3 = DBHelper.CreateParameter("@STORE_NAME", info.STORE_NAME);
                    SqlParameter p4 = DBHelper.CreateParameter("@DISPATCH_AREA", info.DISPATCH_AREA);
                    SqlParameter p5 = DBHelper.CreateParameter("@COLLECT_WAVE", info.COLLECT_WAVE);
                    SqlParameter p6 = DBHelper.CreateParameter("@VSART_DES", info.VSART_DES);
                    SqlParameter p7 = DBHelper.CreateParameter("@ROUTE_NO", info.ROUTE_NO);
                    SqlParameter p8 = DBHelper.CreateParameter("@ADDRESS", info.ADDRESS);
                    SqlParameter p9 = DBHelper.CreateParameter("@TEL_NUMBER", info.TEL_NUMBER);
                    SqlParameter p10 = DBHelper.CreateParameter("@LANE_ID", info.LANE_ID);
                    SqlParameter p11 = DBHelper.CreateParameter("@ROUTE_DES", info.ROUTE_DES);

                    SqlParameter p12 = DBHelper.CreateParameter("@FYDT", info.FYDT);
                    SqlParameter p13 = DBHelper.CreateParameter("@VSART", info.VSART);
                    SqlParameter p14 = DBHelper.CreateParameter("@ZYT", info.ZYT);
                    SqlParameter p15 = DBHelper.CreateParameter("@FY_WAVE", info.FY_WAVE);
                    SqlParameter p16 = DBHelper.CreateParameter("@WAVE_AND_YT", info.WAVE_AND_YT);
                    SqlParameter p17 = DBHelper.CreateParameter("@IS_FBC", info.IS_FBC);

                    SqlParameter p18 = DBHelper.CreateParameter("@COLLECT_SEQ", info.COLLECT_SEQ);
                    SqlParameter p19 = DBHelper.CreateParameter("@ZSDABW", info.ZSDABW);
                    SqlParameter p20 = DBHelper.CreateParameter("@ZSDABW_DES", info.ZSDABW_DES);
                    SqlParameter p21 = DBHelper.CreateParameter("@DOCNO", info.DOCNO);

                    SqlParameter p22 = DBHelper.CreateParameter("@PICK_DATE", info.PICK_DATE);

                    int re = DBHelper.ExecuteSql(sql, false, p1, p2, p3, p4, p5, p6
                        , p7, p8, p9, p10, p11
                        , p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22);

                    return re > 0;
                }


            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return false;
            }

        }
        /// <summary>
        /// 保存发运标签信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool SaveShippingLabel(ShippingLabel info)
        {
            try
            {
                /*
                string sql = string.Format("P_ShippingLabel_Insert ");
                SqlParameter p1 = DBHelper.CreateParameter("@SHIP_DATE", info.SHIP_DATE);
                SqlParameter p2 = DBHelper.CreateParameter("@STORE_ID", info.STORE_ID);
                SqlParameter p3 = DBHelper.CreateParameter("@STORE_NAME", info.STORE_NAME);
                SqlParameter p4 = DBHelper.CreateParameter("@DISPATCH_AREA", info.DISPATCH_AREA);
                SqlParameter p5 = DBHelper.CreateParameter("@COLLECT_WAVE", info.COLLECT_WAVE);
                SqlParameter p6 = DBHelper.CreateParameter("@VSART_DES", info.VSART_DES);
                SqlParameter p7 = DBHelper.CreateParameter("@ROUTE_NO", info.ROUTE_NO);
                SqlParameter p8 = DBHelper.CreateParameter("@ADDRESS", info.ADDRESS);
                SqlParameter p9 = DBHelper.CreateParameter("@TEL_NUMBER", info.TEL_NUMBER);
                SqlParameter p10 = DBHelper.CreateParameter("@LANE_ID", info.LANE_ID);
                SqlParameter p11 = DBHelper.CreateParameter("@ROUTE_DES", info.ROUTE_DES);

                SqlParameter p12 = DBHelper.CreateParameter("@FYDT", info.FYDT);
                SqlParameter p13 = DBHelper.CreateParameter("@VSART", info.VSART);
                SqlParameter p14 = DBHelper.CreateParameter("@ZYT", info.ZYT);
                SqlParameter p15 = DBHelper.CreateParameter("@FY_WAVE", info.FY_WAVE);
                SqlParameter p16 = DBHelper.CreateParameter("@WAVE_AND_YT", info.WAVE_AND_YT);
                SqlParameter p17 = DBHelper.CreateParameter("@IS_FBC", info.IS_FBC);

                SqlParameter p18 = DBHelper.CreateParameter("@COLLECT_SEQ", info.COLLECT_SEQ);
                SqlParameter p19 = DBHelper.CreateParameter("@ZSDABW", info.ZSDABW);
                SqlParameter p20 = DBHelper.CreateParameter("@ZSDABW_DES", info.ZSDABW_DES);
                SqlParameter p21 = DBHelper.CreateParameter("@DOCNO", info.DOCNO);

                int result = int.Parse(DBHelper.GetValue(sql, true
                    , p1, p2, p3, p4, p5, p6
                    , p7, p8, p9, p10, p11
                    , p12, p13, p14, p15, p16, p17, p18, p19, p20,p21).ToString());

                return true;
                */

                string sql = string.Format(@"select STORE_ID from ShippingLabel where SHIP_DATE=@SHIP_DATE AND STORE_ID=@STORE_ID AND FYDT=@FYDT AND VSART=@VSART");
                SqlParameter s1 = DBHelper.CreateParameter("@SHIP_DATE", info.SHIP_DATE);
                SqlParameter s2 = DBHelper.CreateParameter("@STORE_ID", info.STORE_ID);
                SqlParameter s3 = DBHelper.CreateParameter("@FYDT", info.FYDT);
                SqlParameter s4 = DBHelper.CreateParameter("@VSART", info.VSART);

                string docno = nullStr(DBHelper.GetValue(sql, false, s1, s2, s3, s4));
                if (string.IsNullOrEmpty(docno))
                {
                    //insert
                    sql = string.Format(@"insert into ShippingLabel (SHIP_DATE,STORE_ID,STORE_NAME,DISPATCH_AREA,
					    COLLECT_WAVE,VSART_DES,ROUTE_NO,ADDRESS,TEL_NUMBER,LANE_ID,ROUTE_DES,FYDT,VSART,ZYT,FY_WAVE,
					    WAVE_AND_YT,IS_FBC,COLLECT_SEQ,ZSDABW,ZSDABW_DES,DOCNO) values (@SHIP_DATE,@STORE_ID,@STORE_NAME,
                        @DISPATCH_AREA,
					    @COLLECT_WAVE,@VSART_DES,@ROUTE_NO,@ADDRESS,@TEL_NUMBER,@LANE_ID,@ROUTE_DES,@FYDT,@VSART,@ZYT,
                        @FY_WAVE,
					    @WAVE_AND_YT,@IS_FBC,@COLLECT_SEQ,@ZSDABW,@ZSDABW_DES,@DOCNO)");
                }
                else
                {
                    //update
                    sql = string.Format(@"update ShippingLabel set STORE_NAME = @STORE_NAME,
			            DISPATCH_AREA=@DISPATCH_AREA,
			            COLLECT_WAVE =@COLLECT_WAVE,
			            VSART_DES =@VSART_DES,
			            ROUTE_NO =@ROUTE_NO,
			            [ADDRESS] =@ADDRESS,
			            TEL_NUMBER =@TEL_NUMBER,
			            LANE_ID =@LANE_ID,
			            ROUTE_DES = @ROUTE_DES,
			            ZYT = @ZYT,
			            FY_WAVE = @FY_WAVE,
			            WAVE_AND_YT = @WAVE_AND_YT,
			            IS_FBC = @IS_FBC,
			            COLLECT_SEQ = @COLLECT_SEQ,
			            ZSDABW = @ZSDABW,
			            ZSDABW_DES = @ZSDABW_DES,
                        DOCNO = @DOCNO WHERE SHIP_DATE=@SHIP_DATE AND STORE_ID=@STORE_ID AND FYDT=@FYDT AND VSART=@VSART");

                }

                SqlParameter p1 = DBHelper.CreateParameter("@SHIP_DATE", info.SHIP_DATE);
                SqlParameter p2 = DBHelper.CreateParameter("@STORE_ID", info.STORE_ID);
                SqlParameter p3 = DBHelper.CreateParameter("@STORE_NAME", info.STORE_NAME);
                SqlParameter p4 = DBHelper.CreateParameter("@DISPATCH_AREA", info.DISPATCH_AREA);
                SqlParameter p5 = DBHelper.CreateParameter("@COLLECT_WAVE", info.COLLECT_WAVE);
                SqlParameter p6 = DBHelper.CreateParameter("@VSART_DES", info.VSART_DES);
                SqlParameter p7 = DBHelper.CreateParameter("@ROUTE_NO", info.ROUTE_NO);
                SqlParameter p8 = DBHelper.CreateParameter("@ADDRESS", info.ADDRESS);
                SqlParameter p9 = DBHelper.CreateParameter("@TEL_NUMBER", info.TEL_NUMBER);
                SqlParameter p10 = DBHelper.CreateParameter("@LANE_ID", info.LANE_ID);
                SqlParameter p11 = DBHelper.CreateParameter("@ROUTE_DES", info.ROUTE_DES);

                SqlParameter p12 = DBHelper.CreateParameter("@FYDT", info.FYDT);
                SqlParameter p13 = DBHelper.CreateParameter("@VSART", info.VSART);
                SqlParameter p14 = DBHelper.CreateParameter("@ZYT", info.ZYT);
                SqlParameter p15 = DBHelper.CreateParameter("@FY_WAVE", info.FY_WAVE);
                SqlParameter p16 = DBHelper.CreateParameter("@WAVE_AND_YT", info.WAVE_AND_YT);
                SqlParameter p17 = DBHelper.CreateParameter("@IS_FBC", info.IS_FBC);

                SqlParameter p18 = DBHelper.CreateParameter("@COLLECT_SEQ", info.COLLECT_SEQ);
                SqlParameter p19 = DBHelper.CreateParameter("@ZSDABW", info.ZSDABW);
                SqlParameter p20 = DBHelper.CreateParameter("@ZSDABW_DES", info.ZSDABW_DES);
                SqlParameter p21 = DBHelper.CreateParameter("@DOCNO", info.DOCNO);

                int re = DBHelper.ExecuteSql(sql, false, p1, p2, p3, p4, p5, p6
                    , p7, p8, p9, p10, p11
                    , p12, p13, p14, p15, p16, p17, p18, p19, p20, p21);

                return re > 0;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return false;
            }
        }

        public static bool SaveShippingLabelTest(ShippingLabel info)
        {
            try
            {
                string sql = string.Format("P_TEST");
                SqlParameter p1 = DBHelper.CreateParameter("@SHIP_DATE", info.SHIP_DATE);
                SqlParameter p2 = DBHelper.CreateParameter("@STORE_ID", info.STORE_ID);
                SqlParameter p3 = DBHelper.CreateParameter("@STORE_NAME", info.STORE_NAME);
                SqlParameter p4 = DBHelper.CreateParameter("@DISPATCH_AREA", info.DISPATCH_AREA);
                SqlParameter p5 = DBHelper.CreateParameter("@COLLECT_WAVE", info.COLLECT_WAVE);
                SqlParameter p6 = DBHelper.CreateParameter("@VSART_DES", info.VSART_DES);
                SqlParameter p7 = DBHelper.CreateParameter("@ROUTE_NO", info.ROUTE_NO);
                SqlParameter p8 = DBHelper.CreateParameter("@ADDRESS", info.ADDRESS);
                SqlParameter p9 = DBHelper.CreateParameter("@TEL_NUMBER", info.TEL_NUMBER);
                SqlParameter p10 = DBHelper.CreateParameter("@LANE_ID", info.LANE_ID);
                SqlParameter p11 = DBHelper.CreateParameter("@ROUTE_DES", info.ROUTE_DES);

                SqlParameter p12 = DBHelper.CreateParameter("@FYDT", info.FYDT);
                SqlParameter p13 = DBHelper.CreateParameter("@VSART", info.VSART);
                SqlParameter p14 = DBHelper.CreateParameter("@ZYT", info.ZYT);
                SqlParameter p15 = DBHelper.CreateParameter("@FY_WAVE", info.FY_WAVE);
                SqlParameter p16 = DBHelper.CreateParameter("@WAVE_AND_YT", info.WAVE_AND_YT);
                SqlParameter p17 = DBHelper.CreateParameter("@IS_FBC", info.IS_FBC);

                SqlParameter p18 = DBHelper.CreateParameter("@COLLECT_SEQ", info.COLLECT_SEQ);
                SqlParameter p19 = DBHelper.CreateParameter("@ZSDABW", info.ZSDABW);
                SqlParameter p20 = DBHelper.CreateParameter("@ZSDABW_DES", info.ZSDABW_DES);

                int result = int.Parse(DBHelper.GetValue(sql, true
                    , p1, p2, p3, p4, p5, p6
                    , p7, p8, p9, p10, p11
                    , p12, p13, p14, p15, p16, p17, p18, p19, p20).ToString());

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return false;
            }
        }

        public static List<string> GetParterByHu(string hu)
        {
            List<string> re = new List<string>();
            try
            {
                string sql = @"SELECT PARTNER FROM BoxPickTaskMap WHERE HU = @HU";
                SqlParameter p1 = DBHelper.CreateParameter("@HU", hu);

                DataTable table = DBHelper.GetTable(sql, false, p1);
                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        re.Add((string)row["PARTNER"]);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            return re;
        }

        /// <summary>
        /// 根据发运日期和门店代码获取发运标签信息
        /// </summary>
        /// <param name="shipDate"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public static ShippingLabel GetShippingLabelByShipDateAndStoreId(DateTime shipDate, string storeId, string FYDT, string VSART)
        {
            //try
            //{
            //    if (SysConfig.IsTest)
            //    {
            //        ShippingLabel label = new ShippingLabel();
            //        label.ADDRESS = "福建省厦门市思明区";
            //        label.COLLECT_WAVE = "1001";
            //        label.DISPATCH_AREA = "AREA11";
            //        label.LANE_ID = "29";
            //        label.ROUTE_NO = "1922";
            //        label.ROUTE_DES = "1922 Des";
            //        label.SHIP_DATE = DateTime.Now;
            //        label.STORE_ID = "H001";
            //        label.STORE_NAME = "百衣百顺";
            //        label.TEL_NUMBER = "13616003493";
            //        label.VSART_DES = "集装箱";
            //        return label;
            //    }
            //    string sql = @"SELECT SHIP_DATE, STORE_ID, STORE_NAME, DISPATCH_AREA, COLLECT_WAVE,
            //    					            VSART_DES, ROUTE_NO, ROUTE_DES, [ADDRESS], TEL_NUMBER, LANE_ID,
            //                                    FYDT, VSART,ZYT,FY_WAVE,WAVE_AND_YT,IS_FBC,COLLECT_SEQ,ZSDABW,ZSDABW_DES,PICK_DATE 
            //                                    FROM ShippingLabel WHERE SHIP_DATE = @SHIP_DATE AND STORE_ID = @STORE_ID AND FYDT = @FYDT AND VSART = @VSART";
            //    SqlParameter p1 = DBHelper.CreateParameter("@SHIP_DATE", shipDate);
            //    SqlParameter p2 = DBHelper.CreateParameter("@STORE_ID", storeId);
            //    SqlParameter p3 = DBHelper.CreateParameter("@FYDT", FYDT);
            //    SqlParameter p4 = DBHelper.CreateParameter("@VSART", VSART);

            //    DataTable table = DBHelper.GetTable(sql, false, p1, p2, p3, p4);

            //    if (table != null && table.Rows.Count > 0)
            //    {
            //        DataRow row = table.Rows[0];
            //        ShippingLabel item = new ShippingLabel();
            //        item.SHIP_DATE = DateTime.Parse(row["SHIP_DATE"].ToString());
            //        item.STORE_ID = (string)row["STORE_ID"];
            //        item.STORE_NAME = (string)row["STORE_NAME"];
            //        item.DISPATCH_AREA = (string)row["DISPATCH_AREA"];
            //        item.COLLECT_WAVE = (string)row["COLLECT_WAVE"];
            //        item.VSART_DES = (string)row["VSART_DES"];
            //        item.ROUTE_NO = (string)row["ROUTE_NO"];
            //        item.ROUTE_DES = row["ROUTE_DES"] == null ? null : row["ROUTE_DES"].ToString();
            //        item.ADDRESS = (string)row["ADDRESS"];
            //        item.TEL_NUMBER = (string)row["TEL_NUMBER"];
            //        item.LANE_ID = (string)row["LANE_ID"];

            //        item.FYDT = (string)row["FYDT"];
            //        item.VSART = (string)row["VSART"];
            //        item.ZYT = (string)row["ZYT"];
            //        item.FY_WAVE = (string)row["FY_WAVE"];
            //        item.WAVE_AND_YT = (string)row["WAVE_AND_YT"];
            //        item.IS_FBC = row["IS_FBC"] == null ? "" : row["IS_FBC"].ToString();

            //        item.COLLECT_SEQ = row["COLLECT_SEQ"] == null ? "" : row["COLLECT_SEQ"].ToString();
            //        item.ZSDABW = row["ZSDABW"] == null ? "" : row["ZSDABW"].ToString();
            //        item.ZSDABW_DES = row["ZSDABW_DES"] == null ? "" : row["ZSDABW_DES"].ToString();

            //        DateTime pd = Convert.ToDateTime("2000-1-1");
            //        DateTime.TryParse(row["PICK_DATE"].ToString(), out pd);
            //        item.PICK_DATE = pd;

            //        return item;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            //}

            return null;
        }
        public static ShippingLabel GetShippingLabelByDOCNO(string docno,string fydt)
        {
            try
            {
                if (SysConfig.IsTest)
                {
                    ShippingLabel label = new ShippingLabel();
                    label.ADDRESS = "福建省厦门市思明区";
                    label.COLLECT_WAVE = "1001";
                    label.DISPATCH_AREA = "AREA11";
                    label.LANE_ID = "29";
                    label.ROUTE_NO = "1922";
                    label.ROUTE_DES = "1922 Des";
                    label.SHIP_DATE = DateTime.Now;
                    label.STORE_ID = "H001";
                    label.STORE_NAME = "百衣百顺";
                    label.TEL_NUMBER = "13616003493";
                    label.VSART_DES = "集装箱";
                    return label;
                }

                string sql = @"SELECT * FROM ShippingLabel WHERE DOCNO = @DOCNO AND FYDT = @FYDT";
                SqlParameter p1 = DBHelper.CreateParameter("@DOCNO", docno);
                SqlParameter p2 = DBHelper.CreateParameter("@FYDT", fydt);

                DataTable table = DBHelper.GetTable(sql, false, p1, p2);

                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    ShippingLabel item = new ShippingLabel();
                    item.SHIP_DATE = DateTime.Parse(row["SHIP_DATE"].ToString());
                    item.STORE_ID = (string)row["STORE_ID"];
                    item.STORE_NAME = (string)row["STORE_NAME"];
                    item.DISPATCH_AREA = (string)row["DISPATCH_AREA"];
                    item.COLLECT_WAVE = (string)row["COLLECT_WAVE"];
                    item.VSART_DES = (string)row["VSART_DES"];
                    item.ROUTE_NO = (string)row["ROUTE_NO"];
                    item.ROUTE_DES = row["ROUTE_DES"] == null ? null : row["ROUTE_DES"].ToString();
                    item.ADDRESS = (string)row["ADDRESS"];
                    item.TEL_NUMBER = (string)row["TEL_NUMBER"];
                    item.LANE_ID = (string)row["LANE_ID"];

                    item.FYDT = (string)row["FYDT"];
                    item.VSART = (string)row["VSART"];
                    item.ZYT = (string)row["ZYT"];
                    item.FY_WAVE = (string)row["FY_WAVE"];
                    item.WAVE_AND_YT = (string)row["WAVE_AND_YT"];
                    item.IS_FBC = row["IS_FBC"] == null ? "" : row["IS_FBC"].ToString();

                    item.COLLECT_SEQ = row["COLLECT_SEQ"] == null ? "" : row["COLLECT_SEQ"].ToString();
                    item.ZSDABW = row["ZSDABW"] == null ? "" : row["ZSDABW"].ToString();
                    item.ZSDABW_DES = row["ZSDABW_DES"] == null ? "" : row["ZSDABW_DES"].ToString();

                    item.DOCNO = row["DOCNO"] == null ? "" : row["DOCNO"].ToString();

                    DateTime pd = Convert.ToDateTime("2000-1-1");
                    DateTime.TryParse(row["PICK_DATE"].ToString(), out pd);
                    item.PICK_DATE = pd;

                    return item;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            return null;
        }
        /// <summary>
        /// 根据箱号获取发运箱
        /// </summary>
        /// <param name="boxNo">箱号</param>
        /// <returns></returns>
        public static ShippingBox GetShippingBoxByHu(string hu)
        {
            try
            {
                string sql = "P_ShippingBox_GetByHu ";
                SqlParameter p1 = DBHelper.CreateParameter("@HU", hu);

                DataSet ds = DBHelper.GetDataSet(sql, true, p1);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dtBox = ds.Tables[0];
                    DataTable dtBoxdetail = ds.Tables[1];

                    DataRow row = dtBox.Rows[0];
                    ShippingBox item = new ShippingBox();
                    item.HU = string.IsNullOrEmpty(row["HU"].ToString().Trim()) ? "" : row["HU"].ToString().Trim();
                    item.Floor = string.IsNullOrEmpty(row["Floor"].ToString().Trim()) ? "" : row["Floor"].ToString().Trim();
                    item.LGNUM = string.IsNullOrEmpty(row["LGNUM"].ToString().Trim()) ? "" : row["LGNUM"].ToString().Trim();
                    item.PARTNER = string.IsNullOrEmpty(row["PARTNER"].ToString().Trim()) ? "" : row["PARTNER"].ToString().Trim();
                    item.PMAT_MATNR = string.IsNullOrEmpty(row["PMAT_MATNR"].ToString().Trim()) ? "" : row["PMAT_MATNR"].ToString().Trim();
                    item.MAKTX = string.IsNullOrEmpty(row["MAKTX"].ToString().Trim()) ? "" : row["MAKTX"].ToString().Trim();
                    item.SHIP_DATE = string.IsNullOrEmpty(row["SHIP_DATE"].ToString().Trim()) ? new DateTime(1900, 1, 1) : DateTime.Parse(row["SHIP_DATE"].ToString().Trim());
                    item.IsFull = string.IsNullOrEmpty(row["IsFull"].ToString().Trim()) ? (byte)0 : byte.Parse(row["IsFull"].ToString().Trim());
                    item.QTY = string.IsNullOrEmpty(row["QTY"].ToString().Trim()) ? 0 : int.Parse(row["QTY"].ToString().Trim());
                    item.AddQTY = string.IsNullOrEmpty(row["QTY_ADD"].ToString().Trim()) ? 0 : int.Parse(row["QTY_ADD"].ToString().Trim());
                    item.SKUCOUNT = string.IsNullOrEmpty(row["SKUCOUNT"].ToString().Trim()) ? 0 : int.Parse(row["SKUCOUNT"].ToString().Trim());
                    item.IsScanBox = string.IsNullOrEmpty(row["IsScanBox"].ToString().Trim()) ? (byte)0 : byte.Parse(row["IsScanBox"].ToString().Trim());
                    DataRow[] detailRows = dtBoxdetail.Select("HU=" + item.HU);
                    foreach (DataRow rowdetail in detailRows)
                    {
                        ShippingBoxDetail detail = new ShippingBoxDetail();
                        detail.CHARG = string.IsNullOrEmpty(rowdetail["CHARG"].ToString().Trim()) ? "" : rowdetail["CHARG"].ToString().Trim();
                        detail.EPC = string.IsNullOrEmpty(rowdetail["EPC"].ToString().Trim()) ? "" : rowdetail["EPC"].ToString().Trim();
                        detail.Handled = string.IsNullOrEmpty(rowdetail["Handled"].ToString().Trim()) ? (byte)0 : byte.Parse(rowdetail["Handled"].ToString().Trim());
                        detail.HU = string.IsNullOrEmpty(rowdetail["HU"].ToString().Trim()) ? "" : rowdetail["HU"].ToString().Trim();
                        detail.Id = string.IsNullOrEmpty(rowdetail["Id"].ToString().Trim()) ? 0 : long.Parse(rowdetail["Id"].ToString().Trim());
                        detail.IsADD = string.IsNullOrEmpty(rowdetail["IsADD"].ToString().Trim()) ? (byte)0 : byte.Parse(rowdetail["IsADD"].ToString().Trim());
                        detail.IsRFID = string.IsNullOrEmpty(rowdetail["IsRFID"].ToString().Trim()) ? (byte)0 : byte.Parse(rowdetail["IsRFID"].ToString().Trim());
                        detail.PICK_TASK = string.IsNullOrEmpty(rowdetail["PICK_TASK"].ToString().Trim()) ? "" : rowdetail["PICK_TASK"].ToString().Trim();
                        detail.PICK_TASK_ITEM = string.IsNullOrEmpty(rowdetail["PICK_TASK_ITEM"].ToString().Trim()) ? "" : rowdetail["PICK_TASK_ITEM"].ToString().Trim();
                        detail.ZCOLSN = string.IsNullOrEmpty(rowdetail["ZCOLSN"].ToString().Trim()) ? "" : rowdetail["ZCOLSN"].ToString().Trim();
                        detail.ZSATNR = string.IsNullOrEmpty(rowdetail["ZSATNR"].ToString().Trim()) ? "" : rowdetail["ZSATNR"].ToString().Trim();
                        detail.ZSIZTX = string.IsNullOrEmpty(rowdetail["ZSIZTX"].ToString().Trim()) ? "" : rowdetail["ZSIZTX"].ToString().Trim();
                        detail.MATNR = string.IsNullOrEmpty(rowdetail["MATNR"].ToString().Trim()) ? "" : rowdetail["MATNR"].ToString().Trim();
                        detail.UOM = string.IsNullOrEmpty(rowdetail["UOM"].ToString().Trim()) ? "" : rowdetail["UOM"].ToString().Trim();
                        if (item.Details == null)
                            item.Details = new List<ShippingBoxDetail>();
                        item.Details.Add(detail);
                    }
                    return item;
                }

                return null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 根据下架单号和箱号删除发运箱epc明细
        /// </summary>
        /// <param name="pickTask"></param>
        /// <param name="hu"></param>
        /// <param name="newqty"></param>
        /// <param name="newaddqty"></param>
        /// <returns></returns>
        public static bool DeleteShippingBoxDetail(string pickTask, string hu, int newqty, int newaddqty)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE FROM ShippingBoxDetail WHERE HU = @HU AND PICK_TASK = @PICK_TASK");
                //string sql = "DELETE FROM ShippingBoxDetail WHERE HU = @HU AND PICK_TASK = @PICK_TASK";
                SqlParameter p1 = DBHelper.CreateParameter("@PICK_TASK", pickTask);
                SqlParameter p2 = DBHelper.CreateParameter("@HU", hu);

                int result = DBHelper.ExecuteSql(sb.ToString(), false, p1, p2);

                sb.Clear();
                sb.AppendFormat("SELECT COUNT(1) FROM ShippingBoxDetail WHERE HU='{0}'", hu);
                int count = int.Parse(DBHelper.GetValue(sb.ToString(), false).ToString());
                if (count <= 0)
                {
                    sb.Clear();
                    sb.AppendFormat("DELETE FROM ShippingBox WHERE HU = '{0}'", hu);
                }
                else
                {
                    sb.Clear();
                    sb.AppendFormat("UPDATE ShippingBox SET QTY={0},QTY_ADD={1} WHERE HU='{2}'", newqty, newaddqty, hu);
                }
                DBHelper.ExecuteSql(sb.ToString(), false);
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 设置下架单是否出库
        /// </summary>
        /// <param name="pickTask">下架单号</param>
        /// <param name="isOut">出库状态</param>
        /// <returns></returns>
        public static bool SetInventoryOutLogDetailOutStatus(string pickTask, int isOut)
        {
            try
            {
                string sql = "UPDATE InventoryOutLogDetail SET IsOut = @IsOut WHERE PICK_TASK = @PICK_TASK";
                SqlParameter p1 = DBHelper.CreateParameter("@PICK_TASK", pickTask);
                SqlParameter p2 = DBHelper.CreateParameter("@IsOut", isOut);

                int result = DBHelper.ExecuteSql(sql, false, p1, p2);

                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// 保存下架单
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool SaveInventoryOutLogDetail(InventoryOutLogDetailInfo info)
        {
            try
            {
                string sql = string.Format("P_InventoryOutLogDetail_Insert ");
                SqlParameter p1 = DBHelper.CreateParameter("@LGNUM", info.LGNUM);
                SqlParameter p2 = DBHelper.CreateParameter("@PICK_TASK", info.PICK_TASK);
                SqlParameter p3 = DBHelper.CreateParameter("@PICK_TASK_ITEM", info.PICK_TASK_ITEM);
                SqlParameter p4 = DBHelper.CreateParameter("@DOCNO", info.DOCNO);
                SqlParameter p5 = DBHelper.CreateParameter("@ITEMNO", info.ITEMNO);
                SqlParameter p6 = DBHelper.CreateParameter("@PRODUCTNO", info.PRODUCTNO);
                SqlParameter p7 = DBHelper.CreateParameter("@QTY", info.QTY);
                SqlParameter p8 = DBHelper.CreateParameter("@UOM", info.UOM);
                SqlParameter p9 = DBHelper.CreateParameter("@SHIP_DATE", info.SHIP_DATE);
                SqlParameter p10 = DBHelper.CreateParameter("@PARTNER", info.PARTNER);
                SqlParameter p11 = DBHelper.CreateParameter("@STOBIN", info.STOBIN);
                SqlParameter p12 = DBHelper.CreateParameter("@LGTYP", info.LGTYP);
                SqlParameter p13 = DBHelper.CreateParameter("@LGTYP_R", info.LGTYP_R);
                SqlParameter p14 = DBHelper.CreateParameter("@ZXJD_TYPE", info.ZXJD_TYPE);
                SqlParameter p15 = DBHelper.CreateParameter("@IsOut", info.IsOut);
                SqlParameter p16 = DBHelper.CreateParameter("@REALQTY", info.REALQTY);
                SqlParameter p17 = DBHelper.CreateParameter("@REALQTY_ADD", info.REALQTY_ADD);
                SqlParameter p18 = DBHelper.CreateParameter("@MX", info.MX);

                SqlParameter p19 = DBHelper.CreateParameter("@VSART", info.VSART);
                SqlParameter p20 = DBHelper.CreateParameter("@IS_FBC", info.IS_FBC);
                SqlParameter p21 = DBHelper.CreateParameter("@BRAND", info.BRAND);
                //SqlParameter p19 = DBHelper.CreateParameter("@STATUS", info.Status);
                int result = DBHelper.GetValue(sql, true, p1, p2, p3, p4, p5, p6
                    , p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18,p19,p20,p21
                    ).CastTo<int>(-1);
                return result>0;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return false;
            }
        }
        public static bool SaveInventoryOutLogDetailWithReset(InventoryOutLogDetailInfo info)
        {
            try
            {
                string sql = string.Format("P_InventoryOutLogDetail_Insert_ResetIfExist");
                SqlParameter p1 = DBHelper.CreateParameter("@LGNUM", info.LGNUM);
                SqlParameter p2 = DBHelper.CreateParameter("@PICK_TASK", info.PICK_TASK);
                SqlParameter p3 = DBHelper.CreateParameter("@PICK_TASK_ITEM", info.PICK_TASK_ITEM);
                SqlParameter p4 = DBHelper.CreateParameter("@DOCNO", info.DOCNO);
                SqlParameter p5 = DBHelper.CreateParameter("@ITEMNO", info.ITEMNO);
                SqlParameter p6 = DBHelper.CreateParameter("@PRODUCTNO", info.PRODUCTNO);
                SqlParameter p7 = DBHelper.CreateParameter("@QTY", info.QTY);
                SqlParameter p8 = DBHelper.CreateParameter("@UOM", info.UOM);
                SqlParameter p9 = DBHelper.CreateParameter("@SHIP_DATE", info.SHIP_DATE);
                SqlParameter p10 = DBHelper.CreateParameter("@PARTNER", info.PARTNER);
                SqlParameter p11 = DBHelper.CreateParameter("@STOBIN", info.STOBIN);
                SqlParameter p12 = DBHelper.CreateParameter("@LGTYP", info.LGTYP);
                SqlParameter p13 = DBHelper.CreateParameter("@LGTYP_R", info.LGTYP_R);
                SqlParameter p14 = DBHelper.CreateParameter("@ZXJD_TYPE", info.ZXJD_TYPE);
                SqlParameter p15 = DBHelper.CreateParameter("@IsOut", info.IsOut);
                SqlParameter p16 = DBHelper.CreateParameter("@REALQTY", info.REALQTY);
                SqlParameter p17 = DBHelper.CreateParameter("@REALQTY_ADD", info.REALQTY_ADD);
                SqlParameter p18 = DBHelper.CreateParameter("@MX", info.MX);

                SqlParameter p19 = DBHelper.CreateParameter("@VSART", info.VSART);
                SqlParameter p20 = DBHelper.CreateParameter("@IS_FBC", info.IS_FBC);
                SqlParameter p21 = DBHelper.CreateParameter("@BRAND", info.BRAND);
                //SqlParameter p19 = DBHelper.CreateParameter("@STATUS", info.Status);
                int result = DBHelper.GetValue(sql, true, p1, p2, p3, p4, p5, p6
                    , p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21
                    ).CastTo<int>(-1);
                return result > 0;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 保存发运箱信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool SaveShippingBox(ShippingBox info)
        {

            try
            {
                //先保存主表
                string sql = string.Format("P_ShippingBox_Save ");
                SqlParameter p1 = DBHelper.CreateParameter("@LGNUM", info.LGNUM);
                SqlParameter p2 = DBHelper.CreateParameter("@HU", info.HU);
                SqlParameter p3 = DBHelper.CreateParameter("@IsFull", info.IsFull);
                SqlParameter p4 = DBHelper.CreateParameter("@Floor", info.Floor);
                SqlParameter p5 = DBHelper.CreateParameter("@QTY", info.QTY);
                SqlParameter p6 = DBHelper.CreateParameter("@PMAT_MATNR", info.PMAT_MATNR);
                SqlParameter p8 = DBHelper.CreateParameter("@MAKTX", info.MAKTX);
                SqlParameter p9 = DBHelper.CreateParameter("@SHIP_DATE", info.SHIP_DATE);
                SqlParameter p10 = DBHelper.CreateParameter("@PARTNER", info.PARTNER);
                SqlParameter p7 = DBHelper.CreateParameter("@SKUCOUNT", info.SKUCOUNT);
                SqlParameter p11 = DBHelper.CreateParameter("@IsScanBox", info.IsScanBox);
                SqlParameter p12 = DBHelper.CreateParameter("@QTY_ADD", info.QTY);
                int result = int.Parse(DBHelper.GetValue(sql, true, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12).ToString());

                //保存从表

                if (info.Details != null && info.Details.Count > 0)
                {
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.AppendFormat("DELETE FROM ShippingBoxDetail WHERE HU='{0}' ", info.HU);
                    sbSql.AppendLine("INSERT INTO ShippingBoxDetail(HU ,ZSATNR ,ZCOLSN ,ZSIZTX ,PICK_TASK ,EPC,Handled,IsADD,IsRFID,PICK_TASK_ITEM,CHARG,MATNR,UOM) VALUES ");
                    foreach (ShippingBoxDetail item in info.Details)
                    {
                        sbSql.AppendFormat("('{0}', '{1}', '{2}', '{3}', '{4}', '{5}',{6},{7},{8},'{9}','{10}','{11}','{12}'),",
                            item.HU, item.ZSATNR, item.ZCOLSN, item.ZSIZTX, item.PICK_TASK, item.EPC, item.Handled,
                            item.IsADD, item.IsRFID, item.PICK_TASK_ITEM, item.CHARG, item.MATNR, item.UOM);
                    }

                    if (sbSql.ToString().EndsWith(","))
                        sbSql.Remove(sbSql.Length - 1, 1);

                    DBHelper.ExecuteSql(sbSql.ToString(), false);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return false;
            }


        }

        /// <summary>
        /// 设置扫描箱
        /// </summary>
        /// <param name="hu"></param>
        /// <returns></returns>
        public static bool SetShippingBoxIsScanBox(string hu)
        {
            try
            {
                string sql = string.Format("P_ShippingBox_SetScanBox ");
                SqlParameter p1 = DBHelper.CreateParameter("@HU", hu);
                int result = int.Parse(DBHelper.GetValue(sql, true, p1).ToString());

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return false;
            }

        }

        /// <summary>
        /// 根据下架单号，获取该下架单所属的门店下的所有下架单信息
        /// </summary>
        /// <param name="pick_task"></param>
        /// <returns></returns>
        public static List<InventoryOutLogDetailInfo> GetPartnerInventoryOutLogDetailList(string pick_task)
        {
            try
            {
                List<InventoryOutLogDetailInfo> list = new List<InventoryOutLogDetailInfo>();
                string sql = "P_InventoryOutLogDetail_Get ";
                SqlParameter p1 = DBHelper.CreateParameter("@PICK_TASK", pick_task);

                DataTable dt = DBHelper.GetTable(sql, true, p1);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        InventoryOutLogDetailInfo item = new InventoryOutLogDetailInfo();
                        item.DOCNO = string.IsNullOrEmpty(row["DOCNO"].ToString().Trim()) ? "" : row["DOCNO"].ToString().Trim();
                        item.ITEMNO = string.IsNullOrEmpty(row["ITEMNO"].ToString().Trim()) ? "" : row["ITEMNO"].ToString().Trim();
                        item.LGNUM = string.IsNullOrEmpty(row["LGNUM"].ToString().Trim()) ? "" : row["LGNUM"].ToString().Trim();
                        item.LGTYP = string.IsNullOrEmpty(row["LGTYP"].ToString().Trim()) ? "" : row["LGTYP"].ToString().Trim();
                        item.LGTYP_R = string.IsNullOrEmpty(row["LGTYP_R"].ToString().Trim()) ? "" : row["LGTYP_R"].ToString().Trim();
                        item.PARTNER = string.IsNullOrEmpty(row["PARTNER"].ToString().Trim()) ? "" : row["PARTNER"].ToString().Trim();
                        item.PICK_TASK = string.IsNullOrEmpty(row["PICK_TASK"].ToString().Trim()) ? "" : row["PICK_TASK"].ToString().Trim();
                        item.PICK_TASK_ITEM = string.IsNullOrEmpty(row["PICK_TASK_ITEM"].ToString().Trim()) ? "" : row["PICK_TASK_ITEM"].ToString().Trim();
                        item.PRODUCTNO = string.IsNullOrEmpty(row["PRODUCTNO"].ToString().Trim()) ? "" : row["PRODUCTNO"].ToString().Trim();
                        item.QTY = string.IsNullOrEmpty(row["QTY"].ToString().Trim()) ? 0 : int.Parse(row["QTY"].ToString().Trim());
                        item.REALQTY_ADD = string.IsNullOrEmpty(row["REALQTY_ADD"].ToString().Trim()) ? 0 : int.Parse(row["REALQTY_ADD"].ToString().Trim());
                        item.SHIP_DATE = string.IsNullOrEmpty(row["SHIP_DATE"].ToString().Trim()) ? new DateTime(1900, 1, 1) : DateTime.Parse(row["SHIP_DATE"].ToString().Trim());
                        item.STOBIN = string.IsNullOrEmpty(row["STOBIN"].ToString().Trim()) ? "" : row["STOBIN"].ToString().Trim();
                        item.UOM = string.IsNullOrEmpty(row["UOM"].ToString().Trim()) ? "" : row["UOM"].ToString().Trim();
                        item.ZXJD_TYPE = string.IsNullOrEmpty(row["ZXJD_TYPE"].ToString().Trim()) ? "" : row["ZXJD_TYPE"].ToString().Trim();
                        item.IsOut = string.IsNullOrEmpty(row["IsOut"].ToString().Trim()) ? (byte)0 : byte.Parse(row["IsOut"].ToString().Trim());
                        item.REALQTY = string.IsNullOrEmpty(row["REALQTY"].ToString().Trim()) ? 0 : int.Parse(row["REALQTY"].ToString().Trim());
                        item.REALQTY_ADD = string.IsNullOrEmpty(row["REALQTY_ADD"].ToString().Trim()) ? 0 : int.Parse(row["REALQTY_ADD"].ToString().Trim());

                        item.VSART = string.IsNullOrEmpty(row["VSART"].ToString().Trim()) ? "" : row["VSART"].ToString().Trim();
                        item.IS_FBC = string.IsNullOrEmpty(row["IS_FBC"].ToString().Trim()) ? "" : row["IS_FBC"].ToString().Trim();
                        item.BRAND = string.IsNullOrEmpty(row["BRAND"].ToString().Trim()) ? "" : row["BRAND"].ToString().Trim();
                        list.Add(item);
                    }
                    return list;
                }

                return list;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        public static List<InventoryOutLogDetailInfo> GetDeliverInventoryOutLogDetailByPicktask(string picktask)
        {
            try
            {
                List<InventoryOutLogDetailInfo> list = new List<InventoryOutLogDetailInfo>();
                string sql = string.Format(
                    @"SELECT  LGNUM ,PICK_TASK ,PICK_TASK_ITEM ,DOCNO ,
                    ITEMNO ,PRODUCTNO ,QTY ,UOM ,SHIP_DATE ,PARTNER ,
                    STOBIN ,LGTYP ,LGTYP_R ,ZXJD_TYPE ,IsOut,REALQTY_ADD,REALQTY,VSART,IS_FBC
                    FROM InventoryOutLogDetail
                    WHERE PICK_TASK = '{0}' and ZXJD_TYPE='2'", picktask);
                DataTable dt = DBHelper.GetTable(sql, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        InventoryOutLogDetailInfo item = new InventoryOutLogDetailInfo();
                        item.DOCNO = string.IsNullOrEmpty(row["DOCNO"].ToString().Trim()) ? "" : row["DOCNO"].ToString().Trim();
                        item.ITEMNO = string.IsNullOrEmpty(row["ITEMNO"].ToString().Trim()) ? "" : row["ITEMNO"].ToString().Trim();
                        item.LGNUM = string.IsNullOrEmpty(row["LGNUM"].ToString().Trim()) ? "" : row["LGNUM"].ToString().Trim();
                        item.LGTYP = string.IsNullOrEmpty(row["LGTYP"].ToString().Trim()) ? "" : row["LGTYP"].ToString().Trim();
                        item.LGTYP_R = string.IsNullOrEmpty(row["LGTYP_R"].ToString().Trim()) ? "" : row["LGTYP_R"].ToString().Trim();
                        item.PARTNER = string.IsNullOrEmpty(row["PARTNER"].ToString().Trim()) ? "" : row["PARTNER"].ToString().Trim();
                        item.PICK_TASK = string.IsNullOrEmpty(row["PICK_TASK"].ToString().Trim()) ? "" : row["PICK_TASK"].ToString().Trim();
                        item.PICK_TASK_ITEM = string.IsNullOrEmpty(row["PICK_TASK_ITEM"].ToString().Trim()) ? "" : row["PICK_TASK_ITEM"].ToString().Trim();
                        item.PRODUCTNO = string.IsNullOrEmpty(row["PRODUCTNO"].ToString().Trim()) ? "" : row["PRODUCTNO"].ToString().Trim();
                        item.QTY = string.IsNullOrEmpty(row["QTY"].ToString().Trim()) ? 0 : int.Parse(row["QTY"].ToString().Trim());
                        //item.REALQTY_ADD = string.IsNullOrEmpty(row["REALQTY_ADD"].ToString().Trim()) ? 0 : int.Parse(row["REALQTY_ADD"].ToString().Trim());
                        item.SHIP_DATE = string.IsNullOrEmpty(row["SHIP_DATE"].ToString().Trim()) ? new DateTime(1900, 1, 1) : DateTime.Parse(row["SHIP_DATE"].ToString().Trim());
                        item.STOBIN = string.IsNullOrEmpty(row["STOBIN"].ToString().Trim()) ? "" : row["STOBIN"].ToString().Trim();
                        item.UOM = string.IsNullOrEmpty(row["UOM"].ToString().Trim()) ? "" : row["UOM"].ToString().Trim();
                        item.ZXJD_TYPE = string.IsNullOrEmpty(row["ZXJD_TYPE"].ToString().Trim()) ? "" : row["ZXJD_TYPE"].ToString().Trim();
                        item.IsOut = string.IsNullOrEmpty(row["IsOut"].ToString().Trim()) ? (byte)0 : byte.Parse(row["IsOut"].ToString().Trim());
                        item.REALQTY = string.IsNullOrEmpty(row["REALQTY"].ToString().Trim()) ? 0 : int.Parse(row["REALQTY"].ToString().Trim());
                        item.REALQTY_ADD = string.IsNullOrEmpty(row["REALQTY_ADD"].ToString().Trim()) ? 0 : int.Parse(row["REALQTY_ADD"].ToString().Trim());

                        item.VSART = string.IsNullOrEmpty(row["VSART"].ToString().Trim()) ? "" : row["VSART"].ToString().Trim();
                        item.IS_FBC = string.IsNullOrEmpty(row["IS_FBC"].ToString().Trim()) ? "" : row["IS_FBC"].ToString().Trim();

                        list.Add(item);
                    }
                    return list;
                }

                return list;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 根据下架单号获取该下架单的详细信息
        /// </summary>
        /// <param name="picktask"></param>
        /// <returns></returns>
        public static List<InventoryOutLogDetailInfo> GetInventoryOutLogDetailByPicktask(string picktask)
        {
            try
            {
                List<InventoryOutLogDetailInfo> list = new List<InventoryOutLogDetailInfo>();
                string sql = string.Format(
                    @"SELECT  LGNUM ,PICK_TASK ,PICK_TASK_ITEM ,DOCNO ,
                    ITEMNO ,PRODUCTNO ,QTY ,UOM ,SHIP_DATE ,PARTNER ,
                    STOBIN ,LGTYP ,LGTYP_R ,ZXJD_TYPE ,IsOut,REALQTY_ADD,REALQTY,VSART,IS_FBC
                    FROM InventoryOutLogDetail
                    WHERE PICK_TASK = '{0}'", picktask);
                DataTable dt = DBHelper.GetTable(sql, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        InventoryOutLogDetailInfo item = new InventoryOutLogDetailInfo();
                        item.DOCNO = string.IsNullOrEmpty(row["DOCNO"].ToString().Trim()) ? "" : row["DOCNO"].ToString().Trim();
                        item.ITEMNO = string.IsNullOrEmpty(row["ITEMNO"].ToString().Trim()) ? "" : row["ITEMNO"].ToString().Trim();
                        item.LGNUM = string.IsNullOrEmpty(row["LGNUM"].ToString().Trim()) ? "" : row["LGNUM"].ToString().Trim();
                        item.LGTYP = string.IsNullOrEmpty(row["LGTYP"].ToString().Trim()) ? "" : row["LGTYP"].ToString().Trim();
                        item.LGTYP_R = string.IsNullOrEmpty(row["LGTYP_R"].ToString().Trim()) ? "" : row["LGTYP_R"].ToString().Trim();
                        item.PARTNER = string.IsNullOrEmpty(row["PARTNER"].ToString().Trim()) ? "" : row["PARTNER"].ToString().Trim();
                        item.PICK_TASK = string.IsNullOrEmpty(row["PICK_TASK"].ToString().Trim()) ? "" : row["PICK_TASK"].ToString().Trim();
                        item.PICK_TASK_ITEM = string.IsNullOrEmpty(row["PICK_TASK_ITEM"].ToString().Trim()) ? "" : row["PICK_TASK_ITEM"].ToString().Trim();
                        item.PRODUCTNO = string.IsNullOrEmpty(row["PRODUCTNO"].ToString().Trim()) ? "" : row["PRODUCTNO"].ToString().Trim();
                        item.QTY = string.IsNullOrEmpty(row["QTY"].ToString().Trim()) ? 0 : int.Parse(row["QTY"].ToString().Trim());
                        //item.REALQTY_ADD = string.IsNullOrEmpty(row["REALQTY_ADD"].ToString().Trim()) ? 0 : int.Parse(row["REALQTY_ADD"].ToString().Trim());
                        item.SHIP_DATE = string.IsNullOrEmpty(row["SHIP_DATE"].ToString().Trim()) ? new DateTime(1900, 1, 1) : DateTime.Parse(row["SHIP_DATE"].ToString().Trim());
                        item.STOBIN = string.IsNullOrEmpty(row["STOBIN"].ToString().Trim()) ? "" : row["STOBIN"].ToString().Trim();
                        item.UOM = string.IsNullOrEmpty(row["UOM"].ToString().Trim()) ? "" : row["UOM"].ToString().Trim();
                        item.ZXJD_TYPE = string.IsNullOrEmpty(row["ZXJD_TYPE"].ToString().Trim()) ? "" : row["ZXJD_TYPE"].ToString().Trim();
                        item.IsOut = string.IsNullOrEmpty(row["IsOut"].ToString().Trim()) ? (byte)0 : byte.Parse(row["IsOut"].ToString().Trim());
                        item.REALQTY = string.IsNullOrEmpty(row["REALQTY"].ToString().Trim()) ? 0 : int.Parse(row["REALQTY"].ToString().Trim());
                        item.REALQTY_ADD = string.IsNullOrEmpty(row["REALQTY_ADD"].ToString().Trim()) ? 0 : int.Parse(row["REALQTY_ADD"].ToString().Trim());

                        item.VSART = string.IsNullOrEmpty(row["VSART"].ToString().Trim()) ? "" : row["VSART"].ToString().Trim();
                        item.IS_FBC = string.IsNullOrEmpty(row["IS_FBC"].ToString().Trim()) ? "" : row["IS_FBC"].ToString().Trim();

                        list.Add(item);
                    }
                    return list;
                }

                return list;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 根据门店/楼层 获取发运箱数据
        /// </summary>
        /// <param name="partner">门店</param>
        /// <param name="floor">楼层</param>
        /// <param name="isfull">是否满箱 1是0否2全部</param>
        /// <returns></returns>
        public static List<ShippingBox> GetShippingBoxList(string partner, string floor, DateTime SHIP_DATE, byte isfull)
        {
            try
            {
                string sql = "P_ShippingBox_Get ";
                SqlParameter p1 = DBHelper.CreateParameter("@PARTNER", partner);
                SqlParameter p2 = DBHelper.CreateParameter("@Floor", floor);
                SqlParameter p3 = DBHelper.CreateParameter("@SHIP_DATE", SHIP_DATE);
                SqlParameter p4 = DBHelper.CreateParameter("@IsFull", isfull);

                DataSet ds;
                if (isfull == 1 || isfull == 0)
                    ds = DBHelper.GetDataSet(sql, true, p1, p2, p3, p4);
                else
                    ds = DBHelper.GetDataSet(sql, true, p1, p2, p3);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dtBox = ds.Tables[0];
                    DataTable dtBoxdetail = ds.Tables[1];
                    List<ShippingBox> list = new List<ShippingBox>();
                    foreach (DataRow row in dtBox.Rows)
                    {
                        ShippingBox item = new ShippingBox();
                        item.HU = string.IsNullOrEmpty(row["HU"].ToString().Trim()) ? "" : row["HU"].ToString().Trim();
                        item.Floor = string.IsNullOrEmpty(row["Floor"].ToString().Trim()) ? "" : row["Floor"].ToString().Trim();
                        item.LGNUM = string.IsNullOrEmpty(row["LGNUM"].ToString().Trim()) ? "" : row["LGNUM"].ToString().Trim();
                        item.PARTNER = string.IsNullOrEmpty(row["PARTNER"].ToString().Trim()) ? "" : row["PARTNER"].ToString().Trim();
                        item.PMAT_MATNR = string.IsNullOrEmpty(row["PMAT_MATNR"].ToString().Trim()) ? "" : row["PMAT_MATNR"].ToString().Trim();
                        item.MAKTX = string.IsNullOrEmpty(row["MAKTX"].ToString().Trim()) ? "" : row["MAKTX"].ToString().Trim();
                        item.SHIP_DATE = string.IsNullOrEmpty(row["SHIP_DATE"].ToString().Trim()) ? new DateTime(1900, 1, 1) : DateTime.Parse(row["SHIP_DATE"].ToString().Trim());
                        item.IsFull = string.IsNullOrEmpty(row["IsFull"].ToString().Trim()) ? (byte)0 : byte.Parse(row["IsFull"].ToString().Trim());
                        item.QTY = string.IsNullOrEmpty(row["QTY"].ToString().Trim()) ? 0 : int.Parse(row["QTY"].ToString().Trim());
                        item.SKUCOUNT = string.IsNullOrEmpty(row["SKUCOUNT"].ToString().Trim()) ? 0 : int.Parse(row["SKUCOUNT"].ToString().Trim());
                        item.IsScanBox = string.IsNullOrEmpty(row["IsScanBox"].ToString().Trim()) ? (byte)0 : byte.Parse(row["IsScanBox"].ToString().Trim());
                        DataRow[] detailRows = dtBoxdetail.Select("HU=" + item.HU);
                        foreach (DataRow rowdetail in detailRows)
                        {
                            ShippingBoxDetail detail = new ShippingBoxDetail();
                            detail.CHARG = string.IsNullOrEmpty(rowdetail["CHARG"].ToString().Trim()) ? "" : rowdetail["CHARG"].ToString().Trim();
                            detail.EPC = string.IsNullOrEmpty(rowdetail["EPC"].ToString().Trim()) ? "" : rowdetail["EPC"].ToString().Trim();
                            detail.Handled = string.IsNullOrEmpty(rowdetail["Handled"].ToString().Trim()) ? (byte)0 : byte.Parse(rowdetail["Handled"].ToString().Trim());
                            detail.HU = string.IsNullOrEmpty(rowdetail["HU"].ToString().Trim()) ? "" : rowdetail["HU"].ToString().Trim();
                            detail.Id = string.IsNullOrEmpty(rowdetail["Id"].ToString().Trim()) ? 0 : long.Parse(rowdetail["Id"].ToString().Trim());
                            detail.IsADD = string.IsNullOrEmpty(rowdetail["IsADD"].ToString().Trim()) ? (byte)0 : byte.Parse(rowdetail["IsADD"].ToString().Trim());
                            detail.IsRFID = string.IsNullOrEmpty(rowdetail["IsRFID"].ToString().Trim()) ? (byte)0 : byte.Parse(rowdetail["IsRFID"].ToString().Trim());
                            detail.PICK_TASK = string.IsNullOrEmpty(rowdetail["PICK_TASK"].ToString().Trim()) ? "" : rowdetail["PICK_TASK"].ToString().Trim();
                            detail.PICK_TASK_ITEM = string.IsNullOrEmpty(rowdetail["PICK_TASK_ITEM"].ToString().Trim()) ? "" : rowdetail["PICK_TASK_ITEM"].ToString().Trim();
                            detail.ZCOLSN = string.IsNullOrEmpty(rowdetail["ZCOLSN"].ToString().Trim()) ? "" : rowdetail["ZCOLSN"].ToString().Trim();
                            detail.ZSATNR = string.IsNullOrEmpty(rowdetail["ZSATNR"].ToString().Trim()) ? "" : rowdetail["ZSATNR"].ToString().Trim();
                            detail.ZSIZTX = string.IsNullOrEmpty(rowdetail["ZSIZTX"].ToString().Trim()) ? "" : rowdetail["ZSIZTX"].ToString().Trim();
                            detail.MATNR = string.IsNullOrEmpty(rowdetail["MATNR"].ToString().Trim()) ? "" : rowdetail["MATNR"].ToString().Trim();
                            detail.UOM = string.IsNullOrEmpty(rowdetail["UOM"].ToString().Trim()) ? "" : rowdetail["UOM"].ToString().Trim();

                            if (item.Details == null)
                                item.Details = new List<ShippingBoxDetail>();
                            item.Details.Add(detail);
                        }
                        list.Add(item);
                    }
                    return list;
                }

                return null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 根据下架单号获取发运箱数据
        /// </summary>
        /// <param name="picktask"></param>
        /// <returns></returns>
        public static List<ShippingBox> GetShippingBoxListByPickTask(string picktask, byte isfull)
        {
            //P_ShippingBox_GetByPickTask
            try
            {
                string sql = "P_ShippingBox_GetByPickTask ";
                SqlParameter p1 = DBHelper.CreateParameter("@PICK_TASK", picktask);
                SqlParameter p2 = DBHelper.CreateParameter("@IsFull", isfull);

                DataSet ds;
                //if (isfull == 1 || isfull == 0)
                ds = DBHelper.GetDataSet(sql, true, p1, p2);
                //else
                //    ds = DBHelper.GetDataSet(sql, true, p1);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dtBox = ds.Tables[0];
                    DataTable dtBoxdetail = ds.Tables[1];
                    List<ShippingBox> list = new List<ShippingBox>();
                    foreach (DataRow row in dtBox.Rows)
                    {
                        ShippingBox item = new ShippingBox();
                        item.HU = string.IsNullOrEmpty(row["HU"].ToString().Trim()) ? "" : row["HU"].ToString().Trim();
                        item.Floor = string.IsNullOrEmpty(row["Floor"].ToString().Trim()) ? "" : row["Floor"].ToString().Trim();
                        item.LGNUM = string.IsNullOrEmpty(row["LGNUM"].ToString().Trim()) ? "" : row["LGNUM"].ToString().Trim();
                        item.PARTNER = string.IsNullOrEmpty(row["PARTNER"].ToString().Trim()) ? "" : row["PARTNER"].ToString().Trim();
                        item.PMAT_MATNR = string.IsNullOrEmpty(row["PMAT_MATNR"].ToString().Trim()) ? "" : row["PMAT_MATNR"].ToString().Trim();
                        item.MAKTX = string.IsNullOrEmpty(row["MAKTX"].ToString().Trim()) ? "" : row["MAKTX"].ToString().Trim();
                        item.SHIP_DATE = string.IsNullOrEmpty(row["SHIP_DATE"].ToString().Trim()) ? new DateTime(1900, 1, 1) : DateTime.Parse(row["SHIP_DATE"].ToString().Trim());
                        item.IsFull = string.IsNullOrEmpty(row["IsFull"].ToString().Trim()) ? (byte)0 : byte.Parse(row["IsFull"].ToString().Trim());
                        item.QTY = string.IsNullOrEmpty(row["QTY"].ToString().Trim()) ? 0 : int.Parse(row["QTY"].ToString().Trim());
                        item.AddQTY = string.IsNullOrEmpty(row["QTY_ADD"].ToString().Trim()) ? 0 : int.Parse(row["QTY_ADD"].ToString().Trim());
                        item.SKUCOUNT = string.IsNullOrEmpty(row["SKUCOUNT"].ToString().Trim()) ? 0 : int.Parse(row["SKUCOUNT"].ToString().Trim());
                        item.IsScanBox = string.IsNullOrEmpty(row["IsScanBox"].ToString().Trim()) ? (byte)0 : byte.Parse(row["IsScanBox"].ToString().Trim());
                        DataRow[] detailRows = dtBoxdetail.Select("HU=" + item.HU);
                        foreach (DataRow rowdetail in detailRows)
                        {
                            ShippingBoxDetail detail = new ShippingBoxDetail();
                            detail.CHARG = string.IsNullOrEmpty(rowdetail["CHARG"].ToString().Trim()) ? "" : rowdetail["CHARG"].ToString().Trim();
                            detail.EPC = string.IsNullOrEmpty(rowdetail["EPC"].ToString().Trim()) ? "" : rowdetail["EPC"].ToString().Trim();
                            detail.Handled = string.IsNullOrEmpty(rowdetail["Handled"].ToString().Trim()) ? (byte)0 : byte.Parse(rowdetail["Handled"].ToString().Trim());
                            detail.HU = string.IsNullOrEmpty(rowdetail["HU"].ToString().Trim()) ? "" : rowdetail["HU"].ToString().Trim();
                            detail.Id = string.IsNullOrEmpty(rowdetail["Id"].ToString().Trim()) ? 0 : long.Parse(rowdetail["Id"].ToString().Trim());
                            detail.IsADD = string.IsNullOrEmpty(rowdetail["IsADD"].ToString().Trim()) ? (byte)0 : byte.Parse(rowdetail["IsADD"].ToString().Trim());
                            detail.IsRFID = string.IsNullOrEmpty(rowdetail["IsRFID"].ToString().Trim()) ? (byte)0 : byte.Parse(rowdetail["IsRFID"].ToString().Trim());
                            detail.PICK_TASK = string.IsNullOrEmpty(rowdetail["PICK_TASK"].ToString().Trim()) ? "" : rowdetail["PICK_TASK"].ToString().Trim();
                            detail.PICK_TASK_ITEM = string.IsNullOrEmpty(rowdetail["PICK_TASK_ITEM"].ToString().Trim()) ? "" : rowdetail["PICK_TASK_ITEM"].ToString().Trim();
                            detail.ZCOLSN = string.IsNullOrEmpty(rowdetail["ZCOLSN"].ToString().Trim()) ? "" : rowdetail["ZCOLSN"].ToString().Trim();
                            detail.ZSATNR = string.IsNullOrEmpty(rowdetail["ZSATNR"].ToString().Trim()) ? "" : rowdetail["ZSATNR"].ToString().Trim();
                            detail.ZSIZTX = string.IsNullOrEmpty(rowdetail["ZSIZTX"].ToString().Trim()) ? "" : rowdetail["ZSIZTX"].ToString().Trim();
                            detail.MATNR = string.IsNullOrEmpty(rowdetail["MATNR"].ToString().Trim()) ? "" : rowdetail["MATNR"].ToString().Trim();
                            detail.UOM = string.IsNullOrEmpty(rowdetail["UOM"].ToString().Trim()) ? "" : rowdetail["UOM"].ToString().Trim();
                            if (item.Details == null)
                                item.Details = new List<ShippingBoxDetail>();
                            item.Details.Add(detail);
                        }
                        list.Add(item);
                    }
                    return list;
                }

                return null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 根据箱码获取该发运箱的epc明细
        /// </summary>
        /// <param name="hu"></param>
        /// <returns></returns>
        public static List<ShippingBoxDetail> GetShippingBoxDetailListByHu(string hu)
        {
            try
            {
                string sql = "P_ShippingBoxDetail_GetByHu ";
                SqlParameter p1 = DBHelper.CreateParameter("@HU", hu);
                DataTable dt = DBHelper.GetTable(sql, true, p1);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<ShippingBoxDetail> list = new List<ShippingBoxDetail>();
                    foreach (DataRow row in dt.Rows)
                    {
                        ShippingBoxDetail item = new ShippingBoxDetail();
                        item.HU = string.IsNullOrEmpty(row["HU"].ToString().Trim()) ? "" : row["HU"].ToString().Trim();
                        item.EPC = string.IsNullOrEmpty(row["EPC"].ToString().Trim()) ? "" : row["EPC"].ToString().Trim();
                        item.Id = string.IsNullOrEmpty(row["Id"].ToString().Trim()) ? 0 : long.Parse(row["Id"].ToString().Trim());
                        item.PICK_TASK = string.IsNullOrEmpty(row["PICK_TASK"].ToString().Trim()) ? "" : row["PICK_TASK"].ToString().Trim();
                        item.ZCOLSN = string.IsNullOrEmpty(row["ZCOLSN"].ToString().Trim()) ? "" : row["ZCOLSN"].ToString().Trim();
                        item.ZSATNR = string.IsNullOrEmpty(row["ZSATNR"].ToString().Trim()) ? "" : row["ZSATNR"].ToString().Trim();
                        item.ZSIZTX = string.IsNullOrEmpty(row["ZSIZTX"].ToString().Trim()) ? "" : row["ZSIZTX"].ToString().Trim();
                        item.Handled = string.IsNullOrEmpty(row["Handled"].ToString().Trim()) ? (byte)0 : byte.Parse(row["Handled"].ToString().Trim());
                        item.IsADD = string.IsNullOrEmpty(row["IsADD"].ToString().Trim()) ? (byte)0 : byte.Parse(row["IsADD"].ToString().Trim());
                        item.IsRFID = string.IsNullOrEmpty(row["IsRFID"].ToString().Trim()) ? (byte)0 : byte.Parse(row["IsRFID"].ToString().Trim());
                        item.PICK_TASK_ITEM = string.IsNullOrEmpty(row["PICK_TASK_ITEM"].ToString().Trim()) ? "" : row["PICK_TASK_ITEM"].ToString().Trim();
                        item.CHARG = string.IsNullOrEmpty(row["CHARG"].ToString().Trim()) ? "" : row["CHARG"].ToString().Trim();
                        item.MATNR = string.IsNullOrEmpty(row["MATNR"].ToString().Trim()) ? "" : row["MATNR"].ToString().Trim();
                        item.UOM = string.IsNullOrEmpty(row["UOM"].ToString().Trim()) ? "" : row["UOM"].ToString().Trim();
                        list.Add(item);
                    }
                    return list;
                }

                return null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 根据下架单号获取相关的epc明细
        /// </summary>
        /// <param name="hu"></param>
        /// <returns></returns>
        public static List<ShippingBoxDetail> GetShippingBoxDetailListByPickTask(string picktask)
        {
            try
            {
                string sql = "P_ShippingBoxDetail_GetByPickTask ";
                SqlParameter p1 = DBHelper.CreateParameter("@PICK_TASK", picktask);
                DataTable dt = DBHelper.GetTable(sql, true, p1);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<ShippingBoxDetail> list = new List<ShippingBoxDetail>();
                    foreach (DataRow row in dt.Rows)
                    {
                        ShippingBoxDetail item = new ShippingBoxDetail();
                        item.HU = string.IsNullOrEmpty(row["HU"].ToString().Trim()) ? "" : row["HU"].ToString().Trim();
                        item.EPC = string.IsNullOrEmpty(row["EPC"].ToString().Trim()) ? "" : row["EPC"].ToString().Trim();
                        item.Id = string.IsNullOrEmpty(row["Id"].ToString().Trim()) ? 0 : long.Parse(row["Id"].ToString().Trim());
                        item.PICK_TASK = string.IsNullOrEmpty(row["PICK_TASK"].ToString().Trim()) ? "" : row["PICK_TASK"].ToString().Trim();
                        item.ZCOLSN = string.IsNullOrEmpty(row["ZCOLSN"].ToString().Trim()) ? "" : row["ZCOLSN"].ToString().Trim();
                        item.ZSATNR = string.IsNullOrEmpty(row["ZSATNR"].ToString().Trim()) ? "" : row["ZSATNR"].ToString().Trim();
                        item.ZSIZTX = string.IsNullOrEmpty(row["ZSIZTX"].ToString().Trim()) ? "" : row["ZSIZTX"].ToString().Trim();
                        item.Handled = string.IsNullOrEmpty(row["Handled"].ToString().Trim()) ? (byte)0 : byte.Parse(row["Handled"].ToString().Trim());
                        item.IsADD = string.IsNullOrEmpty(row["IsADD"].ToString().Trim()) ? (byte)0 : byte.Parse(row["IsADD"].ToString().Trim());
                        item.IsRFID = string.IsNullOrEmpty(row["IsRFID"].ToString().Trim()) ? (byte)0 : byte.Parse(row["IsRFID"].ToString().Trim());
                        item.PICK_TASK_ITEM = string.IsNullOrEmpty(row["PICK_TASK_ITEM"].ToString().Trim()) ? "" : row["PICK_TASK_ITEM"].ToString().Trim();
                        item.CHARG = string.IsNullOrEmpty(row["CHARG"].ToString().Trim()) ? "" : row["CHARG"].ToString().Trim();
                        item.MATNR = string.IsNullOrEmpty(row["MATNR"].ToString().Trim()) ? "" : row["MATNR"].ToString().Trim();
                        item.UOM = string.IsNullOrEmpty(row["UOM"].ToString().Trim()) ? "" : row["UOM"].ToString().Trim();
                        list.Add(item);
                    }
                    return list;
                }

                return null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }


        /// <summary>
        /// 根据下架单号获取相关物料信息
        /// </summary>
        /// <param name="picktask">下架单号</param>
        /// <returns></returns>
        public static List<MaterialInfo> GetMaterialListByPickTask(string picktask)
        {
            try
            {
                /*
                string sql = "P_materialinfo_GetByPickTask ";
                SqlParameter p1 = DBHelper.CreateParameter("@PICK_TASK", picktask);

                DataTable dt = DBHelper.GetTable(sql, true, p1);
                */
                string sql = string.Format("SELECT * FROM dbo.materialinfo WHERE MATNR IN(SELECT PRODUCTNO FROM dbo.InventoryOutLogDetail WHERE PICK_TASK = '{0}' )", picktask);
                DataTable dt = DBHelper.GetTable(sql, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<MaterialInfo> list = new List<MaterialInfo>();
                    foreach (DataRow row in dt.Rows)
                    {
                        MaterialInfo item = new MaterialInfo();
                        item.MATNR = row["MATNR"].CastTo("");
                        item.PXQTY = row["PXQTY"].CastTo(0);
                        item.PXQTY_FH = row["PXQTY_FH"].CastTo(0);
                        item.ZCOLSN = row["ZCOLSN"].CastTo("");
                        item.ZSATNR = row["ZSATNR"].CastTo("");
                        item.ZSIZTX = row["ZSIZTX"].CastTo("");
                        item.ZSUPC2 = row["ZSUPC2"].CastTo("");
                        double brgew = 0;
                        double.TryParse(row["BRGEW"].ToString(), out brgew);
                        item.BRGEW = brgew;
                        item.PUT_STRA = row["PUT_STRA"].CastTo("");
                        item.PXMAT_FH = row["PXMAT_FH"].CastTo("");
                        item.PXMAT = row["PXMAT"].CastTo("");
                        item.MAKTX = row["MAKTX"].CastTo("");

                        list.Add(item);
                    }
                    return list;
                }

                return null;
                

            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 根据下架单号获取相关吊牌信息
        /// </summary>
        /// <param name="picktask">下架单号</param>
        /// <returns></returns>
        public static List<HLATagInfo> GetTagListByPickTask(string picktask)
        {
            try
            {
                /*
                string sql = "P_taginfo_GetByPickTask ";
                SqlParameter p1 = DBHelper.CreateParameter("@PICK_TASK", picktask);

                DataTable dt = DBHelper.GetTable(sql, true, p1);
                */

                string sql = string.Format("SELECT * FROM dbo.taginfo WHERE MATNR IN (SELECT PRODUCTNO FROM dbo.InventoryOutLogDetail WHERE PICK_TASK = '{0}' )", picktask);
                DataTable dt = DBHelper.GetTable(sql, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<HLATagInfo> list = new List<HLATagInfo>();
                    foreach (DataRow row in dt.Rows)
                    {
                        HLATagInfo item = HLATagInfo.BuildHLATagInfo(row);
                        list.Add(item);
                    }
                    return list;
                }

                return null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }


        /// <summary>
        /// 获取最新的版本信息
        /// </summary>
        /// <param name="type">
        /// 1 大通道机交货软件
        /// 2 单检机
        /// 3 挂装机
        /// </param>
        /// <returns></returns>
        public static VersionsInfo GetVersionsInfo(int type)
        {
            try
            {
                string sql = string.Format("SELECT TOP 1 Id,Version,DownloadUrl,Timestamp,UpdateLog FROM dbo.Versions WHERE SoftwareType={0} ORDER BY Timestamp DESC", type);

                DataTable dt = DBHelper.GetTable(sql, false);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    VersionsInfo result = new VersionsInfo();
                    result.Id = string.IsNullOrEmpty(row["Id"].ToString().Trim()) ? 0 : long.Parse(row["Id"].ToString().Trim());
                    result.DownloadUrl = row["DownloadUrl"].ToString().Trim();
                    result.Timestamp = string.IsNullOrEmpty(row["Timestamp"].ToString().Trim()) ? new DateTime(1900, 1, 1) : DateTime.Parse(row["Timestamp"].ToString().Trim());
                    result.UpdateLog = row["UpdateLog"].ToString().Trim();
                    result.Version = row["Version"].ToString().Trim();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
            return null;
        }


        /// <summary>
        /// 保存物料主数据
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static bool SaveMaterialInfo(MaterialInfo m)
        {
            SqlParameter p1 = DBHelper.CreateParameter("@MATNR", m.MATNR);

            //判断产品编码是否存在
            string sql = "SELECT COUNT(*) FROM materialinfo WHERE MATNR = @MATNR";
            int num = DBHelper.GetValue(sql, false, p1).CastTo<int>(-1);
            if (num == 0)
            {
                SqlParameter p2 = DBHelper.CreateParameter("@MATNR", m.MATNR);
                SqlParameter p3 = DBHelper.CreateParameter("@ZSATNR", m.ZSATNR);
                SqlParameter p4 = DBHelper.CreateParameter("@ZCOLSN", m.ZCOLSN);
                SqlParameter p5 = DBHelper.CreateParameter("@ZSIZTX", m.ZSIZTX);
                SqlParameter p6 = DBHelper.CreateParameter("@ZSUPC2", m.ZSUPC2);
                SqlParameter p7 = DBHelper.CreateParameter("@PXQTY", m.PXQTY);
                SqlParameter p8 = DBHelper.CreateParameter("@PXQTY_FH", m.PXQTY_FH);
                SqlParameter p9 = DBHelper.CreateParameter("@BRGEW", m.BRGEW);
                SqlParameter p10 = DBHelper.CreateParameter("@PUT_STRA", m.PUT_STRA);
                SqlParameter p11 = DBHelper.CreateParameter("@ZCOLSN_WFG", m.ZCOLSN_WFG);
                SqlParameter p12 = DBHelper.CreateParameter("@PXMAT_FH", m.PXMAT_FH);
                SqlParameter p13 = DBHelper.CreateParameter("@PXMAT", m.PXMAT);
                SqlParameter p14 = DBHelper.CreateParameter("@MAKTX", m.MAKTX);

                sql = @"INSERT INTO materialinfo (MATNR, ZSATNR, ZCOLSN, ZSIZTX, ZSUPC2, PXQTY, PXQTY_FH,BRGEW,PUT_STRA,ZCOLSN_WFG,PXMAT_FH,PXMAT,MAKTX) 
VALUES (@MATNR, @ZSATNR, @ZCOLSN, @ZSIZTX, @ZSUPC2, @PXQTY,@PXQTY_FH,@BRGEW,@PUT_STRA,@ZCOLSN_WFG,@PXMAT_FH,@PXMAT,@MAKTX)";
                int result = DBHelper.ExecuteSql(sql, false, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11,p12,p13,p14);
                return result > 0 ? true : false;
            }
            else if(num == -1)
            {
                return false;
            }
            else
            {
                SqlParameter p2 = DBHelper.CreateParameter("@MATNR", m.MATNR);
                SqlParameter p3 = DBHelper.CreateParameter("@ZSATNR", m.ZSATNR);
                SqlParameter p4 = DBHelper.CreateParameter("@ZCOLSN", m.ZCOLSN);
                SqlParameter p5 = DBHelper.CreateParameter("@ZSIZTX", m.ZSIZTX);
                SqlParameter p6 = DBHelper.CreateParameter("@ZSUPC2", m.ZSUPC2);
                SqlParameter p7 = DBHelper.CreateParameter("@PXQTY", m.PXQTY);
                SqlParameter p8 = DBHelper.CreateParameter("@PXQTY_FH", m.PXQTY_FH);
                SqlParameter p9 = DBHelper.CreateParameter("@BRGEW", m.BRGEW);
                SqlParameter p10 = DBHelper.CreateParameter("@PUT_STRA", m.PUT_STRA);
                SqlParameter p11 = DBHelper.CreateParameter("@ZCOLSN_WFG", m.ZCOLSN_WFG);
                SqlParameter p12 = DBHelper.CreateParameter("@PXMAT_FH", m.PXMAT_FH);
                SqlParameter p13 = DBHelper.CreateParameter("@PXMAT", m.PXMAT);
                SqlParameter p14 = DBHelper.CreateParameter("@MAKTX", m.MAKTX);

                sql = @"UPDATE materialinfo SET ZSATNR = @ZSATNR, ZCOLSN = @ZCOLSN, ZSIZTX = @ZSIZTX, 
                            ZSUPC2 = @ZSUPC2, PXQTY = @PXQTY,PXQTY_FH=@PXQTY_FH,BRGEW=@BRGEW,Timestamp=GETDATE(),
                            PUT_STRA=@PUT_STRA,ZCOLSN_WFG=@ZCOLSN_WFG,PXMAT_FH=@PXMAT_FH,PXMAT=@PXMAT,MAKTX=@MAKTX 
                        WHERE MATNR = @MATNR";

                int result = DBHelper.ExecuteSql(sql, false, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11,p12,p13,p14);
                return result > 0 ? true : false;
            }
        }

        /// <summary>
        /// 保存吊牌信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static bool SaveTagInfo(HLATagInfo tag)
        {
            SqlParameter p1 = DBHelper.CreateParameter("@MATNR", tag.MATNR);
            SqlParameter p9 = DBHelper.CreateParameter("@BARCD", tag.BARCD);

            //判断主epc是否存在
            string sql = "SELECT COUNT(1) FROM taginfo WHERE MATNR = @MATNR AND BARCD=@BARCD";
            int num = 0;
            try
            {
                num = int.Parse(DBHelper.GetValue(sql, false, p1, p9).ToString());
            }
            catch { }
            if (num <= 0)
            {
                SqlParameter p2 = DBHelper.CreateParameter("@MATNR", tag.MATNR);
                SqlParameter p3 = DBHelper.CreateParameter("@CHARG", tag.CHARG);
                SqlParameter p4 = DBHelper.CreateParameter("@BARCD", tag.BARCD);
                SqlParameter p5 = DBHelper.CreateParameter("@BARCD_ADD", tag.BARCD_ADD);
                SqlParameter p6 = DBHelper.CreateParameter("@RFID_EPC", tag.RFID_EPC);
                SqlParameter p7 = DBHelper.CreateParameter("@RFID_ADD_EPC", tag.RFID_ADD_EPC);
                SqlParameter p8 = DBHelper.CreateParameter("@BARDL", tag.BARDL);
                SqlParameter p10 = DBHelper.CreateParameter("@LIFNR", tag.LIFNR);
                sql = @"INSERT INTO taginfo (MATNR, CHARG, BARCD, BARCD_ADD, RFID_EPC, RFID_ADD_EPC, BARDL,LIFNR) 
                    VALUES (@MATNR, @CHARG, @BARCD, @BARCD_ADD, @RFID_EPC, @RFID_ADD_EPC, @BARDL, @LIFNR)";

                int result = DBHelper.ExecuteSql(sql, false, p2, p3, p4, p5, p6, p7, p8, p10);
                return result > 0 ? true : false;
            }
            else
            {
                SqlParameter p2 = DBHelper.CreateParameter("@MATNR", tag.MATNR);
                SqlParameter p3 = DBHelper.CreateParameter("@CHARG", tag.CHARG);
                SqlParameter p4 = DBHelper.CreateParameter("@BARCD", tag.BARCD);
                SqlParameter p5 = DBHelper.CreateParameter("@BARCD_ADD", tag.BARCD_ADD);
                SqlParameter p6 = DBHelper.CreateParameter("@RFID_EPC", tag.RFID_EPC);
                SqlParameter p7 = DBHelper.CreateParameter("@RFID_ADD_EPC", tag.RFID_ADD_EPC);
                SqlParameter p8 = DBHelper.CreateParameter("@BARDL", tag.BARDL);
                SqlParameter p10 = DBHelper.CreateParameter("@LIFNR", tag.LIFNR);
                sql = @"UPDATE taginfo SET MATNR = @MATNR, CHARG = @CHARG, BARCD = @BARCD, BARCD_ADD = @BARCD_ADD, 
                            RFID_EPC=@RFID_EPC,RFID_ADD_EPC = @RFID_ADD_EPC, BARDL = @BARDL,Timestamp=GETDATE(),LIFNR=@LIFNR
                        WHERE  MATNR = @MATNR AND BARCD=@BARCD";

                int result = DBHelper.ExecuteSql(sql, false, p2, p3, p4, p5, p6, p7, p8, p10);
                return result > 0 ? true : false;
            }
        }

        /// <summary>
        /// 获取未处理的epc明细列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, List<string>> GetUnhandledEpcDetails(ReceiveType type)
        {
            string sql = string.Format(@"SELECT LGNUM, DOCNO, DOCCAT, HU, EPC_SER FROM epcdetail{0} 
WHERE (Handled IS NULL OR Handled != 1) AND Result = 'S'", type == ReceiveType.交接单收货 ? "_dema" : "");
            DataTable table = DBHelper.GetTable(sql, false);
            if (table != null && table.Rows.Count > 0)
            {
                Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
                foreach (DataRow row in table.Rows)
                {
                    string key = string.Format("{0},{1},{2},{3}", row["LGNUM"], row["DOCNO"], row["DOCCAT"], row["HU"]);
                    if (dic.ContainsKey(key))
                    {
                        List<string> list = dic[key];
                        list.Add(row["EPC_SER"].ToString());
                    }
                    else
                    {
                        List<string> list = new List<string>();
                        list.Add(row["EPC_SER"].ToString());
                        dic.Add(key, list);
                    }
                }

                return dic;
            }

            return null;
        }

        /// <summary>
        /// 设置指定epc明细为已处理
        /// </summary>
        /// <param name="lgnum"></param>
        /// <param name="docno"></param>
        /// <param name="doccat"></param>
        /// <param name="hu"></param>
        /// <returns></returns>
        public static bool SetEpcDetailsHandled(string lgnum, string docno, string doccat, string hu, ReceiveType type)
        {
            SqlParameter p1 = DBHelper.CreateParameter("@LGNUM", lgnum);
            SqlParameter p2 = DBHelper.CreateParameter("@DOCNO", docno);
            SqlParameter p3 = DBHelper.CreateParameter("@DOCCAT", doccat);
            SqlParameter p4 = DBHelper.CreateParameter("@HU", hu);
            string sql = string.Format(@"UPDATE epcdetail{0} SET Handled = 1,[Timestamp] = GETDATE()
WHERE LGNUM = @LGNUM AND DOCNO = @DOCNO AND DOCCAT = @DOCCAT AND HU = @HU", type == ReceiveType.交接单收货 ? "_dema" : "");
            int result = DBHelper.ExecuteSql(sql, false, p1, p2, p3, p4);

            return result > 0 ? true : false;
        }

        /// <summary>
        /// 获取物料主数据列表
        /// </summary>
        /// <returns></returns>
        public static List<MaterialInfo> GetMaterialInfoList()
        {
            //string sql = "SELECT MATNR, ZSATNR, ZCOLSN, ZSIZTX, ZSUPC2, PXQTY FROM materialinfo";
            string sql = "SELECT * FROM materialinfo WHERE MATNR IN (SELECT MATNR FROM taginfo WHERE ISNULL(RFID_EPC, '')!='')";
            DataTable table = DBHelper.GetTable(sql, false);

            if (table != null && table.Rows.Count > 0)
            {
                List<MaterialInfo> list = new List<MaterialInfo>();
                foreach (DataRow row in table.Rows)
                {
                    MaterialInfo item = new MaterialInfo();
                    item.MATNR = row["MATNR"].CastTo("");
                    item.ZSATNR = row["ZSATNR"].CastTo("");
                    item.ZCOLSN = row["ZCOLSN"].CastTo("");
                    item.ZSIZTX = row["ZSIZTX"].CastTo("");
                    item.ZSUPC2 = row["ZSUPC2"].CastTo("");
                    item.PUT_STRA = row["PUT_STRA"].CastTo("");
                    item.ZCOLSN_WFG = row["ZCOLSN_WFG"].CastTo("");
                    item.PXMAT_FH = row["PXMAT_FH"].CastTo("");
                    item.PXMAT = row["PXMAT"].CastTo("");
                    item.PXQTY = row["PXQTY"].CastTo(0);
                    item.BRGEW = row["BRGEW"].CastTo(0);
                    item.PXQTY_FH = row["PXQTY_FH"].CastTo(0);
                    item.MAKTX = row["MAKTX"].CastTo("");
                    list.Add(item);
                }
                return list;
            }

            return null;
        }

        public static Dictionary<string, MaterialInfo> GetMaterialInfoDic()
        {
            string sql = "SELECT * FROM materialinfo WHERE MATNR IN (SELECT MATNR FROM taginfo WHERE ISNULL(RFID_EPC, '')!='')";
            DataTable table = DBHelper.GetTable(sql, false);

            if (table != null && table.Rows.Count > 0)
            {
                Dictionary<string, MaterialInfo> list = new Dictionary<string, MaterialInfo>();
                foreach (DataRow row in table.Rows)
                {
                    MaterialInfo item = new MaterialInfo();
                    item.MATNR = row["MATNR"].CastTo("");
                    item.ZSATNR = row["ZSATNR"].CastTo("");
                    item.ZCOLSN = row["ZCOLSN"].CastTo("");
                    item.ZSIZTX = row["ZSIZTX"].CastTo("");
                    item.ZSUPC2 = row["ZSUPC2"].CastTo("");
                    item.PUT_STRA = row["PUT_STRA"].CastTo("");
                    item.ZCOLSN_WFG = row["ZCOLSN_WFG"].CastTo("");
                    item.PXMAT_FH = row["PXMAT_FH"].CastTo("");
                    item.PXMAT = row["PXMAT"].CastTo("");
                    item.PXQTY = row["PXQTY"].CastTo(0);
                    double brgew = 0;
                    double.TryParse(row["BRGEW"].ToString(), out brgew);
                    item.BRGEW = brgew;
                    item.PXQTY_FH = row["PXQTY_FH"].CastTo(0);
                    item.MAKTX = row["MAKTX"].CastTo("");
                    if (!list.ContainsKey(item.MATNR))
                        list.Add(item.MATNR, item);
                }
                return list;
            }

            return null;
        }

        /// <summary>
        /// 获取所有RFID类型的吊牌数据
        /// </summary>
        /// <returns></returns>
        public static List<HLATagInfo> GetAllRfidHlaTagList()
        {
            string sql = "SELECT * FROM dbo.taginfo WHERE ISNULL(RFID_EPC,'')!=''";
            DataTable table = DBHelper.GetTable(sql, false);

            if (table != null && table.Rows.Count > 0)
            {
                List<HLATagInfo> list = new List<HLATagInfo>();
                foreach (DataRow row in table.Rows)
                {
                    HLATagInfo item = HLATagInfo.BuildHLATagInfo(row);
                    list.Add(item);
                }
                return list;
            }

            return null;
        }


        public static HLATagInfo GetTagInfoByMatnr(string matnr)
        {
            string sql = string.Format("select top 1 * from taginfo where MATNR = '{0}'", matnr);
            DataTable table = DBHelper.GetTable(sql, false);
            HLATagInfo result = null;
            if (table != null && table.Rows.Count > 0)
            {
                foreach(DataRow row in table.Rows)
                {
                    result = HLATagInfo.BuildHLATagInfo(row);
                }
            }
            return result;
        }

        public static Dictionary<string, HLATagInfo> GetAllRfidHlaTagDic()
        {
            string sql = "SELECT * FROM dbo.taginfo WHERE ISNULL(RFID_EPC,'')!=''";
            DataTable table = DBHelper.GetTable(sql, false);

            if (table != null && table.Rows.Count > 0)
            {
                Dictionary<string, HLATagInfo> list = new Dictionary<string, HLATagInfo>();
                foreach (DataRow row in table.Rows)
                {
                    HLATagInfo item = HLATagInfo.BuildHLATagInfo(row);
                    if(!list.ContainsKey(item.RFID_ADD_EPC))
                        list.Add(item.RFID_ADD_EPC, item);
                    if (!list.ContainsKey(item.RFID_EPC))
                        list.Add(item.RFID_EPC, item);
                }
                return list;
            }

            return null;
        }

        /// <summary>
        /// 根据epc获取标签详细信息
        /// </summary>
        /// <param name="epc"></param>
        /// <returns></returns>
        public static TagDetailInfo GetTagDetailInfoByEpc(string epc)
        {
            if (string.IsNullOrEmpty(epc) || epc.Length < 20)
                return null;

            string rfidEpc = epc.Substring(0, 14) + "000000";
            string rfidAddEpc = rfidEpc.Substring(0, 14);
            SqlParameter p1 = DBHelper.CreateParameter("@RFID_EPC", rfidEpc);
            SqlParameter p2 = DBHelper.CreateParameter("@RFID_ADD_EPC", rfidAddEpc);

            string sql = "SELECT ti.RFID_EPC, ti.RFID_ADD_EPC, ti.CHARG, ti.BARCD, mi.MATNR, mi.ZSATNR, mi.ZCOLSN, mi.ZSIZTX, mi.PXQTY,mi.PXQTY_FH FROM taginfo as ti, materialinfo as mi WHERE (ti.RFID_EPC = @RFID_EPC OR ti.RFID_ADD_EPC = @RFID_ADD_EPC) AND ti.MATNR = mi.MATNR";
            DataTable table = DBHelper.GetTable(sql, false, p1, p2);

            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                TagDetailInfo item = new TagDetailInfo();
                item.RFID_EPC = row["RFID_EPC"].ToString();
                item.RFID_ADD_EPC = row["RFID_ADD_EPC"].ToString();
                item.CHARG = row["CHARG"].ToString();
                item.MATNR = row["MATNR"].ToString();
                item.BARCD = row["BARCD"].ToString();
                item.ZSATNR = row["ZSATNR"].ToString();
                item.ZCOLSN = row["ZCOLSN"].ToString();
                item.ZSIZTX = row["ZSIZTX"].ToString();
                item.PXQTY = int.Parse(row["PXQTY"].ToString());
                item.PXQTY_FH = int.Parse(row["PXQTY_FH"].ToString());

                //判断是否为辅条码epc
                if (rfidEpc == item.RFID_EPC)
                    item.IsAddEpc = false;
                else
                    item.IsAddEpc = true;

                return item;
            }
            return null;
        }

        /// <summary>
        /// 保存epc明细
        /// </summary>
        /// <param name="epclist"></param>
        /// <returns></returns>
        public static bool SaveEpcDetail(bool inventoryResult, string lgnum, string docno, string doccat, string hu, List<string> epclist, ReceiveType type)
        {
            try
            {
                if(SysConfig.IsTest)
                {
                    return true;
                }
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("INSERT INTO epcdetail{0}(LGNUM, DOCNO, DOCCAT, HU, EPC_SER, Result, Handled,[Timestamp],Floor) VALUES ", type == ReceiveType.交接单收货 ? "_dema" : "");
                //string sql = "INSERT INTO epcdetail(LGNUM, DOCNO, DOCCAT, HU, EPC_SER, Result, Handled,[Timestamp],Floor) VALUES (@LGNUM, @DOCNO, @DOCCAT, @HU, @EPC_SER, @Result, 0, GETDATE(),@Floor)";
                if (epclist != null && epclist.Count > 0)
                {
                    foreach (string epc in epclist)
                    {
                        sbSql.AppendFormat("('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', 0, GETDATE(),'{6}'),", lgnum, docno, doccat, hu, epc, inventoryResult ? "S" : "E", SysConfig.Floor);
                        //SqlParameter p1 = DBHelper.CreateParameter("@LGNUM", lgnum);
                        //SqlParameter p2 = DBHelper.CreateParameter("@DOCNO", docno);
                        //SqlParameter p3 = DBHelper.CreateParameter("@DOCCAT", doccat);
                        //SqlParameter p4 = DBHelper.CreateParameter("@HU", hu);
                        //SqlParameter p5 = DBHelper.CreateParameter("@Result", inventoryResult ? "S" : "E");
                        //SqlParameter p6 = DBHelper.CreateParameter("@EPC_SER", epc);
                        //SqlParameter p7 = DBHelper.CreateParameter("@Floor", SysConfig.Floor);
                        //int result = DBHelper.ExecuteSql(sql, p1, p2, p3, p4, p5, p6, p7);
                    }

                    if (sbSql.ToString().EndsWith(","))
                        sbSql.Remove(sbSql.Length - 1, 1);

                    int result = DBHelper.ExecuteSql(sbSql.ToString(), false);
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
            }
            return true;

        }

        /// <summary>
        /// 删除epc明细
        /// </summary>
        /// <param name="lgnum"></param>
        /// <param name="docno"></param>
        /// <param name="doccat"></param>
        /// <param name="hu"></param>
        /// <returns></returns>
        public static bool DeleteEpcDetail(string docno, string hu, ReceiveType type)
        {
            SqlParameter p1 = DBHelper.CreateParameter("@DOCNO", docno);
            SqlParameter p2 = DBHelper.CreateParameter("@HU", hu);
            string sql = string.Format(@"DELETE FROM epcdetail{0} WHERE DOCNO = @DOCNO AND HU = @HU", type == ReceiveType.交接单收货 ? "_dema" : "");
            int result = DBHelper.ExecuteSql(sql, false, p1, p2);

            return result > 0 ? true : false;
        }

        /// <summary>
        /// 获取系统信息字段值
        /// </summary>
        /// <param name="fieldname"></param>
        /// <returns></returns>
        public static string GetSysInfoFieldValue(string fieldname)
        {
            SqlParameter p1 = DBHelper.CreateParameter("@FieldName", fieldname);
            string sql = "SELECT FieldValue FROM sysinfo WHERE FieldName = @FieldName";

            object value = DBHelper.GetValue(sql, false, p1);

            return value != null ? value.ToString() : null;
        }

        public static void GetGhostAndTrigger(out int ghost, out int trigger, out int r6ghost)
        {
            /*
            try
            {
                string sql = @"select FieldName,FieldValue from sysinfo where FieldName IN ('GHOST','TRIGGER','R6GHOST')";
                r6ghost = 0;
                ghost = 0;
                trigger = 270;
                DataTable dt = DBHelper.GetTable(sql, false);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["FieldName"] != null)
                        {
                            if (row["FieldName"].ToString() == "GHOST")
                            {
                                ghost = row["FieldValue"] != null ? int.Parse(row["FieldValue"].ToString()) : 0;
                            }
                            else if (row["FieldName"].ToString() == "TRIGGER")
                            {
                                trigger = row["FieldValue"] != null ? int.Parse(row["FieldValue"].ToString()) : new Random(DateTime.Now.Millisecond).Next(200, 290);
                            }
                            else if (row["FieldName"].ToString() == "R6GHOST")
                            {
                                r6ghost = row["FieldValue"] != null ? int.Parse(row["FieldValue"].ToString()) : 0;
                            }
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
                ghost = 3;
                trigger = 280;
                r6ghost = 3;
            }
            */
            ghost = 0;
            trigger = 0;
            r6ghost = 0;
        }

        /// <summary>
        /// 保存系统信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SaveSysInfo(string fieldname, string fieldvalue)
        {
            SqlParameter p1 = DBHelper.CreateParameter("@FieldName", fieldname);
            string sql = "SELECT COUNT(*) FROM sysinfo WHERE FieldName = @FieldName";
            int num = DBHelper.GetValue(sql, false, p1).CastTo<int>(0);

            if (num > 0)
            {
                SqlParameter p2 = DBHelper.CreateParameter("@FieldName", fieldname);
                SqlParameter p3 = DBHelper.CreateParameter("@FieldValue", fieldvalue);
                sql = "UPDATE sysinfo SET FieldValue = @FieldValue WHERE FieldName = @FieldName";
                int result = DBHelper.ExecuteSql(sql, false, p2, p3);

                return result > 0 ? true : false;
            }
            else
            {
                SqlParameter p2 = DBHelper.CreateParameter("@FieldName", fieldname);
                SqlParameter p3 = DBHelper.CreateParameter("@FieldValue", fieldvalue);
                sql = "INSERT sysinfo (FieldName, FieldValue) VALUES (@FieldName, @FieldValue)";
                int result = DBHelper.ExecuteSql(sql, false, p2, p3);

                return result > 0 ? true : false;
            }
        }

        /// <summary>
        /// 根据EPC列表查找EPC明细
        /// </summary>
        /// <param name="epc"></param>
        /// <returns></returns>
        public static EpcDetail GetEpcDetailByEpcList(List<string> epc, ReceiveType type)
        {
            string epcString = "";
            foreach (string item in epc)
            {
                epcString += "'" + item + "',";
            }
            if (epcString.Length > 0)
            {
                epcString = epcString.Substring(0, epcString.Length - 1);
            }

            string sql = string.Format(@"SELECT TOP 1 LGNUM, DOCNO, DOCCAT, HU, EPC_SER, Handled 
FROM epcdetail{0} WHERE EPC_SER IN ({1})", type == ReceiveType.交接单收货 ? "_dema" : "", epcString);
            DataTable table = DBHelper.GetTable(sql, false);

            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                EpcDetail item = new EpcDetail();
                item.LGNUM = row["LGNUM"].ToString();
                item.DOCNO = row["DOCNO"].ToString();
                item.DOCCAT = row["DOCCAT"].ToString();
                item.HU = row["HU"].ToString();
                item.EPC_SER = row["EPC_SER"].ToString();
                item.Handled = int.Parse(row["Handled"].ToString());

                return item;
            }

            return null;
        }

        /// <summary>
        /// 从epc上传列表中获取epc明细信息
        /// </summary>
        /// <param name="epc"></param>
        /// <returns></returns>
        public static EpcDetail GetEpcDetailByEpc(string epc, ReceiveType type)
        {
            SqlParameter p1 = DBHelper.CreateParameter("@EPC_SER", epc);
            string sql = string.Format(@"SELECT LGNUM, DOCNO, DOCCAT, HU, EPC_SER, Handled 
FROM epcdetail{0} WHERE EPC_SER = @EPC_SER", type == ReceiveType.交接单收货 ? "_dema" : "");
            DataTable table = DBHelper.GetTable(sql, false, p1);

            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                EpcDetail item = new EpcDetail();
                item.LGNUM = row["LGNUM"].ToString();
                item.DOCNO = row["DOCNO"].ToString();
                item.DOCCAT = row["DOCCAT"].ToString();
                item.HU = row["HU"].ToString();
                item.EPC_SER = row["EPC_SER"].ToString();
                item.Handled = int.Parse(row["Handled"].ToString());

                return item;
            }

            return null;
        }

        /// <summary>
        /// 根据箱号获取盘点结果
        /// </summary>
        /// <param name="lgnum"></param>
        /// <param name="boxNo"></param>
        /// <returns></returns>
        public static string GetInventoryResultByBoxNo(string lgnum, string boxNo, ReceiveType type)
        {
            object value = null;
            if (boxNo.Trim() != "")
            {
                SqlParameter p1 = DBHelper.CreateParameter("@LGNUM", lgnum);
                SqlParameter p2 = DBHelper.CreateParameter("@HU", boxNo);
                string sql = string.Format("SELECT Result FROM hulist{0} WHERE LGNUM = @LGNUM AND HU = @HU",
                    type == ReceiveType.交接单收货 ? "_dema" : "");

                value = DBHelper.GetValue(sql, false, p1, p2);

            }

            return value != null ? value.ToString() : null;
        }

        /// <summary>
        /// 根据仓库编号获取所有箱子信息
        /// </summary>
        /// <param name="lgnum"></param>
        /// <returns></returns>
        public static List<HuInfo> GetHuInfoListByLGNUM(string lgnum, DateTime lastUpdateTime, ReceiveType type)
        {
            string sql = string.Format("SELECT HU,QTY,Result,Timestamp FROM hulist{2} WHERE LGNUM = '{0}' AND Timestamp >= '{1}'",
                lgnum, lastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss"), type == ReceiveType.交接单收货 ? "_dema" : "");
            DataTable table = DBHelper.GetTable(sql, false);
            List<HuInfo> result = new List<HuInfo>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    HuInfo hu = new HuInfo();
                    //hu.Floor = row["Floor"].ToString().Trim() != "" ? row["Floor"].ToString() : "";
                    hu.HU = row["HU"].ToString().Trim() != "" ? row["HU"].ToString() : "";
                    //hu.Id = row["Id"].ToString().Trim() != "" ? long.Parse(row["Id"].ToString()) : 0;
                    //hu.LGNUM = row["LGNUM"].ToString().Trim() != "" ? row["LGNUM"].ToString() : "";
                    hu.QTY = row["QTY"].ToString().Trim() != "" ? int.Parse(row["QTY"].ToString()) : 0;
                    hu.Result = row["Result"].ToString().Trim() != "" ? row["Result"].ToString() : "";
                    hu.Timestamp = row["Timestamp"].ToString().Trim() != "" ? DateTime.Parse(row["Timestamp"].ToString()) : new DateTime(1900, 1, 1);
                    result.Add(hu);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取所有箱信息的总数
        /// </summary>
        /// <param name="lgnum"></param>
        /// <param name="lastUpdateTime"></param>
        /// <returns></returns>
        public static int GetHuInfoTotalCount(string lgnum, DateTime lastUpdateTime, ReceiveType type)
        {
            return (int)DBHelper.GetValue(
                string.Format("select COUNT(1) from hulist{2} where LGNUM = '{0}' AND [Timestamp] > = '{1}'",
                lgnum, lastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss"), type == ReceiveType.交接单收货 ? "_dema" : ""), false);
        }

        /// <summary>
        /// 分页获取历史收货数据
        /// </summary>
        /// <param name="lgnum"></param>
        /// <param name="lastUpdateTime"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static List<HuInfo> GetHuInfoListByPage(string lgnum, DateTime lastUpdateTime, int page, int rows, ReceiveType type)
        {
            string sql = string.Format(@"select HU,QTY,Result,[Timestamp] from 
(SELECT ROW_NUMBER() over(order by Id) as NumIndex,HU,QTY,Result,[Timestamp] 
FROM hulist{4} WHERE LGNUM = '{0}' AND Timestamp >= '{1}') a 
where a.NumIndex between {2} and {3}"
, lgnum, lastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss"), (page - 1) * rows + 1,
page * rows, type == ReceiveType.交接单收货 ? "_dema" : "");
            DataTable table = DBHelper.GetTable(sql, false);
            List<HuInfo> result = new List<HuInfo>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    HuInfo hu = new HuInfo();
                    //hu.Floor = row["Floor"].ToString().Trim() != "" ? row["Floor"].ToString() : "";
                    hu.HU = row["HU"].ToString().Trim() != "" ? row["HU"].ToString() : "";
                    //hu.Id = row["Id"].ToString().Trim() != "" ? long.Parse(row["Id"].ToString()) : 0;
                    //hu.LGNUM = row["LGNUM"].ToString().Trim() != "" ? row["LGNUM"].ToString() : "";
                    hu.QTY = row["QTY"].ToString().Trim() != "" ? int.Parse(row["QTY"].ToString()) : 0;
                    hu.Result = row["Result"].ToString().Trim() != "" ? row["Result"].ToString() : "";
                    hu.Timestamp = row["Timestamp"].ToString().Trim() != "" ? DateTime.Parse(row["Timestamp"].ToString()) : new DateTime(1900, 1, 1);
                    result.Add(hu);
                }
            }
            return result;
        }

        /// <summary>
        /// 根据箱码获取箱码信息
        /// </summary>
        /// <param name="hu"></param>
        /// <returns></returns>
        public static HuInfo GetHuInfoByHU(string hu, string lgnum, ReceiveType type)
        {
            string sql = string.Format("SELECT TOP 1 Id,LGNUM,HU,QTY,Timestamp,Floor,Result FROM hulist{2} WHERE LGNUM = '{0}' AND HU = '{1}'", lgnum, hu, type == ReceiveType.交接单收货 ? "_dema" : "");
            DataTable table = DBHelper.GetTable(sql, false);
            HuInfo result = null;
            if (table != null && table.Rows.Count > 0)
            {
                result = new HuInfo();
                DataRow row = table.Rows[0];
                result.Floor = row["Floor"].ToString().Trim() != "" ? row["Floor"].ToString() : "";
                result.HU = row["HU"].ToString().Trim() != "" ? row["HU"].ToString() : "";
                result.Id = row["Id"].ToString().Trim() != "" ? long.Parse(row["Id"].ToString()) : 0;
                result.LGNUM = row["LGNUM"].ToString().Trim() != "" ? row["LGNUM"].ToString() : "";
                result.QTY = row["QTY"].ToString().Trim() != "" ? int.Parse(row["QTY"].ToString()) : 0;
                result.Result = row["Result"].ToString().Trim() != "" ? row["Result"].ToString() : "";
                result.Timestamp = row["Timestamp"].ToString().Trim() != "" ? DateTime.Parse(row["Timestamp"].ToString()) : new DateTime(1900, 1, 1);
            }
            return result;
        }
        /// <summary>
        /// 根据箱码删除相关信息 包括EPC明细 错误记录 和 箱码信息
        /// </summary>
        /// <param name="hu"></param>
        /// <returns></returns>
        public static bool DeleteAboutHuInfo(string hu, ReceiveType type)
        {
            string sql = string.Format("DELETE FROM dbo.epcdetail{1} WHERE HU={0} DELETE FROM dbo.ErrorRecord{1} WHERE HU={0} DELETE FROM dbo.hulist{1} WHERE HU={0}", hu, type == ReceiveType.交接单收货 ? "_dema" : "");
            int count = DBHelper.ExecuteSql(sql, false);
            return true;
        }

        /// <summary>
        /// 获取所有已扫描过的结果为S的EPC明细 
        /// </summary>
        /// <returns></returns>
        public static List<EpcDetail> GetAllRightEpcDetail(ReceiveType type)
        {
            string sql = string.Format(@"SELECT DISTINCT LGNUM ,DOCNO ,DOCCAT ,HU ,EPC_SER , Handled ,Result 
FROM dbo.epcdetail{0} WHERE Result='S'", type == ReceiveType.交接单收货 ? "_dema" : "");
            DataTable table = DBHelper.GetTable(sql, false);
            List<EpcDetail> result = null;
            if (table != null && table.Rows.Count > 0)
            {
                result = new List<EpcDetail>();
                foreach (DataRow row in table.Rows)
                {
                    EpcDetail item = new EpcDetail();
                    item.LGNUM = row["LGNUM"].ToString();
                    item.DOCNO = row["DOCNO"].ToString();
                    item.DOCCAT = row["DOCCAT"].ToString();
                    item.HU = row["HU"].ToString();
                    item.EPC_SER = row["EPC_SER"].ToString();
                    item.Result = row["Result"].ToString();
                    item.Handled = row["Handled"].ToString() == "1" ? 1 : 0;
                    result.Add(item);
                }
            }
            return result;
        }


        public static List<EpcDetail> GetEpcDetailListInfoByDocno(string docno, ReceiveType type)
        {
            string sql = string.Format("SELECT HU,EPC_SER,Result FROM dbo.epcdetail{1} WHERE DOCNO='{0}' AND Result='S'",
                docno,
                type == ReceiveType.交接单收货 ? "_dema" : "");
            DataTable table = DBHelper.GetTable(sql, false);
            List<EpcDetail> result = null;
            if (table != null && table.Rows.Count > 0)
            {
                result = new List<EpcDetail>();
                foreach (DataRow row in table.Rows)
                {
                    EpcDetail item = new EpcDetail();
                    //item.LGNUM = row["LGNUM"].ToString();
                    //item.DOCNO = row["DOCNO"].ToString();
                    //item.DOCCAT = row["DOCCAT"].ToString();
                    item.HU = row["HU"].ToString();
                    item.EPC_SER = row["EPC_SER"].ToString();
                    item.Result = row["Result"].ToString();
                    //item.Handled = row["Handled"].ToString() == "1" ? 1 : 0;
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 根据品 色 获取所有相关的EPC明细 
        /// </summary>
        /// <param name="ZSATNR">品号</param>
        /// <param name="ZCOLSN">色号</param>
        /// <returns></returns>
        public static List<EpcDetail> GetEpcDetailListInfoByPinSeGui(string ZSATNR, string ZCOLSN, string ZSIZTX, ReceiveType type)
        {
            string sql = string.Format("SELECT HU,EPC_SER,Result FROM dbo.epcdetail{3} WHERE DOCNO IN (SELECT DOCNO FROM dbo.docdetail{3} WHERE ZSATNR = '{0}' AND ZCOLSN='{1}' AND ZSIZTX='{2}') AND Result='S'",
                ZSATNR, ZCOLSN, ZSIZTX, type == ReceiveType.交接单收货 ? "_dema" : "");
            DataTable table = DBHelper.GetTable(sql, false);
            List<EpcDetail> result = null;
            if (table != null && table.Rows.Count > 0)
            {
                result = new List<EpcDetail>();
                foreach (DataRow row in table.Rows)
                {
                    EpcDetail item = new EpcDetail();
                    //item.LGNUM = row["LGNUM"].ToString();
                    //item.DOCNO = row["DOCNO"].ToString();
                    //item.DOCCAT = row["DOCCAT"].ToString();
                    item.HU = row["HU"].ToString();
                    item.EPC_SER = row["EPC_SER"].ToString();
                    item.Result = row["Result"].ToString();
                    //item.Handled = row["Handled"].ToString() == "1" ? 1 : 0;
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 根据箱码获取EPC明细
        /// </summary>
        /// <param name="boxNo"></param>
        /// <returns></returns>
        public static List<EpcDetail> GetEpcDetailListInfoByBoxNo(string boxNo, ReceiveType type)
        {
            SqlParameter p1 = DBHelper.CreateParameter("@HU", boxNo);
            string sql = string.Format(@"SELECT DISTINCT LGNUM,DOCNO,DOCCAT,HU,EPC_SER,Handled 
FROM dbo.epcdetail{0} WHERE HU=@HU AND Result='S'", type == ReceiveType.交接单收货 ? "_dema" : "");
            DataTable table = DBHelper.GetTable(sql, false, p1);
            List<EpcDetail> result = null;
            if (table != null && table.Rows.Count > 0)
            {
                result = new List<EpcDetail>();
                foreach (DataRow row in table.Rows)
                {
                    EpcDetail item = new EpcDetail();
                    item.LGNUM = row["LGNUM"].ToString();
                    item.DOCNO = row["DOCNO"].ToString();
                    item.DOCCAT = row["DOCCAT"].ToString();
                    item.HU = row["HU"].ToString();
                    item.EPC_SER = row["EPC_SER"].ToString();
                    item.Handled = row["Handled"].ToString() == "1" ? 1 : 0;
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 保存盘点结果
        /// </summary>
        /// <param name="lgnum"></param>
        /// <param name="boxNo"></param>
        /// <param name="inventoryResult"></param>
        /// <returns></returns>
        public static bool SaveInventoryResult(string lgnum, string boxNo, bool inventoryResult, int qty, ReceiveType type)
        {
            try
            {
                if (SysConfig.IsTest)
                    return true;

                SqlParameter p1 = DBHelper.CreateParameter("@LGNUM", lgnum);
                SqlParameter p2 = DBHelper.CreateParameter("@HU", boxNo);
                string sql = string.Format("SELECT COUNT(*) FROM hulist{0} WHERE LGNUM = @LGNUM AND HU = @HU", type == ReceiveType.交接单收货 ? "_dema" : "");
                object reuslttemp = DBHelper.GetValue(sql, false, p1, p2);

                int num = reuslttemp == null ? 0 : int.Parse(reuslttemp.ToString());

                if (num > 0)
                {
                    SqlParameter p3 = DBHelper.CreateParameter("@LGNUM", lgnum);
                    SqlParameter p4 = DBHelper.CreateParameter("@HU", boxNo);
                    SqlParameter p5 = DBHelper.CreateParameter("@Result", inventoryResult ? "S" : "E");
                    SqlParameter p6 = DBHelper.CreateParameter("@Floor", SysConfig.Floor);
                    SqlParameter p7 = DBHelper.CreateParameter("@QTY", qty);
                    sql = string.Format("UPDATE hulist{0} SET Result = @Result,[Timestamp] = GETDATE(),Floor = @Floor,QTY = @QTY WHERE LGNUM = @LGNUM AND HU = @HU", type == ReceiveType.交接单收货 ? "_dema" : "");
                    int result = DBHelper.ExecuteSql(sql, false, p3, p4, p5, p6, p7);

                    return result > 0 ? true : false;
                }
                else
                {
                    SqlParameter p3 = DBHelper.CreateParameter("@LGNUM", lgnum);
                    SqlParameter p4 = DBHelper.CreateParameter("@HU", boxNo);
                    SqlParameter p5 = DBHelper.CreateParameter("@Result", inventoryResult ? "S" : "E");
                    SqlParameter p6 = DBHelper.CreateParameter("@Floor", SysConfig.Floor);
                    SqlParameter p7 = DBHelper.CreateParameter("@QTY", qty);
                    sql = string.Format("INSERT hulist{0} (LGNUM, HU, Result,[Timestamp],Floor,QTY) VALUES (@LGNUM, @HU, @Result,GETDATE(),@Floor,@QTY)", type == ReceiveType.交接单收货 ? "_dema" : "");
                    int result = DBHelper.ExecuteSql(sql, false, p3, p4, p5, p6, p7);

                    return result > 0 ? true : false;
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
            return false;
        }

        public static bool hasExistHu(string lgnum, string boxNo, ReceiveType type)
        {
            SqlParameter p1 = DBHelper.CreateParameter("@LGNUM", lgnum);
            SqlParameter p2 = DBHelper.CreateParameter("@HU", boxNo);
            string sql = string.Format("SELECT COUNT(*) FROM hulist{0} WHERE LGNUM = @LGNUM AND HU = @HU AND Result='S'", type == ReceiveType.交接单收货 ? "_dema" : "");
            object reuslttemp = DBHelper.GetValue(sql, false, p1, p2);

            int num = reuslttemp == null ? 0 : int.Parse(reuslttemp.ToString());

            return num > 0 ? true : false;

        }
        /// <summary>
        /// 保存交货单明细记录
        /// </summary>
        /// <param name="docNo">交货单号</param>
        /// <param name="itemNo">行项目号</param>
        /// <param name="zsatnr">品号</param>
        /// <param name="zcolsn">色号</param>
        /// <param name="zsiztx">规格</param>
        /// <param name="zcharg">原始批次</param>
        /// <param name="qty">应收数量</param>
        /// <param name="realQty">实收数量</param>
        /// <param name="zpbno">配比类型</param>
        /// <returns></returns>
        public static bool SaveDocDetail(string docNo, string itemNo, string zsatnr,
            string zcolsn, string zsiztx, string zcharg, int qty, int realQty,
            int boxCount, ReceiveType type, string zpbno)
        {
            SqlParameter p1 = DBHelper.CreateParameter("@DOCNO", docNo);
            //SqlParameter p2 = DBHelper.CreateParameter("@ITEMNO", itemNo);
            SqlParameter p3 = DBHelper.CreateParameter("@ZSATNR", zsatnr);
            SqlParameter p4 = DBHelper.CreateParameter("@ZCOLSN", zcolsn);
            SqlParameter p5 = DBHelper.CreateParameter("@ZSIZTX", zsiztx);
            //SqlParameter p6 = DBHelper.CreateParameter("@ZCHARG", zcharg);
            string sql = string.Format(@"SELECT COUNT(*) FROM docdetail{0} 
WHERE DOCNO = @DOCNO AND ZSATNR = @ZSATNR 
AND ZCOLSN = @ZCOLSN AND ZSIZTX = @ZSIZTX", type == ReceiveType.交接单收货 ? "_dema" : "");
            int num = int.Parse(DBHelper.GetValue(sql, false, p1, p3, p4, p5).ToString());

            if (num > 0)
            {
                SqlParameter p7 = DBHelper.CreateParameter("@DOCNO", docNo);
                //SqlParameter p8 = DBHelper.CreateParameter("@ITEMNO", itemNo);
                SqlParameter p9 = DBHelper.CreateParameter("@ZSATNR", zsatnr);
                SqlParameter p10 = DBHelper.CreateParameter("@ZCOLSN", zcolsn);
                SqlParameter p11 = DBHelper.CreateParameter("@ZSIZTX", zsiztx);
                //SqlParameter p12 = DBHelper.CreateParameter("@ZCHARG", zcharg);
                SqlParameter p13 = DBHelper.CreateParameter("@QTY", qty);
                SqlParameter p14 = DBHelper.CreateParameter("@REALQTY", realQty);
                SqlParameter p15 = DBHelper.CreateParameter("@BOXCOUNT", boxCount);
                SqlParameter p16 = DBHelper.CreateParameter("@Floor", SysConfig.Floor);
                sql = string.Format(@"UPDATE docdetail{0} 
SET REALQTY = REALQTY+@REALQTY, BOXCOUNT = BOXCOUNT+@BOXCOUNT, 
[Timestamp] = GETDATE(), Floor = @Floor 
WHERE DOCNO = @DOCNO AND ZSATNR = @ZSATNR 
AND ZCOLSN = @ZCOLSN AND ZSIZTX = @ZSIZTX", type == ReceiveType.交接单收货 ? "_dema" : "");
                int result = DBHelper.ExecuteSql(sql, false, p7, p9, p10, p11, p13, p14, p15, p16);

                return result > 0 ? true : false;
            }
            else
            {
                SqlParameter p7 = DBHelper.CreateParameter("@DOCNO", docNo);
                SqlParameter p8 = DBHelper.CreateParameter("@ITEMNO", itemNo);
                SqlParameter p9 = DBHelper.CreateParameter("@ZSATNR", zsatnr);
                SqlParameter p10 = DBHelper.CreateParameter("@ZCOLSN", zcolsn);
                SqlParameter p11 = DBHelper.CreateParameter("@ZSIZTX", zsiztx);
                SqlParameter p12 = DBHelper.CreateParameter("@ZCHARG", zcharg);
                SqlParameter p13 = DBHelper.CreateParameter("@QTY", qty);
                SqlParameter p14 = DBHelper.CreateParameter("@REALQTY", realQty);
                SqlParameter p15 = DBHelper.CreateParameter("@BOXCOUNT", boxCount);
                SqlParameter p16 = DBHelper.CreateParameter("@Floor", SysConfig.Floor);
                SqlParameter p17 = DBHelper.CreateParameter("@ZPBNO", zpbno);
                sql = string.Format(@"INSERT docdetail{0} 
(DOCNO, ITEMNO, ZSATNR, ZCOLSN, ZSIZTX, ZCHARG, QTY, REALQTY, BOXCOUNT, [Timestamp],Floor,ZPBNO) 
VALUES (@DOCNO, @ITEMNO, @ZSATNR, @ZCOLSN, @ZSIZTX, @ZCHARG, @QTY, @REALQTY,@BOXCOUNT,GETDATE(),@Floor,@ZPBNO)",
type == ReceiveType.交接单收货 ? "_dema" : "");
                int result = DBHelper.ExecuteSql(sql, false, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17);

                return result > 0 ? true : false;
            }
        }

        /// <summary>
        /// 根据交货单号获取本地交货单明细
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public static List<DocDetailInfo> GetDocDetailInfoListByDocNo(string docNo, ReceiveType type)
        {
            SqlParameter p1 = DBHelper.CreateParameter("@DOCNO", docNo);
            string sql = string.Format(@"SELECT DOCNO, ITEMNO, ZSATNR, ZCOLSN, ZSIZTX, 
ZCHARG, QTY, REALQTY, BOXCOUNT, ZPBNO FROM docdetail{0} WHERE DOCNO = @DOCNO",
type == ReceiveType.交接单收货 ? "_dema" : "");
            DataTable table = DBHelper.GetTable(sql, false, p1);

            if (table != null && table.Rows.Count > 0)
            {
                List<DocDetailInfo> list = new List<DocDetailInfo>();
                foreach (DataRow row in table.Rows)
                {
                    DocDetailInfo item = new DocDetailInfo();
                    item.DOCNO = row["DOCNO"].ToString();
                    item.ITEMNO = row["ITEMNO"].ToString();
                    item.ZSATNR = row["ZSATNR"].ToString();
                    item.ZCOLSN = row["ZCOLSN"].ToString();
                    item.ZSIZTX = row["ZSIZTX"].ToString();
                    item.ZCHARG = row["ZCHARG"].ToString();
                    item.ZPBNO = row["ZPBNO"].ToString();
                    item.QTY = int.Parse(row["QTY"].ToString());
                    item.REALQTY = int.Parse(row["REALQTY"].ToString());
                    item.BOXCOUNT = int.Parse(row["BOXCOUNT"].ToString() == "" ? "0" : row["BOXCOUNT"].ToString());
                    list.Add(item);
                }

                return list;
            }

            return null;
        }

        /// <summary>
        /// 保存错误记录
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool SaveErrorRecord(ErrorRecord error, ReceiveType type)
        {
            string sql = string.Format(@"INSERT INTO ErrorRecord{0}
(HU, ZSATNR, ZCOLSN, ZSIZTX, QTY, REMARK,RESULT,[Timestamp],Floor,DOCNO,ZPBNO) 
VALUES (@HU, @ZSATNR, @ZCOLSN, @ZSIZTX, @QTY, @REMARK,@RESULT,GETDATE(),@Floor,@DOCNO,@ZPBNO)", type == ReceiveType.交接单收货 ? "_dema" : "");

            SqlParameter p1 = DBHelper.CreateParameter("@ZSATNR", error.ZSATNR);
            SqlParameter p2 = DBHelper.CreateParameter("@ZCOLSN", error.ZCOLSN);
            SqlParameter p3 = DBHelper.CreateParameter("@ZSIZTX", error.ZSIZTX);
            SqlParameter p4 = DBHelper.CreateParameter("@HU", error.HU);
            SqlParameter p5 = DBHelper.CreateParameter("@QTY", error.QTY);
            SqlParameter p6 = DBHelper.CreateParameter("@REMARK", error.REMARK);
            SqlParameter p7 = DBHelper.CreateParameter("@RESULT", error.RESULT);
            SqlParameter p8 = DBHelper.CreateParameter("@Floor", SysConfig.Floor);
            SqlParameter p9 = DBHelper.CreateParameter("@DOCNO", error.DOCNO);
            SqlParameter p10 = DBHelper.CreateParameter("@ZPBNO", error.ZPBNO);
            //SqlParameter p10 = DBHelper.CreateParameter("@SapStatus", error.SapStatus);
            //SqlParameter p10 = DBHelper.CreateParameter("@SapResult", error.SapResult);
            int result = DBHelper.ExecuteSql(sql, false, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
            return true;
        }

        /// <summary>
        /// 根据交货单号获取错误记录
        /// </summary>
        /// <param name="DOCNO">交货单号</param>
        /// <returns></returns>
        public static List<ErrorRecord> GetErrorRecordsByDocNo(string DOCNO, ReceiveType type)
        {
            string sql = string.Format("SELECT Id,ZSATNR,ZCOLSN,ZSIZTX,HU,QTY,REMARK,RESULT,DOCNO,ZPBNO FROM dbo.ErrorRecord{2} WHERE DOCNO='{0}' AND Floor = '{1}' ORDER BY Id DESC", DOCNO, SysConfig.Floor, type == ReceiveType.交接单收货 ? "_dema" : "");
            //string sql = string.Format("SELECT Id,ZSATNR,ZCOLSN,ZSIZTX,HU,QTY,REMARK,RESULT FROM dbo.ErrorRecord WHERE HU IN(SELECT DISTINCT HU FROM dbo.epcdetail WHERE DOCNO = '{0}') AND Floor = '{1}' ORDER BY Id DESC", DOCNO, SysConfig.Floor);
            DataTable table = DBHelper.GetTable(sql, false);
            List<ErrorRecord> result = null;
            if (table != null && table.Rows.Count > 0)
            {
                result = new List<ErrorRecord>();
                foreach (DataRow row in table.Rows)
                {
                    ErrorRecord item = new ErrorRecord();
                    item.Id = long.Parse(row["Id"].ToString());
                    item.QTY = int.Parse(row["QTY"].ToString());
                    item.ZSATNR = row["ZSATNR"].ToString();
                    item.ZCOLSN = row["ZCOLSN"].ToString();
                    item.ZSIZTX = row["ZSIZTX"].ToString();
                    item.HU = row["HU"].ToString();
                    item.REMARK = row["REMARK"].ToString();
                    item.RESULT = row["RESULT"].ToString();
                    item.DOCNO = row["DOCNO"].ToString();
                    item.ZPBNO = row["ZPBNO"].ToString();
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 根据箱码获取错误记录
        /// </summary>
        /// <param name="HU">箱码</param>
        /// <returns></returns>
        public static List<ErrorRecord> GetErrorRecordsByHU(string HU, string startTime, string endTime, string SORE, string docno, string floor, ReceiveType type)
        {
            //SqlParameter p1 = DBHelper.CreateParameter("@HU", HU);
            if (string.IsNullOrEmpty(docno))
            {
                string sql = string.Format("SELECT Id,ZSATNR,ZCOLSN,ZSIZTX,HU,QTY,REMARK,RESULT,[Timestamp],DOCNO FROM dbo.ErrorRecord{4} WHERE HU LIKE '%{0}%' AND Timestamp BETWEEN '{1}' AND '{2}' AND Floor='{3}' ", HU, startTime, endTime, floor, type == ReceiveType.交接单收货 ? "_dema" : "");

                if (SORE != "")
                {
                    sql += string.Format(" AND Result='{0}'", SORE);
                }

                DataTable table = DBHelper.GetTable(sql, false);
                List<ErrorRecord> result = null;
                if (table != null && table.Rows.Count > 0)
                {
                    result = new List<ErrorRecord>();
                    foreach (DataRow row in table.Rows)
                    {
                        ErrorRecord item = new ErrorRecord();
                        item.Id = long.Parse(row["Id"].ToString());
                        item.QTY = int.Parse(row["QTY"].ToString());
                        item.ZSATNR = row["ZSATNR"].ToString();
                        item.ZCOLSN = row["ZCOLSN"].ToString();
                        item.ZSIZTX = row["ZSIZTX"].ToString();
                        item.HU = row["HU"].ToString();
                        item.REMARK = row["REMARK"].ToString();
                        item.RESULT = row["RESULT"].ToString();
                        item.Timestamp = row["Timestamp"].ToString().Trim();
                        item.DOCNO = row["DOCNO"].ToString().Trim();
                        result.Add(item);
                    }
                }
                return result;
            }
            else
            {
                string sql = string.Format("SELECT Id,ZSATNR,ZCOLSN,ZSIZTX,HU,QTY,REMARK,RESULT,[Timestamp],DOCNO FROM dbo.ErrorRecord{5} WHERE HU LIKE '%{0}%' AND Timestamp BETWEEN '{1}' AND '{2}' AND Floor='{3}' AND DOCNO='{4}' ", HU, startTime, endTime, floor, docno, type == ReceiveType.交接单收货 ? "_dema" : "");
                //string sql = string.Format("SELECT Id,ZSATNR,ZCOLSN,ZSIZTX,HU,QTY,REMARK,RESULT,[Timestamp] FROM dbo.ErrorRecord WHERE HU IN (SELECT HU FROM (SELECT DISTINCT HU FROM dbo.epcdetail WHERE DOCNO='{0}')a WHERE a.HU LIKE '%{1}%') AND Timestamp BETWEEN '{2}' AND '{3}' AND Floor = '{4}' ", docno, HU, startTime, endTime, floor);

                if (SORE != "")
                {
                    sql += string.Format(" AND Result='{0}'", SORE);
                }

                DataTable table = DBHelper.GetTable(sql, false);
                List<ErrorRecord> result = null;
                if (table != null && table.Rows.Count > 0)
                {
                    result = new List<ErrorRecord>();
                    foreach (DataRow row in table.Rows)
                    {
                        ErrorRecord item = new ErrorRecord();
                        item.Id = long.Parse(row["Id"].ToString());
                        item.QTY = int.Parse(row["QTY"].ToString());
                        item.ZSATNR = row["ZSATNR"].ToString();
                        item.ZCOLSN = row["ZCOLSN"].ToString();
                        item.ZSIZTX = row["ZSIZTX"].ToString();
                        item.HU = row["HU"].ToString();
                        item.REMARK = row["REMARK"].ToString();
                        item.RESULT = row["RESULT"].ToString();
                        item.DOCNO = row["DOCNO"].ToString();
                        item.Timestamp = row["Timestamp"].ToString().Trim();
                        result.Add(item);
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// 根据箱码精准匹配,获取检货记录
        /// </summary>
        /// <param name="HU"></param>
        /// <returns></returns>
        public static List<ErrorRecord> GetErrorRecordsByPrecisionHU(string HU, ReceiveType type)
        {
            string sql = string.Format("SELECT Id,ZSATNR,ZCOLSN,ZSIZTX,HU,QTY,REMARK,RESULT,DOCNO FROM dbo.ErrorRecord{1} WHERE HU ='{0}' ", HU, type == ReceiveType.交接单收货 ? "_dema" : "");
            DataTable table = DBHelper.GetTable(sql, false);
            List<ErrorRecord> result = null;
            if (table != null && table.Rows.Count > 0)
            {
                result = new List<ErrorRecord>();
                foreach (DataRow row in table.Rows)
                {
                    ErrorRecord item = new ErrorRecord();
                    item.Id = long.Parse(row["Id"].ToString());
                    item.QTY = int.Parse(row["QTY"].ToString());
                    item.ZSATNR = row["ZSATNR"].ToString();
                    item.ZCOLSN = row["ZCOLSN"].ToString();
                    item.ZSIZTX = row["ZSIZTX"].ToString();
                    item.HU = row["HU"].ToString();
                    item.REMARK = row["REMARK"].ToString();
                    item.RESULT = row["RESULT"].ToString();
                    item.DOCNO = row["DOCNO"].ToString();

                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 根据箱码精准匹配,获取epc信息
        /// </summary>
        /// <param name="HU"></param>
        /// <returns></returns>
        public static List<EpcDetail> GetEpcDetailByPrecisionHU(string HU, ReceiveType type)
        {
            string sql = string.Format(@"SELECT Id,LGNUM,DOCNO,DOCCAT,HU,EPC_SER,Handled,Result,Timestamp,Floor 
 FROM dbo.epcdetail{1} WHERE HU ='{0}' ORDER BY EPC_SER ", HU, type == ReceiveType.交接单收货 ? "_dema" : "");
            DataTable table = DBHelper.GetTable(sql, false);
            List<EpcDetail> result = null;
            if (table != null && table.Rows.Count > 0)
            {
                result = new List<EpcDetail>();
                foreach (DataRow row in table.Rows)
                {
                    EpcDetail item = new EpcDetail();
                    item.Id = long.Parse(row["Id"].ToString());
                    item.Handled = int.Parse(row["Handled"].ToString());
                    item.LGNUM = row["LGNUM"].ToString();
                    item.DOCCAT = row["DOCCAT"].ToString();
                    item.DOCNO = row["DOCNO"].ToString();
                    item.HU = row["HU"].ToString();
                    item.EPC_SER = row["EPC_SER"].ToString();
                    item.Floor = row["Floor"].ToString();
                    item.Result = row["Result"].ToString();

                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 根据交货单号和epc获取epc明细
        /// </summary>
        /// <param name="docNo">交货单号</param>
        /// <param name="epc">Epc号</param>
        /// <returns></returns>
        public static List<EpcDetail> GetEpcDetailByDocNoAndEpc(string docNo, string epc, ReceiveType type)
        {
            string sql = null;
            if (string.IsNullOrEmpty(epc))
                sql = string.Format("SELECT Id,LGNUM,DOCNO,DOCCAT,HU,EPC_SER,Handled,Result,Timestamp,Floor FROM dbo.epcdetail{1} WHERE DOCNO ='{0}' ", docNo, type == ReceiveType.交接单收货 ? "_dema" : "");
            else
                sql = string.Format("SELECT Id,LGNUM,DOCNO,DOCCAT,HU,EPC_SER,Handled,Result,Timestamp,Floor FROM dbo.epcdetail{2} WHERE DOCNO ='{0}' AND EPC_SER = '{1}' ", docNo, epc, type == ReceiveType.交接单收货 ? "_dema" : "");
            DataTable table = DBHelper.GetTable(sql, false);
            List<EpcDetail> result = null;
            if (table != null && table.Rows.Count > 0)
            {
                result = new List<EpcDetail>();
                foreach (DataRow row in table.Rows)
                {
                    EpcDetail item = new EpcDetail();
                    item.Id = long.Parse(row["Id"].ToString());
                    item.Handled = int.Parse(row["Handled"].ToString());
                    item.LGNUM = row["LGNUM"].ToString();
                    item.DOCCAT = row["DOCCAT"].ToString();
                    item.DOCNO = row["DOCNO"].ToString();
                    item.HU = row["HU"].ToString();
                    item.EPC_SER = row["EPC_SER"].ToString();
                    item.Floor = row["Floor"].ToString();
                    item.Result = row["Result"].ToString();

                    result.Add(item);
                }
            }
            return result;
        }



        #region  单检机代码
        /// <summary>
        /// 通过EPC查询对应明细
        /// </summary>
        /// <returns></returns>
        public static DataTable GetEPCByMT(string sEPC)
        {
            string sql = string.Format(@"select t.MATNR,m.ZSATNR,m.ZSIZTX,m.ZCOLSN,t.RFID_EPC,t.RFID_ADD_EPC 
                from taginfo t inner join materialinfo m on t.MATNR=m.MATNR 
                where SUBSTRING(t.RFID_EPC,0,15)=SUBSTRING('{0}',0,15) or t.RFID_ADD_EPC=SUBSTRING('{0}',0,15) or 
                t.BARCD='{0}' or t.BARCD_ADD='{0}'", sEPC);
            DataTable table = DBHelper.GetTable(sql, false);
            if (table != null && table.Rows.Count > 0)
            {
                return table;
            }
            return null;
        }

        /// <summary>
        /// 通过箱码查询对应已上传SAP明细
        /// </summary>
        /// <returns></returns>
        public static DataTable GetHUByList(string sHU, ReceiveType type)
        {
            string sql = string.Format("select e.HU,e.ZSATNR,e.ZCOLSN,e.ZSIZTX,p.EPC_SER,e.RESULT from ErrorRecord{1} e inner join epcdetail{1} p on e.HU=p.HU where e.HU='{0}' and p.Handled=1 ", sHU, type == ReceiveType.交接单收货 ? "_dema" : "");
            DataTable table = DBHelper.GetTable(sql, false);
            if (table != null && table.Rows.Count > 0)
            {
                return table;
            }
            return null;
        }

        /// <summary>
        /// 获取EPC已扫描的标签信息
        /// </summary>
        /// <param name="hu"></param>
        /// <returns></returns>
        public static DataTable GetErrorEpcByHU(string hu, ReceiveType type)
        {
            if (string.IsNullOrEmpty(hu.Trim()))
                return null;
            string sql = string.Format("SELECT DISTINCT a.LGNUM,a.DOCNO,a.DOCCAT,b.HU,a.EPC_SER FROM dbo.epcdetail{1} a INNER JOIN dbo.epcdetail{1} b ON a.EPC_SER = b.EPC_SER AND a.HU<>b.HU AND b.Result='S' WHERE a.HU = '{0}'", hu, type == ReceiveType.交接单收货 ? "_dema" : "");
            return DBHelper.GetTable(sql, false);
        }

        /// <summary>
        /// 获取该箱内扫描过的所有EPC{做重复过滤}
        /// </summary>
        /// <param name="hu"></param>
        /// <returns></returns>
        public static DataTable GetAllDistinctEpcByHU(string hu, ReceiveType type)
        {
            if (string.IsNullOrEmpty(hu.Trim()))
                return null;
            string sql = string.Format("SELECT  DISTINCT LGNUM , DOCNO ,DOCCAT , HU , EPC_SER FROM dbo.epcdetail{1} WHERE HU='{0}'", hu, type == ReceiveType.交接单收货 ? "_dema" : "");
            return DBHelper.GetTable(sql, false);
        }

        /// <summary>
        /// 获取需要短拣的箱子的数据
        /// </summary>
        /// <param name="hu"></param>
        /// <returns></returns>
        public static DataSet GetShortPickHuInfo(string hu)
        {
            string sql = string.Format(@"
select b.HU,a.PICK_TASK,a.PICK_TASK_ITEM,a.PRODUCTNO,ISNULL(a.QTY, 0) - ISNULL(a.REALQTY, 0) as QTY from InventoryOutLogDetail a
inner join BoxPickTaskMap b on a.PICK_TASK = b.PICK_TASK and b.HU = '{0}'
SELECT Id ,
       LGNUM ,
       SHIP_DATE ,
       PARTNER ,
       HU ,
       EPC_SER ,
       Handled ,
       Result ,
       Timestamp ,
       LOUCENG ,
       BOXGUID ,
       MATNR ,
       ZSATNR ,
       ZCOLSN ,
       ZSIZTX, IsAdd FROM dbo.DeliverEpcDetail WHERE HU = '{0}'
", hu);
            return DBHelper.GetDataSet(sql, false);
        }


        public static int GetJianHuoShortNum(string hu,string mat)
        {
            try
            {
                string sql = string.Format(@"select SHORT_QTY from DeliverJianHuo where HU='{0}' AND MAT='{1}'", hu, mat);
                object val = DBHelper.GetValue(sql, false);
                int re = 0;
                if (val != null)
                {
                    int.TryParse(val.ToString(), out re);
                }
                return re;
            }
            catch (Exception e)
            {
                Log4netHelper.LogError(e);
            }
            return 0;
        }
        public static List<CJianHuoHu> GetJianHuoHu(string hu)
        {
            List<CJianHuoHu> re = new List<CJianHuoHu>();
            try
            {
                if (string.IsNullOrEmpty(hu.Trim()))
                    return re;

                string sql = string.Format(@"select * from DeliverJianHuo where HU='{0}'", hu);
                DataTable dt = DBHelper.GetTable(sql, false);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        CJianHuoHu jh = new CJianHuoHu();
                        jh.hu = row["HU"].ToString();
                        jh.pick_list = row["PICK_LIST"].ToString();
                        jh.mat = row["MAT"].ToString();
                        jh.p = row["ZSATNR"].ToString();
                        jh.s = row["ZCOLSN"].ToString();
                        jh.g = row["ZSIZTX"].ToString();
                        jh.should_qty = row["SHOULD_QTY"].ToString();
                        jh.real_qty = row["REAL_QTY"].ToString();
                        jh.short_qty = row["SHORT_QTY"].ToString();
                        jh.opr_time = row["TIMESTAMP"].ToString();

                        re.Add(jh);
                    }
                }
            }
            catch (Exception e)
            {
                Log4netHelper.LogError(e);
            }

            return re;
        }
        public static void SaveJianHuoInfo(string hu,string pick_list,List<CJianHuoContrastRe> info)
        {
            try
            {
                if (string.IsNullOrEmpty(hu.Trim()))
                    return;

                if (info == null || info.Count <= 0)
                    return;

                string sql = string.Format(@"delete from DeliverJianHuo where HU='{0}'", hu);
                DBHelper.ExecuteNonQuery(sql);

                foreach (var v in info)
                {
                    sql = string.Format(@"insert into DeliverJianHuo (HU
                ,PICK_LIST
                ,MAT
                ,ZSATNR
                ,ZCOLSN
                ,ZSIZTX
                ,SHOULD_QTY
                ,REAL_QTY
                ,SHORT_QTY
                ,TIMESTAMP) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},{8},GETDATE())", hu
                    , pick_list, v.mat, v.p, v.s, v.g, v.shouldQty, v.realQty,v.shortQty);

                    DBHelper.ExecuteNonQuery(sql);
                }
            }
            catch(Exception e)
            {
                Log4netHelper.LogError(e);
            }
        }

        public static void updateShortJianHuo(string hu,string mat,int shortQty)
        {
            try
            {
                string sql = string.Format(@"update DeliverJianHuo set SHORT_QTY={0} where HU='{1}' and MAT='{2}'", shortQty, hu, mat);
                DBHelper.ExecuteNonQuery(sql);

                sql = string.Format(@" UPDATE dbo.BoxPickTaskMap SET IS_SHORT_PICK = 1 WHERE HU='{0}'", hu);
                DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                Log4netHelper.LogError(e);
            }
        }

        #endregion

        public static List<string> GetMaterialInfoByPin(string pin)
        {
            List<string> re = new List<string>();
            string sSql = string.Format("select MATNR from materialinfo where  ZSATNR='{0}'", pin);
            DataTable dt = DBHelper.GetTable(sSql, false);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        string matnr = r["MATNR"] == null ? "" : r["MATNR"].ToString();
                        if (!string.IsNullOrEmpty(matnr))
                        {
                            if (!re.Exists(i => i == matnr))
                                re.Add(matnr);
                        }
                    }
                }
            }
            return re;
        }

        public static int InsertCancelReData(string doc,string hu,int main,int add,int real,int mainDiff,int addDiff,int noReg,int noInnum,string msg,int re)
        {
            try
            {
                string sql = string.Format("insert into CancelInResult values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',GETDATE(),'{9}','{10}')", doc, hu, main, add, real, mainDiff, addDiff, noReg, noInnum, msg, re);
                return DBHelper.ExecuteNonQuery(sql);
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
            }

            return 0;
        }
        public static DataTable GetCancelReData(string doc)
        {
            try
            {
                string sql = string.Format("select * from CancelInResult where docNo='{0}' order by insertTime", doc);
                return DBHelper.GetTable(sql,false);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
            }

            return null;
        }
        public static DataTable GetCancelReData(string doc,bool inventory)
        {
            try
            {
                string sql = string.Format("select * from CancelInResult where docNo='{0}' and re={1} order by insertTime", doc, inventory ? 0 : 1);
                return DBHelper.GetTable(sql, false);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
            }

            return null;
        }
        public static DataTable GetCancelReDataHu(string hu)
        {
            try
            {
                string sql = string.Format("select * from CancelInResult where hu='{0}' order by insertTime", hu);
                return DBHelper.GetTable(sql, false);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace.ToString());
            }

            return null;
        }

        public static DataTable GetCancelUpload(string docno)
        {
            try
            {
                string sql = string.Format("select * from CancelUpload where docNo='{0}' order by doTime", docno);
                return DBHelper.GetTable(sql, false);
                
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
            return null;
        }
        public static DataTable GetCancelUpload(string docno,string hu)
        {
            try
            {
                string sql = string.Format("select * from CancelUpload where docNo='{0}' and hu='{1}' order by doTime", docno, hu);
                return DBHelper.GetTable(sql, false);

            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
            return null;
        }

        public static DataTable getInfoFromEpc(string epc,bool jiaohuodan)
        {
            try
            {
                string sql = string.Format("select * from {0} where EPC_SER = '{1}'", jiaohuodan ? "epcdetail" : "epcdetail_dema", epc);
                return DBHelper.GetTable(sql, false);

            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
            return null;
        }
        public static string getXiangXingStr(string xinghaoID)
        {
            string fhbc = xinghaoID;
            if (fhbc == "1200000100")
            {
                return "箱装标准箱";
            }
            else if (fhbc == "1200000102")
            {
                return "挂装标准箱";
            }
            else if (fhbc == "1200000103")
            {
                return "鞋子箱";
            }
            else if (fhbc == "1200000104")
            {
                return "小件箱";
            }
            else if (fhbc == "1200000105")
            {
                return "大衣箱";
            }
            else if (fhbc == "1200000172")
            {
                return "箱装标准箱";
            }
            else if (fhbc == "1200000173")
            {
                return "挂装标准箱";
            }
            else if (fhbc == "1200000174")
            {
                return "鞋子箱";
            }
            else if (fhbc == "1200000175")
            {
                return "小件箱";
            }
            else if (fhbc == "1200000176")
            {
                return "大衣箱";
            }
            //------------------------------------------
            else if (fhbc == "1200000918")
            {
                return "箱装标准箱";
            }
            else if (fhbc == "1200000919")
            {
                return "挂装标准箱";
            }
            else if (fhbc == "1200000921")
            {
                return "鞋子箱";
            }
            else if (fhbc == "1200000922")
            {
                return "小件箱";
            }

            return "";
        }
        public static double getXiangXingWeight(string fhbc)
        {
            double re = 0;
            if (fhbc == "1200000100")
            {
                re = 1.211;
            }
            else if (fhbc == "1200000102")
            {
                re = 1.764;
            }
            else if (fhbc == "1200000103")
            {
                re = 1.041;
            }
            else if (fhbc == "1200000104")
            {
                re = 0.784;
            }
            else if (fhbc == "1200000105")
            {
                re = 1.900;
            }
            else if (fhbc == "1200000172")
            {
                re = 1.211;
            }
            else if (fhbc == "1200000173")
            {
                re = 1.764;
            }
            else if (fhbc == "1200000174")
            {
                re = 1.041;
            }
            else if (fhbc == "1200000175")
            {
                re = 0.784;
            }
            else if (fhbc == "1200000176")
            {
                re = 1.900;
            }
            //------------------------------------------
            else if (fhbc == "1200000918")
            {
                re = 1.200;
            }
            else if (fhbc == "1200000919")
            {
                re = 1.800;
            }
            else if (fhbc == "1200000921")
            {
                re = 0.9;
            }
            else if (fhbc == "1200000922")
            {
                re = 0.7;
            }

            return re;
        }

        public static bool clearJiaoJieDan(string doc,ref string msg)
        {
            bool re = false;
            msg = "";
            try
            {
                string sql = string.Format("delete from JiaoJieDan where doc='{0}'", doc);
                DBHelper.ExecuteNonQuery(sql);
                re = true;
            }
            catch(Exception ex)
            {
                Log4netHelper.LogError(ex);
                msg = ex.ToString();
            }
            return re;
        }
        public static bool clearJiaoJieDanHu(string doc,string hu,ref string msg)
        {
            bool re = false;
            msg = "";
            try
            {
                string sql = string.Format("delete from JiaoJieDan where doc='{0}' and hu='{1}'", doc, hu);
                DBHelper.ExecuteNonQuery(sql);
                re = true;
            }
            catch(Exception ex)
            {
                Log4netHelper.LogError(ex);
                msg = ex.ToString();
            }
            return re;
        }
        public static List<CJJBox> getJiaoJieDan(string doc)
        {
            List<CJJBox> re = new List<CJJBox>();
            try
            {
                string sql = string.Format("select detail from JiaoJieDan where doc='{0}' order by timestamp", doc);
                DataTable dt = DBHelper.GetTable(sql, false);
                if(dt!=null && dt.Rows.Count>0)
                {
                    foreach(DataRow r in dt.Rows)
                    {
                        CJJBox box = JsonConvert.DeserializeObject<CJJBox>(r["detail"].ToString());
                        if (box != null)
                            re.Add(box);
                    }
                }
            }
            catch(Exception ex)
            {
                Log4netHelper.LogError(ex);
            }
            return re;
        }
        public static void updateJiaoJieDanSAP(string doc,string hu,string sapRe,string sapMsg)
        {
            try
            {
                string sql = string.Format("select hu from JiaoJieDan where doc='{0}' and hu='{1}'", doc, hu);
                string tarHu = nullStr(DBHelper.GetValue(sql, false));
                if (!string.IsNullOrEmpty(tarHu))
                {
                    sql = string.Format("update JiaoJieDan set sapRe='{0}',sapMsg='{1}' where  doc='{2}' and hu='{3}'", sapRe, sapMsg, doc, hu);
                    DBHelper.ExecuteNonQuery(sql);
                }
            }
            catch(Exception ex)
            {
                Log4netHelper.LogError(ex);
            }
        }
        public static void saveJiaoJieDan(CJJBox box)
        {
            try
            {
                string sql = string.Format("select hu from JiaoJieDan where doc='{0}' and hu='{1}'", box.doc, box.hu);
                string hu = nullStr(DBHelper.GetValue(sql, false));
                if (!string.IsNullOrEmpty(hu))
                {
                    sql = string.Format("delete from JiaoJieDan where doc='{0}' and hu='{1}'", box.doc, box.hu);
                    DBHelper.ExecuteNonQuery(sql);
                }

                sql = string.Format("insert into JiaoJieDan (doc,hu,detail,re,msg,sapRe,sapMsg,timestamp) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}', GETDATE())"
                    , box.doc, box.hu, JsonConvert.SerializeObject(box), box.inventoryRe, box.inventoryMsg, box.sapRe, box.sapMsg);

                DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(ex);
            }
        }
    }

    public class CTimeLog
    {
        bool mEnabled = true;
        System.Diagnostics.Stopwatch mWatch = new Stopwatch();
        public CTimeLog(bool e)
        {
            mEnabled = e;
        }

        public void startTimeLog(string mMsg)
        {
            if (!mEnabled)
                return;
            try
            {
                LogHelper.WriteLine("开始：" + mMsg);
                mWatch.Restart();
            }
            catch(Exception )
            { }
        }

        public void stopTimeLog(string mMsg)
        {
            if (!mEnabled)
                return;

            try
            {
                mWatch.Stop();
                LogHelper.WriteLine("结束：" + mMsg + "---" + "耗时：" + mWatch.Elapsed.TotalSeconds.ToString());
            }
            catch(Exception )
            {

            }
        }
    }
}
