using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 箱码信息
    /// </summary>
    public class HuInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string LGNUM { get; set; }
        /// <summary>
        /// 箱码
        /// </summary>
        public string HU { get; set; }
        /// <summary>
        /// 检测结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 实收数量
        /// </summary>
        public int QTY { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public string Floor { get; set; }
    }
}
