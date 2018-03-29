using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using HLAWebService.Model;
using System.Data;

namespace HLAWebService.Utils
{
    public class LocalDataService
    {
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
        public static bool SaveBoxPickTaskMapInfo(string HU, string LOUCENG, DateTime? SHIP_DATE, float SHULIANG, string STORE_ID, string XIANGXING, string PICK_TASK)
        {
            try
            {
                string sql = string.Format("P_BoxPickTaskMap_Save ");
                SqlParameter p1 = DBHelper.CreateParameter("@HU", !string.IsNullOrEmpty(HU) ? HU.TrimStart('0') : null);
                SqlParameter p2 = DBHelper.CreateParameter("@PARTNER", STORE_ID);
                SqlParameter p3 = DBHelper.CreateParameter("@LOUCENG", LOUCENG);
                SqlParameter p4 = DBHelper.CreateParameter("@SHIP_DATE", SHIP_DATE);
                SqlParameter p5 = DBHelper.CreateParameter("@PACKMAT", XIANGXING);
                SqlParameter p6 = DBHelper.CreateParameter("@QUAN", SHULIANG);
                SqlParameter p7 = DBHelper.CreateParameter("@PICK_TASK", PICK_TASK);
                int result = int.Parse(DBHelper.GetValue(sql, true, p1, p2, p3, p4, p5, p6, p7).ToString());

                return true;
            }
            catch (Exception ex)
            {
                //LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 删除发运箱和门店对应关系
        /// </summary>
        /// <param name="HU"></param>
        /// <param name="SHIP_DATE"></param>
        /// <returns></returns>
        public static bool DeleteBoxPickTaskMapInfo(string HU, DateTime SHIP_DATE)
        {
            try
            {
                string sql = string.Format("DELETE FROM BoxPickTaskMap WHERE HU = @HU AND SHIP_DATE = @SHIP_DATE ");
                SqlParameter p1 = DBHelper.CreateParameter("@HU", !string.IsNullOrEmpty(HU) ? HU.TrimStart('0') : null);
                SqlParameter p2 = DBHelper.CreateParameter("@SHIP_DATE", SHIP_DATE);
                int result = DBHelper.ExecuteSql(sql, false, p1, p2);

                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                //LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 根据箱号和发运日期获取箱号和下架单对应关系
        /// </summary>
        /// <param name="HU"></param>
        /// <returns></returns>
        public static List<BoxPickTaskMapInfo> GetBoxPickTaskMapInfoByHU(string HU, DateTime SHIP_DATE)
        {
            try
            {
                string sql = "SELECT HU, PICK_TASK, PARTNER, LOUCENG, SHIP_DATE, PACKMAT, QUAN, IS_SCAN FROM BoxPickTaskMap WHERE HU = @HU AND SHIP_DATE = @SHIP_DATE";
                SqlParameter p1 = DBHelper.CreateParameter("@HU", HU);
                SqlParameter p2 = DBHelper.CreateParameter("@SHIP_DATE", SHIP_DATE);
                DataTable table = DBHelper.GetTable(sql, false, p1, p2);
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

                        list.Add(item);
                    }

                    return list;
                }

                return null;
            }
            catch (Exception ex)
            {
                //LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 获取发运箱信息
        /// </summary>
        /// <param name="LOUCENG"></param>
        /// <param name="PARTNER"></param>
        /// <param name="shipDate"></param>
        /// <param name="HU"></param>
        /// <param name="RESULT"></param>
        /// <returns></returns>
        public static PKDeliverBox GetDeliverBox(string LOUCENG, string PARTNER, DateTime shipDate, string HU, string RESULT)
        {
            try
            {
                string sql = @"SELECT GUID, LOUCENG, PARTNER, SHIP_DATE, HU, RESULT, REMARK
                               FROM DeliverBox WHERE LOUCENG = @LOUCENG  AND PARTNER = @PARTNER AND SHIP_DATE = @SHIP_DATE AND HU = @HU
                                    AND RESULT = @RESULT AND REMARK = ''";
                SqlParameter p1 = DBHelper.CreateParameter("@LOUCENG", LOUCENG);
                SqlParameter p2 = DBHelper.CreateParameter("@PARTNER", PARTNER);
                SqlParameter p3 = DBHelper.CreateParameter("@SHIP_DATE", shipDate);
                SqlParameter p4 = DBHelper.CreateParameter("@HU", HU);
                SqlParameter p5 = DBHelper.CreateParameter("@RESULT", RESULT);
                DataTable table = DBHelper.GetTable(sql, false, p1, p2, p3, p4, p5);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    
                    PKDeliverBox item = new PKDeliverBox();
                    item.GUID = row["GUID"] == null ? null : row["GUID"].ToString();
                    item.LOUCENG = row["LOUCENG"] == null ? null : row["LOUCENG"].ToString();
                    item.PARTNER = row["PARTNER"] == null ? null : row["PARTNER"].ToString();
                    item.SHIP_DATE = DateTime.Parse(row["SHIP_DATE"].ToString());
                    item.HU = row["HU"] == null ? null : row["HU"].ToString();
                    item.RESULT = row["RESULT"] == null ? null : row["RESULT"].ToString();
                    item.REMARK = row["REMARK"] == null ? null : row["REMARK"].ToString();
                    
                    return item;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public static List<PKDeliverErrorBox> GetDeliverErrorBoxListByBoxGuid(string BOXGUID)
        {
            try
            {
                string sql = @"SELECT BOXGUID, LOUCENG, PARTNER, SHIP_DATE, HU, ZSATNR, ZCOLSN, ZSIZTX,
                                      QUAN, REAL, DIFF, REMARK, SHORTQTY, PICK_TASK, PICK_TASK_ITEM, MATNR, ADD_REAL
                               FROM DeliverErrorBox WHERE BOXGUID = @BOXGUID ";
                SqlParameter p1 = DBHelper.CreateParameter("@BOXGUID", BOXGUID);
                DataTable table = DBHelper.GetTable(sql, false, p1);
                if (table != null && table.Rows.Count > 0)
                {
                    List<PKDeliverErrorBox> list = new List<PKDeliverErrorBox>();
                    foreach (DataRow row in table.Rows)
                    {
                        PKDeliverErrorBox item = new PKDeliverErrorBox();
                        item.BOXGUID = row["BOXGUID"] != null ? row["BOXGUID"].ToString() : null;
                        item.LOUCENG = row["LOUCENG"] != null ? row["LOUCENG"].ToString() : null;
                        item.PICK_TASK = row["PICK_TASK"] != null ? row["PICK_TASK"].ToString() : null;
                        item.PICK_TASK_ITEM = row["PICK_TASK_ITEM"] != null ? row["PICK_TASK_ITEM"].ToString() : null;
                        item.PARTNER = row["PARTNER"] != null ? row["PARTNER"].ToString() : null;
                        item.SHIP_DATE = row["SHIP_DATE"] != null ? DateTime.Parse(row["SHIP_DATE"].ToString()) : DateTime.MinValue;
                        item.SHORTQTY = row["SHORTQTY"] != null ? long.Parse(row["SHORTQTY"].ToString()) : 0;
                        item.HU = row["HU"] != null ? row["HU"].ToString() : null;
                        item.MATNR = row["MATNR"] != null ? row["MATNR"].ToString() : null;
                        item.ZSATNR = row["ZSATNR"] != null ? row["ZSATNR"].ToString() : null;
                        item.ZCOLSN = row["ZCOLSN"] != null ? row["ZCOLSN"].ToString() : null;
                        item.ZSIZTX = row["ZSIZTX"] != null ? row["ZSIZTX"].ToString() : null;
                        item.QUAN = row["QUAN"] != null ? long.Parse(row["QUAN"].ToString()) : 0;
                        item.REAL = row["REAL"] != null ? long.Parse(row["REAL"].ToString()) : 0;
                        item.ADD_REAL = row["ADD_REAL"] != null ? long.Parse(row["ADD_REAL"].ToString()) : 0;
                        item.DIFF = row["DIFF"] != null ? long.Parse(row["DIFF"].ToString()) : 0;
                        item.REMARK = row["REMARK"] != null ? row["REMARK"].ToString() : null;
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
        /// 删除发运箱
        /// </summary>
        /// <param name="GUID"></param>
        /// <returns></returns>
        public static bool DeleteDeliverBox(string HU, DateTime SHIP_DATE)
        {
            try
            {
                string sql = @"DELETE FROM DeliverBox WHERE HU = @HU AND SHIP_DATE = @SHIP_DATE";
                SqlParameter p1 = DBHelper.CreateParameter("@HU", HU);
                SqlParameter p2 = DBHelper.CreateParameter("@SHIP_DATE", SHIP_DATE);
                int result = DBHelper.ExecuteSql(sql, false, p1, p2);

                return result > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除发运错误箱信息
        /// </summary>
        /// <param name="HU"></param>
        /// <param name="SHIP_DATE"></param>
        /// <returns></returns>
        public static bool DeleteDeliverErrorBox(string HU, DateTime SHIP_DATE)
        {
            try
            {
                string sql = @"DELETE FROM DeliverErrorBox WHERE HU = @HU AND SHIP_DATE = @SHIP_DATE";
                SqlParameter p1 = DBHelper.CreateParameter("@HU", HU);
                SqlParameter p2 = DBHelper.CreateParameter("@SHIP_DATE", SHIP_DATE);
                int result = DBHelper.ExecuteSql(sql, false, p1, p2);

                return result > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除发运箱epc明细
        /// </summary>
        /// <param name="BOXGUID"></param>
        /// <returns></returns>
        public static bool DeleteDeliverEpcDetail(string HU, DateTime SHIP_DATE)
        {
            try
            {
                string sql = @"DELETE FROM DeliverEpcDetail WHERE HU = @HU AND SHIP_DATE = @SHIP_DATE";
                SqlParameter p1 = DBHelper.CreateParameter("@HU", HU);
                SqlParameter p2 = DBHelper.CreateParameter("@SHIP_DATE", SHIP_DATE);
                int result = DBHelper.ExecuteSql(sql, false, p1, p2);

                return result > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public static List<DeliverEpcDetailInfo> GetDeliverEpcDetailListByBoxGuid(string BOXGUID)
        {
            string sql = "SELECT LGNUM, SHIP_DATE, LOUCENG, PARTNER, HU, EPC_SER, Result, BOXGUID FROM DeliverEpcDetail WHERE BOXGUID = @BOXGUID";
            SqlParameter p1 = DBHelper.CreateParameter("@BOXGUID", BOXGUID);
            DataTable table = DBHelper.GetTable(sql, false, p1);
            if (table != null && table.Rows.Count > 0)
            {
                List<DeliverEpcDetailInfo> list = new List<DeliverEpcDetailInfo>();
                foreach (DataRow row in table.Rows)
                {
                    DeliverEpcDetailInfo item = new DeliverEpcDetailInfo();
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

        public static bool RemoveDeliverDetailNum(string LOUCENG, string PARTNER, DateTime SHIP_DATE, string ZSATNR, string ZCOLSN, string ZSIZTX, int count)
        {
            try
            {
                string sql = @"UPDATE DeliverDetail SET REAL = REAL - @COUNT, DIFF = DIFF + @COUNT
                                WHERE LOUCENG = @LOUCENG AND PARTNER = @PARTNER AND SHIP_DATE = @SHIP_DATE
                                    AND ZSATNR = @ZSATNR AND ZCOLSN = @ZCOLSN AND ZSIZTX = @ZSIZTX";
                SqlParameter p1 = DBHelper.CreateParameter("@LOUCENG", LOUCENG);
                SqlParameter p2 = DBHelper.CreateParameter("@PARTNER", PARTNER);
                SqlParameter p3 = DBHelper.CreateParameter("@SHIP_DATE", SHIP_DATE);
                SqlParameter p4 = DBHelper.CreateParameter("@ZSATNR", ZSATNR);
                SqlParameter p5 = DBHelper.CreateParameter("@ZCOLSN", ZCOLSN);
                SqlParameter p6 = DBHelper.CreateParameter("@ZSIZTX", ZSIZTX);
                SqlParameter p7 = DBHelper.CreateParameter("@COUNT", count);

                int result = DBHelper.ExecuteSql(sql, false, p1, p2, p3, p4, p5, p6, p7);

                return result > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public static bool RemovePickTaskRealNum(string pickTask, string pickTaskItem, string productNo, int count, int subTagCount)
        {
            try
            {
                string sql = @"UPDATE InventoryOutLogDetail SET REALQTY = REALQTY - @COUNT, REALQTY_ADD = REALQTY_ADD - @SUBCOUNT
                                WHERE PICK_TASK = @PICK_TASK AND PICK_TASK_ITEM = @PICK_TASK_ITEM AND PRODUCTNO = @PRODUCTNO ";
                SqlParameter p1 = DBHelper.CreateParameter("@PICK_TASK", pickTask);
                SqlParameter p2 = DBHelper.CreateParameter("@PRODUCTNO", productNo);
                SqlParameter p3 = DBHelper.CreateParameter("@COUNT", count);
                SqlParameter p4 = DBHelper.CreateParameter("@SUBCOUNT", subTagCount);
                SqlParameter p5 = DBHelper.CreateParameter("@PICK_TASK_ITEM", pickTaskItem);

                int result = DBHelper.ExecuteSql(sql, false, p1, p2, p3, p4, p5);

                return result > 0 ? true : false;
            }
            catch
            {
                return false;
            }
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
                    HLATagInfo item = new HLATagInfo();
                    item.MATNR = row["MATNR"].ToString();
                    item.BARCD = row["BARCD"].ToString();
                    item.BARCD_ADD = row["BARCD_ADD"].ToString();
                    item.BARDL = row["BARDL"].ToString();
                    item.CHARG = row["CHARG"].ToString();
                    item.Id = long.Parse(row["Id"].ToString());
                    item.RFID_ADD_EPC = row["RFID_ADD_EPC"].ToString();
                    item.RFID_EPC = row["RFID_EPC"].ToString();
                    list.Add(item);
                }
                return list;
            }

            return null;
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
                    item.MATNR = row["MATNR"].ToString();
                    item.ZSATNR = row["ZSATNR"].ToString();
                    item.ZCOLSN = row["ZCOLSN"].ToString();
                    item.ZSIZTX = row["ZSIZTX"].ToString();
                    item.ZSUPC2 = row["ZSUPC2"].ToString();
                    item.PXQTY = int.Parse(row["PXQTY"].ToString());

                    list.Add(item);
                }
                return list;
            }

            return null;
        }

        public static bool Log(string device, string msg)
        {
            string sql = string.Format(@"INSERT INTO dbo.DeviceLog( DeviceNO, [Log] ) VALUES  ( '{0}', '{1}')", device, msg);
            return DBHelper.ExecuteNonQuery(sql) > 0;
        }
    }
}