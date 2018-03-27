using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLAPackingBoxChannelMachine.Models
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
        public UploadBoxInfo Data { get; set; }

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
