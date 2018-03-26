using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 单检机箱码EPC
    /// </summary>
    public class EPCRecord
    {
        /// <summary>
        /// 箱码
        /// </summary>
        public string HU { get; set; }
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
        /// EPC 
        /// </summary>
        public string EPC { get; set; }

    }
}
