using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    public class UploadEbBoxInfo
    {
        public string Guid { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string LGNUM { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EQUIP_HLA { get; set; }
        /// <summary>
        /// 箱码
        /// </summary>
        public string HU { get; set; }
        /// <summary>
        /// 箱码不一致的情况下，要全部箱码都上传一次，但是明细要一致
        /// </summary>
        public List<string> HuList { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime ChangeTime { get; set; }
        /// <summary>
        /// 盘点结果
        /// </summary>
        public bool InventoryResult { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 提交用户
        /// </summary>
        public string SubUser { get; set; }
        /// <summary>
        /// 吊牌明细信息
        /// </summary>
        public List<TagDetailInfo> TagDetailList { get; set; }
        /// <summary>
        /// 上传重试次数
        /// </summary>
        public int RetryTimes { get; set; }
    }
}
