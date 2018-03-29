using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLAWebService.Model
{
    public class PKDeliverBox
    {
        public string GUID { get; set; }
        public string LOUCENG { get; set; }
        public string PARTNER { get; set; }
        public DateTime SHIP_DATE { get; set; }
        public string HU { get; set; }
        public string RESULT { get; set; }
        public string REMARK { get; set; }
    }
}