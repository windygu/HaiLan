using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    public class DocDetailInfo
    {
        public DocDetailInfo()
        {
            ZPBNO = ""; 
        }
        /// <summary>
        /// 交货单号
        /// </summary>
        public string DOCNO { get; set; }
        /// <summary>
        /// 交货单行项目号
        /// </summary>
        public string ITEMNO { get; set; }
        /// <summary>
        /// SKU（产品编码）
        /// </summary>
        public string PRODUCTNO { get; set; }
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
        /// 应收数量
        /// </summary>
        public int QTY { get; set; }
        /// <summary>
        /// 实收数量
        /// </summary>
        public int REALQTY { get; set; }
        /// <summary>
        /// 原始批次
        /// </summary>
        public string ZCHARG { get; set; }
        /// <summary>
        /// 行项目正确箱数
        /// </summary>
        public int BOXCOUNT { get; set; }
        /// <summary>
        /// 配比类型
        /// </summary>
        public string ZPBNO { get; set; }
    }
}
