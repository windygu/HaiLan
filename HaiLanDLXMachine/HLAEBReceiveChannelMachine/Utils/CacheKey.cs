using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLAEBReceiveChannelMachine.Utils
{
    public class CacheKey
    {
        /// <summary>
        /// 异常记录缓存key
        /// </summary>
        public const string ERROR_RECORD = "ERRORRECORD";
        /// <summary>
        /// 装箱检查记录缓存key
        /// </summary>
        public const string CHECK_RECORD = "CHECKRECORD";
        /// <summary>
        /// 物料数据缓存key
        /// </summary>
        public const string MATERIAL = "MATERIAL";
        /// <summary>
        /// 吊牌数据缓存key
        /// </summary>
        public const string TAG = "TAG";
    }
}
