using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model.RECEIVE
{
    public class MixRatioInfo
    {
        /// <summary>
        /// 配比类型
        /// </summary>
        public string ZPBNO { get; set; }
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
        /// 配比件数
        /// </summary>
        public int QUAN { get; set; }
    }
}
