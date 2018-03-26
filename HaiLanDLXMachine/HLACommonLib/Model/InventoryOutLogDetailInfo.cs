using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 下架单
    /// </summary>
    public class InventoryOutLogDetailInfo
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
        public int QTY { get; set; }
        public int QTY_ADD { get; set; }
        /// <summary>
        /// 实际数量
        /// </summary>
        public int REALQTY { get; set; }
        public int REALQTY_ADD { get; set; }

        public int DJQTY
        {
            get
            {
                return QTY - REALQTY;
            }
        }
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

        /*
        public string TYPE_STRING {
            get
            {
                return string.Format("{0}{1}",
                    ZXJD_TYPE == "2" ? "RFID" : "非RFID",
                    LGTYP != LGTYP_R ? "[拼]" : "");
            }
        }*/
        /// <summary>
        /// 是否下架 1是 0否
        /// </summary>
        public byte IsOut { get; set; }
        /// <summary>
        /// 满箱标记（’X’满箱；‘ ’尾箱）
        /// </summary>
        public string MX { get; set; }

        /// <summary>
        /// 装运类型
        /// </summary>
        public string VSART { get; set; }

        /// <summary>
        /// 是否为非波次
        /// </summary>
        public string IS_FBC { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string BRAND { get; set; }
        /*=============添加三禾数据 Z_EW_RFID_050 by linzw 160509===============*/
        ///// <summary>
        ///// 存储类型
        ///// </summary>
        //public string STOTYPER { get; set; }
        /// <summary>
        /// 数据来源
        ///// </summary>
        //public string DATA_SOURCE { get; set; }
        /// <summary>
        /// 数据状态：0为初始状态，9为已上传的数据
        /// </summary>
        public short Status { get; set; }
        /// <summary>
        /// 未扫描箱数（整箱发货软件专用，需要自行查询并赋值）
        /// </summary>
        public int UnScanBoxCount { get; set; }
    }
    /// <summary>
    /// 下架单
    /// </summary> 
}
