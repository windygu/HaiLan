using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 版本管理信息
    /// </summary>
    public class VersionsInfo
    {
        public long Id { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 下载URL
        /// </summary>
        public string DownloadUrl { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// 更新日志
        /// </summary>
        public string UpdateLog { get; set; }
    }
}
