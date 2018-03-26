using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    public class SapResult
    {
        public SapResult()
        {
            STATUS = "E";
            MSG = "未调用SAP接口";
        }
        public bool SUCCESS
        {
            get
            {
                return STATUS.ToUpper().Trim() == "S" ? true : false;
            }
        }
        public string STATUS { get; set; }

        public string MSG { get; set; }
    }
}
