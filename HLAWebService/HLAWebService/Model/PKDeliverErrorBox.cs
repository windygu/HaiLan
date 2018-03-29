using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace HLAWebService.Model
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
    }
}