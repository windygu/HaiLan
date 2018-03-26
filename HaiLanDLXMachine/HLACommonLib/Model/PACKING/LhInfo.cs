using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model.PACKING
{
    /// <summary>
    /// 楼号信息
    /// </summary>
    public class LhInfo
    {
        /// <summary>
        /// 楼号
        /// </summary>
        public string Lh { get; set; }
        /// <summary>
        /// 入库控制标识
        /// </summary>
        public string InTag { get; set; }
        /// <summary>
        /// 退货类型
        /// </summary>
        public string ReturnType { get; set; }
    }
}
