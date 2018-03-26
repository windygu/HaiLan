using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 错误记录
    /// </summary>
    public class ErrorRecord
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 箱码
        /// </summary>
        public string HU { get; set; }
        /// <summary>
        /// 配比类型
        /// </summary>
        public string ZPBNO { get; set; }
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
        /// 数量
        /// </summary>
        public int QTY { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK { get; set; }
        /// <summary>
        /// 结果 S正常 E异常
        /// </summary>
        public string RESULT { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }
        /// <summary>
        /// 交货单号
        /// </summary>
        public string DOCNO { get; set; }

        public string SapStatus { get; set; }
        public string SapResult { get; set; }
    }
}
