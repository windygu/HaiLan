using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    public class TagDetailInfo:ICloneable
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

        public string ZCOLSN_WFG { get; set; }
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
        /// 发货箱规
        /// </summary>
        public int PXQTY_FH { get; set; }
        /// <summary>
        /// 是否为辅条码epc
        /// </summary>
        public bool IsAddEpc { get; set; }
        /// <summary>
        /// 包装材料（收货）
        /// </summary>
        public string PACKMAT { get; set; }
        /// <summary>
        /// 包装材料（发货）
        /// </summary>
        public string PACKMAT_FH { get; set; }
        /// <summary>
        /// 交接号
        /// </summary>
        public List<string> LIFNRS { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public double BRGEW { get; set; }

        public string PUT_STRA { get; set; }

        public string MAKTX { get; set; }

        public object Clone()
        {
            TagDetailInfo re = new TagDetailInfo();

            re.BARCD = BARCD;
            re.BARCD_ADD = BARCD_ADD;
            re.BRGEW = BRGEW;
            re.CHARG = CHARG;
            re.EPC = EPC;
            re.IsAddEpc = IsAddEpc;
            re.LIFNRS = LIFNRS.ToList();
            re.MATNR = MATNR;
            re.PACKMAT = PACKMAT;
            re.PACKMAT_FH = PACKMAT_FH;
            re.PXQTY = PXQTY;
            re.PXQTY_FH = PXQTY_FH;
            re.RFID_ADD_EPC = RFID_ADD_EPC;
            re.RFID_EPC = RFID_EPC;
            re.ZCOLSN = ZCOLSN;
            re.ZCOLSN_WFG = ZCOLSN_WFG;
            re.ZSATNR = ZSATNR;
            re.ZSIZTX = ZSIZTX;
            re.PUT_STRA = PUT_STRA;
            re.MAKTX = MAKTX;
            return re;
        }
    }
}
