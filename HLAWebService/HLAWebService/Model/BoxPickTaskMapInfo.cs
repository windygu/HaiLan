using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLAWebService.Model
{
    public class BoxPickTaskMapInfo
    {
        public string HU { get; set; }
        public string PICK_TASK { get; set; }
        public string PARTNER { get; set; }
        public string LOUCENG { get; set; }
        public DateTime? SHIP_DATE { get; set; }
        public string PACKMAT { get; set; }
        public float QUAN { get; set; }
        public bool IS_SCAN { get; set; }
    }
}