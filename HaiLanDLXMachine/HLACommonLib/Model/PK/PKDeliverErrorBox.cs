using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OSharp.Utility.Extensions;

namespace HLACommonLib.Model.PK
{
    public class PKDeliverErrorBox
    {
        public string BOXGUID { get; set; }
        public string PICK_TASK { get; set; }
        public string PICK_TASK_ITEM { get; set; }
        public string LOUCENG { get; set; }
        public string PARTNER { get; set; }
        public DateTime SHIP_DATE { get; set; }
        public string HU { get; set; }
        public string MATNR { get; set; }

        public string BARCD { get; set; }
        public string ZSATNR { get; set; }
        public string ZCOLSN { get; set; }
        public string ZSIZTX { get; set; }
        /// <summary>
        /// 该箱指定下架单应发总数
        /// </summary>
        public long QUAN { get; set; }
        public long SHORTQTY { get; set; }
        /// <summary>
        /// 该箱指定下架单当前数量
        /// </summary>
        public long REAL { get; set; }
        /// <summary>
        /// 该箱指定下架单当前辅标签数量
        /// </summary>
        public long ADD_REAL { get; set; }
        /// <summary>
        /// 该箱指定下架单差异数量
        /// </summary>
        public long DIFF { get; set; }
        public string REMARK { get; set; }
        /// <summary>
        /// 是否异常记录
        /// </summary>
        public bool IsError { get; set; }
        /// <summary>
        /// 错误类型
        /// </summary>
        public DeliverRecordType RecordType { get; set; }

        public static PKDeliverErrorBox BuildPKDeliverErrorBox(DataRow row)
        {
            PKDeliverErrorBox result = new PKDeliverErrorBox();
            result.BOXGUID = row["BOXGUID"].CastTo<string>("");
            result.LOUCENG = row["LOUCENG"].CastTo<string>("");
            result.PICK_TASK = row["PICK_TASK"].CastTo<string>("");
            result.PICK_TASK_ITEM = row["PICK_TASK_ITEM"].CastTo<string>("");
            result.PARTNER = row["PARTNER"].CastTo<string>("");
            result.SHIP_DATE = row["SHIP_DATE"].CastTo<DateTime>(DateTime.Now.Date);
            result.SHORTQTY = row["SHORTQTY"].CastTo<long>(0);
            result.HU = row["HU"].CastTo<string>("");
            result.MATNR = row["MATNR"] != null ? row["MATNR"].ToString() : null;
            result.ZSATNR = row["ZSATNR"].CastTo<string>("");
            result.ZCOLSN = row["ZCOLSN"].CastTo<string>("");
            result.ZSIZTX = row["ZSIZTX"].CastTo<string>("");
            result.QUAN = row["QUAN"].CastTo<long>(0);
            result.REAL = row["REAL"].CastTo<long>(0);
            result.ADD_REAL = row["ADD_REAL"].CastTo<long>(0);
            result.DIFF = row["DIFF"].CastTo<long>(0);
            result.REMARK = row["REMARK"].CastTo<string>("");
            return result;
        }


    }

    public enum DeliverRecordType
    {
        正常 = 0,
        少拣 = 1,
        多拣 = 2,
        拣错 = 3,
        其他 = 10
    }
}
