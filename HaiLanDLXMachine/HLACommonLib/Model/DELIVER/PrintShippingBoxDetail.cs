using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model.DELIVER
{
    /// <summary>
    /// 用于SAP远程打印的下架单明细类
    /// </summary>
    public class PrintShippingBoxDetail
    {
        public string MATNR { get; set; }
        public string UOM { get; set; }
        public string QTY { get; set; }
    }
}
