using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 漳州通道机专用
    /// </summary>
    public class InventoryResult
    {
        /// <summary>
        /// 盘点结果 1：正常 2：重检 3：异常 4：延时检测
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// 盘点结果描述
        /// </summary>
        public string Message { get; set; }
    }
}
