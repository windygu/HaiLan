using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLAWebService.Model
{
    public class HLATagInfo
    {
        public long Id { get; set; }
        /// <summary>
        /// 产品编码 
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string CHARG { get; set; }
        /// <summary>
        /// 主条码
        /// </summary>
        public string BARCD { get; set; }
        /// <summary>
        /// 辅条码
        /// </summary>
        public string BARCD_ADD { get; set; }
        /// <summary>
        /// 主条码EPC
        /// </summary>
        public string RFID_EPC { get; set; }
        /// <summary>
        /// 辅条码EPC
        /// </summary>
        public string RFID_ADD_EPC { get; set; }
        /// <summary>
        /// 失效标记（有值即为失效）
        /// </summary>
        public string BARDL { get; set; }
    }
}