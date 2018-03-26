using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 电商大通道机-箱信息
    /// </summary>
    public class EbBoxInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 箱号
        /// </summary>
        public string HU { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string PRODUCTNO { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int QTY { get; set; }
        /// <summary>
        /// 发运日期
        /// </summary>
        public DateTime SHIPDATE { get; set; }
    }
}
