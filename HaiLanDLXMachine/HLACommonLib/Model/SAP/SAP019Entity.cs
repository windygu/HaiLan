using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model.SAP
{
    public class SAP019Entity
    {
        public string BARCD { get; set; }
        public string PICK_TASK { get; set; }
        public long QTY { get; set; }
        public long Err_Qty { get; set; }
        public long DJ_QTY { get; set; }
    }
}
