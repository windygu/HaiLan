using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OSharp.Utility.Extensions;

namespace HLACommonLib.Model
{
    public class EpcDetail
    {
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string LGNUM { get; set; }
        /// <summary>
        /// 交货单号
        /// </summary>
        public string DOCNO { get; set; }
        /// <summary>
        /// 凭证类别
        /// </summary>
        public string DOCCAT { get; set; }
        /// <summary>
        /// 箱码
        /// </summary>
        public string HU { get; set; }
        /// <summary>
        /// EPC流水号
        /// </summary>
        public string EPC_SER { get; set; }
        /// <summary>
        /// 是否已上传 0未上传 1已上传
        /// </summary>
        public int Handled { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 检测结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public string Floor { get; set; }

        //public static EpcDetail BuildEpcDetail(DataRow row)
        //{
        //    EpcDetail result = new EpcDetail();
        //    result.DOCCAT = row["DOCCAT"].CastTo("");
        //    result.DOCNO = row["DOCNO"].CastTo("");
        //    result.EPC_SER = row["EPC_SER"].CastTo("");
        //    result.Floor = row["Floor"].CastTo("");
        //    result.Handled = row["Handled"].CastTo(0);
        //    result.HU = row["HU"].CastTo("");
        //    result.LGNUM = row["LGNUM"].CastTo("");
        //    result.Result = row["Result"].CastTo("");
        //    result.Timestamp = row["Timestamp"].CastTo(DateTime.MinValue);
        //    result.Id = row["Id"].CastTo(0);
        //    return result;
        //}
    }
}
