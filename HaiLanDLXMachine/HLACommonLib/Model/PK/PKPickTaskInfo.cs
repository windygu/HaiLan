using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model.PK
{
    public class PKPickTaskInfo
    {
        public string HU { get; set; }
        public string PICK_TASK { get; set; }
        public string PICK_TASK_ITEM { get; set; }
        public string MATNR { get; set; }
        public DateTime SHIP_DATE { get; set; }
        public string ZSATNR { get; set; }

        public string ZCOLSN { get; set; }
        public string ZSIZTX { get; set; }
        /// <summary>
        /// 下架单总应发数量
        /// </summary>
        public int QTY { get; set; }
        public int REAL_QTY { get; set; }
        public int ADD_REAL_QTY { get; set; }
        public int Current_QTY { get; set; }
        public int ADD_Current_QTY { get; set; }
        /// <summary>
        /// 是否尾箱
        /// </summary>
        public bool ISLASTBOX { get; set; }
        /// <summary>
        /// 是否有辅条码
        /// </summary>
        public bool? HAS_ADD_TAG { get; set; }
    }
}
