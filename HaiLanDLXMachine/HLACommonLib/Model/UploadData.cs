using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    public class UploadData
    {
        /// <summary>
        /// GUID主键
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 要上传的数据
        /// </summary>
        public ResultDataInfo Data { get; set; }

        /// <summary>
        /// 是否已上传 1是 0否
        /// </summary>
        public uint IsUpload { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    public class CUploadData
    {
        /// <summary>
        /// GUID主键
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 要上传的数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 是否已上传 1是 0否
        /// </summary>
        public uint IsUpload { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public string MSG { get; set; }
        public string REMARK { get; set; }

    }
}
