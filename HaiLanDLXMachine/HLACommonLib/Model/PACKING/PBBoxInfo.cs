using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OSharp.Utility.Extensions;

namespace HLACommonLib.Model.PACKING
{
    public class PBBoxInfo
    {

        public PBBoxInfo()
        {
            Timestamps = DateTime.Now;
            Details = new List<PBBoxDetailInfo>();
        }
        public string HU { get; set; }

        public string LH { get; set; }
        public int QTY { get; set; } 
        public string EQUIP { get; set; }
        public string RESULT { get; set; }

        public string MSG { get; set; }
        public string PACKRESULT { get; set; }

        public string PACKMSG { get; set; }
        public string MX { get; set; }
        /// <summary>
        /// 当前箱交接号（只针对交接楼号为TH的有效）
        /// </summary>
        public string LIFNR { get; set; }
        public DateTime Timestamps { get; set; }
        public List<PBBoxDetailInfo> Details { get; set; }


        public static PBBoxInfo BuildPBBoxInfo(DataRow row)
        {
            PBBoxInfo result = new PBBoxInfo();
            result.EQUIP = row?["EQUIP"].CastTo<string>("");
            result.HU = row?["HU"].CastTo<string>("");
            result.LH = row?["LH"].CastTo<string>("");
            result.MSG = row?["MSG"].CastTo<string>("");
            result.MX = row?["MX"].CastTo<string>("");
            result.PACKMSG = row?["PACKMSG"].CastTo<string>("");
            result.PACKRESULT = row?["PACKRESULT"].CastTo<string>("");
            result.QTY = row["QTY"].CastTo<int>(0);
            result.RESULT = row?["RESULT"].CastTo<string>("");
            result.LIFNR = row?["LIFNR"].CastTo<string>("");
            result.Timestamps = row["Timestamps"].CastTo<DateTime>(DateTime.Now.Date);
            return result;
        }
    }
}
