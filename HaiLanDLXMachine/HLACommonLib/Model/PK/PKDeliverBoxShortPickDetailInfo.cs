using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model.PK
{
    /// <summary>
    /// 发货箱短拣明细
    /// </summary>
    public class PKDeliverBoxShortPickDetailInfo
    {
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string LGNUM { get; set; }
        /// <summary>
        /// 箱码
        /// </summary>
        public string HU { get; set; }
        /// <summary>
        /// 下架单号
        /// </summary>
        public string PICK_TASK { get; set; }
        /// <summary>
        /// 下架单行项目号
        /// </summary>
        public string PICK_TASK_ITEM { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int QTY { get; set; }

        public int DJQTY { get; set; }
        /// <summary>
        /// 实际主条码数量
        /// </summary>
        public int REAL_QTY { get; set; }
        /// <summary>
        /// 实际辅条码数量
        /// </summary>
        public int ADD_REAL_QTY{ get; set; }
        public string ZSATNR { get; set; }
        public string ZCOLSN { get; set; }
        public string ZSIZTX { get; set; }

        public bool? HAS_ADD_TAG { get; set; }
    }
}
