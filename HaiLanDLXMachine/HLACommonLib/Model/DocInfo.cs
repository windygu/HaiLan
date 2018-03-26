using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    public class DocInfo : ICloneable
    {
        public DocInfo()
        {
            ZYPXFLG = "";
        }
        /// <summary>
        /// 交货单号
        /// </summary>
        public string DOCNO { get; set; }
        /// <summary>
        /// 凭证类型
        /// </summary>
        public string DOCTYPE { get; set; }
        /// <summary>
        /// 卸载完成（’X’卸载完成）
        /// </summary>
        public string ZXZWC { get; set; }
        /// <summary>
        /// 质检完成（’A’通过,’D’拒绝）
        /// </summary>
        public string ZZJWC { get; set; }
        /// <summary>
        /// 实际收货日期
        /// </summary>
        public string GRDATE { get; set; }
        /// <summary>
        /// 整箱发放标记
        /// </summary>
        public string ZYPXFLG { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
