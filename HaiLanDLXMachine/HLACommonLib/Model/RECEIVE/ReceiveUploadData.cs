using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OSharp.Utility.Extensions;

namespace HLACommonLib.Model.RECEIVE
{
    public class ReceiveUploadData
    {
        public string Guid { get; set; }
        public string Data { get; set; }
        public int IsUpload { get; set; }
        public DateTime CreateTime { get; set; }
        public string Device { get; set; }
        public string SapStatus { get; set; }
        public string SapResult { get; set; }
        public string Hu { get; set; }
    }
}
