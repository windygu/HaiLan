using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 下架单拣货错误记录
    /// </summary>
    public class OutLogErrorRecord
    {
        /// <summary>
        /// 所属下架单
        /// </summary>
        public string PICK_TASK { get; set; }
        /// <summary>
        /// 标签数据
        /// </summary>
        public string EPC { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 条码数据
        /// </summary>
        public string BARCD { get; set; }
        /// <summary>
        /// 拣错、多拣、少拣
        /// </summary>
        public ErrorType ERRTYPE { get; set; }
        /// <summary>
        /// 数量，仅对少拣的情况生效-add by jrzhuang 20160805
        /// </summary>
        public int QTY { get; set; }
    }

    public enum ErrorType
    { 
        拣错=1,
        多拣=2,
        少拣=3
    }
}
