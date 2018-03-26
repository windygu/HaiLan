using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    public class MaterialInfo
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 品号
        /// </summary>
        public string ZSATNR { get; set; }
        /// <summary>
        /// 色号
        /// </summary>
        public string ZCOLSN { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string ZSIZTX { get; set; }
        /// <summary>
        /// 商品大类
        /// </summary>
        public string ZSUPC2 { get; set; }
        /// <summary>
        /// 箱规
        /// </summary>
        public int PXQTY { get; set; }
        /// <summary>
        /// 发货箱规
        /// </summary>
        public int PXQTY_FH { get; set; }
        /// <summary>
        /// 控制标识
        /// </summary>
        public string PUT_STRA { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public double BRGEW { get; set; }
        /// <summary>
        /// 色号流水号
        /// </summary>
        public string ZCOLSN_WFG { get; set; }
        /// <summary>
        /// 收货包材
        /// </summary>
        public string PXMAT { get; set; }
        /// <summary>
        /// 发货包材
        /// </summary>
        public string PXMAT_FH { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        public string MAKTX { get; set; }

    }
}
