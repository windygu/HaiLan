using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    public class DeliverDetailInfo
    {
        public string LOUCENG { get; set; }
        public DateTime SHIP_DATE { get; set; }
        public string PARTNER { get; set; }
        public string ZSATNR { get; set; }
        public string ZCOLSN { get; set; }
        public string ZSIZTX { get; set; }
        public long QUAN { get; set; }
        public long REAL { get; set; }
        public long DIFF { get; set; }
    }
}
