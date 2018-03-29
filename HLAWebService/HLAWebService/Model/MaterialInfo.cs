using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLAWebService.Model
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
    }
}