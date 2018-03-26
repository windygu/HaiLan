using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OSharp.Utility.Extensions;
using System.Data;

namespace HLACommonLib.Model.PACKING
{
    /// <summary>
    /// 自定义退货类型
    /// </summary>
    public class ReturnTypeInfo
    {
        /// <summary>
        /// 品号
        /// </summary>
        public string ZPRDNR { get; set; }
        /// <summary>
        /// 色号
        /// </summary>
        public string ZCOLSN { get; set; }
        /// <summary>
        /// 退货类型
        /// </summary>
        public string THTYPE { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamps { get; set; }

        public static ReturnTypeInfo BuildReturnType(DataRow row)
        {
            ReturnTypeInfo result = new ReturnTypeInfo();
            result.THTYPE = row["THTYPE"].CastTo<string>("");
            result.ZPRDNR = row["ZPRDNR"].CastTo<string>("");
            result.ZCOLSN = row["ZCOLSN"].CastTo<string>("");
            return result;
        }
    }
}
