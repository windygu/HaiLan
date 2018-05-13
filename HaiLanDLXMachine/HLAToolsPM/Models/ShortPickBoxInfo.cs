using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLATools.Models
{
    public class ShortPickBoxInfo
    {
        public string HU { get; set; }
        public string PICK_TASK { get; set; }
        public string PICK_TASK_ITEM { get; set; }
        public string MATNR { get; set; }
        public string ZSATNR { get; set; }

        public string ZCOLSN { get; set; }
        public string ZSIZTX { get; set; }
        public int QTY { get; set; }
        /// <summary>
        /// 实际主条码数量
        /// </summary>
        public int RQTY { get; set; }
        /// <summary>
        /// 实际辅条码数量
        /// </summary>
        public int Add_RQTY { get; set; }
        public int SHORTQTY
        {
            get
            {
                if (ISLAST == "是")
                    return QTY - RQTY;
                else
                    return 0;
            }
        }
        public string ISLAST { get; set; }
    }
}
