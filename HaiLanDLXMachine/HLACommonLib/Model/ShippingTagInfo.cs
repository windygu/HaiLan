using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 发运信息
    /// </summary>
    public class ShippingTagInfo
    {
        /// <summary>
        /// 发运日期
        /// </summary>
        public DateTime SHIP_DATE { get; set; }
        /// <summary>
        /// 合作伙伴
        /// </summary>
        public string STORE_ID { get; set; }
        /// <summary>
        /// 合作伙伴名称
        /// </summary>
        public string STORE_NAME { get; set; }
        /// <summary>
        /// 集货区标识
        /// </summary>
        public string DISPATCH_AREA { get; set; }
        /// <summary>
        /// 当天集货波次
        /// </summary>
        public string COLLECT_WAVE { get; set; }
        /// <summary>
        /// 货运方式
        /// </summary>
        public string VSART_DES { get; set; }
        /// <summary>
        /// 线路
        /// </summary>
        public string ROUTE_NO { get; set; }
        /// <summary>
        /// 合作伙伴地址
        /// </summary>
        public string ADDRESS { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string TEL_NUMBER { get; set; }
        /// <summary>
        /// 发运滑道
        /// </summary>
        public string LANE_ID { get; set; }
    }
}
