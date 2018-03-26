using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OSharp.Utility.Extensions;

namespace HLACommonLib.Model
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
        /// <summary>
        /// 交接号
        /// </summary>
        public string LIFNR { get; set; }
        public static HLATagInfo BuildHLATagInfo(DataRow row)
        {
            HLATagInfo result = new HLATagInfo();

            result.BARCD = row["BARCD"].CastTo("");
            result.Id = row["Id"].CastTo(0);
            result.MATNR = row["MATNR"].CastTo("");
            result.RFID_ADD_EPC = row["RFID_ADD_EPC"].CastTo("");
            result.RFID_EPC = row["RFID_EPC"].CastTo("");
            result.CHARG = row["CHARG"].CastTo("");
            result.BARCD_ADD = row["BARCD_ADD"].CastTo("");
            result.BARDL = row["BARDL"].CastTo("");
            result.LIFNR = row["LIFNR"].CastTo("");

            return result;
        }
    }
}
