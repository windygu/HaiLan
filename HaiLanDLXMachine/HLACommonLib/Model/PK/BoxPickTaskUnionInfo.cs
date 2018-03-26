using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model.PK
{
    public class BoxPickTaskUnionInfo
    {
        public List<BoxPickTaskMapInfo> BoxPickTaskMapInfoList { get; set; }

        public List<InventoryOutLogDetailInfo> InventoryOutLogDetailList { get; set; }
    }
}
