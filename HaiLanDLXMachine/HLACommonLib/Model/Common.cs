using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OSharp.Utility.Extensions;
using ThingMagic;

namespace HLACommonLib.Model
{
    public class CBarQty
    {
        public string barcd;
        public int qty;
        public CBarQty(string bar,int q)
        {
            barcd = bar;
            qty = q;
        }
    }
    public class CCancelDocData
    {
        public string hu;
        public bool mIsHz;
        public bool mIsDd;
        public bool mIsCp;
        public bool mIsRFID;
        public List<CBarQty> barQty = new List<CBarQty>();

        public void addBarQty(string bar,int qty)
        {
            if (string.IsNullOrEmpty(bar))
                return;

            if(barQty.Exists(i=>i.barcd == bar))
            {
                barQty.FirstOrDefault(j => j.barcd == bar).qty += qty;
            }
            else
            {
                barQty.Add(new CBarQty(bar, qty));
            }
        }
    }
    public class CCancelDoc
    {
        public string doc;
        public List<CCancelDocData> docData = new List<CCancelDocData>();
    }
    public class CCancelUpload
    {
        public string lgnum;
        public string boxno;
        public string subuser;
        public bool inventoryRe;
        public string equipID;
        public string loucheng;
        public string docno;
        public string dianshuBoCi;

        public bool isHZ;

        public List<TagDetailInfo> tagDetailList = new List<TagDetailInfo>();
        public List<string> epcList = new List<string>();
    }

    public class ZLKBoxInfo
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
        public List<ZLKBoxDetailInfo> Details = new List<ZLKBoxDetailInfo>();

        public static ZLKBoxInfo BuildBoxInfo(DataRow row)
        {
            ZLKBoxInfo result = new ZLKBoxInfo();
            if (row != null)
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

    public class ZLKBoxDetailInfo
    {
        public long Id { get; set; }
        public string Hu { get; set; }
        public string Epc { get; set; }

        public string Matnr { get; set; }

        public string Zsatnr { get; set; }
        public string Zcolsn { get; set; }
        public string Zsiztx { get; set; }
        public string Barcd { get; set; }
        public DateTime Timestamps { get; set; }
        public byte IsAdd { get; set; }

        public static ZLKBoxDetailInfo BuildBoxDetailInfo(DataRow row)
        {
            ZLKBoxDetailInfo result = new ZLKBoxDetailInfo();
            if (row != null)
            {
                result.Barcd = row["Barcd"] != null ? row["Barcd"].ToString() : "";
                result.Epc = row["Epc"] != null ? row["Epc"].ToString() : "";
                result.Hu = row["Hu"] != null ? row["Hu"].ToString() : "";
                result.Id = row["Id"] != null ? long.Parse(row["Id"].ToString()) : 0;
                result.IsAdd = row["IsAdd"].CastTo<byte>((byte)0);
                result.Matnr = row["Matnr"] != null ? row["Matnr"].ToString() : "";
                result.Zcolsn = row["Zcolsn"] != null ? row["Zcolsn"].ToString() : "";
                result.Zsatnr = row["Zsatnr"] != null ? row["Zsatnr"].ToString() : "";
                result.Zsiztx = row["Zsiztx"] != null ? row["Zsiztx"].ToString() : "";
                result.Timestamps = row["Timestamps"] != null ? DateTime.Parse(row["Timestamps"].ToString()) : DateTime.Now;
            }
            return result;
        }
    }

    public class CStoreQuery
    {
        public string barcd;
        public string storeid;
        public string status;
        public string msg;
        public string hu;
        public int pxqty_fh;
        public string flag;
        public string equip_hla;
        public string loucheng;
        public string date;
        public string time;
    }

    public class CDeliverStoreQuery
    {
        public string bar;
        public string store;
        public string mtr;
        public string msg;
        public string status;
        public string shipDate;
        public string hu;
    }

    public class CJieHuoDan
    {
        public string PICK_LIST = "";
        public List<CJieHuoDanDetail> mJieHuo = new List<CJieHuoDanDetail>();
        public string mStatus = "";
        public string mMsg = "";
    }
    public class CJieHuoDanDetail
    {
        public string PICK_LIST = "";
        public string PICK_LIST_ITEM = "";
        public string PRODUCTNO = "";
        public int QTY = 0;
        public string SHIP_DATE = "";
        public string PICK_DATE = "";
        public string WAVEID = "";
        public string EXPORT_NO = "";
        public string STOTYPE = "";
        public string STOBIN = "";
    }

    public class CJianHuoContrastRe
    {
        public string mat = "";
        public string barcd = "";
        public string p = "";
        public string s = "";
        public string g = "";
        public int shouldQty = 0;
        public int realQty = 0;
        public int shortQty = 0;
    }

    public class CJianHuoUpload
    {
        public string LGNUM = "";
        public string SHIP_DATE = "";
        public string HU = "";
        public string STATUS_IN = "";
        public string MSG_IN = "";
        public string SUBUSER = "";
        public string LOUCENG = "";
        public string EQUIP_HLA = "";

        public List<CJianHuoUploadBar> bars = new List<CJianHuoUploadBar>();
    }

    public class CJianHuoUploadBar
    {
        public string PICK_LIST = "";
        public string BARCD = "";
        public string QTY = "";
        public string DJ_QTY = "";
        public string ERR_QTY = "";
    }

    public class CJianHuoHu
    {
        public string hu = "";
        public string pick_list = "";
        public string mat = "";
        public string p = "";
        public string s = "";
        public string g = "";
        public string should_qty = "";
        public string real_qty = "";
        public string short_qty = "";
        public string opr_time = "";
    }

    public class CJiaoJieDanData
    {
        public string barcd = "";
        public string barcd_add = "";
        public int quan = 0;
        public CJiaoJieDanData() { }
        public CJiaoJieDanData(string bar,string bar_add,int qu)
        {
            barcd = bar;
            barcd_add = bar_add;
            quan = qu;
        }
    }
    public class CJiaoJieDan
    {
        public string doc = "";
        public Dictionary<string, List<CJiaoJieDanData>> huData = new Dictionary<string, List<CJiaoJieDanData>>();
    }

    public class CJJBox
    {
        public string doc = "";
        public string user = "";
        public string devno = "";
        public string loucheng = "";
        public string hu = "";
        public string inventoryRe = "";
        public string inventoryMsg = "";
        public string sapRe = "";
        public string sapMsg = "";
        public List<string> epc = new List<string>();
        public List<TagDetailInfo> tags = new List<TagDetailInfo>();
    }
    //电商接口参数
    public class CPPInfo
    {
        public string Inerfae_key;
        public string Secret;
        public string Interface_url;
        public CPPInfo(string key,string url,string sec)
        {
            Interface_url = url;
            Inerfae_key = key;
            Secret = sec;
        }
    }

    public class CDianShangDoc
    {
        public string doc = "";
        public List<CBarQty> dsData = new List<CBarQty>();
    }
    public class CDianShangBox
    {
        public string doc = "";
        public string hu = "";
        public List<TagDetailInfo> tags = new List<TagDetailInfo>();
        public List<string> epc = new List<string>();

        public string inventoryRe = "";
        public string inventoryMsg = "";
        //SUCCESS FAILURE
        public string sapRe = "";
        public string sapMsg = "";
    }
}
