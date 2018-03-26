using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 电商箱异常信息表
    /// </summary>
    public class EbBoxErrorRecordInfo
    {
        public long Id { get; set; }
        /// <summary>
        /// 箱码
        /// </summary>
        public string HU { get; set; }
        /// <summary>
        /// 产品编码
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
        /// 差异
        /// </summary>
        public int DIFF { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK { get; set; }
    }
}
