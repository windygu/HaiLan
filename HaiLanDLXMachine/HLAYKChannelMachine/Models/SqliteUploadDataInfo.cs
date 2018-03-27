using HLACommonLib.Model.YK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLAYKChannelMachine.Models
{
    public class SqliteUploadDataInfo
    {
        /// <summary>
        /// GUID主键
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 要上传的数据
        /// </summary>
        public YKBoxInfo Data { get; set; }

        /// <summary>
        /// 是否已上传 1是 0否
        /// </summary>
        public uint IsUpload { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
