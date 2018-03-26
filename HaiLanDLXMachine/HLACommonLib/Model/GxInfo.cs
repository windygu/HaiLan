using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 工序信息
    /// </summary>
    public class GxInfo
    {
        /// <summary>
        /// 工序编码
        /// </summary>
        public string GX_CODE { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        public string GX_NAME { get; set; }
        /// <summary>
        /// 组号
        /// </summary>
        public string VIEWGROUP { get; set; }
        /// <summary>
        /// 复核员工号
        /// </summary>
        public string VIEWUSR { get; set; }
    }
}
