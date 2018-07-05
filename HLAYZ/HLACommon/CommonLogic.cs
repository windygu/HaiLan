using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HLACommon
{
    public class CErrorMsg
    {
        public static string EPC_WEI_ZHU_CE = "";
        public static string TWO_NUMBER_ERROR = "";
        public static string XIANG_MA_BU_YI_ZHI = "";
        public static string WEI_SAO_DAO_EPC = "";

    }

    public enum READER_TYPE
    {
        READER_IMP,
        READER_TM,
        READER_DLX_PM,
        READER_XD_PM
    }
    public enum PLC_TYPE
    {
        PLC_XD,
        PLC_DLX,
        PLC_BJ
    }
    public class CheckResult
    {
        public CheckResult()
        {
            IsRecheck = false;
            InventoryResult = true;
            Message = "";
        }
        public bool IsRecheck { get; set; }

        public bool InventoryResult { get; set; }
        public string Message { get; set; }

        public void UpdateMessage(string message)
        {
            if (!Message.Contains(message))
            {
                Message += message + ";";
            }
        }

        public void Init()
        {
            IsRecheck = false;
            InventoryResult = true;
            Message = "";
        }

        public CheckResult copy()
        {
            CheckResult re = new CheckResult();
            re.IsRecheck = IsRecheck;
            re.InventoryResult = InventoryResult;
            re.Message = Message;

            return re;
        }
    }

    public class CTagDetail
    {
        public string bar;
        public string zsatnr;
        public string zcolsn;
        public string zsiztx;
        public string charg;
        public int quan;
    }

    public class CUploadData
    {
        /// <summary>
        /// GUID主键
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 要上传的数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 是否已上传 1是 0否
        /// </summary>
        public uint IsUpload { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        public string HU { get; set; }

        public string MSG { get; set; }

    }

    public class TagDetailInfo : ICloneable
    {
        /// <summary>
        /// 实际EPC信息
        /// </summary>
        public string EPC { get; set; }
        /// <summary>
        /// 主条码EPC
        /// </summary>
        public string RFID_EPC { get; set; }
        /// <summary>
        /// 辅条码EPC
        /// </summary>
        public string RFID_ADD_EPC { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 主条码
        /// </summary>
        public string BARCD { get; set; }
        /// <summary>
        /// 辅条码
        /// </summary>
        public string BARCD_ADD { get; set; }
        /// <summary>
        /// 品号
        /// </summary>
        public string ZSATNR { get; set; }
        /// <summary>
        /// 色号
        /// </summary>
        public string ZCOLSN { get; set; }

        public string ZCOLSN_WFG { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string ZSIZTX { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string CHARG { get; set; }
        /// <summary>
        /// 箱规
        /// </summary>
        public int PXQTY { get; set; }
        /// <summary>
        /// 发货箱规
        /// </summary>
        public int PXQTY_FH { get; set; }
        /// <summary>
        /// 是否为辅条码epc
        /// </summary>
        public bool IsAddEpc { get; set; }
        /// <summary>
        /// 包装材料（收货）
        /// </summary>
        public string PACKMAT { get; set; }
        /// <summary>
        /// 包装材料（发货）
        /// </summary>
        public string PACKMAT_FH { get; set; }
        /// <summary>
        /// 交接号
        /// </summary>
        public List<string> LIFNRS { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public double BRGEW { get; set; }

        public string PUT_STRA { get; set; }

        public string MAKTX { get; set; }

        public object Clone()
        {
            TagDetailInfo re = new TagDetailInfo();

            re.BARCD = BARCD;
            re.BARCD_ADD = BARCD_ADD;
            re.BRGEW = BRGEW;
            re.CHARG = CHARG;
            re.EPC = EPC;
            re.IsAddEpc = IsAddEpc;
            re.LIFNRS = LIFNRS.ToList();
            re.MATNR = MATNR;
            re.PACKMAT = PACKMAT;
            re.PACKMAT_FH = PACKMAT_FH;
            re.PXQTY = PXQTY;
            re.PXQTY_FH = PXQTY_FH;
            re.RFID_ADD_EPC = RFID_ADD_EPC;
            re.RFID_EPC = RFID_EPC;
            re.ZCOLSN = ZCOLSN;
            re.ZCOLSN_WFG = ZCOLSN_WFG;
            re.ZSATNR = ZSATNR;
            re.ZSIZTX = ZSIZTX;
            re.PUT_STRA = PUT_STRA;
            re.MAKTX = MAKTX;
            return re;
        }
    }

    public class MaterialInfo
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 品号
        /// </summary>
        public string ZSATNR { get; set; }
        /// <summary>
        /// 色号
        /// </summary>
        public string ZCOLSN { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string ZSIZTX { get; set; }
        /// <summary>
        /// 商品大类
        /// </summary>
        public string ZSUPC2 { get; set; }
        /// <summary>
        /// 箱规
        /// </summary>
        public int PXQTY { get; set; }
        /// <summary>
        /// 发货箱规
        /// </summary>
        public int PXQTY_FH { get; set; }
        /// <summary>
        /// 控制标识
        /// </summary>
        public string PUT_STRA { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public double BRGEW { get; set; }
        /// <summary>
        /// 色号流水号
        /// </summary>
        public string ZCOLSN_WFG { get; set; }
        /// <summary>
        /// 收货包材
        /// </summary>
        public string PXMAT { get; set; }
        /// <summary>
        /// 发货包材
        /// </summary>
        public string PXMAT_FH { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        public string MAKTX { get; set; }

    }

    public class HLATagInfo
    {
        public long Id { get; set; }
        /// <summary>
        /// 产品编码 
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string CHARG { get; set; }
        /// <summary>
        /// 主条码
        /// </summary>
        public string BARCD { get; set; }
        /// <summary>
        /// 辅条码
        /// </summary>
        public string BARCD_ADD { get; set; }
        /// <summary>
        /// 主条码EPC
        /// </summary>
        public string RFID_EPC { get; set; }
        /// <summary>
        /// 辅条码EPC
        /// </summary>
        public string RFID_ADD_EPC { get; set; }
        /// <summary>
        /// 失效标记（有值即为失效）
        /// </summary>
        public string BARDL { get; set; }
        /// <summary>
        /// 交接号
        /// </summary>
        public string LIFNR { get; set; }        
    }

    //电商接口参数
    public class CPPInfo
    {
        public string Inerfae_key;
        public string Secret;
        public string Interface_url;
        public CPPInfo(string key, string url, string sec)
        {
            Interface_url = url;
            Inerfae_key = key;
            Secret = sec;
        }
    }

    public class GxInfo
    {
        /// <summary>
        /// 工序编码
        /// </summary>
        public string GX_CODE { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        public string GX_NAME { get; set; }
        /// <summary>
        /// 组号
        /// </summary>
        public string VIEWGROUP { get; set; }
        /// <summary>
        /// 复核员工号
        /// </summary>
        public string VIEWUSR { get; set; }
    }

    public class DeviceTable
    {
        /// <summary>
        /// 设备终端号
        /// </summary>
        public string EQUIP_HLA { get; set; }
        /// <summary>
        /// 终端描述
        /// </summary>
        public string EQUIP_DEC { get; set; }
        /// <summary>
        /// 终端类型 
        /// </summary>
        public string EQUIP_TPE { get; set; }
        /// <summary>
        /// 终端类型描述
        /// </summary>
        public string EQUIP_TPC { get; set; }
        /// <summary>
        /// 仓库楼层
        /// </summary>
        public string LOUCENG { get; set; }
        /// <summary>
        /// 存储类型
        /// </summary>
        public List<string> LGTYP { get; set; }
        /// <summary>
        /// 是否打印(01是02否)
        /// </summary>
        public string IS_PRINT { get; set; }
        /// <summary>
        /// 是否信用(x是''否
        /// </summary>
        public string IS_NONUSE { get; set; }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string EQUIP { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK { get; set; }
        /// <summary>
        /// 库房类型（’X’箱装’G’挂装）
        /// </summary>
        public string KF_LX { get; set; }

        public List<GxInfo> GxList { get; set; }

        public List<AuthInfo> AuthList { get; set; }
    }

    public class AuthInfo
    {
        /// <summary>
        /// 权限编码
        /// A:源存储类型
        /// B:目标存储类型
        /// C:实际存储类型
        /// D:交接楼号与入库控制标识的对应关系
        /// E:退货类型对应楼层
        /// F:楼层信息
        /// </summary>
        public string AUTH_CODE { get; set; }
        /// <summary>
        /// 权限属性
        /// </summary>
        public string AUTH_VALUE { get; set; }
        /// <summary>
        /// 权限编码描述
        /// </summary>
        public string AUTH_CODE_DES { get; set; }
        /// <summary>
        /// 权限属性描述
        /// </summary>
        public string AUTH_VALUE_DES { get; set; }
        /// <summary>
        /// 设备终端号
        /// </summary>
        public string EQUIP_HLA { get; set; }
        /// <summary>
        /// 终端描述
        /// </summary>
        public string EQUIP_DEC { get; set; }
    }

}
