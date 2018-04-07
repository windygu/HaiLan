using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 每个箱子经过通道机检测之后，需要进行保存的数据
    /// </summary>
    public class ResultDataInfo
    {
        /// <summary>
        /// 通道机检测结果
        /// true 正常
        /// false 异常
        /// </summary>
        public bool InventoryResult { get; set; }
        /// <summary>
        /// 箱码
        /// </summary>
        public string BoxNO { get; set; }
        /// <summary>
        /// 该箱子的上一次检测结果
        /// </summary>
        public string LastResult { get; set; }
        /// <summary>
        /// 错误描述信息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 本箱子所属交货单号
        /// </summary>
        public DocInfo Doc { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string LGNUM { get; set; }
        /// <summary>
        /// 当前检测的标签信息
        /// </summary>
        public Dictionary<string, TagDetailInfoExtend> TdiExtendList { get; set; }
        /// <summary>
        /// 检测该箱时使用的运行模式
        /// </summary>
        public RunMode RunningMode { get; set; }
        /// <summary>
        /// 当前登录用户的ID
        /// </summary>
        public string CurrentUserId { get; set; }
        /// <summary>
        /// 当前楼层
        /// </summary>
        public string Floor { get; set; }
        /// <summary>
        /// 当前设备终端号
        /// </summary>
        public string sEQUIP_HLA { get; set; }
        /// <summary>
        /// 是否重投
        /// </summary>
        public bool IsRecheck { get; set; }
        /// <summary>
        /// 本轮扫描到的标签数
        /// </summary>
        public int CurrentNum { get; set; }
        /// <summary>
        /// 主铺条码汇总数据 来自lvTagDetail
        /// </summary>
        public List<ListViewTagInfo> LvTagInfo { get; set; }
        /// <summary>
        /// 本轮的epc列表
        /// </summary>
        public List<string> EpcList { get; set; }
        /// <summary>
        /// 1 交接单
        /// 2 交货单
        /// </summary>
        public int ReceiveType { get; set; }
        /// <summary>
        /// 配比类型
        /// </summary>
        public string ZPBNO { get; set; }
    }

    public class ListViewTagInfo 
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        public string MATNR { get; private set; }
        /// <summary>
        /// 品号
        /// </summary>
        public string ZSATNR { get; private set; }
        /// <summary>
        /// 色号
        /// </summary>
        public string ZCOLSN { get; private set; }
        /// <summary>
        /// 规格 
        /// </summary>
        public string ZSIZTX { get; private set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string CHARG { get; private set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int QTY { get; set; }

        public ListViewTagInfo(string _MATNR, string _ZSATNR, string _ZCOLSN, string _ZSIZTX, string _CHARG, int _QTY)
        {
            MATNR = _MATNR;
            ZSATNR = _ZSATNR;
            ZCOLSN = _ZCOLSN;
            ZSIZTX = _ZSIZTX;
            CHARG = _CHARG;
            QTY = _QTY;
        }
    }
}
