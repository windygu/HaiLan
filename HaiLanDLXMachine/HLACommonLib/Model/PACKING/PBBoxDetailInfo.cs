using OSharp.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model.PACKING
{
    public class PBBoxDetailInfo
    {
        public string EPC { get; set; }
        public string HU { get; set; }
        public string BARCD { get; set; }
        public string MATNR { get; set; }
        public string ZSATNR { get; set; }
        public string ZCOLSN { get; set; }
        public string ZSIZTX { get; set; }
        public int IsAdd { get; set; }

        public static PBBoxDetailInfo BuildPBBoxDetailInfo(DataRow row)
        {
            PBBoxDetailInfo result = new PBBoxDetailInfo();
            result.BARCD = row?["BARCD"].CastTo<string>("");
            result.EPC = row?["EPC"].CastTo<string>("");
            result.HU = row?["HU"].CastTo<string>("");
            result.IsAdd = (int)row?["IsAdd"].CastTo<int>(0);
            result.MATNR = row?["MATNR"].CastTo<string>("");
            result.ZCOLSN = row?["ZCOLSN"].CastTo<string>("");
            result.ZSATNR = row?["ZSATNR"].CastTo<string>("");
            result.ZSIZTX = row?["ZSIZTX"].CastTo<string>("");
            return result;
        }
    }
}
