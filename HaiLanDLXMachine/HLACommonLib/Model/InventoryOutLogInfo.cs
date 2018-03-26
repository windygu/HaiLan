using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 下架单
    /// </summary>
    public class InventoryOutLogInfo
    {
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string LGNUM { get; set; }
        /// <summary>
        /// 下架单号
        /// </summary>
        public string PICK_TASK { get; set; }
        /// <summary>
        /// 下架单行项目号
        /// </summary>
        public string PICK_TASK_ITEM { get; set; }
        /// <summary>
        /// 交货单号
        /// </summary>
        public string DOCNO { get; set; }
        /// <summary>
        /// 交货单行项目号
        /// </summary>
        public string ITEMNO { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public string PRODUCTNO { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public double QTY { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string UOM { get; set; }
        /// <summary>
        /// 发运日期
        /// </summary>
        public DateTime SHIP_DATE { get; set; }
        /// <summary>
        /// 业务伙伴编号（门店）
        /// </summary>
        public string PARTNER { get; set; }
        /// <summary>
        /// 仓位
        /// </summary>
        public string STOBIN { get; set; }
        /// <summary>
        /// 存储类型（系统）
        /// </summary>
        public string LGTYP { get; set; }
        /// <summary>
        /// 存储类型（实际）
        /// </summary>
        public string LGTYP_R { get; set; }
        /// <summary>
        /// 下架单类型（’2’的为RFID下架单）
        /// </summary>
        public string ZXJD_TYPE { get; set; }
    }
}
