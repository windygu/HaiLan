using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model.PACKING
{
    /// <summary>
    /// 自定义箱规
    /// </summary>
    public class BoxQtyInfo
    {
        /// <summary>
        /// SKU产品编码
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 自定义箱规
        /// </summary>
        public int PXQTY { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamps { get; set; }
    }
}
