using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model.PK
{
    public class UploadPKBoxInfo
    {
        public string Guid { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string LGNUM { get; set; }
        /// <summary>
        /// 发运日期
        /// </summary>
        public DateTime SHIP_DATE { get; set; }
        /// <summary>
        /// 门店
        /// </summary>
        public string PARTNER { get; set; }
        /// <summary>
        /// 箱码
        /// </summary>
        public string HU { get; set; }
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
        /// 楼层
        /// </summary>
        public string LOUCENG { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EQUIP_HLA { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime ChangeTime { get; set; }
        /// <summary>
        /// 吊牌明细信息
        /// </summary>
        public List<TagDetailInfo> TagDetailList { get; set; }
        /// <summary>
        /// 该箱码包含的箱明细列表
        /// </summary>
        public List<PKDeliverErrorBox> DeliverErrorBoxList { get; set; }
        /// <summary>
        /// 上传重试次数
        /// </summary>
        public int RetryTimes { get; set; }
        /// <summary>
        /// 上传后sap返回的信息
        /// </summary>
        public string UploadMsg { get; set; }

        public bool IsJieHuoDanDeliver { get; set; }
        public bool IsWholeDeviver { get; set; }
    }
}
