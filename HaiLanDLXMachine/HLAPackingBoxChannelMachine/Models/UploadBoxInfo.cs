using HLACommonLib.Model.PACKING;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLAPackingBoxChannelMachine.Models
{
    public class UploadBoxInfo
    {
        public PBBoxInfo Box { get; set; }

        public string EQUIP_HLA { get; set; }

        public string LOUCENG { get; set; }

        public string SUBUSER { get; set; }

        public string LGNUM { get; set; }

    }
}
