using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OSharp.Utility.Extensions;

namespace HLACommonLib.Model.YK
{
    public class YKBoxInfo
    {
        /// <summary>
        /// 箱码
        /// </summary>
        public string Hu { get; set; } 
        /// <summary>
        /// 源存储类型
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 目标存储类型
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamps { get; set; }
        /// <summary>
        /// 海澜设备号
        /// </summary>
        public string EquipHla { get; set; }
        /// <summary>
        /// 信达设备号
        /// </summary>
        public string EquipXindeco { get; set; }
        /// <summary>
        /// 是否交接
        /// </summary>
        public byte IsHandover { get; set; }
        /// <summary>
        /// 是否满箱
        /// </summary>
        public byte IsFull { get; set; }
        /// <summary>
        /// 提交用户
        /// </summary>
        public string SubUser { get; set; }
        /// <summary>
        /// sap返回结果 S成功 E失败
        /// </summary>
        public string SapStatus { get; set; }
        /// <summary>
        /// sap返回信息
        /// </summary>
        public string SapRemark { get; set; }
        /// <summary>
        /// 楼层号
        /// </summary>
        public string LouCeng { get; set; }
        /// <summary>
        /// 包装材料
        /// </summary>
        public string PackMat { get; set; }
        /// <summary>
        /// 交接号（当目标存储类型为Y001时才需要打印该字段）
        /// </summary>
        public string LIFNR { get; set; }
        /// <summary>
        /// 箱明细
        /// </summary>
        public List<YKBoxDetailInfo> Details = new List<YKBoxDetailInfo>();
        
        public static YKBoxInfo BuildBoxInfo(DataRow row)
        {
            YKBoxInfo result = new YKBoxInfo();
            if(row!=null)
            {
                result.EquipHla = row["EquipHla"] != null ? row["EquipHla"].ToString() : "";
                result.EquipXindeco = row["EquipXindeco"] != null ? row["EquipXindeco"].ToString() : "";
                result.Hu = row["Hu"] != null ? row["Hu"].ToString() : "";
                result.IsFull = row["IsFull"] != null ? byte.Parse(row["IsFull"].ToString()) : (byte)0;
                result.IsHandover = row["IsHandover"] != null ? byte.Parse(row["IsHandover"].ToString()) : (byte)0;
                result.Remark = row["Remark"] != null ? row["Remark"].ToString() : "";
                result.SapStatus = row["SapStatus"] != null ? row["SapStatus"].ToString() : "";
                result.SapRemark = row["SapRemark"] != null ? row["SapRemark"].ToString() : "";
                result.Source = row["Source"] != null ? row["Source"].ToString() : "";
                result.Status = row["Status"] != null ? row["Status"].ToString() : "";
                result.LouCeng = row["LouCeng"] != null ? row["LouCeng"].ToString() : "";
                result.SubUser = row["SubUser"] != null ? row["SubUser"].ToString() : "";
                result.Target = row["Target"] != null ? row["Target"].ToString() : "";
                result.PackMat = row["PackMat"] != null ? row["PackMat"].ToString() : "";
                result.LIFNR = row["LIFNR"] != null ? row["LIFNR"].ToString() : "";
                result.Timestamps = (row["Timestamps"] != null && row["Timestamps"].ToString().Trim() != "") ? DateTime.Parse(row["Timestamps"].ToString()) : DateTime.Now;
            }
            return result;
        }
    }
}
