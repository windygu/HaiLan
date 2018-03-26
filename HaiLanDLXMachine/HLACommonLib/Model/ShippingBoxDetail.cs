using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    public class ShippingBoxDetail
    {
        public long Id { get; set; }
        /// <summary>
        /// 箱码
        /// </summary>
        public string HU { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string UOM { get; set; }
        /// <summary>
        /// 品号
        /// </summary>
        public string ZSATNR { get; set; }
        /// <summary>
        /// 色号
        /// </summary>
        public string ZCOLSN { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string ZSIZTX { get; set; }
        /// <summary>
        /// 下架单号
        /// </summary>
        public string PICK_TASK { get; set; }
        public string EPC { get; set; }
        /// <summary>
        /// 是否是辅条码
        /// 1是 0否
        /// </summary>
        public byte IsADD { get; set; }
        /// <summary>
        /// 是否是RFID标签
        /// 1是 0否
        /// 如果是RFID标签，则EPC字段存储的是EPC，如果不是RFID标签，则EPC字段存储的条形码
        /// </summary>
        public byte IsRFID { get; set; }
        /// <summary>
        /// 是否上传
        /// </summary>
        public byte Handled { get; set; }
        /// <summary>
        /// 下架单行项目号
        /// </summary>
        public string PICK_TASK_ITEM { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string CHARG { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public double BRGEW { get; set; }

        /// <summary>
        /// 包装材料（发货）
        /// </summary>
        public string PACKMAT_FH { get; set; }

    }
}
