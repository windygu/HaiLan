using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLAWebService.Model
{
    public class TagDetailInfo
    {
        /// <summary>
        /// 实际EPC信息
        /// </summary>
        public string EPC { get; set; }
        /// <summary>
        /// 主条码EPC
        /// </summary>
        public string RFID_EPC { get; set; }
        /// <summary>
        /// 辅条码EPC
        /// </summary>
        public string RFID_ADD_EPC { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 主条码
        /// </summary>
        public string BARCD { get; set; }
        /// <summary>
        /// 辅条码
        /// </summary>
        public string BARCD_ADD { get; set; }
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
        /// 批次
        /// </summary>
        public string CHARG { get; set; }
        /// <summary>
        /// 箱规
        /// </summary>
        public int PXQTY { get; set; }
        /// <summary>
        /// 是否为辅条码epc
        /// </summary>
        public bool IsAddEpc { get; set; }
    }
}