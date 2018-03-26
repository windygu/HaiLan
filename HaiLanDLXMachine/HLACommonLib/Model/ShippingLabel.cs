using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    public class ShippingLabel
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
        /// 线路描述
        /// </summary>
        public string ROUTE_DES { get; set; }
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

        /// <summary>
        /// 发运大厅
        /// </summary>
        public string FYDT { get; set; }

        /// <summary>
        /// 装运类型
        /// </summary>
        public string VSART { get; set; }

        /// <summary>
        /// 月台编码
        /// </summary>
        public string ZYT { get; set; }

        /// <summary>
        /// 发运波次
        /// </summary>
        public string FY_WAVE { get; set; }

        /// <summary>
        /// 波次+月台+发运波次
        /// </summary>
        public string WAVE_AND_YT { get; set; }

        /// <summary>
        /// 是否为非波次
        /// </summary>
        public string IS_FBC { get; set; }
        /// <summary>
        /// 集货顺序
        /// </summary>
        public string COLLECT_SEQ { get; set; }
        /// <summary>
        /// 特殊处理标志
        /// </summary>
        public string ZSDABW { get; set; }
        /// <summary>
        /// 特殊处理标志描述
        /// </summary>
        public string ZSDABW_DES { get; set; }
        /// <summary>
        /// 凭证编号
        /// </summary>
        public string DOCNO { get; set; }

        /// <summary>
        /// 集货日期
        /// </summary>
        public DateTime PICK_DATE { get; set; }

    }
}
