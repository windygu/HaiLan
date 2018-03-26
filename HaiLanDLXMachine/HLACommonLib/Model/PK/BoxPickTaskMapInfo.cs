using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    public class BoxPickTaskMapInfo
    {
        /// <summary>
        /// 箱码
        /// </summary>
        public string HU { get; set; }
        /// <summary>
        /// 下架单号
        /// </summary>
        public string PICK_TASK { get; set; }
        /// <summary>
        /// 门店
        /// </summary>
        public string PARTNER { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public string LOUCENG { get; set; }
        /// <summary>
        /// 发运日期
        /// </summary>
        public DateTime? SHIP_DATE { get; set; }
        /// <summary>
        /// 包装物料
        /// </summary>
        public string PACKMAT { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public float QUAN { get; set; }
        /// <summary>
        /// 是否扫描
        /// </summary>
        public bool IS_SCAN { get; set; }
        /// <summary>
        /// 是否短拣
        /// </summary>
        public bool IS_SHORT_PICK { get; set; }
    }
}
