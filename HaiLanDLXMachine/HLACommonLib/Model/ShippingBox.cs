using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 发运箱主各
    /// </summary>
    public class ShippingBox
    {
        /// <summary>
        /// 箱码
        /// </summary>
        public string HU { get; set; }
        public bool IsMargeHU { get; set; }// 是否拼箱
        public string DisplayHU {
            get
            {
                return HU;
                //return IsMargeHU ? HU + "[拼]" : HU;
            }
        }
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string LGNUM { get; set; }
        /// <summary>
        /// 发运日期
        /// </summary>
        public DateTime SHIP_DATE { get; set; }
        /// <summary>
        /// 所属门店
        /// </summary>
        public string PARTNER { get; set; }
        /// <summary>
        /// 所属楼层
        /// </summary>
        public string Floor { get; set; }
        /// <summary>
        /// 是否满箱
        /// 1是 0否
        /// </summary>
        public byte IsFull { get; set; }
        /// <summary>
        /// 件数
        /// </summary>
        public int QTY { get; set; }
        public int AddQTY { get; set; }
        /// <summary>
        /// 箱型ID
        /// </summary>
        public string PMAT_MATNR { get; set; }
        /// <summary>
        /// 箱型显示内容
        /// </summary>
        public string MAKTX { get; set; }
        /// <summary>
        /// SKU数
        /// </summary>
        public int SKUCOUNT { get; set; }
        /// <summary>
        /// 是否扫描箱[同一个下架单中只能有一个扫描箱]
        /// 1是 0否
        /// </summary>
        public byte IsScanBox { get; set; }

        public string IsScanBoxString 
        {
            get
            {
                if (IsScanBox == 1)
                    return "扫描箱";
                else
                    return "非扫描箱";
            }
        }
        /// <summary>
        /// 发运箱明细
        /// </summary>
        public List<ShippingBoxDetail> Details { get; set; }
    }
}
