using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 电商箱复核记录
    /// </summary>
    public class EbBoxCheckRecordInfo
    {
        public long Id { get; set; }
        /// <summary>
        /// 箱码
        /// </summary>
        public string HU { get; set; }
        /// <summary>
        /// 预计数量
        /// </summary>
        public int PQTY { get; set; }
        /// <summary>
        /// 实际数量
        /// </summary>
        public int RQTY { get; set; }
        /// <summary>
        /// 状态
        /// 1正常 0异常
        /// </summary>
        public int STATUS { get; set; }
    }
}
